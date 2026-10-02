using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class VoteSelectS2CRPC
{
	public delegate UniTask OnVoteSelectS2CServerDelegate(VoteSelectS2C model, int errId, bool isDispatch);

	public OnVoteSelectS2CServerDelegate OnVoteSelectS2CServerCallBackAsync;

	internal virtual async UniTask PushVoteSelectS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnVoteSelectS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议VoteSelectS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		VoteSelectS2C model = param.ReadObject<VoteSelectS2C>();
		await OnVoteSelectS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
