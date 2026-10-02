using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChallengeDataChangeS2CRPC
{
	public delegate UniTask OnChallengeDataChangeS2CServerDelegate(ChallengeDataChangeS2C model, int errId, bool isDispatch);

	public OnChallengeDataChangeS2CServerDelegate OnChallengeDataChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushChallengeDataChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChallengeDataChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChallengeDataChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChallengeDataChangeS2C model = param.ReadObject<ChallengeDataChangeS2C>();
		await OnChallengeDataChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
