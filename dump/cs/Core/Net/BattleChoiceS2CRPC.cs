using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BattleChoiceS2CRPC
{
	public delegate UniTask OnBattleChoiceS2CServerDelegate(BattleChoiceS2C model, int errId, bool isDispatch);

	public OnBattleChoiceS2CServerDelegate OnBattleChoiceS2CServerCallBackAsync;

	internal virtual async UniTask PushBattleChoiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBattleChoiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BattleChoiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BattleChoiceS2C model = param.ReadObject<BattleChoiceS2C>();
		await OnBattleChoiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
