using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILuckyStarMission_Com_Card : GComponent
{
	public GLoader loader_Icon;

	public GTextField txt_Title;

	public GTextField txt_Content;

	public GList list_Star;

	public GTextField txt_Progress;

	public Transition Cut_in;

	public const string URL = "ui://pwex29p4brvlq";

	public static UILuckyStarMission_Com_Card CreateInstance()
	{
		return (UILuckyStarMission_Com_Card)UIPackage.CreateObject("LuckyStarMission", "LuckyStarMission_Com_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(3);
		txt_Content = (GTextField)GetChildAt(4);
		list_Star = (GList)GetChildAt(5);
		txt_Progress = (GTextField)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
