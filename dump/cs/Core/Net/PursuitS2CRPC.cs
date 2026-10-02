using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PursuitS2CRPC
{
	public delegate UniTask OnPursuitS2CServerDelegate(PursuitS2C model, int errId, bool isDispatch);

	public OnPursuitS2CServerDelegate OnPursuitS2CServerCallBackAsync;

	internal virtual async UniTask PushPursuitS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPursuitS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PursuitS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PursuitS2C model = param.ReadObject<PursuitS2C>();
		await OnPursuitS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
