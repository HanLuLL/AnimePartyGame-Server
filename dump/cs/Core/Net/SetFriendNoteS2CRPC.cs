using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SetFriendNoteS2CRPC
{
	public delegate UniTask OnSetFriendNoteS2CServerDelegate(SetFriendNoteS2C model, int errId, bool isDispatch);

	public OnSetFriendNoteS2CServerDelegate OnSetFriendNoteS2CServerCallBackAsync;

	internal virtual async UniTask PushSetFriendNoteS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSetFriendNoteS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SetFriendNoteS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SetFriendNoteS2C model = param.ReadObject<SetFriendNoteS2C>();
		await OnSetFriendNoteS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
