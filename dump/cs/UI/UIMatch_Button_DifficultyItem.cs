using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIMatch_Button_DifficultyItem : GButton
{
	public ChoosingTimeLimitdifficultyConfigure Info;

	public Controller status;

	public Controller colorType;

	public GTextField txt_Name;

	public const string URL = "ui://qxwapsemgib2u";

	public bool CurrentSelectedStatus => status.selectedIndex == 1;

	public void Refresh(int difficultyType)
	{
		if (!StaticConfigure.ChoosingTimeLimit.DifficultyDict.TryGetValue(difficultyType, out Info))
		{
			Debug.LogError($"difficulty {difficultyType} not found at ChoosingTimeLimit.DifficultyDict");
			return;
		}
		icon = CommonUIManager.DifficultyIcons.GetSafeByIndex((int)Info.GameDifficultyType);
		txt_Name.text = Info.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
		colorType.selectedIndex = (int)Info.GameDifficultyType;
	}

	public void UpdateStatus()
	{
		if (Info != null)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.match.IsVailDifficulty((int)Info.GameDifficultyType);
			base.grayed = !flag;
		}
	}

	public static UIMatch_Button_DifficultyItem CreateInstance()
	{
		return (UIMatch_Button_DifficultyItem)UIPackage.CreateObject("Match", "Match_Button_DifficultyItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		colorType = GetControllerAt(2);
		txt_Name = (GTextField)GetChildAt(3);
	}
}
