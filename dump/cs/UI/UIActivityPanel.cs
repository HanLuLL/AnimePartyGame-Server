using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPanel : GComponent
{
	public Controller Status;

	public UIActivity_Com_Type1 com_Type1;

	public UIActivity_Com_Type2 com_Type2;

	public UIActivity_Com_Type5 com_Type5;

	public GButton btn_Return;

	public GButton btn_Return_1;

	public Transition Cutin;

	public const string URL = "ui://vckl96ksrsc70";

	public static UIActivityPanel CreateInstance()
	{
		BindAll();
		return (UIActivityPanel)UIPackage.CreateObject("Activity", "ActivityPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksbakuq", typeof(UIActivity_Com_Type1));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksg402em", typeof(UIActivity_Com_Type5_ItemName));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksize7sq4v", typeof(UIActivity_Com_Type5_XRPreRevard));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1d", typeof(UIActivity_Com_Type2));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1g", typeof(UIActivity_Com_Type2_Main));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1h", typeof(UIActivity_Com_Type2_Scratchoff));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1i", typeof(UIActivity_Button_Type2_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1l", typeof(UIActivity_Com_Type2_Label));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1n", typeof(UIActivity_Button_Type2_Scratchoff_ListItem));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1o", typeof(UIActivity_Button_Type2_Scratchoff));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksjzxa1p", typeof(UIActivity_Button_Type2_Scratchoff_NextPage));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96kslw7ydi", typeof(UIActivity_Com_Type5));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96kso812dt", typeof(UIActivity_Com_Type5_PreRevard));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96kso812e8", typeof(UIActivity_Com_Type5_Button_ShowPlatform));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksrsc70", typeof(UIActivityPanel));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksrsc71", typeof(UIActivity_Button_Type1_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksrsc77", typeof(UIActivity_Com_Type1_Label));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksrsc7a", typeof(UIActivity_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksru20ib", typeof(UIActivity_Button_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq76", typeof(UIActivity_Com_Type5_Scratchoff_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq7n", typeof(UIActivity_Com_Type5_Button_Purchase_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq7p", typeof(UIActivity_Com_Type5_Button_Light_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq7q", typeof(UIActivity_Button_Type5_Scratchoff_ListItem_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq7v", typeof(UIActivity_Com_Type5_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksxcsllsq80", typeof(UIActivity_Com_Type5_ShowSkin_Lin));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq83", typeof(UIActivity_Com_Type5_TaiDao));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq88", typeof(UIActivity_Com_Type5_Scratchoff_TaiDao));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq8h", typeof(UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq8n", typeof(UIActivity_Com_Type5_Button_Purchase_TaiDao));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq8p", typeof(UIActivity_Com_Type5_Button_Light_TaiDao));
		UIObjectFactory.SetPackageItemExtension("ui://vckl96ksyjnjlsq8u", typeof(UIActivity_Com_Type5_ShowSkin_TaiDao));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		com_Type1 = (UIActivity_Com_Type1)GetChildAt(0);
		com_Type2 = (UIActivity_Com_Type2)GetChildAt(1);
		com_Type5 = (UIActivity_Com_Type5)GetChildAt(2);
		btn_Return = (GButton)GetChildAt(4);
		btn_Return_1 = (GButton)GetChildAt(5);
		Cutin = GetTransitionAt(0);
	}
}
