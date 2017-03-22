using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Model
{
    public class Material
    {
        public string ShaderPath;
        public Shader Shader;

        public void Read(IMetaNode[] mode)
        {
            // Get Shader Path
            ShaderPath = (string)mode[1].GetValue();

            // Read Shader
            Shader = new Shader(ShaderPath);
        }
    }
}
