using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UpdateHeroAttrS2CRPC
{
	public delegate UniTask OnUpdateHeroAttrS2CServerDelegate(UpdateHeroAttrS2C model, int errId, bool isDispatch);

	public OnUpdateHeroAttrS2CServerDelegate OnUpdateHeroAttrS2CServerCallBackAsync;

	internal virtual async UniTask PushUpdateHeroAttrS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUpdateHeroAttrS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UpdateHeroAttrS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UpdateHeroAttrS2C model = param.ReadObject<UpdateHeroAttrS2C>();
		await OnUpdateHeroAttrS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
