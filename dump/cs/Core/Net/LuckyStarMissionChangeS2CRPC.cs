using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LuckyStarMissionChangeS2CRPC
{
	public delegate UniTask OnLuckyStarMissionChangeS2CServerDelegate(LuckyStarMissionChangeS2C model, int errId, bool isDispatch);

	public OnLuckyStarMissionChangeS2CServerDelegate OnLuckyStarMissionChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushLuckyStarMissionChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLuckyStarMissionChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LuckyStarMissionChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LuckyStarMissionChangeS2C model = param.ReadObject<LuckyStarMissionChangeS2C>();
		await OnLuckyStarMissionChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
