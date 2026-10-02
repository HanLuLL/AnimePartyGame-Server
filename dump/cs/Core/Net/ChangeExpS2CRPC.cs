using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeExpS2CRPC
{
	public delegate UniTask OnChangeExpS2CServerDelegate(ChangeExpS2C model, int errId, bool isDispatch);

	public OnChangeExpS2CServerDelegate OnChangeExpS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeExpS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeExpS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeExpS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeExpS2C model = param.ReadObject<ChangeExpS2C>();
		await OnChangeExpS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
