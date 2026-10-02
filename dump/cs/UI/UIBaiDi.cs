using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBaiDi : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00s9kvqq5p";

	public static UIBaiDi CreateInstance()
	{
		return (UIBaiDi)UIPackage.CreateObject("Store", "白底");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
