using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SelectEventS2CRPC
{
	public delegate UniTask OnSelectEventS2CServerDelegate(SelectEventS2C model, int errId, bool isDispatch);

	public OnSelectEventS2CServerDelegate OnSelectEventS2CServerCallBackAsync;

	internal virtual async UniTask PushSelectEventS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSelectEventS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SelectEventS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SelectEventS2C model = param.ReadObject<SelectEventS2C>();
		await OnSelectEventS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
