using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    /// <summary>
    /// Encapsulates a to-from range using Short integers (Int16).
    /// </summary>
    public class ShortBounds : MetaNode
    {
        #region Fields

        /// <summary>
        /// Size of the ShortBounds data type.
        /// </summary>
        public const int FieldSize = 4;

        /// <summary>
        /// Lower limit of the range.
        /// </summary>
        public short To { get; set; }

        /// <summary>
        /// Upper limit of the range.
        /// </summary>
        public short From { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new ShortBounds object.
        /// </summary>
        /// <param name="name">Name of the ShortBounds field.</param>
        public ShortBounds(string name) 
            : base (name, FieldSize, FieldType.ShortBounds, EngineManager.HaloEngine.Neutral)
        {
            // Set the fields to default values.
            To = 0;
            From = 0;
        }

        #endregion

        #region MetaNode Members

        /// <summary>
        /// Reads the field in a decompiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        public override void Read(EndianReader br)
        {
            To = br.ReadInt16();
            From = br.ReadInt16();
        }

        /// <summary>
        /// Reads the field in a compiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        /// <param name="magic">Virtual address modifier value for the specified stream.</param>
        public override void Read(EndianReader br, int magic)
        {
            Read(br);
        }

        /// <summary>
        /// Writes the field in a decompiled state to the stream specified.
        /// </summary>
        /// <param name="bw">Stream to write the field to.</param>
        public override void Write(EndianWriter bw)
        {
            bw.Write(To);
            bw.Write(From);
        }

        /// <summary>
        /// Writes the field in a compiled state to the stream specified.
        /// </summary>
        /// <param name="bw">Stream to write the field to.</param>
        /// <param name="magic">Virtual address modifier value for the specified stream.</param>
        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        /// <summary>
        /// Gets the value of this ShortBounds object.
        /// </summary>
        /// <param name="parameters">Optional parameter to control the return value.</param>
        /// <returns>Value of this ShortBounds object.</returns>
        public override object GetValue(params object[] parameters)
        {
            return new object[] { To, From };
        }

        /// <summary>
        /// Sets the value of this ShortBounds object.
        /// </summary>
        /// <param name="value">New value of this ShortBounds object.</param>
        /// <param name="parameters">Optional parameter to control the new value.</param>
        public override void SetValue(object value, params object[] parameters)
        {
            To = (short)((object[])value)[0];
            From = (short)((object[])value)[1];
        }

        public override object Clone()
        {
            ShortBounds s = new ShortBounds(this.Name);
            s.To = this.To;
            s.From = this.From;
            return s;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            // Check that the parameters is not null, and is of the same data type.
            if (obj != null && obj.GetType() == typeof(ShortBounds))
            {
                // Cast it to a ShortBounds object.
                ShortBounds other = (ShortBounds)obj;

                // Check if the range matches for both objects.
                if (this.To.Equals(other.To) == true && this.From.Equals(other.From) == true)
                    return true;
            }

            // Objects do not match.
            return false;
        }

        public override string ToString()
        {
            return "ShortBounds: " + Name + ", To: " + To.ToString() + ", From: " + From.ToString();
        }

        #endregion
    }
}
