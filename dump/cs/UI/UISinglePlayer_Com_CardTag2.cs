using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_CardTag2 : GComponent
{
	public Controller type;

	public const string URL = "ui://mi9vm3w0tvj1q7z";

	public static UISinglePlayer_Com_CardTag2 CreateInstance()
	{
		return (UISinglePlayer_Com_CardTag2)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardTag2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
	}
}
