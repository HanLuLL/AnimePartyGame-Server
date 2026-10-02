using System.Collections.Generic;

namespace Core;

public class RegisterTabInfo
{
	private readonly Dictionary<int, List<AttrBoardData>> attrDataDict = new Dictionary<int, List<AttrBoardData>>();

	public void UpdateData(int roundIndex, AttrBoardData attrBoard)
	{
		if (attrDataDict.TryGetValue(roundIndex, out var value))
		{
			value.Add(attrBoard);
			return;
		}
		List<AttrBoardData> value2 = new List<AttrBoardData> { attrBoard };
		attrDataDict.Add(roundIndex, value2);
	}

	public List<AttrBoardData> GetData(int roundIndex)
	{
		if (!attrDataDict.TryGetValue(roundIndex, out var value))
		{
			return null;
		}
		return value;
	}

	public void Dispose()
	{
		attrDataDict.Clear();
	}
}
