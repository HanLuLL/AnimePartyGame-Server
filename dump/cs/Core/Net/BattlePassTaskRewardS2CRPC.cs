using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassTaskRewardS2CRPC
{
	public delegate UniTask OnBattlePassTaskRewardS2CServerDelegate(BattlePassTaskRewardS2C model, int errId, bool isDispatch);

	public OnBattlePassTaskRewardS2CServerDelegate OnBattlePassTaskRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassTaskRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassTaskRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassTaskRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassTaskRewardS2C model = param.ReadObject<BattlePassTaskRewardS2C>();
		await OnBattlePassTaskRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
