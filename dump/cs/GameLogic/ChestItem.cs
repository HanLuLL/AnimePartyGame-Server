using UnityEngine;

namespace GameLogic;

public class ChestItem : BagItem
{
	public ChestInfoConfigure chestConfig { get; private set; }

	public void Init(int chestId)
	{
		ChestInfoConfigure value;
		if (chestId == 0)
		{
			Debug.LogError($"策划没有配置宝箱的分表id！itemId:{base.config.Id}");
		}
		else if (StaticConfigure.Chest.InfoDict.TryGetValue(chestId, out value))
		{
			chestConfig = value;
		}
	}
}
