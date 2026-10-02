using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UpdateGuildSettingsS2CRPC
{
	public delegate UniTask OnUpdateGuildSettingsS2CServerDelegate(UpdateGuildSettingsS2C model, int errId, bool isDispatch);

	public OnUpdateGuildSettingsS2CServerDelegate OnUpdateGuildSettingsS2CServerCallBackAsync;

	internal virtual async UniTask PushUpdateGuildSettingsS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUpdateGuildSettingsS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UpdateGuildSettingsS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UpdateGuildSettingsS2C model = param.ReadObject<UpdateGuildSettingsS2C>();
		await OnUpdateGuildSettingsS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
