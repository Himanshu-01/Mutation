using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace Xapien
{
    public class Panel : Control
    {
        public List<Control> Controls = new List<Control>();

        Sprite Background;
        Texture Texture;

        public override void Initialize(Device device)
        {
            // Initialize the Sprite
            Background = new Sprite(device);
            Texture = Texture.FromBitmap(device, Xapien.Properties.Resources.Blank, Usage.None, Pool.Managed);

            // Initialize Size
            Size = new Size(100, 100);

            // Initialize Child Controls
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Initialize(device);
                Controls[i].Parent = this;
            }

            // Initialize Base
            base.Initialize(device);
        }

        public override void Render(Device device)
        {
            if (Initialized)
            {
                // Draw the Panel
                Background.Begin(SpriteFlags.AlphaBlend);
                Background.Draw(Texture, new Rectangle(0, 0, Size.Width, Size.Height), Vector3.Empty, new Vector3((float)(Position.X + (Parent != null ? Parent.Position.X : 0)), (float)(Position.Y + (Parent != null ? Parent.Position.Y : 0)), 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                Background.End();

                // Render all Child Controls
                for (int i = 0; i < Controls.Count; i++)
                {
                    // Check if it's Initialized
                    if (!Controls[i].Initialized)
                    {
                        Controls[i].Initialize(device);
                        Controls[i].Parent = this;
                    }

                    // Draw
                    Controls[i].Render(device);
                }
            }
        }

        public override void OnMouseDown(int MouseX, int MouseY)
        {
            // Update all Child Controls
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].OnMouseDown(MouseX, MouseY);

            // Update Base
            base.OnMouseDown(MouseX, MouseY);
        }

        public override void OnMouseUp(int MouseX, int MouseY)
        {
            // Update all Child Controls
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].OnMouseUp(MouseX, MouseY);

            // Update Base
            base.OnMouseUp(MouseX, MouseY);
        }

        public override void OnMouseOver(int MouseX, int MouseY)
        {
            // Update all Child Controls
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].OnMouseOver(MouseX, MouseY);

            // Update Base
            base.OnMouseOver(MouseX, MouseY);
        }
    }
}
