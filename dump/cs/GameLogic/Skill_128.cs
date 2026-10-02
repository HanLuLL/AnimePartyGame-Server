using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_128 : Skill
{
	private const int _skillEffectId = 12801;

	private const int _skillBurstEffectId = 12802;

	private Effect _skillEffect;

	private bool _roadBlockElement;

	public int SkillBuffId;

	private Vector3 _effectScale = Vector3.one;

	private const float _effectScaleDelta = 0.1f;

	public override async void SkillReleaseAction(long actionSn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).RefreshCardInfo_SkillSelectPoint(maxPoint: skillConfig.Params.GetSafeByIndex(1), actionSn: actionSn, skillId: skillId);
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(_playerId);
	}

	public void SetSkillAnime(long playerId, bool status)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null))
		{
			playerDataById.CharacterInst.characterAnimator?.SetAnime("Talenting", status);
		}
	}

	public async UniTask PlayRoleSkillEffect(long playerId)
	{
		_roadBlockElement = false;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null)
		{
			RoomPlayer player = playerDataById.player;
			int effectId = ((player != null && player.standingPainting?.ItemID == 100128003) ? 12804 : 12801);
			_skillEffect = await playerDataById.CharacterInst.PlayCharacterEffect(effectId);
		}
	}

	public async UniTask PlayBurstEffect(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null)
		{
			Effect effect = await playerDataById.CharacterInst.PlayCharacterEffect(12802);
			if (effect != null)
			{
				effect.transform.localScale = _effectScale;
			}
		}
	}

	private void StopRoleSkillEffect(long playerId)
	{
		SetSkillAnime(playerId, status: false);
		if ((object)_skillEffect != null)
		{
			_skillEffect.ReleaseEffect();
			_skillEffect = null;
		}
	}

	public void SetSignalByRoadblock()
	{
		_roadBlockElement = true;
	}

	private void DoScaleSkillEffect(int count)
	{
		if ((object)_skillEffect != null)
		{
			_skillEffect.transform.localScale += Vector3.one * count * 0.1f;
			_effectScale = _skillEffect.transform.localScale;
		}
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<HeroAttrEffect> list = model.EffectDatas.Where((HeroAttrEffect attrData) => attrData.Place != null).ToList();
		if (list.Count > 0)
		{
			for (int num = 0; num < list.Count; num++)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(list[num].Place.PlayerId);
				if (playerDataById != null && playerDataById.CharacterInst != null)
				{
					playerDataById.CharacterInst.SendCharacter(list[num].Place.Place.NodeId, list[num].Place.Place.FrontNodeIds);
					playerDataById.FlashShow();
				}
				else
				{
					Debug.LogError($"根据玩家ID: {list[num].Place.PlayerId} 查询到底数据存在问题，{playerDataById == null} {playerDataById?.CharacterInst == null}");
				}
			}
			BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
			if (playerDataById2 == null)
			{
				return;
			}
			SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerDataById2.CharacterInst, willMove: false);
		}
		List<HeroAttrEffect> list2 = model.EffectDatas.Where((HeroAttrEffect attrData) => attrData.Disappear != null).ToList();
		if (list2.Count > 0)
		{
			DoScaleSkillEffect(list2.Count);
		}
		List<HeroAttrEffect> list3 = model.EffectDatas.Where((HeroAttrEffect attrData) => attrData.Def != null).ToList();
		if (list3.Count > 0)
		{
			for (int num2 = 0; num2 < list3.Count; num2++)
			{
				perform.PlayPlayerShow(list3[num2].PlayerId, skillConfig.PerformTarget, $"主动技{skillConfig.Id} 目标演出").Forget();
			}
		}
		List<HeroAttrEffect> list4 = model.EffectDatas.Where((HeroAttrEffect attrData) => attrData.Buff != null).ToList();
		if (list4.Count <= 0)
		{
			return;
		}
		foreach (HeroAttrEffect item in list4)
		{
			if (item.Buff.Buff.BuffId != SkillBuffId)
			{
				continue;
			}
			if (item.Buff.Op == HeroBuffChangeS2C.Types.Oper.Insert)
			{
				await perform.PlayPlayerShow(item.Buff.PlayerId, skillConfig.PerformSelf, $"主动技{skillConfig.Id} 释放演出");
				if (perform.isCancel)
				{
					return;
				}
			}
			else
			{
				if (item.Buff.Op != HeroBuffChangeS2C.Types.Oper.Delete)
				{
					continue;
				}
				StopRoleSkillEffect(item.Buff.PlayerId);
				if (_roadBlockElement)
				{
					await perform.PlayPlayerShow(item.Buff.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(2), $"主动技{skillConfig.Id} 强化路障演出", ignoreDuration: true);
					if (perform.isCancel)
					{
						return;
					}
				}
				await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(1), $"主动技{skillConfig.Id} 结束演出");
				if (perform.isCancel)
				{
					return;
				}
			}
		}
	}
}
