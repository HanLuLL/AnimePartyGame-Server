using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ImpeachGuildMasterS2CRPC
{
	public delegate UniTask OnImpeachGuildMasterS2CServerDelegate(ImpeachGuildMasterS2C model, int errId, bool isDispatch);

	public OnImpeachGuildMasterS2CServerDelegate OnImpeachGuildMasterS2CServerCallBackAsync;

	internal virtual async UniTask PushImpeachGuildMasterS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnImpeachGuildMasterS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ImpeachGuildMasterS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ImpeachGuildMasterS2C model = param.ReadObject<ImpeachGuildMasterS2C>();
		await OnImpeachGuildMasterS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
