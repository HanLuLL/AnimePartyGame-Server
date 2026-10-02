using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class BuffEffectManager : SimpleSingletonProvider<BuffEffectManager>
{
	private readonly Dictionary<long, Effect> buffEffectDict = new Dictionary<long, Effect>();

	private readonly Dictionary<int, IBuffEffectHandler> _specialHandlers = new Dictionary<int, IBuffEffectHandler>
	{
		{
			2000701,
			new SoulLinkBuffEffectHandler()
		},
		{
			3030101,
			new BlendingBuffEffectHandler()
		},
		{
			1140101,
			new HaiQingBuffEffectHandler()
		},
		{
			1140102,
			new HaiQingBuffEffectHandler()
		},
		{
			10111202,
			new YaYangSoulClingingEffectHandler()
		},
		{
			10460101,
			new KongQueBiggerBuffEffectHandler()
		},
		{
			1270202,
			new BangNiStealthEffectHandler()
		},
		{
			1230101,
			new JiMengZhaoBuffEffectHandler()
		},
		{
			1230201,
			new JiMengZhaoBuffEffectHandler()
		},
		{
			1071101,
			new LianSkillShieldBuffEffectHandler()
		},
		{
			6011102,
			new TimePauseBuffEffectHandler()
		},
		{
			10651105,
			new BuffEffectHandler_10651105()
		},
		{
			10611101,
			new BuffEffectHandler_10611101()
		},
		{
			10671301,
			new BuffEffectHandler_10671301()
		}
	};

	public Dictionary<int, IBuffEffectHandler> SpecialHandlers => _specialHandlers;

	public async UniTask SyncBuffEffect(Buff Buff, Character target)
	{
		if (_specialHandlers.TryGetValue(Buff.BuffId, out var value))
		{
			await value.Play(Buff, target);
			return;
		}
		BuffInfoConfigure buffConfigure = Buff.BuffId.GetBuffConfigure();
		int num = buffConfigure.EffectID;
		if (buffConfigure.SkinReplaceEffectID.Count > 0)
		{
			int itemID = target.player.standingPainting.ItemID;
			if (buffConfigure.SkinReplaceEffectID.TryGetValue(itemID, out var value2))
			{
				num = value2;
			}
		}
		if (num != 0)
		{
			Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(num, Vector3.zero, Quaternion.identity, target.EffectContainer);
			if (effect != null)
			{
				buffEffectDict.TryAdd(Buff.UniqueId, effect);
			}
		}
	}

	public void DestroyBuffEffect(Buff Buff, Character target)
	{
		if (Buff.Source.S == buff_source.Types.source.Skill)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.card.SkillDeleteShow(Buff.Source.Id, target.player.Id);
		}
		Effect value2;
		if (_specialHandlers.TryGetValue(Buff.BuffId, out var value))
		{
			value.DestroyEffect(Buff, target);
		}
		else if (Buff.BuffId.GetBuffConfigure().EffectID != 0 && buffEffectDict.TryGetValue(Buff.UniqueId, out value2) && value2 != null)
		{
			value2.ReleaseEffect();
			buffEffectDict.Remove(Buff.UniqueId);
		}
	}

	public async UniTask UpdateBuffEffect(Buff Buff, Character target)
	{
		if (_specialHandlers.TryGetValue(Buff.BuffId, out var value))
		{
			await value.UpdateEffect(Buff, target);
		}
	}

	public void Dispose()
	{
		buffEffectDict.Clear();
		foreach (KeyValuePair<int, IBuffEffectHandler> specialHandler in _specialHandlers)
		{
			specialHandler.Value.Dispose();
		}
		OnDestroyInstance();
	}
}
