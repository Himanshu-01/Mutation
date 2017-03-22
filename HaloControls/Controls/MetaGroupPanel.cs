using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HaloControls.Controls
{
    public class MetaGroupPanel : MetaControl
    {
        /// <summary>
        /// Gets or sets the color of the panel's border.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the panel's border.
        /// </summary>
        public float BorderThickness { get; set; }

        /// <summary>
        /// Gets or sets the corner radius in pixels.
        /// </summary>
        public int RoundCorners { get; set; }

        // The sweep angle of the arc.
        public const int SweepAngle = 90;

        public MetaGroupPanel() : base()
        {
            // Set the control to be double buffered.
            this.DoubleBuffered = true;

            // Turn on auto sizing and turn off auto scrolling.
            this.AutoSize = true;
            this.AutoScroll = false;

            // Set the default size of the control.
            this.Size = new Size(10, 10);

            // Set default values for some of the custom fields.
            this.BorderColor = Color.FromKnownColor(KnownColor.ControlDark);
            this.BorderThickness = 1.0f;
            this.RoundCorners = 10;
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            throw new NotImplementedException();
        }

        public override bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            // Create a new pen to draw the panel with.
            //Pen pen = new Pen(Color.FromKnownColor(KnownColor.Control), 1.0f);
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            // Create a new GraphicsPath object to define the controls frame.
            //GraphicsPath path = new GraphicsPath();

            int ArcWidth = this.RoundCorners * 2;
            int ArcHeight = this.RoundCorners * 2;
            int ArcX1 = 0;
            int ArcX2 = this.Width -(ArcWidth + 1);
            int ArcY1 = 0;// 10;
            int ArcY2 = this.Height -(ArcHeight + 1);

            GraphicsPath path = new GraphicsPath();
            Brush BorderBrush = new SolidBrush(this.BorderColor);
            Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
            LinearGradientBrush BackgroundGradientBrush = null;
            Brush BackgroundBrush = new SolidBrush(this.BackColor);
            SolidBrush ShadowBrush = null;
            GraphicsPath ShadowPath  = null;
            //-----------------------------------

            //Create Rounded Rectangle Path------
            path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, SweepAngle); // Top Left
            path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, SweepAngle); //Top Right
            path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, SweepAngle); //Bottom Right
            path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, SweepAngle); //Bottom Left
            path.CloseAllFigures(); 
            //-----------------------------------

            //Check if Gradient Mode is enabled--
            //if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
            //{
                //Paint Rounded Rectangle------------
                e.Graphics.FillPath(BackgroundBrush, path);
                //-----------------------------------
            //}
            //else
            //{
            //    BackgroundGradientBrush = 
            //      new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), 
            //      this.BackgroundColor, this.BackgroundGradientColor, 
            //      (LinearGradientMode)this.BackgroundGradientMode);
                            
            //    //Paint Rounded Rectangle------------
            //    g.FillPath(BackgroundGradientBrush, path);
            //    //-----------------------------------
            //}
            //-----------------------------------

            //Paint Borded-----------------------
            e.Graphics.DrawPath(BorderPen, path);
            //-----------------------------------

            //Destroy Graphic Objects------------
            if(path!=null){path.Dispose();}
            if(BorderBrush!=null){BorderBrush.Dispose();}
            if(BorderPen!=null){BorderPen.Dispose();}
            if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
            if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
            if(ShadowBrush!=null){ShadowBrush.Dispose();}
            if (ShadowPath != null) { ShadowPath.Dispose(); }
        }
    }
}
