using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Audio;
using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic.Replay;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class BattleLogic : IRPCSync
{
	public readonly BattleSignal signal = new BattleSignal();

	public readonly List<BattlePlayerData> PlayerDatas = new List<BattlePlayerData>();

	public readonly SummonLogic summon = new SummonLogic();

	public bool clientFinishReady;

	public BattleInfoPanel battleInfo;

	public HandCardPanel handCard;

	private HashSet<int> _playShowVoices = new HashSet<int>();

	private List<Character> _deployPlayers = new List<Character>();

	private readonly RepeatedField<HeroAttrEffect> _CacheHeroAttrs = new RepeatedField<HeroAttrEffect>();

	private long CurPlayerId;

	public float mapCharacterOffsetHeight
	{
		get
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo == null)
			{
				return 0f;
			}
			return curRoomInfo.SceneConfig?.CharacterHeight ?? 0f;
		}
	}

	public RoomBattleBGM BattleBGM => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.battleBgm;

	public int CurMaxGameProgress => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GameMaxProgress;

	public long curPlayerId
	{
		get
		{
			if (CurPlayerId == 0L)
			{
				List<BattlePlayerData> playerDatas = PlayerDatas;
				if (playerDatas != null && playerDatas.Count > 0)
				{
					return PlayerDatas[0].player.Id;
				}
			}
			return CurPlayerId;
		}
	}

	public void Connect()
	{
		summon.Connect();
		Connect_Player();
		Connect_Monster();
		Connect_PVE();
		signal.deadDeal.AddListener(DeadDeal);
	}

	public void Disconnect()
	{
		summon?.Disconnect();
		Disconnect_Player();
		Disconnect_Monster();
		Disconnect_PVE();
		signal.deadDeal.RemoveListener(DeadDeal);
	}

	public void Dispose()
	{
		summon?.Dispose();
		PlayerDatas.Clear();
		_deployPlayers.Clear();
	}

	public async UniTask BattleStart(bool initialization)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (roomInfo.IsMutatorPve())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.performTriggerLogic.InitPerformTrigger();
		}
		await SimpleSingletonProvider<SceneManager>.inst.ActivateScene();
		PlayerDatas.Clear();
		foreach (RoomPlayer player in roomInfo.Players)
		{
			BattlePlayerData item = new BattlePlayerData(player);
			PlayerDatas.Add(item);
			if (SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(player.Id))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.SetLockChatPlayer(player.Id);
			}
		}
		foreach (RoomPlayer monster in roomInfo.Monsters)
		{
			if (monster.Property.HP.Value != 0)
			{
				BattlePlayerData item2 = new BattlePlayerData(monster);
				PlayerDatas.Add(item2);
			}
		}
		PlayerDatas.Sort(PlayerDataCompare);
		await summon.UpdateSummonData(roomInfo.info, initialization);
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: true);
		await ReadyBattleUI();
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if (playerData.characterType != CharacterType.Monster || playerData.Property.HP.Value != 0)
			{
				playerData.InitCharacter(initialization);
				await SimpleSingletonProvider<EffectManager>.inst.PreLoadFightEffect(playerData.player.Id);
			}
		}
		if (initialization && battleInfo != null)
		{
			await battleInfo.PlayerDebut();
			if (roomInfo.IsLuckyStarBattle() && roomInfo.Round <= 1)
			{
				await SimpleSingletonProvider<UIManager>.inst.LuckyStarMission.ShowRoomMissionAtGameStart();
			}
		}
		clientFinishReady = true;
		signal.showActionMask.Dispatch(t: false);
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: false);
		await SimpleSingletonProvider<GameLogicManager>.inst.room.BuildChessboardData();
		Debug.LogError($"对局开始: RoomId-{roomInfo.Id} RoomMapId-{roomInfo.MapId} RoomPlayer:{string.Join(',', roomInfo.RoomActorDict.Keys)}");
	}

	public async UniTask ReadyBattleUI()
	{
		battleInfo = (await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.BattleInfo)) as BattleInfoPanel;
		handCard = (await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.HandCard)) as HandCardPanel;
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.TryInitCampaignData();
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst?.room?.curRoomInfo;
		if (roomInfo != null && (roomInfo.MapType == 10 || !roomInfo.IsSingleGameModel()))
		{
			SimpleSingletonProvider<UIManager>.inst.expression.TryShowAsync().Forget();
			SimpleSingletonProvider<UIManager>.inst.ExpressionList.TryShowAsync().Forget();
		}
		PreLoadOtherUI();
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session;
		if (replaySession != null && replaySession.IsReplay && !SimpleSingletonProvider<UIManager>.inst.Replay.isShowing)
		{
			await SimpleSingletonProvider<UIManager>.inst.Replay.ShowOperate();
		}
	}

	public void CloseBattleUI()
	{
		battleInfo?.Close();
		handCard?.Close();
	}

	private void PreLoadOtherUI()
	{
		SimpleSingletonProvider<UIManager>.inst.skill.Load(null);
		SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.Load(null);
	}

	public void PlayVoice(HeroVoiceType type, long playerId)
	{
		if (GetPlayerDataById(playerId) != null)
		{
			int voice = GetPlayerDataById(playerId).player.standingPainting.Voice;
			PlayVoice(type, voice);
		}
	}

	public void PlayVoice(HeroVoiceType type, int voiceConfigId)
	{
		if (voiceConfigId != 0)
		{
			CharacterVoiceConfigure voiceConfigure = voiceConfigId.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				BGMHelper.TryBattleRoleVoice(GetVoiceEventId(voiceConfigure, type));
			}
		}
	}

	private int GetVoiceEventId(CharacterVoiceConfigure voiceConfig, HeroVoiceType type)
	{
		return type switch
		{
			HeroVoiceType.SELECT => voiceConfig.SelectVoice, 
			HeroVoiceType.SKILL => voiceConfig.SkillVoice, 
			HeroVoiceType.CARD => voiceConfig.CardVoice, 
			HeroVoiceType.MOVE => voiceConfig.MoveVoice, 
			HeroVoiceType.EVENT => voiceConfig.EventVoice, 
			HeroVoiceType.SHOP => voiceConfig.ShopVoice, 
			HeroVoiceType.ATK => voiceConfig.AtkVoice, 
			HeroVoiceType.DEF => voiceConfig.DefVoice, 
			HeroVoiceType.LEVELUP => voiceConfig.LvUpVoice, 
			HeroVoiceType.ACHIEVE => voiceConfig.AchieveVoice, 
			HeroVoiceType.KILL => voiceConfig.KillVoice, 
			HeroVoiceType.WIN => voiceConfig.WinVoice, 
			HeroVoiceType.AWAKE => voiceConfig.AwakeVoice, 
			HeroVoiceType.FANFAREVOICE => voiceConfig.FanfareVoice, 
			HeroVoiceType.SHOW => GetHeroShowVoice(voiceConfig.ShowVoice), 
			_ => 0, 
		};
	}

	private int GetHeroShowVoice(MapField<int, int> showVoiceMap)
	{
		if (showVoiceMap == null || showVoiceMap.Count == 0)
		{
			return 0;
		}
		if (!showVoiceMap.TryGetValue(0, out var value))
		{
			return 0;
		}
		if (showVoiceMap.Count == 1)
		{
			return value;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GameProgress > 1)
		{
			return value;
		}
		_playShowVoices.Clear();
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if (showVoiceMap.TryGetValue(playerData.player.characterConfig.Id, out var value2))
			{
				_playShowVoices.Add(value2);
			}
		}
		if (_playShowVoices.Count == 0)
		{
			return value;
		}
		return _playShowVoices.ElementAt((showVoiceMap.Count > 1) ? UnityEngine.Random.Range(0, _playShowVoices.Count) : 0);
	}

	private void DeadDeal(long playerId)
	{
		ResetCurPlayerState(playerId);
		BattlePlayerData playerDataById = GetPlayerDataById(playerId);
		if (playerDataById != null && playerDataById.characterType == CharacterType.Monster)
		{
			signal.minimapDelete.Dispatch(playerDataById.player.Id);
			DeployPlayers(playerDataById.CharacterInst, willMove: true);
			playerDataById.Dispose();
		}
	}

	public void DeployPlayers(Character cc, bool willMove, bool forceDeploy = false)
	{
		if (cc == null)
		{
			return;
		}
		_deployPlayers.Clear();
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if ((playerData.characterType == CharacterType.Monster && playerData.Property.HP.Value == 0) || !(playerData.CharacterInst != null) || !(playerData.CharacterInst.characterAnimator != null) || playerData.CharacterInst.characterAnimator.IsHide() || cc.standLand.Id != playerData.CharacterInst.standLand.Id)
			{
				continue;
			}
			if (cc.player.Id == playerData.player.Id)
			{
				if (!willMove)
				{
					_deployPlayers.Add(playerData.CharacterInst);
				}
			}
			else
			{
				_deployPlayers.Add(playerData.CharacterInst);
			}
		}
		if (forceDeploy && !_deployPlayers.Contains(cc))
		{
			_deployPlayers.Add(cc);
		}
		if (_deployPlayers.Count > 1 || willMove)
		{
			Vector3 localPosition = cc.standLand.transform.localPosition;
			for (int i = 1; i <= _deployPlayers.Count; i++)
			{
				float num = ((float)i - (float)(_deployPlayers.Count + 1) * 0.5f) * 7f;
				float pos_X = localPosition.x + num;
				float pos_Y = localPosition.z + num + (float)i * 0.2f;
				_deployPlayers[i - 1].SetCharacterPos(pos_X, pos_Y);
			}
		}
		_deployPlayers.Clear();
	}

	public void DeployPlayers(int landId)
	{
		if (landId == 0)
		{
			return;
		}
		_deployPlayers.Clear();
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if ((playerData.characterType != CharacterType.Monster || playerData.Property.HP.Value != 0) && playerData.CharacterInst != null && playerData.CharacterInst.characterAnimator != null && !playerData.CharacterInst.characterAnimator.IsHide() && landId == playerData.CharacterInst.standLand.Id)
			{
				_deployPlayers.Add(playerData.CharacterInst);
			}
		}
		if (_deployPlayers.Count > 1)
		{
			Vector3 localPosition = SimpleSingletonProvider<LandManager>.inst.GetLandById(landId).transform.localPosition;
			for (int i = 1; i <= _deployPlayers.Count; i++)
			{
				float num = ((float)i - (float)(_deployPlayers.Count + 1) * 0.5f) * 7f;
				float pos_X = localPosition.x + num;
				float pos_Y = localPosition.z + num + (float)i * 0.2f;
				_deployPlayers[i - 1].SetCharacterPos(pos_X, pos_Y);
			}
		}
		_deployPlayers.Clear();
	}

	public void Connect_PVE()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GameProgressChangeS2C.OnGameProgressChangeS2CServerCallBackAsync = OnGameProgressChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MapMissionNotifyS2C.OnMapMissionNotifyS2CServerCallBackAsync = OnMapMissionNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.DelayProgressMapEventS2C.OnDelayProgressMapEventS2CServerCallBackAsync = OnDelayProgressMapEventS2C;
		MonoSingletonProvider<NetManager>.inst.RPC.ClueNotifyS2C.OnClueNotifyS2CServerCallBackAsync = OnClueNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GameRoundChangeS2C.OnGameRoundChangeS2CServerCallBackAsync = OnGameRoundChangeS2CServerCallBack;
	}

	public void Disconnect_PVE()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GameProgressChangeS2C.OnGameProgressChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MapMissionNotifyS2C.OnMapMissionNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.DelayProgressMapEventS2C.OnDelayProgressMapEventS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ClueNotifyS2C.OnClueNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GameRoundChangeS2C.OnGameRoundChangeS2CServerCallBackAsync = null;
	}

	private async UniTask OnGameProgressChangeS2CServerCallBack(GameProgressChangeS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo?.UpdatePveProgress(model.Progress, model.MaxProgress);
			signal.progressChange.Dispatch();
			SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData?.TriggerProgress();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnGameRoundChangeS2CServerCallBack(GameRoundChangeS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo?.UpdateRound(model.Round);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnMapMissionNotifyS2CServerCallBack(MapMissionNotifyS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
			if (room.IsInRoom)
			{
				await room.curRoomInfo.UpdateMapMission(model.Mission);
				signal.mapMissionChange.Dispatch();
			}
		}
	}

	private async UniTask OnClueNotifyS2CServerCallBack(ClueNotifyS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errid != 0 || model.Clue == null)
		{
			return;
		}
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room.IsInRoom)
		{
			ClueMissionData clueMissionData = room.curRoomInfo.UpdateClueMissionData(model.Clue);
			signal.clueChange.Dispatch();
			if (clueMissionData.IsCompleted)
			{
				await SimpleSingletonProvider<UIManager>.inst.tips.ShowPVEClueTaskTip(clueMissionData?.PrimaryTarget);
			}
		}
		await UniTask.CompletedTask;
	}

	private async UniTask OnDelayProgressMapEventS2C(DelayProgressMapEventS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			Debug.LogError($"OnDelayProgressMapEventS2C 主推失败，错误码：{errId}");
			return;
		}
		battleInfo?.RefreshPVEProgress(model.DelayProgressMapEvent);
		await UniTask.CompletedTask;
	}

	public List<MapMissionData> GetMapMissionsData()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (!room.IsInRoom)
		{
			return null;
		}
		Dictionary<int, MapMissionData> mapMissionDict = room.curRoomInfo.MapMissionDict;
		List<MapMissionData> list = new List<MapMissionData>();
		foreach (KeyValuePair<int, MapMissionData> item in mapMissionDict)
		{
			if (item.Value.state == 1)
			{
				list.Add(item.Value);
			}
		}
		return list;
	}

	public List<MapMissionTargetData> GetMapMissionsData(List<MapMissionData> mapMissionsData)
	{
		if (mapMissionsData == null || mapMissionsData.Count == 0)
		{
			return null;
		}
		List<MapMissionTargetData> list = new List<MapMissionTargetData>();
		foreach (MapMissionData mapMissionsDatum in mapMissionsData)
		{
			list.AddRange(mapMissionsDatum.targetsData);
		}
		return list;
	}

	public void TryShowFirstDeadTip(long playerId)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController.roomStateType != RoomStateType.RUNNING || roomController.localRoom == null || !roomController.localRoom.IsPVE())
		{
			return;
		}
		BattlePlayerData playerDataById = GetPlayerDataById(playerId);
		if (playerDataById == null || playerDataById.characterType != CharacterType.Hero || playerDataById.player?.Hero?.Cond == null || playerDataById.player.Level > StaticGlobalData.ROOM_PVELOCK_LEVEL || playerDataById.player.Hero.Cond.TotalDie > 0 || playerDataById.player.Hero.Cond.SelfDie > 0)
		{
			return;
		}
		playerDataById.player.Hero.Cond.TotalDie++;
		string content = string.Format(11031.GetLocal(UIStringType.Message), playerDataById.player.GetNick(showRemark: true));
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (!account.IsCampaignRecord(CampaignTutorialType.PVEFirstDead))
			{
				account.UpdateCampaignData(CampaignTutorialType.PVEFirstDead);
				SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(1004).Forget();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(content, 2f);
			}
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(content, 2f);
		}
	}

	public void RecordHeroAttr(RepeatedField<HeroAttrEffect> attrsData)
	{
		_CacheHeroAttrs.Clear();
		_CacheHeroAttrs.AddRange(attrsData);
	}

	public List<long> GetChangeAttrPlayerIds()
	{
		List<long> list = new List<long>();
		for (int i = 0; i < _CacheHeroAttrs.Count; i++)
		{
			long playerId = _CacheHeroAttrs[i].PlayerId;
			if (!list.Contains(playerId))
			{
				list.Add(playerId);
			}
		}
		return list;
	}

	public RepeatedField<HeroAttrEffect> GetAttrDataById(long playerId)
	{
		RepeatedField<HeroAttrEffect> repeatedField = new RepeatedField<HeroAttrEffect>();
		for (int i = 0; i < _CacheHeroAttrs.Count; i++)
		{
			if (_CacheHeroAttrs[i].PlayerId == playerId)
			{
				repeatedField.Add(_CacheHeroAttrs[i]);
			}
		}
		return repeatedField;
	}

	public RepeatedField<HeroAttrEffect> TakeOutHeroAttr(long playerId)
	{
		RepeatedField<HeroAttrEffect> attrDataById = GetAttrDataById(playerId);
		if (attrDataById.Count <= 0)
		{
			return null;
		}
		for (int i = 0; i < attrDataById.Count; i++)
		{
			_CacheHeroAttrs.Remove(attrDataById[i]);
		}
		return attrDataById;
	}

	public bool FinishUpdateAttr()
	{
		return _CacheHeroAttrs.Count == 0;
	}

	public async UniTask UpdateOtherHeroAttr()
	{
		await UpdateAllHeroAttr(_CacheHeroAttrs);
		_CacheHeroAttrs.Clear();
	}

	public async UniTask UpdateAttr(BattlePlayerData playerData, RepeatedField<HeroAttrEffect> attrDatas)
	{
		if (playerData == null)
		{
			return;
		}
		foreach (HeroAttrEffect attrData in attrDatas)
		{
			await UpdateBaseAttr(playerData, attrData);
		}
	}

	public async UniTask UpdateAllHeroAttr(RepeatedField<HeroAttrEffect> attrDatas, bool isFight = false)
	{
		for (int i = 0; i < attrDatas.Count; i++)
		{
			BattlePlayerData playerData = GetPlayerDataById(attrDatas[i].PlayerId);
			if (playerData == null)
			{
				continue;
			}
			if (attrDatas.Count > i)
			{
				await UpdateBaseAttr(playerData, attrDatas[i], isFight);
			}
			if (attrDatas.Count > i && attrDatas[i].Lottery != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.land.OnLotteryChanged(playerData, attrDatas[i].Lottery.Lotterys);
			}
			if (attrDatas.Count > i && attrDatas[i].Bomb != null && playerData.CharacterInst != null)
			{
				if (!(await playerData.CharacterInst.SwitchCamera()))
				{
					return;
				}
				await summon.UpdateBombs(playerData.player.Id, attrDatas[i].Bomb.Bombs);
			}
		}
		_CacheHeroAttrs.Clear();
	}

	public async UniTask UpdateBaseAttr(BattlePlayerData playerData, HeroAttrEffect _Attr, bool isFight = false)
	{
		if (_Attr.Lv != null)
		{
			await playerData.Property.OnLevelChanged(_Attr.Lv.CurrLv);
		}
		if (_Attr.Gold != null && _Attr.Gold.ChangeGold != 0)
		{
			await playerData.Property.OnGoldChanged(_Attr.Gold);
		}
		if (_Attr.Hp != null)
		{
			await playerData.Property.OnLifeChanged(_Attr.Hp, isFight);
		}
		if (_Attr.Atk != null)
		{
			playerData.Property.OnATKChanged(_Attr.Atk);
		}
		if (_Attr.Def != null)
		{
			playerData.Property.OnDEFChanged(_Attr.Def);
		}
		if (_Attr.Card != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.card.OnCardChanged(playerData, _Attr.Card.Cards, show: true);
		}
		if (_Attr.ConvertCard != null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.card.OnCardConvertChanged(playerData, _Attr.ConvertCard.ConvertCards);
		}
		if (_Attr.Buff != null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.buff.OnBuffChanged(playerData, _Attr.Buff);
		}
		if (_Attr.Cd != null && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_Attr.Cd.PlayerId))
		{
			playerData.Property.UpdateSkillCD(_Attr.Cd.Cd);
		}
		if (_Attr.Reroll != null)
		{
			playerData.Property.OnReRelicCountChanged(_Attr.Reroll);
		}
		if (_Attr.SpecialScore != null)
		{
			playerData.Property.OnScoreChanged(_Attr.SpecialScore);
		}
		if (_Attr.CureNum != null)
		{
			playerData.Property.OnCureCountChanged(_Attr.CureNum);
		}
		if (_Attr.SalaryNum != null)
		{
			playerData.Property.OnSalaryCountChanged(_Attr.SalaryNum);
		}
		if (_Attr.MarkNum != null)
		{
			playerData.Property.OnMarkCountChanged(_Attr.MarkNum);
		}
		if (_Attr.CounterNum != null)
		{
			playerData.Property.OnCounterCountChanged(_Attr.CounterNum);
		}
		if (_Attr.UseCardNum != null)
		{
			playerData.Property.OnCardCountChanged(_Attr.UseCardNum);
		}
		if (_Attr.Place != null)
		{
			HeroPlace place = _Attr.Place.Place;
			if (place != null)
			{
				playerData.player.UpdateHeroPlace(place.NodeId, place.FrontNodeIds, place.BackNodeId);
			}
		}
		if (_Attr.CardDistance != null)
		{
			playerData.Property.OnCardDistanceChanged(_Attr.CardDistance);
		}
		if (_Attr.CardAtk != null)
		{
			playerData.Property.OnCardAtkChanged(_Attr.CardAtk);
		}
		if (_Attr.CanCounter != null)
		{
			playerData.Property.OnCanCounterChanged(_Attr.CanCounter);
		}
		if (_Attr.ModifyNum != null)
		{
			await playerData.Property.OnModifyNumChanged(_Attr.ModifyNum);
		}
		if (_Attr.NotSelect != null)
		{
			playerData.Property.OnNotSelectChanged(_Attr.NotSelect);
		}
		if (_Attr.Disappear != null)
		{
			GetPlayerDataById(_Attr.Disappear.PlayerId).Absorb();
		}
		if (_Attr.AddMove != null)
		{
			playerData.Property.OnChangeExtraMovePoint(_Attr.AddMove.CurrAddMove, _Attr.AddMove.CurrDecMove);
		}
		if (_Attr.UniqueNum != null)
		{
			await playerData.Property.OnUniqueNumChanged(_Attr.UniqueNum);
		}
		if (_Attr.EnergyNum != null)
		{
			playerData.Property.OnEnergyNumChanged(_Attr.EnergyNum);
		}
		if (_Attr.CrimeNum != null)
		{
			await playerData.Property.OnCrimeNumChanged(_Attr.CrimeNum);
		}
	}

	public async UniTask OnMonsterCombine(RepeatedField<HeroAttrEffect> attrDatas)
	{
		PveBossCombineS2C pveBossCombineS2C = null;
		if (attrDatas == null || attrDatas.Count == 0)
		{
			Debug.LogError("合并数据为空");
			return;
		}
		foreach (HeroAttrEffect attrData in attrDatas)
		{
			if (attrData.Combine != null)
			{
				pveBossCombineS2C = attrData.Combine;
			}
			else if (attrData.Hp != null)
			{
				GetPlayerDataById(attrData.PlayerId)?.DisposeMonster_Hide();
			}
		}
		if (pveBossCombineS2C != null && pveBossCombineS2C.CombineType == 1)
		{
			await SimpleSingletonProvider<UIManager>.inst.BattleVideo.PlayCombineVideo(pveBossCombineS2C.BossIds);
		}
	}

	public void CLearLottery()
	{
		for (int i = 0; i < PlayerDatas.Count; i++)
		{
			PlayerDatas[i].player.Hero.Lotterys.Clear();
		}
	}

	private void Connect_Monster()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync = OnMonsterRefreshS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BossSleepS2C.OnBossSleepS2CServerCallBackAsync = OnBossSleepS2CServerCallBack;
	}

	private void Disconnect_Monster()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BossSleepS2C.OnBossSleepS2CServerCallBackAsync = null;
	}

	private async UniTask OnMonsterRefreshS2CServerCallBack(MonsterRefreshS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			RoomPlayer monsterData = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.TryAddMonster(model.Monster);
			BattlePlayerData battlePlayerData = await InstantiateMonster(monsterData, initialization: true);
			if (!(battlePlayerData?.CharacterInst == null))
			{
				DeployPlayers(battlePlayerData.CharacterInst, willMove: false);
				SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.TriggerMonsterShow();
			}
		}
	}

	private async UniTask<BattlePlayerData> InstantiateMonster(RoomPlayer _monsterData, bool initialization)
	{
		BattlePlayerData _data = new BattlePlayerData(_monsterData);
		PlayerDatas.Add(_data);
		_data.InitCharacter(initialization);
		await SimpleSingletonProvider<EffectManager>.inst.PreLoadFightEffect(_data.player.Id);
		if (initialization)
		{
			SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: true);
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_monsterData.Id, _data.player.standingPainting.FanfarePerform, "怪物登场");
			SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state: false);
			SimpleSingletonProvider<GameLogicManager>.inst.performTriggerLogic.signal.monsterShowSignal.Dispatch(_monsterData.characterConfig.Id);
		}
		return _data;
	}

	public async UniTask UpdateMonsterByReconnect(List<RoomPlayer> monsters)
	{
		foreach (RoomPlayer monster in monsters)
		{
			BattlePlayerData battlePlayerData = GetPlayerDataById(monster.Id);
			if (battlePlayerData == null)
			{
				if (monster.Property.HP.Value <= 0)
				{
					continue;
				}
				battlePlayerData = await InstantiateMonster(monster, initialization: false);
			}
			else
			{
				if (monster.Property.HP.Value <= 0)
				{
					battlePlayerData.Dispose();
					continue;
				}
				battlePlayerData.UpdateCharacter(monster);
			}
			if (battlePlayerData.CharacterInst != null)
			{
				DeployPlayers(battlePlayerData.CharacterInst, willMove: false);
			}
		}
		List<long> list = new List<long>();
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if (playerData.characterType == CharacterType.Monster)
			{
				if (playerData.player.Property.HP.Value == 0)
				{
					list.Add(playerData.player.Id);
				}
				else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerById(playerData.player.Id) == null)
				{
					list.Add(playerData.player.Id);
				}
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			DeadDeal(list[i]);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.TriggerMonsterShow();
	}

	public void ReconcilePlayerDatasForReplaySnapshot(RoomInfo newRoom)
	{
		if (newRoom == null)
		{
			return;
		}
		HashSet<long> hashSet = new HashSet<long>(newRoom.Monsters.Count);
		foreach (RoomPlayer monster in newRoom.Monsters)
		{
			hashSet.Add(monster.Id);
		}
		for (int num = PlayerDatas.Count - 1; num >= 0; num--)
		{
			BattlePlayerData battlePlayerData = PlayerDatas[num];
			if (battlePlayerData.characterType == CharacterType.Monster && !hashSet.Contains(battlePlayerData.player.Id))
			{
				battlePlayerData.Dispose();
				PlayerDatas.RemoveAt(num);
			}
		}
		foreach (RoomPlayer monster2 in newRoom.Monsters)
		{
			if (monster2.Property.HP.Value > 0)
			{
				BattlePlayerData playerDataById = GetPlayerDataById(monster2.Id);
				if (playerDataById != null && !(playerDataById.CharacterInst != null))
				{
					playerDataById.Dispose();
					PlayerDatas.Remove(playerDataById);
					BattlePlayerData battlePlayerData2 = new BattlePlayerData(monster2);
					PlayerDatas.Add(battlePlayerData2);
					battlePlayerData2.InitCharacter(initialization: false);
				}
			}
		}
	}

	private async UniTask OnBossSleepS2CServerCallBack(BossSleepS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await GetPlayerDataById(model.PlayerId).CharacterInst.characterAnimator.Sleep(sleep: true);
		}
	}

	public BattlePlayerData GetPlayerDataById(long playerId)
	{
		_ = PlayerDatas.Count;
		return PlayerDatas.Find((BattlePlayerData x) => x.player.Id == playerId);
	}

	public List<BattlePlayerData> GetPlayerDataByTeamId(long teamId)
	{
		List<BattlePlayerData> list = new List<BattlePlayerData>();
		for (int i = 0; i < PlayerDatas.Count; i++)
		{
			if (PlayerDatas[i].player.TeamId == teamId)
			{
				list.Add(PlayerDatas[i]);
			}
		}
		return list;
	}

	public BattlePlayerData GetPlayerDataBySlot(int slot)
	{
		return PlayerDatas.Find((BattlePlayerData x) => x.player.Slot == slot && x.characterType == CharacterType.Hero);
	}

	public int GetPlayerIndexDataById(long playerId)
	{
		return PlayerDatas.FindIndex((BattlePlayerData x) => x.player.Id == playerId);
	}

	public BattlePlayerData GetSelfPlayerData()
	{
		WatchLogic watch = SimpleSingletonProvider<GameLogicManager>.inst.watch;
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		if (watch != null && watch.IsWatcher(playerID))
		{
			return GetPlayerDataById(watch.SubscribePlayerId.Value);
		}
		return GetPlayerDataById(playerID);
	}

	public int GetCardId(long playerId, int cardUid)
	{
		int result = 0;
		BattlePlayerData playerDataById = GetPlayerDataById(playerId);
		HandCardData value = null;
		if (playerDataById?.cardContainer?._HandCardData?.TryGetValue(cardUid, out value) == true)
		{
			result = value.CardId;
		}
		return result;
	}

	public BattlePlayerData GetPlayerDataByHeroId(int heroId)
	{
		return PlayerDatas.Find((BattlePlayerData x) => x.player.Hero.HeroId == heroId);
	}

	public BattlePlayerData GetCurrentPlayer()
	{
		return GetPlayerDataById(curPlayerId);
	}

	public bool IsCurrentPlayer(long playerId)
	{
		return playerId == curPlayerId;
	}

	public BattlePlayerData GetPlayerBySlot(int _slot)
	{
		for (int i = 0; i < PlayerDatas.Count; i++)
		{
			if (PlayerDatas[i].player.Slot == _slot && PlayerDatas[i].characterType == CharacterType.Hero)
			{
				return PlayerDatas[i];
			}
		}
		return null;
	}

	public bool IsContainHero(int heroId)
	{
		for (int i = 0; i < PlayerDatas.Count; i++)
		{
			if (PlayerDatas[i].characterType == CharacterType.Hero && PlayerDatas[i].player.Hero.HeroId == heroId)
			{
				return true;
			}
		}
		return false;
	}

	public List<BattlePlayerData> GetRankData(long winTeamId)
	{
		List<BattlePlayerData> list = new List<BattlePlayerData>(4);
		for (int i = 0; i < PlayerDatas.Count; i++)
		{
			if (PlayerDatas[i].characterType == CharacterType.Hero)
			{
				list.Add(PlayerDatas[i]);
			}
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && (curRoomInfo.IsAsymmetricalBattle() || curRoomInfo.IsLuckyStarBattle()))
		{
			for (int j = 0; j < list.Count; j++)
			{
				list[j].rank = ((list[j].player.TeamId != winTeamId) ? 1 : 0);
			}
			list.Sort(ComparerByRank);
		}
		else
		{
			list.Sort(ComparerByLevel);
			UpdateRank(list);
		}
		return list;
	}

	private int ComparerByRank(BattlePlayerData x, BattlePlayerData y)
	{
		return x.rank.CompareTo(y.rank);
	}

	private int ComparerByLevel(BattlePlayerData x, BattlePlayerData y)
	{
		int num = y.Property.level.Value.CompareTo(x.Property.level.Value);
		if (num != 0)
		{
			return num;
		}
		return y.Property.gold.Value.CompareTo(x.Property.gold.Value);
	}

	private void UpdateRank(List<BattlePlayerData> randData)
	{
		int num = 0;
		for (int i = 0; i < randData.Count; i++)
		{
			randData[i].rank = num;
			if (i >= randData.Count - 1 || randData[i].Property.level.Value != randData[i + 1].Property.level.Value || randData[i].Property.gold.Value != randData[i + 1].Property.gold.Value)
			{
				num++;
			}
		}
	}

	public void UpdateHeroByReconnect(List<RoomPlayer> players)
	{
		foreach (RoomPlayer player in players)
		{
			BattlePlayerData battlePlayerData = GetPlayerDataById(player.Id);
			if (battlePlayerData == null)
			{
				battlePlayerData = new BattlePlayerData(player);
				battlePlayerData.InitCharacter(initialization: false);
				PlayerDatas.Add(battlePlayerData);
				Debug.LogError($"出现了未知错误，断线重连中更新玩家数据，房间内玩家ID:{player.Id} 无法找到对应的玩家对战数据");
			}
			else
			{
				battlePlayerData.UpdateCharacter(player);
			}
			DeployPlayers(battlePlayerData.CharacterInst, willMove: false);
		}
	}

	private void Connect_Player()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.PredictActionS2C.OnPredictActionS2CServerCallBackAsync = OnPredictActionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceS2C.OnThrowDiceS2CServerCallBackAsync = OnThrowDiceS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MoveS2C.OnMoveS2CServerCallBackAsync = OnMoveS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoundStartS2C.OnRoundStartS2CServerCallBackAsync = OnRoundStartS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MovePointBuffS2C.OnMovePointBuffS2CServerCallBackAsync = OnMovePointBuffS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ActionStartNotifyS2C.OnActionStartNotifyS2CServerCallBackAsync = OnActionStartNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangePlayerSlotS2C.OnChangePlayerSlotS2CServerCallBackAsync = OnChangePlayerSlotS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerOnlineS2C.OnPlayerOnlineS2CServerCallBackAsync = OnPlayerOnlineS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetHeroInfoS2C.OnGetHeroInfoS2CServerCallBackAsync = OnGetHeroInfoS2CServerCallBackAsync;
	}

	private void Disconnect_Player()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.PredictActionS2C.OnPredictActionS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceS2C.OnThrowDiceS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MoveS2C.OnMoveS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoundStartS2C.OnRoundStartS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MovePointBuffS2C.OnMovePointBuffS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ActionStartNotifyS2C.OnActionStartNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangePlayerSlotS2C.OnChangePlayerSlotS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerOnlineS2C.OnPlayerOnlineS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetHeroInfoS2C.OnGetHeroInfoS2CServerCallBackAsync = null;
	}

	private async UniTask OnPredictActionS2CServerCallBack(PredictActionS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo != null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.UpdatePredicts(model.Actions);
		}
	}

	public void RecordFinishSn(long _Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.RecordFinishSn(_Sn);
	}

	public RPCAsyncResult RequestThrowDiceC2S(long _sn)
	{
		OperationTimer.CancelOperatTimer(_sn);
		RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceC2S.ThrowDiceC2SCall(new ThrowDiceC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_MovePoint
		});
	}

	private async UniTask OnThrowDiceS2CServerCallBack(ThrowDiceS2C data, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(data.PlayerId);
		ResetCurPlayerState(data.PlayerId);
		if (errId != 0)
		{
			return;
		}
		BattlePlayerData movePlayer = GetPlayerDataById(data.PlayerId);
		if (movePlayer != null && !(movePlayer.CharacterInst == null))
		{
			movePlayer.CharacterInst.ResetStep(data.MovePoint);
			if (await movePlayer.CharacterInst.SwitchCamera() && ((SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.speedType == 2 && movePlayer.characterType == CharacterType.Monster) || await SimpleSingletonProvider<DiceManager>.inst.ThrowDice(data.PlayerId, data.Vals, data.MovePoint, data.IsControlMovePoint)))
			{
				PlayVoice(HeroVoiceType.MOVE, data.PlayerId);
			}
		}
	}

	public RPCAsyncResult RequestMoveC2S(long _sn, int nextNodeId)
	{
		OperationTimer.CancelOperatTimer(_sn);
		RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.MoveC2S.MoveC2SCall(new MoveC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Direction = nextNodeId
		});
	}

	private async UniTask OnMoveS2CServerCallBack(MoveS2C model, int errId, bool isDispatch)
	{
		SimpleSingletonProvider<UIManager>.inst.tips.HideTopTip();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.TryCloseCampaignTutorial();
		if (!isDispatch)
		{
			return;
		}
		SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
		SimpleSingletonProvider<MoveArrowManager>.inst.CloseArrow();
		BattlePlayerData data = GetPlayerDataById(model.PlayerId);
		if (data == null || data.CharacterInst == null)
		{
			Debug.LogError($"当前角色Id{model.PlayerId}, 找不到玩家数据");
		}
		else
		{
			if (!(await data.CharacterInst.SwitchCamera()))
			{
				return;
			}
			Queue<int> queue = new Queue<int>();
			foreach (int nodeId in model.NodeIds)
			{
				queue.Enqueue(nodeId);
			}
			DeployPlayers(data.CharacterInst, willMove: true);
			await data.CharacterInst.ExecuteMove(queue, model.End);
		}
	}

	private async UniTask OnMovePointBuffS2CServerCallBack(MovePointBuffS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		BattlePlayerData playerDataById = GetPlayerDataById(model.PlayerId);
		if (playerDataById == null || playerDataById.CharacterInst == null || model.Attrs == null || model.Attrs.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (AdditionAttribute attr in model.Attrs)
		{
			if (playerDataById.buffContainer.GetBuffById(attr.UniqueId) == null || attr.MovePoint == 0)
			{
				Debug.Log($"#移动buff# 当前并没有从自身的buff中找到buff——{attr.UniqueId},需要检查是否存在问题");
			}
			else
			{
				num += attr.MovePoint;
			}
		}
		playerDataById.CharacterInst.signal.attrChange.Dispatch((0, 0, num, 6), "");
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
	}

	private async UniTask OnRoundStartS2CServerCallBack(RoundStartS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		await UniTask.WaitUntil(() => clientFinishReady);
		BattlePlayerData selfData = GetSelfPlayerData();
		if (selfData == null || selfData.CharacterInst == null || (selfData.player.Id != model.PlayerId && SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher()))
		{
			return;
		}
		RoomInfo curRoom = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (model.Round == 1 && curRoom != null && BattleConfig.IsPVE(curRoom.MapType) && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsPassMap(curRoom.MapId))
		{
			await SimpleSingletonProvider<UIManager>.inst.explain.ShowPVETutorial(curRoom.MapId);
		}
		selfData.Property.cardUseTimes.JustSetValue(0);
		selfData.Property.OnCardCountChanged(model.UseCardMaxNum);
		GameModeInfoConfigure gameModeInfoConfigure = curRoom.MapType.GetGameModeInfoConfigure();
		int num = gameModeInfoConfigure.DistributeResources[1];
		int mapType = curRoom.MapType;
		if (mapType == 4 || mapType == 12)
		{
			GameModeDifficultyDataConfigureItem difficultyData = gameModeInfoConfigure.GetDifficultyData(4, curRoom.Difficulty);
			if (difficultyData != null)
			{
				num = difficultyData.DistributeResources[1];
			}
		}
		if (model.Round == num)
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowTipsAndWait(string.Format(10022.GetLocal(UIStringType.Message)), 2f);
		}
		if (model.Round > 0 && model.Round % 3 == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowRoundRewardTip(model.Gold.ToString("+#;-#;0"), model.CardNum.ToString("+#;-#;0"), 2f);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowTipsAndWait(string.Format(10001.GetLocal(UIStringType.Message), model.Round), 2f);
			if (!(await PlayerDatas[0].CharacterInst.SwitchCamera()))
			{
				return;
			}
		}
		foreach (BattlePlayerData playerData in PlayerDatas)
		{
			if (playerData.CharacterInst != null)
			{
				playerData.CharacterInst.UpdateActiveSkillCD(model.SkillCds);
			}
		}
		curRoom.UpdateRound(model.Round);
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData?.TriggerRound();
	}

	private async UniTask OnActionStartNotifyS2CServerCallBack(ActionStartNotifyS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		BattlePlayerData _data = GetPlayerDataById(model.PlayerId);
		if (_data == null || _data.CharacterInst == null)
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.CurPlayerId = model.PlayerId;
		SimpleSingletonProvider<GameLogicManager>.inst.action.signal.notifyAllPlayer.Dispatch(model.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId) || SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.Value == model.PlayerId)
		{
			SimpleSingletonProvider<CameraManager>.inst.CancelFreeStatus();
		}
		if (!(await _data.CharacterInst.SwitchCamera()))
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.TryStartTutorialSkill().Forget();
		_data.CharacterInst.characterAnimator.Hospitalized(model.IsHospital);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowSingleStart(_data.player.Slot);
			GameSettings.PlayTipsVibrate();
		}
		if (model.IsHospital)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(model.PlayerId, 11012);
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000))
			{
				return;
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
		}
		else
		{
			string nickname = ((_data.characterType == CharacterType.Hero) ? _data.player.GetNick() : CharacterHandle.GetCharacterName(_data.player.Hero.HeroId));
			int slotIndex = ((_data.characterType == CharacterType.Hero) ? _data.player.Slot : 4);
			SimpleSingletonProvider<UIManager>.inst.tips.ShowRoundStartTip(nickname, slotIndex, 2f).Forget();
		}
		if (!model.IsDie && _data != null && _data.CharacterInst != null)
		{
			await _data.CharacterInst.characterAnimator.Die(die: false);
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() && SimpleSingletonProvider<GameLogicManager>.inst.watch.IsFollow.Value && _data.characterType == CharacterType.Hero)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.watch.UpdateSubscribePlayer(SimpleSingletonProvider<GameLogicManager>.inst.battle.CurPlayerId);
		}
		if (_data.CharacterInst.showComponent is IActionStartNotify actionStartNotify)
		{
			actionStartNotify.ActionStartNotify();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM?.PlayBattleBGM(_data.player.Id);
	}

	private async UniTask OnChangePlayerSlotS2CServerCallBack(ChangePlayerSlotS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowChangeActionTip();
			for (int i = 0; i < model.PlayerSlot.Count; i++)
			{
				GetPlayerDataById(model.PlayerSlot[i].PlayerId).player.UpdateChangeSlot(model.PlayerSlot[i].ChangeSlot);
			}
			PlayerDatas.Sort(PlayerDataCompare);
			signal.playerInfoRefresh.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private int PlayerDataCompare(BattlePlayerData x, BattlePlayerData y)
	{
		if (x.characterType == CharacterType.Hero && y.characterType == CharacterType.Monster)
		{
			return -1;
		}
		if (x.characterType == CharacterType.Monster && y.characterType == CharacterType.Hero)
		{
			return 1;
		}
		if (x.player.ChangeSlot > y.player.ChangeSlot)
		{
			return 1;
		}
		if (x.player.ChangeSlot < y.player.ChangeSlot)
		{
			return -1;
		}
		return 0;
	}

	private async UniTask OnPlayerOnlineS2CServerCallBack(PlayerOnlineS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING)
			{
				GetPlayerDataById(model.PlayerId)?.Property.Online.SetValue(model.IsOnline);
			}
		}
	}

	public RPCAsyncResult RequestGetHeroInfoC2S(long playerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetHeroInfoC2S.GetHeroInfoC2SCall(new GetHeroInfoC2S
		{
			PlayerId = playerId
		});
	}

	private async UniTask OnGetHeroInfoS2CServerCallBackAsync(GetHeroInfoS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		await UniTask.CompletedTask;
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING)
		{
			BattlePlayerData playerDataById = GetPlayerDataById(model.PlayerId);
			if (playerDataById != null)
			{
				playerDataById.KillCount = model.KillCount;
				playerDataById.TotalDamage = model.TotalDamage;
				playerDataById.TotalDie = model.TotalDie;
				playerDataById.TotalInjured = model.TotalInjured;
				playerDataById.TreatmentScore = model.TreatmentScore;
				playerDataById.SkillCD = model.Cd;
			}
		}
	}

	public void ResetCurPlayerState(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) || (SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher() && playerId == SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.Value))
		{
			BattlePlayerData selfPlayerData = GetSelfPlayerData();
			selfPlayerData.Property.OnCardCountChanged(selfPlayerData.Property.CardMaxVailUseCount.Value);
			selfPlayerData.Property.cardUseTimes.JustSetValue(0);
			selfPlayerData.Property.cardUseData.Clear();
		}
	}
}
