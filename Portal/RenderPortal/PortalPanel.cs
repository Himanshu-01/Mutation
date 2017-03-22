using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloControls;
using System.IO;
using HaloObjects;
using Global;
using System.Drawing;
using Xapien;
using System.Collections;
using SlimDX.Direct3D9;
using SlimDX;

namespace Portal
{
    public class PortalPanel : System.Windows.Forms.Panel
    {
        string TagPath;
        bool LoadIntoGlobals = false;

        IRenderable model;

        Device Device = null;
        Camera cam;

        // UI
        Xapien.Panel Panel = new Xapien.Panel();
        Xapien.CheckBox Button = new Xapien.CheckBox("Test Check Box");

        public PortalPanel(string TagPath, bool LoadIntoGlobals)
        {
            // Set Info
            this.TagPath = TagPath;
            this.LoadIntoGlobals = LoadIntoGlobals;

            // Read out Model
            if (TagPath.EndsWith(".render_model"))
                model = new RenderModel();
            else if (TagPath.EndsWith(".decorator"))
                model = new DecoratorModel();
            else if (TagPath.EndsWith(".cloth"))
                model = new Cloth();
            else if (TagPath.EndsWith(".collision_model"))
                model = new CollisionModel();

            if (LoadIntoGlobals)
            {
                Plugin p = new Plugin(TagPath.Substring(TagPath.LastIndexOf(".") + 1), null);
                p.Layout.Read(new BinaryReader(new FileStream(Globals.p.Directory + TagPath, FileMode.Open, FileAccess.Read, FileShare.Read)));
            }

            // Read
            model.Read(((TagDefinition)Globals.Plugin).Fields, TagPath);
        }

        public void BeginRender()
        {
            // Initialize DirectX
            if (this.InitializeDirect3D() == false) // Check if D3D could be initialized
            {
                MessageBox.Show("Could not initialize Direct3D.", "Error");
                return;
            }

            // Load Control
            this.Dock = DockStyle.Fill;
            this.MouseDown += new MouseEventHandler(PortalPanel_MouseDown);
            this.MouseUp += new MouseEventHandler(PortalPanel_MouseUp);
            this.MouseMove += new MouseEventHandler(PortalPanel_MouseMove);
            this.Show();

            // Render
            while (this.Created)
            {
                this.Render();
                Application.DoEvents();
            }
        }

        void PortalPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // Move Camera
            if (e.Button == MouseButtons.Left && !Panel.IsMouseOver)
            {
                cam.change(e.X, e.Y);
            }

            // Check UI
            Panel.OnMouseOver(e.X, e.Y);
        }

        void PortalPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Cam
            cam.oldx = e.X;
            cam.oldy = e.Y;

            // Click
            model.Click(Device, e.X, e.Y);

