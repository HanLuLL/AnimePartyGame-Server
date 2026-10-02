using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class BattlePassLogic : IRPCSync
{
	public BattlePassSignal signal = new BattlePassSignal();

	public BattlePassData BattlePassData;

	private int _TaskId;

	public void InitFromServer(BattlePass BattlePass)
	{
		UpdateBattlePass(BattlePass);
	}

	public void UpdateBattlePass(BattlePass BattlePass)
	{
		if (BattlePass != null)
		{
			BattlePassInfoConfigure value;
			if (BattlePassData != null && BattlePassData.BattlePassInfo.Id == BattlePass.DefId)
			{
				BattlePassData.UpdateBattlePass(BattlePass);
			}
			else if (StaticConfigure.BattlePass.InfoDict.TryGetValue(BattlePass.DefId, out value))
			{
				BattlePassData = new BattlePassData(value, BattlePass);
			}
		}
	}

	public int GetProgress(int battlePassId, int taskConfigId, int taskConfigConditionType)
	{
		if (BattlePassData.BattlePassInfo.Id != battlePassId)
		{
			return 0;
		}
		return BattlePassData.GetProgress(taskConfigId, taskConfigConditionType);
	}

	public bool GetStatus(int battlePassId, int taskConfigId)
	{
		if (BattlePassData.BattlePassInfo.Id != battlePassId)
		{
			return false;
		}
		return BattlePassData.GetStatus(taskConfigId);
	}

	public int GetFinishRewardLV(BattlePassGearType gearType)
	{
		return BattlePassData.GetFinishRewardLV(gearType);
	}

	public bool GetGearStatus(BattlePassGearType gearType)
	{
		return BattlePassData.gearType >= gearType;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassInfoS2C.OnBattlePassInfoS2CServerCallBackAsync = OnBattlePassInfoS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassLvS2C.OnBattlePassLvS2CServerCallBackAsync = OnBattlePassLvS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassTaskInfoS2C.OnBattlePassTaskInfoS2CServerCallBackAsync = OnBattlePassTaskInfoS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassUpdateTaskS2C.OnBattlePassUpdateTaskS2CServerCallBackAsync = OnBattlePassUpdateTaskS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassGetRewardS2C.OnBattlePassGetRewardS2CServerCallBackAsync = OnBattlePassGetRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassTaskRewardS2C.OnBattlePassTaskRewardS2CServerCallBackAsync = OnBattlePassTaskRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassUpLvS2C.OnBattlePassUpLvS2CServerCallBackAsync = OnBattlePassUpLvS2CServerCallBackA;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassBuyS2C.OnBattlePassBuyS2CServerCallBackAsync = OnBattlePassBuyS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassInfoS2C.OnBattlePassInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassLvS2C.OnBattlePassLvS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassTaskInfoS2C.OnBattlePassTaskInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassUpdateTaskS2C.OnBattlePassUpdateTaskS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassGetRewardS2C.OnBattlePassGetRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassTaskRewardS2C.OnBattlePassTaskRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassUpLvS2C.OnBattlePassUpLvS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattlePassBuyS2C.OnBattlePassBuyS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestBattlePassGetRewardC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.BattlePassGetRewardC2S.BattlePassGetRewardC2SCall(new BattlePassGetRewardC2S());
	}

	private async UniTask OnBattlePassGetRewardS2CServerCallBack(BattlePassGetRewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			BattlePassData.RecordBattlePassRewardLocal();
			signal.updateBattlePass.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestBattlePassTaskRewardC2S(int taskId)
	{
		_TaskId = taskId;
		return MonoSingletonProvider<NetManager>.inst.RPC.BattlePassTaskRewardC2S.BattlePassTaskRewardC2SCall(new BattlePassTaskRewardC2S
		{
			DefId = taskId
		});
	}

	private async UniTask OnBattlePassTaskRewardS2CServerCallBack(BattlePassTaskRewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			BattlePassData.UpdateTaskFinishIds(_TaskId);
			signal.updateTask.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestBattlePassUpLvC2S(int LV)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.BattlePassUpLvC2S.BattlePassUpLvC2SCall(new BattlePassUpLvC2S
		{
			Count = LV
		});
	}

	private async UniTask OnBattlePassUpLvS2CServerCallBackA(BattlePassUpLvS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBattlePassLvS2CServerCallBack(BattlePassLvS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && BattlePassData != null)
		{
			BattlePassData.UpdateLVExp(model.Lv, model.Exp);
			signal.updateBattlePass.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBattlePassUpdateTaskS2CServerCallBack(BattlePassUpdateTaskS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && BattlePassData != null)
		{
			BattlePassData.UpdateTaskAchieve(model.Task, Noop: false);
			signal.updateTask.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBattlePassTaskInfoS2CServerCallBack(BattlePassTaskInfoS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && BattlePassData != null)
		{
			BattlePassData.UpdateTaskAchieve(model.Task, Noop: true);
			BattlePassData.UpdateTaskFinishIds(model.TaskRewardIs);
			signal.updateTask.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBattlePassInfoS2CServerCallBack(BattlePassInfoS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			UpdateBattlePass(model.Inf);
			signal.updateBattlePass.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBattlePassBuyS2CServerCallBack(BattlePassBuyS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (BattlePassData != null)
			{
				BattlePassData.gearType = (BattlePassGearType)model.Gear;
			}
			signal.updateGear.Dispatch();
			await UniTask.CompletedTask;
		}
	}
}
