using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class UpdateGuildInAnnouncementS2CRPC
{
	public delegate UniTask OnUpdateGuildInAnnouncementS2CServerDelegate(UpdateGuildInAnnouncementS2C model, int errId, bool isDispatch);

	public OnUpdateGuildInAnnouncementS2CServerDelegate OnUpdateGuildInAnnouncementS2CServerCallBackAsync;

	internal virtual async UniTask PushUpdateGuildInAnnouncementS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnUpdateGuildInAnnouncementS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议UpdateGuildInAnnouncementS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		UpdateGuildInAnnouncementS2C model = param.ReadObject<UpdateGuildInAnnouncementS2C>();
		await OnUpdateGuildInAnnouncementS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
