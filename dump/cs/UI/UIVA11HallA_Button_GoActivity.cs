using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Button_GoActivity : GButton
{
	public Transition Cut_in;

	public const string URL = "ui://zlsk81wwgu0j2r";

	public static UIVA11HallA_Button_GoActivity CreateInstance()
	{
		return (UIVA11HallA_Button_GoActivity)UIPackage.CreateObject("VA11HallA", "VA11HallA_Button_GoActivity");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
