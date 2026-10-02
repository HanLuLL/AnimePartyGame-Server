using UnityEngine;
using party.model;

namespace GameLogic;

public class PCAccountBase : AccountBase<ulong>
{
	public PCAccountBase(AccountInfo _accountInfo, Player _playerInfo)
	{
		Login(_accountInfo, _playerInfo);
		base.AccountID = _accountInfo.AccountId;
		RunTimeAstralErrorHandler runTimeAstralErrorHandler = Object.FindObjectOfType<RunTimeAstralErrorHandler>();
		if (runTimeAstralErrorHandler != null)
		{
			runTimeAstralErrorHandler.UpdatePlayerId(base.AccountID, _accountInfo.Nick);
		}
	}
}
