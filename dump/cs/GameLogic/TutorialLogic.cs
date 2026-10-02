using System.Collections.Generic;
using Core;
using Core.Audio;
using Core.Net;
using Core.Scene;
using Core.Tutorial;
using Core.Tutorial.SceneConfig;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class TutorialLogic : IRPCSync
{
	public readonly int[] HeroPool = new int[23]
	{
		102, 105, 107, 103, 104, 115, 116, 101, 106, 109,
		110, 112, 113, 117, 118, 119, 120, 121, 122, 123,
		124, 125, 126
	};

	private TutorialSceneConfig _tutorialSceneConfig;

	private long attackPlayerId;

	private long defendPlayerId;

	public bool IsNovice
	{
		get
		{
			if (GMConfig._Enable && !GMConfig.Tutorial)
			{
				if (GetTutorialStatus())
				{
					RequestTeachingC2S();
				}
				return false;
			}
			return GetTutorialStatus();
		}
	}

	private bool IsNovice1001
	{
		get
		{
			if (GMConfig._Enable && !GMConfig.Tutorial1001)
			{
				return false;
			}
			return TutorialStatus(GuideType.Tutorial1001);
		}
	}

	private bool IsNovice1002
	{
		get
		{
			if (GMConfig._Enable && !GMConfig.Tutorial1002)
			{
				return false;
			}
			return TutorialStatus(GuideType.Tutorial1002);
		}
	}

	private bool IsNoviceSingle
	{
		get
		{
			if (GMConfig._Enable && !GMConfig.TutorialSingle)
			{
				return false;
			}
			return TutorialStatus(GuideType.TutorialSinglePlayer);
		}
	}

	public bool IsReturnNovice(RoomInfo localRoom)
	{
		if (localRoom == null)
		{
			return false;
		}
		if (localRoom.IsNovice())
		{
			TutorialSceneConfig tutorialSceneConfig = _tutorialSceneConfig;
			if (tutorialSceneConfig != null && tutorialSceneConfig.SourceSystemType == UIPanelType.Campaign)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public bool IsCampaignTutorial(RoomInfo localRoom)
	{
		if (localRoom == null || !localRoom.IsNovice())
		{
			return false;
		}
		TutorialSceneConfig tutorialSceneConfig = _tutorialSceneConfig;
		if (tutorialSceneConfig != null && tutorialSceneConfig.SourceSystemType == UIPanelType.Campaign)
		{
			return true;
		}
		return false;
	}

	private bool GetTutorialStatus()
	{
		TaskLogic task = SimpleSingletonProvider<GameLogicManager>.inst.task;
		if (task == null)
		{
			return false;
		}
		return task.GetAchieveInfo_1(1) == 0;
	}

	private bool TutorialStatus(GuideType type)
	{
		if (IsNovice)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.account.GetGuidedRecord((int)type) == 0;
		}
		return false;
	}

	public async UniTask StartTutorial()
	{
		if (IsNovice1001)
		{
			await StartTutorialScene1001();
		}
		else if (IsNovice1002)
		{
			await StartTutorialScene1002();
		}
		else if (IsNoviceSingle)
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowFinishTip();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room?.roomController?.ClearRoom();
			RequestTeachingC2S();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
		}
	}

	public void FinishTutorial(int lastStepId)
	{
		SimpleSingletonProvider<WebServerManager>.inst.PostTutorialRecord(lastStepId);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(10011, 1);
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestTeachingC2S();
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TeachingS2C.OnTeachingS2CServerCallBackAsync = OnTeachingS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TeachingS2C.OnTeachingS2CServerCallBackAsync = null;
	}

	public void RequestTeachingC2S()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TeachingC2S.TeachingC2SCall(new TeachingC2S());
	}

	private async UniTask OnTeachingS2CServerCallBack(TeachingS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public async UniTask StartTutorialById(int id)
	{
		switch (id)
		{
		case 1001:
			await StartTutorialScene1001();
			break;
		case 1002:
			await StartTutorialScene1002();
			break;
		}
	}

	public async UniTask StartTutorialScene1001()
	{
		BGMHelper.TryPlayBGM(102);
		SimpleSingletonProvider<UIManager>.inst.TryShowBackground().Forget();
		SimpleSingletonProvider<GameLogicManager>.inst.room.roomController?.ClearRoom();
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_HERO_GUIDE);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(1001);
		var (selectHeroId, skinItemId) = await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSelectHero();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login)
		{
			_tutorialSceneConfig = new TutorialSceneConfig_1001(selectHeroId, skinItemId);
			SimpleSingletonProvider<GameLogicManager>.inst.room.CreateGuidanceRoom(_tutorialSceneConfig.info);
			await SimpleSingletonProvider<GameLogicManager>.inst.room.LoadGuidanceScene();
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001 mapGimmickManager_Tutorial)
			{
				await mapGimmickManager_Tutorial.InitScene(_tutorialSceneConfig);
			}
		}
	}

	public async UniTask StartTutorialScene1002()
	{
		BGMHelper.TryPlayBGM(102);
		SimpleSingletonProvider<UIManager>.inst.TryShowBackground().Forget();
		SimpleSingletonProvider<GameLogicManager>.inst.room.roomController?.ClearRoom();
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_HERO_GUIDE);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowTutorial(20001);
		var (selectHeroId, skinItemId) = await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSelectHero();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login)
		{
			_tutorialSceneConfig = new TutorialSceneConfig_1002(selectHeroId, skinItemId);
			SimpleSingletonProvider<GameLogicManager>.inst.room.CreateGuidanceRoom(_tutorialSceneConfig.info);
			await SimpleSingletonProvider<GameLogicManager>.inst.room.LoadGuidanceScene();
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial)
			{
				await mapGimmickManager_Tutorial.InitScene(_tutorialSceneConfig);
			}
		}
	}

	public async UniTask TryStartTutorialSkill()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null || curRoomInfo.MapType != 6 || curRoomInfo.Round != 1)
		{
			return;
		}
		bool campaignPass = SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetCampaignPass(curRoomInfo.MapId);
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.GetGuidedRecord(100111) == 0;
		if (!campaignPass && flag)
		{
			await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(180);
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<SceneManager>.inst.loadingScene.Value.stateType == SceneStateType.End)
			{
				await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowOpenBattleInfoMask();
				await SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.ShowBattleInfoByTutorial();
				await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowOpenSkillMask();
				SimpleSingletonProvider<UIManager>.inst.tutorial.Hide();
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(100111, 1);
			}
		}
	}

	public void TryCloseCampaignTutorial()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && curRoomInfo.MapType == 6 && curRoomInfo.Round == 1)
		{
			SimpleSingletonProvider<UIManager>.inst.tutorial.Hide();
		}
	}

	public void RequestThrowDiceC2S(long playerId)
	{
		TutorialPlayerActionFSM system = TutorialGame.GetSystem<TutorialPlayerActionFSM>();
		if (system.PlayerId != playerId)
		{
			Debug.LogError($"当前玩家{playerId} 不是正在行动的玩家{system.PlayerId}，不能进行投点");
		}
		else
		{
			system.SwitchState(PlayerActionType.ThrowDice).Forget();
		}
	}

	public void RequestPVEShopBuyC2S(long _sn, List<int> indexList, long assistPlayer, bool isClose = false)
	{
		if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(LandType.Pveshop) is TutorialLandPveshop tutorialLandPveshop)
		{
			tutorialLandPveshop.RequestPVEShopBuyC2S(_sn, indexList, assistPlayer, isClose);
		}
	}

	public void RequestMove(int landId)
	{
		BattlePlayerData actionPlayerData = TutorialGame.GetSystem<TutorialPlayerActionFSM>().GetActionPlayerData();
		if (actionPlayerData?.CharacterInst != null)
		{
			actionPlayerData.CharacterInst.StopThinkEffect();
		}
		TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SelectDirLandId = landId;
		TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Moving).Forget();
	}

	public void RequestStopOrContinueC2S(bool stop)
	{
		BattlePlayerData actionPlayerData = TutorialGame.GetSystem<TutorialPlayerActionFSM>().GetActionPlayerData();
		if (actionPlayerData != null)
		{
			LandType landType = actionPlayerData.CharacterInst.standLand.LandType;
			if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(landType) is TutorialLandFillingStation tutorialLandFillingStation)
			{
				tutorialLandFillingStation.RequestStopOrContinueC2S(actionPlayerData.player.Id, stop).Forget();
			}
		}
		else
		{
			Debug.LogError("处理出生点或者加油站，未能取得当前行动角色");
		}
	}

	public void RequestMonsterPursuitC2S(long _sn, long _monsterId)
	{
		BattlePlayerData actionPlayerData = TutorialGame.GetSystem<TutorialPlayerActionFSM>().GetActionPlayerData();
		if (actionPlayerData != null)
		{
			LandType landType = actionPlayerData.CharacterInst.standLand.LandType;
			if (TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(landType) is TutorialLandMonsterPursuit tutorialLandMonsterPursuit)
			{
				tutorialLandMonsterPursuit.RequestMonsterPursuitC2S(actionPlayerData.player.Id, _monsterId);
			}
		}
	}

	public int GetDefenderDeadPoint()
	{
		BattleFightData battleFightData = SimpleSingletonProvider<GameLogicManager>.inst.fight.battleFightData;
		if (battleFightData == null)
		{
			return 1;
		}
		int atk = battleFightData.attackerInfo.Atk;
		int point = battleFightData.attackerInfo.Point;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(battleFightData.defenderInfo.PlayerId);
		if (playerDataById == null)
		{
			return 1;
		}
		int value = playerDataById.Property.HP.Value;
		int def = battleFightData.defenderInfo.Def;
		int num = atk + point - (value + def);
		if (num > 0)
		{
			num = Mathf.Clamp(num, 1, 6);
			return UnityEngine.Random.Range(1, num + 1);
		}
		return 1;
	}

	private bool IsSelf(long playerId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId);
	}

	private bool IsPKPlayer()
	{
		if (!IsSelf(attackPlayerId))
		{
			return IsSelf(defendPlayerId);
		}
		return true;
	}

	private bool IsPKAttacker()
	{
		return IsSelf(attackPlayerId);
	}

	private bool IsPKDefender()
	{
		return IsSelf(defendPlayerId);
	}

	public async UniTask AskFight(long actionPlayerId, long targetPlayerId, bool fightBack)
	{
		attackPlayerId = actionPlayerId;
		defendPlayerId = targetPlayerId;
		SimpleSingletonProvider<GameLogicManager>.inst.fight.fightType = FightType.FIGHT_NOTIFY;
		FightWindow fightWindow = await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
		int sn = UIDGenerator.NextUID();
		if (fightBack)
		{
			Debug.Log("<color=#00ff00>教程PK 开始反击PK");
			await RequestAskBattleC2S(sn, is_battle: true, isBackFight: true);
			return;
		}
		Debug.Log("<color=#00ff00>教程PK 发起PK");
		fightWindow.OpenChallengeWin(actionPlayerId, targetPlayerId, sn);
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(actionPlayerId).characterType == CharacterType.Monster)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(600);
		}
		if (!IsPKAttacker())
		{
			await RequestAskBattleC2S(sn, is_battle: true, isBackFight: false);
		}
	}

	public async UniTask RequestAskBattleC2S(long challengeActionSn, bool is_battle, bool isBackFight)
	{
		if (challengeActionSn == 0L)
		{
			Debug.LogError("AskBattleC2S 已经提交");
			return;
		}
		Debug.Log("<color=#00ff00>教程PK PK已经确认，构建PK战局");
		AskBattleS2C model = new AskBattleS2C
		{
			PlayerId = attackPlayerId,
			AskPlayerId = defendPlayerId,
			IsBattle = is_battle
		};
		await MonoSingletonProvider<NetManager>.inst.RPC.AskBattleS2C.OnAskBattleS2CServerCallBackAsync(model, 0, isDispatch: true);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackPlayerId);
		BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defendPlayerId);
		Battle battleData = new Battle
		{
			BattleId = UIDGenerator.NextUID(),
			Attacker = CreateBattleRole(playerDataById),
			Defender = CreateBattleRole(playerDataById2),
			FightBack = isBackFight
		};
		SimpleSingletonProvider<GameLogicManager>.inst.fight.UpdateFightData(battleData);
		await TutorialGame.GetSystem<TutorialBoardManager>().OnPKStart(attackPlayerId, defendPlayerId);
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleS2C.OnBattleS2CServerCallBackAsync(new BattleS2C
		{
			Battle = battleData
		}, 0, isDispatch: true);
		await DealFightUseCard();
	}

	private BattleRole CreateBattleRole(BattlePlayerData player)
	{
		int value = player.Property.ATK.Value;
		int value2 = player.Property.DEF.Value;
		int heroId = player.player.Hero.HeroId;
		return new BattleRole
		{
			PlayerId = player.player.Id,
			Atk = value,
			Def = value2,
			Cost = 3,
			MaxCost = 3,
			HeroId = heroId,
			MaxAtk = value,
			MinAtk = value,
			MaxDef = value2,
			MinDef = value2,
			InitAtk = value,
			InitDef = value2
		};
	}

	private async UniTask DealFightUseCard()
	{
		Debug.Log("<color=#00ff00>教程PK 处理PK卡牌的使用");
		if (!IsPKPlayer())
		{
			int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
			if (round <= 3)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackPlayerId);
				BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defendPlayerId);
				if (playerDataById.characterType == CharacterType.Hero)
				{
					await ReadyPlayerFightUseCard(attackPlayerId);
					await RequestBattleUseCardC2S(defendPlayerId, UIDGenerator.NextUID(), 0);
				}
				else
				{
					if (playerDataById2.characterType != CharacterType.Hero)
					{
						return;
					}
					if (round == 3 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial && playerDataById.player.Id == mapGimmickManager_Tutorial.BossData.player.Id && playerDataById2.player.Id == mapGimmickManager_Tutorial.NPC.player.Id)
					{
						BattleFightData battleFightData = SimpleSingletonProvider<GameLogicManager>.inst.fight.battleFightData;
						if (battleFightData != null && battleFightData.fightBack)
						{
							await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(160);
						}
					}
					await ReadyPlayerFightUseCard(defendPlayerId);
					await RequestBattleUseCardC2S(attackPlayerId, UIDGenerator.NextUID(), 0);
				}
			}
			else
			{
				await RequestBattleUseCardC2S(attackPlayerId, UIDGenerator.NextUID(), 0);
				await RequestBattleUseCardC2S(defendPlayerId, UIDGenerator.NextUID(), 0);
			}
		}
		else
		{
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			await ReadyPlayerFightUseCard(playerID);
			if (!IsPKAttacker())
			{
				await RequestBattleUseCardC2S(attackPlayerId, UIDGenerator.NextUID(), 0);
			}
			if (!IsPKDefender())
			{
				await RequestBattleUseCardC2S(defendPlayerId, UIDGenerator.NextUID(), 0);
			}
		}
	}

	private async UniTask ReadyPlayerFightUseCard(long playerId)
	{
		FightLogic fight = SimpleSingletonProvider<GameLogicManager>.inst.fight;
		FightWindow fightWindow = await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
		if (fight.fightType != FightType.FIGHT_CARD_NOTIFY)
		{
			fightWindow.InitPKCard(attackPlayerId);
			fight.fightType = FightType.FIGHT_CARD_NOTIFY;
		}
		if (!IsSelf(playerId))
		{
			int tutorialCost = ((playerId == attackPlayerId) ? fight.attackData.Cost : fight.defendData.Cost);
			fightWindow.TutorialRefreshPKCard(playerId, UIDGenerator.NextUID(), tutorialCost).Forget();
		}
		else
		{
			fightWindow.RefreshPKCard(playerId, UIDGenerator.NextUID());
		}
	}

	public async UniTask RequestBattleUseCardC2S(long playerId, long actionSn, int cardGuid)
	{
		if (actionSn == 0L)
		{
			Debug.LogError("BattleUseCardC2S 已经提交");
			return;
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleUseCardS2C.OnBattleUseCardS2CServerCallBackAsync(new BattleUseCardS2C
		{
			PlayerId = playerId,
			CardId = cardGuid
		}, 0, isDispatch: true);
		FightLogic fight = SimpleSingletonProvider<GameLogicManager>.inst.fight;
		BattleFightData fightData = fight.battleFightData;
		if (cardGuid == 0)
		{
			fightData.isFinishCard[playerId] = true;
		}
		if (playerId == attackPlayerId)
		{
			CalFightCardValue(fightData.attackerInfo, cardGuid);
		}
		else if (playerId == defendPlayerId)
		{
			CalFightCardValue(fightData.defenderInfo, cardGuid);
		}
		Battle battle = new Battle
		{
			BattleId = fightData.battleId,
			Attacker = fightData.attackerInfo,
			Defender = fightData.defenderInfo,
			CardUseState = { (IDictionary<long, bool>)fightData.isFinishCard },
			FightBack = fightData.fightBack
		};
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleS2C.OnBattleS2CServerCallBackAsync(new BattleS2C
		{
			Battle = battle
		}, 0, isDispatch: true);
		if (cardGuid != 0)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			List<HandCardData> handCards = playerDataById.cardContainer._HandCards;
			HandCardData handCardData = handCards.Find((HandCardData card) => card.Guid == cardGuid);
			if (handCards.Remove(handCardData))
			{
				HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(playerId, playerDataById.cardContainer._CardInfos);
				await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
				{
					Cause = new CauseOrigin
					{
						S = CauseOrigin.Types.source.Unknown,
						Id = handCardData.CardId
					},
					PlayerId = playerId,
					EffectDatas = { cardUpdate }
				});
			}
		}
		bool value;
		bool isFinishDefendCard = default(bool);
		if (IsSelf(playerId) && cardGuid != 0)
		{
			ReadyPlayerFightUseCard(playerId).Forget();
		}
		else if (fightData.isFinishCard.TryGetValue(attackPlayerId, out value) && value && fightData.isFinishCard.TryGetValue(defendPlayerId, out isFinishDefendCard) && isFinishDefendCard)
		{
			await DealPKPoint();
		}
	}

	private void CalFightCardValue(BattleRole role, int cardGuid)
	{
		if (cardGuid == 0)
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(role.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"CalFightCardValue 未找到玩家数据 PlayerId{role.PlayerId}");
			return;
		}
		HandCardData handCardData = playerDataById.cardContainer._HandCards.Find((HandCardData handCard) => handCard.Guid == cardGuid);
		if (handCardData == null)
		{
			Debug.LogError($"CalFightCardValue 未找到卡牌数据 CardGuid{cardGuid}");
			return;
		}
		int cardId = handCardData.CardId;
		if (!StaticConfigure.Card.InfoDict.TryGetValue(cardId, out var value))
		{
			return;
		}
		int safeByIndex = value.Params.GetSafeByIndex(0);
		int safeByIndex2 = value.Params.GetSafeByIndex(1);
		if (value.EffectType == EffectType.Attack)
		{
			role.MinAtk += safeByIndex;
			role.MaxAtk += safeByIndex2;
			int num = UnityEngine.Random.Range(safeByIndex, safeByIndex2 + 1);
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002)
			{
				bool num2 = !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(attackPlayerId);
				int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
				if (num2)
				{
					if (round == 1)
					{
						num = 3;
					}
					if (round == 3)
					{
						num = UnityEngine.Random.Range(1, 3);
					}
				}
			}
			role.Atk += num;
			role.CardCombatBonus.Add(new cardCombat
			{
				CardBonus = num,
				CardId = cardId
			});
		}
		else if (value.EffectType == EffectType.Defense)
		{
			role.MinDef += safeByIndex;
			role.MaxDef += safeByIndex2;
			int num3 = UnityEngine.Random.Range(safeByIndex, safeByIndex2 + 1);
			role.Def += num3;
			role.CardCombatBonus.Add(new cardCombat
			{
				CardBonus = num3,
				CardId = cardId
			});
		}
		role.Cost -= value.Cost;
		role.UseCards.Add(handCardData.Guid);
	}

	private async UniTask DealPKPoint()
	{
		FightLogic fight = SimpleSingletonProvider<GameLogicManager>.inst.fight;
		await fight.ReadyFightThrowDice(new Action
		{
			Sn = UIDGenerator.NextUID(),
			PlayerId = 0L
		});
		int pKAttackPoint = TutorialGame.GetSystem<TutorialBoardManager>().gameManager.GetPKAttackPoint(attackPlayerId, defendPlayerId);
		fight.battleFightData.attackerInfo.Point = pKAttackPoint;
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleThrowDiceS2C.OnBattleThrowDiceS2CServerCallBackAsync(new BattleThrowDiceS2C
		{
			PlayerId = attackPlayerId,
			Val = pKAttackPoint
		}, 0, isDispatch: true);
		if (IsPKDefender())
		{
			(await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin()).RefreshDefendReadyChoice(UIDGenerator.NextUID(), defendPlayerId, _NoDodge: false);
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defendPlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round > 3 || playerDataById.characterType != CharacterType.Hero)
		{
			await RequestBattleChoiceC2S(UIDGenerator.NextUID(), dodge: false);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.Fight.TutorialBattleChoice(attackPlayerId, defendPlayerId, fight.battleFightData.fightBack);
		}
	}

	public async UniTask RequestBattleChoiceC2S(long actionSn, bool dodge)
	{
		if (actionSn == 0L)
		{
			Debug.LogError("BattleChoiceC2S 已经提交");
			return;
		}
		FightLogic fight = SimpleSingletonProvider<GameLogicManager>.inst.fight;
		TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
		int pKDefendPoint = TutorialGame.GetSystem<TutorialBoardManager>().gameManager.GetPKDefendPoint(attackPlayerId, defendPlayerId);
		int point = fight.battleFightData.attackerInfo.Point;
		fight.battleFightData.defenderInfo.Point = pKDefendPoint;
		HeroAttrEffect defendHPAttrData = null;
		if (dodge)
		{
			fight.battleFightData.defenderInfo.Dodge = true;
			if (point >= pKDefendPoint && pKDefendPoint != 6)
			{
				fight.battleFightData.attackerInfo.Atk += point;
				fight.battleFightData.defenderInfo.Def = 0;
				defendHPAttrData = characterManager.GetHpUpdate(defendPlayerId, -fight.battleFightData.attackerInfo.Atk);
				fight.battleFightData.defenderInfo.IncHp = defendHPAttrData.Hp.ChangeHp;
			}
			else
			{
				fight.battleFightData.defenderInfo.IncHp = 0;
			}
		}
		else
		{
			fight.battleFightData.defenderInfo.Dodge = false;
			fight.battleFightData.attackerInfo.Atk += point;
			fight.battleFightData.defenderInfo.Def += pKDefendPoint;
			int b = fight.battleFightData.defenderInfo.Def - fight.battleFightData.attackerInfo.Atk;
			defendHPAttrData = characterManager.GetHpUpdate(defendPlayerId, Mathf.Min(-1, b));
			fight.battleFightData.defenderInfo.IncHp = defendHPAttrData.Hp.ChangeHp;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defendPlayerId);
		bool defenderDead = fight.battleFightData.defenderInfo.IncHp + playerDataById.Property.HP.Value <= 0;
		if (defenderDead)
		{
			if (playerDataById.characterType == CharacterType.Monster)
			{
				fight.battleFightData.defenderInfo.DropGold = -playerDataById.Property.gold.Value;
			}
			fight.battleFightData.fightBack = false;
		}
		else
		{
			fight.battleFightData.fightBack = playerDataById.player.characterConfig.CanCounter;
		}
		Battle battleData = new Battle
		{
			BattleId = fight.battleFightData.battleId,
			Attacker = fight.battleFightData.attackerInfo,
			Defender = fight.battleFightData.defenderInfo,
			IsEnd = true,
			FightBack = fight.battleFightData.fightBack
		};
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleChoiceS2C.OnBattleChoiceS2CServerCallBackAsync(new BattleChoiceS2C
		{
			Val = pKDefendPoint,
			Dodge = dodge,
			PlayerId = defendPlayerId,
			ExistFightBack = battleData.FightBack
		}, 0, isDispatch: true);
		await MonoSingletonProvider<NetManager>.inst.RPC.BattleS2C.OnBattleS2CServerCallBackAsync(new BattleS2C
		{
			Battle = battleData
		}, 0, isDispatch: true);
		if (defendHPAttrData != null)
		{
			await characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin(),
				EffectDatas = { defendHPAttrData },
				PlayerId = defendPlayerId
			});
		}
		if (defenderDead && fight.battleFightData.defenderInfo.DropGold < 0)
		{
			HeroAttrEffect goldUpdate = characterManager.GetGoldUpdate(attackPlayerId, -fight.battleFightData.defenderInfo.DropGold);
			await characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin(),
				EffectDatas = { goldUpdate },
				PlayerId = attackPlayerId
			});
		}
		if (battleData.FightBack)
		{
			AskFight(defendPlayerId, attackPlayerId, fightBack: true).Forget();
			return;
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().OnPKEnd(attackPlayerId, defendPlayerId);
		TutorialGame.GetSystem<TutorialBoardManager>().gameManager.FinishPK();
	}
}
