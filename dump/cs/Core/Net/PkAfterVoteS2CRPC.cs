using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PkAfterVoteS2CRPC
{
	public delegate UniTask OnPkAfterVoteS2CServerDelegate(PkAfterVoteS2C model, int errId, bool isDispatch);

	public OnPkAfterVoteS2CServerDelegate OnPkAfterVoteS2CServerCallBackAsync;

	internal virtual async UniTask PushPkAfterVoteS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPkAfterVoteS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PkAfterVoteS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PkAfterVoteS2C model = param.ReadObject<PkAfterVoteS2C>();
		await OnPkAfterVoteS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
