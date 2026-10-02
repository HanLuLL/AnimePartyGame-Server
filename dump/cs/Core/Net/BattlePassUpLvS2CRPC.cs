using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattlePassUpLvS2CRPC
{
	public delegate UniTask OnBattlePassUpLvS2CServerDelegate(BattlePassUpLvS2C model, int errId, bool isDispatch);

	public OnBattlePassUpLvS2CServerDelegate OnBattlePassUpLvS2CServerCallBackAsync;

	internal virtual async UniTask PushBattlePassUpLvS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattlePassUpLvS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattlePassUpLvS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattlePassUpLvS2C model = param.ReadObject<BattlePassUpLvS2C>();
		await OnBattlePassUpLvS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
