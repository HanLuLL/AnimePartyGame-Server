using UnityEngine.Scripting;

namespace SinglePlayer.GamePlay.Relic;

[Preserve]
public class RelicInfo_60008 : RelicBuff
{
	public override void Initialize(int relicId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicId, out Config))
		{
			Id = relicId;
			OnThrowDice = new RelicBuffModule_60008();
		}
	}
}
