using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    /// <summary>
    /// Encapsulates a floating point vector3 using floating point integers (Single).
    /// </summary>
    public class RealVector3d : MetaNode
    {
        #region Fields

        /// <summary>
        /// The size of the RealVector3d data type.
        /// </summary>
        public const int FieldSize = 12;

        /// <summary>
        /// Gets or sets the X coordinate of the vector.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the vector.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate of the vector.
        /// </summary>
        public float Z { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new RealVector3d object.
        /// </summary>
        /// <param name="name">Name of the RealVector3d field.</param>
        public RealVector3d(string name) 
            : base(name, FieldSize, FieldType.RealVector3d, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        #endregion

        #region MetaNode Members

        /// <summary>
        /// Reads the field in a decompiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        public override void Read(EndianReader br)
        {
            Z = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
        }

        /// <summary>
        /// Reads the field in a compiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        /// <param name="magic">Virtual address modifier value for the specified stream.</param>
        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        /// <summary>
        /// Writes the field in a decompiled state to the stream specified.
        /// </summary>
        /// <param name="bw">Stream to write the field to.</param>
        public override void Write(EndianWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
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
        /// Gets the value of this RealVector3d object.
        /// </summary>
        /// <param name="parameters">Optional parameter to control the return value.</param>
        /// <returns>Value of this RealVector3d object.</returns>
        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y, Z };
        }

        /// <summary>
        /// Sets the value of this RealVector3d object.
        /// </summary>
        /// <param name="value">New value of this RealVector3d object.</param>
        /// <param name="parameters">Optional parameter to control the new value.</param>
        public override void SetValue(object value, params object[] parameters)
        {
            X = (float)((object[])value)[0];
            Y = (float)((object[])value)[1];
            Z = (float)((object[])value)[2];
        }

        public override object Clone()
        {
            RealVector3d v = new RealVector3d(this.Name);
            v.X = this.X;
            v.Y = this.Y;
            v.Z = this.Z;
            return v;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            // Check the object is not null and is of type RealVector3d.
            if (obj != null && obj.GetType() == typeof(RealVector3d))
            {
                // Cast it to a RealVector3d object.
                RealVector3d vec = (RealVector3d)obj;

                // Check that the X, Y, and Z coords match
                if (this.X.Equals(vec.X) == true && this.Y.Equals(vec.Y) == true 
                    && this.Z.Equals(vec.Z) == true)
                    return true;
            }

            // The objects do not match.
            return false;
        }

        public override string ToString()
        {
            return "RealVector2d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", Z: " + Z.ToString();
        }

        #endregion
    }
}
