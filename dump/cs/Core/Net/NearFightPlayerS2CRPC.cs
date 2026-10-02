using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NearFightPlayerS2CRPC
{
	public delegate UniTask OnNearFightPlayerS2CServerDelegate(NearFightPlayerS2C model, int errId, bool isDispatch);

	public OnNearFightPlayerS2CServerDelegate OnNearFightPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushNearFightPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNearFightPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NearFightPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NearFightPlayerS2C model = param.ReadObject<NearFightPlayerS2C>();
		await OnNearFightPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
