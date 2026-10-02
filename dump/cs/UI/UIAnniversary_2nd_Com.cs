using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Com : GComponent
{
	public Controller language;

	public GLoader loader_Skin1;

	public GLoader loader_Skin2;

	public GButton btn_Close;

	public GButton btn_buy;

	public GButton btn_ShowSkin1;

	public GButton btn_ShowSkin2;

	public Transition Cut_in;

	public const string URL = "ui://k49wk9ftnqmf1e";

	public static UIAnniversary_2nd_Com CreateInstance()
	{
		return (UIAnniversary_2nd_Com)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_Skin1 = (GLoader)GetChildAt(0);
		loader_Skin2 = (GLoader)GetChildAt(1);
		btn_Close = (GButton)GetChildAt(2);
		btn_buy = (GButton)GetChildAt(3);
		btn_ShowSkin1 = (GButton)GetChildAt(16);
		btn_ShowSkin2 = (GButton)GetChildAt(24);
		Cut_in = GetTransitionAt(0);
	}
}
