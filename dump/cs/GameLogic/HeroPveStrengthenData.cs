using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class HeroPveStrengthenData
{
	public HeroCardData HeroCard;

	public PVENurturanceEnhancementConfigure EnhancementConfig;

	private readonly List<PVENurturanceBreakConfigure> _pveTalentsConfigure = new List<PVENurturanceBreakConfigure>();

	private int _Level;

	public int Exp;

	public readonly List<int> Talent = new List<int>();

	public Signal talentChanged = new Signal();

	public List<PVENurturanceBreakConfigure> PveNurturanceBreakConfigures => _pveTalentsConfigure;

	public int Level
	{
		get
		{
			int num = 0;
			if (HeroCard.heroStatus == HeroStatus.None)
			{
				return 0;
			}
			if (HeroCard.heroStatus == HeroStatus.Activate)
			{
				return _Level;
			}
			return StaticConfigure.Trial.Paramss[0].PveLevel;
		}
	}

	public HeroPveStrengthenData(HeroCardData heroCard, PveHeroStrengthen _PveStrengthen)
	{
		HeroCard = heroCard;
		if (!StaticConfigure.PVENurturance.EnhancementDict.TryGetValue(HeroCard.HeroId, out EnhancementConfig))
		{
			Debug.LogError($"通过角色Id:{HeroCard.HeroId}, 无法从PVENurturance.EnhancementDict获取数据");
		}
		foreach (int item in HeroCard.InfoConfig.PveBreak)
		{
			if (!StaticConfigure.PVENurturance.BreakDict.TryGetValue(item, out var value))
			{
				Debug.LogError($"角色Id:{HeroCard.HeroId}, PVENurturance.BreakDict获取id={item}的配置！");
			}
			else
			{
				_pveTalentsConfigure.Add(value);
			}
		}
		UpdateData(_PveStrengthen);
	}

	public void UpdateData(PveHeroStrengthen _PveStrengthen)
	{
		if (_PveStrengthen != null)
		{
			_Level = _PveStrengthen.Level;
			Exp = _PveStrengthen.Exp;
			Talent.Clear();
			Talent.AddRange(_PveStrengthen.Talent);
		}
	}

	public void UpdateData(int _level, int _exp)
	{
		_Level = _level;
		Exp = _exp;
	}

	public bool IsTalentUnlock(int talentId)
	{
		return Talent.Contains(talentId);
	}

	public void UpdateTalent(RepeatedField<int> talentIds)
	{
		Talent.Clear();
		Talent.AddRange(talentIds);
		talentChanged.Dispatch();
	}

	public bool CanTalentConfig()
	{
		return _pveTalentsConfigure.Count > 0;
	}

	public PVENurturanceBreakConfigure GetCurrentTalentConfigure()
	{
		if (_pveTalentsConfigure.Count == 0)
		{
			return null;
		}
		foreach (PVENurturanceBreakConfigure item in _pveTalentsConfigure)
		{
			if (!IsTalentUnlock(item.Id))
			{
				return item;
			}
		}
		List<PVENurturanceBreakConfigure> pveTalentsConfigure = _pveTalentsConfigure;
		return pveTalentsConfigure[pveTalentsConfigure.Count - 1];
	}

	public int GetUnlockTalentId()
	{
		if (_pveTalentsConfigure.Count == 0)
		{
			return 0;
		}
		foreach (PVENurturanceBreakConfigure item in _pveTalentsConfigure)
		{
			if (!Talent.Contains(item.Id))
			{
				return item.Id;
			}
		}
		return 0;
	}

	public int GetLastLockTalentId()
	{
		if (Talent.Count == 0)
		{
			return 0;
		}
		List<int> talent = Talent;
		return talent[talent.Count - 1];
	}

	public int GetBattleActiveSkillId()
	{
		int lastLockTalentId = GetLastLockTalentId();
		return CharacterHandle.GetBattleActiveSkillId(HeroCard.HeroId, CharacterType.Hero, lastLockTalentId);
	}

	public RepeatedField<int> GetBattlePassiveSkillId()
	{
		int lastLockTalentId = GetLastLockTalentId();
		return CharacterHandle.GetBattlePassiveSkills(HeroCard.HeroId, CharacterType.None, lastLockTalentId);
	}

	public int GetBattlePveLevel()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null || !room.IsInRoom)
		{
			Debug.LogError("当前不在房间内，请以选择其他方式获取等级");
			return Level;
		}
		int pveLevel = StaticConfigure.Trial.Paramss[0].PveLevel;
		if (room.curRoomInfo.IsCampaign())
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsCampaignTrialHeroStatus(HeroCard.HeroId))
			{
				return Mathf.Max(pveLevel, _Level);
			}
			return _Level;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsActivityTrialHeroIds(HeroCard.HeroId))
		{
			return Mathf.Max(pveLevel, _Level);
		}
		ComebackParamsConfigure comebackParamsConfigure = StaticConfigure.Comeback?.ParamsDict?.GetValueOrDefault(1);
		if (comebackParamsConfigure != null && comebackParamsConfigure.PveLevel > 0 && SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsComebackTrialHeroIds(HeroCard.HeroId))
		{
			return Mathf.Max(comebackParamsConfigure.PveLevel, _Level);
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsNoviceTrialHeroIds(HeroCard.HeroId))
		{
			return Mathf.Max(pveLevel, _Level);
		}
		return _Level;
	}
}