            // UI
            Panel.OnMouseDown(e.X, e.Y);
        }

        void PortalPanel_MouseUp(object sender, MouseEventArgs e)
        {
            // UI
            Panel.OnMouseUp(e.X, e.Y);
        }

        public bool InitializeDirect3D()
        {
            //try
            //{
                PresentParameters presentParams = new PresentParameters();
                presentParams.Windowed = true; // We don't want to run fullscreen
                presentParams.SwapEffect = SwapEffect.Discard; // Discard the frames 
                presentParams.AutoDepthStencilFormat = Format.D16; //24 bits for the depth and 8 bits for the stencil
                presentParams.EnableAutoDepthStencil = true; //Let direct3d handle the depth buffers for the application
                presentParams.PresentationInterval = PresentInterval.One;

                Direct3D d3d = new Direct3D();
                IEnumerator i = d3d.Adapters.GetEnumerator();
                while (i.MoveNext())
                {
                    AdapterInformation ai = i.Current as AdapterInformation;
                    int adapterOrdinal = ai.Adapter;
                    Capabilities hardware = d3d.GetDeviceCaps(adapterOrdinal, DeviceType.Hardware);
                    CreateFlags flags = CreateFlags.SoftwareVertexProcessing;
                    if (hardware.DeviceCaps == DeviceCaps.HWTransformAndLight)
                        flags = CreateFlags.HardwareVertexProcessing;

                    Device = new Device(d3d, adapterOrdinal, hardware.DeviceType, this.Handle, flags, presentParams); //Create a device
                    if (Device != null)
                    {
                        break;
                    }
                }

                // Camera
                this.Focus();
                cam = new Camera(this);
                cam.speed = 0.02f;

                cam.Position.X = 0.5741551f;
                cam.Position.Y = 0.01331316f;
                cam.Position.Z = 0.4271703f;

                cam.radianv = 6.161014f;
                cam.radianh = 3.14159f;

                cam.x = 0.5741551f;
                cam.y = 0.01331316f;
                cam.z = 0.4271703f;

                cam.ComputePosition();

                //Device.DeviceReset += new EventHandler(Device_DeviceReset);
                Device_DeviceReset(Device, null);

                // Load Model
                model.Initialize(Device);
                //Button.Position = new Microsoft.DirectX.Vector2(10f, 10f);
                //Panel.Controls.Add(Button);
                //Panel.Initialize(Device);
                //Panel.Opacity = 100;
                //Panel.Size = new Size(500, 100);
                return true;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString(), "Error"); // Handle all the exceptions
            //    return false;
            //}
        }

        void Device_DeviceReset(object sender, EventArgs e)
        {
            // Turn off culling, so we see the front and back of the triangle
            Device.SetRenderState(RenderState.CullMode, Cull.None);
            // Turn off D3D lighting

            Device.SetRenderState(RenderState.Lighting, true);

            // Turn on the ZBuffer
            Device.SetRenderState(RenderState.ZEnable, true);
            Device.SetRenderState(RenderState.FillMode, FillMode.Solid);

            if (Device.Capabilities.SourceBlendCaps == BlendCaps.InverseSourceAlpha && Device.Capabilities.SourceBlendCaps == BlendCaps.SourceAlpha)
            {
                Device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
            }

            if (Device.Capabilities.TextureFilterCaps == FilterCaps.MinLinear)
            {
                Device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.Linear);
                Device.SetSamplerState(1, SamplerState.MinFilter, TextureFilter.Linear);
                Device.SetSamplerState(2, SamplerState.MinFilter, TextureFilter.Linear);
                Device.SetSamplerState(3, SamplerState.MinFilter, TextureFilter.Linear);
            }
            if (Device.Capabilities.TextureFilterCaps == FilterCaps.MagLinear)
            {
                Device.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Linear);
                Device.SetSamplerState(1, SamplerState.MagFilter, TextureFilter.Linear);
                Device.SetSamplerState(2, SamplerState.MagFilter, TextureFilter.Linear);
                Device.SetSamplerState(3, SamplerState.MagFilter, TextureFilter.Linear);
            }
            if (Device.Capabilities.TextureFilterCaps == FilterCaps.MipLinear)
            {
                Device.SetSamplerState(0, SamplerState.MipFilter, TextureFilter.Linear);
                Device.SetSamplerState(1, SamplerState.MipFilter, TextureFilter.Linear);
                Device.SetSamplerState(2, SamplerState.MipFilter, TextureFilter.Linear);
                Device.SetSamplerState(3, SamplerState.MipFilter, TextureFilter.Linear);
            }
            Device.SetRenderState(RenderState.Ambient, 0x00AFAFAF);
        }

        private void SetupMatrices()
        {
            Device.SetTransform(TransformState.World, Matrix.Identity);
            Device.SetTransform(TransformState.View, Matrix.LookAtRH(cam.Position, cam.LookAt, cam.UpVector));
            Device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovRH(0.78f, 1.0f, 0.1f, 1000.0f));
        }

        private void Render()
        {
            if (Device == null) // If the device is empty don't bother rendering
                return;

            Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, System.Drawing.Color.Blue, 1.0f, 0); // Clear the window to blue
            Device.BeginScene();

            // Move Camera
            cam.move();
            SetupMatrices();

            // Draw Model
            model.Draw(Device);

            // UI
            //Panel.Render(Device);

            Device.EndScene();
            Device.Present();
        }
    }
}
