using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UnLockDifficultyS2CRPC
{
	public delegate UniTask OnUnLockDifficultyS2CServerDelegate(UnLockDifficultyS2C model, int errId, bool isDispatch);

	public OnUnLockDifficultyS2CServerDelegate OnUnLockDifficultyS2CServerCallBackAsync;

	internal virtual async UniTask PushUnLockDifficultyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUnLockDifficultyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UnLockDifficultyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UnLockDifficultyS2C model = param.ReadObject<UnLockDifficultyS2C>();
		await OnUnLockDifficultyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
