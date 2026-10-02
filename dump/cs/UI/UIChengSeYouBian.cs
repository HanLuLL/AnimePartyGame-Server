using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChengSeYouBian : GComponent
{
	public GLoader loader_Right;

	public const string URL = "ui://50xzye56r1gk1p";

	public static UIChengSeYouBian CreateInstance()
	{
		return (UIChengSeYouBian)UIPackage.CreateObject("AssistVoteS7", "橙色右边");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Right = (GLoader)GetChildAt(0);
	}
}
