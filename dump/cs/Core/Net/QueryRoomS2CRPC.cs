using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class QueryRoomS2CRPC
{
	public delegate UniTask OnQueryRoomS2CServerDelegate(QueryRoomS2C model, int errId, bool isDispatch);

	public OnQueryRoomS2CServerDelegate OnQueryRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushQueryRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnQueryRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议QueryRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		QueryRoomS2C model = param.ReadObject<QueryRoomS2C>();
		await OnQueryRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
