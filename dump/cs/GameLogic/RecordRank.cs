using Core;
using Tools;
using party.model;

namespace GameLogic;

public class RecordRank
{
	private readonly string _nick;

	private readonly string coverName;

	public PlayerFightData data;

	public string nick
	{
		get
		{
			if (GameSettings.IncoverMode && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(data.PlayerId))
			{
				return coverName;
			}
			return _nick;
		}
	}

	public RecordRank(PlayerFightData _data, string _coverName)
	{
		coverName = _coverName;
		data = _data;
		_nick = _data.Name;
	}
}
