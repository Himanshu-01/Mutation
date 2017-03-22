using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    /// <summary>
    /// Encapsulates the position and size of a rectangle using Short integers (Int16).
    /// </summary>
    public class Rectangle2d : MetaNode
    {
        #region Fields

        /// <summary>
        /// Size of the Rectangle2d data type.
        /// </summary>
        public const int FieldSize = 8;

        /// <summary>
        /// Gets or sets the X position of the rectangle.
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the rectangle.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the Width of the rectangle.
        /// </summary>
        public short W { get; set; }

        /// <summary>
        /// Gets or sets the Height of the rectangle.
        /// </summary>
        public short H { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Rectangle2D object.
        /// </summary>
        /// <param name="name">Name of the Rectangle2d field.</param>
        public Rectangle2d(string name) 
            : base(name, FieldSize, FieldType.Rectangle2d, EngineManager.HaloEngine.Neutral)
        {
            // Initialize fields to default values.
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        #endregion

        #region MetaNode Members

        /// <summary>
        /// Reads the field in a decompiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        public override void Read(EndianReader br)
        {
            X = br.ReadInt16();
            Y = br.ReadInt16();
            W = br.ReadInt16();
            H = br.ReadInt16();
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
            bw.Write(W);
            bw.Write(H);
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
        /// Gets the value of this Rectangle2d object.
        /// </summary>
        /// <param name="parameters">Optional parameter to control the return value.</param>
        /// <returns>Value of this Rectangle2d object.</returns>
        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y, W, H };
        }

        /// <summary>
        /// Sets the value of this Rectangle2d object.
        /// </summary>
        /// <param name="value">New value of this Rectangle2d object.</param>
        /// <param name="parameters">Optional parameter to control the new value.</param>
        public override void SetValue(object value, params object[] parameters)
        {
            X = (short)((object[])value)[0];
            Y = (short)((object[])value)[1];
            W = (short)((object[])value)[2];
            H = (short)((object[])value)[3];
        }

        public override object Clone()
        {
            Rectangle2d r = new Rectangle2d(this.Name);
            r.X = this.X;
            r.Y = this.Y;
            r.W = this.W;
            r.H = this.H;
            return r;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            // Check that the object is not null and is of Rectangle2d type.
            if (obj != null && obj.GetType() == typeof(Rectangle2d))
            {
                // Cast the object to a Rectangle2d.
                Rectangle2d rec = (Rectangle2d)obj;

                // Check if the x, y, w, and h values match.
                if (this.X.Equals(rec.X) == true && this.Y.Equals(rec.Y) == true
                    && this.W.Equals(rec.W) == true && this.H.Equals(rec.H) == true)
                    return true;
            }

            // Objects do not match.
            return false;
        }

        public override string ToString()
        {
            return "Rectangle2d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", W: " + W.ToString() + ", H: " + H.ToString();
        }

        #endregion
    }
}
