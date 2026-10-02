using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class StartMatchS2CRPC
{
	public delegate UniTask OnStartMatchS2CServerDelegate(StartMatchS2C model, int errId, bool isDispatch);

	public OnStartMatchS2CServerDelegate OnStartMatchS2CServerCallBackAsync;

	internal virtual async UniTask PushStartMatchS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnStartMatchS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议StartMatchS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		StartMatchS2C model = param.ReadObject<StartMatchS2C>();
		await OnStartMatchS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
