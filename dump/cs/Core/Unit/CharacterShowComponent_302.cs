using System.Collections.Generic;
using Google.Protobuf.Collections;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_302 : CharacterShowComponent
{
	public override string TryGetAttackTimelineAsset(MapField<int, string> attackDict)
	{
		SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem = Owner?.player?.standingPainting;
		if (skinStandingPaintingConfigureItem != null && skinStandingPaintingConfigureItem.ItemID == 100301004)
		{
			BuffContainer buffContainer = Owner.player.buffContainer;
			int num = 0;
			if (buffContainer != null)
			{
				foreach (KeyValuePair<long, Buff> item in buffContainer._buffDict)
				{
					int buffId = item.Value.BuffId;
					if (buffId == 3020101 || buffId == 3021101)
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
