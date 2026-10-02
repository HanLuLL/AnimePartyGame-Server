using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapEventCrabS2CRPC
{
	public delegate UniTask OnMapEventCrabS2CServerDelegate(MapEventCrabS2C model, int errId, bool isDispatch);

	public OnMapEventCrabS2CServerDelegate OnMapEventCrabS2CServerCallBackAsync;

	internal virtual async UniTask PushMapEventCrabS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapEventCrabS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapEventCrabS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapEventCrabS2C model = param.ReadObject<MapEventCrabS2C>();
		await OnMapEventCrabS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
