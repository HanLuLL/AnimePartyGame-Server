using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Tutorial;
using Core.Tutorial.SceneConfig;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Unit;

public sealed class MapGimmickManager_Tutorial1001 : MapGimmickManager_Tutorial
{
	private TutorialSceneConfig_1001 TutorialSceneConfig;

	private const int NPCHeroId = 108;

	public BattlePlayerData NPC;

	public override void Initialize()
	{
	}

	public override async UniTask InitScene(TutorialSceneConfig config)
	{
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: true);
		TutorialSceneConfig = config as TutorialSceneConfig_1001;
		await base.InitScene(config);
		NPC = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByHeroId(108);
		SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(NPC.CharacterInst);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10001);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowBattleLabel();
		UnitLand land = SimpleSingletonProvider<LandManager>.inst.GetLandById(53);
		if (land != null)
		{
			SimpleSingletonProvider<CameraManager>.inst.ControlFreeCamera(land.transform.position);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10002);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowFillingStation(land);
		}
		SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(NPC.CharacterInst);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10003);
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: false);
		TutorialGame.GetSystem<GamePlayManager>().Run().Forget();
	}

	public override async UniTask TutorialEnd()
	{
		await SimpleSingletonProvider<UIManager>.inst.upgradeWindow.ShowPVETip(isWin: true);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1001, 1);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10010);
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_HERO_GUIDE);
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home");
		if (!MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			MonoSingletonProvider<NetManager>.inst.HandleReConnect();
		}
	}

	public override async UniTask StartRound(long playerId)
	{
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		if (round == 1)
		{
			await StartRound_First(playerId);
		}
		if (round == 2)
		{
			await StartRound_Second(playerId);
		}
		if (round == 3)
		{
			await StartRound_Third(playerId);
		}
		if (round == 4)
		{
			await StartRound_Forth(playerId);
		}
	}

	public override async UniTask StopRound(long playerId)
	{
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		if (round == 1)
		{
			await StopRound_First(playerId);
		}
		if (round == 2)
		{
			await StopRound_Second(playerId);
		}
		if (round == 3)
		{
			await StopRound_Third(playerId);
		}
		if (round == 4)
		{
			await StopRound_Forth(playerId);
		}
	}

	public override int GetPKAttackPoint(long attackerId, long defenderId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 3)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackerId))
			{
				return 6;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerId).player.Hero.HeroId == 108)
			{
				return 5;
			}
		}
		return base.GetPKAttackPoint(attackerId, defenderId);
	}

	public override int GetPKDefendPoint(long attackerId, long defenderId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 3)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackerId))
			{
				return 1;
			}
			return 5;
		}
		return base.GetPKDefendPoint(attackerId, defenderId);
	}

	private async UniTask StartRound_First(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 2;
		}
		else
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById.player.Hero.HeroId == 108)
			{
				TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 2;
			}
			else
			{
				Debug.LogError($"场景1001只有配置角色：108和1001，请检查{playerDataById.player.Hero.HeroId}");
			}
		}
		await UniTask.CompletedTask;
	}

	private async UniTask StopRound_First(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId);
		int randomIndex = (flag ? 4 : 3);
		int tutorialId = (flag ? 10005 : 10004);
		UnitLand standLand = playerDataById.CharacterInst.standLand;
		BaseTutorialLand land = TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType);
		if (land is TutorialLandRollGold tutorialLandRollGold)
		{
			tutorialLandRollGold.RandomIndex = randomIndex;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(110);
		}
		await land.Stay(standLand.Id, playerId);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(tutorialId);
	}

	private async UniTask StartRound_Second(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 3;
		}
		else
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById.player.Hero.HeroId == 108)
			{
				TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 4;
			}
			else if (playerDataById.player.Hero.HeroId == 1001)
			{
				if (playerDataById.player.Hero.MonsterIndex == 1)
				{
					TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 2;
				}
				else if (playerDataById.player.Hero.MonsterIndex == 2)
				{
					TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 5;
				}
			}
			else
			{
				Debug.LogError($"场景1001只有配置角色：108和1001，请检查{playerDataById.player.Hero.HeroId}");
			}
		}
		await UniTask.CompletedTask;
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
			if (standLand2.LandType != LandType.Event)
			{
				Debug.LogError($"场景：1001, 回合：1，当前站立的地图格：{standLand2.Id} 不是Events");
			}
			else if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand2.LandType) is TutorialLandEvent tutorialLandEvent)
			{
				tutorialLandEvent.SpecifyEventId = 30203;
				await tutorialLandEvent.Stay(standLand2.Id, playerId);
			}
		}
	}

	private async UniTask StartRound_Third(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 4;
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.player.Hero.HeroId == 108)
		{
			int specifyCardId = 21004;
			if (playerDataById.cardContainer._HandCards.Find((HandCardData card) => card.CardId == specifyCardId) != null)
			{
				await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10006);
				await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[specifyCardId].TutorialCardEffect(playerId, new UseEffectCardC2S
				{
					CardId = specifyCardId
				});
			}
			else
			{
				TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 6;
			}
		}
		else if (playerDataById.player.Hero.HeroId == 1001)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 1;
		}
		else
		{
			Debug.LogError($"场景1001只有配置角色：108和1001，请检查{playerDataById.player.Hero.HeroId}");
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
			if (standLand2.LandType != LandType.Event)
			{
				Debug.LogError($"场景1001, 回合：1，当前站立的地图格：{standLand2.Id} 不是Events");
			}
			else if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand2.LandType) is TutorialLandEvent tutorialLandEvent)
			{
				tutorialLandEvent.SpecifyEventId = 30014;
				await tutorialLandEvent.Stay(standLand2.Id, playerId);
			}
		}
	}

	private async UniTask StartRound_Forth(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 10;
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.player.Hero.HeroId == 108)
		{
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = 2;
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10008);
		}
		else if (playerDataById.player.Hero.HeroId != 1001)
		{
			Debug.LogError($"场景1001只有配置角色：108和1001，请检查{playerDataById.player.Hero.HeroId}");
		}
	}

	private async UniTask StopRound_Forth(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			UnitLand standLand = playerDataById.CharacterInst.standLand;
			await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType).Stay(standLand.Id, playerId);
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SetTutorialStatus(TutorialStatus.Success);
		}
		else if (playerDataById.player.Hero.HeroId == 108)
		{
			UnitLand standLand2 = playerDataById.CharacterInst.standLand;
			await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand2.LandType).Stay(standLand2.Id, playerId);
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(10009);
		}
	}

	public async UniTask CreateLittleRaccoon()
	{
		Player player = TutorialSceneConfig.BuildRaccoonServerPlayer(41, 42);
		player.Hero.MonsterIndex = 1;
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = player
		}, 0, isDispatch: true);
		Player player2 = TutorialSceneConfig.BuildRaccoonServerPlayer(1, 2);
		player2.Hero.MonsterIndex = 2;
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = player2
		}, 0, isDispatch: true);
	}

	public override async UniTask DealChooseDir(long playerId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) && SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).player.Hero.HeroId == 108)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMove(48);
		}
		await UniTask.CompletedTask;
	}
}
