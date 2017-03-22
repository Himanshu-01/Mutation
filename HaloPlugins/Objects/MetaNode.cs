using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloPlugins.Objects
{
    #region FieldType

    /// <summary>
    /// Used to enumerate through HaloPlugins.Objects data types.
    /// </summary>
    public enum FieldType
    {
        None = 0,

        // Color
        ColorArgb,
        ColorRgb,
        RealColorArgb,
        RealColorRgb,

        // Data
        Enum,
        Flags,
        Padding,
        Value,

        // Reference
        BlockIndex,
        InfoBlock,
        StringId,
        TagBlock,
        DatumIndex,
        TagReference,

        // Strings
        LongString,
        ShortString,
        Tag,

        // Vector
        AngleBounds,
        Point2d,
        RealAngle2d,
        RealAngle3d,
        RealBounds,
        RealPlane2d,
        RealPlane3d,
        RealPoint2d,
        RealPoint3d,
        RealQuaternion,
        RealVector2d,
        RealVector3d,
        Rectangle2d,
        ShortBounds
    }

    #endregion

    public abstract class MetaNode : ICloneable
    {
        #region Fields

        protected string name;
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get { return name; } }

        protected int fieldSize;
        /// <summary>
        /// Gets the size of the field.
        /// </summary>
        public int FieldSize { get { return fieldSize; } }

        protected FieldType fieldType = FieldType.None;
        /// <summary>
        /// Gets the HaloPlugins.Objects.FieldType this object represents.
        /// </summary>
        public FieldType FieldType { get { return fieldType; } }

        protected EngineManager.HaloEngine engine;
        /// <summary>
        /// Gets the engine version this field corresponds to.
        /// </summary>
        public EngineManager.HaloEngine Engine { get { return engine; } }

        #endregion

        #region Constructor

        public MetaNode(string name, int fieldSize, FieldType fieldType, EngineManager.HaloEngine engine)
        {
            // Set fields.
            this.name = name;
            this.fieldSize = fieldSize;
            this.fieldType = fieldType;
            this.engine = engine;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the field in a decompiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        public abstract void Read(IO.EndianReader br);

        /// <summary>
        /// Reads the field in a compiled state from the stream specified.
        /// </summary>
        /// <param name="br">Stream to read the field from.</param>
        /// <param name="magic">Virtual address modifier value for the specified stream.</param>
        public abstract void Read(IO.EndianReader br, int magic);

        /// <summary>
        /// Writes the field in a decompiled state to the stream specified.
        /// </summary>
        /// <param name="bw">Stream to write the field to.</param>
        public abstract void Write(IO.EndianWriter bw);

        /// <summary>
        /// Writes the field in a compiled state to the stream specified.
        /// </summary>
        /// <param name="bw">Stream to write the field to.</param>
        /// <param name="magic">Virtual address modifier value for the specified stream.</param>
        public abstract void Write(IO.EndianWriter bw, int magic);

        /// <summary>
        /// Gets the value of this MetaNode.
        /// </summary>
        /// <param name="parameters">Optional parameter to control the return value.</param>
        /// <returns>Value of the MetaNode.</returns>
        public abstract object GetValue(params object[] parameters);

        /// <summary>
        /// Sets the value of this MetaNode.
        /// </summary>
        /// <param name="value">New value of the MetaNode.</param>
        /// <param name="parameters">Optional parameter to control the new value.</param>
        public abstract void SetValue(object value, params object[] parameters);

        #endregion

        #region ICloneable Members

        public abstract object Clone();

        #endregion
    }
}
