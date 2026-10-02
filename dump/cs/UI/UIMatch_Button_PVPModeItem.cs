using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIMatch_Button_PVPModeItem : GButton
{
	public GameModeInfoConfigure Info;

	public Controller status;

	public Controller colorType;

	public GTextField txt_Name;

	public const string URL = "ui://qxwapsemr26s1m";

	public void Refresh(GameModeInfoConfigure info)
	{
		Info = info;
		status.selectedIndex = 0;
		txt_Name.text = info.NameID.GetLocal(UIStringType.GameMode);
		int indexByMode = GetIndexByMode(Info.MapModeType);
		icon = CommonUIManager.PVPModeIcons.GetSafeByIndex(indexByMode);
		colorType.selectedIndex = indexByMode;
	}

	private int GetIndexByMode(MapModeType mapMode)
	{
		return mapMode switch
		{
			MapModeType.Standard => 0, 
			MapModeType.AsymmetricalBattle => 1, 
			MapModeType.Ultra => 2, 
			_ => 0, 
		};
	}

	public static UIMatch_Button_PVPModeItem CreateInstance()
	{
		return (UIMatch_Button_PVPModeItem)UIPackage.CreateObject("Match", "Match_Button_PVPModeItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		colorType = GetControllerAt(2);
		txt_Name = (GTextField)GetChildAt(3);
	}
}
