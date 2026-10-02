using party.model;

namespace GameLogic;

public interface IAccount
{
	void Login(AccountInfo _accountInfo, Player _playerInfo);

	bool IsSelf(long playerId);

	string GetAccountName();

	AccountInfo GetAccountInfo();

	Player GetPlayerInfo();

	void UpdateLevel(int level, int exp);

	void SetAccountName(string name);
}
