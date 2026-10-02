using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_FinishTip : GComponent
{
	public GTextField txt_0;

	public GTextField txt_1;

	public GTextField txt_2;

	public Transition Cut_in;

	public const string URL = "ui://b96qpoz6iu43p";

	public void RefreshTips()
	{
		Cut_in.Play();
		txt_0.text = 20009101.GetLocal(UIStringType.Tutorial);
		txt_1.text = 20009102.GetLocal(UIStringType.Tutorial);
		txt_2.text = 20009103.GetLocal(UIStringType.Tutorial);
	}

	public static UITutorial_Com_FinishTip CreateInstance()
	{
		return (UITutorial_Com_FinishTip)UIPackage.CreateObject("Tutorial", "Tutorial_Com_FinishTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_0 = (GTextField)GetChildAt(3);
		txt_1 = (GTextField)GetChildAt(4);
		txt_2 = (GTextField)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
