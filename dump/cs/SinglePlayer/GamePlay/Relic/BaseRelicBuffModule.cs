using System.Collections.Generic;
using SinglePlayer.GamePlay.Build;

namespace SinglePlayer.GamePlay.Relic;

public abstract class BaseRelicBuffModule
{
	public virtual bool TryAddChargeOnThrowDice(RelicInfo info, List<int> dicePoints)
	{
		return false;
	}

	public virtual bool TryAddChargeOnBuildTriggerEffect(RelicInfo info, int configId, ParameterType effectType)
	{
		return false;
	}

	public abstract void Apply(RelicInfo info);
}
