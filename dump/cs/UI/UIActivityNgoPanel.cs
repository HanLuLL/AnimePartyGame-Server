using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityNgoPanel : GComponent
{
	public UIActivity_Com_Type4 com_Type4;

	public GButton btn_Return_NGO;

	public const string URL = "ui://c1v285vtbf6p1d";

	public static UIActivityNgoPanel CreateInstance()
	{
		BindAll();
		return (UIActivityNgoPanel)UIPackage.CreateObject("ActivityNgo", "ActivityNgoPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtbf6p1d", typeof(UIActivityNgoPanel));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2i0", typeof(UIActivity_Com_Type4));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2i1", typeof(UIActivity_Com_Type4_Main));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2i11", typeof(UIActivity_Com_Type4_Scratchoff));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2i17", typeof(UIActivity_Button_Type4_ScratchoffPreReward));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2i19", typeof(UIActivity_Button_Type4_Scratchoff_Item));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2ik", typeof(UIActivity_Com_Type4_TaskLabel));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2im", typeof(UIActivity_Button_Type4_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2iu", typeof(UIActivity_Button_Type4_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2iw", typeof(UIActivity_Button_Type4_MainReward));
		UIObjectFactory.SetPackageItemExtension("ui://c1v285vtpu2ix", typeof(UIActivity_Com_Type4_OpenScratchOff));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Type4 = (UIActivity_Com_Type4)GetChildAt(0);
		btn_Return_NGO = (GButton)GetChildAt(1);
	}
}
