using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Com_SkinItem : GComponent
{
	public GGraph graph_Skin;

	public GTextField txt_Get;

	public GTextField txt_Title;

	public const string URL = "ui://k49wk9ftnqmf2b";

	public static UIAnniversary_2nd_Com_SkinItem CreateInstance()
	{
		return (UIAnniversary_2nd_Com_SkinItem)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Skin = (GGraph)GetChildAt(0);
		txt_Get = (GTextField)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
