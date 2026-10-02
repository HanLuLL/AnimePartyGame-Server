using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SearchRoomS2CRPC
{
	public delegate UniTask OnSearchRoomS2CServerDelegate(SearchRoomS2C model, int errId, bool isDispatch);

	public OnSearchRoomS2CServerDelegate OnSearchRoomS2CServerCallBackAsync;

	internal virtual async UniTask PushSearchRoomS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSearchRoomS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SearchRoomS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SearchRoomS2C model = param.ReadObject<SearchRoomS2C>();
		await OnSearchRoomS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
