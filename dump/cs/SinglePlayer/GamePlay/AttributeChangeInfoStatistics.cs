using System.Collections.Generic;

namespace SinglePlayer.GamePlay;

public class AttributeChangeInfoStatistics
{
	private Dictionary<AttributeChangeSource, int> _goldDict = new Dictionary<AttributeChangeSource, int>();

	public int ChangeStar { get; private set; }

	public int ChangeHP { get; private set; }

	public int ChangeGold { get; private set; }

	public void Combine(AttributeChangeInfo message)
	{
		ChangeStar += message.ChangeStar;
		ChangeHP += message.ChangeHP;
		ChangeGold += message.ChangeGold;
		if (_goldDict.ContainsKey(message.Source.type))
		{
			_goldDict[message.Source.type] += message.ChangeGold;
		}
		else
		{
			_goldDict.Add(message.Source.type, message.ChangeGold);
		}
	}

	public int GetGold()
	{
		int num = 0;
		foreach (KeyValuePair<AttributeChangeSource, int> item in _goldDict)
		{
			if (item.Key != AttributeChangeSource.Building)
			{
				num += item.Value;
			}
		}
		return num;
	}
}
