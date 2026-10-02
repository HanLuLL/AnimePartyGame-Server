using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class HeroBarBoxChangeS2CRPC
{
	public delegate UniTask OnHeroBarBoxChangeS2CServerDelegate(HeroBarBoxChangeS2C model, int errId, bool isDispatch);

	public OnHeroBarBoxChangeS2CServerDelegate OnHeroBarBoxChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushHeroBarBoxChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnHeroBarBoxChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议HeroBarBoxChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		HeroBarBoxChangeS2C model = param.ReadObject<HeroBarBoxChangeS2C>();
		await OnHeroBarBoxChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
