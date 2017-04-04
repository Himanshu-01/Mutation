using LayoutViewer.Guerilla.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public class TagLayoutGenerator
    {
        //public delegate void StatusUpdateHandler(out float progress, out string status);
        ///// <summary>
        ///// Event triggered when there is a status update during layout generation.
        ///// </summary>
        //public StatusUpdateHandler StatusUpdate;

        /// <summary>
        /// The global code scope for all of the tag layouts.
        /// </summary>
        private MutationCodeScope globalCodeScope;

        /// <summary>
        /// List of functions to pre-process tag block definitions.
        /// </summary>
        private Dictionary<string, MethodInfo> preProcessingFunctions;

        /// <summary>
        /// List of functions to post-process tag block definitions.
        /// </summary>
        private Dictionary<string, MethodInfo> postProcessingFunctions;

        public TagLayoutGenerator()
        {
            // Initialize the global code scope.
            this.globalCodeScope = new MutationCodeScope(MutationCodeCreator.MutationTagsNamespace, "", -1, MutationCodeScopeType.GlobalNamespace);

            // Cache all of the pre/post processing functions.
            CachePreProcessingFunctions();
            CachePostProcessingFunctions();
        }

        public void GenerateLayouts(GuerillaReader reader, string outputFolder)
        {
            // Check if the subfolder for block definitions exists.
            if (Directory.Exists(string.Format("{0}\\BlockDefinitions", outputFolder)) == false)
            {
                // Create the subfolder for block definitions.
                Directory.CreateDirectory(string.Format("{0}\\BlockDefinitions", outputFolder));
            }

            // Build a list of references for each tag block definition we have.
            Dictionary<string, List<TagBlockDefinition>> tagBlockReferences = PreProcessTagBlockDefinitions(reader);
            Dictionary<string, List<TagBlockDefinition>> nonUniqueDefinitions = tagBlockReferences.Where(b => b.Value.Count > 1).ToDictionary(p => p.Key, p => p.Value);

            // Initialize our list of tag definitions to process with the tag groups from the guerilla reader.
            //tag_group hlmt = reader.TagGroups.First(tag => tag.GroupTag.Equals("hlmt"));
            List<TagBlockDefinition> tagBlockDefinitions = new List<TagBlockDefinition>(reader.TagBlockDefinitions.Values.Where(block => block.IsTagGroup == true));
            //tagBlockDefinitions.Add(reader.TagBlockDefinitions[hlmt.definition_address]);

            // Loop through all of the non-unique tag blocks and add them to the list of definitions to be processed.
            foreach (TagBlockDefinition definition in reader.TagBlockDefinitions.Values)
            {
                // Check if the definition name is in the non-unique block list.
                if (nonUniqueDefinitions.Keys.Contains(definition.s_tag_block_definition.Name) == true)
                {
                    // Add the definition to the list to be extracted.
                    tagBlockDefinitions.Add(definition);
                }
            }

            // Pre-process any tag layouts that need fixups.
            for (int i = 0; i < this.preProcessingFunctions.Count; i++)
            {
                // Find the tag block definition this preprocessing function is associated with.
                TagBlockDefinition tagBlock = reader.TagBlockDefinitions.Single(
                    block => block.Value.s_tag_block_definition.Name.Equals(this.preProcessingFunctions.ElementAt(i).Key) == true).Value;

                // Invoke the preprocessing method.
                this.preProcessingFunctions.ElementAt(i).Value.Invoke(null, new object[] { tagBlock });
            }

            // Create a list of layout creators for all of the definitions we will be processing.
            List <MutationTagLayoutCreator> layoutCreators = new List<MutationTagLayoutCreator>();

            // Loop through the list of tag block definitions and create a code scope for each one.
            // This will build a list of types in the global namespace which we need before we start processing layouts.
            for (int i = 0; i < tagBlockDefinitions.Count; i++)
            {
                // Create a new tag layout creator and have it create its code scope using the tag block definition.
                MutationTagLayoutCreator layoutCreator = new MutationTagLayoutCreator(tagBlockDefinitions[i]);
                layoutCreator.CreateCodeScope(this.globalCodeScope);

                // Add the layout creator to the list.
                layoutCreators.Add(layoutCreator);
            }

            // Now that we have a type list built loop through all of the layout creators and create the actual tag layouts.
            for (int i = 0; i < layoutCreators.Count; i++)
            {
                // Generate the layout.
                layoutCreators[i].CreateTagLayout(reader, this.globalCodeScope);

                // Write it to file.
                layoutCreators[i].WriteToFile(outputFolder);
            }
        }

        #region PreProcessing

        private Dictionary<string, List<TagBlockDefinition>> PreProcessTagBlockDefinitions(GuerillaReader reader)
        {
            // Create a new reference dictionary.
            Dictionary<string, List<TagBlockDefinition>> references = new Dictionary<string, List<TagBlockDefinition>>();

            // Loop through all of the tag block definitions and preprocess each one.
            for (int i = 0; i < reader.TagGroups.Length; i++)
            {
                // Get the tag block definition for the tag group.
                TagBlockDefinition definition = reader.TagBlockDefinitions[reader.TagGroups[i].definition_address];

                // Preprocess the tag block definition.
                PreProcessTagBlockDefinitions(definition, reader, references);
            }

            // Return the references dictionary we just built.
            return references;
        }

        private void PreProcessTagBlockDefinitions(TagBlockDefinition definition, GuerillaReader reader, Dictionary<string, List<TagBlockDefinition>> references)
        {
            // Loop through all of the fields and process each one.
            foreach (tag_field field in definition.TagFields[definition.TagFieldSetLatestIndex])
            {
                // Handle the field type accordingly.
                switch (field.type)
                {
                    case field_type._field_block:
                        {
                            // Get the definition struct from the field address.
                            TagBlockDefinition def = reader.TagBlockDefinitions[field.definition_address];

                            // Format the name and check if it is already in the references dictionary.
                            if (references.Keys.Contains(def.s_tag_block_definition.Name) == true)
                            {
                                // Increment the reference count.
                                if (references[def.s_tag_block_definition.Name].Contains(definition) == false)
                                    references[def.s_tag_block_definition.Name].Add(definition);
                            }
                            else
                            {
                                // Add the block name to the references dictionary.
                                references.Add(def.s_tag_block_definition.Name, new List<TagBlockDefinition>(new TagBlockDefinition[] { definition }));

                                // Preprocess the tag block definition.
                                PreProcessTagBlockDefinitions(def, reader, references);
                            }
                            break;
                        }
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)field;

                            // Get the definition struct from the field address.
                            TagBlockDefinition def = reader.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Format the name and check if it is already in the references dictionary.
                            if (references.Keys.Contains(def.s_tag_block_definition.Name) == true)
                            {
                                // Increment the reference count.
                                if (references[def.s_tag_block_definition.Name].Contains(definition) == false)
                                    references[def.s_tag_block_definition.Name].Add(definition);
                            }
                            else
                            {
                                // Add the block name to the references dictionary.
                                references.Add(def.s_tag_block_definition.Name, new List<TagBlockDefinition>(new TagBlockDefinition[] { definition }));

                                // Preprocess the tag block definition.
                                PreProcessTagBlockDefinitions(def, reader, references);
                            }
                            break;
                        }
                }
            }
        }

        #endregion

        #region Caching Functions

        /// <summary>
        /// Caches a list of all methods in the assembly that have a GuerillaPreProcessAttribute associated with them.
        /// </summary>
        private void CachePreProcessingFunctions()
        {
            // Initialize the preprocessing function dictionary.
            this.preProcessingFunctions = new Dictionary<string, MethodInfo>();

            // Get a list of types that have the present in the current assembly.
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                // Get a list of methods for the current type that contain the GuerillaPreProcessAttribute attribute.
                MethodInfo[] methods = (from method in types[i].GetMethods(BindingFlags.Public | BindingFlags.Static)
                                        where method.IsDefined(typeof(GuerillaPreProcessAttribute)) == true
                                        select method).ToArray();

                // If there are any methods with the attribute present, add them to the preprocessing function list.
                if (methods.Length > 0)
                {
                    // Get the block name the method is associated with.
                    GuerillaPreProcessAttribute attribute = (GuerillaPreProcessAttribute)methods[0].GetCustomAttribute(typeof(GuerillaPreProcessAttribute));

                    // Add the method to the preprocessing function list.
                    this.preProcessingFunctions.Add(attribute.BlockName, methods[0]);
                }
            }
        }

        /// <summary>
        /// Caches a list of all methods in the assembly that have a GuerillaPostProcessAttribute associated with them.
        /// </summary>
        private void CachePostProcessingFunctions()
        {
            // Initialize the post processing function dictionary.
            this.postProcessingFunctions = new Dictionary<string, MethodInfo>();

            // Get a list of types that have the present in the current assembly.
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                // Get a list of methods for the current type that contain the GuerillaPostProcessAttribute attribute.
                MethodInfo[] methods = (from method in types[i].GetMethods(BindingFlags.Public | BindingFlags.Static)
                                        where method.IsDefined(typeof(GuerillaPostProcessAttribute)) == true
                                        select method).ToArray();

                // If there are any methods with the attribute present, add them to the postprocessing function list.
                if (methods.Length > 0)
                {
                    // Get the block name the method is associated with.
                    GuerillaPostProcessAttribute attribute = (GuerillaPostProcessAttribute)methods[0].GetCustomAttribute(typeof(GuerillaPostProcessAttribute));

                    // Add the method to the postprocessing function list.
                    this.postProcessingFunctions.Add(attribute.BlockName, methods[0]);
                }
            }
        }

        #endregion
    }
}
