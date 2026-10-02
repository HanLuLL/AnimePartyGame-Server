using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattleThrowDiceS2CRPC
{
	public delegate UniTask OnBattleThrowDiceS2CServerDelegate(BattleThrowDiceS2C model, int errId, bool isDispatch);

	public OnBattleThrowDiceS2CServerDelegate OnBattleThrowDiceS2CServerCallBackAsync;

	internal virtual async UniTask PushBattleThrowDiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattleThrowDiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattleThrowDiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattleThrowDiceS2C model = param.ReadObject<BattleThrowDiceS2C>();
		await OnBattleThrowDiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
