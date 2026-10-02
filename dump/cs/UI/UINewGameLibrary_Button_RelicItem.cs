using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Button_RelicItem : GButton
{
	public Controller status;

	public GComponent loader_Frame;

	public GLoader loader_Icon;

	public GTextField txt_tltle;

	public GRichTextField txt_Desc;

	public GComponent com_Keyword;

	public Transition cutIn;

	public const string URL = "ui://mc0y3plupj0z4";

	public static UINewGameLibrary_Button_RelicItem CreateInstance()
	{
		return (UINewGameLibrary_Button_RelicItem)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Button_RelicItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		loader_Frame = (GComponent)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		txt_tltle = (GTextField)GetChildAt(3);
		txt_Desc = (GRichTextField)GetChildAt(4);
		com_Keyword = (GComponent)GetChildAt(6);
		cutIn = GetTransitionAt(0);
	}
}
