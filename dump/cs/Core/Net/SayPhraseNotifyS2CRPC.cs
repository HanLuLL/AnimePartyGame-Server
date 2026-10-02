using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SayPhraseNotifyS2CRPC
{
	public delegate UniTask OnSayPhraseNotifyS2CServerDelegate(SayPhraseNotifyS2C model, int errId, bool isDispatch);

	public OnSayPhraseNotifyS2CServerDelegate OnSayPhraseNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushSayPhraseNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSayPhraseNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SayPhraseNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SayPhraseNotifyS2C model = param.ReadObject<SayPhraseNotifyS2C>();
		await OnSayPhraseNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
