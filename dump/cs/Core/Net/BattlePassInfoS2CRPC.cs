using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassInfoS2CRPC
{
	public delegate UniTask OnBattlePassInfoS2CServerDelegate(BattlePassInfoS2C model, int errId, bool isDispatch);

	public OnBattlePassInfoS2CServerDelegate OnBattlePassInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassInfoS2C model = param.ReadObject<BattlePassInfoS2C>();
		await OnBattlePassInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
