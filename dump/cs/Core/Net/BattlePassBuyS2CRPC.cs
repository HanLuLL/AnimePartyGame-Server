using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassBuyS2CRPC
{
	public delegate UniTask OnBattlePassBuyS2CServerDelegate(BattlePassBuyS2C model, int errId, bool isDispatch);

	public OnBattlePassBuyS2CServerDelegate OnBattlePassBuyS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassBuyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassBuyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassBuyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassBuyS2C model = param.ReadObject<BattlePassBuyS2C>();
		await OnBattlePassBuyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
