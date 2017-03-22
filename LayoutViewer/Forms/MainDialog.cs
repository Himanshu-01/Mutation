using HaloPlugins;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using LayoutViewer.Guerilla;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LayoutViewer.Forms
{
    public enum ViewMode
    {
        Basic,
        Advanced
    }

    public partial class MainDialog : Form
    {
        // Reads all the information from the guerilla executable.
        GuerillaReader guerilla;

        // Background worker to handle reading the guerilla executable.
        BackgroundWorker guerillaWorker;

        // View mode for the tag groups.
        ViewMode tagGroupViewMode = ViewMode.Basic;

        // Mutation plugin repository.
        Halo2Xbox haloXbox = new Halo2Xbox();
        TagDefinition[] mutationLayouts;

        public MainDialog()
        {
            // Initialize the form.
            InitializeComponent();

            // Initialize the mutation layouts array.
            this.mutationLayouts = new TagDefinition[this.haloXbox.TagExtensions.Length];
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            // Show the form.
            this.Show();

            // Check if the local guerilla file exists.
            if (File.Exists(Application.StartupPath + "\\H2Guerilla.exe") == true)
            {
                // Initialize the guerilla reader.
                this.guerilla = new GuerillaReader(Application.StartupPath + "\\H2Guerilla.exe");

                // Initialize the guerilla worker.
                this.guerillaWorker = new BackgroundWorker();
                this.guerillaWorker.DoWork += guerillaWorker_DoWork;
                this.guerillaWorker.RunWorkerCompleted += guerillaWorker_RunWorkerCompleted;

                // Disable the form and update the status bar.
                this.lblStatus.Text = "Reading guerilla...";
                this.Enabled = false;

                // Run the worker.
                this.guerillaWorker.RunWorkerAsync(this.guerilla);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an open file dialog and setup the file filter.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "H2Guerilla.exe (*.exe)|*.exe";

            // Prompt the user for the file.
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Initialize the guerilla reader.
                this.guerilla = new GuerillaReader(ofd.FileName);

                // Initialize the guerilla worker.
                this.guerillaWorker = new BackgroundWorker();
                this.guerillaWorker.DoWork += guerillaWorker_DoWork;
                this.guerillaWorker.RunWorkerCompleted += guerillaWorker_RunWorkerCompleted;

                // Disable the form and update the status bar.
                this.lblStatus.Text = "Reading guerilla...";
                this.Enabled = false;

                // Run the worker.
                this.guerillaWorker.RunWorkerAsync(this.guerilla);
            }
        }

        void guerillaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Cast the startup parameter to a GuerillaReader object.
            GuerillaReader guerilla = e.Argument as GuerillaReader;

            // Try to read all the information from the guerilla executable.
            if (guerilla.Read() == false)
            {
                // Failed to read the guerilla executable, return false.
                e.Result = false;
                return;
            }

            // Successfully read the tag info from guerilla, return true;
            e.Result = true;
            return;
        }

        void guerillaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check the result of the worker.
            if ((bool)e.Result == false)
            {
                // Re-enable the form and update the status label.
                this.Enabled = true;
                this.lblStatus.Text = "Idle";
                return;
            }

            // Update the status label.
            this.lblStatus.Text = "Loading data...";

            // Clear any old nodes from the treeview.
            this.tvTagGroups.Nodes.Clear();

            // Loop through all the tag groups and add each one to the treeview.
            for (int i = 0; i < this.guerilla.TagGroups.Length; i++)
            {
                // Create a new treenode for the tag group.
                TreeNode node = new TreeNode(string.Format("{0} - {1}", 
                    this.guerilla.TagGroups[i].GroupTag, this.guerilla.TagGroups[i].Name));
                node.Tag = this.guerilla.TagGroups[i];

                // Recursively read the tag_block_definition and setup any child nodes.
                TreeView_ProcessTagDefinition(node, this.guerilla.TagGroups[i].definition_address);

                // Add the treenode to the treeview.
                this.tvTagGroups.Nodes.Add(node);
            }

            // Loop through all of the tag block definitions and add each one to the treeview.
            foreach (int address in this.guerilla.TagBlockDefinitions.Keys)
            {
                // Find the tag_blocK_definition structure from guerilla.
                TagBlockDefinition tagBlock = this.guerilla.TagBlockDefinitions[address];

                // Create a new tree node for it.
                TreeNode node = new TreeNode(tagBlock.s_tag_block_definition.Name);
                node.Tag = address;
                node.ToolTipText = string.Format("{0} - {1}", tagBlock.s_tag_block_definition.Name, address.ToString("x08"));

                // Loop through all the field sets for this definition.
                for (int i = 0; i < tagBlock.TagFieldSets.Length; i++)
                {
                    // Create a new treenode for this field set.
                    string structName = tagBlock.TagFieldSets[i].SizeString.Replace("sizeof(struct ", "").Replace("sizeof(", "").Replace(")", "");
                    TreeNode childNode = new TreeNode(structName);

                    // Check if this is the latest field set or not.
                    if (tagBlock.TagFieldSetLatestIndex == i)
                    {
                        childNode.Text += " (latest)";
                    }

                    // Set the tag to the definition address and field set index.
                    childNode.Tag = new object[] { address, i };
                    node.Nodes.Add(childNode);
                }

                // Add the node to the treeview.
                this.tvTagDefs.Nodes.Add(node);
            }

            // Loop through all of the tag extensions for the halo 2 xbox engine.
            for (int i = 0; i < this.haloXbox.TagExtensions.Length; i++)
            {
                // Create an instance of the tag definition
                this.mutationLayouts[i] = this.haloXbox.CreateInstance(this.haloXbox.TagExtensions[i]);

                // Create a new tree node for the tag layout.
                TreeNode node = new TreeNode(string.Format("{0} - {1}", this.haloXbox.GroupTagNames[i], this.haloXbox.TagExtensions[i]));
                node.Tag = this.mutationLayouts[i];

                // Loop through all of the fields in the layout.
                for (int x = 0; x < this.mutationLayouts[i].Fields.Count; x++)
                {
                    // Check if the field is a tab block.
                    if (this.mutationLayouts[i].Fields[x].GetType() == typeof(TagBlock))
                    {
                        // Cast the field to a TagBlock.
                        TagBlock tagBlock = (TagBlock)this.mutationLayouts[i].Fields[x];

                        // Create a new tree node for this tag block.
                        TreeNode blockNode = new TreeNode(tagBlock.Name);
                        blockNode.Tag = tagBlock;

                        // Process the fields in the tag block.
                        TreeView_ProcessMutationDefinition(blockNode, tagBlock);

                        // Add the tree node to the parent node.
                        node.Nodes.Add(blockNode);
                    }
                }

                // Add the node to the tree view.
                this.tvMutation.Nodes.Add(node);
            }

            // Sort the treeview.
            this.tvTagGroups.Sort();
            this.tvTagDefs.Sort();
            this.tvMutation.Sort();

            // Re-enable the form and update the status label.
            this.Enabled = true;
            this.lblStatus.Text = "Idle";
        }

        #region Tag Groups TreeView

        void TreeView_ProcessTagDefinition(TreeNode node, int address)
        {
            // Find the tag_blocK_definition structure from guerilla.
            TagBlockDefinition tagBlock = this.guerilla.TagBlockDefinitions[address];

            // Loop through all the field sets for this tag_block_defintion and add a treenode for each one to the parent node.
            for (int i = 0; i < tagBlock.s_tag_block_definition.field_set_count; i++)
            {
                // Pull the structure name out of the sizeof string.
                string structName = tagBlock.TagFieldSets[i].SizeString.Replace("sizeof(struct ", "").Replace("sizeof(", "").Replace(")", "");

                // Create a new treenode for the field set.
                TreeNode fieldNode = new TreeNode(structName);
                fieldNode.Tag = new object[] { address, i };

                // Check if this is the latest field set or not.
                if (tagBlock.TagFieldSetLatestIndex == i)
                {
                    // Change the color of the treenode so we know it's the latest field set.
                    //node.NodeFont = new Font(this.tvTagGroups.Font, FontStyle.Bold);
                    fieldNode.Text += " (latest)";
                }

                // Check for any nested tag_blocK_definition fields.
                foreach (tag_field field in tagBlock.TagFields[i])
                {
                    // Check the type of tag_field.
                    if (field.type == field_type._field_struct)
                    {
                        // Cast the field to a tag_struct_definition.
                        tag_struct_definition tagStruct = field as tag_struct_definition;

                        // Find the tag block definition structure from guerilla.
                        TagBlockDefinition childBlock = this.guerilla.TagBlockDefinitions[tagStruct.block_definition_address];

                        // Create a new node for this tag block.
                        TreeNode blockNode = new TreeNode(childBlock.s_tag_block_definition.Name);
                        blockNode.Tag = new object[] { tagStruct.block_definition_address, childBlock.TagFieldSetLatestIndex };

                        // Recursively read the tag_block_definition and setup a treenode for it.
                        TreeView_ProcessTagDefinition(blockNode, tagStruct.block_definition_address);

                        // Add the block node to the parent node.
                        fieldNode.Nodes.Add(blockNode);
                    }
                    else if (field.type == field_type._field_block)
                    {
                        // Check if the definition address is valid.
                        if (field.definition_address != 0)
                        {
                            // Find the tag block definition structure from guerilla.
                            TagBlockDefinition childBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            // Create a new node for this tag block.
                            TreeNode blockNode = new TreeNode(childBlock.s_tag_block_definition.Name);
                            blockNode.Tag = new object[] { field.definition_address, childBlock.TagFieldSetLatestIndex };

                            // Recursively read the tag_block_definition and setup a treenode for it.
                            TreeView_ProcessTagDefinition(fieldNode, field.definition_address);

                            // Add the block node to the parent node.
                            fieldNode.Nodes.Add(blockNode);
                        }
                    }
                }

                // Add the node to the parent node.
                node.Nodes.Add(fieldNode);
            }
        }

        private void tvTagGroups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Load the tag group/definition.
            LoadTagGroupFromTreeNode(e.Node);
        }

        private void LoadTagGroupFromTreeNode(TreeNode node)
        {
            // Get the selected treenode and check if it is null or has a tag property.
            if (node == null || node.Tag == null)
                return;

            // Clear the guerilla code view.
            this.txtGuerillaCode.Text = "";

            // Check the object type of the Tag property and handle accordingly.
            if (node.Tag.GetType() == typeof(tag_group))
            {
                // Load the tag group into the guerilla view panel.
                LoadTagGroup((tag_group)node.Tag);
            }
            else if (node.Tag.GetType() == typeof(object[]))
            {
                // Begin an update on the fast text box.
                this.txtGuerillaCode.BeginUpdate();

                // Load the tag group into the guerilla view panel.
                object[] tag = (object[])node.Tag;
                LoadTagDefinition((int)tag[0], (int)tag[1], ViewMode.Basic, false);

                // Select all the text.
                this.txtGuerillaCode.SelectAll();
                this.txtGuerillaCode.DoAutoIndent();
                this.txtGuerillaCode.SelectionStart = 0;
                this.txtGuerillaCode.SelectionLength = 0;

                // End the update.
                this.txtGuerillaCode.EndUpdate();
            }
        }

        private void LoadTagGroup(tag_group tagGroup)
        {
            // Begin an update on the fast text box.
            this.txtGuerillaCode.BeginUpdate();

            // Write the header for the layout.
            WriteGuerillaCodeLine("/*********************************************************************");
            WriteGuerillaCodeLine("* name: {0}", tagGroup.Name);
            WriteGuerillaCodeLine("* flags: {0}", tagGroup.flags);
            WriteGuerillaCodeLine("* group_tag: {0}", tagGroup.GroupTag);
            WriteGuerillaCodeLine("* parent group_tag: {0}", tagGroup.ParentGroupTag);
            WriteGuerillaCodeLine("* version: {0}", tagGroup.version);
            WriteGuerillaCodeLine("*");
            WriteGuerillaCodeLine("* postprocess_proc:\t\t0x{0}", tagGroup.postprocess_proc.ToString("x08"));
            WriteGuerillaCodeLine("* save_postprocess_proc:\t0x{0}", tagGroup.save_postprocess_proc.ToString("x08"));
            WriteGuerillaCodeLine("* postprocess_for_sync_proc:\t0x{0}", tagGroup.postprocess_for_sync_proc.ToString("x08"));
            WriteGuerillaCodeLine("**********************************************************************/");

            // Load the underlying tag_block_definition data.
            LoadTagDefinition(tagGroup.definition_address, -1, this.tagGroupViewMode, true);

            // Select all the text.
            this.txtGuerillaCode.SelectAll();
            this.txtGuerillaCode.DoAutoIndent();
            this.txtGuerillaCode.SelectionStart = 0;
            this.txtGuerillaCode.SelectionLength = 0;

            // End the update.
            this.txtGuerillaCode.EndUpdate();
        }

        #endregion

        #region Definitions TreeView

        private void tvTagDefs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get the selected treenode and check if it is null or has a tag property.
            TreeNode node = e.Node;
            if (node == null || node.Tag == null)
                return;

            // Clear the guerilla code view.
            this.txtGuerillaCode.Text = "";

            // Check the object type of the Tag property and handle accordingly.
            if (node.Tag.GetType() == typeof(object[]))
            {
                // Begin an update on the fast text box.
                this.txtGuerillaCode.BeginUpdate();

                // Load the tag group into the guerilla view panel.
                object[] tag = (object[])node.Tag;
                LoadTagDefinition((int)tag[0], (int)tag[1], ViewMode.Basic, false);

                // Select all the text.
                this.txtGuerillaCode.SelectAll();
                this.txtGuerillaCode.DoAutoIndent();
                this.txtGuerillaCode.SelectionStart = 0;
                this.txtGuerillaCode.SelectionLength = 0;

                // End the update.
                this.txtGuerillaCode.EndUpdate();
            }
        }

        #endregion

        #region Mutation TreeView

        private void TreeView_ProcessMutationDefinition(TreeNode node, TagBlock tagBlock)
        {
            // Loop through all the fields in the tag block
            for (int i = 0; i < tagBlock.definition.Length; i++)
            {
                // Check if the current field is a tag block.
                if (tagBlock.definition[i].GetType() == typeof(TagBlock))
                {
                    // Cast the field to a TagBlock.
                    TagBlock childBlock = (TagBlock)tagBlock.definition[i];

                    // Create a new tree node for this tag block.
                    TreeNode blockNode = new TreeNode(childBlock.Name);
                    blockNode.Tag = childBlock;

                    // Process the fields in the tag block.
                    TreeView_ProcessMutationDefinition(blockNode, childBlock);

                    // Add the tree node to the parent node.
                    node.Nodes.Add(blockNode);
                }
            }
        }

        private void tvMutation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get the selected treenode and check if it is null or has a tag property.
            TreeNode node = e.Node;
            if (node == null || node.Tag == null)
                return;

            // Clear the mutation code textbox.
            this.txtMutationCode.Text = "";

            // Check the tag property of the node.
            if (node.Tag.GetType().BaseType == typeof(TagDefinition))
            {
                // Get the tag definition from the tag property.
                TagDefinition tagDef = (TagDefinition)node.Tag;

                // Begin an update on the fast text box.
                this.txtMutationCode.BeginUpdate();

                // Write the header for the layout.
                WriteMutationCodeLine("/*********************************************************************");
                WriteMutationCodeLine("* name: {0}", tagDef.Name);
                WriteMutationCodeLine("* group_tag: {0}", tagDef.GroupTag);
                WriteMutationCodeLine("* header size: {0}", tagDef.HeaderSize);
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("**********************************************************************/");
                WriteMutationCodeLine("struct {0}", tagDef.DefaultExtension);
                WriteMutationCodeLine("{");

                // Load the underlying tag_block_definition data.
                LoadMutationDefinition(tagDef.Fields.ToArray(), this.tagGroupViewMode);
                WriteMutationCodeLine("}");

                // Select all the text.
                this.txtMutationCode.SelectAll();
                this.txtMutationCode.DoAutoIndent();
                this.txtMutationCode.SelectionStart = 0;
                this.txtMutationCode.SelectionLength = 0;

                // End the update.
                this.txtMutationCode.EndUpdate();
            }
            else if (node.Tag.GetType() == typeof(TagBlock))
            {
                // Get the tag block from the tag property.
                TagBlock tagBlock = (TagBlock)node.Tag;

                // Begin an update on the fast text box.
                this.txtMutationCode.BeginUpdate();

                // Write the tag definition header.
                WriteMutationCodeLine("");
                WriteMutationCodeLine("/****************************************");
                WriteMutationCodeLine("* size:\t{0}", tagBlock.DefinitionSize);
                WriteMutationCodeLine("* alignment:\t{0}", tagBlock.PaddingInterval);
                WriteMutationCodeLine("* max_count:\t{0}", tagBlock.MaxBlockCount);
                WriteMutationCodeLine("*");
                WriteMutationCodeLine("****************************************/");
                WriteMutationCodeLine("struct {0}", tagBlock.Name.ToLower().Replace(" ", "_"));
                WriteMutationCodeLine("{");

                // Load the underlying tag_block_definition data.
                LoadMutationDefinition(tagBlock.definition, ViewMode.Basic);
                WriteMutationCodeLine("}");

                // Select all the text.
                this.txtMutationCode.SelectAll();
                this.txtMutationCode.DoAutoIndent();
                this.txtMutationCode.SelectionStart = 0;
                this.txtMutationCode.SelectionLength = 0;

                // End the update.
                this.txtMutationCode.EndUpdate();
            }
        }

        #endregion

        #region Guerilla Code Parsing

        private void LoadTagDefinition(int address, int fieldIndex, ViewMode viewMode, bool isTagGroup)
        {
            // Get the tag_block_definition from the guerilla dictionary using the address.
            TagBlockDefinition tag_block = this.guerilla.TagBlockDefinitions[address];

            // Pull out the field set we are loading into the code view.
            if (fieldIndex == -1)
                fieldIndex = tag_block.TagFieldSetLatestIndex;
            tag_field_set fieldSet = tag_block.TagFieldSets[fieldIndex];

            // Create the main struct for this tag field set in the output file.
            string structName = fieldSet.SizeString.Replace("sizeof(struct ", "").Replace("sizeof(", "").Replace(")", "");
            WriteGuerillaCodeLine("");
            WriteGuerillaCodeLine("/****************************************");
            WriteGuerillaCodeLine("* size:\t{0}", fieldSet.size);
            WriteGuerillaCodeLine("* alignment:\t{0}", fieldSet.alignment_bit != 0 ? (1 << fieldSet.alignment_bit) : 0);
            if (Mutation.HEK.Guerilla.Guerilla.IsNumeric(tag_block.s_tag_block_definition.MaximumElementCount) == true)
                WriteGuerillaCodeLine("* max_count:\t{0}", tag_block.s_tag_block_definition.maximum_element_count);
            else
                WriteGuerillaCodeLine("* max_count:\t{0} = {1}", tag_block.s_tag_block_definition.MaximumElementCount, tag_block.s_tag_block_definition.maximum_element_count);
            WriteGuerillaCodeLine("*");

            // Print the proc addresses only if they exist.
            if (tag_block.s_tag_block_definition.postprocess_proc != 0)
                WriteGuerillaCodeLine("* postprocess_proc: 0x{0}", tag_block.s_tag_block_definition.postprocess_proc.ToString("x08"));
            if (tag_block.s_tag_block_definition.format_proc != 0)
                WriteGuerillaCodeLine("* format_proc: 0x{0}", tag_block.s_tag_block_definition.format_proc.ToString("x08"));
            if (tag_block.s_tag_block_definition.generate_default_proc != 0)
                WriteGuerillaCodeLine("* generate_default_proc: 0x{0}", tag_block.s_tag_block_definition.generate_default_proc.ToString("x08"));
            if (tag_block.s_tag_block_definition.dispose_element_proc != 0)
                WriteGuerillaCodeLine("* dispose_element_proc: 0x{0}", tag_block.s_tag_block_definition.dispose_element_proc.ToString("x08"));
            WriteGuerillaCodeLine("****************************************/");

            // Write the block header.
            if (isTagGroup == true)
            {
                // The selected node's Tag property is a tag_group object.
                tag_group tagGroup = (tag_group)this.tvTagGroups.SelectedNode.Tag;

                // Create the struct header.
                WriteGuerillaCodeLine("TAG_GROUP({0}, \"{1}\", {2});",
                    tag_block.s_tag_block_definition.Name, tagGroup.GroupTag, fieldSet.SizeString);
                WriteGuerillaCodeLine("struct {0}", tag_block.s_tag_block_definition.Name);
                WriteGuerillaCodeLine("{");
            }
            else
            {
                // Create the struct header.
                WriteGuerillaCodeLine("TAG_BLOCK({0}, {1}, {2});", tag_block.s_tag_block_definition.Name,
                    tag_block.s_tag_block_definition.MaximumElementCount, fieldSet.SizeString);
                WriteGuerillaCodeLine("struct {0}", tag_block.s_tag_block_definition.Name);
                WriteGuerillaCodeLine("{");
            }

            // Counters for padding variables.
            int padCount = 0;
            int uselessPadCount = 0;
            int skipCount = 0;
            int badFieldCount = 0;

            // Loop through all the fields in the tag field set.
            foreach (tag_field field in tag_block.TagFields[fieldIndex])
            {
                // Check for field terminator.
                if (field.type == field_type._field_terminator)
                    continue;

                // Process the field name.
                string fieldName, fieldComment;
                ProcessFieldName(field.Name, out fieldName, out fieldComment);

                // Check if the field name is bad.
                if (IsValidFieldName(fieldName) == false)
                    fieldName = string.Format("noNameField{0}", badFieldCount++);

                // Check the field type and handle accordingly.
                switch (field.type)
                {
                    case field_type._field_angle: WriteGuerillaField("float", fieldName, fieldComment); break;
                    case field_type._field_real: WriteGuerillaField("float", fieldName, fieldComment); break;
                    case field_type._field_real_fraction: WriteGuerillaField("float", fieldName, fieldComment); break;

                    case field_type._field_byte_flags:
                    case field_type._field_word_flags:
                    case field_type._field_long_flags:
                    case field_type._field_char_enum:
                    case field_type._field_enum:
                    case field_type._field_long_enum:
                        {
                            // Cast the field to an enum_definition.
                            enum_definition @enum = (enum_definition)field;

                            // Format the name.
                            string name = string.Format("{0}{1}", char.ToUpper(fieldName[0]), fieldName.Substring(1));

                            // Determine the typ of enum.
                            string baseType = "";
                            if (field.type == field_type._field_byte_flags || field.type == field_type._field_char_enum)
                                baseType = "byte";
                            else if (field.type == field_type._field_word_flags || field.type == field_type._field_enum)
                                baseType = "short";
                            else
                                baseType = "int";

                            // Determine if the field is a bitmask or enum.
                            bool isBitmask = false;
                            if (field.type == field_type._field_byte_flags || field.type == field_type._field_word_flags ||
                                field.type == field_type._field_long_flags)
                                isBitmask = true;

                            // Write the enum header.
                            WriteGuerillaCodeLine("");
                            WriteGuerillaCodeLine("public enum {0} : {1}", name, baseType);
                            WriteGuerillaCodeLine("{");

                            // Loop through all of the options and write each one.
                            for (int i = 0; i < @enum.option_count; i++)
                            {
                                // Check if the option has a valid name.
                                if (@enum.options[i] == "")
                                    continue;

                                // Write the option.
                                WriteGuerillaCodeLine("{0} = {1},", ProcessMemberName(@enum.options[i]), isBitmask == true ? 
                                    ("0x" + (1 << i).ToString("X")) : i.ToString());
                            }

                            // Write the field.
                            WriteGuerillaCodeLine("}");
                            WriteGuerillaField(name, fieldName, fieldComment); 
                            break;
                        }

                    case field_type._field_byte_block_flags:
                        {
                            // Get the tag block definition from the guerilla dictionary using the field definition address.
                            TagBlockDefinition flagBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            // Write the field info.
                            string attribute = string.Format("[BlockFlags(\"{0}\")]", flagBlock.s_tag_block_definition.Name);
                            WriteGuerillaField("byte", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_word_block_flags:
                        {
                            // Get the tag block definition from the guerilla dictionary using the field definition address.
                            TagBlockDefinition flagBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            // Write the field info.
                            string attribute = string.Format("[BlockFlags(\"{0}\")]", flagBlock.s_tag_block_definition.Name);
                            WriteGuerillaField("short", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_long_block_flags:
                        {
                            // Get the tag block definition from the guerilla dictionary using the field definition address.
                            TagBlockDefinition flagBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            // Write the field info.
                            string attribute = string.Format("[BlockFlags(\"{0}\")]", flagBlock.s_tag_block_definition.Name);
                            WriteGuerillaField("int", fieldName, fieldComment, attribute);
                            break;
                        }

                    case field_type._field_char_integer: WriteGuerillaField("byte", fieldName, fieldComment); break;
                    case field_type._field_short_integer: WriteGuerillaField("short", fieldName, fieldComment); break;
                    case field_type._field_long_integer: WriteGuerillaField("int", fieldName, fieldComment); break;

                    case field_type._field_char_block_index1:
                        {
                            // Get the tag_block_definition from the guerilla dictionary using the address.
                            TagBlockDefinition indexBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            string attribute = string.Format("[BlockIndex1(\"{0}\")]", indexBlock.s_tag_block_definition.Name);
                            WriteGuerillaField("byte", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_short_block_index1:
                        {
                            string attribute = string.Empty;

                            // Check if the field definition address exists in the tag block dictionary.
                            if (this.guerilla.TagBlockDefinitions.ContainsKey(field.definition_address) == true)
                            {
                                // Get the tag_block_definition from the guerilla dictionary using the address.
                                TagBlockDefinition indexBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                                // Write the field attribute.
                                attribute = string.Format("[BlockIndex1(\"{0}\")]", indexBlock.s_tag_block_definition.Name);
                            }
                            else if (field.definition_address != 0)
                            {
                                // Write the field attribute.
                                attribute = string.Format("[BlockIndex1(\"{0}\")]", "fuck");
                            }
                            WriteGuerillaField("short", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_long_block_index1:
                        {
                            // Get the tag_block_definition from the guerilla dictionary using the address.
                            TagBlockDefinition indexBlock = this.guerilla.TagBlockDefinitions[field.definition_address];

                            string attribute = string.Format("[BlockIndex1(\"{0}\")]", indexBlock.s_tag_block_definition.Name);
                            WriteGuerillaField("int", fieldName, fieldComment, attribute);
                            break;
                        }

                    case field_type._field_char_block_index2:
                        {
                            // Cast the tag_field to a block index search definition.
                            block_index_custom_search_definition indexDef = field as block_index_custom_search_definition;

                            // Write the field info.
                            string attribute = string.Format("[BlockIndex2(GetBlockProc = 0x{0}, IsValidSourceBlockProc = 0x{1})]",
                                indexDef.get_block_proc.ToString("x08"), indexDef.is_valid_source_block_proc.ToString("x08"));
                            WriteGuerillaField("byte", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_short_block_index2:
                        {
                            // Cast the tag_field to a block index search definition.
                            block_index_custom_search_definition indexDef = field as block_index_custom_search_definition;

                            // Write the field info.
                            string attribute = string.Format("[BlockIndex2(GetBlockProc = 0x{0}, IsValidSourceBlockProc = 0x{1})]", 
                                indexDef.get_block_proc.ToString("x08"), indexDef.is_valid_source_block_proc.ToString("x08"));
                            WriteGuerillaField("short", fieldName, fieldComment, attribute);
                            break;
                        }
                    case field_type._field_long_block_index2:
                        {
                            // Cast the tag_field to a block index search definition.
                            block_index_custom_search_definition indexDef = field as block_index_custom_search_definition;

                            // Write the field info.
                            string attribute = string.Format("[BlockIndex2(GetBlockProc = 0x{0}, IsValidSourceBlockProc = 0x{1})]",
                                indexDef.get_block_proc.ToString("x08"), indexDef.is_valid_source_block_proc.ToString("x08"));
                            WriteGuerillaField("int", fieldName, fieldComment, attribute);
                            break;
                        }

                    case field_type._field_angle_bounds: WriteGuerillaField("Guerilla.FieldTypes.AngleBounds", fieldName, fieldComment); break;
                    case field_type._field_short_bounds: WriteGuerillaField("Guerilla.FieldTypes.ShortBounds", fieldName, fieldComment); break;

                    case field_type._field_point_2d: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_real_bounds: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_real_euler_angles_2d: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_real_fraction_bounds: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_real_vector_2d: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_rectangle_2d: WriteGuerillaField("Vector2", fieldName, fieldComment); break;
                    case field_type._field_real_point_2d: WriteGuerillaField("Vector2", fieldName, fieldComment); break;

                    case field_type._field_real_euler_angles_3d: WriteGuerillaField("Vector3", fieldName, fieldComment); break;
                    case field_type._field_real_plane_2d: WriteGuerillaField("Vector3", fieldName, fieldComment); break;
                    case field_type._field_real_point_3d: WriteGuerillaField("Vector3", fieldName, fieldComment); break;
                    case field_type._field_real_vector_3d: WriteGuerillaField("Vector3", fieldName, fieldComment); break;

                    case field_type._field_real_plane_3d: WriteGuerillaField("Vector4", fieldName, fieldComment); break;
                    case field_type._field_real_quaternion: WriteGuerillaField("Quaternion", fieldName, fieldComment); break;

                    case field_type._field_argb_color: WriteGuerillaField("Guerilla.FieldTypes.ColorArgb", fieldName, fieldComment); break;
                    case field_type._field_real_ahsv_color: WriteGuerillaField("Guerilla.FieldTypes.RealAHSVColor", fieldName, fieldComment); break;
                    case field_type._field_real_argb_color: WriteGuerillaField("Guerilla.FieldTypes.RealArgbColor", fieldName, fieldComment); break;
                    case field_type._field_real_hsv_color: WriteGuerillaField("Guerilla.FieldTypes.RealHSVColor", fieldName, fieldComment); break;
                    case field_type._field_real_rgb_color: WriteGuerillaField("Guerilla.FieldTypes.RealRgbColor", fieldName, fieldComment); break;
                    case field_type._field_rgb_color: WriteGuerillaField("Guerilla.FieldTypes.ColorRgb", fieldName, fieldComment); break;

                    case field_type._field_long_string: WriteGuerillaField("string", fieldName, fieldComment); break;
                    case field_type._field_string: WriteGuerillaField("string", fieldName, fieldComment); break;

                    case field_type._field_block:
                        {
                            // Get the definition struct from the field address.
                            TagBlockDefinition def = this.guerilla.TagBlockDefinitions[field.definition_address];

                            // Check if we are in advanced view mode or basic.
                            if (viewMode == ViewMode.Advanced)
                            {
                                // Recursively process the tag block.
                                LoadTagDefinition(field.definition_address, -1, viewMode, false);
                            }

                            // Write a reference to the field.
                            WriteGuerillaField(def.s_tag_block_definition.Name, fieldName, fieldComment);
                            WriteGuerillaCodeLine("");
                            break;
                        }
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)field;

                            // Get the definition struct from the field address.
                            TagBlockDefinition def = this.guerilla.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Check if we are in advanced view mode or basic.
                            if (viewMode == ViewMode.Advanced)
                            {
                                // Recursively process the tag block.
                                LoadTagDefinition(tagStruct.block_definition_address, -1, viewMode, false);
                            }

                            // Write a reference to the field.
                            WriteGuerillaField(def.s_tag_block_definition.Name, fieldName, fieldComment);
                            WriteGuerillaCodeLine("");
                            break;
                        }

                    case field_type._field_old_string_id: WriteGuerillaField("Guerilla.FieldTypes.OldStringId", fieldName, fieldComment); break;
                    case field_type._field_string_id: WriteGuerillaField("Guerilla.FieldTypes.StringId", fieldName, fieldComment); break;

                    case field_type._field_tag:
                        {
                            WriteGuerillaField("Guerilla.FieldTypes.Tag", fieldName, fieldComment);
                            break;
                        }
                    case field_type._field_tag_reference:
                        {
                            // Cast the field to a tag reference definition structure.
                            tag_reference_definition tagRef = (tag_reference_definition)field;

                            // Write the field information.
                            WriteGuerillaCodeLine("[TagReference(\"{0}\")]", Mutation.HEK.Guerilla.Guerilla.GroupTagToString(tagRef.group_tag));
                            WriteGuerillaField("Guerilla.FieldTypes.TagReference", fieldName, fieldComment);
                            break;
                        }

                    case field_type._field_pad:
                        {
                            WriteGuerillaCodeLine("[PaddingSize({0})]", field.definition_address);
                            WriteGuerillaField("Guerilla.FieldTypes.Padding", string.Format("pad{0}", padCount++), fieldComment);
                            break;
                        }
                    case field_type._field_skip:
                        {
                            WriteGuerillaCodeLine("[PaddingSize({0})]", field.definition_address);
                            WriteGuerillaField("Guerilla.FieldTypes.Padding", string.Format("skip{0}", skipCount++), fieldComment);
                            break;
                        }
                    case field_type._field_useless_pad:
                        {
                            WriteGuerillaCodeLine("[PaddingSize({0})]", field.definition_address);
                            WriteGuerillaField("Guerilla.FieldTypes.Padding", string.Format("uselessPad{0}", uselessPadCount++), fieldComment);
                            break;
                        }
                    case field_type._field_data:
                        {
                            // Cast the field a a data definition structure.
                            tag_data_definition dataDef = (tag_data_definition)field;

                            // Write a field header.
                            WriteGuerillaCodeLine("/****************************************");
                            WriteGuerillaCodeLine("* definition_name: {0}", dataDef.DefinitionName);
                            WriteGuerillaCodeLine("* flags: {0}", dataDef.flags);
                            WriteGuerillaCodeLine("* alignment_bit: {0}", dataDef.alignment_bit == 0 ? 0 : (1 << dataDef.alignment_bit));
                            if (dataDef.byteswap_proc != 0) WriteGuerillaCodeLine("* byteswap_proc: 0x{0}", dataDef.byteswap_proc.ToString("x08"));
                            if (dataDef.copy_proc != 0) WriteGuerillaCodeLine("* copy_proc: 0x{0}", dataDef.copy_proc.ToString("x08"));
                            WriteGuerillaCodeLine("****************************************/");

                            // Check if the size string is a constant or number.
                            if (Mutation.HEK.Guerilla.Guerilla.IsNumeric(dataDef.MaximumSize) == true)
                            {
                                WriteGuerillaCodeLine("public const int {0} = {1};", dataDef.MaximumSize, dataDef.maximum_size);
                                WriteGuerillaCodeLine("[DataSize({0})]", dataDef.MaximumSize);
                            }
                            else
                                WriteGuerillaCodeLine("[DataSize({0})]", dataDef.maximum_size);

                            // Write the field.
                            WriteGuerillaField("byte[]", fieldName, fieldComment);
                            break;
                        }
                    case field_type._field_vertex_buffer: WriteGuerillaField("Guerilla.FieldTypes.VertexBuffer", fieldName, fieldComment); break;
                    case field_type._field_array_start:
                        {
                            // Format the structure name.
                            string structureName = string.Format("s_{0}", fieldName);

                            // Write the field header.
                            WriteGuerillaCodeLine("");
                            WriteGuerillaCodeLine("[ArrayCount({0})]", field.definition_address);
                            WriteGuerillaField(structureName, fieldName, fieldComment);

                            // Write the header for the struct.
                            WriteGuerillaCodeLine("public struct {0}", structureName);
                            WriteGuerillaCodeLine("{");
                            break;
                        }
                    case field_type._field_array_end:
                        {
                            // Write the end of the array structure.
                            WriteGuerillaCodeLine("}");
                            WriteGuerillaCodeLine("");
                            break;
                        }
                    case field_type._field_custom:
                        {
                            WriteGuerillaCodeLine("/* {0} */", Mutation.HEK.Guerilla.Guerilla.GroupTagToString(field.group_tag));
                            break;
                        }
                    case field_type._field_explanation:
                        {
                            // Get the explaination definition.
                            explaination_definition explain = field as explaination_definition;

                            // Write an explaination attribute.
                            WriteGuerillaCodeLine("[Explaination(\"{0}\", \"{1}\")]", explain.Name, explain.Explaination);
                            break;
                        }
                }
            }

            // Finish the tag_field_set struct.
            WriteGuerillaCodeLine("}");
        }

        private void ProcessFieldName(string fieldText, out string name, out string comment)
        {
            // Satisfy the compiler.
            name = string.Empty;
            comment = string.Empty;

            // Loop through the field text and process.
            char lastChar = ' ';
            bool doComment = false;
            for (int i = 0; i < fieldText.Length; i++)
            {
                // Check if we are doing the comment or not.
                if (doComment == false)
                {
                    // Check the current character.
                    if (char.IsLetterOrDigit(fieldText[i]) == true)
                    {
                        // Check if this is the first character.
                        if (i == 0)
                        {
                            // Just add the character
                            name += char.ToLower(fieldText[i]);
                        }
                        else
                        {
                            // Check the last character.
                            if (lastChar == ' ' || lastChar == ':')
                            {
                                // Capitalize the character and add it to the name.
                                name += char.ToUpper(fieldText[i]);
                            }
                            else
                                name += fieldText[i];
                        }
                    }
                    else if (fieldText[i] == '#')
                    {
                        // Switch to comment.
                        doComment = true;
                    }

                    // Save the current char as the last char.
                    lastChar = fieldText[i];
                }
                else
                {
                    // Just add the character to the comment.
                    comment += fieldText[i];
                }
            }
        }

        private string ProcessMemberName(string memberName)
        {
            // Create a temporary string.
            string name = "";

            // Loop through the name and process each character.
            char lastChar = ' ';
            for (int i = 0; i < memberName.Length; i++)
            {
                // Check the current character.
                if (char.IsLetterOrDigit(memberName[i]) == true)
                {
                    // Check if this is the first character.
                    if (i == 0)
                    {
                        // Just add the character
                        name += char.ToUpper(memberName[i]);
                    }
                    else
                    {
                        // Check the last character.
                        if (lastChar == ' ' || lastChar == ':' || lastChar == '_')
                        {
                            // Capitalize the character and add it to the name.
                            name += char.ToUpper(memberName[i]);
                        }
                        else
                            name += memberName[i];
                    }
                }

                // Save the current char as the last char.
                lastChar = memberName[i];
            }

            // Return the processed name
            return name;
        }

        private bool IsValidFieldName(string value)
        {
            string[] invalidNames = new[] 
            { 
                "EMPTY_STRING",
                "EMPTYSTRING", 
                "", 
                "YOUR MOM"
            };
            return !invalidNames.Any(x => value.Equals(x));
        }

        private void WriteGuerillaCodeLine(string text)
        {
            // Append the line to the code view.
            this.txtGuerillaCode.AppendText(text + "\r\n");
        }

        private void WriteGuerillaCodeLine(string format, params object[] values)
        {
            // Append the line to the code view.
            this.txtGuerillaCode.AppendText(string.Format(format, values) + "\r\n");
        }

        private void WriteGuerillaField(string type, string name, string comment, string attribute = "")
        {
            // Check if there is a comment.
            if (comment != string.Empty)
            {
                WriteGuerillaCodeLine("/// <summary>");
                WriteGuerillaCodeLine("/// {0}", comment);
                WriteGuerillaCodeLine("/// </summary>");
            }

            // Check if there is an attribute to be written.
            if (attribute != string.Empty)
                WriteGuerillaCodeLine(attribute);

            // Write the value.
            WriteGuerillaCodeLine("public {0} {1};", type, name);
        }

        #endregion

        #region Mutation Code Parsing

        private void LoadMutationDefinition(MetaNode[] fields, ViewMode viewMode)
        {
            // Temporary counters for padding fields.
            int padCount = 0;

            // Loop through all the fields in the array.
            foreach (MetaNode field in fields)
            {
                // Process the field name and comments.
                string fieldName, fieldComment;
                ProcessFieldName(field.Name, out fieldName, out fieldComment);

                // Check the field type and handle accordingly.
                switch (field.FieldType)
                {
                    case FieldType.AngleBounds: WriteMutationField("Guerilla.FieldTypes.AngleBounds", fieldName, fieldComment); break;
                    case FieldType.BlockIndex: WriteMutationField("Guerilla.FieldTypes.BlockIndex", fieldName, fieldComment); break;
                    case FieldType.ColorArgb: WriteMutationField("Guerilla.FieldTypes.ColorArgb", fieldName, fieldComment); break;
                    case FieldType.ColorRgb: WriteMutationField("Guerilla.FieldTypes.ColorRgb", fieldName, fieldComment); break;
                    case FieldType.DatumIndex: WriteMutationField("Guerilla.FieldTypes.DatumIndex", fieldName, fieldComment); break;
                    case FieldType.Enum:
                    case FieldType.Flags:
                        {
                            // Format the name.
                            string name = string.Format("{0}{1}", char.ToUpper(fieldName[0]), fieldName.Substring(1));

                            // Determine the typ of enum.
                            string baseType = "";
                            if (field.FieldSize == 1)
                                baseType = "byte";
                            else if (field.FieldSize == 2)
                                baseType = "short";
                            else
                                baseType = "int";

                            // Determine if the field is a bitmask or enum.
                            bool isBitmask = field.FieldType == FieldType.Flags;

                            // Write the enum header.
                            WriteMutationCodeLine("");
                            WriteMutationCodeLine("public enum {0} : {1}", name, baseType);
                            WriteMutationCodeLine("{");

                            // Get the list of options from the field.
                            string[] options;
                            if (isBitmask == true)
                                options = ((Flags)field).Options;
                            else
                                options = ((HaloPlugins.Objects.Data.Enum)field).Options;

                            // Loop through all of the options and write each one.
                            for (int i = 0; i < options.Length; i++)
                            {
                                // Check if the option has a valid name.
                                if (options[i] == "")
                                    continue;

                                // Write the option.
                                WriteMutationCodeLine("{0} = {1},", ProcessMemberName(options[i]), isBitmask == true ? 
                                    ("0x" + (1 << i).ToString("X")) : i.ToString());
                            }

                            // Write the field.
                            WriteMutationCodeLine("}");
                            WriteMutationField(name, fieldName, fieldComment);
                            break;
                        }
                    case FieldType.LongString:
                    case FieldType.ShortString: WriteMutationField("string", fieldName, fieldComment); break;
                    case FieldType.Padding:
                        {
                            WriteMutationCodeLine("[PaddingSize({0})]", field.FieldSize);
                            WriteMutationField("Guerilla.FieldTypes.Padding", string.Format("pad{0}", padCount++), "");
                            break;
                        }
                    case FieldType.Point2d: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.RealAngle2d: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.RealAngle3d: WriteMutationField("Vector3", fieldName, fieldComment); break;
                    case FieldType.RealBounds: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.RealColorArgb: WriteMutationField("Guerilla.FieldTypes.RealArgbColor", fieldName, fieldComment); break;
                    case FieldType.RealColorRgb: WriteMutationField("Guerilla.FieldTypes.RealRgbColor", fieldName, fieldComment); break;
                    case FieldType.RealPlane2d: WriteMutationField("Vector3", fieldName, fieldComment); break;
                    case FieldType.RealPlane3d: WriteMutationField("Vector4", fieldName, fieldComment); break;
                    case FieldType.RealPoint2d: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.RealPoint3d: WriteMutationField("Vector3", fieldName, fieldComment); break;
                    case FieldType.RealQuaternion: WriteMutationField("Quaternion", fieldName, fieldComment); break;
                    case FieldType.RealVector2d: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.RealVector3d: WriteMutationField("Vector3", fieldName, fieldComment); break;
                    case FieldType.Rectangle2d: WriteMutationField("Vector2", fieldName, fieldComment); break;
                    case FieldType.ShortBounds: WriteMutationField("Guerilla.FieldTypes.ShortBounds", fieldName, fieldComment); break;
                    case FieldType.StringId: WriteMutationField("Guerilla.FieldTypes.StringId", fieldName, fieldComment); break;
                    case FieldType.Tag: WriteMutationField("Guerilla.FieldTypes.Tag", fieldName, fieldComment); break;
                    case FieldType.TagBlock:
                        {
                            // Cast the field to a tag block field.
                            TagBlock tagBlock = (TagBlock)field;

                            // Check if we are in basic view mode or advanced.
                            if (viewMode == ViewMode.Basic)
                            {
                                // Write the field info.
                                WriteMutationField(tagBlock.Name.ToLower().Replace(" ", "_"), fieldName, fieldComment);
                            }
                            else
                            {
                                // Write the tag definition header.
                                WriteMutationCodeLine("");
                                WriteMutationCodeLine("/****************************************");
                                WriteMutationCodeLine("* size:\t{0}", tagBlock.DefinitionSize);
                                WriteMutationCodeLine("* alignment:\t{0}", tagBlock.PaddingInterval);
                                WriteMutationCodeLine("* max_count:\t{0}", tagBlock.MaxBlockCount);
                                WriteMutationCodeLine("*");
                                WriteMutationCodeLine("****************************************/");
                                WriteMutationCodeLine("struct {0}", tagBlock.Name.ToLower().Replace(" ", "_"));
                                WriteMutationCodeLine("{");

                                // Load the underlying tag_block_definition data.
                                LoadMutationDefinition(tagBlock.definition, viewMode);
                                WriteMutationCodeLine("}");
                                // Write the field info.
                                WriteMutationField(tagBlock.Name.ToLower().Replace(" ", "_"), fieldName, fieldComment);
                            }
                            break;
                        }
                    case FieldType.TagReference:
                        {
                            // Cast the field to a tag reference field.
                            TagReference tagRef = (TagReference)field;

                            // Write the field information.
                            WriteMutationCodeLine("[TagReference(\"{0}\")]", tagRef.GroupTag);
                            WriteMutationField("Guerilla.FieldTypes.TagReference", fieldName, fieldComment);
                            break;
                        }
                    case FieldType.Value:
                        {
                            // Cast the field to a value field.
                            Value value = (Value)field;

                            // Check the value type.
                            if (value.DataType == typeof(byte))
                                WriteMutationField("byte", fieldName, fieldComment);
                            else if (value.DataType == typeof(short))
                                WriteMutationField("short", fieldName, fieldComment);
                            else if (value.DataType == typeof(ushort))
                                WriteMutationField("ushort", fieldName, fieldComment);
                            else if (value.DataType == typeof(int))
                                WriteMutationField("int", fieldName, fieldComment);
                            else if (value.DataType == typeof(uint))
                                WriteMutationField("uint", fieldName, fieldComment);
                            else if (value.DataType == typeof(float))
                                WriteMutationField("float", fieldName, fieldComment);
                            break;
                        }
                }
            }
        }

        private void WriteMutationCodeLine(string text)
        {
            // Append the line to the code view.
            this.txtMutationCode.AppendText(text + "\r\n");
        }

        private void WriteMutationCodeLine(string format, params object[] values)
        {
            // Append the line to the code view.
            this.txtMutationCode.AppendText(string.Format(format, values) + "\r\n");
        }

        private void WriteMutationField(string type, string name, string comment)
        {
            // Check if there is a comment.
            if (comment != string.Empty)
            {
                WriteMutationCodeLine("/// <summary>");
                WriteMutationCodeLine("/// {0}", comment);
                WriteMutationCodeLine("/// </summary>");
            }

            // Write the value.
            WriteMutationCodeLine("public {0} {1};", type, name);
        }

        #endregion

        #region View Menu

        private void btnTagGroupBasic_Click(object sender, EventArgs e)
        {
            // Check if we are switching view modes.
            if (this.tagGroupViewMode == ViewMode.Basic)
                return;

            // Check the other button
            this.btnTagGroupBasic.Checked = true;
            this.btnTagGroupAdvanced.Checked = false;

            // Set the new view mode.
            this.tagGroupViewMode = ViewMode.Basic;

            // Check if the tag groups treeview is active.
            if (this.tabControl1.SelectedIndex == 0)
            {
                // Reload the tag group.
                LoadTagGroupFromTreeNode(this.tvTagGroups.SelectedNode);
            }
        }

        private void btnTagGroupAdvanced_Click(object sender, EventArgs e)
        {
            // Check if we are switching view modes.
            if (this.tagGroupViewMode == ViewMode.Advanced)
                return;

            // Check the other button
            this.btnTagGroupBasic.Checked = false;
            this.btnTagGroupAdvanced.Checked = true;

            // Set the new view mode.
            this.tagGroupViewMode = ViewMode.Advanced;

            // Check if the tag groups treeview is active.
            if (this.tabControl1.SelectedIndex == 0)
            {
                // Reload the tag group.
                LoadTagGroupFromTreeNode(this.tvTagGroups.SelectedNode);
            }
        }

        #endregion

        private void upgradeFromMutationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialize a new open file dialog to search for the file(s) to upgrade.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Mutation Plugin Files (*.cs)|*.cs";
            ofd.Multiselect = true;

            // Prompt the user to select the file(s) to upgrade.
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            // Initialize a folder browse dialog to save the new plugins to.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Output folder";

            // Prompt the user to select an output folder.
            if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            //
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginConverter converter = new PluginConverter();
            converter.ShowDialog();
        }
    }
}
