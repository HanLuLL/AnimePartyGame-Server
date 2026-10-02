using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_playerInfo_Head : GComponent
{
	public Controller Camp;

	public GLoader loader_Icon;

	public const string URL = "ui://1ov1i0v9cz37bp";

	public static UICom_playerInfo_Head CreateInstance()
	{
		return (UICom_playerInfo_Head)UIPackage.CreateObject("Common_Internal", "Com_playerInfo_Head");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Camp = GetControllerAt(0);
		loader_Icon = (GLoader)GetChildAt(0);
	}
}
