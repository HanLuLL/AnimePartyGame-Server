using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchPunishmentS2CRPC
{
	public delegate UniTask OnMatchPunishmentS2CServerDelegate(MatchPunishmentS2C model, int errId, bool isDispatch);

	public OnMatchPunishmentS2CServerDelegate OnMatchPunishmentS2CServerCallBackAsync;

	internal virtual async UniTask PushMatchPunishmentS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchPunishmentS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchPunishmentS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchPunishmentS2C model = param.ReadObject<MatchPunishmentS2C>();
		await OnMatchPunishmentS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
