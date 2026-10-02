using System.Collections.Generic;
using Core;
using Core.Unit;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class BattlePlayerData
{
	public RoomPlayer player;

	public Character CharacterInst;

	public int rank;

	public int KillCount;

	public int TotalDie;

	public int TotalDamage;

	public int TotalInjured;

	public int TreatmentScore;

	public int SkillCD;

	private AltArtCardData altArtCardData = new AltArtCardData();

	public BattleProperty Property => player.Property;

	public CardContainer cardContainer => player.cardContainer;

	public BuffContainer buffContainer => player.buffContainer;

	public CharacterType characterType => player.characterType;

	public BattlePlayerData(RoomPlayer _player)
	{
		player = _player;
		Debug.Log($"#BattleData#玩家：初始化玩家{player.GetNick()}_{player.Id} \n角色ID:{player.Hero.HeroId} \n出生点：{player.NodeId} \n");
		altArtCardData.InitFromServer(_player.serverPlayer.AltArtCards);
	}

	public bool TryGetAltArtCardId(int cardId, out int altArtCardId)
	{
		return altArtCardData.TryGetArtCardId(cardId, out altArtCardId);
	}

	public void InitCharacter(bool initialization)
	{
		if (characterType == CharacterType.Hero)
		{
			InitBornLand();
		}
		GameObject gameObject = SimpleSingletonProvider<CharacterAssetManager>.inst.InstantiateCharacter(player.Id, player.Hero.HeroId);
		gameObject.SetActiveEx(active: true);
		CharacterInst = gameObject.GetComponent<Character>();
		CharacterInst.InitData(this);
		if (initialization)
		{
			InitObjectState();
		}
		if (!initialization)
		{
			CharacterInst.showComponent?.UpdateAfterReconnect();
		}
		buffContainer.InitBuffEffect(CharacterInst);
		Property.ResetCharacter();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapUpdate.Dispatch();
	}

	public void UpdateCharacter(RoomPlayer _player)
	{
		player = _player;
		CharacterInst.UpdateData(this);
		CharacterInst.characterAnimator.UpdateAnimationObjectScale(CharacterInst.AnimationDefaultScale);
		CharacterInst.EffectContainer.localPosition = Vector3.zero;
		buffContainer.InitBuffEffect(CharacterInst);
		Property.ResetCharacter();
		CharacterInst.showComponent?.UpdateAfterReconnect();
	}

	private void InitObjectState()
	{
		if (!(CharacterInst == null))
		{
			HidePendant();
			CharacterInst.characterAnimator.DoScaleAnimation(new Vector3(0f, CharacterInst.AnimationDefaultScale + 1.6f, 1f), 0f);
		}
	}

	public void StartSend()
	{
		if (!(CharacterInst == null))
		{
			HidePendant();
			CharacterInst.characterAnimator.DoScaleAnimation(new Vector3(0f, CharacterInst.AnimationDefaultScale + 1.6f, 1f), 0.2f);
		}
	}

	public void StartSend_Car()
	{
		if (!(CharacterInst == null))
		{
			HidePendant();
			CharacterInst.characterAnimator.DoScaleAnimation(new Vector3(0f, 0f, 1f), 0.2f);
		}
	}

	private void HidePendant()
	{
		if (!(CharacterInst == null))
		{
			CharacterInst.EffectContainer.localPosition = Vector3.one * 10000f;
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(player.Id);
		}
	}

	public void FinishSend()
	{
		if (!(CharacterInst == null))
		{
			Vector3 scale = new Vector3(CharacterInst.AnimationDefaultScale, CharacterInst.AnimationDefaultScale, 1f);
			CharacterInst.characterAnimator.DoScaleAnimation(scale, 0.1f, delegate
			{
				CharacterInst.EffectContainer.localPosition = Vector3.zero;
				CharacterInst.ShowWalkDirections(player.FrontIds);
			});
		}
	}

	public void FlashShow()
	{
		if (!(CharacterInst == null))
		{
			CharacterInst.characterAnimator.UpdateAnimationObjectScale(CharacterInst.AnimationDefaultScale);
			CharacterInst.EffectContainer.localPosition = Vector3.zero;
			CharacterInst.ShowWalkDirections(player.FrontIds);
		}
	}

	public void Absorb()
	{
		if (!(CharacterInst == null))
		{
			HidePendant();
			CharacterInst.characterAnimator.DoScaleAnimation(new Vector3(0f, 0f, 0f), 0.2f);
		}
	}

	public void DisposeMonster_Hide()
	{
		if (!(CharacterInst == null))
		{
			HidePendant();
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapDelete.Dispatch(player.Id);
			Property.HP.JustSetValue(0);
			Dispose();
		}
	}

	public void InitBornLand()
	{
		int bornLandId = GetBornLandId();
		SimpleSingletonProvider<LandManager>.inst.GetLandById(bornLandId).InitBornMaterial(player, player.NodeId == bornLandId);
	}

	public int GetBornLandId()
	{
		foreach (var (result, unitLand2) in SimpleSingletonProvider<LandManager>.inst.NodeDict)
		{
			if (unitLand2.LandType == LandType.Born && unitLand2.GetNodeInfo().playerSerialNumber == player.Slot)
			{
				return result;
			}
		}
		Debug.LogError($"$通过玩家位置Slot:{player.Slot}, 无法地图配置中找到玩家出生点数据");
		return 0;
	}

	public void UpdateSelectedRelic(int relicId, bool isActivate = true)
	{
		if (player != null && relicId != 0)
		{
			player.TryUpdateSelectedRelic(relicId, isActivate);
		}
	}

	public List<int> GetRelicIds()
	{
		return player.ShowSelectedRelics;
	}

	public string GetCharacterHeadUrl()
	{
		CharacterHandle characterHandle = player?.characterConfig;
		if (characterHandle == null)
		{
			return "";
		}
		return characterHandle.CharacterMap;
	}

	public void Dispose()
	{
		if (CharacterInst != null)
		{
			CharacterInst.Dispose();
			CharacterInst.gameObject.SetActive(value: false);
			CharacterInst = null;
		}
		if (player != null)
		{
			player.Property?.Dispose();
		}
	}
}
