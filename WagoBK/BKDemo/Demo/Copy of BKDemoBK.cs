//#define AUTODETECT


using System;
using System.Collections.Generic;
using System.Text;
using WagoBK;

namespace BKDemo
{
	public class BKDemoBK : WagoBKBase
	{
		public BKDemoBK(): base() {}
		protected override Channels CreateChannels()
		{ return new BKDemoChannels(); }
		public new BKDemoChannels Ch()
		{ return (BKDemoChannels)base.Ch(); }
		public class BKDemoChannels : Channels
		{
			public BKDemoChannels() : base()
			{ }
            protected override void InitChannelList()
            {
#if AUTODETECT
                AutodetectChannels = true; 
                }
#else           
                m_Card482 = new CardDef842();
                m_Card468 = new CardDef468();
                m_Card479 = new CardDef479();
                m_Card430 = new CardDef430();
                m_Card430_2 = new CardDef430();
                m_Card550 = new CardDef550();
                m_Card511 = new CardDef511();

                int Slot = 0;
                ProjectCards.AddCard(Slot++, m_Card482);
                ProjectCards.AddCard(Slot++, m_Card430);
                ProjectCards.AddCard(Slot++, m_Card511);
                ProjectCards.AddCard(Slot++, m_Card430_2);
                ProjectCards.AddCard(Slot++, m_Card479);
                ProjectCards.AddCard(Slot++, m_Card468);
                ProjectCards.AddCard(Slot++, m_Card550);
                

                DIChannels.Add("DI_0", new DIChannelDef("DI_0", "010100A0.0", m_Card430, 0));
                DIChannels.Add("DI_1", new DIChannelDef("DI_1", "010100A0.1", m_Card430, 1));
                DIChannels.Add("DI_2", new DIChannelDef("DI_2", "010100A0.2", m_Card430, 2));
                DIChannels.Add("DI_3", new DIChannelDef("DI_3", "010100A0.3", m_Card430, 3));
                DIChannels.Add("DI_4", new DIChannelDef("DI_4", "010100A0.4", m_Card430, 4));
                DIChannels.Add("DI_5", new DIChannelDef("DI_5", "010100A0.5", m_Card430, 5));
                DIChannels.Add("DI_6", new DIChannelDef("DI_6", "010100A0.6", m_Card430, 6));
                DIChannels.Add("DI_7", new DIChannelDef("DI_7", "010100A0.7", m_Card430, 7));

                AIChannels.Add("AI_0", new AIChannelDef("AI_0","",m_Card468,0));
                AIChannels.Add("AI_1", new AIChannelDef("AI_1", "", m_Card468, 1));
                AIChannels.Add("AI_2", new AIChannelDef("AI_2", "", m_Card468, 2));
                AIChannels.Add("AI_3", new AIChannelDef("AI_3", "", m_Card468, 3));

                AOChannels.Add("AO_0", new AOChannelDef("AO_0", "", m_Card550, 0));
                AOChannels.Add("AO_1", new AOChannelDef("AO_1", "", m_Card550, 1));
                AOChannels.Add("PWM_0", new AOChannelDef("PWM_0", "", m_Card511, 0));
                AOChannels.Add("PWM_1", new AOChannelDef("PWM_1", "", m_Card511, 1));
			}
			public bool DI_0
			{
                get { return DIChannels["DI_0"].Get(); }
                set { DIChannels["DI_0"].Set(value); }
			}
            public bool DI_1
			{
                get { return DIChannels["DI_1"].Get(); }
                set { DIChannels["DI_1"].Set(value); }
			}
            public bool DI_2
			{
                get { return DIChannels["DI_2"].Get(); }
                set { DIChannels["DI_2"].Set(value); }
			}
            public bool DI_3
			{
                get { return DIChannels["DI_3"].Get(); }
                set { DIChannels["DI_3"].Set(value); }
			}
            public bool DI_4
			{
                get { return DIChannels["DI_4"].Get(); }
                set { DIChannels["DI_4"].Set(value); }
			}
            public bool DI_5
			{
                get { return DIChannels["DI_5"].Get(); }
                set { DIChannels["DI_5"].Set(value); }
			}
            public bool DI_6
			{
                get { return DIChannels["DI_6"].Get(); }
                set { DIChannels["DI_6"].Set(value); }
			}
            public bool DI_7
			{
                get { return DIChannels["DI_7"].Get(); }
                set { DIChannels["DI_7"].Set(value); }
			}
#endif
            
            CardDef842 m_Card482;
            CardDef468 m_Card468;
            CardDef479 m_Card479;
            CardDef430 m_Card430;
            CardDef430 m_Card430_2;
            CardDef550 m_Card550; 
            CardDef511 m_Card511;
            public CardDef511 Card511
            {
                get { return m_Card511; }
            }
		}
	}
}
