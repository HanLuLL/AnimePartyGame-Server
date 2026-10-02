using System;
using System.Collections.Generic;
using Core;
using Core.Audio;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic.Replay;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class RoomLogic : IRPCSync
{
	public readonly RoomSignal signal = new RoomSignal();

	public bool voluntaryWithdrawal;

	public readonly RoomController roomController = new RoomController();

	private const float ContinuousClickInterval = 0.25f;

	private const float ClickCountResetInterval = 0.5f;

	private float _lastInvalidClickTime = -10f;

	private int _invalidContinuousClickCount;

	private const int InvalidClickGiveUpThreshold = 100;

	private int _giveUpAttemptCount;

	private const int GiveUpAttemptThreshold = 9;

	private float _invalidClickDetectCooldownUntil;

	public RoomInfo curRoomInfo => roomController.localRoom;

	public bool IsInRoom => roomController.RoomValid;

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.CreateRoomS2C.OnCreateRoomS2CServerCallBackAsync = OnCreateRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeRoomS2C.OnChangeRoomS2CServerCallBackAsync = OnChangeRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.StartGameS2C.OnStartGameS2CServerCallBackAsync = OnStartGameS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ExitRoomS2C.OnExitRoomS2CServerCallBackAsync = OnExitRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.JoinRoomS2C.OnJoinRoomS2CServerCallBackAsync = OnJoinRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomKickPlayerS2C.OnRoomKickPlayerS2CServerCallBackAsync = OnRoomKickPlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomAbdicationS2C.OnRoomAbdicationS2CServerCallBackAsync = OnRoomAbdicationS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomReadyS2C.OnRoomReadyS2CServerCallBackAsync = OnRoomReadyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SingleCampaignS2C.OnSingleCampaignS2CServerCallBackAsync = OnSingleCampaignS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomRoundAddTermS2C.OnRoomRoundAddTermS2CServerCallBackAsync = OnRoomRoundAddTermS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChoiceHeroS2C2.OnChoiceHeroS2C2ServerCallBackAsync = OnChoiceHeroS2C2ServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AffirmHeroS2C.OnAffirmHeroS2CServerCallBackAsync = OnAffirmHeroS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.HeroBarBoxChangeS2C.OnHeroBarBoxChangeS2CServerCallBackAsync = OnHeroBarBoxChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChooseSkinS2C.OnChooseSkinS2CServerCallBackAsync = OnChooseSkinS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ApplyChangeSlotS2C.OnApplyChangeSlotS2CServerCallBackAsync = OnApplyChangeSlotS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.OpsChangeSlotS2C.OnOpsChangeSlotS2CServerCallBackAsync = OnOpsChangeSlotS2CServerCallBackAsync;
		Connect_StartGame();
		MonoSingletonProvider<NetManager>.inst.RPC.ActionOverTimeLogS2C.OnActionOverTimeLogS2CServerCallBackAsync = OnActionOverTimeLogS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.CreateRoomS2C.OnCreateRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeRoomS2C.OnChangeRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.StartGameS2C.OnStartGameS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ExitRoomS2C.OnExitRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.JoinRoomS2C.OnJoinRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomKickPlayerS2C.OnRoomKickPlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomAbdicationS2C.OnRoomAbdicationS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomReadyS2C.OnRoomReadyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomRoundAddTermS2C.OnRoomRoundAddTermS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChoiceHeroS2C2.OnChoiceHeroS2C2ServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AffirmHeroS2C.OnAffirmHeroS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.HeroBarBoxChangeS2C.OnHeroBarBoxChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChooseSkinS2C.OnChooseSkinS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ApplyChangeSlotS2C.OnApplyChangeSlotS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.OpsChangeSlotS2C.OnOpsChangeSlotS2CServerCallBackAsync = null;
		Disconnect_StartGame();
		MonoSingletonProvider<NetManager>.inst.RPC.ActionOverTimeLogS2C.OnActionOverTimeLogS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestCreateC2S(string roomName, string pwd, int map_id, int max_time, int upgrade_plan, int time_plan, ulong steamLobbyId, int speedType, int mapModeType, int difficulty, bool skipStory, int roomLabel)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.CreateRoomC2S.CreateRoomC2SCall(new CreateRoomC2S
		{
			Id = 0L,
			Name = roomName,
			Pwd = pwd,
			MapId = map_id,
			MaxTime = max_time,
			UpgradePlan = upgrade_plan,
			TimePlan = time_plan,
			LobbyId = steamLobbyId,
			SpeedType = speedType,
			Mode = mapModeType,
			Difficulty = difficulty,
			SkipStory = skipStory,
			RoomLabel = roomLabel
		});
	}

	private async UniTask OnCreateRoomS2CServerCallBack(CreateRoomS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			roomController.CreateRoom(model.Room);
			if (BattleConfig.IsPractice(model.Room.MapType))
			{
				RequestStartGameC2S(isAddBot: true);
			}
			else
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomWait);
			}
		}
	}

	public RPCAsyncResult RequestJoinRoomC2S(long _roomId, string pwd, int roomServerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.JoinRoomC2S.JoinRoomC2SCall(new JoinRoomC2S
		{
			RoomId = _roomId,
			Pwd = pwd,
			RoomServerId = roomServerId
		});
	}

	private async UniTask OnJoinRoomS2CServerCallBack(JoinRoomS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await DealJoinRoom(model.Room);
		}
	}

	public async UniTask DealJoinRoom(Room _room)
	{
		if (_room != null)
		{
			if (!IsInRoom || curRoomInfo.Id != _room.Id)
			{
				roomController.CreateRoom(_room);
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomWait);
			}
			else
			{
				roomController.UpdateRoomByJoin(_room);
				signal.roomPlayerChange.Dispatch();
			}
		}
	}

	public RPCAsyncResult RequestChangeRoomC2S(string pwd, int map_id, int time_plan, int upgrade_plan, int speedType, int difficulty, bool skipStory, int roomLabel)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ChangeRoomC2S.ChangeRoomC2SCall(new ChangeRoomC2S
		{
			Pwd = pwd,
			MapId = map_id,
			UpgradePlan = upgrade_plan,
			TimePlan = time_plan,
			SpeedType = speedType,
			Difficulty = difficulty,
			SkipStory = skipStory,
			RoomLabel = roomLabel
		});
	}

	private async UniTask OnChangeRoomS2CServerCallBack(ChangeRoomS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			roomController.UpdateRoomSetting(model.Room);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType exitType)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ExitRoomC2S.ExitRoomC2SCall(new ExitRoomC2S
		{
			ExitType = exitType
		});
	}

	private async UniTask OnExitRoomS2CServerCallBack(ExitRoomS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && IsInRoom)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId) && SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomHeroPanel)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11021);
			}
			roomController.UpdateRoomByExit(model.MasterId, model.PlayerId, model.Dissolve);
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId) || model.Dissolve)
			{
				ClearRoomInfo();
			}
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || !model.GameFinish)
			{
				await UniTask.CompletedTask;
				return;
			}
			Debug.LogError("Server notify giveUpGame in battle");
			ExitRunningRoom(null);
		}
	}

	public RPCAsyncResult RequestStartGameC2S(bool isAddBot)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.StartGameC2S.StartGameC2SCall(new StartGameC2S
		{
			IsAddBot = isAddBot
		});
	}

	private async UniTask OnStartGameS2CServerCallBack(StartGameS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
			{
				signal.roomSettingRefresh.Dispatch();
			}
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Clear();
		if (model.Room != null)
		{
			if (SimpleSingletonProvider<UIManager>.inst.existMessageBox && SimpleSingletonProvider<UIManager>.inst.messageBox.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.HideImmediately();
			}
			roomController.SwitchRoomState(model.Room, RoomStateType.CHOICE);
			if (MonoSingletonProvider<NetManager>.inst.IsConnected)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
			}
		}
	}

	public RPCAsyncResult RequestChoiceHeroC2S2(int _heroId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ChoiceHeroC2S2.ChoiceHeroC2S2Call(new ChoiceHeroC2S2
		{
			HeroId = _heroId
		});
	}

	public RPCAsyncResult RequestAffirmHeroC2S(bool _auto)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.AffirmHeroC2S.AffirmHeroC2SCall(new AffirmHeroC2S
		{
			Auto = _auto
		});
	}

	private async UniTask OnChoiceHeroS2C2ServerCallBack(ChoiceHeroS2C2 model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnAffirmHeroS2CServerCallBack(AffirmHeroS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		RoomPlayer player = curRoomInfo.GetPlayerById(model.PlayerId);
		if (model.UseAdorn != 0)
		{
			player.UpdateStandingPainting(model.HeroId, model.UseAdorn);
		}
		if (!model.HasChoice && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			await SimpleSingletonProvider<CharacterAssetManager>.inst.LoadHeroVoiceBank(model.HeroId, player.standingPainting.Voice);
			CharacterVoiceConfigure voiceConfigure = player.standingPainting.Voice.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				BGMHelper.TryBattleRoleVoice(voiceConfigure.SelectVoice);
			}
		}
		signal.SureHero.Dispatch(model.HeroId, model.PlayerId, model.HasChoice);
	}

	private async UniTask OnHeroBarBoxChangeS2CServerCallBack(HeroBarBoxChangeS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		curRoomInfo.UpdateHeroBox(model.Box);
		foreach (KeyValuePair<long, HeroBar> item in model.Box.Box)
		{
			RoomPlayer playerById = curRoomInfo.GetPlayerById(item.Key);
			playerById.UpdatePVEHeroLV(item.Value.PveLevel);
			playerById.UpdateStandingPainting(item.Value.HeroId, item.Value.UseAdorn);
		}
		signal.refreshHeroList.Dispatch(model.Box, t2: false);
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestChooseSkinC2S(int heroId, int useAdorn, bool affirmed)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ChooseSkinC2S.ChooseSkinC2SCall(new ChooseSkinC2S
		{
			HeroId = heroId,
			UseAdorn = useAdorn,
			Affirmed = affirmed
		});
	}

	private async UniTask OnChooseSkinS2CServerCallBack(ChooseSkinS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			RoomPlayer playerById = curRoomInfo.GetPlayerById(model.PlayerId);
			if (!model.Affirmed && model.UseAdorn == 0)
			{
				Debug.LogError("ChooseSkinS2C dispatch model.Affirmed = false, model.UseAdorn = 0");
			}
			if (model.UseAdorn != 0)
			{
				playerById.UpdateStandingPainting(model.HeroId, model.UseAdorn);
			}
			signal.SureSkin.Dispatch(model.PlayerId, model.Affirmed);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestKickPlayer(long _playerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RoomKickPlayerC2S.RoomKickPlayerC2SCall(new RoomKickPlayerC2S
		{
			PlayerId = _playerId
		});
	}

	private async UniTask OnRoomKickPlayerS2CServerCallBack(RoomKickPlayerS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1029);
			RoomStateType roomStateType = roomController.roomStateType;
			if (roomStateType != RoomStateType.RUNNING && roomStateType != RoomStateType.SETTLEMENT)
			{
				await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.RoomList, null, UIPanelType.MatchEntrance);
			}
			ClearRoomInfo();
		}
		else
		{
			curRoomInfo.RemovePlayerInfo(model.PlayerId);
			signal.roomPlayerChange.Dispatch();
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestAbdicationC2S(long _playerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RoomAbdicationC2S.RoomAbdicationC2SCall(new RoomAbdicationC2S
		{
			PlayerId = _playerId
		});
	}

	private async UniTask OnRoomAbdicationS2CServerCallBack(RoomAbdicationS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			curRoomInfo.GetPlayerById(model.MasterId)?.UpdateRoomReady(isReady: false);
			curRoomInfo.UpdateMasterId(model.MasterId);
			signal.masterChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestRoomReadyC2S(bool isReady)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RoomReadyC2S.RoomReadyC2SCall(new RoomReadyC2S
		{
			IsReady = isReady
		});
	}

	private async UniTask OnRoomReadyS2CServerCallBack(RoomReadyS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		if (curRoomInfo == null || model.PlayerId == 0L)
		{
			Debug.LogError($"curRoomInfo是否为空:{curRoomInfo == null}， 玩家Id:{model.PlayerId}");
			return;
		}
		RoomPlayer playerById = curRoomInfo.GetPlayerById(model.PlayerId);
		if (playerById != null)
		{
			playerById.UpdateRoomReady(model.IsReady);
			signal.roomPlayerChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public void CreateGuidanceRoom(Room info)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			roomController.CreateRoom(info);
			SimpleSingletonProvider<UIManager>.inst.UnLoadLoadingTip();
		}
	}

	public async UniTask LoadGuidanceScene()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Clear();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
		signal.refreshHeroList.Dispatch(curRoomInfo.Box, t2: false);
		SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1005, 2.5f);
		roomController.SwitchRoomState(null, RoomStateType.READY);
		signal.UpdateHeroProgress.Dispatch();
		signal.HeroLoadReady.Dispatch();
		await SimpleSingletonProvider<InternalAssetManager>.inst.PreLoadBattleAsset();
		roomController.SwitchRoomState(null, RoomStateType.RUNNING);
		RoomInfo localRoom = roomController.localRoom;
		bool initialization = localRoom != null && localRoom.MapType == 10;
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleStart(initialization);
	}

	public void ClearRoomInfo(bool _voluntaryWithdrawal = false)
	{
		ResetInvalidClickState();
		roomController.roomStateType = RoomStateType.NONE;
		RoomInfo localRoom = roomController.localRoom;
		if (localRoom != null && localRoom.IsSingleGameModel())
		{
			voluntaryWithdrawal = false;
		}
		else
		{
			voluntaryWithdrawal = _voluntaryWithdrawal;
		}
	}

	public void GiveUpGame(System.Action onComplete, ExitRoomC2S.Types.ForceExitType exitType)
	{
		RoomStateType? roomStateType = roomController?.roomStateType;
		if (!roomStateType.HasValue || roomStateType != RoomStateType.RUNNING)
		{
			onComplete?.Invoke();
			return;
		}
		RequestExitRoomC2S(exitType).OnFinishedOnly.AddOnce(delegate
		{
			ExitRunningRoom(onComplete);
		});
	}

	public void ExitRunningRoom(System.Action onComplete)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ClearRPC();
		ClearRoomInfo(_voluntaryWithdrawal: true);
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelAllTask();
		SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.CloseBattleUI();
		onComplete?.Invoke();
	}

	private async UniTask OnSingleCampaignS2CServerCallBack(SingleCampaignS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			roomController.CreateRoom(model.Room);
			roomController.SwitchRoomState(model.Room, RoomStateType.CHOICE);
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
		}
	}

	public RPCAsyncResult CreateCampaignRoom(int mapId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SingleCampaignC2S.SingleCampaignC2SCall(new SingleCampaignC2S
		{
			LevelId = mapId
		});
	}

	public RPCAsyncResult RequestApplyChangeSlot(long targetPlayerId, bool isCancel)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ApplyChangeSlotC2S.ApplyChangeSlotC2SCall(new ApplyChangeSlotC2S
		{
			TargetId = targetPlayerId,
			IsCancel = isCancel
		});
	}

	private async UniTask OnApplyChangeSlotS2CServerCallBackAsync(ApplyChangeSlotS2C model, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || curRoomInfo == null || !curRoomInfo.IsPVE() || roomController.roomStateType != RoomStateType.CHOICE || errId != 0 || (model.PlayerId == 0L && model.TargetId == 0L))
		{
			return;
		}
		int num = roomController.ChangeSlotData.FindIndex((ApplySlotData x) => x.applyId == model.PlayerId && x.targetId == model.TargetId);
		if (num == -1)
		{
			if (!model.IsCancel)
			{
				roomController.ChangeSlotData.Add(new ApplySlotData
				{
					applyId = model.PlayerId,
					targetId = model.TargetId,
					Reject = false
				});
			}
		}
		else
		{
			ApplySlotData applySlotData = roomController.ChangeSlotData[num];
			if (model.IsCancel)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(applySlotData.targetId))
				{
					RoomPlayer playerById = curRoomInfo.GetPlayerById(applySlotData.applyId);
					if (playerById != null)
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1116.GetLocal(UIStringType.Message), playerById.GetNick()));
					}
				}
				roomController.ChangeSlotData.RemoveAt(num);
			}
			else
			{
				roomController.ChangeSlotData[num] = new ApplySlotData
				{
					applyId = model.PlayerId,
					targetId = model.TargetId,
					Reject = applySlotData.Reject
				};
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.applyChangeSlot.Dispatch();
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestOpsChangeSlot(bool isAgree, long applyPlayerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.OpsChangeSlotC2S.OpsChangeSlotC2SCall(new OpsChangeSlotC2S
		{
			IsAgree = isAgree,
			ApplyId = applyPlayerId
		});
	}

	private async UniTask OnOpsChangeSlotS2CServerCallBackAsync(OpsChangeSlotS2C model, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || curRoomInfo == null || !curRoomInfo.IsPVE() || roomController.roomStateType != RoomStateType.CHOICE || errId != 0)
		{
			return;
		}
		int num = roomController.ChangeSlotData.FindIndex((ApplySlotData x) => x.applyId == model.ApplyId && x.targetId == model.OpsPlayerId);
		if (num == -1)
		{
			RoomPlayer playerById = curRoomInfo.GetPlayerById(model.OpsPlayerId);
			if (playerById == null || !playerById.IsBot)
			{
				Debug.LogError($"未找到换位申请数据：申请人={model.ApplyId}，被申请人={model.OpsPlayerId}。为避免状态卡住，执行兜底处理。");
			}
			if (model.IsAgree)
			{
				AgreeChangeSlot(model);
			}
			else
			{
				roomController.ChangeSlotData.Add(new ApplySlotData
				{
					applyId = model.ApplyId,
					targetId = model.OpsPlayerId,
					Reject = true
				});
			}
		}
		else if (model.IsAgree)
		{
			AgreeChangeSlot(model);
		}
		else
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.ApplyId))
			{
				RoomPlayer playerById2 = curRoomInfo.GetPlayerById(model.OpsPlayerId);
				if (playerById2 != null)
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1117.GetLocal(UIStringType.Message), playerById2.GetNick()));
				}
			}
			ApplySlotData value = roomController.ChangeSlotData[num];
			value.Reject = true;
			roomController.ChangeSlotData[num] = value;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.applyChangeSlot.Dispatch();
		await UniTask.CompletedTask;
	}

	private void AgreeChangeSlot(OpsChangeSlotS2C model)
	{
		if (ApplyFinalSlotData(model))
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.OpsPlayerId) || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.ApplyId))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1118);
			}
			ClearRelatedApplySlotDataAfterChangeSlotSuccess(model.ApplyId, model.OpsPlayerId);
		}
	}

	private bool ApplyFinalSlotData(OpsChangeSlotS2C model)
	{
		foreach (KeyValuePair<long, int> item in model.FinalSlot)
		{
			item.Deconstruct(out var key, out var value);
			long num = key;
			int slot = value;
			RoomPlayer playerById = curRoomInfo.GetPlayerById(num);
			if (playerById == null)
			{
				Debug.LogError($"无法处理换位：无法通过玩家ID {num} 找到房间内玩家");
				return false;
			}
			playerById.UpdateRoomSlot(slot);
		}
		return true;
	}

	private void ClearRelatedApplySlotDataAfterChangeSlotSuccess(long applyId, long opsPlayerId)
	{
		List<ApplySlotData> changeSlotData = roomController.ChangeSlotData;
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		bool flag = false;
		for (int num = changeSlotData.Count - 1; num >= 0; num--)
		{
			ApplySlotData applySlotData = changeSlotData[num];
			if (applySlotData.applyId == applyId || applySlotData.targetId == applyId || applySlotData.applyId == opsPlayerId || applySlotData.targetId == opsPlayerId)
			{
				if (applySlotData.applyId == playerID && (applySlotData.targetId == applyId || applySlotData.targetId == opsPlayerId) && !applySlotData.Reject)
				{
					flag = true;
				}
				changeSlotData.RemoveAt(num);
			}
		}
		if (playerID != applyId && playerID != opsPlayerId && flag)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1119);
		}
	}

	public void TrackInvalidFrequentClick()
	{
		if (!ShouldCheckInvalidFrequentClick())
		{
			ResetInvalidClickState();
			return;
		}
		float unscaledTime = Time.unscaledTime;
		if (unscaledTime < _invalidClickDetectCooldownUntil)
		{
			return;
		}
		if (unscaledTime - _lastInvalidClickTime > 0.5f)
		{
			_invalidContinuousClickCount = 0;
		}
		if (unscaledTime - _lastInvalidClickTime <= 0.25f)
		{
			_invalidContinuousClickCount++;
		}
		else
		{
			_invalidContinuousClickCount = 1;
		}
		_lastInvalidClickTime = unscaledTime;
		if (_invalidContinuousClickCount > 100)
		{
			ResetInvalidClickState();
			_giveUpAttemptCount++;
			if (_giveUpAttemptCount > 9)
			{
				_giveUpAttemptCount = 0;
				long valueOrDefault = (roomController?.localRoom?.Id).GetValueOrDefault();
				long num = SimpleSingletonProvider<GameLogicManager>.inst.account?.GetPlayerID() ?? 0;
				Debug.LogError($"Player:{num} RoomId：{valueOrDefault}, InvalidClick");
				_invalidClickDetectCooldownUntil = unscaledTime + 10f;
			}
		}
	}

	private void ResetInvalidClickState()
	{
		_lastInvalidClickTime = -10f;
		_invalidContinuousClickCount = 0;
	}

	private bool ShouldCheckInvalidFrequentClick()
	{
		RoomStateType? roomStateType = roomController?.roomStateType;
		if (!roomStateType.HasValue || roomStateType != RoomStateType.RUNNING)
		{
			return false;
		}
		long num = SimpleSingletonProvider<GameLogicManager>.inst.account?.GetPlayerID() ?? 0;
		if (num == 0L)
		{
			return false;
		}
		return !SimpleSingletonProvider<GameLogicManager>.inst.battle.IsCurrentPlayer(num);
	}

	public void RequestActionOverTimeLogC2S(int actionProtocolId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ActionOverTimeLogC2S.ActionOverTimeLogC2SCall(new ActionOverTimeLogC2S
		{
			ActionType = actionProtocolId
		});
	}

	private async UniTask OnActionOverTimeLogS2CServerCallBackAsync(ActionOverTimeLogS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnRoomRoundAddTermS2CServerCallBack(RoomRoundAddTermS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			curRoomInfo?.TryAddRoomTerms(model.TermIds);
			await UniTask.CompletedTask;
		}
	}

	private void Connect_StartGame()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoomNotifyS2C.OnRoomNotifyS2CServerCallBackAsync = OnRoomNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RunningGameS2C.OnRunningGameS2CServerCallBackAsync = OnRunningGameS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncRoomS2C.OnSyncRoomS2CServerCallBackAsync = OnSyncRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshRoomStateS2C.OnRefreshRoomStateS2CServerCallBackAsync = OnRefreshRoomStateS2CServerCallBack;
	}

	private void Disconnect_StartGame()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoomNotifyS2C.OnRoomNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RunningGameS2C.OnRunningGameS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncRoomS2C.OnSyncRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshRoomStateS2C.OnRefreshRoomStateS2CServerCallBackAsync = null;
	}

	public void RequestRefreshRoomC2S(int progress)
	{
		if (curRoomInfo == null || curRoomInfo.State != Room.Types.State.Running)
		{
			MonoSingletonProvider<NetManager>.inst.RPC.RefreshRoomStateC2S.RefreshRoomStateC2SCall(new RefreshRoomStateC2S
			{
				Progress = progress
			});
		}
	}

	private async UniTask OnRefreshRoomStateS2CServerCallBack(RefreshRoomStateS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (roomInfo != null)
			{
				roomInfo.State = Room.Types.State.None;
			}
		}
		else
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnRoomNotifyS2CServerCallBack(RoomNotifyS2C model, int errId, bool isdispatch)
	{
		if (errId == 0 && model.Room != null)
		{
			roomController.SwitchRoomState(model.Room, RoomStateType.READY);
			signal.refreshHeroList.Dispatch(model.Room.Box, t2: true);
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1005, 2.5f);
			signal.UpdateHeroProgress.Dispatch();
			signal.HeroLoadReady.Dispatch();
			SimpleSingletonProvider<InternalAssetManager>.inst.PreLoadBattleAsset().Forget();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnRunningGameS2CServerCallBack(RunningGameS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model.Room != null)
		{
			if (model.Room.State == Room.Types.State.Ready1)
			{
				roomController.SwitchRoomState(model.Room, RoomStateType.READY);
				signal.UpdateHeroProgress.Dispatch();
			}
			if (model.Room.State == Room.Types.State.Running)
			{
				roomController.SwitchRoomState(model.Room, RoomStateType.RUNNING);
				signal.UpdateHeroProgress.Dispatch();
				await SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleStart(initialization: true);
			}
		}
	}

	public void RequestSyncRoomC2S(long _curRoomId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SyncRoomC2S.SyncRoomC2SCall(new SyncRoomC2S
		{
			RoomId = _curRoomId
		});
	}

	private async UniTask OnSyncRoomS2CServerCallBack(SyncRoomS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model.Room == null)
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home)
			{
				await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.Home, null);
			}
			else
			{
				Debug.LogError($"同步房间 下发错误码：{errId}, 当前场景类型：{SimpleSingletonProvider<SceneManager>.inst.currentType.Value}");
			}
			return;
		}
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			Debug.LogWarning("[ReplaySession] 收到真实房间同步时仍处于回放状态，正在清理残留会话。");
			SimpleSingletonProvider<GameLogicManager>.inst.replay.ExitReplay();
		}
		roomController.Dispose();
		SimpleSingletonProvider<GameLogicManager>.inst.SetConnectRoomId(model.Room.Id);
		if (!IsInRoom)
		{
			roomController.CreateRoom(model.Room);
		}
		if (model.Room.State == Room.Types.State.Wait)
		{
			roomController.SwitchRoomState(model.Room, RoomStateType.WAIT);
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.RoomWait, null);
		}
		else if (model.Room.State == Room.Types.State.Running)
		{
			roomController.SwitchRoomState(model.Room, RoomStateType.RUNNING);
			await SyncRunningGame(roomController.localRoom);
		}
		if (SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.ResetConnectRoomId();
	}

	public async UniTask SyncRunningGame(RoomInfo room)
	{
		if (room.State != Room.Types.State.Running)
		{
			return;
		}
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
			await SimpleSingletonProvider<InternalAssetManager>.inst.PreLoadBattleAsset();
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleStart(initialization: false);
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady = false;
			SimpleSingletonProvider<ActionListener>.inst.Dispose();
			OperationTimer.Dispose();
			if (BattleSceneController.inst.MapSceneIndex != room.MapIndex)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.map.MapSceneChange();
			}
			else
			{
				SimpleSingletonProvider<EffectManager>.inst.Dispose();
				SimpleSingletonProvider<UIManager>.inst.Fight.Hide();
				SimpleSingletonProvider<UIManager>.inst.CloseAllUnFightWin();
				SimpleSingletonProvider<RoadLineManager>.inst.DestroyRoad();
				SimpleSingletonProvider<MoveArrowManager>.inst.CloseArrow();
				SimpleSingletonProvider<BuffEffectManager>.inst.Dispose();
				SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateHeroByReconnect(room.Players);
				SimpleSingletonProvider<UIManager>.inst.UnLoadPanel();
				await SimpleSingletonProvider<GameLogicManager>.inst.battle.ReadyBattleUI();
				await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateMonsterByReconnect(room.Monsters);
				SimpleSingletonProvider<SummonManager>.inst.Dispose();
				SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.Dispose();
				await SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.UpdateSummonData(room.info, Show: false);
				SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.UpdatePlayerAttrCom();
				await BuildChessboardData();
			}
			SimpleSingletonProvider<LandManager>.inst.MapGimmickManager?.Initialize();
		}
		MonoSingletonProvider<NetManager>.inst.RPC.DealReconnectCache();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady = true;
	}

	public async UniTask BuildChessboardData()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.assistVote.BuildChessboardData();
		if (curRoomInfo != null)
		{
			if (curRoomInfo.info.Battle != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.fight.UpdateFightData(curRoomInfo.info.Battle);
				await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
				await SimpleSingletonProvider<UIManager>.inst.Fight.ReadyFight(curRoomInfo.info.Battle.Attacker.PlayerId, curRoomInfo.info.Battle.Defender.PlayerId);
				return;
			}
			if (curRoomInfo.info.VoteInfo != null)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.assistVote.TryShowVoteAfterReconnect(curRoomInfo.info.VoteInfo);
			}
		}
		BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
		if (battleShowDirector != null && battleShowDirector.gameObject != null && battleShowDirector.gameObject.activeSelf)
		{
			BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = 0f;
			battleShowDirector.CloseBattlePlatform();
			await SimpleSingletonProvider<UIManager>.inst.Fight.CloseFightWin();
			await UniTask.DelayFrame(1);
			BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = BattleConfig.CameraBlendTime;
		}
	}
}
