using System;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.protocol;

namespace Core.Unit;

public class MapGimmickManager020 : MapGimmickManager
{
	public enum EffectType
	{
		KillElites,
		PlayerDeath,
		Term
	}

	[SerializeField]
	public GameObject[] DreamEffects;

	[SerializeField]
	public Transform HumanNpcRoot;

	[SerializeField]
	public Transform RobotNpcRoot;

	private const int _effectAudioId = 1104;

	protected override void Awake()
	{
		Initialize();
		AddListener();
		base.Awake();
	}

	protected override void OnDestroy()
	{
		RemoveListener();
		base.OnDestroy();
	}

	public override void Initialize()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room != null && DreamEffects != null && DreamEffects.Length == 3)
		{
			DreamEffects[0].SetActive(value: false);
			DreamEffects[1].SetActive(value: false);
			DreamEffects[2].SetActive(value: false);
		}
	}

	private void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerHpChange.AddListener(OnPlayerHpChange);
	}

	private void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerHpChange.RemoveListener(OnPlayerHpChange);
	}

	private void OnPlayerHpChange(HeroHpChangeS2C hpChange, bool isFight)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(hpChange.PlayerId);
		if (playerDataById == null || hpChange.RealChangeHp > 0 || hpChange.Killer == hpChange.PlayerId)
		{
			return;
		}
		if (playerDataById.characterType == CharacterType.Hero && playerDataById.Property.HP.Value <= 0)
		{
			SwitchEffect(EffectType.PlayerDeath).Forget();
		}
		if (playerDataById.characterType == CharacterType.Monster && playerDataById.Property.HP.Value <= 0)
		{
			BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(hpChange.Killer);
			if (playerDataById2 != null && playerDataById2.characterType == CharacterType.Hero && playerDataById.player.characterConfig.MonsterType == MonsterType.Elite)
			{
				SwitchEffect(EffectType.KillElites).Forget();
			}
		}
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		return UniTask.CompletedTask;
	}

	public async UniTask SwitchEffect(EffectType effectType)
	{
		await PlayEffect(effectType);
	}

	private async UniTask PlayEffect(EffectType effectType)
	{
		for (int i = 0; i < DreamEffects.Length; i++)
		{
			DreamEffects[i].SetActive(i == (int)effectType);
		}
		switch (effectType)
		{
		case EffectType.KillElites:
		case EffectType.Term:
			PlayNpcAnim(HumanNpcRoot, "Cheer");
			break;
		case EffectType.PlayerDeath:
			PlayNpcAnim(RobotNpcRoot, "Cheer");
			break;
		}
		Stage.inst.PlayOneShotSound(1104);
		await PlayEffectAnim(DreamEffects[(int)effectType]);
		PlayNpcAnim(HumanNpcRoot, "Idle");
		PlayNpcAnim(RobotNpcRoot, "Idle");
	}

	private async UniTask PlayEffectAnim(GameObject effectObj)
	{
		AnimatorStateInfo currentAnimatorStateInfo = effectObj.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0);
		float length = ((AnimatorStateInfo)(ref currentAnimatorStateInfo)).length;
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(length));
		effectObj.SetActive(value: false);
	}

	private void PlayNpcAnim(Transform npcRoot, string param)
	{
		for (int i = 0; i < npcRoot.childCount; i++)
		{
			npcRoot.GetChild(i).GetComponent<Animator>().SetTrigger(param);
		}
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
