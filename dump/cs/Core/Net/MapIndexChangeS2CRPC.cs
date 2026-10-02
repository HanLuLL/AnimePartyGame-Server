using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapIndexChangeS2CRPC
{
	public delegate UniTask OnMapIndexChangeS2CServerDelegate(MapIndexChangeS2C model, int errId, bool isDispatch);

	public OnMapIndexChangeS2CServerDelegate OnMapIndexChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushMapIndexChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapIndexChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapIndexChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapIndexChangeS2C model = param.ReadObject<MapIndexChangeS2C>();
		await OnMapIndexChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
