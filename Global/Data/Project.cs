using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Global.Data
{
    [System.Serializable()]
    public class Project
    {
        #region Enums

        public enum ProjectType
        {
            Standalone,
            Resource,
            Decompile,
            None
        }

        #endregion

        /// <summary>
        /// The full path to the main project file
        /// </summary>
        [System.NonSerialized()]
        private string fileName;
        public string FileName { get { return fileName; } }

        /// <summary>
        /// The directory this project is located in
        /// </summary>
        public string Directory { get { return FileName.Substring(0, FileName.LastIndexOf("\\") + 1); } }

        private string name;
        /// <summary>
        /// The name of the project
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        private string engine;
        /// <summary>
        /// The halo engine this project is for
        /// </summary>
        public string Engine { get { return engine; } set { engine = value; } }

        private string build;
        /// <summary>
        /// Build version info
        /// </summary>
        public string Build { get { return build; } set { build = value; } }

        private ProjectType type;
        /// <summary>
        /// Type of project
        /// </summary>
        public ProjectType Type { get { return type; } set { type = value; } }

        /// <summary>
        /// Gets the directory the tags for this project are located in
        /// </summary>
        public string TagFolder { get { return Directory + "Tags\\"; } }

        /// <summary>
        /// Gets the directory the output files for this project are located in
        /// </summary>
        public string BinFolder { get { return Directory + "Bin\\"; } }

        private Global.Misc.SerializableDictionary<string, string> projectFiles;
        /// <summary>
        /// Gets a collection of miscelaneous project files
        /// </summary>
        public Global.Misc.SerializableDictionary<string, string> ProjectFiles { get { return projectFiles; } set { projectFiles = value; } }

        private List<string> tags;
        /// <summary>
        /// Gets a list of tags that belong to this project
        /// </summary>
        public List<string> Tags { get { return tags; } set { tags = value; } }

        Project() 
        { 
            // Initialize fields to the default values
            fileName = "";
            name = "";
            engine = "";
            build = "";
            type = ProjectType.None;

            // Initialize out list type objects
            projectFiles = new Global.Misc.SerializableDictionary<string, string>();
            tags = new List<string>();
        }

        public static Project Open(string fileName)
        {
            // Check that the file exists
            if (!File.Exists(fileName))
            {
                // Print an error message and return null
                Console.WriteLine("Project file \"{0}\" does not exist!", fileName);
                return null;
            }

            // Create a new stream reader and deserialize the project file
            Project project = null;
            using (StreamReader reader = new StreamReader(fileName))
            {
                // Initialize our serializabler
                XmlSerializer mySerializer = new XmlSerializer(typeof(Project));

                // Deserialize the project file into a Project object
                project = (Project)mySerializer.Deserialize(reader);

                // Set the project file name
                project.fileName = fileName;
            }

            // Return the project object
            return project;
        }

        public static Project Create(string Directory, string Name, string Engine)
        {
            // Create new Project
            Project project = new Project();

            // Sanity check
            if (Name == "")
                throw new Exception("what the fuck");

            // Set project info
            project.fileName = string.Format("{0}\\{1}.hpr", Directory, Name);
            project.Name = Name;
            project.Engine = Engine;
            project.Build = string.Format("{0}.{1}.{2}.0", DateTime.Now.Year % 2000, DateTime.Now.Month, DateTime.Now.Day);

            // Create Folders
            System.IO.Directory.CreateDirectory(project.TagFolder);
            System.IO.Directory.CreateDirectory(project.BinFolder);

            // Deserialize the project
            project.Save(null);

            // Return the new project
            return project;
        }

        public void Save(string fileName)
        {
            // Check if we are saving it to a different location
            string savePath = this.FileName;
            if (fileName != null)
                savePath = fileName;

            // Create a new stream writer to serialize our project to
            using (StreamWriter writer = new StreamWriter(savePath, false))
            {
                // Initialize our serializabler
                XmlSerializer mySerializer = new XmlSerializer(typeof(Project));

                // Serialize this project to a file
                mySerializer.Serialize(writer, this);
            }
        }

        public int AddTag(string Tag)
        {
            // Create directorys
            string Path = this.Directory + this.TagFolder;
            string[] Folders = Tag.Split(new string[] { "\\" }, StringSplitOptions.None);
            for (int i = 0; i < Folders.Length - 1; i++)
            {
                Path += "\\" + Folders[i] + "\\";
                if (!System.IO.Directory.Exists(Path))
                    System.IO.Directory.CreateDirectory(Path);
            }

            // Copy the Tag
            string Name = Tag.Substring(Tag.LastIndexOf("\\") + 1);
            File.Copy(Tag, Path + Name);

            // Add to project
            Tags.Add(this.Directory + this.TagFolder + Name);
            int Index = Tags.Count - 1;

            // Parse tag def

            return -1;
        }
    }
}
