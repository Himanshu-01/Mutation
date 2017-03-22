using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SlimDX.DirectInput;
using SlimDX.XInput;
using SlimDX;

namespace Portal
{
    public class Camera : IDisposable
    {
        public Device<KeyboardState> Keyboard;
        public Controller XboxControler;
        public float x = 0f;
        public float y = 3f;
        public float z = -5f;
        public float i = 0f;
        public float j = 0f;
        public float k = 0f;

        public Vector3 Position;
        public Vector3 LookAt = new Vector3(0, 0, 0f);
        public Vector3 UpVector = new Vector3(0, 0, 1);
        public float radius = 1.0f;
        public float speed = 1.0f;

        public float radianh;
        public float radianv;
        public bool fixedrotation = false;
        public int oldx = 0;
        public int oldy = 0;
        public Camera(Control form)
        {
            // Initialize Keyboard
            DirectInput Input = new DirectInput();
            Keyboard = new Device<KeyboardState>(Input, SystemGuid.Keyboard);
            Keyboard.SetCooperativeLevel(form.Parent.Parent.Parent.Handle, CooperativeLevel.Foreground | CooperativeLevel.Nonexclusive);

            // Initialize Xbox Controller
            XboxControler = new Controller(UserIndex.One);
            
            radianh = 0.0f;
            radianv = 0.0f;
            Position = new Vector3(x, y, z);
            ComputePosition();
        }
        public void SetFixed()
        {
            fixedrotation = true;
        }
        public void move()
        {
            // Aquire Devices
            try
            {
                Keyboard.Acquire();
            }
            catch
            {
                return;
            }

            // Handle Input

            #region Keyboard Handler

            // Get Keyboard State
            KeyboardState KState = Keyboard.GetCurrentState();
            foreach (Key kk in KState.PressedKeys)
            {
                switch (kk.ToString())
                {
                    case "W":
                        x += i * speed;
                        y += j * speed;
                        z += k * speed;
                        Position.X = x;
                        Position.Y = y;
                        Position.Z = z;
                        break;
                    case "S":
                        x -= i * speed;
                        y -= j * speed;
                        z -= k * speed;
                        Position.X = x;
                        Position.Y = y;
                        Position.Z = z;
                        break;
                    case "A":
                        ComputeStrafe(false);
                        break;
                    case "D":
                        ComputeStrafe(true);
                        break;
                    case "Z":
                        z -= speed;
                        Position.Z = z;
                        break;
                    case "X":
                        z += speed;
                        Position.Z = z;
                        break;
                    case "Equals":
                    case "Add":
                        speed += 0.001f;

                        break;
                    case "Minus":
                    case "NumPadMinus":
                        speed -= 0.001f;
                        if (speed < 0) { speed = 0.01f; }
                        break;
                }
                ComputePosition();
            }

            #endregion

            // Check Connection
            if (XboxControler.IsConnected)
            {
                // Get State
                State XState = XboxControler.GetState();
                
                // Handle Input
                //if (XState.Gamepad.
            }

        }
        public void change(int x, int y)
        {

            int tempx = oldx - x;
            int tempy = oldy - y;

            radianh += DegreesToRadian((float)tempx);
            radianv += DegreesToRadian((float)tempy);


            ComputePosition();
            oldx = x;
            oldy = y;
        }

        public void AimCamera(Vector3 v)
        {
            i = v.X;
            j = v.Y;
            k = v.Z;
            LookAt.X = i;
            LookAt.Y = j;
            LookAt.Z = k;
        }
        public void ComputeStrafe(bool right)
        {
            // Keep all rotations between 0 and 2PI
            radianh = radianh > (float)Math.PI * 2 ? radianh - (float)Math.PI * 2 : radianh;
            radianh = radianh < 0 ? radianh + (float)Math.PI * 2 : radianh;

            radianv = radianv > (float)Math.PI * 2 ? radianv - (float)Math.PI * 2 : radianv;
            radianv = radianv < 0 ? radianv + (float)Math.PI * 2 : radianv;

            //Switch up-vector based on vertical rotation
            // UpVector = Position.X < 0 ? new Vector3(-1, 0, 1) : new Vector3(1, 0, 1);
            // radianv > Math.PI / 2 && radianv < Math.PI / 2 * 3 ?
            //  new Vector3(0,  1,0) : new Vector3(0,  -1,0);

            float tempi = radius * (float)(Math.Cos(radianh - 1.57) * Math.Cos(radianv)) * this.speed;
            float tempj = radius * (float)(Math.Sin(radianh - 1.57) * Math.Cos(radianv)) * this.speed;

            if (right == true)
            {
                LookAt.X += tempi;
                LookAt.Y += tempj;

                x += tempi;
                y += tempj;

                Position.X = x;
                Position.Y = y;
            }
            else
            {
                LookAt.X -= tempi;
                LookAt.Y -= tempj;

                x -= tempi;
                y -= tempj;

                Position.X = x;
                Position.Y = y;
            }

        }

        public void Dispose()
        {
            Keyboard = null;
        }
        public void ComputePosition()
        {
            // Keep all rotations between 0 and 2PI
            //if (fixedcam==true){return;}

            radianh = radianh > (float)Math.PI * 2 ? radianh - (float)Math.PI * 2 : radianh;
            radianh = radianh < 0 ? radianh + (float)Math.PI * 2 : radianh;

            radianv = radianv > (float)Math.PI * 2 ? radianv - (float)Math.PI * 2 : radianv;
            radianv = radianv < 0 ? radianv + (float)Math.PI * 2 : radianv;

            i = radius * (float)(Math.Cos(radianh) * Math.Cos(radianv));
            j = radius * (float)(Math.Sin(radianh) * Math.Cos(radianv));
            k = radius * (float)Math.Sin(radianv); ;

            if (fixedrotation)
            {
                Position.X = i + x;
                Position.Y = j + y;
                Position.Z = k + z;
            }
            else
            {
                LookAt.X = i + x;
                LookAt.Y = j + y;
                LookAt.Z = k + z;
            }

        }


        public float DegreesToRadian(float degree)
        {
            return (float)(degree * (Math.PI / 180));
        }
    }
}
