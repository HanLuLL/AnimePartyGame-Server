using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetPlayerSimpleS2CRPC
{
	public delegate UniTask OnGetPlayerSimpleS2CServerDelegate(GetPlayerSimpleS2C model, int errId, bool isDispatch);

	public OnGetPlayerSimpleS2CServerDelegate OnGetPlayerSimpleS2CServerCallBackAsync;

	internal virtual async UniTask PushGetPlayerSimpleS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetPlayerSimpleS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetPlayerSimpleS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetPlayerSimpleS2C model = param.ReadObject<GetPlayerSimpleS2C>();
		await OnGetPlayerSimpleS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
