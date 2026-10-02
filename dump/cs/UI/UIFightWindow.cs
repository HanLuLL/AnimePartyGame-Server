using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFightWindow : GComponent
{
	public Controller step;

	public Controller playerState;

	public UIFight_LaunchPK com_LaunchPK;

	public UIFight_Show com_Show;

	public UIFight_Card com_Card;

	public GButton btn_Attack;

	public GGraph loader_FightShow;

	public GButton btn_Defend;

	public UIFight_Button_Dodge btn_Dodge;

	public GGroup chooseActive;

	public GGroup group__audienceCard;

	public GTextField txt_DiceTip;

	public GGroup group__audienceDice;

	public GTextField txt_ChoiceTip;

	public GGroup group__audienceChoice;

	public GTextField txt_Defend;

	public GTextField txt_Dodge;

	public GTextField txt_ChoiceResult;

	public GGroup group__audienceChoiceResult;

	public GTextField txt_Failure;

	public GTextField txt_Success;

	public GTextField txt_ResultTip;

	public GGroup group__audienceResult;

	public UIFight_Com_Value com_value;

	public const string URL = "ui://8irq146hmjruc";

	public static UIFightWindow CreateInstance()
	{
		BindAll();
		return (UIFightWindow)UIPackage.CreateObject("Fight", "FightWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://8irq146ha90c4p", typeof(UIFight_Com_Point));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hdde05b", typeof(UIFight_Com_Value_Player));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hglhu4j", typeof(UIFight_Com_Cost));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hmjruc", typeof(UIFightWindow));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hmjrud", typeof(UIFight_LaunchPK));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hmjrug", typeof(UIFight_Show));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hmjruj", typeof(UIFight_Com_CardHotZone));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hmjruk", typeof(UIFight_Card));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146ht4dl2g", typeof(UIFight_Com_Attacker));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146ht4dl2j", typeof(UIFight_Com_ReadyLabel));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hwa8253", typeof(UIFight_Com_Value));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hwa8254", typeof(UIFight_Com_Value_Card));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hwvm91a", typeof(UIFight_Com_Defenser));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hwvm91c", typeof(UIFight_PlayerInfo));
		UIObjectFactory.SetPackageItemExtension("ui://8irq146hwvm91g", typeof(UIFight_Button_Dodge));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		step = GetControllerAt(0);
		playerState = GetControllerAt(1);
		com_LaunchPK = (UIFight_LaunchPK)GetChildAt(0);
		com_Show = (UIFight_Show)GetChildAt(1);
		com_Card = (UIFight_Card)GetChildAt(2);
		btn_Attack = (GButton)GetChildAt(3);
		loader_FightShow = (GGraph)GetChildAt(4);
		btn_Defend = (GButton)GetChildAt(5);
		btn_Dodge = (UIFight_Button_Dodge)GetChildAt(6);
		chooseActive = (GGroup)GetChildAt(7);
		group__audienceCard = (GGroup)GetChildAt(10);
		txt_DiceTip = (GTextField)GetChildAt(13);
		group__audienceDice = (GGroup)GetChildAt(14);
		txt_ChoiceTip = (GTextField)GetChildAt(17);
		group__audienceChoice = (GGroup)GetChildAt(18);
		txt_Defend = (GTextField)GetChildAt(20);
		txt_Dodge = (GTextField)GetChildAt(21);
		txt_ChoiceResult = (GTextField)GetChildAt(22);
		group__audienceChoiceResult = (GGroup)GetChildAt(23);
		txt_Failure = (GTextField)GetChildAt(25);
		txt_Success = (GTextField)GetChildAt(26);
		txt_ResultTip = (GTextField)GetChildAt(27);
		group__audienceResult = (GGroup)GetChildAt(28);
		com_value = (UIFight_Com_Value)GetChildAt(29);
	}
}
