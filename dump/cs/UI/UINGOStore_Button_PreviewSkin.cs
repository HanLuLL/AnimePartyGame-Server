using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStore_Button_PreviewSkin : GButton
{
	public Controller Skin;

	public const string URL = "ui://na6sy4s6kqgjk";

	public static UINGOStore_Button_PreviewSkin CreateInstance()
	{
		return (UINGOStore_Button_PreviewSkin)UIPackage.CreateObject("NGOStore", "NGOStore_Button_PreviewSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Skin = GetControllerAt(1);
	}
}
