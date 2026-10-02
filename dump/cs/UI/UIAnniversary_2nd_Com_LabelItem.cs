using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Com_LabelItem : GComponent
{
	public GComponent com_Label;

	public GGraph graph_LabelGray;

	public GTextField txt_GetLabel;

	public GTextField txt_Title;

	public const string URL = "ui://k49wk9ftc76f51";

	public static UIAnniversary_2nd_Com_LabelItem CreateInstance()
	{
		return (UIAnniversary_2nd_Com_LabelItem)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com_LabelItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Label = (GComponent)GetChildAt(0);
		graph_LabelGray = (GGraph)GetChildAt(1);
		txt_GetLabel = (GTextField)GetChildAt(2);
		txt_Title = (GTextField)GetChildAt(4);
	}
}
