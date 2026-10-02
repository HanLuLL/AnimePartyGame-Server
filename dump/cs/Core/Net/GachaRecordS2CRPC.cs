using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GachaRecordS2CRPC
{
	public delegate UniTask OnGachaRecordS2CServerDelegate(GachaRecordS2C model, int errId, bool isDispatch);

	public OnGachaRecordS2CServerDelegate OnGachaRecordS2CServerCallBackAsync;

	internal virtual async UniTask PushGachaRecordS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGachaRecordS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GachaRecordS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GachaRecordS2C model = param.ReadObject<GachaRecordS2C>();
		await OnGachaRecordS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
