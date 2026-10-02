using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class BattlePassRewardData
{
	public ItemInfoConfigure itemConfig;

	public int rewardCount;

	public string specialIcon;

	public int LV;

	public int infoType;

	public bool vailLV => LV <= SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.LV;

	public BattlePassRewardData(MapField<int, int> _Reward, int _LV, int _infoType, string _SpecialIcon)
	{
		itemConfig = _Reward.ElementAt(0).Key.GetItemInfoConfigure();
		rewardCount = _Reward.ElementAt(0).Value;
		specialIcon = _SpecialIcon;
		infoType = _infoType;
		LV = _LV;
	}
}
