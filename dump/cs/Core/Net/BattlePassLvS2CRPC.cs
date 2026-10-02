using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassLvS2CRPC
{
	public delegate UniTask OnBattlePassLvS2CServerDelegate(BattlePassLvS2C model, int errId, bool isDispatch);

	public OnBattlePassLvS2CServerDelegate OnBattlePassLvS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassLvS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassLvS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassLvS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassLvS2C model = param.ReadObject<BattlePassLvS2C>();
		await OnBattlePassLvS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
