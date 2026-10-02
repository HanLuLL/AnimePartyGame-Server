using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UseQuickCardS2CRPC
{
	public delegate UniTask OnUseQuickCardS2CServerDelegate(UseQuickCardS2C model, int errId, bool isDispatch);

	public OnUseQuickCardS2CServerDelegate OnUseQuickCardS2CServerCallBackAsync;

	internal virtual async UniTask PushUseQuickCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUseQuickCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UseQuickCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UseQuickCardS2C model = param.ReadObject<UseQuickCardS2C>();
		await OnUseQuickCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
