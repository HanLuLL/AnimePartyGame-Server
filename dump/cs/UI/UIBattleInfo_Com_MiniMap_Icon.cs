using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Com_MiniMap_Icon : GComponent
{
	private int _LandID;

	private long playerID;

	public Controller player;

	public Controller deploy;

	public GLoader loader_Head;

	public const string URL = "ui://fxejlqlfvi802w";

	public void InitData(BattlePlayerData data, Vector2 miniPos, int landId)
	{
		playerID = data.player.Id;
		if (data.characterType == CharacterType.Hero)
		{
			player.selectedIndex = data.player.Slot;
			loader_Head.url = data.player.characterConfig.CharacterMap;
		}
		else if (data.characterType == CharacterType.Monster)
		{
			loader_Head.url = data.player.characterConfig.CharacterMap;
			if (data.player.characterConfig.MonsterType == MonsterType.Boss)
			{
				player.selectedIndex = 6;
			}
			else
			{
				player.selectedIndex = 4;
			}
		}
		Move(miniPos, landId);
	}

	public void Move(Vector2 miniPos, int landId)
	{
		SetXY(miniPos.x, miniPos.y);
		_LandID = landId;
	}

	public void SetDeploy(Dictionary<long, UIBattleInfo_Com_MiniMap_Icon> _miniMapIcons)
	{
		deploy.selectedIndex = 0;
		if (_miniMapIcons == null)
		{
			return;
		}
		foreach (KeyValuePair<long, UIBattleInfo_Com_MiniMap_Icon> _miniMapIcon in _miniMapIcons)
		{
			if (_miniMapIcon.Key != playerID && _miniMapIcon.Value._LandID == _LandID)
			{
				deploy.selectedIndex = player.selectedIndex + 1;
				break;
			}
		}
	}

	public static UIBattleInfo_Com_MiniMap_Icon CreateInstance()
	{
		return (UIBattleInfo_Com_MiniMap_Icon)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_MiniMap_Icon");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
		deploy = GetControllerAt(1);
		loader_Head = (GLoader)GetChildAt(0);
	}
}
