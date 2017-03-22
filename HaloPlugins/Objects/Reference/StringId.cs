using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class StringId : MetaNode
    {
        /// <summary>
        /// Size of the StringId data type.
        /// </summary>
        public const int FieldSize = 4;

        private Blam.Objects.DataTypes.string_id handle;
        /// <summary>
        /// Gets the 32-bit string handle.
        /// </summary>
        public Blam.Objects.DataTypes.string_id Handle { get { return this.handle; } }

        private string stringConstant;
        /// <summary>
        /// Gets the string constant value of this string_id.
        /// </summary>
        public string StringConstant { get { return stringConstant; } }

        #region Constructor

        public StringId(string name)
            : base(name, FieldSize , FieldType.StringId, EngineManager.HaloEngine.Halo2)
        {
            // Set fields to default values.
            this.handle = Blam.Objects.DataTypes.string_id._string_id_empty;
            stringConstant = "";
        }

        public StringId(string name, EngineManager.HaloEngine engine) 
            : base(name, FieldSize, FieldType.StringId, engine)
        {
            // Set fields to default values.
            this.handle = Blam.Objects.DataTypes.string_id._string_id_empty;
            stringConstant = "";
        }

        #endregion

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            // Read a tag_string from the stream.
            this.stringConstant = new string(br.ReadChars(32)).Replace("\0", "");
        }

        public override void Read(EndianReader br, int Magic)
        {
            // Read the 32bit string_id value from the stream.
            this.handle = br.ReadUInt32();

            // Check if the string_id is not invalid.
            if (this.handle != Blam.Objects.DataTypes.string_id._string_id_invalid)
            {
                // Resolve the string constant.
                this.stringConstant = Blam.TagFiles.TagGroups.string_id_get_string_const(this.handle);
            }
        }

        public override void Write(EndianWriter bw)
        {
            // Write a tag_string to the stream.
            bw.WriteNullTerminatingString(this.stringConstant, 128);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            // Add the string to the string_id_globals table.
            this.handle = Blam.TagFiles.TagGroups._CreateStringId(this.stringConstant);

            // Write the string handle to the steam.
            bw.Write((uint)this.handle);
        }

        public override object GetValue(params object[] parameters)
        {
            return StringConstant;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            stringConstant = (string)value;
        }

        public override object Clone()
        {
            StringId s = new StringId(this.Name, base.Engine);
            s.handle = this.handle;
            s.stringConstant = this.stringConstant;
            return s;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "StringId: " + Name + ", StringHandle: " + ((uint)this.handle).ToString("X") + ", StringConstant: " + StringConstant;
        }

        #endregion
    }
}
