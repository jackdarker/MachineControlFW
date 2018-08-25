using System;
using System.Collections.Generic;
using System.Windows.Forms; //for property dialog??
using System.Text;

namespace WagoBK
{
    /// <summary>
    /// configuration dialog for 511
    /// </summary>
    public partial class DlgCardDef511Properties : Form
    {
        public DlgCardDef511Properties()
        {
            InitializeComponent();
        }
        public DlgCardDef511Properties(CardDef511 Card)
        {
            m_Card = Card;
            InitializeComponent();
            this.Text = m_Card.GetCardDescrName()+ " Slot "+m_Card.GetSlot().ToString();
            this.numericUpDown1.Maximum = 400;
            this.numericUpDown1.Minimum = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            m_Card.SetFreq(0, (double)this.numericUpDown1.Value);
        }

        private Button btOK;
        private Label label1;

        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Vom Windows Form-Designer generierter Code
        private void InitializeComponent()
        {
            this.btOK = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(167, 12);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Frequency";
            // 
            // DlgCardDef511Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 52);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgCardDef511Properties";
            this.Text = "Form1";
            this.VisibleChanged += new System.EventHandler(this.DlgCardDef511Properties_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgCardDef511Properties_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CardDef511 m_Card = null;
        private System.Windows.Forms.NumericUpDown numericUpDown1;

        private void DlgCardDef511Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void DlgCardDef511Properties_VisibleChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = (decimal)m_Card.GetFreq(0);
        }
    }
    /// <summary>
    /// BK Carddefinition 
    /// "2xPWM 24V 100Hz"
    /// </summary>
    public class CardDef511 : CardDefAnalogIO
    {
        public enum SpecialFunction
        {
            none =0,    
            WriteFreq,
            ReadFreq
        }
        public CardDef511(int Slot)
            : base(Slot)
        {
            m_OutPA.Initialize();
            m_AO.Initialize();
            m_Freq[0] = 100; //default Frequency 100Hz
            m_Freq[1] = 100;
            m_Dlgproperties = new DlgCardDef511Properties(this);
        }
        public override void CardPOR()
        {
            //setup should be done; ??read instead write?
            m_SpecialFunc = SpecialFunction.WriteFreq;
        }
        public override double GetAO(int Ch)
        {
            double Value = 0;

            if (Ch >= 0 && Ch <= 2)
            {
                Value = m_AO[Ch];
            }
            return Value;
        }
        public override void SetAO(int Ch, double Value)
        {
            if (Ch >= 0 && Ch <= 2)
            {
                m_AO[Ch] = Value;
            }
        }
        public override int GetChannelCount()
        {
            return 2;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.AO;
        }
        public override string GetCardName()
        {
            return "750-511";
        }
        public override string GetCardDescrName()
        {
            return "2xPWM 24V 100Hz";
        }
        public override CardType GetCardType()
        {
            return CardType.SpecialIO;
        }
        public double GetFreq(int Ch)
        {//Freq is not read from Card, because this would take several cycles
            //bevor function could return
            //instead the value is read when written or in CardPOR and stored locally
            double Value = 0;
            if (Ch >= 0 && Ch <= 2)
            {
                Value = m_Freq[Ch];
            }
            return Value;
        }
        /// <summary>
        /// Modify output frequency.
        /// Will be reset if Koppler is repowered
        /// </summary>
        /// <param name="Ch">should be 0; both channels are affected</param>
        /// <param name="Freq">5 .. 470 Hz</param>
        public void SetFreq(int Ch, double Freq)
        {
            if (Ch >= 0 && Ch <= 2)
            {   
                m_Freq[0] = Freq;
                m_Freq[1] = Freq;   //same Frequency only
                m_SpecialFunc = SpecialFunction.WriteFreq;
            };     
        }
        /// <summary>
        /// PA[0] = 0x00, PA[1] = 0x00
        /// PA[2] = Ch0 HighByte, PA[3] = Ch0 LowByte, 
        /// PA[4] = 0x00, PA[5] = 0x00
        /// PA[6] = Ch1 HighByte, PA[7] = Ch1 LowByte, 
        /// </summary>
        /// <returns></returns>
        public override byte[] CreateOutPA()
        {
            int unscaled = 0;
            if (m_SpecialFunc == SpecialFunction.ReadFreq)
            {// create read message
                m_OutPA[0] = m_OutPA[4] = m_OutPA[5] = m_OutPA[6] = m_OutPA[7] = 0;
                m_OutPA[1] = 0x82; //read Time-Register
                m_OutPA[2] = 0;
                m_OutPA[3] = 0;
            }
            else if (m_SpecialFunc == SpecialFunction.WriteFreq)
            {// create write message
                unscaled = (int)(1000000/(m_Freq[0] *  m_FreqPerDigit));
                m_OutPA[0] = m_OutPA[4] = m_OutPA[5] = m_OutPA[6] = m_OutPA[7] = 0;
                m_OutPA[1] = 0xC2; //write Time-Register
                m_OutPA[2] = (byte)((unscaled >> 8) & 0xFF);
                m_OutPA[3] = (byte)((unscaled) & 0xFF);
            }
            else
            {// just output PWM
                for (int i = 0; i < m_AO.Length; i++)
                {
                    unscaled = ((int)(m_AO[i] / m_UnitPerDigit)) * 32; //shift 5 Bit
                    m_OutPA[i * 4] = 0;
                    m_OutPA[(i * 4) + 1] = 0; //ControlByte
                    m_OutPA[(i * 4) + 2] = (byte)((unscaled >> 8) & 0xFF);
                    m_OutPA[(i * 4) + 3] = (byte)((unscaled) & 0xFF);
                }
            }
            return m_OutPA;
        }
        public override int GetOutPALength()
        {
            //(1x0x00 + 1xStatus + 2 Data)*2
            return m_OutPA.Length * 8;
        }
        public override void ParseInPA(byte[] PA)
        {
            //Input is normally 0 and can be ignored
            //But if cfg read or write is done, we have to check
            int unscaled = 0;
            if (PA.Length >= m_OutPA.Length)
            {
                    m_OutPA[0 * 4] = PA[0 * 4];
                    m_OutPA[(0 * 4) + 1] = PA[(0 * 4) + 1];//Statusbyte
                    m_OutPA[(0 * 4) + 2] = PA[(0 * 4) + 2];
                    m_OutPA[(0 * 4) + 3] = PA[(0 * 4) + 3];
                    if ((m_OutPA[(0 * 4) + 1] & 0x80)>0)
                    {// special mode
                        if (m_SpecialFunc == SpecialFunction.ReadFreq)
                        {// data contains Frequency, switch back to normal operation
                            unscaled = ((int)m_OutPA[(0 * 4) + 2] << 8) + (int)m_OutPA[(0 * 4) + 3];
                            m_Freq[0] = 1000000 / ((double)(unscaled) * m_FreqPerDigit);
                            m_Freq[1] = m_Freq[0];
                            m_SpecialFunc = SpecialFunction.none;
                        }
                        else if (m_SpecialFunc == SpecialFunction.WriteFreq)
                        {// ack on Write, next we should readback cfg
                            m_SpecialFunc = SpecialFunction.ReadFreq;
                        };
                    }
            }
        }
        /// <summary>
        /// only PWM, not frequency, will be read
        /// </summary>
        /// <param name="PA"></param>
        public override void ParseOutPA(byte[] PA)
        {
            int unscaled = 0;
            if (PA.Length >= m_OutPA.Length)
            {
                for (int i = 0; i < m_AO.Length; i++)
                {
                    m_OutPA[i * 4] = PA[i * 4];
                    m_OutPA[(i * 4) + 1] = PA[(i * 4) + 1];
                    m_OutPA[(i * 4) + 2] = PA[(i * 4) + 2];
                    m_OutPA[(i * 4) + 3] = PA[(i * 4) + 3];
                    unscaled = ((int)m_OutPA[(i * 4) + 2] << 8) + (int)m_OutPA[(i * 4) + 3];
                    m_AO[i] = ((double)(unscaled/32)) * m_UnitPerDigit; //shift 3bit
                }
            }
        }
        public override int GetInPALength()
        {
            //(1x0x00 + 1xStatus + 2 Data)*2
            return m_InPA.Length * 8;
        }
        public override Form GetPropertyDialog()
        {
            return m_Dlgproperties;
        }

        private const double m_UnitPerDigit = (double)(1 * 100) / (double)1024; //100% = 1024 dig
        private const double m_FreqPerDigit = 8; //800us = 100 dig, 
        //? using Frequ-Register limits to 30..470Hz; using Timing register limits to 5..470Hz
        private double[] m_AO= new double[2];
        private byte[] m_OutPA = new byte[8];
        private byte[] m_InPA = new byte[8];
        private double[] m_Freq = new double[2]; //currently configured Frequency
        private SpecialFunction m_SpecialFunc;   //Flag if read/write is pending or normal operation
        private DlgCardDef511Properties m_Dlgproperties = null;
    }
}
