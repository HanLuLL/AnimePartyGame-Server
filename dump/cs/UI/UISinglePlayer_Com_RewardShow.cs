using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_RewardShow : GComponent
{
	public GButton btn_Confirm;

	public UISinglePlayer_Com_CardItemSlot com_card;

	public GTextField txt_Title;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0wcumq6u";

	public static UISinglePlayer_Com_RewardShow CreateInstance()
	{
		return (UISinglePlayer_Com_RewardShow)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_RewardShow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Confirm = (GButton)GetChildAt(2);
		com_card = (UISinglePlayer_Com_CardItemSlot)GetChildAt(4);
		txt_Title = (GTextField)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
