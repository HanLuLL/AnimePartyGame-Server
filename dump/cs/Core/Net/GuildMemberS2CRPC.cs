using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GuildMemberS2CRPC
{
	public delegate UniTask OnGuildMemberS2CServerDelegate(GuildMemberS2C model, int errId, bool isDispatch);

	public OnGuildMemberS2CServerDelegate OnGuildMemberS2CServerCallBackAsync;

	internal virtual async UniTask PushGuildMemberS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGuildMemberS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GuildMemberS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GuildMemberS2C model = param.ReadObject<GuildMemberS2C>();
		await OnGuildMemberS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
