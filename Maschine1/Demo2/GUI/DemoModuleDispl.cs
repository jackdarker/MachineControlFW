using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Maschine1;

namespace Demo2
{
    public partial class DemoModuleDispl : ModuleDisplay
    {
        public class NewCanvas : Canvas
        {
            public NewCanvas(ModuleDisplay Parent): base(Parent)
            {

                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.UserPaint |
                        ControlStyles.OptimizedDoubleBuffer, true);
                UpdateStyles();
            }
            protected new DemoModuleDispl GetParent()
            {
                return (DemoModuleDispl)m_Parent;
            }
          /* protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (bitmapGraphics == null)
                    return;
                if (bitmap != null) e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            }*/
            
            public override void DrawScreen()
            {
                bitmapGraphics.Clear(Color.White);
                System.Drawing.Font _Font = new System.Drawing.Font("Arial", 10);
                SolidBrush _BrushRed = new SolidBrush(Color.Red);
                SolidBrush _BrushGreen = new SolidBrush(Color.Green);
                SolidBrush _BrushYellow = new SolidBrush(Color.Yellow);
                Pen _PenBlack = new Pen(Color.Black);

                Point _TmpPt= new Point(0,0);
                if (GetParent().m_LoadingDone) {
                    bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpBckGnd, _TmpPt);
                    
                    switch (GetParent().GetDrawState()) {
                        case DemoRobot2.State.Unknown: 
                        case DemoRobot2.State.PowerOK:
                        case DemoRobot2.State.HP:
                        case DemoRobot2.State.SledgeOut:
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpUUT, m_PtStation1);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpSledge, m_PtStation1);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpIndex, m_PtStation2);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpGripperOpen, m_PtStation2);
                            break;
                        case DemoRobot2.State.SledgeIn:
                        case DemoRobot2.State.IndexHP:
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpUUT, m_PtStation2);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpSledge, m_PtStation2);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpIndex, m_PtStation2);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpGripperOpen, m_PtStation2);
                            break;
                        case DemoRobot2.State.IndexWP:
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpUUT, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpSledge, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpIndex, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpGripperOpen, m_PtStation2);
                            break;
                        case DemoRobot2.State.GripperOpen:
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpUUT, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpSledge, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpIndex, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpGripperOpen, m_PtStation2);
                            break;
                        case DemoRobot2.State.GripperClosed:
                        case DemoRobot2.State.MeasureTorque:
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpUUT, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpSledge, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpIndex, m_PtStation2Up);
                            bitmapGraphics.DrawImageUnscaled(GetParent().m_BmpGripperClosed, m_PtStation2);
                            break;

                        default:
                            break;

                    }
                } else {
                    GetParent().LoadBitmaps();
                }
                _Font.Dispose();
                _BrushRed.Dispose();
                _BrushGreen.Dispose();
                _BrushGreen.Dispose();
                _PenBlack.Dispose();

                Invalidate();
            }

            private Rectangle m_RectSledge = new Rectangle(-15, -15, 30, 30);
            private Point m_PtStation1 = new Point(0, 0);
            private Point m_PtStation2 = new Point(140, 0);
            private Point m_PtStation2Up = new Point(140, -15);
        }

        public DemoModuleDispl():base()
        {

        }
        protected override void CreateControls()
        {
            m_Canvas = new NewCanvas(this);
            m_Canvas.SetBounds(5, 5, panel1.Width - 10, panel1.Height - 10 - textBox1.Height - 10);
            m_Canvas.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(m_Canvas);
            
        }
        protected void LoadBitmaps() {
            m_BmpBckGnd = Demo2.Properties.Resources.bckgnd;// Image.FromFile("bckgnd.png");
            m_BmpGripperClosed = Demo2.Properties.Resources.GripperClosed;// Image.FromFile("GripperClosed.png");
            m_BmpGripperOpen = Demo2.Properties.Resources.GripperOpen;// Image.FromFile("GripperOpen.png");
            m_BmpIndex = Demo2.Properties.Resources.Index;// Image.FromFile("Index.png");
            m_BmpUUT = Demo2.Properties.Resources.UUT;// Image.FromFile("UUT.png");
            m_BmpSledge = Demo2.Properties.Resources.Sledge;// Image.FromFile("Sledge.png");
            m_LoadingDone = true;
        }
        public delegate void DelegateDrawState(DemoRobot2.State RobotState);
        public void DrawState(DemoRobot2.State RobotState)    {
            if (this.InvokeRequired) {
                this.Invoke(new DelegateDrawState(DrawState), new System.Object[] { RobotState });
            }
            else  {
                m_RobotState2 = RobotState;
                m_Canvas.DrawScreen();
            }
        }
        public DemoRobot2.State GetDrawState()  {
            return m_RobotState2;
        }
        private bool m_LoadingDone=false;
        private Image m_BmpBckGnd = null;
        private Image m_BmpGripperClosed = null;
        private Image m_BmpGripperOpen = null;
        private Image m_BmpSledge = null;
        private Image m_BmpUUT = null;
        private Image m_BmpIndex = null;
        private DemoRobot2.State m_RobotState2;
        
    }
}
