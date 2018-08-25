using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.GUI
{
    public partial class MainSoftKeys : UserControl
    {
        //Event send to client
        public class ButtonClickEventArgs : EventArgs
        {
            private readonly int m_Button;
            private readonly string m_Value;

            public ButtonClickEventArgs(int button)
            {
                this.m_Button = button;
                this.m_Value = "";
            }
            public ButtonClickEventArgs(int button, string value)
            {
                this.m_Button = button;
                this.m_Value = value;
            }

            public int Button
            {
                get { return m_Button; }
            }
            public string Value
            {
                get { return m_Value; }
            }
        }
        public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);
        //This enum defines function of Button
        public enum ButtonFunction  {
            Call = 1,  //call a function
            ReturnSubMenu=2, //get back to previous menu
            OpenSubMenu=3, //open submenu
            OpenTopMenu=4 //get back to rrot menu
        }

        public MainSoftKeys()
        {
            InitializeComponent();
            DisableButtons();
            m_Menustack.Push("");       
        }
        public void SetButtonNavigation(ButtonNavigation buttonConfig)
        {
            //verify Config??
            Boolean _init = (m_ButtonNavigation == null);
            m_ButtonNavigation = buttonConfig;
            if (_init) {
                Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
                UserManagement.UserManager.getUserManager.EventUserChanged += new UserManagement.UserManager.OnUserChangedEventHandler(OnRetranslateEvent);
                Retranslate();
            }
            OpenMenu("");
        }
        public void OpenMenu(string menu)
        {
            DisableButtons();
            if (m_Menustack.Peek()!=menu) m_Menustack.Push(menu);  
            int BtIndex=0;
            if (m_ButtonNavigation == null) return;
            ButtonNavigation SubMenu = m_ButtonNavigation.FilterByMenu(menu);
            while (BtIndex < 10  )
            {
                if (BtIndex >= SubMenu.Count)
                {
                    ConfigureButton(BtIndex, new ButtonConfig());   //preload disable
                }
                else
                {
                    ConfigureButton(BtIndex, SubMenu[BtIndex]);
                }
                BtIndex++;
            }

        }
        public void DisableButtons()
        {
            button1.Enabled=false;
            button2.Enabled=false;
            button3.Enabled=false;
            button4.Enabled=false;
            button5.Enabled=false;
            button6.Enabled=false;
            button7.Enabled=false;
            button8.Enabled=false;
            button9.Enabled=false;
            button10.Enabled=false;
        }
        public void EnableButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
        }
        public void EnableButton(int button)
        {
            System.Windows.Forms.Button Button = GetButton(button);
            if (Button!=null)
            {
                Button.Enabled = true;
            }
        }
        private void Retranslate() {
            OpenMenu(m_Menustack.Peek());
        }
        private void OnRetranslateEvent(object sender, EventArgs e) {
            if (this != null && this.IsHandleCreated) {
                if (this.InvokeRequired) {
                    this.Invoke(new Translator.DelegateRetranslate(Retranslate));
                } else {
                    Retranslate();
                }
            }
        }

        public event ButtonClickEventHandler ButtonClick;
        protected virtual void OnButtonClick(ButtonClickEventArgs e)
        {
            //Menuumschaltung nicht durchreichen
            ButtonNavigation SubMenu = m_ButtonNavigation.FilterByMenu(m_Menustack.Peek());
            switch(SubMenu[e.Button].GetBtFunction())
            {
                case ButtonFunction.Call:
                    if (ButtonClick != null)
                    {
                        ButtonClickEventArgs Event = new ButtonClickEventArgs(e.Button,SubMenu[e.Button].GetValue() );
                        ButtonClick(this, Event);
                    }
                    break;
                case ButtonFunction.OpenSubMenu:
                    OpenMenu(SubMenu[e.Button].GetValue());
                    break;
                case ButtonFunction.OpenTopMenu:
                    OpenMenu("");
                    break;
                case ButtonFunction.ReturnSubMenu:
                    m_Menustack.Pop();
                    OpenMenu(m_Menustack.Peek());
                    break;
                default:
                    break;
            }
        }

        private void ConfigureButton(int button, ButtonConfig buttonConfig)
        {
            System.Windows.Forms.Button Button= GetButton(button);
            if (Button != null)
            {
                Button.Text = buttonConfig.GetText();
                Button.Enabled = (Button.Text != "" && 
                    (buttonConfig.DelegateEnableItems==null || buttonConfig.DelegateEnableItems(buttonConfig)));
            }
        }
        private System.Windows.Forms.Button GetButton(int button)
        {
            System.Windows.Forms.Button Button = null;
            switch (button)
            {
                case 0:
                    Button = this.button1;
                    break;
                case 1:
                    Button = this.button2;
                    break;
                case 2:
                    Button = this.button3;
                    break;
                case 3:
                    Button = this.button4;
                    break;
                case 4:
                    Button = this.button5;
                    break;
                case 5:
                    Button = this.button6;
                    break;
                case 6:
                    Button = this.button7;
                    break;
                case 7:
                    Button = this.button8;
                    break;
                case 8:
                    Button = this.button9;
                    break;
                case 9:
                    Button = this.button10;
                    break;
                default:
                    break;
            }
            return Button;
        }
        private ButtonNavigation m_ButtonNavigation;
        private Stack<string> m_Menustack = new Stack<string>();
        #region ButtonClicked
        private void button1_Click(object sender, EventArgs e)
        {  
            OnButtonClick(new ButtonClickEventArgs(0));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(1));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(2));
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(3));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(4));
        }
        private void button6_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(5));
        }
        private void button7_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(6));
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(7));
        }
        private void button9_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(8));
        }
        private void button10_Click(object sender, EventArgs e)
        {
            OnButtonClick(new ButtonClickEventArgs(9));
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public class ButtonConfig {
            public delegate bool EnableItems(ButtonConfig BtCfg);
            public EnableItems DelegateEnableItems = null;
            private bool EnableMainSoftKeys(MainSoftKeys.ButtonConfig BtCfg) {
                return true;
            }
            public ButtonConfig() {
                m_Value = "";
                m_Text = "";
                m_BtFunction = ButtonFunction.Call;
                m_Menu = "";
                DelegateEnableItems = EnableMainSoftKeys;
            }
            public ButtonConfig(string value, string text, string menu, ButtonFunction btFunction) {
                m_Value = value;
                m_Text = text;
                m_BtFunction = btFunction;
                m_Menu = menu;
                DelegateEnableItems = EnableMainSoftKeys;
            }
            public ButtonConfig(string value, string text, string menu, ButtonFunction btFunction, EnableItems FuncEnable) {
                m_Value = value;
                m_Text = text;
                m_BtFunction = btFunction;
                m_Menu = menu;
                DelegateEnableItems = FuncEnable;
            }
            public string GetMenu() {
                return m_Menu;
            }
            public string GetValue() {
                return m_Value;
            }
            public string GetText() {
                return Translator.Tr(m_Text, "MainSoftKeys"); //Todo:?? sollte als delegate übergeben werden
            }
            public ButtonFunction GetBtFunction() {
                return m_BtFunction;
            }

            private string m_Value;
            private string m_Text;
            private ButtonFunction m_BtFunction;
            private string m_Menu;
        }
        /// <summary>
        /// 
        /// </summary>
        public class ButtonNavigation : List<ButtonConfig> {
            public ButtonNavigation FilterByMenu(string menu) {
                ButtonNavigation SubMenu = new ButtonNavigation();
                int ConfigIndex = 0;
                while (ConfigIndex < this.Count) {
                    if (this[ConfigIndex].GetMenu() == menu) {
                        SubMenu.Add(this[ConfigIndex]);
                    }
                    ConfigIndex++;
                }
                return SubMenu;
            }
        }

    }
}
