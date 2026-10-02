using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_Skill : GComponent
{
	public Controller skillType;

	public GTextField txt_SkillName;

	public GRichTextField txt_SkillDesc;

	public const string URL = "ui://l82hrmsqkqgj1h";

	public static UIRoomHero_Com_Skill CreateInstance()
	{
		return (UIRoomHero_Com_Skill)UIPackage.CreateObject("RoomHero", "RoomHero_Com_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		skillType = GetControllerAt(0);
		txt_SkillName = (GTextField)GetChildAt(0);
		txt_SkillDesc = (GRichTextField)GetChildAt(1);
	}
}
