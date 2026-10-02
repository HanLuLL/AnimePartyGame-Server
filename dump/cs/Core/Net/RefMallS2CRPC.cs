using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RefMallS2CRPC
{
	public delegate UniTask OnRefMallS2CServerDelegate(RefMallS2C model, int errId, bool isDispatch);

	public OnRefMallS2CServerDelegate OnRefMallS2CServerCallBackAsync;

	internal virtual async UniTask PushRefMallS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRefMallS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RefMallS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RefMallS2C model = param.ReadObject<RefMallS2C>();
		await OnRefMallS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
