using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_CharacterName : GComponent
{
	public GTextField txt_Title;

	public const string URL = "ui://b96qpoz6ia9gc";

	public static UITutorial_Com_CharacterName CreateInstance()
	{
		return (UITutorial_Com_CharacterName)UIPackage.CreateObject("Tutorial", "Tutorial_Com_CharacterName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(0);
	}
}
