using System.Collections.Generic;
using Core;
using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

public class GuideLogic : IRPCSync
{
	private int guideActionSn;

	public int playerMovePoint;

	private int residuePoint;

	private readonly List<long> fightPlayers = new List<long>();

	private readonly List<int> pathNodes = new List<int>();

	private readonly RepeatedField<int> battleCardAtk = new RepeatedField<int>();

	private readonly GuideConfigData guideConfigData = new GuideConfigData();

	private GuideInfo runningGuideInfo;

	private GuideSceneData GuideRoomData;

	public bool NoviceStatus
	{
		get
		{
			if (GMConfig._Enable && !GMConfig.Tutorial)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1(1) == 0)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.guide.RequestTeachingC2S();
				}
				return false;
			}
			return SimpleSingletonProvider<GameLogicManager>.inst.task.GetAchieveInfo_1(1) == 0;
		}
	}

	public async void GuidanceCardResult(int cardId)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		long playerId = playerData.player.Id;
		CardInfoConfigure _cardConfig = cardId.GetCardConfigure();
		SimpleSingletonProvider<GameLogicManager>.inst.card.UpdateUseCardCount(cardId);
		await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[cardId].CardCallBack(playerId, new RepeatedField<long> { playerId }, reverse: false, 0);
		await SimpleSingletonProvider<GameLogicManager>.inst.action.CardShowAction(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = cardId
			},
			PlayerId = playerId,
			EffectDatas = { GetHpUpdate(playerId, _cardConfig.Params[0], playerData.player.characterConfig.Blood, playerData.player.characterConfig.Blood, _cardConfig.Params[0], playerData.player.characterConfig.Blood) }
		});
		await UpdateCards(playerId, cardId);
	}

	public async void GuidanceThrowDice(long playerId, int DicePoint)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(playerId);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.Property.HP.Value == 0)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetHpUpdate(playerId, playerDataById.player.characterConfig.Blood, 0, playerDataById.player.characterConfig.Blood, playerDataById.player.characterConfig.Blood, playerDataById.player.characterConfig.Blood) });
			DealNextPlayerMove(playerId);
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.ResetCurPlayerState(playerId);
		Character character = playerDataById.CharacterInst;
		character.ResetStep(DicePoint);
		await character.SwitchCamera();
		if (character.player.characterType == CharacterType.Hero)
		{
			await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(character, DicePoint);
		}
		if (!(await SimpleSingletonProvider<DiceManager>.inst.ThrowDice(playerId, new RepeatedField<int> { DicePoint }, DicePoint, controlled: false)))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.MOVE, playerId);
			GetPossiblePaths(playerId, character.standLand.Id, DicePoint, character.fromLandId, -1);
			await DealGuidanceMove(playerId, DicePoint);
		}
	}

	public async UniTask DealGuidanceMove(long playerId, int DicePoint)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(playerId);
		await GuidanceMove(playerId, pathNodes);
		residuePoint = DicePoint - pathNodes.Count;
		if (residuePoint != 0)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) && fightPlayers.Count > 0)
			{
				await DealOperateFight();
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			else if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
			{
				LandType landType = playerData.CharacterInst.standLand.LandType;
				if ((landType == LandType.Born || landType == LandType.FillingStation) && pathNodes.Count != 0)
				{
					OnBornAction(playerId);
				}
				else
				{
					TriggerGuide(100306);
				}
			}
			else
			{
				int nextLandId = playerData.CharacterInst.standLand.CanSelectedLandId(playerData.CharacterInst.fromLandId)[0];
				GetPossiblePaths(playerData.player.Id, playerData.CharacterInst.standLand.Id, residuePoint, playerData.CharacterInst.fromLandId, nextLandId);
				await DealGuidanceMove(playerData.player.Id, residuePoint);
			}
		}
		else if (playerData.CharacterInst.standLand.LandType == LandType.RollGold)
		{
			await OnRollGold(playerId, Mathf.RoundToInt(UnityEngine.Random.Range(2f, 4f)));
			FinishStepGuide();
		}
		else if (playerData.CharacterInst.standLand.LandType == LandType.Born)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
		else if (!(await GuidanceGameOver(playerId)))
		{
			DealNextPlayerMove(playerData.player.Id);
		}
	}

	public async UniTask GuidanceMove(long playerId, List<int> _NodeIds, bool _End = false)
	{
		SimpleSingletonProvider<MoveArrowManager>.inst.CloseArrow();
		Character character = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).CharacterInst;
		await character.SwitchCamera();
		Queue<int> queue = new Queue<int>();
		foreach (int _NodeId in _NodeIds)
		{
			queue.Enqueue(_NodeId);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(character, willMove: true);
		await character.ExecuteMove(queue, _End);
	}

	public async void GuidanceSelectDir(long actionSn, int targetLandId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
		GetPossiblePaths(playerDataById.player.Id, playerDataById.CharacterInst.standLand.Id, residuePoint, playerDataById.CharacterInst.fromLandId, targetLandId);
		await DealGuidanceMove(playerDataById.player.Id, residuePoint);
	}

	private void DealNextPlayerMove(long curPlayerId)
	{
		int num = (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(curPlayerId).CharacterInst.player.Slot + 1) % SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas.Count;
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		long id = curRoomInfo.GetPlayerBySlot(num).Id;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(id))
		{
			playerMovePoint = 10;
		}
		else
		{
			GuidanceThrowDice(id, 1);
		}
		if (num == 0)
		{
			curRoomInfo.UpdateRound(curRoomInfo.Round + 1);
		}
	}

	public async UniTask ContinueMove(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		GetPossiblePaths(playerDataById.player.Id, playerDataById.CharacterInst.standLand.Id, residuePoint, playerDataById.CharacterInst.fromLandId, -1);
		await DealGuidanceMove(playerDataById.player.Id, residuePoint);
	}

	private void GetPossiblePaths(long playerId, int curLandId, int step, int fromLand, int nextLandId)
	{
		fightPlayers.Clear();
		pathNodes.Clear();
		if (nextLandId != -1)
		{
			FindCurPath(playerId, pathNodes, nextLandId, curLandId, step);
			return;
		}
		List<int> list = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId).CanSelectedLandId(fromLand);
		if (list.Count <= 1)
		{
			FindCurPath(playerId, pathNodes, list[0], curLandId, step);
		}
	}

	private void FindCurPath(long playerId, List<int> currentPath, int curLandId, int preLandId, int step)
	{
		step--;
		currentPath.Add(curLandId);
		if (step == 0)
		{
			return;
		}
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId);
		if (landById.LandType == LandType.FillingStation || (landById.LandType == LandType.Born && SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).GetBornLandId() == curLandId))
		{
			return;
		}
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerId != playerData.player.Id && playerData.CharacterInst.standLand.Id == curLandId)
			{
				fightPlayers.Add(playerData.player.Id);
			}
		}
		if (fightPlayers.Count == 0)
		{
			List<int> list = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId).CanSelectedLandId(preLandId);
			if (list.Count == 1)
			{
				FindCurPath(playerId, currentPath, list[0], curLandId, step);
			}
		}
	}

	private async UniTask OnRollGold(long playerId, int index)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.landRollGold.ShowLand()).RefreshRollGoldDice(index);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		int num = StaticConfigure.Land.InfoDict[15].Params[index];
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetGoldUpdate(playerId, num, playerDataById.Property.gold.Value, num + playerDataById.Property.gold.Value) });
	}

	private async void OnBornAction(long playerId)
	{
		LandFillingStationWindow obj = await SimpleSingletonProvider<UIManager>.inst.landFillingStation.ShowLand();
		TriggerGuide(100307);
		obj.ShowStopOrContinueLand(playerId, guideActionSn++);
	}

	public async void SureMoveStop()
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		FinishStepGuide();
		SimpleSingletonProvider<UIManager>.inst.landFillingStation.HideImmediately();
		pathNodes.Clear();
		await GuidanceMove(playerData.player.Id, pathNodes, _End: true);
		await GuidanceGameOver(playerData.player.Id);
	}

	private async UniTask<bool> GuidanceGameOver(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		LandType landType = playerDataById.CharacterInst.standLand.LandType;
		if (landType == LandType.Born || landType == LandType.FillingStation)
		{
			int upgradePlan = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.UpgradePlan;
			UpgradeDataConfigureItem upgradeDataConfigureItem = StaticConfigure.Upgrade.DataDict[upgradePlan].UpgradeDataConfigureItems[2];
			if (playerDataById.Property.gold.Value > upgradeDataConfigureItem.Gold)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetLVUpdate(playerId, 3) });
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
				SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: true);
				FinishGame();
				return true;
			}
		}
		return false;
	}

	public async void FinishGame()
	{
		SimpleSingletonProvider<InternalAssetManager>.inst.Release();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.CloseBattleUI();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady = false;
		await SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home");
	}

	private async UniTask UpdateCards(long playerId, int cardId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		List<CardInfo> list = new List<CardInfo>();
		list.AddRange(playerDataById.cardContainer._CardInfos);
		list.Remove(list.Find((CardInfo card) => card.CardId == cardId));
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetCardUpdate(playerId, list) });
	}

	public async void CancelGuidanceFight(long playerId)
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.HideImmediately();
		await ContinueMove(playerId);
	}

	public async void SureGuidanceFight(long playerID)
	{
		battleCardAtk.Clear();
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerID);
		BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(fightPlayers[0]);
		UpdateFightData(new BattleRole
		{
			PlayerId = playerDataById.player.Id,
			Atk = playerDataById.Property.ATK.Value,
			Def = playerDataById.player.Hero.Defense,
			Point = 0,
			DropGold = Mathf.CeilToInt((float)playerDataById2.Property.gold.Value / 2f),
			Cost = 3
		}, isDodge: false, 0);
		await SimpleSingletonProvider<UIManager>.inst.Fight.ReadyFight(playerID, fightPlayers[0]);
		SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		SimpleSingletonProvider<UIManager>.inst.Fight.InitPKCard(playerID);
		SimpleSingletonProvider<UIManager>.inst.Fight.RefreshPKCard(playerID, guideActionSn++);
		BattleSceneController.inst.directorManager.ShowReadyLabel(fightPlayers[0]);
	}

	public async void GuidanceBattleCardUse(BattlePlayerData attackerData, BattlePlayerData defenderData, int cardId)
	{
		await UpdateCards(attackerData.player.Id, cardId);
		battleCardAtk.Add(cardId);
		UpdateFightData(new BattleRole
		{
			PlayerId = attackerData.player.Id,
			Atk = attackerData.Property.ATK.Value,
			Def = attackerData.player.Hero.Defense,
			Point = 0,
			DropGold = Mathf.CeilToInt((float)defenderData.Property.gold.Value / 2f),
			Cost = 3 - cardId.GetCardConfigure().Cost,
			UseCards = { (IEnumerable<int>)battleCardAtk }
		}, isDodge: false, 1);
		SimpleSingletonProvider<UIManager>.inst.Fight.RefreshCardWinData();
		SimpleSingletonProvider<UIManager>.inst.Fight.RefreshPKCard(attackerData.player.Id, guideActionSn++);
	}

	public async void GuidanceBattleCardFinish(BattlePlayerData attackerData, BattlePlayerData defenderData)
	{
		SimpleSingletonProvider<UIManager>.inst.guide.HideMask();
		BattleSceneController.inst.directorManager.ShowReadyLabel(attackerData.player.Id);
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		BattleSceneController.inst.directorManager.CloseReadyLabel();
		BattleSceneController.inst.directorManager.PlayDetachDirect();
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.ATK, attackerData.player.Id);
		int attackPoint = ((defenderData.player.Hero.HeroId == 101) ? 1 : 6);
		await BattleSceneController.inst.directorManager.attacker._UI.RefreshDice_Attacker(1, attackPoint);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.dodgeShow.Dispatch(attackPoint);
		SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
	}

	public async void GuidanceBattleAttackDice(BattlePlayerData attackerData, BattlePlayerData defenderData, int attackPoint, int defendPoint, bool isDodge)
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.ShowChoiceResult(isDodge);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.DEF, defenderData.player.Id);
		await BattleSceneController.inst.directorManager.RefreshDice_Defender(0, defendPoint, isDodge, counterAttack: false);
		BattleSceneController.inst.directorManager.PlayApproach();
		await BattleSceneController.inst.directorManager.attacker.PlayMove();
		int num = attackerData.Property.ATK.Value;
		for (int i = 0; i < battleCardAtk.Count; i++)
		{
			CardInfoConfigure cardConfigure = battleCardAtk[i].GetCardConfigure();
			num += cardConfigure.Params[1];
		}
		UpdateFightData(new BattleRole
		{
			PlayerId = attackerData.player.Id,
			Atk = num + attackPoint,
			Def = attackerData.player.Hero.Defense,
			Point = attackPoint,
			DropGold = (isDodge ? Mathf.CeilToInt((float)defenderData.Property.gold.Value / 2f) : 0)
		}, isDodge, defendPoint);
		await BattleSceneController.inst.directorManager.PlayBattleResult();
		SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetHpUpdate(defenderData.player.Id, defenderData.player.Hero.Hp, defenderData.player.Hero.Hp, 0, isDodge ? (-defenderData.player.Hero.Hp) : (-1), defenderData.player.Hero.MaxHp) });
		int changeGold = (isDodge ? Mathf.CeilToInt((float)defenderData.Property.gold.Value / 2f) : 0);
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetGoldUpdate(defenderData.player.Id, -changeGold, defenderData.Property.gold.Value, changeGold) });
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetGoldUpdate(attackerData.player.Id, changeGold, attackerData.Property.gold.Value, attackerData.Property.gold.Value + changeGold) });
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
		await ContinueMove(attackerData.player.Id);
	}

	public void UpdateFightData(BattleRole attacker, bool isDodge, int defendPoint)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(fightPlayers[0]);
		Battle battleInfo = new Battle
		{
			BattleId = playerDataById.player.Id,
			Attacker = attacker,
			Defender = new BattleRole
			{
				PlayerId = playerDataById.player.Id,
				Atk = playerDataById.player.Hero.Attack,
				Def = playerDataById.player.Hero.Defense + defendPoint,
				Point = defendPoint,
				Dodge = isDodge,
				IncHp = (isDodge ? (-playerDataById.player.Hero.Hp) : (-1)),
				Cost = 0
			},
			IsEnd = true,
			FightBack = false
		};
		SimpleSingletonProvider<GameLogicManager>.inst.fight.UpdateFightData(battleInfo);
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
			SimpleSingletonProvider<GameLogicManager>.inst.task.finishTeach = true;
			await UniTask.CompletedTask;
		}
	}

	public bool PVPGuideStatus(GuideType type)
	{
		if (NoviceStatus)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.account.GetGuidedRecord((int)type) == 0;
		}
		return false;
	}

	public bool GuideStatus(GuideType type)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.GetGuidedRecord((int)type) == 0;
	}

	public async UniTask<GuideType> HomePanelGuide()
	{
		bool GuideSceneB = PVPGuideStatus(GuideType.GuideSceneB);
		bool flag = PVPGuideStatus(GuideType.GuideStartGame);
		if (GuideSceneB || flag)
		{
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
			if (GuideSceneB)
			{
				return GuideType.GuideSceneB;
			}
			return GuideType.GuideStartGame;
		}
		return GuideType.None;
	}

	public void StartGuideHomePanel()
	{
		if (PVPGuideStatus(GuideType.GuideSceneB))
		{
			TriggerGuide(100400);
		}
		else if (PVPGuideStatus(GuideType.GuideStartGame))
		{
			TriggerGuide(100800);
		}
	}

	public void TriggerGuide(int guideId)
	{
		if (guideConfigData.GuideDict.TryGetValue(guideId, out var value))
		{
			runningGuideInfo = value;
			runningGuideInfo.StartGuide();
		}
	}

	public void TriggerNextGuide(int guideId)
	{
		RepeatedField<GuideInfoConfigure> infos = StaticConfigure.Guide.Infos;
		for (int i = 0; i < infos.Count; i++)
		{
			if (infos[i].GuideId == guideId && i + 1 < infos.Count)
			{
				TriggerGuide(infos[i + 1].GuideId);
				return;
			}
		}
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
		}
	}

	public async void LoadingGuideSceneA()
	{
		SimpleSingletonProvider<WebServerManager>.inst.PostGuideRecord(100300, 1);
		GuideRoomData = new GuideSceneData_A();
		SimpleSingletonProvider<GameLogicManager>.inst.room.CreateGuidanceRoom(GuideRoomData.info);
		await SimpleSingletonProvider<GameLogicManager>.inst.room.LoadGuidanceScene();
		TriggerGuide(100301);
	}

	public async void LoadingGuideSceneB()
	{
		GuideRoomData = new GuideSceneData_B();
		SimpleSingletonProvider<GameLogicManager>.inst.room.CreateGuidanceRoom(GuideRoomData.info);
		await SimpleSingletonProvider<GameLogicManager>.inst.room.LoadGuidanceScene();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(selfPlayerData.player.Id);
		TriggerGuide(100500);
	}

	public void FinishStepGuide()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType != 2 || runningGuideInfo == null || GuideRoomData == null)
		{
			return;
		}
		if (GuideRoomData.sceneType == GuideSceneType.GuideSceneA)
		{
			if (runningGuideInfo.guideId == 100303 && runningGuideInfo.currentGuide.StepId == 3)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			else if (runningGuideInfo.guideId == 100304 && runningGuideInfo.currentGuide.StepId == 2)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			else if (runningGuideInfo.guideId == 100307 && runningGuideInfo.currentGuide.StepId == 2)
			{
				GuideRoomData = null;
				SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1003, 1);
			}
		}
		else if (GuideRoomData.sceneType == GuideSceneType.GuideSceneB)
		{
			if (runningGuideInfo.guideId == 100500 && runningGuideInfo.currentGuide.StepId == 3)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			else if (runningGuideInfo.guideId == 100501 && runningGuideInfo.currentGuide.StepId == 1)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			else if (runningGuideInfo.guideId == 100509 && runningGuideInfo.currentGuide.StepId == 4)
			{
				GuideRoomData = null;
				SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1004, 1);
			}
		}
	}

	public void FinishPlayerInfoStepGuide()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2 && runningGuideInfo != null && GuideRoomData != null && GuideRoomData.sceneType == GuideSceneType.GuideSceneA && runningGuideInfo.guideId == 100303 && runningGuideInfo.currentGuide.StepId == 2)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
	}

	public void FinishTutorialStepGuide()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2 && runningGuideInfo != null && GuideRoomData != null)
		{
			if (GuideRoomData.sceneType == GuideSceneType.GuideSceneA && runningGuideInfo.guideId == 100301 && runningGuideInfo.currentGuide.StepId == 4)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
				SimpleSingletonProvider<GameLogicManager>.inst.guide.TriggerGuide(100302);
			}
			if (GuideRoomData.sceneType == GuideSceneType.GuideSceneB && ((runningGuideInfo.guideId == 100500 && runningGuideInfo.currentGuide.StepId == 2) || (runningGuideInfo.guideId == 100503 && runningGuideInfo.currentGuide.StepId == 3)))
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
		}
	}

	public void FinishBuyGoodsGuide()
	{
		if (runningGuideInfo != null && SimpleSingletonProvider<UIManager>.inst.guide.isShowing && runningGuideInfo.guideId == 200100 && runningGuideInfo.currentGuide.StepId == 2)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
			SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(26, 1);
		}
	}

	public void FinishStartGame()
	{
		if (runningGuideInfo != null && SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			if (NoviceStatus)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.guide.RequestTeachingC2S();
			}
			SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1007, 1);
		}
	}

	public void DealOperateSkill()
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		guideActionSn++;
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = guideActionSn;
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealEffectCard.Dispatch(selfPlayerData.player.Id);
	}

	public async void GuideSkillResult(int skillId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		long playerId = playerData.player.Id;
		SkillInfoConfigure skillConfigure = skillId.GetSkillConfigure();
		playerData.CharacterInst.UpdateActiveSkillCD(new MapField<int, int> { [skillId] = skillConfigure.Round });
		await playerData.CharacterInst.skill.SkillTrigger(playerId);
		List<HandCardData> handCards = playerData.cardContainer._HandCards;
		int passiveSkillGolds = 0;
		foreach (HandCardData item in handCards)
		{
			if (item.CardId.GetCardConfigure().CardType == CardType.Attack)
			{
				passiveSkillGolds++;
			}
		}
		List<CardInfo> list = new List<CardInfo> { HandCardData.GetTutorialCardInfo(10005) };
		for (int i = 0; i < list.Count; i++)
		{
			int id = ((Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f)) == 0) ? 20002 : 10005);
			list.Add(HandCardData.GetTutorialCardInfo(id));
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.action.SkillShowAction(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				Id = skillId,
				S = CauseOrigin.Types.source.Skill
			},
			PlayerId = playerId,
			EffectDatas = { GetCardUpdate(playerId, list) }
		});
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(new RepeatedField<HeroAttrEffect> { GetGoldUpdate(playerId, passiveSkillGolds, playerData.Property.gold.Value, passiveSkillGolds + playerData.Property.gold.Value) });
	}

	public void DealOperateMove()
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		guideActionSn++;
		SimpleSingletonProvider<GameLogicManager>.inst.action.throwDiceSn = guideActionSn;
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.MOVE;
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.dealThrowDice.Dispatch(selfPlayerData.player.Id);
	}

	public async UniTaskVoid DealDirSelect()
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		await SimpleSingletonProvider<MoveArrowManager>.inst.DealMove(selfPlayerData.player.Id, guideActionSn++, ForceDir: false);
	}

	public void DealOperateCard()
	{
		guideActionSn++;
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = guideActionSn;
		SimpleSingletonProvider<GameLogicManager>.inst.action.UsableCards = new RepeatedField<int> { 20013 };
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CARD;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.GuideUseCard();
	}

	public async void GuidanceCard_20013Result(RepeatedField<long> targetPlayerIds)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		BattlePlayerData targetPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerIds[0]);
		CardInfoConfigure _cardConfig = 20013.GetCardConfigure();
		SimpleSingletonProvider<GameLogicManager>.inst.card.UpdateUseCardCount();
		await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[20013].CardCallBack(playerData.player.Id, targetPlayerIds, reverse: false, 0);
		await SimpleSingletonProvider<GameLogicManager>.inst.action.CardShowAction(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = 20013L
			},
			PlayerId = playerData.player.Id,
			EffectDatas = { GetHpUpdate(targetPlayer.player.Id, _cardConfig.Params[2], targetPlayer.player.characterConfig.Blood, targetPlayer.player.characterConfig.Blood + _cardConfig.Params[2], _cardConfig.Params[2], targetPlayer.player.characterConfig.Blood) }
		});
		await UpdateCards(playerData.player.Id, 20013);
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.FinishStepGuide();
		}
	}

	private async UniTask DealOperateFight()
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		FightWindow obj = await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
		obj.touchable = false;
		obj.OpenChallengeWin(playerData.player.Id, fightPlayers[0], guideActionSn++);
	}

	private HeroAttrEffect GetHpUpdate(long playerId, int _ChangeHp, int _OriHp, int _CurrHp, int _RealChangeHp, int _MaxHP)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Hp = new HeroHpChangeS2C
			{
				PlayerId = playerId,
				ChangeHp = _ChangeHp,
				OriHp = _OriHp,
				CurrHp = _CurrHp,
				RealChangeHp = _RealChangeHp,
				MaxHp = _MaxHP
			}
		};
	}

	private HeroAttrEffect GetGoldUpdate(long playerId, int _ChangeGold, int _OriGold, int _CurrGold)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Gold = new HeroGoldChangeS2C
			{
				PlayerId = playerId,
				ChangeGold = _ChangeGold,
				OriGold = _OriGold,
				CurrGold = _CurrGold
			}
		};
	}

	private HeroAttrEffect GetCardUpdate(long playerId, List<CardInfo> cardIds)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Card = new HeroCardChangeS2C
			{
				PlayerId = playerId,
				Cards = { (IEnumerable<CardInfo>)cardIds }
			}
		};
	}

	private HeroAttrEffect GetLVUpdate(long playerId, int _Lv)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Lv = new HeroUpLvS2C
			{
				PlayerId = playerId,
				CurrLv = _Lv
			}
		};
	}
}
