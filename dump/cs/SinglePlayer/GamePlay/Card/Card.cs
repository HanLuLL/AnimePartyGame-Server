using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Card;

public class Card : CardBase
{
	private SinglePlayerCardConfigure _cardConfigure;

	public bool HasPurchase { get; set; }

	public SinglePlayerCardConfigure CardConfigure
	{
		get
		{
			return _cardConfigure;
		}
		set
		{
			_cardConfigure = value;
		}
	}

	public bool CanPlaced { get; set; }

	public Card()
	{
		base.Exp.AddListener(OnExpChange);
	}

	public override void Dispose()
	{
		base.Dispose();
		base.Exp.RemoveListener(OnExpChange);
	}

	protected override int CalculatePrice()
	{
		if (!CanSold())
		{
			return 0;
		}
		return StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0)?.CardPrice.GetSafeByIndex(_cardConfigure.Rarity) ?? 0;
	}

	public override UniTask UseCard()
	{
		return UniTask.CompletedTask;
	}

	public bool CanSold()
	{
		return CardConfigure.Rarity != 4;
	}

	public SinglePlayerCardConfigureItem GetConfigureItem()
	{
		return CardConfigure.SinglePlayerCardConfigureItems.GetSafeByIndex(base.Level.Value - 1);
	}

	public int GetSellAddPrice()
	{
		return GetConfigureItem()?.AddPrice ?? 0;
	}

	protected override async UniTask Upgrade()
	{
		Game.GetModel<GlobalSignal>().CardUpgrade.Dispatch(UID);
		await UniTask.CompletedTask;
	}

	public static int MaxLevel()
	{
		RepeatedField<SinglePlayerCardUpgradeConfigureItem> singlePlayerCardUpgradeConfigureItems = GetUpgradeConfigure().SinglePlayerCardUpgradeConfigureItems;
		return singlePlayerCardUpgradeConfigureItems[singlePlayerCardUpgradeConfigureItems.Count - 1].Star;
	}

	public int GetUpgradeExp()
	{
		int level = base.Level.Value + 1;
		if (base.Level.Value >= MaxLevel())
		{
			return 0;
		}
		return GetUpgradeConfigure(level)?.Exp ?? 0;
	}

	private void OnExpChange(int value)
	{
		int num = MaxLevel();
		if (base.Level.Value >= num)
		{
			base.Exp.JustSetValue(0);
			Debug.Log($"#升级测试# {base.Level.Value} 已达最高等级 {num}，无法继续升级");
			return;
		}
		int upgradeExp = GetUpgradeExp();
		if (upgradeExp == 0)
		{
			return;
		}
		int num2 = value;
		while (num2 >= upgradeExp)
		{
			num2 -= upgradeExp;
			base.Level.Value++;
			base.Exp.JustSetValue(0);
			Upgrade().Forget();
			if (base.Level.Value >= num)
			{
				break;
			}
			upgradeExp = GetUpgradeExp();
			if (upgradeExp == 0)
			{
				break;
			}
		}
		base.Exp.JustSetValue((base.Level.Value < num) ? num2 : 0);
		Game.GetModel<GlobalSignal>().CardGainExp.Dispatch(UID);
	}

	public int ConvertToExp()
	{
		int num = 0;
		foreach (SinglePlayerCardUpgradeConfigureItem singlePlayerCardUpgradeConfigureItem in GetUpgradeConfigure().SinglePlayerCardUpgradeConfigureItems)
		{
			if (base.Level.Value >= singlePlayerCardUpgradeConfigureItem.Star)
			{
				num += singlePlayerCardUpgradeConfigureItem.Exp;
			}
		}
		return num + base.Exp.Value;
	}

	private static SinglePlayerCardUpgradeConfigure GetUpgradeConfigure()
	{
		return StaticConfigure.SinglePlayer.CardUpgradeDict[1];
	}

	public SinglePlayerCardUpgradeConfigureItem GetUpgradeConfigure(int level)
	{
		foreach (SinglePlayerCardUpgradeConfigureItem singlePlayerCardUpgradeConfigureItem in GetUpgradeConfigure().SinglePlayerCardUpgradeConfigureItems)
		{
			if (singlePlayerCardUpgradeConfigureItem.Star == level)
			{
				return singlePlayerCardUpgradeConfigureItem;
			}
		}
		return null;
	}
}
