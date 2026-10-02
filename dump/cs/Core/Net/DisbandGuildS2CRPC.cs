using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class DisbandGuildS2CRPC
{
	public delegate UniTask OnDisbandGuildS2CServerDelegate(DisbandGuildS2C model, int errId, bool isDispatch);

	public OnDisbandGuildS2CServerDelegate OnDisbandGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushDisbandGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnDisbandGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议DisbandGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		DisbandGuildS2C model = param.ReadObject<DisbandGuildS2C>();
		await OnDisbandGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
