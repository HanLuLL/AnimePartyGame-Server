using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MonsterPursuitS2CRPC
{
	public delegate UniTask OnMonsterPursuitS2CServerDelegate(MonsterPursuitS2C model, int errId, bool isDispatch);

	public OnMonsterPursuitS2CServerDelegate OnMonsterPursuitS2CServerCallBackAsync;

	internal virtual async UniTask PushMonsterPursuitS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMonsterPursuitS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MonsterPursuitS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MonsterPursuitS2C model = param.ReadObject<MonsterPursuitS2C>();
		await OnMonsterPursuitS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
