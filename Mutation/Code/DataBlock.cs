using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mutation
{
    public enum DataType
    {
        SoundRaw,
        ModelRaw,
        BSP1,
        BSP2,
        LightmapRaw,
        BSP3,
        BSP4,
        Weather,
        Particle,
        Decorator,
        Coconut,
        Animation,
        BSPMeta,
        LightmapMeta,
        StringID,
        FileTable,
        Unicode,
        BitmapRaw,
        Meta
    }

    public class DataBlock
    {
        public string Name;
        public int Offset = 0, Size = 0;
        public DataType Type;
    }
}
