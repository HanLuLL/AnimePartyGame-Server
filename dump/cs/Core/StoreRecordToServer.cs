namespace Core;

public class StoreRecordToServer
{
	public string id;

	public string uid;

	public string action;

	public string msg = "shopping";

	public string goods_id;

	public string shop_id;

	public LogToServerType storeAction
	{
		set
		{
			action = value switch
			{
				LogToServerType.GOODS_DETAIL => "goods_details", 
				LogToServerType.SHOPPING => "shopping", 
				LogToServerType.SHOPPING_OK => "shopping_ok", 
				_ => action, 
			};
		}
	}
}
