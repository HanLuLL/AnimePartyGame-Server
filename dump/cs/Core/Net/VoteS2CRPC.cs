using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class VoteS2CRPC
{
	public delegate UniTask OnVoteS2CServerDelegate(VoteS2C model, int errId, bool isDispatch);

	public OnVoteS2CServerDelegate OnVoteS2CServerCallBackAsync;

	internal virtual async UniTask PushVoteS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnVoteS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议VoteS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		VoteS2C model = param.ReadObject<VoteS2C>();
		await OnVoteS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
