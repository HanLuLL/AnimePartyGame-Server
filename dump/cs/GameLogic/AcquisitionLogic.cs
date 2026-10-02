using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class AcquisitionLogic : IRPCSync
{
	public AcquisitionData AcquisitionData { get; private set; }

	public void InitFromServer(InviteInfo inviteInfo)
	{
		AcquisitionData = new AcquisitionData(inviteInfo);
	}

	public bool GetSystemStatus()
	{
		if (!AcquisitionData.GetAcceptInviteTaskStatus())
		{
			return AcquisitionData.GetInviteTaskStatus();
		}
		return true;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionS2C.OnAcquisitionS2CServerCallBackAsync = OnAcquisitionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionRewardS2C.OnAcquisitionRewardS2CServerCallBackAsync = OnAcquisitionRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.InviteSuccessS2C.OnInviteSuccessS2CServerCallBackAsync = OnInviteSuccessS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.InviteInfoNotifyS2C.OnInviteInfoNotifyS2CServerCallBackAsync = OnInviteInfoNotifyS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionS2C.OnAcquisitionS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionRewardS2C.OnAcquisitionRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.InviteSuccessS2C.OnInviteSuccessS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.InviteInfoNotifyS2C.OnInviteInfoNotifyS2CServerCallBackAsync = null;
	}

	public int GetProgress(int activityId, int taskId, int conditionType)
	{
		return AcquisitionData.GetProgress(taskId, conditionType);
	}

	public bool GetStatus(int activityId, int taskId)
	{
		return AcquisitionData.GetStatus(taskId);
	}

	public RPCAsyncResult RequestAcquisitionC2S(int activityId, string inviteCode)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionC2S.AcquisitionC2SCall(new AcquisitionC2S
		{
			ActivityId = activityId,
			InviteCode = inviteCode
		});
	}

	private async UniTask OnAcquisitionS2CServerCallBack(AcquisitionS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestAcquisitionRewardC2S(int taskId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.AcquisitionRewardC2S.AcquisitionRewardC2SCall(new AcquisitionRewardC2S
		{
			TaskId = taskId
		});
	}

	private async UniTask OnAcquisitionRewardS2CServerCallBack(AcquisitionRewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnInviteSuccessS2CServerCallBack(InviteSuccessS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			AcquisitionData.UpdateInviteFinishCount(model.Num);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is TaskPanel taskPanel)
			{
				taskPanel.RefreshAcquisition();
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnInviteInfoNotifyS2CServerCallBack(InviteInfoNotifyS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			AcquisitionData.UpdateInviteInfo(model);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is TaskPanel taskPanel)
			{
				taskPanel.RefreshAcquisition();
			}
			else if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.task.RegisterRed();
			}
			await UniTask.CompletedTask;
		}
	}
}
