using System;
using System.ComponentModel;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace Maschine1
{
    [Serializable]
    public class ResultDef
    {
        #region Types and Constants
        public enum TestResultEnum
        {
            NotTested = 0,
            Ignored,
            Pass,
            Fail
        }
        #endregion
        #region LimitDef
        [Serializable]
        public abstract class LimitDefBase { //: Maschine1.Core.ISerializable {
            public LimitDefBase() { }
            public LimitDefBase(string Name){
                m_Name = Name;
            }
            public string GetName() {
                return m_Name;
            }
            public virtual string GetValueString() {
                return "";
            }
            private string m_Name;

           // public abstract void WriteToSerializer(Maschine1.Core.SerializerBase Stream);
           // public abstract void ReadFromSerializer(Maschine1.Core.SerializerBase Stream);
        }
        [Serializable]
        public class LimitDefDouble : LimitDefBase 
        {
            [Serializable]
            public struct strLimit
            {
                public strLimit(double Lower, double Upper)
                {
                    m_LowerLimit = Lower;
                    m_UpperLimit = Upper;
                    m_Unit = "";
                }
                public double m_LowerLimit;
                public double m_UpperLimit;
                [OptionalField]
                public string m_Unit; 
            }
            public LimitDefDouble(string Name, strLimit Limit)
                : base(Name) {
                SetLimit(Limit);
            }
            public strLimit GetLimit()  {
                return m_Limit;
            }
            public void SetLimit(strLimit Limit)  {
                m_Limit = Limit;
            }
            public TestResultEnum Compare(double Value) {
                if (m_Limit.m_LowerLimit <= Value && 
                    Value <= m_Limit.m_UpperLimit)
                {
                    return TestResultEnum.Pass;
                }
                return TestResultEnum.Fail;
            }
            public override string GetValueString()  {
                string text = string.Format("{0:f} <= X <= {1:f}", 
                    m_Limit.m_LowerLimit, m_Limit.m_UpperLimit);
                return text;
            }
            private strLimit m_Limit;

           /* public override void ReadFromSerializer(Core.SerializerBase Stream) {
                throw new NotImplementedException();
            }
            public override void WriteToSerializer(Core.SerializerBase Stream) {
                Stream.WriteElementStart("LimitDefDouble");
                Stream.WriteData("Name", GetName());
                Stream.WriteData("LowerLimit",m_Limit.m_LowerLimit);
                Stream.WriteData("UpperLimit", m_Limit.m_UpperLimit);
                Stream.WriteElementEnd("LimitDefDouble");
            }*/
            /// <summary>
            /// 
            /// </summary>
            public partial class LimitDefDoubleEditor : UserControl
            {
                public LimitDefDoubleEditor()
                {
                    InitializeComponent();
                }
                private System.ComponentModel.IContainer components = null;
                protected override void Dispose(bool disposing)
                {
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }
                private void InitializeComponent()
                {
                    this.txtName = new System.Windows.Forms.TextBox();
                    this.txtLower = new System.Windows.Forms.TextBox();
                    this.txtUpper = new System.Windows.Forms.TextBox();
                    this.SuspendLayout();

                    // txtName
                    // 
                    this.txtName.Location = new System.Drawing.Point(0, 0);
                    this.txtName.Name = "txtName";
                    this.txtName.Size = new System.Drawing.Size(136, 20);
                    this.txtName.TabIndex = 1;
                    // 
                    // txtLower
                    // 
                    this.txtLower.Location = new System.Drawing.Point(143, 0);
                    this.txtLower.Name = "txtLower";
                    this.txtLower.Size = new System.Drawing.Size(68, 20);
                    this.txtLower.TabIndex = 2;
                    // 
                    // txtUpper
                    // 
                    this.txtUpper.Location = new System.Drawing.Point(216, 0);
                    this.txtUpper.Name = "txtUpper";
                    this.txtUpper.Size = new System.Drawing.Size(68, 20);
                    this.txtUpper.TabIndex = 3;
                    // 
                    // LimitEditor
                    // 
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    this.Controls.Add(this.txtUpper);
                    this.Controls.Add(this.txtLower);
                    this.Controls.Add(this.txtName);
                    this.Name = "LimitDefDoubleEditor";
                    this.Size = new System.Drawing.Size(300, 100);
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }
                public void SetData(LimitDefDouble Limit)
                {
                    txtName.Text = Limit.GetName();
                    txtLower.Text = Limit.GetLimit().m_LowerLimit.ToString();
                    txtUpper.Text = Limit.GetLimit().m_UpperLimit.ToString();
                }
                public LimitDefDouble GetData()
                {
                    //?? validieren der Daten
                   strLimit Values = new strLimit(Convert.ToDouble(txtLower.Text),
                                                    Convert.ToDouble(txtUpper.Text));
                    LimitDefDouble Limit = new LimitDefDouble(txtName.Text, Values);
                    return Limit;
                }
                private System.Windows.Forms.TextBox txtName;
                private System.Windows.Forms.TextBox txtLower;
                private System.Windows.Forms.TextBox txtUpper;
            }
        }
        [Serializable]
        public class LimitDefBool : LimitDefBase
        {
            [Serializable]
            public struct strLimit
            {
                public strLimit(bool Limit)
                {
                    m_Limit = Limit;
                }
               public bool m_Limit;
            }
            public LimitDefBool(string Name, strLimit Limit)
                : base(Name)
            {
                SetLimit(Limit);
            }
            public strLimit GetLimit()
            {
                return m_Limit;
            }
            public void SetLimit(strLimit Limit)
            {
                m_Limit = Limit;
            }
            public TestResultEnum Compare(bool Value)
            {
                if (m_Limit.m_Limit == Value )
                {
                    return TestResultEnum.Pass;
                }
                return TestResultEnum.Fail;
            }
            public override string GetValueString()
            {
                string text = m_Limit.m_Limit.ToString();
                return text;
            }
            private strLimit m_Limit;

            public partial class LimitDefBoolEditor : UserControl
            {
                public LimitDefBoolEditor()
                {
                    InitializeComponent();
                }
                private System.ComponentModel.IContainer components = null;
                protected override void Dispose(bool disposing)
                {
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }
                private void InitializeComponent()
                {
                    this.txtName = new System.Windows.Forms.TextBox();
                    this.btValue = new System.Windows.Forms.CheckBox();
                    this.SuspendLayout();

                    // txtName
                    this.txtName.Location = new System.Drawing.Point(0, 0);
                    this.txtName.Name = "txtName";
                    this.txtName.Size = new System.Drawing.Size(136, 20);
                    this.txtName.TabIndex = 1;
                    // 
                    this.btValue.Location = new System.Drawing.Point(143, 0);
                    this.btValue.TabIndex = 2;
   
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    this.Controls.Add(this.txtName);
                    this.Controls.Add(this.btValue);
                    this.Name = "LimitDefBoolEditor";
                    this.Size = new System.Drawing.Size(300, 100);
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }
                public void SetData(LimitDefBool Limit)
                {
                    txtName.Text = Limit.GetName();
                    btValue.Checked = Limit.GetLimit().m_Limit;
                }
                public LimitDefBool GetData()
                {
                    //?? validieren der Daten
                    strLimit Values = new strLimit(btValue.Checked);
                    LimitDefBool Limit = new LimitDefBool(txtName.Text, Values);
                    return Limit;
                }
                private System.Windows.Forms.TextBox txtName;
                private System.Windows.Forms.CheckBox btValue;
            }
        }
        #endregion
        #region TestStepDef
        [Serializable]
        public abstract class TestStepBase
        {
            private TestStepBase() { }
            public TestStepBase(string Name, string LimitName, bool Optional) {
                m_Name = Name;
                m_LimitName = LimitName;
                m_Result = Optional ? TestResultEnum.Ignored :TestResultEnum.NotTested;
            }
            public string GetName() {
                return m_Name;
            }
            public string GetLimitName() {
                return m_LimitName;
            }
            public string GetFailCode()  {
                return m_FailCode;
            }
            public DateTime GetTestTime() {
                return m_TestTime;
            }
            public void SetFailCode(string FailCode) {
                m_FailCode=FailCode;
            }
            public TestResultEnum GetResult()  {
                return m_Result;
            }
            public virtual string GetValueString()  {
                return "";
            }
            private string m_Name;
            private string m_LimitName;
            private string m_FailCode;
            protected TestResultEnum m_Result;
            [NonSerialized]
            protected DateTime m_TestTime = DateTime.MinValue;

            public partial class TestStepEditor : UserControl {
                public TestStepEditor()  {
                    InitializeComponent();
                }
                private System.ComponentModel.IContainer components = null;
                protected override void Dispose(bool disposing)   {
                    if (disposing && (components != null))  {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }
                private void InitializeComponent()  {
                    this.txtName = new System.Windows.Forms.TextBox();
                    this.txtFailCode = new System.Windows.Forms.TextBox();
                    this.txtLimit = new System.Windows.Forms.ComboBox();
                    this.btIgnore = new System.Windows.Forms.CheckBox();
                    this.SuspendLayout();

                    this.txtName.Location = new System.Drawing.Point(0, 0);
                    this.txtName.Name = "txtName";
                    this.txtName.Size = new System.Drawing.Size(120, 20);
                    this.txtName.TabIndex = 1;

                    this.txtFailCode.Location = new System.Drawing.Point(60, 25);
                    this.txtFailCode.Name = "txtFailCode";
                    this.txtFailCode.Size = new System.Drawing.Size(68, 20);
                    this.txtFailCode.TabIndex = 2;

                    this.btIgnore.Location = new System.Drawing.Point(140, 25);
                    this.btIgnore.TabIndex = 3;

                    this.txtLimit.Location = new System.Drawing.Point(125, 0);
                    this.txtLimit.Name = "txtLimit";
                    this.txtLimit.Size = new System.Drawing.Size(120, 20);
                    this.txtLimit.TabIndex = 4;

                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    this.Controls.Add(this.txtLimit);
                    this.Controls.Add(this.txtFailCode);
                    this.Controls.Add(this.txtName);
                    this.Controls.Add(this.btIgnore);
                    this.Name = "TestStepEditor";
                    this.Size = new System.Drawing.Size(300, 100);
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }
                public void SetData(TestStepBase Step,ResultDef LimitDef)  {
                    m_ResultDef = LimitDef;
                    txtLimit.Items.Clear();
                    ICollection LimitNames = m_ResultDef.GetLimitNames();
                    foreach (string Name in LimitNames)  {
                        txtLimit.Items.Add(Name);
                    }
                    if (Step != null)   {   //bei neuen Steps wird null übergeben!
                        txtLimit.Text = Step.GetLimitName();
                        txtName.Text = Step.GetName();
                        btIgnore.Checked = false;
                        txtFailCode.Text = Step.GetFailCode();
                    };
                    
                }
                public TestStepBase GetData()  {
                    //?? validieren der Daten
                    TestStepBase Step=null;
                    LimitDefBase Limit;
                    
                    m_ResultDef.GetLimitValue(txtLimit.Text,out Limit);
                    if (Limit.GetType().Equals(typeof(LimitDefBool)))
                    {
                        Step = new TestStepBool(txtName.Text, txtLimit.Text, btIgnore.Checked);
                    }
                    else if(Limit.GetType().Equals(typeof(LimitDefDouble)))
                    {
                        Step = new TestStepDouble(txtName.Text, txtLimit.Text, btIgnore.Checked);
                    }
                    Step.SetFailCode(txtFailCode.Text);  //??Fehlerbehandlung wenn Limit nicht definiert
                    return Step;
                }
                private ResultDef m_ResultDef;   //nur für die Auflistung der Limits
                private System.Windows.Forms.TextBox txtName;
                private System.Windows.Forms.ComboBox txtLimit;
                private System.Windows.Forms.TextBox txtFailCode;
                private System.Windows.Forms.CheckBox btIgnore; //??
            }
        }
        [Serializable]
        public class TestStepDouble : TestStepBase
        {
            public TestStepDouble(string Name, string LimitName,bool Optional):base(Name, LimitName,Optional)
            {   }
            public override string GetValueString()  {
                return m_Value.ToString("G")+" "+m_Unit;
            }
            public TestResultEnum SetResultValue(double Value, LimitDefDouble Limit) {
                m_Result = TestResultEnum.NotTested;
                m_Value = Value;
                m_Result = Limit.Compare(m_Value);
                m_TestTime = DateTime.Now;
                SetUnit(Limit.GetLimit().m_Unit);
                return m_Result;
            }
            protected void SetUnit(string Name) {
                m_Unit = Name;
            }
            public string GetUnit() {
                return m_Unit;
            }
            private double m_Value;
            private string m_Unit="";
        }
        [Serializable]
        public class TestStepBool : TestStepBase   {
            public TestStepBool(string Name, string LimitName, bool Optional):base(Name, LimitName, Optional)
            {     }
            public override string GetValueString()  {
                return m_Value.ToString();
            }
            public TestResultEnum SetResultValue(bool Value, LimitDefBool Limit)  {
                m_Result = TestResultEnum.NotTested;
                m_Value = Value;
                m_Result = Limit.Compare(m_Value);
                m_TestTime = DateTime.Now;
                return m_Result;
            }
            private bool m_Value;
        }
        #endregion
        #region Collections
        [Serializable]
        public class ResultCollection : DictionaryBase  {
            public ResultCollection(){  }
            public ResultCollection(ResultCollection CopyThis) {
                TestStepBase _Entry;
                IDictionaryEnumerator obj = CopyThis.GetEnumerator();
                while (obj.MoveNext() && obj.Current != null) {
                    _Entry = (TestStepBase)obj.Value;
                    Add(_Entry.GetName(), _Entry );
                }
            }
            public TestStepBase this[String key]  {
                get {
                    return ((TestStepBase)Dictionary[key]);
                }
                set {
                    Dictionary[key] = value;
                }
            }
            public ICollection Keys {
                get   {
                    return (Dictionary.Keys);
                }
            }
            public ICollection Values  {
                get  {
                    return (Dictionary.Values);
                }
            }
            public void Add(String key, TestStepBase value)  {
                Dictionary.Add(key, value);
            }

            public bool Contains(String key)  {
                return (Dictionary.Contains(key));
            }

            public void Remove(String key){
                Dictionary.Remove(key);
            }
        }
        [Serializable]
        public class LimitCollection : DictionaryBase {
            public LimitCollection(){   }
            public LimitCollection(LimitCollection CopyThis) {
                LimitDefBase _Entry;
                IDictionaryEnumerator obj = CopyThis.GetEnumerator();
                while (obj.MoveNext() && obj.Current != null) {
                    _Entry = (LimitDefBase)obj.Value;
                    Add(_Entry.GetName(), _Entry);
                }
            }
            public LimitDefBase this[String key] {
                get {
                    return ((LimitDefBase)Dictionary[key]);
                }
                set {
                    Dictionary[key] = value;
                }
            }
            public ICollection Keys {
                get {
                    return (Dictionary.Keys);
                }
            }
            public ICollection Values  {
                get {
                    return (Dictionary.Values);
                }
            }
            public void Add(String key, LimitDefBase value)      {
                Dictionary.Add(key, value);
            }
            public bool Contains(String key)  {
                return (Dictionary.Contains(key));
            }
            public void Remove(String key)  {
                Dictionary.Remove(key);
            }
        }
        #endregion
        public ResultDef(){
            m_Results = new ResultCollection();
            m_Limits = new LimitCollection(); 
        }
        public ResultDef(ResultDef CopyThis) {
            m_Results = new ResultCollection(CopyThis.m_Results);
            m_Limits = new LimitCollection(CopyThis.m_Limits);
        }
        //check if there is a limit for every test, if testnames are unique
        public bool ValidateResultDef()
        {
            //??
            return true;
        }
        public bool TestStepExists(string Name)
        {
            return m_Results.Contains(Name);
        }
        public bool LimitExists(string Name)
        {
            return m_Limits.Contains(Name);
        }
        public void GetResultValue(string Name,out TestStepBase Result )
        {
            TestStepBase Step = null;
            if (TestStepExists(Name))
            {
                    Step = m_Results[Name];
            };
            Result= Step;
        }
        public void GetResultValue(string Name, out TestStepDouble Result)
        {
            TestStepDouble Step = null;
            if (TestStepExists(Name))
            {
                if (!m_Results[Name].GetType().Equals(typeof(TestStepDouble)))
                {
                    Step = (TestStepDouble)m_Results[Name];
                }
            };
            Result= Step;
        }
        public TestResultEnum SetResultValue(string Name, double Value)
        {
            TestResultEnum _Result = TestResultEnum.NotTested;
            if (!m_Results.Contains(Name))  {
                throw new ArgumentOutOfRangeException(Name,"undefined");
                //return _Result;
            }
            if (!m_Results[Name].GetType().Equals(typeof(TestStepDouble))) {
                throw new ArgumentException(Name + " is not defined as TestStepDouble");
               // return _Result;
            }

            _Result = ((TestStepDouble)m_Results[Name]).SetResultValue(Value,
                (LimitDefDouble)m_Limits[m_Results[Name].GetLimitName()]);
            return _Result;
        }
        public void GetResultValue(string Name, out TestStepBool Result)
        {
            TestStepBool Step = null;
            if (TestStepExists(Name))
            {
                if (!m_Results[Name].GetType().Equals(typeof(TestStepBool)))
                {
                    Step = (TestStepBool)m_Results[Name];
                };
            };
            Result=Step;
        }
        public TestResultEnum SetResultValue(string Name, bool Value)
        {
            TestResultEnum _Result = TestResultEnum.NotTested;
            if (!m_Results.Contains(Name)) {
                throw new ArgumentOutOfRangeException(Name, "undefined");
               // return _Result;
            }
            if (!m_Results[Name].GetType().Equals(typeof(TestStepBool))) {
                throw new ArgumentException(Name + " is not defined as TestStepBool");
               // return _Result;
            }

            _Result = ((TestStepBool)m_Results[Name]).SetResultValue(Value,
                (LimitDefBool)m_Limits[m_Results[Name].GetLimitName()]);
            return _Result;
        }
        public ICollection GetTestStepNames()
        {
            return m_Results.Keys;
        }
        public ICollection GetLimitNames()
        {
            return m_Limits.Keys;
        }
        public TestResultEnum GetTotalResult()
        {
            TestResultEnum _Result = TestResultEnum.Pass;
            foreach (TestStepBase Test in m_Results.Values)
            {
                if((Test.GetResult()==TestResultEnum.Pass ||
                        Test.GetResult()==TestResultEnum.Ignored))
                {}
                else
                {
                    _Result=TestResultEnum.Fail;
                }
            }
            return _Result;
        }
        public TestResultEnum GetCurrentResult()
        {
            TestResultEnum _Result = TestResultEnum.Pass;
            foreach (TestStepBase Test in m_Results.Values)
            {
                if ((Test.GetResult() == TestResultEnum.Pass ||
                        Test.GetResult() == TestResultEnum.Ignored ||
                        Test.GetResult() == TestResultEnum.NotTested))
                { }
                else
                {
                    _Result = TestResultEnum.Fail;
                }
            }
            return _Result;
        }
        public void AddTestStep( TestStepBase Step)
        {
            if (TestStepExists(Step.GetName()))
            {
                //?? eindeutiger Namen erforderlich
            }
            else 
            {
                m_Results.Add(Step.GetName(),Step);
            }

        }
        public void ModifyTestStep(TestStepBase Step)
        {
            if (TestStepExists(Step.GetName()))
            {   //?? Vergleich alter neuer Testschritttype
                m_Results[Step.GetName()] = Step;
            }
            else
            {
                //?? Namen nicht gefunden
            }
        }
        public void DeleteTestStep(string Name)
        {
            if (TestStepExists(Name))
            {   //?? kein Fehler wenn Testschritt nicht existiert
                m_Results.Remove(Name);
            }
            else
            {}
        }
        public void AddLimit(LimitDefBase Limit)
        {
            if (LimitExists(Limit.GetName()))
            {
                //?? eindeutiger Namen erforderlich
            }
            else
            {
                m_Limits.Add(Limit.GetName(), Limit);
            }

        }
        public void ModifyLimit(LimitDefBase Limit)
        {
            if (LimitExists(Limit.GetName()))
            {   //?? Vergleich alter neuer Testschritttype
                m_Limits[Limit.GetName()] = Limit;
            }
            else
            {
                //?? Namen nicht gefunden
            }
        }
        public void DeleteLimit(string Name)
        {
            if (LimitExists(Name))
            {   //?? kein Fehler wenn Testschritt nicht existiert
                m_Limits.Remove(Name);
            }
            else
            { }
        }
        public void GetLimitValue(string Name, out LimitDefBase Limit)
        {
            LimitDefBase _Limit = null;
            if (LimitExists(Name))
            {
                _Limit = m_Limits[Name];
            };
            Limit = _Limit;
        }
        public void GetLimitValue(string Name, out LimitDefDouble Limit)
        {
            LimitDefDouble _Limit = null;
            if (LimitExists(Name))  {
                if (!m_Limits[Name].GetType().Equals(typeof(LimitDefDouble))) {
                    _Limit = (LimitDefDouble)m_Limits[Name];
                }
            };
            Limit = _Limit;
        }
        public void GetLimitValue(string Name, out LimitDefBool Limit)
        {
            LimitDefBool _Limit = null;
            if (LimitExists(Name)) {
                if (!m_Limits[Name].GetType().Equals(typeof(LimitDefBool)))
                {
                    _Limit = (LimitDefBool)m_Limits[Name];
                }
            };
            Limit = _Limit;
        }
        private ResultCollection m_Results;
        private LimitCollection m_Limits;
    }
}
