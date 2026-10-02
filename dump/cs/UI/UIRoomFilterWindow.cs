using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomFilterWindow : GComponent
{
	public GGraph mohu;

	public GRichTextField txt__Advise;

	public GList list_Difficulty;

	public GButton btn_Sure;

	public GButton btn_prePage;

	public GButton btn_nextPage;

	public Transition Cut_in;

	public const string URL = "ui://672kmwr2m49i0";

	public static UIRoomFilterWindow CreateInstance()
	{
		BindAll();
		return (UIRoomFilterWindow)UIPackage.CreateObject("RoomFilter", "RoomFilterWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://672kmwr2m49i0", typeof(UIRoomFilterWindow));
		UIObjectFactory.SetPackageItemExtension("ui://672kmwr2m49ib", typeof(UIRoomFilter_Button_Difficulty));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		txt__Advise = (GRichTextField)GetChildAt(5);
		list_Difficulty = (GList)GetChildAt(6);
		btn_Sure = (GButton)GetChildAt(7);
		btn_prePage = (GButton)GetChildAt(8);
		btn_nextPage = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
