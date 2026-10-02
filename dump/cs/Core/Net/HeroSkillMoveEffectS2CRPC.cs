using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class HeroSkillMoveEffectS2CRPC
{
	public delegate UniTask OnHeroSkillMoveEffectS2CServerDelegate(HeroSkillMoveEffectS2C model, int errId, bool isDispatch);

	public OnHeroSkillMoveEffectS2CServerDelegate OnHeroSkillMoveEffectS2CServerCallBackAsync;

	internal virtual async UniTask PushHeroSkillMoveEffectS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnHeroSkillMoveEffectS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议HeroSkillMoveEffectS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		HeroSkillMoveEffectS2C model = param.ReadObject<HeroSkillMoveEffectS2C>();
		await OnHeroSkillMoveEffectS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
