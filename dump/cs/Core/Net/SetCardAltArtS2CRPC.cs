using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SetCardAltArtS2CRPC
{
	public delegate UniTask OnSetCardAltArtS2CServerDelegate(SetCardAltArtS2C model, int errId, bool isDispatch);

	public OnSetCardAltArtS2CServerDelegate OnSetCardAltArtS2CServerCallBackAsync;

	internal virtual async UniTask PushSetCardAltArtS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSetCardAltArtS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SetCardAltArtS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SetCardAltArtS2C model = param.ReadObject<SetCardAltArtS2C>();
		await OnSetCardAltArtS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
