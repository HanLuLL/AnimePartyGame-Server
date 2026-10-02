using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Card;

public abstract class CardBase : ICard
{
	public int UID;

	public int Count { get; protected set; }

	public int Price => CalculatePrice();

	public int BuyPrice
	{
		get
		{
			float cardCostReductionBonus = Game.GetModel<GameData>().heroProperty.CardCostReductionBonus;
			return Mathf.FloorToInt((float)Price * (1f - cardCostReductionBonus));
		}
	}

	public ReactiveEncryptorProperty<int, Int32Encryptor> Exp { get; private set; } = new ReactiveEncryptorProperty<int, Int32Encryptor>(0);

	public ReactiveProperty<int> Level { get; private set; } = new ReactiveProperty<int>(1);

	protected virtual int CalculatePrice()
	{
		return 0;
	}

	public abstract UniTask UseCard();

	protected abstract UniTask Upgrade();

	public virtual void Dispose()
	{
	}
}
