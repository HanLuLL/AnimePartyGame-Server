using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_RelicInfo : GComponent
{
	public GTextField txt_Name;

	public GRichTextField txt_Desc;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0tvj1q80";

	public static UISinglePlayer_Com_RelicInfo CreateInstance()
	{
		return (UISinglePlayer_Com_RelicInfo)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_RelicInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(1);
		txt_Desc = (GRichTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
