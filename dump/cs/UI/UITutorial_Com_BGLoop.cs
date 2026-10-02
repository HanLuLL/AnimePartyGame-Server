using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_BGLoop : GComponent
{
	public GLoader loader_Bg;

	public Transition loop;

	public Transition Cut_in;

	public Transition loop2;

	public const string URL = "ui://b96qpoz6j7u8q3x";

	public static UITutorial_Com_BGLoop CreateInstance()
	{
		return (UITutorial_Com_BGLoop)UIPackage.CreateObject("Tutorial", "Tutorial_Com_BGLoop");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Bg = (GLoader)GetChildAt(1);
		loop = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
		loop2 = GetTransitionAt(2);
	}
}
