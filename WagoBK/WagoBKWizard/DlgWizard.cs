using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WagoBK;

namespace WagoBKWizard
{
    public partial class DlgWizard : Form
    {
        public class FileCreator
        {
            public FileCreator(Stream OutputFile)
            {
                m_TabLevel = 0;
                if (OutputFile == null) throw new Exception("invalid output file");
                m_TextWriter = new StreamWriter(OutputFile);
            }
            public void CloseFile()
            {
                if (m_TextWriter != null) m_TextWriter.Close();
            }
            public void WriteLine(string Text)
            {
                int TabLevel = m_TabLevel;
                if (Text.StartsWith("}")) 
                {   
                    TabLevel--;
                }
                Text=Text.PadLeft(Text.Length + TabLevel, '\t');
                m_TextWriter.WriteLine(Text);
                if (Text.Contains("{")) m_TabLevel++;
                if (Text.Contains("}")) m_TabLevel--;
            }
            int m_TabLevel;
            System.IO.StreamWriter m_TextWriter;
        }

        public DlgWizard()
        {
            InitializeComponent();
        }
        private void LogStatus(string Msg)
        {
            string OutText = Msg + "\r\n" + textBox1.Text;
            if (OutText.Length > 1000)
            {
                OutText.Remove(1000);
            }
            this.textBox1.Text = OutText;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ProjectName = textBox3.Text;
                if (ProjectName == "") throw new Exception("invalid ProjectPrefix");
                LogStatus("Preparing creation for " + ProjectName);
                //openFileDialog1.Filter = "csv|(*.csv)";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {   //parse the fíle
                    Stream InputFile = openFileDialog1.OpenFile();
                    LogStatus("Reading file...");
                    string OutputFileName = Path.GetDirectoryName(openFileDialog1.FileName) + "\\" + ProjectName + "BK.cs";
                    WagoBK.Channels MyChannels = CreateChannelsFromFile(InputFile);
                    LogStatus("Creating file " + OutputFileName);
                    FileStream ClassFile = System.IO.File.Create(OutputFileName);
                    CreateClassFileFromChannels(MyChannels, ProjectName, ClassFile);
                    LogStatus("Creation completed for " + ProjectName);
                }
                else
                {
                    LogStatus("Creation canceled by user");
                }
            }
            catch (Exception ex)
            {
                LogStatus(ex.Message);
            }
        }
        private void ValidateChannelDef()
        {
 
        }
        private WagoBK.Channels CreateChannelsFromFile(Stream InputFile)
        {
                //create Channel-Collection that will temporary hold imported data
                WagoBK.Channels MyChannels = new Channels();
                MyChannels.DIChannels.Clear();
                MyChannels.DOChannels.Clear();
                MyChannels.AIChannels.Clear();
                MyChannels.AOChannels.Clear();
                CardDefBase Card= null;
                int CardChCounter = 0;
                int CardSlot = 0;
                MyChannels.ProjectCards.AddCard(CardSlot, new CardDef842(CardSlot)); //koppler not contained in file
                CardSlot++;
                System.IO.StreamReader TextReader = new System.IO.StreamReader(InputFile);
                while (!TextReader.EndOfStream)
                {
                    string Line=TextReader.ReadLine();
                    string[] sep = {";"};
                    string[] ChannelDefString= Line.Split(sep, 4, StringSplitOptions.None);
                    string Name = ChannelDefString[3];
                    string BMK = ChannelDefString[0];
                    UInt16 CardCode = UInt16.Parse(ChannelDefString[2]);
                    char InOut = ChannelDefString[1].ToCharArray(0, 1)[0];
                    //check for doubled or invalid names??
                    string[] SpecialChars = {" ",".","!","%","&","/", "\\","(", ")","{","}","*","+","-"};
                    foreach (string SpecialChar in SpecialChars)
                    {
                        if (Name.Contains(SpecialChar)) throw new Exception("invalid name " + Name);
                    }
                    if(Card==null || 
                        CardChCounter>= Card.GetChannelCount() ||
                        Card.GetType() != CardManager.CreateCardByCode(CardSlot,CardCode).GetType())
                    {
                        Card = CardManager.CreateCardByCode(CardSlot,CardCode);
                        CardChCounter =0;
                        MyChannels.ProjectCards.AddCard(CardSlot++,Card);
                    };
                    switch (Card.GetChannelType())
                    {
                                case ChannelType.AI:
                                    MyChannels.AIChannels.Add(Name, new AIChannelDef(Name, BMK, (CardDefAnalogIO)Card, CardChCounter));
                                    break;
                                case ChannelType.AO:
                                    MyChannels.AOChannels.Add(Name, new AOChannelDef(Name, BMK, (CardDefAnalogIO)Card, CardChCounter));
                                    break;
                                case ChannelType.DI:
                                    MyChannels.DIChannels.Add(Name, new DIChannelDef(Name, BMK, (CardDefDigitalIO)Card, CardChCounter));
                                    break;
                                case ChannelType.DO:
                                    MyChannels.DOChannels.Add(Name, new DOChannelDef(Name, BMK, (CardDefDigitalIO)Card, CardChCounter));
                                    break;
                    }
                    CardChCounter++;
                }

                TextReader.Close();
                return MyChannels;
            }
        private void CreateClassFileFromChannels(WagoBK.Channels MyChannels,string ProjectName, Stream OutputFile)
        {
                if (OutputFile == null) throw new Exception("output file is null");
                if (MyChannels == null) throw new Exception("Channel-Obj is null");
                int[] Slots = MyChannels.ProjectCards.GetSlotNumbers();
                FileCreator BKFileOut = new FileCreator(OutputFile);

                BKFileOut.WriteLine("using System;");
                BKFileOut.WriteLine("using System.Collections.Generic;");
                BKFileOut.WriteLine("using System.Text;");
                BKFileOut.WriteLine("using WagoBK;");

                BKFileOut.WriteLine("namespace " + ProjectName);
                BKFileOut.WriteLine("{");
                BKFileOut.WriteLine("public class " + ProjectName + "BK : WagoBKBase");
                BKFileOut.WriteLine("{");
                BKFileOut.WriteLine("public " + ProjectName + "BK(): base() {}");
                BKFileOut.WriteLine("protected override Channels CreateChannels()");
                BKFileOut.WriteLine("{ return new " + ProjectName + "Channels(); }");
                BKFileOut.WriteLine("public override void CheckInterlock(){}");
                BKFileOut.WriteLine("public new " + ProjectName + "Channels Ch()");
                BKFileOut.WriteLine("{ return (" + ProjectName + "Channels)base.Ch(); }");
                BKFileOut.WriteLine("");
                BKFileOut.WriteLine("public class " + ProjectName + "Channels : Channels");
                BKFileOut.WriteLine("{");
                BKFileOut.WriteLine("public " + ProjectName + "Channels() : base()");
                BKFileOut.WriteLine("{ }");
                BKFileOut.WriteLine("protected override void InitChannelList()");
                BKFileOut.WriteLine("{");

            //initialise and add Cards
                BKFileOut.WriteLine("int Slot = 0;");
                foreach (int Slot in Slots)
                {
                    BKFileOut.WriteLine(" m_" + MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "_" + Slot.ToString() +
                        " = new "+ MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "();");
                    BKFileOut.WriteLine("ProjectCards.AddCard(Slot++, " + MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "_" + Slot.ToString()+
                        ");"); 
                }

                // define Channel-List
                foreach (string Name in MyChannels.DIChannels.Keys)
                {
                    BKFileOut.WriteLine("DIChannels.Add(\"" + Name + "\", new DIChannelDef(\"" +
                        MyChannels.DIChannels[Name].GetName() + "\",\"" +
                        MyChannels.DIChannels[Name].GetBMK() + "\"," +
                        " m_" + MyChannels.DIChannels[Name].GetCard().GetType().Name +
                        "_" + MyChannels.ProjectCards.GetCardSlot(MyChannels.DIChannels[Name].GetCard()).ToString() + "," +
                        MyChannels.DIChannels[Name].GetChannel().ToString() + "));");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.DOChannels.Keys)
                {
                    BKFileOut.WriteLine("DOChannels.Add(\"" + Name + "\", new DOChannelDef(\"" +
                        MyChannels.DOChannels[Name].GetName() + "\",\"" +
                        MyChannels.DOChannels[Name].GetBMK() + "\"," +
                        " m_" + MyChannels.DOChannels[Name].GetCard().GetType().Name +
                        "_" + MyChannels.ProjectCards.GetCardSlot(MyChannels.DOChannels[Name].GetCard()).ToString() + "," +
                        MyChannels.DOChannels[Name].GetChannel().ToString() + "));");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.AIChannels.Keys)
                {
                    BKFileOut.WriteLine("AIChannels.Add(\"" + Name + "\", new AIChannelDef(\"" +
                        MyChannels.AIChannels[Name].GetName() + "\",\"" +
                        MyChannels.AIChannels[Name].GetBMK() + "\"," +
                        " m_" + MyChannels.AIChannels[Name].GetCard().GetType().Name +
                        "_" + MyChannels.ProjectCards.GetCardSlot(MyChannels.AIChannels[Name].GetCard()).ToString() + "," +
                        MyChannels.AIChannels[Name].GetChannel().ToString() + "));");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.AOChannels.Keys)
                {
                    BKFileOut.WriteLine("AOChannels.Add(\"" + Name + "\", new AOChannelDef(\"" +
                        MyChannels.AOChannels[Name].GetName() + "\",\"" +
                        MyChannels.AOChannels[Name].GetBMK() + "\"," +
                        " m_" + MyChannels.AOChannels[Name].GetCard().GetType().Name +
                        "_" + MyChannels.ProjectCards.GetCardSlot(MyChannels.AOChannels[Name].GetCard()).ToString() + "," +
                        MyChannels.AOChannels[Name].GetChannel().ToString() + "));");
                }
                BKFileOut.WriteLine("}");
                //add property for each Channel
                foreach (string Name in MyChannels.DOChannels.Keys)
                {
                    BKFileOut.WriteLine("public bool DO_" + Name);
                    BKFileOut.WriteLine("{");
                    BKFileOut.WriteLine("get { return DOChannels[\"" + Name + "\"].Get(); }");
                    BKFileOut.WriteLine("set { DOChannels[\"" + Name + "\"].Set(value); }");
                    BKFileOut.WriteLine("}");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.DIChannels.Keys)
                {
                    BKFileOut.WriteLine("public bool DI_" + Name);
                    BKFileOut.WriteLine("{");
                    BKFileOut.WriteLine("get { return DIChannels[\"" + Name + "\"].Get(); }");
                    BKFileOut.WriteLine("}");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.AIChannels.Keys)
                {
                    BKFileOut.WriteLine("public double AI_" + Name);
                    BKFileOut.WriteLine("{");
                    BKFileOut.WriteLine("get { return AIChannels[\"" + Name + "\"].Get(); }");
                    BKFileOut.WriteLine("}");
                }
                BKFileOut.WriteLine("");
                foreach (string Name in MyChannels.AOChannels.Keys)
                {
                    BKFileOut.WriteLine("public double AO_" + Name);
                    BKFileOut.WriteLine("{");
                    BKFileOut.WriteLine("get { return AOChannels[\"" + Name + "\"].Get(); }");
                    BKFileOut.WriteLine("set { AOChannels[\"" + Name + "\"].Set(value); }");
                    BKFileOut.WriteLine("}");
                }
                //carddefinition
                foreach (int Slot in Slots)
                {
                    BKFileOut.WriteLine("private " + MyChannels.ProjectCards.GetCard(Slot).GetType().Name+ 
                        " m_" +MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "_"+Slot.ToString()+
                        ";");
                    BKFileOut.WriteLine("public " + MyChannels.ProjectCards.GetCard(Slot).GetType().Name + 
                        " "+ MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "_"+Slot.ToString());
                    BKFileOut.WriteLine("{");
                    BKFileOut.WriteLine("get { return m_" +MyChannels.ProjectCards.GetCard(Slot).GetType().Name + "_"+Slot.ToString()+"; }");
                    BKFileOut.WriteLine("}");
                }

                BKFileOut.WriteLine("}");
                BKFileOut.WriteLine("}");
                BKFileOut.WriteLine("}");

                BKFileOut.CloseFile();
            }


   }
 }
