using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PopUpWindow_Bottom : GLabel
{
	public Controller btnsState;

	public GButton closeButton;

	public GButton btn_Sure;

	public GButton btn_Sure_Only;

	public GButton btn_Cancel;

	public const string URL = "ui://xuaw6o8jm48iv";

	public static UICom_PopUpWindow_Bottom CreateInstance()
	{
		BindAll();
		return (UICom_PopUpWindow_Bottom)UIPackage.CreateObject("Common", "Com_PopUpWindow_Bottom");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8j9wtj8h", typeof(UICom_CostPoint));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8j9wy8bw", typeof(UICom_Relic_Quality));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jaepoa6", typeof(UICom_PlayerLabel_Loader));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jak421h", typeof(UICom_HeroSkin));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbczmq3s", typeof(UICom_BuffInfo));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbczmq3t", typeof(UICom_BuffInfoItem));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbfwlq2r", typeof(UICom_Icon_Counter));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbooc3v", typeof(UICom_Backgroup));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jejjwq49", typeof(UIButton_Terms));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jfu2tq3e", typeof(UICom_RelicQuality_Large));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jgj393l", typeof(UICom_Card_Icon));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jgj393m", typeof(UICom_Card_Name));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jh334q3o", typeof(UICom_CardBack));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jheh5a5", typeof(UICom_Loader_PlayerLabel));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8ji1yzq2w", typeof(UIButton_Next2));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jimo37v", typeof(UICom_PlayerLevel));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jm48iv", typeof(UICom_PopUpWindow_Bottom));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jm48iw", typeof(UICom_PopUpWindow_MohuBg));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmicc2g", typeof(UICom_Card));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw3y", typeof(UICom_Icon_Gold));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw3z", typeof(UICom_Icon_Atk));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw40", typeof(UICom_Icon_Def));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw41", typeof(UICom_Icon_Card));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw42", typeof(UICom_Icon_Hp));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw43", typeof(UICom_Icon_Mov));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmouys8q", typeof(UICom_PlayerName));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jo812a", typeof(UICom_RelicKeyword));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0z2", typeof(UICom_LandCard));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq42", typeof(UICom_LibraryCharacterFile));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq45", typeof(UICom_LibraryFileContent));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq46", typeof(UICom_LibrarySkillContent));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78b3", typeof(UIButton_Next));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9k", typeof(UICom_FileContent));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9l", typeof(UICom_SkillContent));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9m", typeof(UICom_CharacterFile));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jrct9q3d", typeof(UIRoomPlayer_Button_RewardUp));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jth3ls8e", typeof(UIGlobalModalWaiting));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8ju43ct", typeof(UICom_Expression));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jz1wk0", typeof(UICom_PlayerLabel));
		UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jz1wk2", typeof(UICom_PlayIcon));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btnsState = GetControllerAt(0);
		closeButton = (GButton)GetChildAt(2);
		btn_Sure = (GButton)GetChildAt(3);
		btn_Sure_Only = (GButton)GetChildAt(4);
		btn_Cancel = (GButton)GetChildAt(5);
	}
}
