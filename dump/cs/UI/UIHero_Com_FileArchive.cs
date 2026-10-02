using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_FileArchive : GComponent
{
	public GTextField txt_CharacterName;

	public GTextField txt_NickName;

	public UIHero_Com_InfoArchiveContentDesc com_Desc;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxbfwlq3v";

	public static UIHero_Com_FileArchive CreateInstance()
	{
		return (UIHero_Com_FileArchive)UIPackage.CreateObject("Hero", "Hero_Com_FileArchive");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_CharacterName = (GTextField)GetChildAt(2);
		txt_NickName = (GTextField)GetChildAt(3);
		com_Desc = (UIHero_Com_InfoArchiveContentDesc)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
