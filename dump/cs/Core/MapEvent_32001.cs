using System.Collections.Generic;
using Core.MapEvents;
using GameLogic;
using Tools;

namespace Core;

public class MapEvent_32001 : MapEvent
{
	public MapEvent_32001()
	{
		StaticConfigure.MapEvent.InfoDict.TryGetValue(32001, out config);
	}

	public override void MonsterShow()
	{
		if (!StaticConfigure.MapEvent.InfoDict.TryGetValue(32004, out var value))
		{
			return;
		}
		int num = value.Triggerparams[0];
		int curMaxGameProgress = SimpleSingletonProvider<GameLogicManager>.inst.battle.CurMaxGameProgress;
		if (num <= curMaxGameProgress)
		{
			return;
		}
		int num2 = config.Params1[0];
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].player.Hero.HeroId == num2 && playerDatas[i].CharacterInst != null)
			{
				playerDatas[i].CharacterInst.characterAnimator.Sleep(sleep: true);
			}
		}
	}
}
