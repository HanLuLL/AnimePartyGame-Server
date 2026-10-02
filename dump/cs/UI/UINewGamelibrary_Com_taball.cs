using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGamelibrary_Com_taball : GComponent
{
	public GList list_Items;

	public GTextField text_title;

	public GLabel com_Desc;

	public UINewGameLibrary_Com_Banner com_Banner;

	public GButton loader_Detail;

	public const string URL = "ui://mc0y3plupj0z2b";

	public static UINewGamelibrary_Com_taball CreateInstance()
	{
		return (UINewGamelibrary_Com_taball)UIPackage.CreateObject("NewGameLibrary", "NewGamelibrary_Com_taball");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Items = (GList)GetChildAt(5);
		text_title = (GTextField)GetChildAt(6);
		com_Desc = (GLabel)GetChildAt(7);
		com_Banner = (UINewGameLibrary_Com_Banner)GetChildAt(8);
		loader_Detail = (GButton)GetChildAt(9);
	}
}
