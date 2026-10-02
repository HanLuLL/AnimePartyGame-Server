using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetGuildMemberChangeMsgS2CRPC
{
	public delegate UniTask OnGetGuildMemberChangeMsgS2CServerDelegate(GetGuildMemberChangeMsgS2C model, int errId, bool isDispatch);

	public OnGetGuildMemberChangeMsgS2CServerDelegate OnGetGuildMemberChangeMsgS2CServerCallBackAsync;

	internal virtual async UniTask PushGetGuildMemberChangeMsgS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetGuildMemberChangeMsgS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetGuildMemberChangeMsgS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetGuildMemberChangeMsgS2C model = param.ReadObject<GetGuildMemberChangeMsgS2C>();
		await OnGetGuildMemberChangeMsgS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
