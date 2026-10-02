using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class KickGuildMemberS2CRPC
{
	public delegate UniTask OnKickGuildMemberS2CServerDelegate(KickGuildMemberS2C model, int errId, bool isDispatch);

	public OnKickGuildMemberS2CServerDelegate OnKickGuildMemberS2CServerCallBackAsync;

	internal virtual async UniTask PushKickGuildMemberS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnKickGuildMemberS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议KickGuildMemberS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		KickGuildMemberS2C model = param.ReadObject<KickGuildMemberS2C>();
		await OnKickGuildMemberS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
