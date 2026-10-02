using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChoiceHeroS2C2RPC
{
	public delegate UniTask OnChoiceHeroS2C2ServerDelegate(ChoiceHeroS2C2 model, int errId, bool isDispatch);

	public OnChoiceHeroS2C2ServerDelegate OnChoiceHeroS2C2ServerCallBackAsync;

	internal virtual async UniTask PushChoiceHeroS2C2CallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChoiceHeroS2C2ServerCallBackAsync == null)
		{
			Debug.LogError("协议ChoiceHeroS2C2的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChoiceHeroS2C2 model = param.ReadObject<ChoiceHeroS2C2>();
		await OnChoiceHeroS2C2ServerCallBackAsync(model, errId, isDispatch);
	}
}
