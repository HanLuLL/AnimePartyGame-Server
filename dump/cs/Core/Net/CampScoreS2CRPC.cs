using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CampScoreS2CRPC
{
	public delegate UniTask OnCampScoreS2CServerDelegate(CampScoreS2C model, int errId, bool isDispatch);

	public OnCampScoreS2CServerDelegate OnCampScoreS2CServerCallBackAsync;

	internal virtual async UniTask PushCampScoreS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCampScoreS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CampScoreS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CampScoreS2C model = param.ReadObject<CampScoreS2C>();
		await OnCampScoreS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
