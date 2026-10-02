using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkinWindow : GComponent
{
	public Controller infoType;

	public Controller Type;

	public GGraph mohu;

	public UIShowSkin_Com_Skin com_Skin;

	public GButton btn_Return;

	public UIShowSkin_Com_Preivew com_Preview;

	public UIShowSkin_Com_Story com_Story;

	public UIShowSkin_Button_SelectInfo btn_Select;

	public UIShowSkin_Button_Purchase btn_Purchase;

	public Transition Cut_in;

	public const string URL = "ui://zfulrgf7qdq50";

	public static UIShowSkinWindow CreateInstance()
	{
		BindAll();
		return (UIShowSkinWindow)UIPackage.CreateObject("ShowSkin", "ShowSkinWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7cabpqqc", typeof(UIShowSkin_Com_Preivew));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7gbr1qqb", typeof(UIShowSkin_Com_Skin));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7ofajqqd", typeof(UIShowSkin_Button_SelectInfo));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7ofajqqe", typeof(UIShowSkin_Com_Story));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7qdq50", typeof(UIShowSkinWindow));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7qdq52", typeof(UIShowSkin_Button_Video));
		UIObjectFactory.SetPackageItemExtension("ui://zfulrgf7qdq53", typeof(UIShowSkin_Button_Purchase));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		infoType = GetControllerAt(0);
		Type = GetControllerAt(1);
		mohu = (GGraph)GetChildAt(0);
		com_Skin = (UIShowSkin_Com_Skin)GetChildAt(2);
		btn_Return = (GButton)GetChildAt(3);
		com_Preview = (UIShowSkin_Com_Preivew)GetChildAt(4);
		com_Story = (UIShowSkin_Com_Story)GetChildAt(5);
		btn_Select = (UIShowSkin_Button_SelectInfo)GetChildAt(6);
		btn_Purchase = (UIShowSkin_Button_Purchase)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}
