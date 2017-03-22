using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    /// <summary>
    /// Encapsulates a quaternion using four floating point integers (Single).
    /// </summary>
    public class RealQuaternion : MetaNode
    {
        #region Fields

        /// <summary>
        /// Size of the RealQuaternion data type.
        /// </summary>
        public const int FieldSize = 16;

        /// <summary>
        /// Gets or sets the X coordinate of the quaternion.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the quaternion.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate of the quaternion.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the W coordinate of the quaternion.
        /// </summary>
        public float W { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new RealQuaternion object.
        /// </summary>
        /// <param name="name">Name of the RealQuaternion field.</param>
        public RealQuaternion(string name) 
            : base(name, FieldSize, FieldType.RealQuaternion, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0F;
            W = 0.0f;
        }

        #endregion

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            W = br.ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            bw.Write(W);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y, Z, W };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            X = (float)((object[])value)[0];
            Y = (float)((object[])value)[1];
            Z = (float)((object[])value)[2];
            W = (float)((object[])value)[3];
        }

        public override object Clone()
        {
            RealQuaternion q = new RealQuaternion(this.Name);
            q.X = this.X;
            q.Y = this.Y;
            q.Z = this.Z;
            q.W = this.W;
            return q;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealQuaternion: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", Z: " + Z.ToString() + ", W: " + W.ToString();
        }

        #endregion
    }
}
