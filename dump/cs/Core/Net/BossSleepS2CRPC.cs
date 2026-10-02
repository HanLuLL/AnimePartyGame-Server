using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BossSleepS2CRPC
{
	public delegate UniTask OnBossSleepS2CServerDelegate(BossSleepS2C model, int errId, bool isDispatch);

	public OnBossSleepS2CServerDelegate OnBossSleepS2CServerCallBackAsync;

	internal virtual async UniTask PushBossSleepS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBossSleepS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BossSleepS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BossSleepS2C model = param.ReadObject<BossSleepS2C>();
		await OnBossSleepS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
