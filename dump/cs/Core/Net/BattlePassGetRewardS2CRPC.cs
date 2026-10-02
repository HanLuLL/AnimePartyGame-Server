using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassGetRewardS2CRPC
{
	public delegate UniTask OnBattlePassGetRewardS2CServerDelegate(BattlePassGetRewardS2C model, int errId, bool isDispatch);

	public OnBattlePassGetRewardS2CServerDelegate OnBattlePassGetRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassGetRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassGetRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassGetRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassGetRewardS2C model = param.ReadObject<BattlePassGetRewardS2C>();
		await OnBattlePassGetRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
