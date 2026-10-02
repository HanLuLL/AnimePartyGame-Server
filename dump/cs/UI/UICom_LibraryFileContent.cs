using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_LibraryFileContent : GComponent
{
	public GTextField txt_Mechanism;

	public GGroup group_Mechanism;

	public GList list_Skills;

	public GTextField txt_Stroy;

	public GGroup group_Story;

	public Transition OPEN;

	public const string URL = "ui://xuaw6o8jpj0zq45";

	public static UICom_LibraryFileContent CreateInstance()
	{
		return (UICom_LibraryFileContent)UIPackage.CreateObject("Common", "Com_LibraryFileContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Mechanism = (GTextField)GetChildAt(2);
		group_Mechanism = (GGroup)GetChildAt(3);
		list_Skills = (GList)GetChildAt(4);
		txt_Stroy = (GTextField)GetChildAt(7);
		group_Story = (GGroup)GetChildAt(8);
		OPEN = GetTransitionAt(0);
	}
}
