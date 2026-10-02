using System;
using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Tools;
using UI;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace GameLogic;

public abstract class Skill_306 : Skill
{
	private List<Tweener> _tweeners = new List<Tweener>();

	private Timer _timer;

	protected void InitConfig(int temporarySkillId)
	{
		skillId = temporarySkillId;
		skillConfig = temporarySkillId.GetSkillConfigure();
	}

	public override async void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).RefreshSkill_SelectLand(_obstacleRange: skillConfig.Params.GetSafeByIndex(1), skillId: skillId, _obstacleNum: 1, _Sn: Sn);
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (!(curPlayerData?.CharacterInst == null))
		{
			await curPlayerData.CharacterInst.SwitchCamera();
			await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
		}
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (!curPlayerData.CharacterInst.activeSkillVail)
		{
			return false;
		}
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		bool result = false;
		foreach (BattlePlayerData item in playerDatas)
		{
			if (item.Property.HP.Value != 0 && curPlayerData.player.Id != item.player.Id && curPlayerData.player.TeamId != item.player.TeamId && !(curPlayerData.CharacterInst == null) && !(item.CharacterInst == null) && SimpleSingletonProvider<LandManager>.inst.CheckDistance(curPlayerData.CharacterInst.standLand.Id, item.CharacterInst.standLand.Id, safeByIndex, 0))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "306主动技能 自己表现");
		if (!StaticConfigure.Perform.InfoDict.TryGetValue(skillConfig.PerformTargets[1], out var value))
		{
			return;
		}
		int totalTime = value.TotalTime;
		int maxHeight = 50;
		List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		int num = 0;
		ResetTweeners();
		for (int i = 0; i < changeAttrPlayerIds.Count; i++)
		{
			foreach (HeroAttrEffect item in SimpleSingletonProvider<GameLogicManager>.inst.battle.GetAttrDataById(changeAttrPlayerIds[i]))
			{
				if (item.Place != null)
				{
					num = item.Place.Place.NodeId;
					BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(item.PlayerId);
					SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerDataById.CharacterInst, willMove: true);
					_ = playerDataById.CharacterInst.standLand.Id;
					PlayParabola(item.Place, maxHeight, totalTime);
				}
			}
		}
		if (num == 0)
		{
			Debug.LogError("Skill_306 技能释放 目标技能 地图格Id 为 0");
			return;
		}
		UnitLand land = SimpleSingletonProvider<LandManager>.inst.GetLandById(num);
		SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayLandShow(land.Id, value.Id, "306主动技能 地图格表现").Forget();
		_timer = Timer.Register(0f, (float)value.TotalTime * 0.001f, (Action)delegate
		{
			ResetTweeners();
			SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(land.Id);
		}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int num2 = 0; num2 < changeAttrPlayerIds.Count; num2++)
		{
			if (changeAttrPlayerIds[num2] != model.PlayerId)
			{
				actionEffectShow.PlayPlayerShow(changeAttrPlayerIds[num2], skillConfig.PerformTarget, $"主动技{skillConfig.Id} 目标演出").Forget();
				if (actionEffectShow.isCancel)
				{
					return;
				}
			}
		}
		if (StaticConfigure.Perform.InfoDict.TryGetValue(skillConfig.PerformTarget, out var value2))
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value2.TotalTime);
		}
	}

	private void PlayParabola(HeroPlaceChangeS2C model, int maxHeight, int awaitTime, Ease ease = Ease.Linear)
	{
		if (model?.Place != null)
		{
			Character character = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId)?.CharacterInst;
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(model.Place.NodeId);
			if (!(character == null) && !(landById == null))
			{
				Vector3 localPosition = character.transform.localPosition;
				Vector3 end = new Vector3(landById.LocalX, localPosition.y, landById.LocalZ);
				character.SendCharacter(model.Place.NodeId, model.Place.FrontNodeIds);
				ParabolaPlayerShow(character.transform, localPosition, end, maxHeight, awaitTime, ease);
			}
		}
	}

	public void ParabolaPlayerShow(Transform obj, Vector3 start, Vector3 end, float maxHeight, int time, Ease ease = Ease.Linear)
	{
		if (obj == null)
		{
			return;
		}
		float num = (float)Mathf.Max(0, time) * 0.001f;
		num /= BattleConfig.OtherSpeed;
		if (num <= 0f)
		{
			obj.localPosition = end;
			return;
		}
		obj.localPosition = start;
		TweenerCore<float, float, FloatOptions> item = DOTween.To(() => 0f, delegate(float progress)
		{
			if (!(obj == null))
			{
				Vector3 localPosition = Vector3.LerpUnclamped(start, end, progress);
				localPosition.y += 4f * Mathf.Max(0f, maxHeight) * progress * (1f - progress);
				obj.localPosition = localPosition;
			}
		}, 1f, num).SetEase(ease).SetTarget(obj)
			.OnComplete(delegate
			{
				if (obj != null)
				{
					obj.localPosition = end;
				}
			})
			.OnKill(delegate
			{
				if (obj != null)
				{
					obj.localPosition = end;
				}
			})
			.SetAutoKill();
		_tweeners.Add(item);
	}

	private void ResetTweeners()
	{
		if (_tweeners == null)
		{
			return;
		}
		foreach (Tweener tweener in _tweeners)
		{
			tweener?.Kill();
		}
		_tweeners.Clear();
	}
}
