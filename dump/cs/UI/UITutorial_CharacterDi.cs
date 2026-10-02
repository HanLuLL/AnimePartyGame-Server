using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_CharacterDi : GComponent
{
	public Controller p;

	public Controller battlePass;

	public GMovieClip aMovie_Selected;

	public Transition aMoive;

	public const string URL = "ui://b96qpoz6ia9ge";

	public static UITutorial_CharacterDi CreateInstance()
	{
		return (UITutorial_CharacterDi)UIPackage.CreateObject("Tutorial", "Tutorial_CharacterDi");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		battlePass = GetControllerAt(1);
		aMovie_Selected = (GMovieClip)GetChildAt(0);
		aMoive = GetTransitionAt(0);
	}
}
