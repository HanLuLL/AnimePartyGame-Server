using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SelectRelicS2CRPC
{
	public delegate UniTask OnSelectRelicS2CServerDelegate(SelectRelicS2C model, int errId, bool isDispatch);

	public OnSelectRelicS2CServerDelegate OnSelectRelicS2CServerCallBackAsync;

	internal virtual async UniTask PushSelectRelicS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSelectRelicS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SelectRelicS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SelectRelicS2C model = param.ReadObject<SelectRelicS2C>();
		await OnSelectRelicS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
