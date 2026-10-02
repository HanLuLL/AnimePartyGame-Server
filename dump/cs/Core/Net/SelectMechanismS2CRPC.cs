using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SelectMechanismS2CRPC
{
	public delegate UniTask OnSelectMechanismS2CServerDelegate(SelectMechanismS2C model, int errId, bool isDispatch);

	public OnSelectMechanismS2CServerDelegate OnSelectMechanismS2CServerCallBackAsync;

	internal virtual async UniTask PushSelectMechanismS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSelectMechanismS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SelectMechanismS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SelectMechanismS2C model = param.ReadObject<SelectMechanismS2C>();
		await OnSelectMechanismS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
