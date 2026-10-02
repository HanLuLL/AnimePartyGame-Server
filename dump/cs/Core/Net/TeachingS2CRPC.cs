using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TeachingS2CRPC
{
	public delegate UniTask OnTeachingS2CServerDelegate(TeachingS2C model, int errId, bool isDispatch);

	public OnTeachingS2CServerDelegate OnTeachingS2CServerCallBackAsync;

	internal virtual async UniTask PushTeachingS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTeachingS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TeachingS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TeachingS2C model = param.ReadObject<TeachingS2C>();
		await OnTeachingS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
