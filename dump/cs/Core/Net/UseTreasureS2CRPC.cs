using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UseTreasureS2CRPC
{
	public delegate UniTask OnUseTreasureS2CServerDelegate(UseTreasureS2C model, int errId, bool isDispatch);

	public OnUseTreasureS2CServerDelegate OnUseTreasureS2CServerCallBackAsync;

	internal virtual async UniTask PushUseTreasureS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUseTreasureS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UseTreasureS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UseTreasureS2C model = param.ReadObject<UseTreasureS2C>();
		await OnUseTreasureS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
