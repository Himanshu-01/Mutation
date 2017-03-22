using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;

namespace Xapien
{
    public class RadioButton : Control
    {
        public string Text { get; set; }
        public bool Checked { get; set; }

        Sprite Main, Box;
        Texture Normal, Hover, Clicked, NormalChecked, HoverChecked, ClickedChecked, Blank;
        Microsoft.DirectX.Direct3D.Font font;

        public RadioButton() 
        {
            Text = "";
        }
        public RadioButton(string Text)
        {
            this.Text = Text;
        }

        public override void Initialize(Device device)
        {
            // Initialize the Sprite
            Main = new Sprite(device);
            Box = new Sprite(device);
            Normal = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButton, Usage.None, Pool.Managed);
            Hover = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButtonOver, Usage.None, Pool.Managed);
            Clicked = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButtonClick, Usage.None, Pool.Managed);
            NormalChecked = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButtonChecked, Usage.None, Pool.Managed);
            HoverChecked = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButtonCheckedOver, Usage.None, Pool.Managed);
            ClickedChecked = Texture.FromBitmap(device, Xapien.Properties.Resources.RadioButtonCheckedClick, Usage.None, Pool.Managed);
            Blank = Texture.FromBitmap(device, Xapien.Properties.Resources.Blank, Usage.None, Pool.Managed);
            font = new Microsoft.DirectX.Direct3D.Font(device, new System.Drawing.Font(FontFamily.GenericSerif, 12.0f, FontStyle.Regular));

            // Initialize Size
            Size = new Size(14, 14);

            // Initialize Base
            base.Initialize(device);
        }

        public override void Render(Device device)
        {
            // Update Size
            Size = new Size(5 + (Text.Length * 8), 20);

            // Update
            if (Initialized)
            {
                // Draw Backgroun
                Main.Begin(SpriteFlags.AlphaBlend);
                Main.Draw(Blank, new Rectangle(0, 0, Size.Width, Size.Height), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)), 0), 0);
                Main.End();

                // Draw the Check Box
                Box.Begin(SpriteFlags.AlphaBlend);
                if (IsClicked && Checked)
                    Box.Draw(ClickedChecked, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else if (IsMouseOver && Checked)
                    Box.Draw(HoverChecked, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else if (Checked)
                    Box.Draw(NormalChecked, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else if (IsClicked)
                    Box.Draw(Clicked, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else if (IsMouseOver)
                    Box.Draw(Hover, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else
                    Box.Draw(Normal, new Rectangle(0, 0, 14, 14), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3, 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));

                // Center and Draw Text
                font.DrawText(Box, Text, new Point((int)(Position.X + (Parent != null ? Parent.Position.X : 0)) + 20, (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0))), Color.Black);
                Box.End();
            }
        }

        public override void OnMouseUp(int MouseX, int MouseY)
        {
            // Update Base
            base.OnMouseUp(MouseX, MouseY);

            // Update Checked
            if (IsMouseOver)
                Checked = Checked ? false : true;
        }
    }
}
