using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_LevelUp : GComponent
{
	public GButton com_playerInfo;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mine4s7t";

	public static UITips_Com_LevelUp CreateInstance()
	{
		return (UITips_Com_LevelUp)UIPackage.CreateObject("Tips", "Tips_Com_LevelUp");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_playerInfo = (GButton)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
