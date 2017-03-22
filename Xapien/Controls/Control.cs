using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Drawing;

namespace Xapien
{
    public class Control
    {
        public delegate void OnMouseHandler();

        public event OnMouseHandler OnMouseOverHandler;
        public event OnMouseHandler OnMouseDownHandler;
        public event OnMouseHandler OnMouseUpHandler;
        public event OnMouseHandler Click;

        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public object Tag { get; set; }
        public Control Parent { get; set; }
        public int Opacity { get; set; }
        public bool Initialized { get; set; }
        public bool IsMouseOver { get; set; }
        public bool IsClicked { get; set; }

        public virtual void Initialize(Device device) 
        {
            Opacity = 255;
            Initialized = true;
        }

        public virtual void Render(Device device) { }

        public virtual void OnMouseDown(int MouseX, int MouseY)
        {
            // Update
            IsClicked = IsMouseOver;

            // Handle
            if (IsClicked && OnMouseDownHandler != null)
                OnMouseDownHandler();
            else if (OnMouseOverHandler != null)
                OnMouseUpHandler();
        }

        public virtual void OnMouseUp(int MouseX, int MouseY)
        {
            // Handle
            if (IsMouseOver && IsClicked && Click != null)
                Click();

            // Update
            IsClicked = false;

            // Handle
            if (IsMouseOver && OnMouseUpHandler != null)
                OnMouseUpHandler();
        }

        public virtual void OnMouseOver(int MouseX, int MouseY)
        {
            // Update
            if (MouseX >= Position.X && MouseX <= Position.X + Size.Width
                && MouseY >= Position.Y && MouseY <= Position.Y + Size.Height)
                IsMouseOver = true;
            else
                IsMouseOver = false;

            //IsClicked = IsMouseOver;

            // Handle
            if (OnMouseOverHandler != null)
                OnMouseOverHandler();
        }
    }
}
