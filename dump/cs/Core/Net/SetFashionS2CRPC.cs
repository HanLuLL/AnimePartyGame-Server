using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SetFashionS2CRPC
{
	public delegate UniTask OnSetFashionS2CServerDelegate(SetFashionS2C model, int errId, bool isDispatch);

	public OnSetFashionS2CServerDelegate OnSetFashionS2CServerCallBackAsync;

	internal virtual async UniTask PushSetFashionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSetFashionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SetFashionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SetFashionS2C model = param.ReadObject<SetFashionS2C>();
		await OnSetFashionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
