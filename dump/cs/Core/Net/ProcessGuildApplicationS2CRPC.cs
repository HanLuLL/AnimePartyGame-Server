using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ProcessGuildApplicationS2CRPC
{
	public delegate UniTask OnProcessGuildApplicationS2CServerDelegate(ProcessGuildApplicationS2C model, int errId, bool isDispatch);

	public OnProcessGuildApplicationS2CServerDelegate OnProcessGuildApplicationS2CServerCallBackAsync;

	internal virtual async UniTask PushProcessGuildApplicationS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnProcessGuildApplicationS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ProcessGuildApplicationS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ProcessGuildApplicationS2C model = param.ReadObject<ProcessGuildApplicationS2C>();
		await OnProcessGuildApplicationS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
