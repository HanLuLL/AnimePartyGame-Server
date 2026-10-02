using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoading_MagicSchool : GComponent
{
	public Transition Cut_out;

	public Transition Cut_in;

	public Transition Loop;

	public const string URL = "ui://bzunkg3bfow93";

	public static UILoading_MagicSchool CreateInstance()
	{
		return (UILoading_MagicSchool)UIPackage.CreateObject("Loading", "Loading_MagicSchool");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_out = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
		Loop = GetTransitionAt(2);
	}
}
