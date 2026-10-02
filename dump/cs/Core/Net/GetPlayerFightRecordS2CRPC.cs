using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetPlayerFightRecordS2CRPC
{
	public delegate UniTask OnGetPlayerFightRecordS2CServerDelegate(GetPlayerFightRecordS2C model, int errId, bool isDispatch);

	public OnGetPlayerFightRecordS2CServerDelegate OnGetPlayerFightRecordS2CServerCallBackAsync;

	internal virtual async UniTask PushGetPlayerFightRecordS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetPlayerFightRecordS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetPlayerFightRecordS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetPlayerFightRecordS2C model = param.ReadObject<GetPlayerFightRecordS2C>();
		await OnGetPlayerFightRecordS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
