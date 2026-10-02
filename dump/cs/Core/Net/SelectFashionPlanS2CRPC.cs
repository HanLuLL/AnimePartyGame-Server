using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SelectFashionPlanS2CRPC
{
	public delegate UniTask OnSelectFashionPlanS2CServerDelegate(SelectFashionPlanS2C model, int errId, bool isDispatch);

	public OnSelectFashionPlanS2CServerDelegate OnSelectFashionPlanS2CServerCallBackAsync;

	internal virtual async UniTask PushSelectFashionPlanS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSelectFashionPlanS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SelectFashionPlanS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SelectFashionPlanS2C model = param.ReadObject<SelectFashionPlanS2C>();
		await OnSelectFashionPlanS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
