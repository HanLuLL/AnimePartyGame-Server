using System;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class Card_10007 : Card
{
	public Card_10007()
	{
		cardId = 10007;
		config = cardId.GetCardConfigure();
	}

	public override UniTask CardAction(long _Sn)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		return UniTask.CompletedTask;
	}

	public override int GetCostValue(long playerId, HandCardData handCardData)
	{
		if (playerId != 0L)
		{
			Buff buff = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).buffContainer.GetBuff(1151201);
			if (buff != null && buff.Chain.Count > 0 && buff.Chain[0].S == buff_source.Types.source.Skill)
			{
				SkillInfoConfigure skillConfigure = buff.Chain[0].Id.GetSkillConfigure();
				if (skillConfigure != null && skillConfigure.Params.Count > 1)
				{
					if (handCardData == null)
					{
						return config.Cost + skillConfigure.Params[5];
					}
					return Math.Max(0, handCardData.BattleCost + skillConfigure.Params[5]);
				}
			}
		}
		return base.GetCostValue(playerId, handCardData);
	}
}
