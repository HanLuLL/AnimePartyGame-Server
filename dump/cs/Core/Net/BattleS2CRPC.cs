using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattleS2CRPC
{
	public delegate UniTask OnBattleS2CServerDelegate(BattleS2C model, int errId, bool isDispatch);

	public OnBattleS2CServerDelegate OnBattleS2CServerCallBackAsync;

	internal virtual async UniTask PushBattleS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattleS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattleS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattleS2C model = param.ReadObject<BattleS2C>();
		await OnBattleS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
