using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangePlayerSlotS2CRPC
{
	public delegate UniTask OnChangePlayerSlotS2CServerDelegate(ChangePlayerSlotS2C model, int errId, bool isDispatch);

	public OnChangePlayerSlotS2CServerDelegate OnChangePlayerSlotS2CServerCallBackAsync;

	internal virtual async UniTask PushChangePlayerSlotS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangePlayerSlotS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangePlayerSlotS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangePlayerSlotS2C model = param.ReadObject<ChangePlayerSlotS2C>();
		await OnChangePlayerSlotS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
