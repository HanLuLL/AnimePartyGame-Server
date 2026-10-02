using System.Collections.Generic;
using Core.Scene;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class CharacterShowComponent_304 : CharacterShowComponent
{
	private const int HERO_ELEMENT = 303;

	public override string TryGetAttackTimelineAsset(MapField<int, string> attackDict)
	{
		if (attackDict.Count > 1)
		{
			List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
			if (battleShowDirector != null)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(battleShowDirector.defendeId);
				if (playerDataById != null && playerDataById.player.Hero.HeroId != 303)
				{
					for (int i = 0; i < playerDatas.Count; i++)
					{
						if (playerDatas[i].player.Hero.HeroId == 303)
						{
							return attackDict[1];
						}
					}
				}
			}
		}
		if (attackDict.TryGetValue(0, out var value))
		{
			return value;
		}
		Debug.LogError("无法获取对战资源配置中Attack资源");
		return null;
	}
}
