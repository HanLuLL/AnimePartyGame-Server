using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SelectRewardCardS2CRPC
{
	public delegate UniTask OnSelectRewardCardS2CServerDelegate(SelectRewardCardS2C model, int errId, bool isDispatch);

	public OnSelectRewardCardS2CServerDelegate OnSelectRewardCardS2CServerCallBackAsync;

	internal virtual async UniTask PushSelectRewardCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSelectRewardCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SelectRewardCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SelectRewardCardS2C model = param.ReadObject<SelectRewardCardS2C>();
		await OnSelectRewardCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
