using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class BuffEffectHandler_10671301 : IBuffEffectHandler
{
	private Effect _curEffects;

	public async UniTask Play(Buff buff, Character target)
	{
		if (buff.Progress == 1)
		{
			await TryPlayBGM(177);
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowPVEClueTaskTip(null, showFact: true, 5f);
		}
		int effectID = buff.BuffId.GetBuffConfigure().EffectID;
		if (effectID != 0)
		{
			if (buff.Progress > 0)
			{
				ReleaseEffect();
			}
			else if (_curEffects == null)
			{
				_curEffects = await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectID, Vector3.zero, Quaternion.identity, target.EffectContainer);
			}
		}
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	private void ReleaseEffect()
	{
		if (!(_curEffects == null))
		{
			_curEffects.ReleaseEffect();
			_curEffects = null;
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect();
	}

	public void Dispose()
	{
		_curEffects = null;
	}

	public async UniTask TryPlayBGM(int bgmId)
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => SimpleSingletonProvider<SceneManager>.inst.loadingScene.Value.stateType != SceneStateType.Begin);
		RoomBattleBGM battleBGM = SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM;
		if (battleBGM != null && battleBGM.Map_BGMId != bgmId)
		{
			battleBGM.Map_BGMId = bgmId;
			battleBGM.PlayBattleBGM(0L);
		}
	}
}
