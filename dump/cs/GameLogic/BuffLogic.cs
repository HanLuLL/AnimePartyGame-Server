using Cysharp.Threading.Tasks;
using Tools;
using party.protocol;

namespace GameLogic;

public class BuffLogic : IRPCSync
{
	public readonly Signal buff = new Signal();

	public void Connect()
	{
	}

	public void Disconnect()
	{
	}

	public async UniTask OnBuffChanged(BattlePlayerData playerData, HeroBuffChangeS2C model)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById != null)
		{
			switch (model.Op)
			{
			case HeroBuffChangeS2C.Types.Oper.Noop:
				await playerDataById.buffContainer.UpdateBuff(model.Buffs);
				break;
			case HeroBuffChangeS2C.Types.Oper.Insert:
				await playerDataById.buffContainer.InsertBuff(model.Buff);
				break;
			case HeroBuffChangeS2C.Types.Oper.Delete:
				playerDataById.buffContainer.DeleteBuff(model.Buff);
				break;
			case HeroBuffChangeS2C.Types.Oper.Update:
				await playerDataById.buffContainer.ChangeBuff(model.Buff);
				break;
			}
			buff.Dispatch();
		}
	}
}
