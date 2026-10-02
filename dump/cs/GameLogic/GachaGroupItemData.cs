namespace GameLogic;

public class GachaGroupItemData
{
	public GachaGroupConfigureItem GachaGroupConfigureItem;

	public float ItemPercentage;

	public GachaGroupItemData(GachaGroupConfigureItem GroupConfigureItem, float Percentage)
	{
		GachaGroupConfigureItem = GroupConfigureItem;
		ItemPercentage = Percentage;
	}
}
