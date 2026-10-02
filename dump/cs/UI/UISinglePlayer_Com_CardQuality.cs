using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_CardQuality : GComponent
{
	public Controller quality;

	public const string URL = "ui://mi9vm3w0s244q3t";

	public static UISinglePlayer_Com_CardQuality CreateInstance()
	{
		return (UISinglePlayer_Com_CardQuality)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardQuality");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		quality = GetControllerAt(0);
	}
}
