using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SetOnlineStatusS2CRPC
{
	public delegate UniTask OnSetOnlineStatusS2CServerDelegate(SetOnlineStatusS2C model, int errId, bool isDispatch);

	public OnSetOnlineStatusS2CServerDelegate OnSetOnlineStatusS2CServerCallBackAsync;

	internal virtual async UniTask PushSetOnlineStatusS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSetOnlineStatusS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SetOnlineStatusS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SetOnlineStatusS2C model = param.ReadObject<SetOnlineStatusS2C>();
		await OnSetOnlineStatusS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
