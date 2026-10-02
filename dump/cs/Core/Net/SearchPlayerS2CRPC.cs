using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SearchPlayerS2CRPC
{
	public delegate UniTask OnSearchPlayerS2CServerDelegate(SearchPlayerS2C model, int errId, bool isDispatch);

	public OnSearchPlayerS2CServerDelegate OnSearchPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushSearchPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSearchPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SearchPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SearchPlayerS2C model = param.ReadObject<SearchPlayerS2C>();
		await OnSearchPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
