using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStoryWindow : GComponent
{
	public UIStory_Com_Video com_Video;

	public GComponent com_Perform;

	public UIStory_Com_Dialog com_Dialog;

	public GGraph graph_Speaker;

	public GTextField txt_Speaker;

	public GLoader loader_Speaker;

	public GGroup group_Speaker;

	public GProgressBar progress_Time;

	public UIStory_Button_Skip btn_Skip;

	public GButton btn_ESC;

	public const string URL = "ui://abmw5cfoec7w0";

	public static UIStoryWindow CreateInstance()
	{
		BindAll();
		return (UIStoryWindow)UIPackage.CreateObject("Story", "StoryWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://abmw5cfoec7w0", typeof(UIStoryWindow));
		UIObjectFactory.SetPackageItemExtension("ui://abmw5cfoec7w4", typeof(UIStory_Com_Dialog));
		UIObjectFactory.SetPackageItemExtension("ui://abmw5cfohkbbc", typeof(UIStory_Button_Skip));
		UIObjectFactory.SetPackageItemExtension("ui://abmw5cfoiznzn", typeof(UIStory_Com_PerformItem));
		UIObjectFactory.SetPackageItemExtension("ui://abmw5cforct9o", typeof(UIStory_Com_Video));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Video = (UIStory_Com_Video)GetChildAt(0);
		com_Perform = (GComponent)GetChildAt(1);
		com_Dialog = (UIStory_Com_Dialog)GetChildAt(2);
		graph_Speaker = (GGraph)GetChildAt(3);
		txt_Speaker = (GTextField)GetChildAt(4);
		loader_Speaker = (GLoader)GetChildAt(5);
		group_Speaker = (GGroup)GetChildAt(6);
		progress_Time = (GProgressBar)GetChildAt(7);
		btn_Skip = (UIStory_Button_Skip)GetChildAt(8);
		btn_ESC = (GButton)GetChildAt(9);
	}
}
