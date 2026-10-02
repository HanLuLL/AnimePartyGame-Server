using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattleUseCardS2CRPC
{
	public delegate UniTask OnBattleUseCardS2CServerDelegate(BattleUseCardS2C model, int errId, bool isDispatch);

	public OnBattleUseCardS2CServerDelegate OnBattleUseCardS2CServerCallBackAsync;

	internal virtual async UniTask PushBattleUseCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattleUseCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattleUseCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattleUseCardS2C model = param.ReadObject<BattleUseCardS2C>();
		await OnBattleUseCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
