using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_AchieveLoader : GComponent
{
	public GLoader loader_Achieve;

	public const string URL = "ui://iepldke7f3tl19";

	public static UIAccountInfo_Com_AchieveLoader CreateInstance()
	{
		return (UIAccountInfo_Com_AchieveLoader)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_AchieveLoader");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Achieve = (GLoader)GetChildAt(0);
	}
}
