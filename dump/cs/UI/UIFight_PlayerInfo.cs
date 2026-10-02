using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIFight_PlayerInfo : GComponent
{
	private BattlePlayerData attackerData;

	private BattlePlayerData defenderData;

	private int minATK;

	private int maxATK;

	private int minDEF;

	private int maxDEF;

	private int defendDicePoint;

	public Controller tab;

	public UIFight_Com_Attacker com_Attack;

	public UIFight_Com_Defenser com_Defend;

	public const string URL = "ui://8irq146hwvm91c";

	public void InitState(bool isAttack, long playerId)
	{
		attackerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		defenderData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (isAttack)
		{
			tab.selectedIndex = 0;
			com_Attack.ShowUp.selectedIndex = 0;
			com_Attack.showPoint.selectedIndex = 0;
			com_Attack.showReady.selectedIndex = 0;
			com_Attack.showHp.selectedIndex = 0;
			UIFight_Com_Point com_Point = com_Attack.com_Point;
			if (attackerData.characterType == CharacterType.Monster)
			{
				com_Point.player.selectedIndex = 4;
				com_Attack.com_ReadyLabel.player.selectedIndex = 4;
			}
			else
			{
				com_Point.player.selectedIndex = attackerData.player.Slot;
				com_Attack.com_ReadyLabel.player.selectedIndex = attackerData.player.Slot;
			}
			return;
		}
		tab.selectedIndex = 1;
		com_Defend.ShowUp.selectedIndex = 0;
		com_Defend.showPoint.selectedIndex = 0;
		com_Defend.showReady.selectedIndex = 0;
		com_Defend.changeState.selectedIndex = 0;
		com_Defend.showResult.selectedIndex = 0;
		UIFight_Com_Point com_Point2 = com_Defend.com_Point;
		if (defenderData.characterType == CharacterType.Monster)
		{
			com_Point2.player.selectedIndex = 4;
			com_Defend.com_ReadyLabel.player.selectedIndex = 4;
		}
		else
		{
			com_Point2.player.selectedIndex = defenderData.player.Slot;
			com_Defend.com_ReadyLabel.player.selectedIndex = defenderData.player.Slot;
		}
	}

	public void RefreshLife(int _curHp)
	{
		com_Defend.txt_Life.text = _curHp.ToString();
	}

	public void RefreshAttackAttr(int life, int _minATK, int _maxATK)
	{
		com_Attack.txt_Life.text = life.ToString();
		com_Attack.txt_CardValue.SetVar("min", _minATK.ToString()).SetVar("max", _maxATK.ToString()).FlushVars();
		if (minATK != _minATK || maxATK != _maxATK)
		{
			minATK = _minATK;
			maxATK = _maxATK;
			com_Attack.ValueChange.Play();
		}
	}

	public void RefreshDefendAttr(int life, int _minDEF, int _maxDEF)
	{
		com_Defend.txt_Life.text = life.ToString();
		com_Defend.txt_CardValue.SetVar("min", _minDEF.ToString()).SetVar("max", _maxDEF.ToString()).FlushVars();
		if (minDEF != _minDEF || maxDEF != _maxDEF)
		{
			minDEF = _minDEF;
			maxDEF = _maxDEF;
			com_Defend.ValueChange.Play();
		}
	}

	public void ShowHitValue(int _HitValue)
	{
		com_Defend.txt_HitValue.SetVar("hp", _HitValue.ToString()).FlushVars();
		if (Mathf.Abs(_HitValue) >= StaticGlobalData.GAME_FIGHT_DAMAGE_THRESHOLD)
		{
			com_Defend.HugeDamage.Play();
		}
		else
		{
			com_Defend.DamgeFloat.Play();
		}
	}

	public void SwitchHpState(bool show)
	{
		com_Attack.showHp.selectedIndex = ((!show) ? 1 : 0);
	}

	public async UniTask RefreshDice_Attacker(int state, int _Point)
	{
		UIFight_Com_Point comPoint = com_Attack.com_Point;
		RefreshPoint_Attacker(state, _Point);
		if (_Point == 0)
		{
			com_Attack.com_Point.ShowDiceEffect.Play();
		}
		comPoint.aMovie_Dice.onPlayEnd.Set((EventCallback0)delegate
		{
			if (_Point == 6)
			{
				PlayMaxPointShow(comPoint.graph_Effect);
				comPoint.MaxPoint.Play();
			}
			else
			{
				comPoint.PointChange.Play(delegate
				{
					com_Attack.com_Point.ShowDiceEffect.Stop();
					com_Attack.com_Point.Image_DiceEffect.visible = false;
				});
				comPoint.graph_Effect.visible = false;
			}
			comPoint.aMovie_Dice.playing = false;
		});
		comPoint.aMovie_Dice.SetPlaySettings(0, -1, 1, _Point - 1);
		comPoint.group_Point.visible = false;
		comPoint.aMovie_Dice.playing = true;
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
	}

	public void RefreshPoint_Attacker(int state, int _Point)
	{
		com_Attack.showPoint.selectedIndex = state;
		UIFight_Com_Point com_Point = com_Attack.com_Point;
		com_Point.txt_max.visible = false;
		com_Point.txt_Point.text = _Point.ToString();
		if (com_Attack.showPoint.selectedIndex == 2)
		{
			com_Point.PointChange.Play();
		}
	}

	public async UniTask<bool> RefreshDice_Defender(int waitMS)
	{
		if (defendDicePoint == 0)
		{
			com_Defend.com_Point.ShowDiceEffect.Play();
		}
		UIFight_Com_Point comPoint = com_Defend.com_Point;
		comPoint.aMovie_Dice.onPlayEnd.Set((EventCallback0)delegate
		{
			if (defendDicePoint == 6)
			{
				PlayMaxPointShow(comPoint.graph_Effect);
				comPoint.MaxPoint.Play();
			}
			else
			{
				comPoint.graph_Effect.visible = false;
				comPoint.PointChange.Play(delegate
				{
					com_Defend.com_Point.ShowDiceEffect.Stop();
					com_Defend.com_Point.Image_DiceEffect.visible = false;
				});
			}
			comPoint.aMovie_Dice.playing = false;
		});
		comPoint.group_Point.visible = false;
		comPoint.aMovie_Dice.playing = true;
		return !(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(waitMS));
	}

	public void RefreshPoint_Defender(int state, int _Point, bool _dodge = false)
	{
		if (_dodge)
		{
			com_Defend.txt_CardValue.SetVar("min", "0").SetVar("max", "0").FlushVars();
		}
		UIFight_Com_Point com_Point = com_Defend.com_Point;
		defendDicePoint = _Point;
		com_Defend.showPoint.selectedIndex = state;
		com_Defend.changeState.selectedIndex = (_dodge ? 1 : 0);
		com_Point.txt_max.visible = false;
		com_Point.txt_Point.text = _Point.ToString();
		if (_Point <= 6)
		{
			com_Point.aMovie_Dice.SetPlaySettings(0, -1, 1, _Point - 1);
		}
		if (com_Defend.showPoint.selectedIndex == 2)
		{
			com_Point.PointChange.Play();
		}
	}

	private async void PlayMaxPointShow(GGraph graph)
	{
		await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(52.GetEffectDataConfigure().EffectName, graph, 20f);
	}

	public void DestroyAttackMaxPointEffect()
	{
		GGraph graph_Effect = com_Attack.com_Point.graph_Effect;
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(graph_Effect);
	}

	public void RefreshReadyLabel(long _playerId)
	{
		if (attackerData != null && attackerData.player.Id == _playerId)
		{
			com_Attack.showReady.selectedIndex = 1;
		}
		if (defenderData != null && defenderData.player.Id == _playerId)
		{
			com_Defend.showReady.selectedIndex = 1;
		}
	}

	public static UIFight_PlayerInfo CreateInstance()
	{
		return (UIFight_PlayerInfo)UIPackage.CreateObject("Fight", "Fight_PlayerInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		com_Attack = (UIFight_Com_Attacker)GetChildAt(0);
		com_Defend = (UIFight_Com_Defenser)GetChildAt(1);
	}
}
