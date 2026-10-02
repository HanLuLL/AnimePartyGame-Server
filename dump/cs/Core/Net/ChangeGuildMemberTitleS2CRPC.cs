using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeGuildMemberTitleS2CRPC
{
	public delegate UniTask OnChangeGuildMemberTitleS2CServerDelegate(ChangeGuildMemberTitleS2C model, int errId, bool isDispatch);

	public OnChangeGuildMemberTitleS2CServerDelegate OnChangeGuildMemberTitleS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeGuildMemberTitleS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeGuildMemberTitleS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeGuildMemberTitleS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeGuildMemberTitleS2C model = param.ReadObject<ChangeGuildMemberTitleS2C>();
		await OnChangeGuildMemberTitleS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
