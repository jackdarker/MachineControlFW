using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Maschine1;
using Maschine1.GUI;

namespace Demo2 {
    
    /// <summary>
    /// 
    /// </summary>
    public partial class DemoDlgTypeParamEditor : DlgTypeEditor {
        public DemoDlgTypeParamEditor() {
           
            InitializeComponent();
        }
        //protected new DemoTypeParams m_TypeParams;
        protected override TypeParams CreateDefaultTypeParameter() {
            return new DemoTypeParams() ;
        }
        /// <summary>
        /// kopiert die Daten vom Benutzerinterface in Typparameter.
        /// </summary>
        /// <returns></returns>
        protected override bool PanelToParams() {
                    /* \code */
                    string _error = "";
                    bool _ret = base.PanelToParams();
                    if (this.txtCustomerNo.Text.Length <= 0) {
                        _error += ",CustomerNo cannot be empty";
                        _ret = false;
                    }
                    if (this.txtVoltage.Value > 16 || this.txtVoltage.Value < 0) {
                        _error += ",Voltage has to be in range 0..16V";
                        _ret = false;
                    }
                    if (_ret) {
                        GetParams().m_CustTypeNumber = this.txtCustomerNo.Text;
                        GetParams().m_SupplyVoltage = double.Parse(this.txtVoltage.Value.ToString());
                    } else {
                        DlgError.HandleException(new Exception(_error));
                    }
                    return _ret;
                    /* \endcode */
                }
        protected DemoTypeParams.DemoParams GetParams() {
            return (DemoTypeParams.DemoParams)((DemoTypeParams)m_TypeParams).GetParams();
        } 
        protected override bool ParamsToPanel() {
            bool _ret=base.ParamsToPanel();
            this.txtCustomerNo.Text = GetParams().m_CustTypeNumber;
            this.txtVoltage.Value = (decimal)GetParams().m_SupplyVoltage;
            return _ret;
        }

        protected override void Retranslate() {
            this.lbCustomerNo.Text = Translator.getTranslator.Txt("lbCustomerNo", "Typeeditor");
            this.lbVoltage.Text = Translator.getTranslator.Txt("lbVoltage", "Typeeditor");
            
        }

       
    }

    public class DemoTypeParams : TypeParams {
        [Serializable]
        public class DemoParams: TypeParams.Params {

        public DemoParams() {
                m_ResultDef=new ResultDef();   
            }
            [OptionalField]
            public string m_CustTypeNumber;
            [OptionalField]
            public double m_SupplyVoltage;
        }
        protected new DemoParams m_Params;
        
        public DemoTypeParams() {
            m_Params = new DemoParams();
        }
        override protected void DeserializeObject(object Data) {
            m_Params = (DemoParams)Data;
        }
        public override Params GetParams() {
            return m_Params;
        }
    }
    
}
