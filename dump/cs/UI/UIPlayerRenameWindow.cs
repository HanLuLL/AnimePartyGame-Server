using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPlayerRenameWindow : GComponent
{
	public GTextField txt_title;

	public GTextField txt_tips;

	public GButton btn_confirm;

	public GTextInput txt_input;

	public GButton btn_close;

	public Transition Cut_in;

	public const string URL = "ui://0eekkm64t7kd0";

	public static UIPlayerRenameWindow CreateInstance()
	{
		BindAll();
		return (UIPlayerRenameWindow)UIPackage.CreateObject("PlayerRename", "PlayerRenameWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://0eekkm64su3p7", typeof(UIPlayerRename_Com_BGLoop));
		UIObjectFactory.SetPackageItemExtension("ui://0eekkm64t7kd0", typeof(UIPlayerRenameWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_title = (GTextField)GetChildAt(2);
		txt_tips = (GTextField)GetChildAt(3);
		btn_confirm = (GButton)GetChildAt(4);
		txt_input = (GTextInput)GetChildAt(6);
		btn_close = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
