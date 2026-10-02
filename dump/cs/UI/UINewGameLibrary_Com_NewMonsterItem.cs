using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_NewMonsterItem : GButton
{
	public GLoader loader_Profile;

	public const string URL = "ui://mc0y3plupj0z2i";

	public static UINewGameLibrary_Com_NewMonsterItem CreateInstance()
	{
		return (UINewGameLibrary_Com_NewMonsterItem)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_NewMonsterItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Profile = (GLoader)GetChildAt(0);
	}
}
