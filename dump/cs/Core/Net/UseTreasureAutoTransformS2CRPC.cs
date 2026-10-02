using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UseTreasureAutoTransformS2CRPC
{
	public delegate UniTask OnUseTreasureAutoTransformS2CServerDelegate(UseTreasureAutoTransformS2C model, int errId, bool isDispatch);

	public OnUseTreasureAutoTransformS2CServerDelegate OnUseTreasureAutoTransformS2CServerCallBackAsync;

	internal virtual async UniTask PushUseTreasureAutoTransformS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUseTreasureAutoTransformS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UseTreasureAutoTransformS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UseTreasureAutoTransformS2C model = param.ReadObject<UseTreasureAutoTransformS2C>();
		await OnUseTreasureAutoTransformS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
