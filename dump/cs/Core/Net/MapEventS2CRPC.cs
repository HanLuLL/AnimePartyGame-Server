using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapEventS2CRPC
{
	public delegate UniTask OnMapEventS2CServerDelegate(MapEventS2C model, int errId, bool isDispatch);

	public OnMapEventS2CServerDelegate OnMapEventS2CServerCallBackAsync;

	internal virtual async UniTask PushMapEventS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapEventS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapEventS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapEventS2C model = param.ReadObject<MapEventS2C>();
		await OnMapEventS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
