using System;
using System.Collections.Generic;
using System.Text;
using WagoBK;
namespace Project
{
	public class ProjectBK : WagoBKBase
	{
		public ProjectBK(): base() {}
		protected override Channels CreateChannels()
		{ return new ProjectChannels(); }
		public new ProjectChannels Ch()
		{ return (ProjectChannels)base.Ch(); }
		public class ProjectChannels() : base()
		{ }
		protected override void InitChannelList()
		{
			DOChannels.Add("LampGreen", new DOChannelDef("LampGreen","010100A0.0"));
			DOChannels.Add("LampYellow", new DOChannelDef("LampYellow","010100A0.1"));
			DOChannels.Add("ReferenzUUT", new DOChannelDef("ReferenzUUT","010100A0.2"));
			DOChannels.Add("LoadPump", new DOChannelDef("LoadPump","010100A0.3"));
			DOChannels.Add("LoadHigh", new DOChannelDef("LoadHigh","010100A0.4"));
			DOChannels.Add("LoadLow", new DOChannelDef("LoadLow","010100A0.5"));
			DOChannels.Add("LampRed", new DOChannelDef("LampRed","010100A0.6"));
			DOChannels.Add("KL87_On", new DOChannelDef("KL87_On","010100A0.7"));
			DOChannels.Add("MUX A0", new DOChannelDef("MUX A0","010100A1.0"));
			DOChannels.Add("MUX A1", new DOChannelDef("MUX A1","010100A1.1"));
			DOChannels.Add("MUX A2", new DOChannelDef("MUX A2","010100A1.2"));
			DOChannels.Add("MUX A3", new DOChannelDef("MUX A3","010100A1.3"));
			DOChannels.Add("MUX A4", new DOChannelDef("MUX A4","010100A1.4"));
			DOChannels.Add("MUX A5", new DOChannelDef("MUX A5","010100A1.5"));
			DOChannels.Add("MUX A6", new DOChannelDef("MUX A6","010100A1.6"));
			DOChannels.Add("MUX A7", new DOChannelDef("MUX A7","010100A1.7"));
			DOChannels.Add("UUT_1", new DOChannelDef("UUT_1","010000A2.0"));
			DOChannels.Add("UUT_2", new DOChannelDef("UUT_2","010000A2.1"));
			DOChannels.Add("UUT_3", new DOChannelDef("UUT_3","010000A2.2"));
			DOChannels.Add("UUT_4", new DOChannelDef("UUT_4","010000A2.3"));
			DOChannels.Add("UUT_5", new DOChannelDef("UUT_5","010000A2.4"));
			DOChannels.Add("UUT_6", new DOChannelDef("UUT_6","010000A2.5"));
			DOChannels.Add("UUT_7", new DOChannelDef("UUT_7","010000A2.6"));
			DOChannels.Add("UUT_8", new DOChannelDef("UUT_8","010000A2.7"));
			DOChannels.Add("UUT_9", new DOChannelDef("UUT_9","010100A3.0"));
			DOChannels.Add("UUT_10", new DOChannelDef("UUT_10","010100A3.1"));
			DOChannels.Add("UUT_11", new DOChannelDef("UUT_11","010100A3.2"));
			DOChannels.Add("UUT_12", new DOChannelDef("UUT_12","010100A3.3"));
			DOChannels.Add("UUT_13", new DOChannelDef("UUT_13","010100A3.4"));
			DOChannels.Add("UUT_14", new DOChannelDef("UUT_14","010100A3.5"));
			DOChannels.Add("UUT_15", new DOChannelDef("UUT_15","010100A3.6"));
			DOChannels.Add("UUT_16", new DOChannelDef("UUT_16","010100A3.7"));
			DOChannels.Add("UUT_17", new DOChannelDef("UUT_17","010100A4.0"));
			DOChannels.Add("UUT_18", new DOChannelDef("UUT_18","010100A4.1"));
			DOChannels.Add("UUT_19", new DOChannelDef("UUT_19","010100A4.2"));
			DOChannels.Add("UUT_20", new DOChannelDef("UUT_20","010100A4.3"));
			DOChannels.Add("UUT_21", new DOChannelDef("UUT_21","010100A4.4"));
			DOChannels.Add("UUT_22", new DOChannelDef("UUT_22","010100A4.5"));
			DOChannels.Add("UUT_23", new DOChannelDef("UUT_23","010100A4.6"));
			DOChannels.Add("UUT_24", new DOChannelDef("UUT_24","010100A4.7"));
			DOChannels.Add("UUT_25", new DOChannelDef("UUT_25","010100A5.0"));
			DOChannels.Add("UUT_26", new DOChannelDef("UUT_26","010100A5.1"));
			DOChannels.Add("UUT_27", new DOChannelDef("UUT_27","010100A5.2"));
			DOChannels.Add("UUT_28", new DOChannelDef("UUT_28","010100A5.3"));
			DOChannels.Add("UUT_29", new DOChannelDef("UUT_29","010100A5.4"));
			DOChannels.Add("UUT_30", new DOChannelDef("UUT_30","010100A5.5"));
			DOChannels.Add("UUT_31", new DOChannelDef("UUT_31","010100A5.6"));
			DOChannels.Add("UUT_32", new DOChannelDef("UUT_32","010100A5.7"));
			DOChannels.Add("UUT_33", new DOChannelDef("UUT_33","010100A6.0"));
			DOChannels.Add("UUT_34", new DOChannelDef("UUT_34","010100A6.1"));
			DOChannels.Add("UUT_35", new DOChannelDef("UUT_35","010100A6.2"));
			DOChannels.Add("UUT_36", new DOChannelDef("UUT_36","010100A6.3"));
			DOChannels.Add("UUT_37", new DOChannelDef("UUT_37","010100A6.4"));
			DOChannels.Add("UUT_38", new DOChannelDef("UUT_38","010100A6.5"));
			DOChannels.Add("UUT_39", new DOChannelDef("UUT_39","010100A6.6"));
			DOChannels.Add("UUT_40", new DOChannelDef("UUT_40","010100A6.7"));
			DOChannels.Add("UUT_41", new DOChannelDef("UUT_41","010100A7.0"));
			DOChannels.Add("UUT_42", new DOChannelDef("UUT_42","010100A7.1"));
			DOChannels.Add("UUT_43", new DOChannelDef("UUT_43","010100A7.2"));
			DOChannels.Add("UUT_44", new DOChannelDef("UUT_44","010100A7.3"));
			DOChannels.Add("UUT_45", new DOChannelDef("UUT_45","010100A7.4"));
			DOChannels.Add("UUT_46", new DOChannelDef("UUT_46","010100A7.5"));
			DOChannels.Add("UUT_47", new DOChannelDef("UUT_47","010100A7.6"));
			DOChannels.Add("UUT_48", new DOChannelDef("UUT_48","010100A7.7"));
			DIChannels.Add("SupplyRelais_Row1_OFF", new DIChannelDef("SupplyRelais_Row1_OFF","010100E0.0"));
			DIChannels.Add("SupplyRelais_Row2_OFF", new DIChannelDef("SupplyRelais_Row2_OFF","010100E0.1"));
			DIChannels.Add("SupplyRelais_Row3_OFF", new DIChannelDef("SupplyRelais_Row3_OFF","010100E0.2"));
			DIChannels.Add("Reserve0.3", new DIChannelDef("Reserve0.3","010100E0.3"));
			DIChannels.Add("Reserve0.4", new DIChannelDef("Reserve0.4","010100E0.4"));
			DIChannels.Add("Reserve0.5", new DIChannelDef("Reserve0.5","010100E0.5"));
			DIChannels.Add("Reserve0.6", new DIChannelDef("Reserve0.6","010100E0.6"));
			DIChannels.Add("Reserve0.7", new DIChannelDef("Reserve0.7","010100E0.7"));
		}
		public bool DO_LampGreen
		{
			get { return DOChannels["LampGreen"].Get(); }
			set { DOChannels["LampGreen"].Set(value); }
		}
		public bool DO_LampYellow
		{
			get { return DOChannels["LampYellow"].Get(); }
			set { DOChannels["LampYellow"].Set(value); }
		}
		public bool DO_ReferenzUUT
		{
			get { return DOChannels["ReferenzUUT"].Get(); }
			set { DOChannels["ReferenzUUT"].Set(value); }
		}
		public bool DO_LoadPump
		{
			get { return DOChannels["LoadPump"].Get(); }
			set { DOChannels["LoadPump"].Set(value); }
		}
		public bool DO_LoadHigh
		{
			get { return DOChannels["LoadHigh"].Get(); }
			set { DOChannels["LoadHigh"].Set(value); }
		}
		public bool DO_LoadLow
		{
			get { return DOChannels["LoadLow"].Get(); }
			set { DOChannels["LoadLow"].Set(value); }
		}
		public bool DO_LampRed
		{
			get { return DOChannels["LampRed"].Get(); }
			set { DOChannels["LampRed"].Set(value); }
		}
		public bool DO_KL87_On
		{
			get { return DOChannels["KL87_On"].Get(); }
			set { DOChannels["KL87_On"].Set(value); }
		}
		public bool DO_MUX A0
		{
			get { return DOChannels["MUX A0"].Get(); }
			set { DOChannels["MUX A0"].Set(value); }
		}
		public bool DO_MUX A1
		{
			get { return DOChannels["MUX A1"].Get(); }
			set { DOChannels["MUX A1"].Set(value); }
		}
		public bool DO_MUX A2
		{
			get { return DOChannels["MUX A2"].Get(); }
			set { DOChannels["MUX A2"].Set(value); }
		}
		public bool DO_MUX A3
		{
			get { return DOChannels["MUX A3"].Get(); }
			set { DOChannels["MUX A3"].Set(value); }
		}
		public bool DO_MUX A4
		{
			get { return DOChannels["MUX A4"].Get(); }
			set { DOChannels["MUX A4"].Set(value); }
		}
		public bool DO_MUX A5
		{
			get { return DOChannels["MUX A5"].Get(); }
			set { DOChannels["MUX A5"].Set(value); }
		}
		public bool DO_MUX A6
		{
			get { return DOChannels["MUX A6"].Get(); }
			set { DOChannels["MUX A6"].Set(value); }
		}
		public bool DO_MUX A7
		{
			get { return DOChannels["MUX A7"].Get(); }
			set { DOChannels["MUX A7"].Set(value); }
		}
		public bool DO_UUT_1
		{
			get { return DOChannels["UUT_1"].Get(); }
			set { DOChannels["UUT_1"].Set(value); }
		}
		public bool DO_UUT_2
		{
			get { return DOChannels["UUT_2"].Get(); }
			set { DOChannels["UUT_2"].Set(value); }
		}
		public bool DO_UUT_3
		{
			get { return DOChannels["UUT_3"].Get(); }
			set { DOChannels["UUT_3"].Set(value); }
		}
		public bool DO_UUT_4
		{
			get { return DOChannels["UUT_4"].Get(); }
			set { DOChannels["UUT_4"].Set(value); }
		}
		public bool DO_UUT_5
		{
			get { return DOChannels["UUT_5"].Get(); }
			set { DOChannels["UUT_5"].Set(value); }
		}
		public bool DO_UUT_6
		{
			get { return DOChannels["UUT_6"].Get(); }
			set { DOChannels["UUT_6"].Set(value); }
		}
		public bool DO_UUT_7
		{
			get { return DOChannels["UUT_7"].Get(); }
			set { DOChannels["UUT_7"].Set(value); }
		}
		public bool DO_UUT_8
		{
			get { return DOChannels["UUT_8"].Get(); }
			set { DOChannels["UUT_8"].Set(value); }
		}
		public bool DO_UUT_9
		{
			get { return DOChannels["UUT_9"].Get(); }
			set { DOChannels["UUT_9"].Set(value); }
		}
		public bool DO_UUT_10
		{
			get { return DOChannels["UUT_10"].Get(); }
			set { DOChannels["UUT_10"].Set(value); }
		}
		public bool DO_UUT_11
		{
			get { return DOChannels["UUT_11"].Get(); }
			set { DOChannels["UUT_11"].Set(value); }
		}
		public bool DO_UUT_12
		{
			get { return DOChannels["UUT_12"].Get(); }
			set { DOChannels["UUT_12"].Set(value); }
		}
		public bool DO_UUT_13
		{
			get { return DOChannels["UUT_13"].Get(); }
			set { DOChannels["UUT_13"].Set(value); }
		}
		public bool DO_UUT_14
		{
			get { return DOChannels["UUT_14"].Get(); }
			set { DOChannels["UUT_14"].Set(value); }
		}
		public bool DO_UUT_15
		{
			get { return DOChannels["UUT_15"].Get(); }
			set { DOChannels["UUT_15"].Set(value); }
		}
		public bool DO_UUT_16
		{
			get { return DOChannels["UUT_16"].Get(); }
			set { DOChannels["UUT_16"].Set(value); }
		}
		public bool DO_UUT_17
		{
			get { return DOChannels["UUT_17"].Get(); }
			set { DOChannels["UUT_17"].Set(value); }
		}
		public bool DO_UUT_18
		{
			get { return DOChannels["UUT_18"].Get(); }
			set { DOChannels["UUT_18"].Set(value); }
		}
		public bool DO_UUT_19
		{
			get { return DOChannels["UUT_19"].Get(); }
			set { DOChannels["UUT_19"].Set(value); }
		}
		public bool DO_UUT_20
		{
			get { return DOChannels["UUT_20"].Get(); }
			set { DOChannels["UUT_20"].Set(value); }
		}
		public bool DO_UUT_21
		{
			get { return DOChannels["UUT_21"].Get(); }
			set { DOChannels["UUT_21"].Set(value); }
		}
		public bool DO_UUT_22
		{
			get { return DOChannels["UUT_22"].Get(); }
			set { DOChannels["UUT_22"].Set(value); }
		}
		public bool DO_UUT_23
		{
			get { return DOChannels["UUT_23"].Get(); }
			set { DOChannels["UUT_23"].Set(value); }
		}
		public bool DO_UUT_24
		{
			get { return DOChannels["UUT_24"].Get(); }
			set { DOChannels["UUT_24"].Set(value); }
		}
		public bool DO_UUT_25
		{
			get { return DOChannels["UUT_25"].Get(); }
			set { DOChannels["UUT_25"].Set(value); }
		}
		public bool DO_UUT_26
		{
			get { return DOChannels["UUT_26"].Get(); }
			set { DOChannels["UUT_26"].Set(value); }
		}
		public bool DO_UUT_27
		{
			get { return DOChannels["UUT_27"].Get(); }
			set { DOChannels["UUT_27"].Set(value); }
		}
		public bool DO_UUT_28
		{
			get { return DOChannels["UUT_28"].Get(); }
			set { DOChannels["UUT_28"].Set(value); }
		}
		public bool DO_UUT_29
		{
			get { return DOChannels["UUT_29"].Get(); }
			set { DOChannels["UUT_29"].Set(value); }
		}
		public bool DO_UUT_30
		{
			get { return DOChannels["UUT_30"].Get(); }
			set { DOChannels["UUT_30"].Set(value); }
		}
		public bool DO_UUT_31
		{
			get { return DOChannels["UUT_31"].Get(); }
			set { DOChannels["UUT_31"].Set(value); }
		}
		public bool DO_UUT_32
		{
			get { return DOChannels["UUT_32"].Get(); }
			set { DOChannels["UUT_32"].Set(value); }
		}
		public bool DO_UUT_33
		{
			get { return DOChannels["UUT_33"].Get(); }
			set { DOChannels["UUT_33"].Set(value); }
		}
		public bool DO_UUT_34
		{
			get { return DOChannels["UUT_34"].Get(); }
			set { DOChannels["UUT_34"].Set(value); }
		}
		public bool DO_UUT_35
		{
			get { return DOChannels["UUT_35"].Get(); }
			set { DOChannels["UUT_35"].Set(value); }
		}
		public bool DO_UUT_36
		{
			get { return DOChannels["UUT_36"].Get(); }
			set { DOChannels["UUT_36"].Set(value); }
		}
		public bool DO_UUT_37
		{
			get { return DOChannels["UUT_37"].Get(); }
			set { DOChannels["UUT_37"].Set(value); }
		}
		public bool DO_UUT_38
		{
			get { return DOChannels["UUT_38"].Get(); }
			set { DOChannels["UUT_38"].Set(value); }
		}
		public bool DO_UUT_39
		{
			get { return DOChannels["UUT_39"].Get(); }
			set { DOChannels["UUT_39"].Set(value); }
		}
		public bool DO_UUT_40
		{
			get { return DOChannels["UUT_40"].Get(); }
			set { DOChannels["UUT_40"].Set(value); }
		}
		public bool DO_UUT_41
		{
			get { return DOChannels["UUT_41"].Get(); }
			set { DOChannels["UUT_41"].Set(value); }
		}
		public bool DO_UUT_42
		{
			get { return DOChannels["UUT_42"].Get(); }
			set { DOChannels["UUT_42"].Set(value); }
		}
		public bool DO_UUT_43
		{
			get { return DOChannels["UUT_43"].Get(); }
			set { DOChannels["UUT_43"].Set(value); }
		}
		public bool DO_UUT_44
		{
			get { return DOChannels["UUT_44"].Get(); }
			set { DOChannels["UUT_44"].Set(value); }
		}
		public bool DO_UUT_45
		{
			get { return DOChannels["UUT_45"].Get(); }
			set { DOChannels["UUT_45"].Set(value); }
		}
		public bool DO_UUT_46
		{
			get { return DOChannels["UUT_46"].Get(); }
			set { DOChannels["UUT_46"].Set(value); }
		}
		public bool DO_UUT_47
		{
			get { return DOChannels["UUT_47"].Get(); }
			set { DOChannels["UUT_47"].Set(value); }
		}
		public bool DO_UUT_48
		{
			get { return DOChannels["UUT_48"].Get(); }
			set { DOChannels["UUT_48"].Set(value); }
		}
		
		public bool DI_SupplyRelais_Row1_OFF
		{
			get { return DIChannels["SupplyRelais_Row1_OFF"].Get(); }
		}
		public bool DI_SupplyRelais_Row2_OFF
		{
			get { return DIChannels["SupplyRelais_Row2_OFF"].Get(); }
		}
		public bool DI_SupplyRelais_Row3_OFF
		{
			get { return DIChannels["SupplyRelais_Row3_OFF"].Get(); }
		}
		public bool DI_Reserve0.3
		{
			get { return DIChannels["Reserve0.3"].Get(); }
		}
		public bool DI_Reserve0.4
		{
			get { return DIChannels["Reserve0.4"].Get(); }
		}
		public bool DI_Reserve0.5
		{
			get { return DIChannels["Reserve0.5"].Get(); }
		}
		public bool DI_Reserve0.6
		{
			get { return DIChannels["Reserve0.6"].Get(); }
		}
		public bool DI_Reserve0.7
		{
			get { return DIChannels["Reserve0.7"].Get(); }
		}
	}
}
