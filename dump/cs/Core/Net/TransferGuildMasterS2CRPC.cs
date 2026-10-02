using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TransferGuildMasterS2CRPC
{
	public delegate UniTask OnTransferGuildMasterS2CServerDelegate(TransferGuildMasterS2C model, int errId, bool isDispatch);

	public OnTransferGuildMasterS2CServerDelegate OnTransferGuildMasterS2CServerCallBackAsync;

	internal virtual async UniTask PushTransferGuildMasterS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTransferGuildMasterS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TransferGuildMasterS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TransferGuildMasterS2C model = param.ReadObject<TransferGuildMasterS2C>();
		await OnTransferGuildMasterS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
