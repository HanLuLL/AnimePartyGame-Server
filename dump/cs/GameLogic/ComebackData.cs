using Tools;
using party.model;

namespace GameLogic;

public class ComebackData
{
	public ReturnInfo ReturnInfo => SimpleSingletonProvider<GameLogicManager>.inst.account?.GetPlayerInfo()?.ReturnInfo;

	public bool IsInPeriod()
	{
		if (ReturnInfo == null)
		{
			return false;
		}
		return TimeHelper.ValidityTime(ReturnInfo.TriggerTime, ReturnInfo.EndTime);
	}
}
