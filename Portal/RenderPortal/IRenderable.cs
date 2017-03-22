using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX.Direct3D9;

namespace Portal
{
    public interface IRenderable
    {
        void Read(IMetaNode[] mode, string TagPath);
        void Initialize(Device device);
        void Draw(Device device);
        void Click(Device device, int x, int y);
    }
}
