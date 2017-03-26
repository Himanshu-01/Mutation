using HaloPlugins;
using LayoutViewer.CodeDOM;
using LayoutViewer.Guerilla;
using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutViewer.Forms
{
    public partial class PluginConverter : Form
    {
        // Background worker to handle the plugin conversion.
        BackgroundWorker conversionWorker;

        public PluginConverter()
        {
            InitializeComponent();
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            // Initialize a folder browse dialog to save the new plugins to.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Output folder";

            // Prompt the user to select an output folder.
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Set the folder path to the textbox.
                this.txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Check if the conversion worker is currently running.
            if (this.conversionWorker != null && this.conversionWorker.IsBusy == true)
            {
                // Cancel the conversion operation.
                this.conversionWorker.CancelAsync();
            }
            else
            {
                // Close the dialog.
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            // Check that the data source has been selected.
            if (this.cmbSource.SelectedIndex == -1)
            {
                // Prompt the user to select a data source.
                MessageBox.Show("Please select a data source before starting a conversion!", 
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Check if the output folder has been set.
            if (this.txtOutputFolder.Text == "")
            {
                // Prompt the user to select an output folder.
                MessageBox.Show("Please select an output folder before starting a conversion!",
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Check if there is a conversion already running.
            if (this.conversionWorker != null && this.conversionWorker.IsBusy == true)
            {
                // This should never happen, but just warn the user and return.
                MessageBox.Show("A previous conversion is still running, please wait until it finishes before starting another!", 
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Initialize the conversion worker.
            this.conversionWorker = new BackgroundWorker();
            this.conversionWorker.WorkerReportsProgress = true;
            this.conversionWorker.WorkerSupportsCancellation = true;
            this.conversionWorker.ProgressChanged += conversionWorker_ProgressChanged;
            this.conversionWorker.RunWorkerCompleted += conversionWorker_RunWorkerCompleted;

            // Set the worker function based on the data source.
            if (this.cmbSource.SelectedIndex == 0)
                this.conversionWorker.DoWork += ConvertGuerillaPlugins;
            else
                this.conversionWorker.DoWork += ConvertMutationPlugins;

            // Disable the conversion button.
            this.btnConvert.Enabled = false;

            // Run the conversion.
            this.conversionWorker.RunWorkerAsync(this.txtOutputFolder.Text);
        }

        void conversionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the status label and progress bar.
            this.lblStatus.Text = (string)e.UserState;
            this.progressBar1.Value = e.ProgressPercentage;
        }

        void conversionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable the convert button.
            this.btnConvert.Enabled = true;

            // Change the status label and clear the progress bar.
            this.lblStatus.Text = "";
            this.progressBar1.Value = 0;

            // Display a message to the user.
            MessageBox.Show((string)e.Result, "Conversion Results", MessageBoxButtons.OK);
        }

        private void ConvertMutationPlugins(object sender, DoWorkEventArgs e)
        {
            // Cast the sender object to a background worker.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Update the status label.
            worker.ReportProgress(0, "Initializing...");

            // Initialize the mutation plugin repository.
            HaloPlugins.Halo2Xbox h2xPlugins = new HaloPlugins.Halo2Xbox();

            // Loop through all of the plugins and process each one.
            for (int i = 0; i < h2xPlugins.TagExtensions.Length; i++)
            {
                // Create an instance of the current plugin definition.
                TagDefinition definition = h2xPlugins.CreateInstance(h2xPlugins.TagExtensions[i]);
            }
        }

        private void ConvertGuerillaPlugins(object sender, DoWorkEventArgs e)
        {
            // Cast the sender object to a background worker.
            BackgroundWorker worker = sender as BackgroundWorker;
            string outputFolder = e.Argument as string;

            // Update the status label.
            worker.ReportProgress(0, "Reading guerilla...");

            // Initialize a new instance of our guerilla reader and read the definitions from the executable.
            GuerillaReader reader = new GuerillaReader(Application.StartupPath + "\\H2Guerilla.exe");
            if (reader.Read() == false)
            {
                // Failed to read guerilla, return to the calling thread with an error.
                e.Result = "Error reading guerilla.exe!";
                return;
            }

            // Preprocess the tag block definitions by counting references to each one.
            Dictionary<string, List<TagBlockDefinition>> referenceList = PreProcessTagBlockDefinitions(reader);
            Dictionary<string, List<TagBlockDefinition>> nonUniqueDefinitions = referenceList.Where(b => b.Value.Count > 1).ToDictionary(p => p.Key, p => p.Value);

            // Initialize our list of tag definitions to process with the basic tag groups.
            List<TagBlockDefinition> tagDefinitions = new List<TagBlockDefinition>(reader.TagBlockDefinitions.Values.Where(block => block.IsTagGroup == true));

            // Loop through all of the non-unique tag blocks and add them to the list of definitions to be processed.
            foreach (TagBlockDefinition definition in reader.TagBlockDefinitions.Values)
            {
                // Check if the definition name is in the non-unique block list.
                if (nonUniqueDefinitions.Keys.Contains(definition.s_tag_block_definition.Name) == true)
                {
                    // Add the definition to the list to be extracted.
                    tagDefinitions.Add(definition);
                }
            }

            // Loop through all the tag definitions and extract each one.
            for (int i = 0; i < tagDefinitions.Count; i++)
            {
                // Compute the progress percentage.
                float percent = ((float)i / (float)tagDefinitions.Count) * 100.0f;

                // Create a new mutation code creator to handle the backend work for creating the class definitions.
                MutationCodeCreator codeCreator = new MutationCodeCreator();

                // Get the current tag group definition.
                worker.ReportProgress((int)percent, string.Format("Converting block: {0}", tagDefinitions[i].s_tag_block_definition.Name));

                // Create a new class for this tag block definition.
                codeCreator.CreateTagDefinitionClass(MutationCodeFormatter.FormatDefinitionName(tagDefinitions[i].s_tag_block_definition.Name), "Mutation.Halo.TagGroups.Tags");

                // Process the tag block definition.
                ProcessTagBlockDefinition(codeCreator, reader, tagDefinitions, tagDefinitions[i]);

                // Write the tag block definition code to file.
                codeCreator.WriteToFile(string.Format("{0}\\{1}.cs", outputFolder, MutationCodeFormatter.FormatDefinitionName(tagDefinitions[i].s_tag_block_definition.Name)));
            }

            // Set the worker result value.
            e.Result = (string)"Completed successfully!";
        }

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

        private void ProcessTagBlockDefinition(MutationCodeCreator codeCreator, GuerillaReader reader, List<TagBlockDefinition> nonUniqueTagBlocks, TagBlockDefinition definition)
        {
            int noNameFieldCount = 0;

            // Loop through all of the fields and process each one.
            foreach (tag_field field in definition.TagFields[definition.TagFieldSetLatestIndex])
            {
                // Process the field name formatting it properly and removing the field comment.
                string fieldName, fieldComment;
                MutationCodeFormatter.ProcessFieldName(field.Name, out fieldName, out fieldComment);

                // Make sure the field has a name.
                if (fieldName == string.Empty)
                    fieldName = string.Format("noNameField{0}", ++noNameFieldCount);

                // Check if there is a comment for this field.
                CodeCommentStatementCollection commentCollection = null;
                if (fieldComment != string.Empty)
                {
                    // Add the comment to the collection.
                    commentCollection = new CodeCommentStatementCollection();
                    commentCollection.Add(new CodeCommentStatement(new CodeComment("<summary>", true)));
                    commentCollection.Add(new CodeCommentStatement(new CodeComment(fieldComment, true)));
                    commentCollection.Add(new CodeCommentStatement(new CodeComment("</summary>", true)));
                }

                // Handle each field accordingly.
                switch (field.type)
                {
                    case field_type._field_char_enum:
                    case field_type._field_enum:
                    case field_type._field_long_enum:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)field;

                            // Create the enum field type in the class definition.
                            string enumType = codeCreator.AddEnum(enumDefinition, fieldName, false);

                            // Add a field to the code class using the enum type name.
                            codeCreator.AddField(field.type, enumType, fieldName, commentCollection);
                            break;
                        }
                    case field_type._field_byte_flags:
                    case field_type._field_word_flags:
                    case field_type._field_long_flags:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)field;

                            // Create the enum field type in the class definition.
                            string enumType = codeCreator.AddEnum(enumDefinition, fieldName, true);

                            // Add a field to the code class using the enum type name.
                            codeCreator.AddField(field.type, enumType, fieldName, commentCollection);
                            break;
                        }
                    case field_type._field_block:
                        {
                            // Get the definition struct from the field address.
                            TagBlockDefinition def = reader.TagBlockDefinitions[field.definition_address];

                            // Create a field for the tag block.
                            codeCreator.AddTagBlock(def, MutationCodeFormatter.FormatDefinitionName(def.s_tag_block_definition.Name), fieldName, commentCollection);

                            // Check the number of references on the tag block to see if we should process it or not.
                            if (nonUniqueTagBlocks.Contains(def) == false)
                            {
                                // The tag block is only referenced from within this class, so create a new code creator for it.
                                MutationCodeCreator childCodeCreator = codeCreator.CreateTagBlockClass(MutationCodeFormatter.FormatDefinitionName(def.s_tag_block_definition.Name));

                                // Process the tag block.
                                ProcessTagBlockDefinition(childCodeCreator, reader, nonUniqueTagBlocks, def);
                            }
                            break;
                        }
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)field;

                            // Get the definition struct from the field address.
                            TagBlockDefinition def = reader.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Create a field for the tag block.
                            codeCreator.AddTagBlock(def, MutationCodeFormatter.FormatDefinitionName(def.s_tag_block_definition.Name), fieldName, commentCollection);

                            // Check the number of references on the tag block to see if we should process it or not.
                            if (nonUniqueTagBlocks.Contains(def) == false)
                            {
                                // The tag block is only referenced from within this class, so create a new code creator for it.
                                MutationCodeCreator childCodeCreator = codeCreator.CreateTagBlockClass(MutationCodeFormatter.FormatDefinitionName(def.s_tag_block_definition.Name));

                                // Process the tag block.
                                ProcessTagBlockDefinition(childCodeCreator, reader, nonUniqueTagBlocks, def);
                            }
                            break;
                        }
                    case field_type._field_tag_reference:
                        {
                            // Cast the field to a tag_reference_definition definition.
                            tag_reference_definition tagRef = (tag_reference_definition)field;

                            // Setup a code type reference for the attribute type.
                            CodeTypeReference attType = new CodeTypeReference(typeof(TagReferenceAttribute).Name);

                            // Setup a TagReferenceAttribute attribute for this field with the group tag filter.
                            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(attType,
                                new CodeAttributeArgument(new CodePrimitiveExpression(Mutation.HEK.Guerilla.Guerilla.GroupTagToString(tagRef.group_tag))));

                            // Add the field with the group tag filter attribute.
                            codeCreator.AddField(field.type, fieldName, commentCollection, new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[] { attribute }));
                            break;
                        }
                    case field_type._field_pad:
                    case field_type._field_skip:
                    case field_type._field_useless_pad:
                        {
                            // Setup a code type reference for the attribute.
                            CodeTypeReference attType = new CodeTypeReference(typeof(PaddingAttribute).Name);

                            // Create an expression for the padding type parameter.
                            CodeFieldReferenceExpression attPadType = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(PaddingType).Name),
                                    PaddingAttribute.PaddingTypeFromFieldType(field.type).ToString());

                            // Setup a PaddingAttribute attribute for this field with the padding type.
                            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(attType, new CodeAttributeArgument(attPadType), 
                                new CodeAttributeArgument(new CodePrimitiveExpression(field.definition_address)));

                            // Add the field with the group tag filter attribute.
                            codeCreator.AddPaddingField(field.definition_address, new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[] { attribute }));
                            break;
                        }
                    default:
                        {
                            // Check if the value type dictionary contains this field type.
                            if (codeCreator.ValueTypeDictionary.Keys.Contains(field.type) == false)
                                continue;

                            // Add the field to the collection.
                            codeCreator.AddField(field.type, fieldName, commentCollection);
                            break;
                        }
                }
            }
        }
    }
}
