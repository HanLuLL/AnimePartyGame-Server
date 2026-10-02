using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Button_GoStore : GButton
{
	public Transition Cut_in;

	public const string URL = "ui://zlsk81wwgu0j2z";

	public static UIVA11HallA_Button_GoStore CreateInstance()
	{
		return (UIVA11HallA_Button_GoStore)UIPackage.CreateObject("VA11HallA", "VA11HallA_Button_GoStore");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
