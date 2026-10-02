using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MonthlyCardS2CRPC
{
	public delegate UniTask OnMonthlyCardS2CServerDelegate(MonthlyCardS2C model, int errId, bool isDispatch);

	public OnMonthlyCardS2CServerDelegate OnMonthlyCardS2CServerCallBackAsync;

	internal virtual async UniTask PushMonthlyCardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMonthlyCardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MonthlyCardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MonthlyCardS2C model = param.ReadObject<MonthlyCardS2C>();
		await OnMonthlyCardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
