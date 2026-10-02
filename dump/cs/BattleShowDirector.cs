using System;
using System.Collections.Generic;
using Cinemachine;
using Core;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;
using party.model;

[RequireComponent(typeof(PlayableDirector))]
public class BattleShowDirector : Unit
{
	private enum PKShowProcess
	{
		None,
		ShowDice,
		AttackerHit,
		DefenderDead,
		ChainHit,
		BattleDraw
	}

	[Header("对战相机")]
	public PKCameraManager PKCamera;

	[SerializeField]
	private PlayableDirector _director;

	private DirectorWrapMode directorWrapMode;

	[SerializeField]
	private BattleActor _attacker;

	[SerializeField]
	private BattleActor _victim;

	[SerializeField]
	private BattleActor _chainAttacker;

	[SerializeField]
	private BattlePlatform _BattlePlatform;

	public long attackerId;

	private BattlePlayerData _attackerData;

	public long defendeId;

	private BattlePlayerData _defenderData;

	private bool COUNTERATTACK;

	[SerializeField]
	public BattleEffectManager BattleEffect;

	private PKShowProcess _pkShowProcess;

	public PlayableDirector director => _director;

	private bool DirectorPlaying
	{
		get
		{
			if (director.state == PlayState.Playing)
			{
				return _director.time < _director.duration;
			}
			return false;
		}
	}

	public BattleActor attacker => _attacker;

	public BattleActor victim => _victim;

	public BattleActor ChainAttacker => _chainAttacker;

	public BattlePlatform battlePlatform => _BattlePlatform;

	private BattleFightData fightData => SimpleSingletonProvider<GameLogicManager>.inst.fight.battleFightData;

	protected override void Awake()
	{
		_director.paused -= OnPaused;
		_director.paused += OnPaused;
	}

	public void UpdateFightConfig(Transform volumeTrigger)
	{
		UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(PKCamera.PKMainCamera);
		Camera UICamera = StageCamera.main;
		if (!universalAdditionalCameraData.cameraStack.Exists((Camera x) => x == UICamera))
		{
			universalAdditionalCameraData.cameraStack.Add(UICamera);
		}
		universalAdditionalCameraData.volumeTrigger = volumeTrigger;
		base.transform.gameObject.SetActiveEx(active: false);
		battlePlatform.InitBattlePlatform();
	}

	public void Play(string timelineAssetName, DirectorWrapMode wrapMode = DirectorWrapMode.None)
	{
		TimelineAsset timelineAsset = GetTimelineAsset(timelineAssetName);
		Play(timelineAsset, wrapMode);
	}

	private void Play(TimelineAsset _timelineAsset, DirectorWrapMode wrapMode = DirectorWrapMode.None)
	{
		foreach (PlayableBinding output in ((PlayableAsset)(object)_timelineAsset).outputs)
		{
			UnityEngine.Object genericBinding = _director.GetGenericBinding(output.sourceObject);
			if (genericBinding != null)
			{
				_director.SetGenericBinding(output.sourceObject, genericBinding);
			}
		}
		directorWrapMode = wrapMode;
		_director.Play((PlayableAsset)(object)_timelineAsset, wrapMode);
		if (_director.playableGraph.IsValid())
		{
			Playable rootPlayable = _director.playableGraph.GetRootPlayable(0);
			if (rootPlayable.IsValid())
			{
				rootPlayable.SetSpeed(BattleConfig.RoleAnimatorSpeed);
			}
		}
	}

	public void Pause()
	{
		_director.Pause();
		_attacker.player.Pause();
		_victim.player.Pause();
	}

	public void Resume()
	{
		_director.Resume();
		_attacker.player.Resume();
		_victim.player.Resume();
	}

	private void OnPaused(PlayableDirector director)
	{
	}

	private TimelineAsset GetTimelineAsset(string name)
	{
		return SimpleSingletonProvider<InternalAssetManager>.inst.GetTimelineAsset(name);
	}

