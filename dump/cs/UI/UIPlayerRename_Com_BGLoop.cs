using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPlayerRename_Com_BGLoop : GComponent
{
	public GLoader loader_Bg;

	public Transition loop;

	public Transition Cut_in;

	public Transition loop2;

	public const string URL = "ui://0eekkm64su3p7";

	public static UIPlayerRename_Com_BGLoop CreateInstance()
	{
		return (UIPlayerRename_Com_BGLoop)UIPackage.CreateObject("PlayerRename", "PlayerRename_Com_BGLoop");
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
