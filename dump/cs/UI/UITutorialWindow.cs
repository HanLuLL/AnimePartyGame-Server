using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorialWindow : GComponent
{
	public Controller tab;

	public UITutorial_Com_Mash com_Mask;

	public GGraph btn_Mask;

	public UITutorial_Com_BG com_BG;

	public UITutorial_Com_Tutorial com_Tutorial;

	public const string URL = "ui://b96qpoz68vxw0";

	public static UITutorialWindow CreateInstance()
	{
		BindAll();
		return (UITutorialWindow)UIPackage.CreateObject("Tutorial", "TutorialWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz68vxw0", typeof(UITutorialWindow));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz68vxw1", typeof(UITutorial_Com_Arrow));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz68vxw2", typeof(UITutorial_Com_Mash));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz68vxw3", typeof(UITutorial_Com_Dialog));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz68vxw8", typeof(UITutorial_Com_Tutorial));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6ia9g9", typeof(UITutorial_Com_SelectHero));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6ia9ga", typeof(UITutorial_Com_SelectHeroItem));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6ia9gc", typeof(UITutorial_Com_CharacterName));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6ia9ge", typeof(UITutorial_CharacterDi));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6ia9gg", typeof(UITutorial_Button_Sure));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6iu43o", typeof(UITutorial_Com_HeroStory));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6iu43p", typeof(UITutorial_Com_FinishTip));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6iu43q", typeof(UITutorial_Com_DialogTip));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6iu43q3v", typeof(UITutorial_Com_BG));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6j7u8q3x", typeof(UITutorial_Com_BGLoop));
		UIObjectFactory.SetPackageItemExtension("ui://b96qpoz6mk9qq48", typeof(UITutorial_Com_SystemInfo));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		com_Mask = (UITutorial_Com_Mash)GetChildAt(0);
		btn_Mask = (GGraph)GetChildAt(1);
		com_BG = (UITutorial_Com_BG)GetChildAt(2);
		com_Tutorial = (UITutorial_Com_Tutorial)GetChildAt(3);
	}
}
