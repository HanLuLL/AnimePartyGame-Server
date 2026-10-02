using System.Collections.Generic;
using Core;
using FairyGUI;
using Tools;
using UI;

namespace GameLogic;

public class GachaItem
{
	private uint _skinVoicePlayId;

	public int ItemId { get; private set; }

	public int Count { get; private set; }

	public ItemInfoConfigure itemInfo { get; private set; }

	public List<KeyValuePair<int, int>> replaceItemIds { get; private set; }

	public bool newAcquire { get; private set; }

	public bool _isOwn { get; private set; }

	public GachaItem(int _itemId, int _count, bool isOwn)
	{
		ItemId = _itemId;
		Count = _count;
		InitState(isOwn);
		AdjustAcquire(isOwn);
		_isOwn = isOwn;
	}

	private void InitState(bool isOwn)
	{
		itemInfo = ItemId.GetItemInfoConfigure();
		if (!(itemInfo.IsAutoTransform && isOwn))
		{
			return;
		}
		replaceItemIds = new List<KeyValuePair<int, int>>(itemInfo.Transform.Count);
		foreach (KeyValuePair<int, int> item in itemInfo.Transform)
		{
			replaceItemIds.Add(new KeyValuePair<int, int>(item.Key, item.Value));
		}
	}

	private void AdjustAcquire(bool isOwn)
	{
		ItemType itemType = itemInfo.ItemType;
		if (itemType == ItemType.AccountBackground || itemType == ItemType.AccountHeadShot || itemType == ItemType.Effect || itemType == ItemType.CardBack || itemType == ItemType.Dice || itemType == ItemType.Hero || itemType == ItemType.HeroStandingPainting)
		{
			newAcquire = !isOwn;
		}
		else
		{
			newAcquire = false;
		}
	}

	public void PlaySkinVoice()
	{
		if (itemInfo.ItemType != ItemType.Hero)
		{
			return;
		}
		SkinStandingPaintingConfigureItem skinConfig = GetSkinConfig();
		if (skinConfig == null)
		{
			return;
		}
		int voice = skinConfig.Voice;
		if (voice != 0)
		{
			CharacterVoiceConfigure voiceConfigure = voice.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				_skinVoicePlayId = SimpleSingletonProvider<AudioManager>.inst.SendEvent(voiceConfigure.FanfareVoice, Stage.inst.gameObject);
			}
		}
	}

	public void StopSkinVoice()
	{
		if (_skinVoicePlayId != 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(_skinVoicePlayId);
		}
	}

	public SkinStandingPaintingConfigureItem GetSkinConfig()
	{
		SkinStandingPaintingConfigureItem result = null;
		if (itemInfo.ItemType == ItemType.Hero)
		{
			result = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(itemInfo.SubMeterID, 0, 0);
		}
		return result;
	}
}
