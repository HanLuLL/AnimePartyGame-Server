using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_CardsProb : GComponent
{
	public GTextField txt_Green;

	public GTextField txt_Bule;

	public GTextField txt_Purple;

	public GTextField txt_Gold;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0tvj1q7w";

	public static UISinglePlayer_Com_CardsProb CreateInstance()
	{
		return (UISinglePlayer_Com_CardsProb)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardsProb");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Green = (GTextField)GetChildAt(2);
		txt_Bule = (GTextField)GetChildAt(4);
		txt_Purple = (GTextField)GetChildAt(6);
		txt_Gold = (GTextField)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
