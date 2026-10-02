using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_PassiveSkillTrigger : GComponent
{
	public GTextField txt_PassiveSkillName;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mine4s7r";

	public static UITips_Com_PassiveSkillTrigger CreateInstance()
	{
		return (UITips_Com_PassiveSkillTrigger)UIPackage.CreateObject("Tips", "Tips_Com_PassiveSkillTrigger");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_PassiveSkillName = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
