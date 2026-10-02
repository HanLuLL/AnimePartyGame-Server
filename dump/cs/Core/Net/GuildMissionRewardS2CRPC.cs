using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GuildMissionRewardS2CRPC
{
	public delegate UniTask OnGuildMissionRewardS2CServerDelegate(GuildMissionRewardS2C model, int errId, bool isDispatch);

	public OnGuildMissionRewardS2CServerDelegate OnGuildMissionRewardS2CServerCallBackAsync;

	internal virtual async UniTask PushGuildMissionRewardS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGuildMissionRewardS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GuildMissionRewardS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GuildMissionRewardS2C model = param.ReadObject<GuildMissionRewardS2C>();
		await OnGuildMissionRewardS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
