using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Tutorial;

public class TutorialBoardGameManager
{
	public TutorialStatus TutorialStatus;

	private int _round;

	public List<long> PKTargetIds;

	private CtsInfo PKTsc;

	public int SpecifyDicePoint;

	private int _movePoint;

	public readonly List<AdditionAttribute> moveAdditionAttribute = new List<AdditionAttribute>();

	public int SelectDirLandId = -1;

	public int Round => _round;

	public int MovePoint => _movePoint;

	public void Initialize()
	{
	}

	public void Dispose()
	{
	}

	public void SetTutorialStatus(TutorialStatus status)
	{
		TutorialStatus = status;
		if (TutorialStatus == TutorialStatus.Success)
		{
			TutorialGame.GetSystem<GamePlayManager>().GameEnd();
		}
	}

	public async UniTask RoundStart()
	{
		_round++;
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			curRoomInfo.UpdateRound(_round);
			await MonoSingletonProvider<NetManager>.inst.RPC.GameProgressChangeS2C.OnGameProgressChangeS2CServerCallBackAsync(new GameProgressChangeS2C
			{
				MaxProgress = _round,
				Progress = _round
			}, 0, isDispatch: true);
		}
		if (_round % 3 != 0 || !(SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002))
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowTipsAndWait(string.Format(10001.GetLocal(UIStringType.Message), _round), 2f);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowRoundRewardTip("+3", "+2", 2.5f);
			TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
			UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.RoundAward
				}
			};
			List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			for (int i = 0; i < playerDatas.Count; i++)
			{
				if (playerDatas[i].characterType == CharacterType.Hero)
				{
					RoomPlayer player = playerDatas[i].player;
					player.cardContainer._HandCards.Add(HandCardData.GetTutorialHandCardData(10003));
					player.cardContainer._HandCards.Add(HandCardData.GetTutorialHandCardData(10004));
					HeroAttrEffect goldUpdate = characterManager.GetGoldUpdate(player.Id, 3);
					HeroAttrEffect cardUpdate = characterManager.GetCardUpdate(player.Id, player.cardContainer._CardInfos);
					updateHeroAttrS2C.EffectDatas.Add(goldUpdate);
					updateHeroAttrS2C.EffectDatas.Add(cardUpdate);
				}
			}
			await characterManager.CreateUpdateAttrData(updateHeroAttrS2C);
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().missionManager.OnRoundStart();
	}

	public void GMSetRound(int gmGP)
	{
		if (!HackerConfig.IsValid())
		{
			_round = gmGP;
		}
	}

	private void RefreshPKTarget(long actionPlayerId, long teamId, int standLandId)
	{
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		PKTargetIds = (from x in playerDatas
			where x.player.Id != actionPlayerId && x.player.TeamId != teamId && x.Property.HP.Value > 0 && x.CharacterInst != null && x.CharacterInst.standLand.Id == standLandId
			select x.player.Id).ToList();
	}

	public async UniTask DealEncounterWithPlayer(long actionPlayerId)
	{
		if (PKTargetIds == null || PKTargetIds.Count == 0)
		{
			TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.MoveStop).Forget();
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(actionPlayerId);
		long num = PKTargetIds[0];
		PKTargetIds.RemoveAt(0);
		if (playerDataById.characterType == CharacterType.Monster)
		{
			int id = playerDataById.player.characterConfig.Id;
			TutorialBaseMonster monsterById = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetMonsterById(id);
			if (monsterById != null)
			{
				await monsterById.StartEncounterEvent(actionPlayerId, num);
			}
		}
		else
		{
			bool flag = true;
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial)
			{
				bool flag2 = !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(actionPlayerId);
				flag = !(mapGimmickManager_Tutorial.BossData.Property.HP.Value < 5 && flag2);
			}
			if (flag)
			{
				await StartPK(actionPlayerId, num);
			}
		}
		TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.MoveStop).Forget();
	}

	public async UniTask StartPK(long actionPlayerId, long targetId)
	{
		PKTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.AskFight(actionPlayerId, targetId, fightBack: false);
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(PKTsc);
	}

	public void FinishPK()
	{
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelTask(PKTsc);
	}

	public int GetPKAttackPoint(long attackerId, long defenderId)
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			return mapGimmickManager_Tutorial.GetPKAttackPoint(attackerId, defenderId);
		}
		return UnityEngine.Random.Range(1, 7);
	}

	public int GetPKDefendPoint(long attackerId, long defenderId)
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			return mapGimmickManager_Tutorial.GetPKDefendPoint(attackerId, defenderId);
		}
		return UnityEngine.Random.Range(1, 7);
	}

	public bool SuggestStop(int bornId, UnitLand land, int remainingPoint)
	{
		if (land.Id == bornId)
		{
			return true;
		}
		if (land.LandType == LandType.Pveshop)
		{
			return true;
		}
		if (land.LandType == LandType.FillingStation)
		{
			return true;
		}
		if (remainingPoint <= 0 && land.LandType == LandType.Born)
		{
			return true;
		}
		return false;
	}

	public bool IsChooseDir(UnitLand standLand, int fromLandId)
	{
		List<int> adjacencyLandIds = standLand.AdjacencyLandIds;
		if (!adjacencyLandIds.Remove(fromLandId))
		{
			Debug.LogError($"节点{fromLandId}与{standLand.Id} 不是邻接节点");
			return false;
		}
		if (adjacencyLandIds.Count == 0)
		{
			Debug.LogError($"节点{standLand.Id} 是绝路");
			return false;
		}
		if (adjacencyLandIds.Count > 1)
		{
			if (adjacencyLandIds.Contains(SelectDirLandId))
			{
				return false;
			}
			Debug.Log($"节点{standLand.Id} 是分叉路口");
			return true;
		}
		return false;
	}

	public int GenerateDicePoint(long playerId)
	{
		_movePoint = SpecifyDicePoint;
		if (_movePoint == 0)
		{
			int minInclusive = 1;
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
			{
				minInclusive = 6;
			}
			_movePoint = UnityEngine.Random.Range(minInclusive, 11);
		}
		SpecifyDicePoint = 0;
		return _movePoint;
	}

	public void AddBunsPoint(int bunsPoint, long buffUID)
	{
		moveAdditionAttribute.Add(new AdditionAttribute
		{
			MovePoint = bunsPoint,
			UniqueId = buffUID
		});
	}

	public void UpdateMovePoint(int point)
	{
		_movePoint = point;
	}

	public List<int> GenerateMovePath(long playerId)
	{
		if (!CanMove(playerId))
		{
			return null;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		List<int> list = new List<int>();
		UnitLand unitLand = playerDataById.CharacterInst.standLand;
		int num = playerDataById.CharacterInst.fromLandId;
		int movePoint = _movePoint;
		for (int i = 0; i < movePoint; i++)
		{
			List<int> adjacencyLandIds = unitLand.AdjacencyLandIds;
			if (!adjacencyLandIds.Remove(num))
			{
				Debug.LogError($"节点{num}与{unitLand.Id} 不是邻接节点");
				return list;
			}
			if (adjacencyLandIds.Count == 0)
			{
				Debug.LogError($"节点{unitLand.Id} 是绝路");
				return list;
			}
			int num2 = adjacencyLandIds[0];
			if (adjacencyLandIds.Contains(SelectDirLandId))
			{
				num2 = SelectDirLandId;
				SelectDirLandId = -1;
			}
			else if (adjacencyLandIds.Count > 1)
			{
				Debug.Log($"节点{unitLand.Id} 是分叉路口");
				return list;
			}
			_movePoint--;
			list.Add(num2);
			num = unitLand.Id;
			unitLand = SimpleSingletonProvider<LandManager>.inst.GetLandById(num2);
			RefreshPKTarget(playerDataById.player.Id, playerDataById.player.TeamId, unitLand.Id);
			List<long> pKTargetIds = PKTargetIds;
			if (pKTargetIds != null && pKTargetIds.Count > 0)
			{
				return list;
			}
			int beBornNodeId = playerDataById.player.serverPlayer.Hero.BeBornNodeId;
			if (SuggestStop(beBornNodeId, unitLand, _movePoint - list.Count))
			{
				return list;
			}
		}
		return list;
	}

	public void StopMove()
	{
		_movePoint = 0;
	}

	public bool CanMove(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null)
		{
			return false;
		}
		if (playerDataById.CharacterInst == null)
		{
			return false;
		}
		if (playerDataById.Property.HP.Value <= 0)
		{
			return false;
		}
		if (_movePoint <= 0)
		{
			return false;
		}
		return true;
	}
}
