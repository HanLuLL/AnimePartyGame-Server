using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MonsterRefreshS2CRPC
{
	public delegate UniTask OnMonsterRefreshS2CServerDelegate(MonsterRefreshS2C model, int errId, bool isDispatch);

	public OnMonsterRefreshS2CServerDelegate OnMonsterRefreshS2CServerCallBackAsync;

	internal virtual async UniTask PushMonsterRefreshS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMonsterRefreshS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MonsterRefreshS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MonsterRefreshS2C model = param.ReadObject<MonsterRefreshS2C>();
		await OnMonsterRefreshS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
