using party.model;

namespace GameLogic;

public abstract class AccountBase<T> : IAccount
{
	protected AccountInfo accountInfo;

	protected string Nick;

	protected Player playerInfo;

	public T AccountID { get; protected set; }

	public void Login(AccountInfo _accountInfo, Player _playerInfo)
	{
		accountInfo = _accountInfo;
		playerInfo = _playerInfo;
		Nick = accountInfo.Nick;
	}

	public bool IsSelf(long playerId)
	{
		return playerInfo.Id == playerId;
	}

	public string GetAccountName()
	{
		return Nick;
	}

	public AccountInfo GetAccountInfo()
	{
		return accountInfo;
	}

	public Player GetPlayerInfo()
	{
		return playerInfo;
	}

	public void UpdateLevel(int level, int exp)
	{
		playerInfo.Level = level;
		playerInfo.Exp = exp;
	}

	public void SetAccountName(string name)
	{
		Nick = name;
	}
}
