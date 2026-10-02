using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIVA11HallA_Button_PreviewSkin : GButton
{
	public Controller type;

	public GTextField txt_Title;

	public const string URL = "ui://zlsk81wwgu0j2t";

	public static UIVA11HallA_Button_PreviewSkin CreateInstance()
	{
		return (UIVA11HallA_Button_PreviewSkin)UIPackage.CreateObject("VA11HallA", "VA11HallA_Button_PreviewSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
