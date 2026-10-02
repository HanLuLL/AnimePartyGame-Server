using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SetShowPlayerS2CRPC
{
	public delegate UniTask OnSetShowPlayerS2CServerDelegate(SetShowPlayerS2C model, int errId, bool isDispatch);

	public OnSetShowPlayerS2CServerDelegate OnSetShowPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushSetShowPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSetShowPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SetShowPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SetShowPlayerS2C model = param.ReadObject<SetShowPlayerS2C>();
		await OnSetShowPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
