using Tools;

namespace GameLogic;

public class BagItem
{
	public ReactiveProperty<int> count;

	public ItemInfoConfigure config { get; private set; }

	public bool enableUse
	{
		get
		{
			if (StaticConfigure.Item.TagDict.TryGetValue((int)config.ItemType, out var value))
			{
				return value.EnableUse;
			}
			return false;
		}
	}

	public void Init(ItemInfoConfigure config, int count)
	{
		this.config = config;
		this.count = new ReactiveProperty<int>(count);
	}
}
