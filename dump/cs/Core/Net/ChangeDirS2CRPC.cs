using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeDirS2CRPC
{
	public delegate UniTask OnChangeDirS2CServerDelegate(ChangeDirS2C model, int errId, bool isDispatch);

	public OnChangeDirS2CServerDelegate OnChangeDirS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeDirS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeDirS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeDirS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeDirS2C model = param.ReadObject<ChangeDirS2C>();
		await OnChangeDirS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
