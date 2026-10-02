using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIGuild_Com_GuildStoreView : GComponent
{
	private readonly List<BaseGoodsData> _goods = new List<BaseGoodsData>();

	private bool _initialized;

	private bool _isShowing;

	public GLoader Character;

	public UIGuild_Com_ItemBg bg;

	public GList list_Store_Goods;

	public const string URL = "ui://w5bj58pzhtd11m";

	public void Init()
	{
		if (!_initialized)
		{
			if (list_Store_Goods != null)
			{
				list_Store_Goods.itemRenderer = RenderGoods;
			}
			_initialized = true;
		}
	}

	public void OnShow()
	{
		_isShowing = true;
		RefreshGoods();
	}

	public void OnHide()
	{
		_isShowing = false;
	}

	public void AddEvent()
	{
	}

	public void RemoveEvent()
	{
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store?.signal.refreshCurShelf.AddListener(OnShelfChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.bag?.signal.bagMapChanged.AddListener(OnBagChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.store?.signal.refreshCurShelf.RemoveListener(OnShelfChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.bag?.signal.bagMapChanged.RemoveListener(OnBagChanged);
	}

	public void ClearData()
	{
		_isShowing = false;
		_goods.Clear();
	}

	private void RefreshGoods()
	{
		_goods.Clear();
		List<BaseGoodsData> list = SimpleSingletonProvider<GameLogicManager>.inst.store?.GetGoodsByShopType(ShopTabType.GuildStore);
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					_goods.Add(list[i]);
				}
			}
		}
		_goods.Sort(CompareGoods);
		if (list_Store_Goods != null)
		{
			list_Store_Goods.numItems = _goods.Count;
		}
	}

	private void RenderGoods(int index, GObject item)
	{
		if (item is UIButton_GoodsItem uIButton_GoodsItem && index >= 0 && index < _goods.Count)
		{
			uIButton_GoodsItem.InitData(44, _goods[index]);
		}
	}

	private static int CompareGoods(BaseGoodsData left, BaseGoodsData right)
	{
		int num = ((left.SellOut() || left.IsOwn()) ? 1 : 0);
		int value = ((right.SellOut() || right.IsOwn()) ? 1 : 0);
		int num2 = num.CompareTo(value);
		if (num2 != 0)
		{
			return num2;
		}
		int num3 = left.goodsOrder.CompareTo(right.goodsOrder);
		if (num3 == 0)
		{
			return left.goodsId.CompareTo(right.goodsId);
		}
		return num3;
	}

	private void OnShelfChanged(int shopType)
	{
		if (_isShowing && (shopType == 0 || shopType == 44))
		{
			RefreshGoods();
		}
	}

	private void OnBagChanged()
	{
		if (_isShowing)
		{
			RefreshGoods();
		}
	}

	public static UIGuild_Com_GuildStoreView CreateInstance()
	{
		return (UIGuild_Com_GuildStoreView)UIPackage.CreateObject("Guild", "Guild_Com_GuildStoreView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Character = (GLoader)GetChildAt(3);
		bg = (UIGuild_Com_ItemBg)GetChildAt(4);
		list_Store_Goods = (GList)GetChildAt(5);
	}
}
