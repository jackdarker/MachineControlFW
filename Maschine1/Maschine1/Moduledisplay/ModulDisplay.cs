using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class ModuleDisplay : UserControl
    {
        //represents a surface to draw modulestate
        public class Canvas : Control
        {
            public Canvas(ModuleDisplay Parent)
            {
                m_Parent = Parent;

                bitmap = new Bitmap(m_Parent.Width, m_Parent.Height);
                bitmapGraphics = Graphics.FromImage(bitmap);
                controlGraphics = CreateGraphics();
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.UserPaint |
                        ControlStyles.OptimizedDoubleBuffer, true);
                UpdateStyles();
                //this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
               // this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);

            }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (bitmapGraphics == null)
                    return;
                if (bitmap != null) e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            }
            protected ModuleDisplay GetParent()
            {
                return m_Parent;
            }
            public virtual void DrawScreen()
            {
                bitmapGraphics.Clear(Color.White);
                System.Drawing.Font _Font = new System.Drawing.Font("Arial", 10);
                SolidBrush _BrushRed = new SolidBrush(Color.Red);
                SolidBrush _BrushGreen = new SolidBrush(Color.Green);
                SolidBrush _BrushYellow = new SolidBrush(Color.Yellow);
                Pen _PenBlack = new Pen(Color.Black);

                bitmapGraphics.DrawEllipse(_PenBlack, m_RotaryRect);
                Rectangle _Station1Rect = m_Station1Rect;
                Rectangle _Station2Rect = m_Station2Rect;
                switch (m_Parent.GetStatus())
                {
                    case 0: //HP
                    case 6:
                        bitmapGraphics.DrawLine(_PenBlack, m_RotaryRect.Left + m_RotaryRect.Width / 2,
                                m_RotaryRect.Top, m_RotaryRect.Left + m_RotaryRect.Width / 2,
                                m_RotaryRect.Top + m_RotaryRect.Height);
                        _Station1Rect.Offset(m_RotaryRect.Left, m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        _Station2Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width, m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        break;
                    case 1: //MoveToWP
                        bitmapGraphics.DrawLine(_PenBlack, m_RotaryRect.Left,
                                m_RotaryRect.Top + m_RotaryRect.Height / 2, m_RotaryRect.Left + m_RotaryRect.Width,
                                m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        _Station1Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width / 2, m_RotaryRect.Top);
                        _Station2Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width / 2, m_RotaryRect.Top + m_RotaryRect.Height);

                        break;
                    case 2://WP
                    case 3:
                    case 4:
                        bitmapGraphics.DrawLine(_PenBlack, m_RotaryRect.Left + m_RotaryRect.Width / 2,
                                m_RotaryRect.Top, m_RotaryRect.Left + m_RotaryRect.Width / 2,
                                m_RotaryRect.Top + m_RotaryRect.Height);
                        _Station2Rect.Offset(m_RotaryRect.Left, m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        _Station1Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width, m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        break;
                    case 5://MoveToHP
                        bitmapGraphics.DrawLine(_PenBlack, m_RotaryRect.Left,
                                m_RotaryRect.Top + m_RotaryRect.Height / 2, m_RotaryRect.Left + m_RotaryRect.Width,
                                m_RotaryRect.Top + m_RotaryRect.Height / 2);
                        _Station2Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width / 2, m_RotaryRect.Top);
                        _Station1Rect.Offset(m_RotaryRect.Left + m_RotaryRect.Width / 2, m_RotaryRect.Top + m_RotaryRect.Height);

                        break;
                    default:
                        break;

                }
                bitmapGraphics.FillEllipse(_BrushYellow, _Station1Rect);
                bitmapGraphics.FillEllipse(_BrushRed, _Station2Rect);
                _Font.Dispose();
                _BrushRed.Dispose();
                _BrushGreen.Dispose();
                _BrushGreen.Dispose();
                _PenBlack.Dispose();

                this.Invalidate();

            }
            private void OnMouseClick(object sender, MouseEventArgs e)
            {
            }
            private void OnKeyUp(object sender, KeyEventArgs e)
            {
            }
            private Rectangle m_RotaryRect = new Rectangle(60, 20, 150, 150);
            private Rectangle m_Station1Rect = new Rectangle(-15, -15, 30, 30);
            private Rectangle m_Station2Rect = new Rectangle(-15, -15, 30, 30);
            protected ModuleDisplay m_Parent;
            protected Bitmap bitmap;
            protected Graphics bitmapGraphics, controlGraphics;
        }

        public ModuleDisplay()
        {
            InitializeComponent();
            CreateControls();
            m_Status=0;
        }

        protected virtual void CreateControls()
        {
            m_Canvas = new Canvas(this);
            m_Canvas.SetBounds(5, 5, panel1.Width-10,panel1.Height-10-textBox1.Height-10);
            m_Canvas.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(m_Canvas);
        }
        public delegate void DelegateSetMessage(string text);
        public virtual void SetMessage(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateSetMessage(SetMessage),new System.Object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
            
        }
        public virtual void ClearStatus()
        {
            textBox1.Text = "";
            m_Canvas.Invalidate();
        }
        public delegate void DelegateSetStatus(int Status);
        public void SetStatus(int Status)
        {
            lock (StatusLock)
            {
                m_Status = Status;
            }
            //textBox1.Text= string.Format("Step {0:d}", GetStatus());
            m_Canvas.DrawScreen();
        }
        public int GetStatus()
        {
            int Status = 0;
            lock (StatusLock)
            {
                Status = m_Status;
            }
            return Status;
        }

        private System.Object StatusLock= new System.Object() ;
        private int m_Status;
        protected Canvas m_Canvas;

    }
}
