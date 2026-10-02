using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapEventTrainS2CRPC
{
	public delegate UniTask OnMapEventTrainS2CServerDelegate(MapEventTrainS2C model, int errId, bool isDispatch);

	public OnMapEventTrainS2CServerDelegate OnMapEventTrainS2CServerCallBackAsync;

	internal virtual async UniTask PushMapEventTrainS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapEventTrainS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapEventTrainS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapEventTrainS2C model = param.ReadObject<MapEventTrainS2C>();
		await OnMapEventTrainS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
