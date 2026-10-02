using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_CardName : GComponent
{
	public Controller type;

	public GTextField txt_Name;

	public const string URL = "ui://mi9vm3w0kducq5m";

	public static UISinglePlayer_Com_CardName CreateInstance()
	{
		return (UISinglePlayer_Com_CardName)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		txt_Name = (GTextField)GetChildAt(1);
	}
}
