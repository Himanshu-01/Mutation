using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDS;
using HaloControls;
using System.IO;
using Global;
using HaloObjects;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal
{
    public enum AlphaType
    {
        None,
        AlphaTest,
        AlphaBlend,

    }

    public class Shader
    {
        bool Initialized = false;
        public Bitmap MainBitmap, BumpMap, CubeMap, NormalMap;
        public Texture MainTexture, BumpTexture, CubeTexture, NormalTexture;
        public AlphaType Alpha = AlphaType.None;
        public Vector3 PrimaryDetialScale, SecondaryDetailScale;

        public Shader(string TagPath)
        {
            // Save the old Plugin
            TagDefinition oldplugin = (TagDefinition)Globals.Plugin;

            // Load the new one
            Plugin p = new Plugin(TagPath.Substring(TagPath.LastIndexOf(".") + 1), null);
            p.Layout.Read(new BinaryReader(new FileStream(Globals.p.Directory + TagPath, FileMode.Open, FileAccess.Read, FileShare.Read)));
            TagDefinition shad = ((TagDefinition)Globals.Plugin);

            #region Alpha Blending Properties

            string Stem = (string)shad.Fields[0].GetValue();
            if (Stem.Contains("alpha") || Stem.Contains("water"))
                Alpha = AlphaType.AlphaBlend;
            else if (Stem.Contains("alphatest"))
                Alpha = AlphaType.AlphaTest;

            #endregion

            #region Illumination Properties

            IMetaNode[] IlluminationProperties = (IMetaNode[])shad.Fields[2].GetValue();
            string Bitmap = "";

            // If the main bitmap exists
            if (((HaloObjects.H2XTagBlock)shad.Fields[2]).GetChunkCount() > 0 && ((string)IlluminationProperties[0].GetValue()) != "Null Reference")
                Bitmap = Globals.p.Directory + (string)IlluminationProperties[0].GetValue();

            // Open and Read the Bitmap
            if (File.Exists(Bitmap))
                MainBitmap = new Bitmap(Bitmap);

            #endregion

            #region Shader Properties

            IMetaNode[] Properties = (IMetaNode[])shad.Fields[6].GetValue();

            // Bitmaps
            HaloObjects.H2XTagBlock Bitmaps = (HaloObjects.H2XTagBlock)Properties[1];
            for (int i = 0; i < Bitmaps.GetChunkCount(); i++)
            {
                // Get Fields
                Bitmaps.ChangeIndex(i);
                IMetaNode[] Fields = (IMetaNode[])Bitmaps.GetValue();
                Bitmap = (string)Fields[0].GetValue();

                // Figure out what Bitmap this is
                if (Bitmap.Contains("_bump"))
                    BumpMap = new Bitmap(Globals.p.Directory + Bitmap);
                else if (Bitmap.Contains("_cube_map"))
                    CubeMap = new Bitmap(Globals.p.Directory + Bitmap);
                else if (!Bitmap.Contains("default_") && !Bitmap.Contains("reflection_maps") && MainBitmap == null)
                    MainBitmap = new Bitmap(Globals.p.Directory + Bitmap);
            }

            // Double Check
            if (MainBitmap == null)
            {
                Bitmaps.ChangeIndex(2);
                IMetaNode[] Fields = (IMetaNode[])Bitmaps.GetValue();
                MainBitmap = new Bitmap(Globals.p.Directory + (string)Fields[0].GetValue());
            }

            #endregion

            // Reset Plugin
            Globals.Plugin = oldplugin;
        }

        public void InitializeTextures(Device device)
        {
            if (!Initialized)
            {
                Initialized = true;
                if (MainBitmap != null)
                {
                    MainTexture = Texture.FromStream(device, MainBitmap.Stream, Usage.None, Pool.Managed);
                }
                if (BumpMap != null)
                {
                    BumpTexture = Texture.FromStream(device, BumpMap.Stream, Usage.None, Pool.Managed);
                }
                if (CubeMap != null)
                {
                    CubeTexture = Texture.FromStream(device, CubeMap.Stream, Usage.None, Pool.Managed);
                }
            }
        }
    }
}
