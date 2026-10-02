using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class ComebackLogic : IRPCSync
{
	public readonly ComebackData Data = new ComebackData();

	public readonly ComebackSignal signal = new ComebackSignal();

	public readonly ReactiveProperty<bool> comebackRedSignal = new ReactiveProperty<bool>(initialValue: false);

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetReturnInfoS2C.OnGetReturnInfoS2CServerCallBackAsync = OnGetReturnInfoS2C;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnGiftClaimS2C.OnReturnGiftClaimS2CServerCallBackAsync = OnReturnGiftClaimS2C;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnSignInClaimS2C.OnReturnSignInClaimS2CServerCallBackAsync = OnReturnSignInClaimS2C;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnInfoS2C.OnReturnInfoS2CServerCallBackAsync = OnReturnInfoS2C;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnSurveyFinishS2C.OnReturnSurveyFinishS2CServerCallBackAsync = OnReturnSurveyFinishS2C;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetReturnInfoS2C.OnGetReturnInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnGiftClaimS2C.OnReturnGiftClaimS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnSignInClaimS2C.OnReturnSignInClaimS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnInfoS2C.OnReturnInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ReturnSurveyFinishS2C.OnReturnSurveyFinishS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestGetReturnInfo()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetReturnInfoC2S.GetReturnInfoC2SCall(new GetReturnInfoC2S());
	}

	public RPCAsyncResult RequestFreeGift()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ReturnGiftClaimC2S.ReturnGiftClaimC2SCall(new ReturnGiftClaimC2S());
	}

	public RPCAsyncResult RequestSignInClaim(int day, bool isAdvanced)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ReturnSignInClaimC2S.ReturnSignInClaimC2SCall(new ReturnSignInClaimC2S
		{
			ActId = day,
			IsAdvanced = isAdvanced
		});
	}

	public RPCAsyncResult RequestSurveyFinish()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ReturnSurveyFinishC2S.ReturnSurveyFinishC2SCall(new ReturnSurveyFinishC2S());
	}

	private async UniTask OnGetReturnInfoS2C(GetReturnInfoS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			HandleReturnInfoUpdate(model.ReturnInfo);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnReturnGiftClaimS2C(ReturnGiftClaimS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (Data.ReturnInfo != null)
			{
				Data.ReturnInfo.FreeGiftClaimed = model.FreeGiftClaimed;
			}
			signal.freeGiftClaimed.Dispatch(model.FreeGiftClaimed);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnReturnSignInClaimS2C(ReturnSignInClaimS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (Data.ReturnInfo != null)
			{
				Data.ReturnInfo.SignIn = model.SignIn;
			}
			signal.signInUpdated.Dispatch(model.SignIn);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnReturnInfoS2C(ReturnInfoS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			HandleReturnInfoUpdate(model.ReturnInfo);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnReturnSurveyFinishS2C(ReturnSurveyFinishS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (Data.ReturnInfo != null)
			{
				Data.ReturnInfo.SurveyState = model.SurveyState;
			}
			signal.surveyStateUpdated.Dispatch(model.SurveyState);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	private void HandleReturnInfoUpdate(ReturnInfo info)
	{
		if (info != null)
		{
			Player player = SimpleSingletonProvider<GameLogicManager>.inst.account?.GetPlayerInfo();
			if (player != null)
			{
				player.ReturnInfo = info;
			}
			signal.infoUpdated.Dispatch(info);
		}
	}

	public void RegisterRed()
	{
		ReturnInfo returnInfo = Data.ReturnInfo;
		if (returnInfo == null)
		{
			comebackRedSignal.Value = false;
			return;
		}
		bool flag = false;
		flag |= !returnInfo.FreeGiftClaimed;
		flag |= returnInfo.SurveyState == 1;
		flag |= HasClaimableSignIn(returnInfo.SignIn);
		flag |= HasClaimableTask();
		comebackRedSignal.Value = flag;
	}

	private bool HasClaimableSignIn(ReturnSignIn signIn)
	{
		if (signIn == null)
		{
			return false;
		}
		int unlockDay = signIn.UnlockDay;
		if (unlockDay <= 0)
		{
			return false;
		}
		RepeatedField<int> freeClaimedDays = signIn.FreeClaimedDays;
		RepeatedField<int> advClaimedDays = signIn.AdvClaimedDays;
		for (int i = 1; i <= unlockDay; i++)
		{
			if (freeClaimedDays == null || !freeClaimedDays.Contains(i))
			{
				return true;
			}
			if (signIn.AdvUnlocked && advClaimedDays != null && !advClaimedDays.Contains(i))
			{
				return true;
			}
		}
		return false;
	}

	private bool HasClaimableTask()
	{
		ActivityLogic activity = SimpleSingletonProvider<GameLogicManager>.inst.activity;
		ComebackParamsConfigure comebackParamsConfigure = StaticConfigure.Comeback?.ParamsDict?.GetValueOrDefault(1);
		if (activity == null || comebackParamsConfigure == null)
		{
			return false;
		}
		TaskActivityData taskActivityData = activity.GetTaskActivityData(comebackParamsConfigure.ActivityId);
		if (taskActivityData == null)
		{
			return false;
		}
		foreach (KeyValuePair<int, BaseTaskData> item in taskActivityData.taskDataDict)
		{
			BaseTaskData value = item.Value;
			if (value != null && value.ValidityTime() && !value._FinishStatus && ((!(value is MissionData missionData)) ? (!value.TaskRunning) : (missionData.AchieveProgress >= missionData.TaskTarget || missionData.Status == 2)))
			{
				return true;
			}
		}
		return false;
	}
}
