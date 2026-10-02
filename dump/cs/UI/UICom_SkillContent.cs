using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_SkillContent : GComponent
{
	public Controller skillType;

	public GTextField txt_SkillName;

	public GRichTextField txt_SkillDesc;

	public GTextField txt_CD;

	public const string URL = "ui://xuaw6o8jpx78j9l";

	public static UICom_SkillContent CreateInstance()
	{
		return (UICom_SkillContent)UIPackage.CreateObject("Common", "Com_SkillContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		skillType = GetControllerAt(0);
		txt_SkillName = (GTextField)GetChildAt(1);
		txt_SkillDesc = (GRichTextField)GetChildAt(2);
		txt_CD = (GTextField)GetChildAt(4);
	}
}
