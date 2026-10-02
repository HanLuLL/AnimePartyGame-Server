using System.Collections.Generic;
using Core.Scene;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class CharacterShowComponent_305 : CharacterShowComponent, IMoveAnimRule, IActionStartNotify
{
	private const int HERO_ELEMENT = 306;

	private bool _resetMoveAnim;

	public override string TryGetAttackTimelineAsset(MapField<int, string> attackDict)
	{
		if (attackDict.Count > 1)
		{
			List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
			if (battleShowDirector != null)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(battleShowDirector.defendeId);
				if (playerDataById != null && playerDataById.player.Hero.HeroId != 306)
				{
					for (int i = 0; i < playerDatas.Count; i++)
					{
						if (playerDatas[i].player.Hero.HeroId == 306 && attackDict.TryGetValue(1, out var value))
						{
							return value;
						}
					}
				}
			}
		}
		if (attackDict.TryGetValue(0, out var value2))
		{
			return value2;
		}
		Debug.LogError("无法获取对战资源配置中Attack资源");
		return null;
	}

	public bool ShouldStopMoveAnim()
	{
		return !KeepMoveAnim();
	}

	public override void UpdateAfterReconnect()
	{
		base.UpdateAfterReconnect();
		if (Owner != null && SkillIsTalenting())
		{
			if (Owner.characterAnimator.GetAnimeStatus("Talenting"))
			{
				Owner.characterAnimator.SetAnime("Talenting", status: false);
			}
			_resetMoveAnim = false;
			Owner.characterAnimator.Move(walk: true);
		}
	}

	public virtual string GetWalkAnimeName(string defaultAnimeName = "Walk")
	{
		if ((Owner != null && SkillIsTalenting()) || _resetMoveAnim)
		{
			return "Walk_02";
		}
		return defaultAnimeName;
	}

	public virtual string GetWalkBackAnimeName(string defaultAnimeName = "Walk-Back")
	{
		if ((Owner != null && SkillIsTalenting()) || _resetMoveAnim)
		{
			return "Walk-Back_02";
		}
		return defaultAnimeName;
	}

	public virtual bool KeepMoveAnim()
	{
		return SkillIsTalenting();
	}

	public void ActionStartNotify()
	{
		if (!SkillIsTalenting())
		{
			_resetMoveAnim = true;
			Owner.characterAnimator.Move(walk: false);
			_resetMoveAnim = false;
		}
	}

	private bool SkillIsTalenting()
	{
		if (Owner.skill is Skill_305 skill_)
		{
			return Owner.player.buffContainer?.GetBuff(skill_.skillConfig.BuffId[0]) != null;
		}
		return false;
	}
}
