using System.Collections.Generic;
using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Tutorial;
using Core.Tutorial.SceneConfig;
using Core.Tutorial.Tools;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Unit;

public sealed class MapGimmickManager_Tutorial1002 : MapGimmickManager_Tutorial
{
	public TutorialSceneConfig_1002 TutorialSceneConfig;

	public BattlePlayerData BossData;

	private const int NPCHeroId = 108;

	public BattlePlayerData NPC;

	private readonly int[] FirstRoundDisable = new int[2] { 29, 7 };

	private readonly int[] SecondRoundDisable = new int[2] { 13, 20 };

	public override void Initialize()
	{
	}

	public override async UniTask InitScene(TutorialSceneConfig config)
	{
		TutorialSceneConfig = config as TutorialSceneConfig_1002;
		await base.InitScene(config);
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: true);
		Player monster1004 = TutorialSceneConfig.BuildMonsterServerPlayer1004();
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster1004
		}, 0, isDispatch: true);
		BossData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(monster1004.Id);
		if (BossData.CharacterInst != null)
		{
			BossData.CharacterInst.window?.Com_PlayerAttrInfo?.AddBossLabel();
		}
		Player monster1005 = TutorialSceneConfig.BuildMonsterServerPlayer1002(1, 2, 3);
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster1005
		}, 0, isDispatch: true);
		NPC = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByHeroId(108);
		SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(NPC.CharacterInst);
		TutorialGame.GetSystem<GamePlayManager>().Run().Forget();
	}

	public override async UniTask TutorialEnd()
	{
		TutorialStatus tutorialStatus = TutorialGame.GetSystem<TutorialBoardManager>().gameManager.TutorialStatus;
		bool isWin = tutorialStatus == TutorialStatus.Success;
		if (isWin)
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20008);
		}
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_HERO_GUIDE);
		await SimpleSingletonProvider<UIManager>.inst.upgradeWindow.ShowPVETip(isWin);
		if (tutorialStatus == TutorialStatus.Success)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1002, 1);
		}
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home");
		if (!MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			MonoSingletonProvider<NetManager>.inst.HandleReConnect();
		}
	}

	public override async UniTask StartRound(long playerId)
	{
		switch (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round)
		{
		case 1:
			await StartRound_First(playerId);
			break;
		case 2:
			await StartRound_Second(playerId);
			break;
		case 3:
			await StartRound_Third(playerId);
			break;
		}
	}

	public override async UniTask StopRound(long playerId)
	{
		switch (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round)
		{
		case 1:
			await StopRound_First(playerId);
			break;
		case 2:
			await StopRound_Second(playerId);
			break;
		case 3:
			await StopRound_Third(playerId);
			break;
		default:
		{
			UnitLand standLand = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).CharacterInst.standLand;
			await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType).Stay(standLand.Id, playerId);
			break;
		}
		}
	}

	public override int GetPKAttackPoint(long attackerId, long defenderId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerId);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defenderId);
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		switch (round)
		{
		case 1:
			return 6;
		case 2:
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackerId))
			{
				return UnityEngine.Random.Range(5, 7);
			}
			if (playerDataById.player.Hero.HeroId == BossData.player.Hero.HeroId)
			{
				return 5;
			}
			if (playerDataById.player.Hero.HeroId == 1005)
			{
				return UnityEngine.Random.Range(1, 7);
			}
			break;
		}
		if (round == 3)
		{
			if (playerDataById.player.Hero.HeroId == 108)
			{
				return 2;
			}
			if (playerDataById.player.Hero.HeroId == BossData.player.Hero.HeroId)
			{
				return 5;
			}
			if (playerDataById.player.Hero.HeroId == 1005)
			{
				return 6;
			}
		}
		return base.GetPKAttackPoint(attackerId, defenderId);
	}

	public override int GetPKDefendPoint(long attackerId, long defenderId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerId);
		BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defenderId);
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		if (round == 1)
		{
			if (attackerId == NPC.player.Id)
			{
				return 3;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackerId))
			{
				return 1;
			}
		}
		if (round == 2)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackerId))
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.tutorial.GetDefenderDeadPoint();
			}
			if (playerDataById.player.Hero.HeroId == BossData.player.Hero.HeroId)
			{
				return 1;
			}
			if (playerDataById.player.Hero.HeroId == 1005)
			{
				BattleRole battleRole = SimpleSingletonProvider<GameLogicManager>.inst.fight?.attackData;
				if (battleRole == null)
				{
					return 6;
				}
				return UnityEngine.Random.Range(Mathf.Min(battleRole.Point + 1, 6), 7);
			}
		}
		if (round == 3)
		{
			if (playerDataById.player.Hero.HeroId == 108)
			{
				return 3;
			}
			if (playerDataById.player.Hero.HeroId == BossData.player.Hero.HeroId)
			{
				return 6;
			}
			if (playerDataById.player.Hero.HeroId == 1005)
			{
				return 1;
			}
		}
		if (playerDataById2.characterType == CharacterType.Hero)
		{
			BattleRole battleRole2 = SimpleSingletonProvider<GameLogicManager>.inst.fight?.attackData;
			if (battleRole2 == null)
			{
				return 6;
			}
			return UnityEngine.Random.Range(Mathf.Min(battleRole2.Point + 1, 6), 7);
		}
		return base.GetPKDefendPoint(attackerId, defenderId);
	}

	public void DealGuideRoad(long playerId, bool disable)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			return;
		}
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		if (round != 1 && round != 2)
		{
			return;
		}
		int[] array = ((round == 1) ? FirstRoundDisable : SecondRoundDisable);
		for (int i = 0; i < array.Length; i++)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(array[i]);
			if (landById != null)
			{
				landById.Disable = disable;
			}
		}
	}

	private async UniTask StartRound_First(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 5;
			return;
		}
		BattlePlayerData player = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (player.player.Hero.HeroId != 108)
		{
			return;
		}
		TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 2;
		if (player.Property.cardUseTimes.Value == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20002);
			SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(BossData.CharacterInst);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20003);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowPVEProgress();
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(23000);
			SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: false);
			SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(player.CharacterInst);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20004);
			int specifyCardId = 21002;
			if (player.cardContainer._HandCards.Find((HandCardData card) => card.CardId == specifyCardId) != null)
			{
				long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
				await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[specifyCardId].TutorialCardEffect(playerId, new UseEffectCardC2S
				{
					CardId = specifyCardId,
					TargetIds = { playerID }
				});
			}
		}
	}

	private async UniTask StopRound_First(long playerId)
	{
		UnitLand standLand = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).CharacterInst.standLand;
		BaseTutorialLand landByType = TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 4;
		}
		await landByType.Stay(standLand.Id, playerId);
	}

	private async UniTask StartRound_Second(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 3;
			return;
		}
		BattlePlayerData player = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (player.player.Hero.HeroId == 108)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 3;
			if (player.Property.cardUseTimes.Value == 0)
			{
				SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(player.CharacterInst);
				await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20006);
				int specifyCardId = 20008;
				if (player.cardContainer._HandCards.Find((HandCardData card) => card.CardId == specifyCardId) != null)
				{
					long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
					await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[specifyCardId].TutorialCardEffect(playerId, new UseEffectCardC2S
					{
						CardId = specifyCardId,
						TargetIds = { playerID }
					});
				}
			}
		}
		else if (player.player.Hero.HeroId == BossData.player.Hero.HeroId)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 7;
		}
		else if (player.player.Hero.HeroId == 1005)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 8;
		}
	}

	private async UniTask StopRound_Second(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			UnitLand standLand = playerDataById.CharacterInst.standLand;
			await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType).Stay(standLand.Id, playerId);
		}
		else if (playerDataById.player.Hero.HeroId == 108)
		{
			UnitLand standLand2 = playerDataById.CharacterInst.standLand;
			if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand2.LandType) is TutorialLandEvent tutorialLandEvent)
			{
				tutorialLandEvent.SpecifyEventId = 30019;
				await tutorialLandEvent.Stay(standLand2.Id, playerId);
			}
		}
	}

	private async UniTask StartRound_Third(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 3;
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.player.Hero.HeroId == 108)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 3;
			if (playerDataById.Property.cardUseTimes.Value == 0)
			{
				SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(playerDataById.CharacterInst);
				int specifyCardId = 21001;
				if (playerDataById.cardContainer._HandCards.Find((HandCardData card) => card.CardId == specifyCardId) != null)
				{
					await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[specifyCardId].TutorialCardEffect(playerId, new UseEffectCardC2S
					{
						CardId = specifyCardId,
						TargetIds = { BossData.player.Id }
					});
				}
			}
		}
		else if (playerDataById.player.Hero.HeroId == BossData.player.Hero.HeroId)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 8;
		}
		else if (playerDataById.player.Hero.HeroId == 1005)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 8;
		}
	}

	private async UniTask StopRound_Third(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			UnitLand standLand = playerDataById.CharacterInst.standLand;
			await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType).Stay(standLand.Id, playerId);
		}
		else if (playerDataById.player.Hero.HeroId == 108)
		{
			UnitLand standLand2 = playerDataById.CharacterInst.standLand;
			if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand2.LandType) is TutorialLandEvent tutorialLandEvent)
			{
				tutorialLandEvent.SpecifyEventId = 30019;
				await tutorialLandEvent.Stay(standLand2.Id, playerId);
			}
		}
	}

	public override async UniTask DealChooseDir(long playerId)
	{
		BattlePlayerData player = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		UnitLand standLand = player.CharacterInst.standLand;
		List<int> adjacencyLandIds = standLand.AdjacencyLandIds;
		int fromLandId = player.CharacterInst.fromLandId;
		adjacencyLandIds.Remove(fromLandId);
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			switch (round)
			{
			case 1:
				await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20005);
				await GuideSelectDir(adjacencyLandIds, player, standLand, 11);
				break;
			case 2:
				await GuideSelectDir(adjacencyLandIds, player, standLand, 15);
				break;
			default:
				await SimpleSingletonProvider<MoveArrowManager>.inst.DealMove(playerId, UIDGenerator.NextUID(), ForceDir: false);
				break;
			}
			return;
		}
		if (round == 2 && player.player.Hero.HeroId == BossData.player.Hero.HeroId)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMove(7);
			return;
		}
		if (round == 3 && player.player.Hero.HeroId == 108)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMove(7);
			return;
		}
		int num = 0;
		if (player.characterType == CharacterType.Monster)
		{
			int movePoint = TutorialGame.GetSystem<TutorialBoardManager>().gameManager.MovePoint;
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			HashSet<int> hashSet = new HashSet<int>
			{
				selfPlayerData.CharacterInst.standLand.Id,
				NPC.CharacterInst.standLand.Id
			};
			for (int i = 0; i < adjacencyLandIds.Count; i++)
			{
				int num2 = movePoint;
				int id = standLand.Id;
				int num3 = adjacencyLandIds[i];
				while (num2 > 0)
				{
					num2--;
					if (hashSet.Contains(num3))
					{
						num = adjacencyLandIds[i];
						break;
					}
					UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(num3);
					if (landById == null)
					{
						break;
					}
					List<int> adjacencyLandIds2 = landById.AdjacencyLandIds;
					adjacencyLandIds2.Remove(id);
					if (adjacencyLandIds2.Count != 1)
					{
						break;
					}
					id = landById.Id;
					num3 = adjacencyLandIds2[0];
				}
				if (num != 0)
				{
					break;
				}
			}
			if (num == 0)
			{
				num = adjacencyLandIds.GetSafeByIndex(UnityEngine.Random.Range(0, adjacencyLandIds.Count));
			}
		}
		else
		{
			num = adjacencyLandIds.GetSafeByIndex(UnityEngine.Random.Range(0, adjacencyLandIds.Count));
		}
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMove(num);
	}

	private static async UniTask GuideSelectDir(List<int> adjacencyLandIds, BattlePlayerData player, UnitLand standLand, int targetLandId)
	{
		adjacencyLandIds.Remove(targetLandId);
		for (int i = 0; i < adjacencyLandIds.Count; i++)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(adjacencyLandIds[i]);
			if (landById != null)
			{
				landById.Disable = true;
			}
		}
		await SimpleSingletonProvider<MoveArrowManager>.inst.DealTutorialMove(player, standLand.Id, targetLandId);
	}
}
