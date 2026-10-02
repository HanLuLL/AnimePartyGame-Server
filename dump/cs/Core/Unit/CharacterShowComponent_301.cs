using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_301 : CharacterShowComponent
{
	private UICom_NGOCounter com_NGOCounter;

	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		UpdateBuffCounter();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(UpdateBuffCounter);
	}

	public override void Dispose()
	{
		base.Dispose();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(UpdateBuffCounter);
	}

	private void UpdateBuffCounter()
	{
		BuffContainer buffContainer = Owner.player.buffContainer;
		int num = 0;
		if (buffContainer != null)
		{
			foreach (KeyValuePair<long, Buff> item in buffContainer._buffDict)
			{
				int buffId = item.Value.BuffId;
				if (buffId == 3011201 || buffId == 3010101)
				{
					num += item.Value.Progress;
				}
			}
		}
		(Owner?.window.Com_PlayerAttrInfo)?.TryUpdateNGOCounter(num);
	}

	public override void RefreshAttrInfo()
	{
		UpdateBuffCounter();
		base.RefreshAttrInfo();
	}

	public override string TryGetAttackTimelineAsset(MapField<int, string> attackDict)
	{
		SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem = Owner?.player?.standingPainting;
		if (skinStandingPaintingConfigureItem != null && skinStandingPaintingConfigureItem.ItemID == 100301003)
		{
			BuffContainer buffContainer = Owner.player.buffContainer;
			int num = 0;
			if (buffContainer != null)
			{
				foreach (KeyValuePair<long, Buff> item in buffContainer._buffDict)
				{
					int buffId = item.Value.BuffId;
					if (buffId == 3011201 || buffId == 3010101)
					{
						num += item.Value.Progress;
					}
				}
			}
			foreach (int key in attackDict.Keys)
			{
				if (key != 0)
				{
					if (num >= key)
					{
						return attackDict[key];
					}
					break;
				}
			}
		}
		return base.TryGetAttackTimelineAsset(attackDict);
	}
}
