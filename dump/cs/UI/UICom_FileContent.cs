using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_FileContent : GComponent
{
	public GTextField txt_CharacterName;

	public GTextField txt_CharacterNick;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_HP;

	public UICom_Icon_Gold com_Gold;

	public GTextField txt_Gold;

	public GTextField txt_Mechanism;

	public GGroup group_Mechanism;

	public GList list_Skills;

	public GTextField txt_Stroy;

	public GGroup group_Story;

	public Transition OPEN;

	public const string URL = "ui://xuaw6o8jpx78j9k";

	public static UICom_FileContent CreateInstance()
	{
		return (UICom_FileContent)UIPackage.CreateObject("Common", "Com_FileContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_CharacterName = (GTextField)GetChildAt(0);
		txt_CharacterNick = (GTextField)GetChildAt(1);
		txt_ATK = (GTextField)GetChildAt(3);
		txt_DEF = (GTextField)GetChildAt(5);
		txt_HP = (GTextField)GetChildAt(7);
		com_Gold = (UICom_Icon_Gold)GetChildAt(8);
		txt_Gold = (GTextField)GetChildAt(9);
		txt_Mechanism = (GTextField)GetChildAt(12);
		group_Mechanism = (GGroup)GetChildAt(13);
		list_Skills = (GList)GetChildAt(14);
		txt_Stroy = (GTextField)GetChildAt(17);
		group_Story = (GGroup)GetChildAt(18);
		OPEN = GetTransitionAt(0);
	}
}
