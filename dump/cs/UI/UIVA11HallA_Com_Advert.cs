using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Com_Advert : GComponent
{
	public Controller language;

	public GGraph graph_Video;

	public GLoader loader_303;

	public GLoader loader_304;

	public GGraph loader_Animation_303;

	public GTextField txt_GoActivityStore;

	public UIVA11HallA_Button_GoActivity btn_GoActivityStore;

	public UIVA11HallA_Button_PreviewSkin btn_Preview_303;

	public UIVA11HallA_Button_PreviewSkin btn_TimeTip_303;

	public GGraph loader_Animation_304;

	public GTextField txt_GoStore;

	public UIVA11HallA_Button_GoStore btn_GoStore;

	public UIVA11HallA_Button_PreviewSkin btn_Preview_304;

	public UIVA11HallA_Button_PreviewSkin btn_TimeTip_304;

	public Transition Cut_In;

	public Transition Loop;

	public const string URL = "ui://zlsk81wwgu0j2h";

	public static UIVA11HallA_Com_Advert CreateInstance()
	{
		return (UIVA11HallA_Com_Advert)UIPackage.CreateObject("VA11HallA", "VA11HallA_Com_Advert");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		graph_Video = (GGraph)GetChildAt(0);
		loader_303 = (GLoader)GetChildAt(6);
		loader_304 = (GLoader)GetChildAt(7);
		loader_Animation_303 = (GGraph)GetChildAt(13);
		txt_GoActivityStore = (GTextField)GetChildAt(14);
		btn_GoActivityStore = (UIVA11HallA_Button_GoActivity)GetChildAt(15);
		btn_Preview_303 = (UIVA11HallA_Button_PreviewSkin)GetChildAt(16);
		btn_TimeTip_303 = (UIVA11HallA_Button_PreviewSkin)GetChildAt(17);
		loader_Animation_304 = (GGraph)GetChildAt(21);
		txt_GoStore = (GTextField)GetChildAt(22);
		btn_GoStore = (UIVA11HallA_Button_GoStore)GetChildAt(23);
		btn_Preview_304 = (UIVA11HallA_Button_PreviewSkin)GetChildAt(24);
		btn_TimeTip_304 = (UIVA11HallA_Button_PreviewSkin)GetChildAt(25);
		Cut_In = GetTransitionAt(0);
		Loop = GetTransitionAt(1);
	}
}
