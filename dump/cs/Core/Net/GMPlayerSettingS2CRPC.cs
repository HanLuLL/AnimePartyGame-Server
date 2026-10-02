using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GMPlayerSettingS2CRPC
{
	public delegate UniTask OnGMPlayerSettingS2CServerDelegate(GMPlayerSettingS2C model, int errId, bool isDispatch);

	public OnGMPlayerSettingS2CServerDelegate OnGMPlayerSettingS2CServerCallBackAsync;

	internal virtual async UniTask PushGMPlayerSettingS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGMPlayerSettingS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GMPlayerSettingS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GMPlayerSettingS2C model = param.ReadObject<GMPlayerSettingS2C>();
		await OnGMPlayerSettingS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
