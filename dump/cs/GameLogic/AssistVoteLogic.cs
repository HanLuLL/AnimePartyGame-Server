using System.Collections.Generic;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class AssistVoteLogic : IRPCSync
{
	public AssistVoteData AssistVote;

	public readonly ActivityVoteData ActivityVoteData = new ActivityVoteData();

	public PVEMissionVoteConfigure LeftMonster => GetVoteConfig(1);

	public PVEMissionVoteConfigure RightMonster => GetVoteConfig(0);

	public PVEMissionVoteConfigure CenterMonster => GetVoteConfig(2);

	private PVEMissionVoteConfigure GetVoteConfig(int targetIndex)
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return null;
		}
		RepeatedField<PVEMissionVoteConfigure> votes = StaticConfigure.PVEMission.Votes;
		int num = -1;
		for (int i = 0; i < votes.Count; i++)
		{
			if (votes[i].MapId == curRoomInfo.MapId)
			{
				num = i;
				break;
			}
		}
		return votes.GetSafeByIndex(num + targetIndex);
	}

	public void BuildChessboardData()
	{
		AssistVote = new AssistVoteData();
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.VoteS2C.OnVoteS2CServerCallBackAsync = OnVoteS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.PkAfterVoteS2C.OnPkAfterVoteS2CServerCallBackAsync = OnPkAfterVoteS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.VoteSelectS2C.OnVoteSelectS2CServerCallBackAsync = OnVoteSelectS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.CampScoreS2C.OnCampScoreS2CServerCallBackAsync = OnCampScoreS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.VoteS2C.OnVoteS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PkAfterVoteS2C.OnPkAfterVoteS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.VoteSelectS2C.OnVoteSelectS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CampScoreS2C.OnCampScoreS2CServerCallBackAsync = null;
	}

	private async UniTask OnPkAfterVoteS2CServerCallBackAsync(PkAfterVoteS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		RoomInfo localRoom = roomController.localRoom;
		if (localRoom != null && localRoom.info != null && roomController.localRoom.info.CampId != 0)
		{
			if (SimpleSingletonProvider<UIManager>.inst.assistVote.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.assistVote.Hide();
			}
			AssistVote = new AssistVoteData();
		}
		else
		{
			if (errid != 0)
			{
				return;
			}
			AssistVoteBaseWindow assistVoteWindow = GetAssistVoteWindow();
			if (assistVoteWindow == null)
			{
				return;
			}
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager015 mapGimmickManager)
			{
				mapGimmickManager.AfterAssistVote(model.CampId);
			}
			long key;
			if (model.DicePoint != null && model.DicePoint.Count > 0)
			{
				foreach (KeyValuePair<long, int> item in model.DicePoint)
				{
					item.Deconstruct(out key, out var value);
					long playerId = key;
					int point = value;
					AssistVote.UpdateVotePoint(playerId, point, AssistVoteStatus.DicePK);
				}
				await assistVoteWindow.RefreshVoteData(showPoint: true);
			}
			else
			{
				foreach (KeyValuePair<long, PlayerVoteData> item2 in AssistVote.PlayerVoteDict)
				{
					item2.Deconstruct(out key, out var value2);
					value2.VoteStatus = AssistVoteStatus.Over;
				}
				await assistVoteWindow.VoteOver();
			}
			AssistVote = new AssistVoteData();
		}
	}

	public RPCAsyncResult RequestVoteC2S(long sn)
	{
		OperationTimer.CancelOperatTimer(sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.VoteC2S.VoteC2SCall(new VoteC2S
		{
			Info = new ActionInfo
			{
				Sn = sn,
				UseTime = OperationTimer.GetExtraTime()
			}
		});
	}

	private async UniTask OnVoteS2CServerCallBackAsync(VoteS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			AssistVote.UpdateVoteData(model.PlayerId, model.SelectId, 0, affirm: true, AssistVoteStatus.WaitVoteResult);
			AssistVoteBaseWindow assistVoteWindow = GetAssistVoteWindow();
			if (assistVoteWindow != null)
			{
				assistVoteWindow.ResetUI(model.PlayerId, model.SelectId);
				await assistVoteWindow.RefreshVoteData(showPoint: false);
			}
		}
	}

	public RPCAsyncResult RequestVoteSelectC2S(int selectId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.VoteSelectC2S.VoteSelectC2SCall(new VoteSelectC2S
		{
			SelectId = selectId
		});
	}

	private async UniTask OnVoteSelectS2CServerCallBackAsync(VoteSelectS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			AssistVote.UpdateVoteData(model.PlayerId, model.SelectId, 0, affirm: false, AssistVoteStatus.Vote);
			AssistVoteBaseWindow assistVoteWindow = GetAssistVoteWindow();
			if (assistVoteWindow != null)
			{
				await assistVoteWindow.RefreshVoteData(showPoint: false);
			}
		}
	}

	public async UniTask TryShowVoteAfterReconnect(RepeatedField<PlayerVoteInfo> VoteInfos)
	{
		if (VoteInfos == null || VoteInfos.Count <= 0)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < VoteInfos.Count; i++)
		{
			if (flag)
			{
				flag = VoteInfos[i].Affirm;
			}
			AssistVoteStatus voteStatus = AssistVoteStatus.None;
			if (VoteInfos[i].Affirm && VoteInfos[i].Point > 0)
			{
				voteStatus = AssistVoteStatus.DicePK;
			}
			AssistVote.UpdateVoteData(VoteInfos[i].PlayerId, VoteInfos[i].SelectId, VoteInfos[i].Point, VoteInfos[i].Affirm, voteStatus);
		}
		if (!flag)
		{
			AssistVoteBaseWindow assistVoteWindow = GetAssistVoteWindow();
			if (assistVoteWindow != null)
			{
				await assistVoteWindow.TryShowAssistVoteAfterReconnect(VoteInfos[0].Affirm && VoteInfos[0].Point > 0);
			}
		}
	}

	public RPCAsyncResult RequestCampScoreC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.CampScoreC2S.CampScoreC2SCall(new CampScoreC2S());
	}

	private async UniTask OnCampScoreS2CServerCallBackAsync(CampScoreS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			ActivityVoteData.RefreshData(model.RefreshTime, model.CampId, model.CampScorePercent);
			await UniTask.CompletedTask;
		}
	}

	public AssistVoteBaseWindow GetAssistVoteWindow()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			Debug.LogError("当前房间信息不存在， 请检查");
			return null;
		}
		int mapId = curRoomInfo.MapId;
		switch (mapId)
		{
		case 82013:
			return SimpleSingletonProvider<UIManager>.inst.assistVote;
		case 82015:
			return SimpleSingletonProvider<UIManager>.inst.AssistVoteS7;
		default:
			Debug.LogError($"当前房间地图：{mapId}, 没有对应的投票窗口");
			return null;
		}
	}

	public async UniTask TryShowAssistVote(Action action)
	{
		AssistVoteBaseWindow assistVoteWindow = GetAssistVoteWindow();
		if (assistVoteWindow != null)
		{
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager017 mapGimmickManager)
			{
				mapGimmickManager.TryPlayBGM(170).Forget();
			}
			VoteC2S voteC2S = ByteBuf.ReadObject<VoteC2S>(action.Data.ToByteArray());
			if (voteC2S?.VoteIds == null || voteC2S.VoteIds.Count == 0)
			{
				Debug.LogError("投票目标数据为空!");
			}
			else
			{
				await assistVoteWindow.TryShowAssistVote(action);
			}
		}
	}
}
