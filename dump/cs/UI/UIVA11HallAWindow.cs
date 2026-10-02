using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallAWindow : GComponent
{
	public Controller page;

	public GGraph graph_Video;

	public UIVA11HallA_Com_Goods com_Goods;

	public UIVA11HallA_Com_Advert com_Advert;

	public GButton btn_Close;

	public const string URL = "ui://zlsk81wwgu0j2a";

	public static UIVA11HallAWindow CreateInstance()
	{
		BindAll();
		return (UIVA11HallAWindow)UIPackage.CreateObject("VA11HallA", "VA11HallAWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2a", typeof(UIVA11HallAWindow));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2d", typeof(UIVA11HallA_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2e", typeof(UIVA11HallA_Button_Purchase));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2h", typeof(UIVA11HallA_Com_Advert));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2r", typeof(UIVA11HallA_Button_GoActivity));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2t", typeof(UIVA11HallA_Button_PreviewSkin));
		UIObjectFactory.SetPackageItemExtension("ui://zlsk81wwgu0j2z", typeof(UIVA11HallA_Button_GoStore));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		graph_Video = (GGraph)GetChildAt(2);
		com_Goods = (UIVA11HallA_Com_Goods)GetChildAt(3);
		com_Advert = (UIVA11HallA_Com_Advert)GetChildAt(4);
		btn_Close = (GButton)GetChildAt(5);
	}
}
