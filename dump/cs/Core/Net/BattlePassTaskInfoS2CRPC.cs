using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassTaskInfoS2CRPC
{
	public delegate UniTask OnBattlePassTaskInfoS2CServerDelegate(BattlePassTaskInfoS2C model, int errId, bool isDispatch);

	public OnBattlePassTaskInfoS2CServerDelegate OnBattlePassTaskInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassTaskInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassTaskInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassTaskInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassTaskInfoS2C model = param.ReadObject<BattlePassTaskInfoS2C>();
		await OnBattlePassTaskInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
