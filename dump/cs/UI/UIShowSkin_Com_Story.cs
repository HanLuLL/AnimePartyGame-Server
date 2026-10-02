using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkin_Com_Story : GComponent
{
	public GTextField txt_Story;

	public const string URL = "ui://zfulrgf7ofajqqe";

	public static UIShowSkin_Com_Story CreateInstance()
	{
		return (UIShowSkin_Com_Story)UIPackage.CreateObject("ShowSkin", "ShowSkin_Com_Story");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Story = (GTextField)GetChildAt(2);
	}
}
