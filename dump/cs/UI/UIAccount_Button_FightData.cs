using FairyGUI;
using FairyGUI.Utils;
using GameLogic;

namespace UI;

public class UIAccount_Button_FightData : GButton
{
	public BattleShortRecord Record;

	public Controller status;

	public GTextField txt_MapMode;

	public GTextField txt_Rank;

	public GTextField txt_HeroName;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7zhzth";

	public void Refresh(BattleShortRecord record)
	{
		Record = record;
		if (StaticConfigure.GameMode.InfoDict.TryGetValue(record.MapType, out var value))
		{
			txt_MapMode.text = value.NameID.GetLocal(UIStringType.GameMode);
		}
		else
		{
			txt_MapMode.text = "";
		}
		status.selectedIndex = ((record.Rank == 0) ? 1 : 0);
		GTextField gTextField = txt_Rank;
		object obj;
		if (BattleConfig.IsPVE(record.MapType))
		{
			obj = ((record.Rank == 1) ? "WIN" : "LOSE");
		}
		else
		{
			int rank = record.Rank;
			obj = rank.ToString();
		}
		gTextField.text = (string)obj;
		base.selected = false;
		if (record.HeroId != 0)
		{
			txt_HeroName.text = CharacterHandle.GetCharacterNickName(record.HeroId);
		}
		else
		{
			txt_HeroName.text = "";
		}
	}

	public static UIAccount_Button_FightData CreateInstance()
	{
		return (UIAccount_Button_FightData)UIPackage.CreateObject("AccountInfo", "Account_Button_FightData");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		txt_MapMode = (GTextField)GetChildAt(1);
		txt_Rank = (GTextField)GetChildAt(2);
		txt_HeroName = (GTextField)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
