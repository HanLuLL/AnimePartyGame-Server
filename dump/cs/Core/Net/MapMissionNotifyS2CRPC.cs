using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MapMissionNotifyS2CRPC
{
	public delegate UniTask OnMapMissionNotifyS2CServerDelegate(MapMissionNotifyS2C model, int errId, bool isDispatch);

	public OnMapMissionNotifyS2CServerDelegate OnMapMissionNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushMapMissionNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMapMissionNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MapMissionNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MapMissionNotifyS2C model = param.ReadObject<MapMissionNotifyS2C>();
		await OnMapMissionNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
