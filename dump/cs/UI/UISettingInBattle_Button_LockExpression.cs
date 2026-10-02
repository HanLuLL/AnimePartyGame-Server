using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingInBattle_Button_LockExpression : GButton
{
	public Controller setLock;

	public GLoader loader_Head;

	public const string URL = "ui://h47kn589d11o1";

	public static UISettingInBattle_Button_LockExpression CreateInstance()
	{
		return (UISettingInBattle_Button_LockExpression)UIPackage.CreateObject("SettingInBattle", "SettingInBattle_Button_LockExpression");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		setLock = GetControllerAt(1);
		loader_Head = (GLoader)GetChildAt(0);
	}
}
