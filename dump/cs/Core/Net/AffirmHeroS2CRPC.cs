using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AffirmHeroS2CRPC
{
	public delegate UniTask OnAffirmHeroS2CServerDelegate(AffirmHeroS2C model, int errId, bool isDispatch);

	public OnAffirmHeroS2CServerDelegate OnAffirmHeroS2CServerCallBackAsync;

	internal virtual async UniTask PushAffirmHeroS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAffirmHeroS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AffirmHeroS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AffirmHeroS2C model = param.ReadObject<AffirmHeroS2C>();
		await OnAffirmHeroS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
