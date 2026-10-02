using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SearchGuildS2CRPC
{
	public delegate UniTask OnSearchGuildS2CServerDelegate(SearchGuildS2C model, int errId, bool isDispatch);

	public OnSearchGuildS2CServerDelegate OnSearchGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushSearchGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSearchGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SearchGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SearchGuildS2C model = param.ReadObject<SearchGuildS2C>();
		await OnSearchGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
