using System;
using System.Collections.Generic;
using System.Text;
using WagoBK;
namespace BKDemo
{
	public class BKDemoBK : WagoBKBase
	{
		public BKDemoBK(): base() {}
        public override void CheckInterlock()
        {
            Ch().AOChannels["PWM0"].DisableUI(!Ch().DIChannels["MainPressure"].Get());
        }
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
				int Slot = 0;
                m_CardDef842_0 = new CardDef842(Slot);
				ProjectCards.AddCard(Slot++, CardDef842_0);
                m_CardDef430_1 = new CardDef430(Slot);
				ProjectCards.AddCard(Slot++, CardDef430_1);
                m_CardDef511_2 = new CardDef511(Slot);
				ProjectCards.AddCard(Slot++, CardDef511_2);
                m_CardDef430_3 = new CardDef430(Slot);
				ProjectCards.AddCard(Slot++, CardDef430_3);
                m_CardDef479_4 = new CardDef479(Slot);
				ProjectCards.AddCard(Slot++, CardDef479_4);
                m_CardDef468_5 = new CardDef468(Slot);
				ProjectCards.AddCard(Slot++, CardDef468_5);
                m_CardDef550_6 = new CardDef550(Slot);
				ProjectCards.AddCard(Slot++, CardDef550_6);
				DIChannels.Add("MainPressure", new DIChannelDef("MainPressure","E 0.0", m_CardDef430_1,0));
				DIChannels.Add("ReducedPressure", new DIChannelDef("ReducedPressure","E 0.1", m_CardDef430_1,1));
				DIChannels.Add("ELOG", new DIChannelDef("ELOG","E 0.2", m_CardDef430_1,2));
				DIChannels.Add("ServiceDoor", new DIChannelDef("ServiceDoor","E 0.3", m_CardDef430_1,3));
				DIChannels.Add("PNOZOK", new DIChannelDef("PNOZOK","E 0.4", m_CardDef430_1,4));
				DIChannels.Add("Bereichsventil", new DIChannelDef("Bereichsventil","E 0.5", m_CardDef430_1,5));
				DIChannels.Add("StartIOButton", new DIChannelDef("StartIOButton","E 0.6", m_CardDef430_1,6));
				DIChannels.Add("StoppNIOButton", new DIChannelDef("StoppNIOButton","E 0.7", m_CardDef430_1,7));
				DIChannels.Add("MainPressure_2", new DIChannelDef("MainPressure_2","E 2.0", m_CardDef430_3,0));
				DIChannels.Add("ReducedPressure_2", new DIChannelDef("ReducedPressure_2","E 2.1", m_CardDef430_3,1));
				DIChannels.Add("ELOG_2", new DIChannelDef("ELOG_2","E 2.2", m_CardDef430_3,2));
				DIChannels.Add("ServiceDoor_2", new DIChannelDef("ServiceDoor_2","E 2.3", m_CardDef430_3,3));
				DIChannels.Add("PNOZOK_2", new DIChannelDef("PNOZOK_2","E 2.4", m_CardDef430_3,4));
				DIChannels.Add("Bereichsventil_2", new DIChannelDef("Bereichsventil_2","E 2.5", m_CardDef430_3,5));
				DIChannels.Add("StartIOButton_2", new DIChannelDef("StartIOButton_2","E 2.6", m_CardDef430_3,6));
				DIChannels.Add("StoppNIOButton_2", new DIChannelDef("StoppNIOButton_2","E 2.7", m_CardDef430_3,7));
				
				
				AIChannels.Add("AI30", new AIChannelDef("AI30","E 3.0", m_CardDef479_4,0));
				AIChannels.Add("AI31", new AIChannelDef("AI31","E 3.1", m_CardDef479_4,1));
				AIChannels.Add("AI40", new AIChannelDef("AI40","E 4.0", m_CardDef468_5,0));
				AIChannels.Add("AI41", new AIChannelDef("AI41","E 4.1", m_CardDef468_5,1));
				AIChannels.Add("AI42", new AIChannelDef("AI42","E 4.2", m_CardDef468_5,2));
				AIChannels.Add("AI43", new AIChannelDef("AI43","E 4.3", m_CardDef468_5,3));
				
				AOChannels.Add("PWM0", new AOChannelDef("PWM0","A 1.0", m_CardDef511_2,0));
				AOChannels.Add("PWM1", new AOChannelDef("PWM1","A 1.1", m_CardDef511_2,1));
				AOChannels.Add("AO0", new AOChannelDef("AO0","A 5.0", m_CardDef550_6,0));
				AOChannels.Add("AO1", new AOChannelDef("AO1","A 5.1", m_CardDef550_6,1));
			}

            public bool DI_MainPressure
			{
                get { return DIChannels["MainPressure"].Get(); ; }
			}
			public bool DI_ReducedPressure
			{
				get { return DIChannels["ReducedPressure"].Get(); }
			}
			public bool DI_ELOG
			{
				get { return DIChannels["ELOG"].Get(); }
			}
			public bool DI_ServiceDoor
			{
				get { return DIChannels["ServiceDoor"].Get(); }
			}
			public bool DI_PNOZOK
			{
				get { return DIChannels["PNOZOK"].Get(); }
			}
			public bool DI_Bereichsventil
			{
				get { return DIChannels["Bereichsventil"].Get(); }
			}
			public bool DI_StartIOButton
			{
				get { return DIChannels["StartIOButton"].Get(); }
			}
			public bool DI_StoppNIOButton
			{
				get { return DIChannels["StoppNIOButton"].Get(); }
			}
			public bool DI_MainPressure_2
			{
				get { return DIChannels["MainPressure_2"].Get(); }
			}
			public bool DI_ReducedPressure_2
			{
				get { return DIChannels["ReducedPressure_2"].Get(); }
			}
			public bool DI_ELOG_2
			{
				get { return DIChannels["ELOG_2"].Get(); }
			}
			public bool DI_ServiceDoor_2
			{
				get { return DIChannels["ServiceDoor_2"].Get(); }
			}
			public bool DI_PNOZOK_2
			{
				get { return DIChannels["PNOZOK_2"].Get(); }
			}
			public bool DI_Bereichsventil_2
			{
				get { return DIChannels["Bereichsventil_2"].Get(); }
			}
			public bool DI_StartIOButton_2
			{
				get { return DIChannels["StartIOButton_2"].Get(); }
			}
			public bool DI_StoppNIOButton_2
			{
				get { return DIChannels["StoppNIOButton_2"].Get(); }
			}
			private CardDef842 m_CardDef842_0;
			public CardDef842 CardDef842_0
			{
				get { return m_CardDef842_0; }
			}
			private CardDef430 m_CardDef430_1;
			public CardDef430 CardDef430_1
			{
				get { return m_CardDef430_1; }
			}
			private CardDef511 m_CardDef511_2;
			public CardDef511 CardDef511_2
			{
				get { return m_CardDef511_2; }
			}
			private CardDef430 m_CardDef430_3;
			public CardDef430 CardDef430_3
			{
				get { return m_CardDef430_3; }
			}
			private CardDef479 m_CardDef479_4;
			public CardDef479 CardDef479_4
			{
				get { return m_CardDef479_4; }
			}
			private CardDef468 m_CardDef468_5;
			public CardDef468 CardDef468_5
			{
				get { return m_CardDef468_5; }
			}
			private CardDef550 m_CardDef550_6;
			public CardDef550 CardDef550_6
			{
				get { return m_CardDef550_6; }
			}
		}
	}
}
