using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuff
{
	public int Id;

	private Int32Encryptor chargeCount = new Int32Encryptor();

	public SinglePlayerRelicConfigure Config;

	public BaseRelicBuffModule OnCreate;

	public BaseRelicBuffModule OnRemove;

	public BaseRelicBuffModule OnRoundStart;

	public BaseRelicBuffModule OnRoundEnd;

	public BaseRelicBuffModule OnThrowDice;

	public BaseRelicBuffModule OnPassLand;

	public BaseRelicBuffModule OnMoveStop;

	public BaseRelicBuffModule OnSellBuilding;

	public BaseRelicBuffModule OnRefreshShop;

	public BaseRelicBuffModule OnBuildingTriggerStayEffect;

	public int ChargeCount
	{
		get
		{
			return chargeCount.DecryptGet();
		}
		set
		{
			chargeCount.EncryptSet(value);
		}
	}

	public virtual void Initialize(int relicId)
	{
	}
}
