using System;
using System.Collections.Generic;
using System.Text;
using WagoBK;

namespace Demo
{
    public class DemoBK : WagoBKBase
    {
        public class DemoChannels : Channels
        {
            public DemoChannels():base()
            {
            }
            protected override void InitChannelList()
            {
                CardDef841 Card481 = new CardDef841(0);
                CardDef530 Card530 = new CardDef530(1);
                CardDef430 Card430 = new CardDef430(2);

                ProjectCards.AddCard(0, Card481);
                ProjectCards.AddCard(1, Card530);
                ProjectCards.AddCard(2, Card430);

                //create channel collection for easier management
                DOChannels.Add("PCReady", new DOChannelDef("PCReady","",Card530,0));
                DOChannels.Add("Pass", new DOChannelDef("Pass", "", Card530, 1));
                DOChannels.Add("Fail", new DOChannelDef("Fail", "", Card530, 2));
                DOChannels.Add("SledgeToWP", new DOChannelDef("SledgeToWP", "", Card530, 3));
                DOChannels.Add("IndexToWP", new DOChannelDef("IndexToWP", "", Card530, 4));

                DIChannels.Add("MainPressureReady", new DIChannelDef("MainPressureReady", "", Card430, 0));
                DIChannels.Add("Start", new DIChannelDef("Start", "", Card430, 1));
                DIChannels.Add("Stop", new DIChannelDef("Stop", "", Card430, 2));
                DIChannels.Add("SledgeInHP", new DIChannelDef("SledgeInHP", "", Card430, 3));
                DIChannels.Add("SledgeInWP", new DIChannelDef("SledgeInWP", "", Card430, 4));
                DIChannels.Add("IndexInHP", new DIChannelDef("IndexInHP", "", Card430, 5));
                DIChannels.Add("IndexInWP", new DIChannelDef("IndexInWP", "", Card430, 6));
            }

            //create properties for simple access
            public bool DO_PCReady
            {
                get { return DOChannels["PCReady"].Get(); }
                set { DOChannels["PCReady"].Set(value); }
            }
            public bool DO_Pass
            {
                get { return DOChannels["Pass"].Get(); }
                set { DOChannels["Pass"].Set(value); }
            }
            public bool DO_Fail
            {
                get { return DOChannels["Fail"].Get(); }
                set { DOChannels["Fail"].Set(value); }
            }
            public bool DO_SledgeToWP
            {
                get { return DOChannels["SledgeToWP"].Get(); }
                set { DOChannels["SledgeToWP"].Set(value); }
            }
            public bool DO_IndexToWP
            {
                get { return DOChannels["IndexToWP"].Get(); }
                set { DOChannels["IndexToWP"].Set(value); }
            }
            public bool DI_MainPressureReady
            {
                get { return DIChannels["MainPressureReady"].Get(); }
            }
            public bool DI_Start
            {
                get { return DIChannels["Start"].Get(); }
            }
            public bool DI_Stop
            {
                get { return DIChannels["Stop"].Get(); }
            }
            public bool DI_SledgeInHP
            {
                get { return DIChannels["SledgeInHP"].Get(); }
            }
            public bool DI_SledgeInWP
            {
                get { return DIChannels["SledgeInWP"].Get(); }
            }
            public bool DI_IndexInHP
            {
                get { return DIChannels["IndexInHP"].Get(); }
            }
            public bool DI_IndexInWP
            {
                get { return DIChannels["IndexInWP"].Get(); }
            }

        }
        public DemoBK():base()
        {
        }
        protected override Channels CreateChannels()
        {
            return new DemoChannels();
        }
        public new DemoChannels Ch()
        {
            return (DemoChannels)base.Ch();
        }

    }
}
