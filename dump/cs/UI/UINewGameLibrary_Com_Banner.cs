using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_Banner : GComponent
{
	public GList list_Eventcard;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://mc0y3plupj0z2e";

	public static UINewGameLibrary_Com_Banner CreateInstance()
	{
		return (UINewGameLibrary_Com_Banner)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_Banner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Eventcard = (GList)GetChildAt(0);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
