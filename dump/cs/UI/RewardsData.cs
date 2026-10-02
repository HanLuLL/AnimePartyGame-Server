using System.Collections.Generic;

namespace UI;

public class RewardsData
{
	public int Type;

	public readonly Queue<List<KeyValuePair<int, int>>> showQueue = new Queue<List<KeyValuePair<int, int>>>();

	public readonly List<KeyValuePair<int, int>> itemList = new List<KeyValuePair<int, int>>();

	public readonly Queue<List<AutoTransformRewardsData>> autoTransformResultQueue = new Queue<List<AutoTransformRewardsData>>();

	public List<AutoTransformRewardsData> autoTransformResult = new List<AutoTransformRewardsData>();

	public bool needShow()
	{
		return showQueue.Count > 0;
	}
}
