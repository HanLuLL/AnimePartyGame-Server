using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CheatItemS2CRPC
{
	public delegate UniTask OnCheatItemS2CServerDelegate(CheatItemS2C model, int errId, bool isDispatch);

	public OnCheatItemS2CServerDelegate OnCheatItemS2CServerCallBackAsync;

	internal virtual async UniTask PushCheatItemS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCheatItemS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CheatItemS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CheatItemS2C model = param.ReadObject<CheatItemS2C>();
		await OnCheatItemS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
