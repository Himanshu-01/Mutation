using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portal.Object;
using HaloControls;
using System.IO;
using Global;
using HaloObjects;

namespace Portal
{
    public class ObjectProperties
    {
        public Variant[] Variants;

        public ObjectProperties(string TagPath)
        {
            // Open
            Plugin p = new Plugin("object_properties", null);
            p.Layout.Read(new BinaryReader(new FileStream(Globals.p.Directory + TagPath.Replace(".render_model", ".object_properties"), FileMode.Open, FileAccess.Read, FileShare.Read)));

            // Read
            Variants = new Variant[((HaloObjects.H2XTagBlock)p.Layout.Fields[16]).GetChunkCount()];
            for (int i = 0; i < Variants.Length; i++)
            {
                ((HaloObjects.H2XTagBlock)p.Layout.Fields[16]).ChangeIndex(i);
                Variants[i] = new Variant();
                Variants[i].Read((IMetaNode[])p.Layout.Fields[16].GetValue());
            }
        }
    }
}
