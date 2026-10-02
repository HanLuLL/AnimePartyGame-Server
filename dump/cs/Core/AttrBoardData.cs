namespace Core;

public class AttrBoardData
{
	public string PlayerName;

	public string AttrName;

	public int AttrValue;

	public string TargetName;

	public string Source;

	public string action;

	public string GetDesc()
	{
		return $"【{PlayerName}】: {AttrName} ~ {AttrValue}, {TargetName}, {Source}";
	}
}
