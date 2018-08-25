using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using WagoBK;

namespace Demo2 {
    //simulate a BK
    public class DemoBK : WagoBKBase {
        public class DemoChannels : Channels {
            public DemoChannels()
                : base() {
            }
            protected override void InitChannelList() {
                int Slot = 0;
                m_CardDef842_0 = new CardDef842(Slot);
                ProjectCards.AddCard(Slot++, CardDef842_0);
                m_CardDef430_1 = new CardDef430(Slot);
                ProjectCards.AddCard(Slot++, CardDef430_1);
                m_CardDef430_2 = new CardDef430(Slot);
                ProjectCards.AddCard(Slot++, CardDef430_2);
                m_CardDef530_1 = new CardDef530(Slot);
                ProjectCards.AddCard(Slot++, CardDef530_1);

                DOChannels.Add("PCReady", new DOChannelDef("PCReady", "A01", m_CardDef530_1, 0));
                DOChannels.Add("Pass", new DOChannelDef("Pass", "A02", m_CardDef530_1, 1));
                DOChannels.Add("Fail", new DOChannelDef("Fail", "A03", m_CardDef530_1, 2));
                DOChannels.Add("SledgeToWP", new DOChannelDef("SledgeToWP", "A04", m_CardDef530_1, 3));
                DOChannels.Add("IndexToWP", new DOChannelDef("IndexToWP", "A05", m_CardDef530_1, 4));
                DOChannels.Add("CloseGripper", new DOChannelDef("CloseGripper", "A06", m_CardDef530_1, 5));

                DIChannels.Add("MainPressureReady", new DIChannelDef("MainPressureReady", "E01", m_CardDef430_1, 0));
                DIChannels.Add("Start", new DIChannelDef("Start", "E02", m_CardDef430_1, 1));
                DIChannels.Add("Stop", new DIChannelDef("Stop", "E03", m_CardDef430_1, 2));
                DIChannels.Add("SledgeInHP", new DIChannelDef("SledgeInHP", "E04", m_CardDef430_1, 3));
                DIChannels.Add("SledgeInWP", new DIChannelDef("SledgeInWP", "E05", m_CardDef430_1, 4));
                DIChannels.Add("IndexInHP", new DIChannelDef("IndexInHP", "E06", m_CardDef430_1, 5));
                DIChannels.Add("IndexInWP", new DIChannelDef("IndexInWP", "E07", m_CardDef430_1, 6));
                DIChannels.Add("GripperInHP", new DIChannelDef("GripperInHP", "E01", m_CardDef430_2, 0));
                DIChannels.Add("GripperInWP", new DIChannelDef("GripperInWP", "E02", m_CardDef430_2, 1));
                DIChannels.Add("PartInserted", new DIChannelDef("PartInserted", "E03", m_CardDef430_2, 2));
            }

            //create properties for simple access
            private CardDef842 m_CardDef842_0;
            public CardDef842 CardDef842_0 {
                get { return m_CardDef842_0; }
            }
            private CardDef430 m_CardDef430_1;
            public CardDef430 CardDef430_1 {
                get { return m_CardDef430_1; }
            }
            private CardDef430 m_CardDef430_2;
            public CardDef430 CardDef430_2 {
                get { return m_CardDef430_2; }
            }
            private CardDef530 m_CardDef530_1;
            public CardDef530 CardDef530_1 {
                get { return m_CardDef530_1; }
            }
            public bool DO_PCReady {
                get { return DOChannels["PCReady"].Get(); }
                set { DOChannels["PCReady"].Set(value); }
            }
            public bool DO_Pass {
                get { return DOChannels["Pass"].Get(); }
                set { DOChannels["Pass"].Set(value); }
            }
            public bool DO_Fail {
                get { return DOChannels["Fail"].Get(); }
                set { DOChannels["Fail"].Set(value); }
            }
            public bool DO_SledgeToWP {
                get { return DOChannels["SledgeToWP"].Get(); }
                set { DOChannels["SledgeToWP"].Set(value); }
            }
            public bool DO_IndexToWP {
                get { return DOChannels["IndexToWP"].Get(); }
                set { DOChannels["IndexToWP"].Set(value); }
            }
            public bool DO_CloseGripper {
                get { return DOChannels["CloseGripper"].Get(); }
                set { DOChannels["CloseGripper"].Set(value); }
            }
            public bool DI_MainPressureReady {
                get { return DIChannels["MainPressureReady"].Get(); }
            }
            public bool DI_Start {
                get { return DIChannels["Start"].Get(); }
            }
            public bool DI_Stop {
                get { return DIChannels["Stop"].Get(); }
            }
            public bool DI_SledgeInHP {
                get { return DIChannels["SledgeInHP"].Get(); }
            }
            public bool DI_SledgeInWP {
                get { return DIChannels["SledgeInWP"].Get(); }
            }
            public bool DI_IndexInHP {
                get { return DIChannels["IndexInHP"].Get(); }
            }
            public bool DI_IndexInWP {
                get { return DIChannels["IndexInWP"].Get(); }
            }
            public bool DI_GripperInHP {
                get { return DIChannels["GripperInHP"].Get(); }
            }
            public bool DI_GripperInWP {
                get { return DIChannels["GripperInWP"].Get(); }
            }
            public bool DI_PartInserted {
                get { return DIChannels["PartInserted"].Get(); }
            }
        }

        public DemoBK()
            : base() {
        }
        protected override Channels CreateChannels() {
            return new DemoChannels();
        }
        public new DemoChannels Ch() {
            return (DemoChannels)base.Ch();
        }

    }
}