	public void ActiveBattlePlatform(long _atkPlayerId, long _defPlayerId)
	{
		BattleEffect.DestroyBattleElement();
		SimpleSingletonProvider<GameLogicManager>.inst.fight.fightStatus = true;
		battlePlatform.OpenFightBackGround(_atkPlayerId);
		base.transform.gameObject.SetActiveEx(active: true);
		attackerId = _atkPlayerId;
		defendeId = _defPlayerId;
		_attackerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_atkPlayerId);
		_defenderData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_defPlayerId);
		BattleResourceInfoConfigure battleResConfig = _attackerData.player.standingPainting.BattleResConfig;
		_attacker.UpdateTimeline(PKCampType.Attacker, _attackerData, battleResConfig.ActorPositionAtk);
		_victim.UpdateTimeline(PKCampType.Defender, _defenderData, battleResConfig.ActorPositionDef);
		_chainAttacker.gameObject.SetActiveEx(active: false);
		_attacker.RefreshUI(_isAttack: true, _atkPlayerId);
		_victim.RefreshUI(_isAttack: false, _defPlayerId);
		PKCamera.EnablePKCamera();
		BattleSceneController.inst.mainCamera.enabled = false;
		int fightBackGroundPP = _attackerData.player.standingPainting.FightBackGroundPP;
		if (StaticConfigure.BattleResource.PostProcessDict.TryGetValue(fightBackGroundPP, out var value))
		{
			BattleSceneController.inst.SetVolumeBloom(value);
		}
	}

	public async UniTask SyncBuffEffect()
	{
		await _attacker.CreateBuffEffect(_attackerData.buffContainer._buffDict);
		await _victim.CreateBuffEffect(_defenderData.buffContainer._buffDict);
	}

	private void ClearStatus()
	{
		BattleEffect.DestroyBattleElement();
		_attacker.DestroyBuffEffect();
		_victim.DestroyBuffEffect();
		_victim.ClearState();
	}

	public void CloseBattlePlatform()
	{
		BattleSceneController.inst.ResetVolumeBloom();
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.None);
		SimpleSingletonProvider<GameLogicManager>.inst.fight.fightStatus = false;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM?.ContinueBGMAfterFight();
		PKCamera.DisablePKCamera();
		BattleSceneController.inst.mainCamera.enabled = true;
		_director.Stop();
		_attacker.ClosePlatform();
		_victim.ClosePlatform();
		_chainAttacker.ClosePlatform();
		base.transform.gameObject.SetActiveEx(active: false);
		ClearStatus();
		battlePlatform.CloseFightBackGround();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
	}

	public void ActiveAttackerAppear()
	{
		attacker.PlayFabfareAttack();
	}

	public void ShowReadyLabel(long _playerId)
	{
		_attacker._UI.RefreshReadyLabel(_playerId);
		_victim._UI.RefreshReadyLabel(_playerId);
	}

	public void CloseReadyLabel()
	{
		attacker.PlayIdle();
		_attacker._UI.com_Attack.showReady.selectedIndex = 0;
		_victim._UI.com_Defend.showReady.selectedIndex = 0;
	}

	public async UniTask RefreshDice_Defender(int index, int modelVal, bool modelDodge, bool counterAttack)
	{
		COUNTERATTACK = counterAttack;
		_attacker._UI.DestroyAttackMaxPointEffect();
		_victim._UI.RefreshPoint_Defender(index, modelVal, modelDodge);
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		_attacker._UI.com_Attack.showPoint.selectedIndex = 0;
		_attacker._UI.com_Attack.ShowUp.selectedIndex = 1;
		_victim._UI.com_Defend.ShowUp.selectedIndex = 1;
	}

	private List<PointInfo> GetPointInfos(BattleRole roleInfo, bool isAtk, bool isDodgeFail)
	{
		List<PointInfo> list = new List<PointInfo>();
		int point = roleInfo.Point;
		long playerId = roleInfo.PlayerId;
		RepeatedField<cardCombat> cardCombatBonus = roleInfo.CardCombatBonus;
		int num = 0;
		foreach (cardCombat item in cardCombatBonus)
		{
			num += item.CardBonus;
		}
		int num2 = (isAtk ? roleInfo.Atk : roleInfo.Def) - num - roleInfo.Point;
		num2 = ((!isDodgeFail) ? num2 : 0);
		point += num2;
		list.Add(new PointInfo
		{
			CurrentPoint = num2,
			TotalPoint = point,
			IsInitVal = true,
			CardId = 0,
			PlayerId = playerId
		});
		if (!isDodgeFail)
		{
			RepeatedField<int> useCards = roleInfo.UseCards;
			RepeatedField<cardCombat> cardCombatBonus2 = roleInfo.CardCombatBonus;
			int num3 = useCards?.Count ?? 0;
			for (int i = 0; i < num3; i++)
			{
				int cardId = cardCombatBonus2[i].CardId;
				int cardBonus = cardCombatBonus2[i].CardBonus;
				point += cardBonus;
				list.Add(new PointInfo
				{
					CurrentPoint = cardBonus,
					TotalPoint = point,
					IsInitVal = false,
					CardId = cardId,
					PlayerId = playerId
				});
			}
		}
		return list;
	}

	private int CalculateWaitTimeMS(int atkPointCount, int defPointCount, float interval = 0.1f)
	{
		int num = 7;
		int num2 = Mathf.Min(Mathf.Max(atkPointCount - 1, defPointCount - 1), num);
		float value = (float)num2 * interval + 0.5f;
		float min = 0.6f;
		float max = (float)(num2 * num) + 0.5f;
		return Mathf.RoundToInt((Mathf.Clamp(value, min, max) + 0.4f * Mathf.Min(num2, 1.5f)) * 1000f);
	}

	private async UniTask<bool> Dice_Show()
	{
		_pkShowProcess = PKShowProcess.ShowDice;
		attacker._UI.com_Attack.showPoint.selectedIndex = 2;
		victim._UI.com_Defend.showPoint.selectedIndex = 2;
		if (!(await victim._UI.RefreshDice_Defender(0)))
		{
			return false;
		}
		float num = 0.2f;
		if (IsHit())
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.fight.defendData.Dodge)
			{
				List<PointInfo> pointInfos = GetPointInfos(fightData.attackerInfo, isAtk: true, isDodgeFail: false);
				List<PointInfo> pointInfos2 = GetPointInfos(fightData.defenderInfo, isAtk: false, isDodgeFail: true);
				SimpleSingletonProvider<UIManager>.inst.Fight.ShowFightValue(pointInfos, pointInfos2, num).Forget();
				int millisecondsDelay = CalculateWaitTimeMS(pointInfos.Count, pointInfos2.Count, num);
				bool num2 = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay);
				Mathf.Min(0, 1000);
				if (num2)
				{
					return false;
				}
				victim._UI.com_Defend.showResult.selectedIndex = 2;
				victim._UI.com_Defend.showPoint.selectedIndex = 0;
				attacker._UI.RefreshPoint_Attacker(2, fightData.attackerInfo.Atk);
				victim._UI.RefreshPoint_Defender(2, 0);
				if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(800))
				{
					return false;
				}
			}
			else
			{
				if (SimpleSingletonProvider<UIManager>.inst.Fight.contentPane is UIFightWindow uIFightWindow)
				{
					Debug.Log($"fightWindow.step.selectedIndex : {uIFightWindow.step.selectedIndex}");
				}
				List<PointInfo> pointInfos3 = GetPointInfos(fightData.attackerInfo, isAtk: true, isDodgeFail: false);
				List<PointInfo> pointInfos4 = GetPointInfos(fightData.defenderInfo, isAtk: false, isDodgeFail: false);
				SimpleSingletonProvider<UIManager>.inst.Fight.ShowFightValue(pointInfos3, pointInfos4, num).Forget();
				int millisecondsDelay2 = CalculateWaitTimeMS(pointInfos3.Count, pointInfos4.Count, num);
				if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay2))
				{
					return false;
				}
				attacker._UI.RefreshPoint_Attacker(2, fightData.attackerInfo.Atk);
				victim._UI.RefreshPoint_Defender(2, fightData.defenderInfo.Def);
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(800);
			}
			SimpleSingletonProvider<UIManager>.inst.Fight.HideFightValue();
		}
		else
		{
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500))
			{
				return false;
			}
			if (victim._UI.com_Defend.changeState.selectedIndex == 1)
			{
				victim._UI.com_Defend.showResult.selectedIndex = 1;
			}
		}
		return true;
	}

	public async UniTask PlayBattleResult()
	{
		if (!(await Dice_Show()))
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo != null && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2 && SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
		_pkShowProcess = PKShowProcess.AttackerHit;
		TimelineAsset timelineAsset = GetTimelineAsset(attacker.GetHitDirect());
		Play(timelineAsset);
		if (!(await attacker.PlayAttack()))
		{
			return;
		}
		_attacker.PlayBattleIdleAttackEnd();
		_victim._UI.com_Defend.showResult.selectedIndex = 0;
		_attacker._UI.com_Attack.showPoint.selectedIndex = 0;
		_victim._UI.com_Defend.showPoint.selectedIndex = 0;
		if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying))
		{
			return;
		}
		if (VictimDead())
		{
			_attacker.PlayWin();
			if (await PlayDefenderDead(VictimHp()))
			{
				return;
			}
		}
		else if (fightData.defenderInfo.ChainAttacker != 0L)
		{
			if (await PlayChainHit())
			{
				return;
			}
		}
		else if (await PlayBattleDraw())
		{
			return;
		}
		if (!COUNTERATTACK)
		{
			BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = 0f;
			if (await SimpleSingletonProvider<UIManager>.inst.Fight.FinishFight())
			{
				CloseBattlePlatform();
				await SimpleSingletonProvider<UIManager>.inst.Fight.CloseFightWin();
				BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)StaticGlobalData.GAME_CAMERA_SWITCH_TIME / 1000f;
			}
		}
		else
		{
			ClearStatus();
		}
	}

	private async UniTask<bool> PlayBattleDraw()
	{
		_pkShowProcess = PKShowProcess.BattleDraw;
		SimpleSingletonProvider<UIManager>.inst.Fight.ShowResultTip(isDead: false);
		_victim._UI.RefreshLife(Mathf.Max(VictimHp(), 0));
		Play("BattleStep_Draw");
		return await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
	}

	private async UniTask<bool> PlayChainHit()
	{
		_victim._UI.RefreshLife(Mathf.Max(VictimHp(), 0));
		_pkShowProcess = PKShowProcess.ChainHit;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(fightData.defenderInfo.ChainAttacker);
		if (playerDataById != null)
		{
			BattleResourceInfoConfigure battleResConfig = _attackerData.player.standingPainting.BattleResConfig;
			_chainAttacker.UpdateTimeline(PKCampType.ChainAttacker, playerDataById, battleResConfig.ActorPositionChain);
			if (await _chainAttacker.PlayChainHit())
			{
				return true;
			}
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying))
			{
				return true;
			}
		}
		if (VictimDead())
		{
			_attacker.PlayWin();
			if (await PlayDefenderDead(VictimHp()))
			{
				return true;
			}
		}
		else if (await PlayBattleDraw())
		{
			return true;
		}
		return false;
	}

	private async UniTask<bool> PlayDefenderDead(int defenderHP)
	{
		_pkShowProcess = PKShowProcess.DefenderDead;
		SimpleSingletonProvider<UIManager>.inst.Fight.ShowResultTip(isDead: true);
		_victim.DestroyBuffEffect();
		int curHp = Mathf.Max(defenderHP, 0);
		if (_attackerData.characterType != CharacterType.Monster)
		{
			_victim.PlayDead();
		}
		else
		{
			curHp = Mathf.Max(defenderHP, 1);
		}
		_victim._UI.RefreshLife(curHp);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.KILL, attackerId);
		Play("BattleStep_SoloWin");
		return await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
	}

	public async UniTaskVoid PlayExHit(float _victimDuration)
	{
		if (VictimDead() && !(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(_victimDuration))))
		{
			_attacker._UI.SwitchHpState(show: false);
			Play("BattleStep_ExHit", DirectorWrapMode.Hold);
		}
	}

	public bool VictimDead()
	{
		return VictimHp() <= 0;
	}

	private int VictimHp()
	{
		int result = _defenderData.Property.HP.Value + fightData.defenderInfo.IncHp;
		if (_pkShowProcess == PKShowProcess.ChainHit)
		{
			result = _defenderData.Property.HP.Value + fightData.defenderInfo.IncHp + fightData.defenderInfo.ChainAttackDamage;
		}
		return result;
	}

	public int VictimIncHp()
	{
		if (_pkShowProcess == PKShowProcess.ChainHit)
		{
			return fightData.defenderInfo.ChainAttackDamage;
		}
		return fightData.defenderInfo.IncHp;
	}

	public void GenerateImpulse(CinemachineImpulseAsset impulseAsset)
	{
		CinemachineImpulseSource[] componentsInChildren = base.gameObject.GetComponentsInChildren<CinemachineImpulseSource>();
		foreach (CinemachineImpulseSource impulseSource in componentsInChildren)
		{
			SimpleSingletonProvider<CameraManager>.inst.GenerateImpulse(impulseSource, impulseAsset);
		}
	}

	public void PlayApproach()
	{
		TimelineAsset timelineAsset = GetTimelineAsset(attacker.GetApproachDirect());
		Play(timelineAsset);
	}

	public bool IsHit()
	{
		if (fightData == null)
		{
			return false;
		}
		if (_pkShowProcess != PKShowProcess.ChainHit)
		{
			if (fightData.defenderInfo.Dodge)
			{
				return !DefendDiceSuccess();
			}
			return true;
		}
		return true;
	}

	private bool DefendDiceSuccess()
	{
		if (fightData == null)
		{
			return false;
		}
		if (fightData.attackerInfo.Point == 6)
		{
			return fightData.defenderInfo.Point == 6;
		}
		return fightData.attackerInfo.Point < fightData.defenderInfo.Point;
	}

	public void PlayDetachDirect()
	{
		TimelineAsset timelineAsset = GetTimelineAsset(attacker.GetDetachDirect());
		Play(timelineAsset);
	}
}
