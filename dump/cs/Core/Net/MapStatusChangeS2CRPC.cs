using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapStatusChangeS2CRPC
{
	public delegate UniTask OnMapStatusChangeS2CServerDelegate(MapStatusChangeS2C model, int errId, bool isDispatch);

	public OnMapStatusChangeS2CServerDelegate OnMapStatusChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushMapStatusChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapStatusChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapStatusChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapStatusChangeS2C model = param.ReadObject<MapStatusChangeS2C>();
		await OnMapStatusChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
