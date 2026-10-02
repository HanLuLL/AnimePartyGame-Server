using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PveHeroTalentUpS2CRPC
{
	public delegate UniTask OnPveHeroTalentUpS2CServerDelegate(PveHeroTalentUpS2C model, int errId, bool isDispatch);

	public OnPveHeroTalentUpS2CServerDelegate OnPveHeroTalentUpS2CServerCallBackAsync;

	internal virtual async UniTask PushPveHeroTalentUpS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPveHeroTalentUpS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PveHeroTalentUpS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PveHeroTalentUpS2C model = param.ReadObject<PveHeroTalentUpS2C>();
		await OnPveHeroTalentUpS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
