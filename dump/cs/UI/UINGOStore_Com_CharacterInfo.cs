using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStore_Com_CharacterInfo : GComponent
{
	public GTextField txt_Nick;

	public GTextField txt_Name;

	public const string URL = "ui://na6sy4s6kqgjm";

	public static UINGOStore_Com_CharacterInfo CreateInstance()
	{
		return (UINGOStore_Com_CharacterInfo)UIPackage.CreateObject("NGOStore", "NGOStore_Com_CharacterInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Nick = (GTextField)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(1);
	}
}
