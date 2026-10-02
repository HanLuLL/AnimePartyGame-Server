using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PraisePlayerS2CRPC
{
	public delegate UniTask OnPraisePlayerS2CServerDelegate(PraisePlayerS2C model, int errId, bool isDispatch);

	public OnPraisePlayerS2CServerDelegate OnPraisePlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushPraisePlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPraisePlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PraisePlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PraisePlayerS2C model = param.ReadObject<PraisePlayerS2C>();
		await OnPraisePlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
