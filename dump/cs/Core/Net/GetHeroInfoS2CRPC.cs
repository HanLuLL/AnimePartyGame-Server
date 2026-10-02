using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetHeroInfoS2CRPC
{
	public delegate UniTask OnGetHeroInfoS2CServerDelegate(GetHeroInfoS2C model, int errId, bool isDispatch);

	public OnGetHeroInfoS2CServerDelegate OnGetHeroInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushGetHeroInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetHeroInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetHeroInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetHeroInfoS2C model = param.ReadObject<GetHeroInfoS2C>();
		await OnGetHeroInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
