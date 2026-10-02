using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BombThrowDiceS2CRPC
{
	public delegate UniTask OnBombThrowDiceS2CServerDelegate(BombThrowDiceS2C model, int errId, bool isDispatch);

	public OnBombThrowDiceS2CServerDelegate OnBombThrowDiceS2CServerCallBackAsync;

	internal virtual async UniTask PushBombThrowDiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBombThrowDiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BombThrowDiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BombThrowDiceS2C model = param.ReadObject<BombThrowDiceS2C>();
		await OnBombThrowDiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
