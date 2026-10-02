using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GuildTaskNotifyS2CRPC
{
	public delegate UniTask OnGuildTaskNotifyS2CServerDelegate(GuildTaskNotifyS2C model, int errId, bool isDispatch);

	public OnGuildTaskNotifyS2CServerDelegate OnGuildTaskNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushGuildTaskNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGuildTaskNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GuildTaskNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GuildTaskNotifyS2C model = param.ReadObject<GuildTaskNotifyS2C>();
		await OnGuildTaskNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
