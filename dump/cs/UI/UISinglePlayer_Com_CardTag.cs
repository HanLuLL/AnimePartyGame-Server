using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_CardTag : GComponent
{
	public Controller type;

	public const string URL = "ui://mi9vm3w0kducq5g";

	public static UISinglePlayer_Com_CardTag CreateInstance()
	{
		return (UISinglePlayer_Com_CardTag)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardTag");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
	}
}
