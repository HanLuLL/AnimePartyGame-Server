using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIComboBox1_popup : GComponent
{
	public GList list;

	public const string URL = "ui://725vhs9ylmo05";

	public static UIComboBox1_popup CreateInstance()
	{
		return (UIComboBox1_popup)UIPackage.CreateObject("GM", "ComboBox1_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
