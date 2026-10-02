using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassUpdateTaskS2CRPC
{
	public delegate UniTask OnBattlePassUpdateTaskS2CServerDelegate(BattlePassUpdateTaskS2C model, int errId, bool isDispatch);

	public OnBattlePassUpdateTaskS2CServerDelegate OnBattlePassUpdateTaskS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassUpdateTaskS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassUpdateTaskS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassUpdateTaskS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassUpdateTaskS2C model = param.ReadObject<BattlePassUpdateTaskS2C>();
		await OnBattlePassUpdateTaskS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
