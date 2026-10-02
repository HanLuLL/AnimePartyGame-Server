using Tools;
using UI;

namespace GameLogic;

public class PveExpItemData
{
	public int Id;

	public int Exp;

	public readonly ItemInfoConfigure itemInfo;

	public int selectCount;

	public UIHero_Com_PveDrop dropItem;

	public int Count => SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(Id);

	public PveExpItemData(PVENurturanceItemExpConfigure expItemConfig)
	{
		Id = expItemConfig.Id;
		itemInfo = expItemConfig.Id.GetItemInfoConfigure();
		Exp = expItemConfig.Exp;
		selectCount = 0;
	}
}
