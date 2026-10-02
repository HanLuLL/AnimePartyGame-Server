using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_LevelLine : GComponent
{
	public GImage line;

	public const string URL = "ui://mi9vm3w0dajbq8c";

	public static UISinglePlayer_Com_LevelLine CreateInstance()
	{
		return (UISinglePlayer_Com_LevelLine)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_LevelLine");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		line = (GImage)GetChildAt(0);
	}
}
