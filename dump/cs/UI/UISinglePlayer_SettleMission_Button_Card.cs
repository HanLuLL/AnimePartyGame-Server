using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_SettleMission_Button_Card : GButton
{
	public Controller type;

	public UISinglePlayer_SettleMission_Item com_Item;

	public GList list_Tags;

	public GRichTextField txt_Desc;

	public UISinglePlayer_Com_CardName com_Name;

	public GButton btn_Confirm;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0fx3oq72";

	public static UISinglePlayer_SettleMission_Button_Card CreateInstance()
	{
		return (UISinglePlayer_SettleMission_Button_Card)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_SettleMission_Button_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		com_Item = (UISinglePlayer_SettleMission_Item)GetChildAt(1);
		list_Tags = (GList)GetChildAt(2);
		txt_Desc = (GRichTextField)GetChildAt(3);
		com_Name = (UISinglePlayer_Com_CardName)GetChildAt(4);
		btn_Confirm = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
