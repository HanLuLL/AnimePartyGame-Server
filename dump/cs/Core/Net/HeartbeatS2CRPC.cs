using UnityEngine;
using party.protocol;

namespace Core.Net;

public class HeartbeatS2CRPC
{
	public delegate void OnHeartbeatS2CServerDelegate(HeartbeatS2C model);

	public OnHeartbeatS2CServerDelegate OnHeartbeatS2CServerCallBack;

	internal virtual void PushHeartbeatS2CCallBack(ByteBuf param)
	{
		if (OnHeartbeatS2CServerCallBack == null)
		{
			Debug.LogError("协议HeartbeatS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		HeartbeatS2C model = param.ReadObject<HeartbeatS2C>();
		OnHeartbeatS2CServerCallBack(model);
	}
}
