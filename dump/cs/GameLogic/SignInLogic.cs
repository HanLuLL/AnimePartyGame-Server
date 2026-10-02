using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class SignInLogic : IRPCSync
{
	private const int _4AM = 4;

	private const int NumberOfActiveDays = 7;

	private Player _player;

	public SignInInfoConfigure SignInInfoConfigure { get; private set; }

	public void InitFromServer(Player player)
	{
		_player = player;
	}

	public int GetSignInByActivityId(int activityId)
	{
		return (int)((double)activityId * 0.1);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetSignInRewardS2C.OnGetSignInRewardS2CServerCallBackAsync = OnGetSignInRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SignInRewardS2C.OnSignInRewardS2CServerCallBackAsync = OnSignInRewardS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetSignInRewardS2C.OnGetSignInRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SignInRewardS2C.OnSignInRewardS2CServerCallBackAsync = null;
	}

	private async UniTask OnGetSignInRewardS2CServerCallBack(GetSignInRewardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			Debug.LogError($"请求签到数据错误：{errId}");
			return;
		}
		Debug.Log($"[签到] 服务器返回签到数据: ActivityId={model.SignInReward.ActivityId}, SignInCount={model.SignInReward.SignInCount}");
		_player.SignInReward[model.SignInReward.ActivityId] = model.SignInReward;
		Debug.Log("[签到] 当前玩家所有签到数据Keys: [" + string.Join(", ", _player.SignInReward.Keys) + "]");
		await UniTask.CompletedTask;
	}

	private async UniTask OnSignInRewardS2CServerCallBack(SignInRewardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		Debug.Log($"[签到] 服务器同步所有签到数据，数量: {model.SignInReward.Count}");
		_player.SignInReward.Clear();
		foreach (var (num2, signInReward2) in model.SignInReward)
		{
			Debug.Log($"[签到] 同步签到数据: ActivityId={num2}, SignInCount={signInReward2.SignInCount}");
			_player.SignInReward.Add(num2, signInReward2);
		}
		Debug.Log("[签到] 同步后玩家所有签到数据Keys: [" + string.Join(", ", _player.SignInReward.Keys) + "]");
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult SignIn(int signInId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetSignInRewardC2S.GetSignInRewardC2SCall(new GetSignInRewardC2S
		{
			ActivityId = signInId
		});
	}

	public int? GetSignInCount(int signInId)
	{
		if (!_player.SignInReward.TryGetValue(signInId, out var value))
		{
			return null;
		}
		return value.SignInCount;
	}

	public bool IsSignInAllCompleted(int signInId)
	{
		if (!_player.SignInReward.TryGetValue(signInId, out var value))
		{
			Debug.LogWarning($"[签到] 无signInId={signInId}的签到数据，未完成");
			return false;
		}
		return value.SignInCount >= 7;
	}

	public bool CanSignIn(int signInId)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		Debug.Log($"[签到] CanSignIn检查: signInId={signInId}");
		if (!StaticConfigure.SignIn.InfoDict.TryGetValue(signInId, out var value))
		{
			Debug.LogWarning($"[签到] SignIn.InfoDict中找不到signInId={signInId}的配置");
			return false;
		}
		if (!TimeHelper.ValidityTime(value.BeginTime, value.EndTime))
		{
			Debug.LogWarning($"[签到] signInId={signInId} 活动时间不在有效期内");
			return false;
		}
		if (!_player.SignInReward.TryGetValue(signInId, out var value2))
		{
			Debug.LogWarning(string.Format("[签到] 玩家签到数据中找不到signInId={0}，当前所有Keys: [{1}]", signInId, string.Join(", ", _player.SignInReward.Keys)));
			return false;
		}
		if (value2.SignInCount >= 7)
		{
			return false;
		}
		if (_player.SignInReward.ContainsKey(signInId) && value2.SignInCount == 0)
		{
			return true;
		}
		DateTime dateTime = (value2.UpdateTime * 1000).StampMillisecondsToDateTime();
		if (dateTime.Hour < 4 && serverTime >= dateTime.Date.AddHours(4.0))
		{
			return true;
		}
		if (dateTime.Hour >= 4 && serverTime >= dateTime.Date.AddDays(1.0).AddHours(4.0))
		{
			return true;
		}
		return false;
	}

	public bool GetCanSignInToday(int signInId)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		SignInReward signInData = GetSignInData(signInId);
		if (signInData == null)
		{
			Debug.LogWarning($"[签到] GetCanSignInToday: signInId={signInId} 没有签到数据");
			return false;
		}
		DateTime dateTime = (signInData.UpdateTime * 1000).StampMillisecondsToDateTime();
		if (dateTime.Hour < 4 && serverTime >= dateTime.Date.AddHours(4.0))
		{
			return true;
		}
		if (dateTime.Hour >= 4 && serverTime >= dateTime.Date.AddDays(1.0).AddHours(4.0))
		{
			return true;
		}
		return false;
	}

	public SignInReward GetSignInData(int signInId)
	{
		return _player.SignInReward.GetValueOrDefault(signInId);
	}

	public int? GetSignInID()
	{
		foreach (var (value, signInInfoConfigure2) in StaticConfigure.SignIn.InfoDict)
		{
			if (TimeHelper.ValidityTime(signInInfoConfigure2.BeginTime, signInInfoConfigure2.EndTime))
			{
				SignInInfoConfigure = signInInfoConfigure2;
				return value;
			}
		}
		return null;
	}
}
