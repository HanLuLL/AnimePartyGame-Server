using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PveHeroUpLvS2CRPC
{
	public delegate UniTask OnPveHeroUpLvS2CServerDelegate(PveHeroUpLvS2C model, int errId, bool isDispatch);

	public OnPveHeroUpLvS2CServerDelegate OnPveHeroUpLvS2CServerCallBackAsync;

	internal virtual async UniTask PushPveHeroUpLvS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPveHeroUpLvS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PveHeroUpLvS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PveHeroUpLvS2C model = param.ReadObject<PveHeroUpLvS2C>();
		await OnPveHeroUpLvS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
