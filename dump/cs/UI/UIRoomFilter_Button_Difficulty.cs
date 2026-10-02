using Core;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UIRoomFilter_Button_Difficulty : GButton
{
	public ChoosingTimeLimitdifficultyConfigure ChooseConfig;

	public Controller colorType;

	public Controller status;

	public GTextField txt_Name;

	public GTextField txt_Desc;

	public Transition CutIn;

	public const string URL = "ui://672kmwr2m49ib";

	public bool CurrentSelectedStatus => status.selectedIndex == 1;

	public void Refresh(int index, int difficulty)
	{
		if (!StaticConfigure.ChoosingTimeLimit.DifficultyDict.TryGetValue(difficulty, out var value))
		{
			Debug.LogError($"difficulty {difficulty} not found at ChoosingTimeLimit.DifficultyDict");
			return;
		}
		ChooseConfig = value;
		status.selectedIndex = (LocalCache.GetPVEDifficultyStatusByIndex(index) ? 1 : 0);
		colorType.selectedIndex = index;
		icon = value.Pic;
		txt_Name.text = value.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
		txt_Desc.text = value.TipsID.GetLocal(UIStringType.ChoosingTimeLimit);
	}

	public void SwitchStatus()
	{
		if (ChooseConfig != null)
		{
			GameDifficultyType gameDifficultyType = ChooseConfig.GameDifficultyType;
			bool flag = status.selectedIndex == 0;
			status.selectedIndex = (flag ? 1 : 0);
			LocalCache.UpdatePVEDifficulty((int)gameDifficultyType, flag);
		}
	}

	public static UIRoomFilter_Button_Difficulty CreateInstance()
	{
		return (UIRoomFilter_Button_Difficulty)UIPackage.CreateObject("RoomFilter", "RoomFilter_Button_Difficulty");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		colorType = GetControllerAt(0);
		status = GetControllerAt(2);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Desc = (GTextField)GetChildAt(4);
		CutIn = GetTransitionAt(0);
	}
}
