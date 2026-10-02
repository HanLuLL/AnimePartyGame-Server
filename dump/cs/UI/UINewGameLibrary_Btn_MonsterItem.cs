using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Btn_MonsterItem : GButton
{
	public GLoader loader_Profile;

	public GTextField txt_Name;

	public const string URL = "ui://mc0y3plupj0z2l";

	public static UINewGameLibrary_Btn_MonsterItem CreateInstance()
	{
		return (UINewGameLibrary_Btn_MonsterItem)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Btn_MonsterItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Profile = (GLoader)GetChildAt(1);
		txt_Name = (GTextField)GetChildAt(3);
	}
}
