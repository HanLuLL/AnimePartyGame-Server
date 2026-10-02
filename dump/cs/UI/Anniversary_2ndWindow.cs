using System;
using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class Anniversary_2ndWindow : BaseWindow
{
	private SkinSellData _skinSellData;

	private readonly List<UIAnniversary_2nd_Com_SkinItem> skinItemList = new List<UIAnniversary_2nd_Com_SkinItem>();

	public Anniversary_2ndWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIAnniversary_2ndWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow)
		{
			uIAnniversary_2ndWindow.com_Main_1.btn_buy.onClick.Add(RefreshSkinSellDetail);
			uIAnniversary_2ndWindow.com_Main_1.btn_Close.onClick.Add(base.Hide);
			uIAnniversary_2ndWindow.com_Goods.btn_ReturnMain.onClick.Add(RefreshMainInfo);
			uIAnniversary_2ndWindow.com_Goods.com_FirstGoods.InitComponent();
			uIAnniversary_2ndWindow.com_Goods.com_SecondGoods.InitComponent();
			uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods.InitComponent();
			uIAnniversary_2ndWindow.com_Goods.com_FirstGoods.AddEvent();
			uIAnniversary_2ndWindow.com_Goods.com_SecondGoods.AddEvent();
			uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods.AddEvent();
			SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshPurchaseInfo);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (!(base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow))
		{
			return;
		}
		uIAnniversary_2ndWindow.com_Main_1.btn_buy.onClick.Remove(RefreshSkinSellDetail);
		uIAnniversary_2ndWindow.com_Main_1.btn_Close.onClick.Remove(base.Hide);
		uIAnniversary_2ndWindow.com_Goods.btn_ReturnMain.onClick.Remove(RefreshMainInfo);
		uIAnniversary_2ndWindow.com_Goods.com_FirstGoods.RemoveEvent();
		uIAnniversary_2ndWindow.com_Goods.com_SecondGoods.RemoveEvent();
		uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods.RemoveEvent();
		uIAnniversary_2ndWindow.com_Goods.com_FirstGoods.Close();
		uIAnniversary_2ndWindow.com_Goods.com_SecondGoods.Close();
		uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods.Close();
		foreach (UIAnniversary_2nd_Com_SkinItem skinItem in skinItemList)
		{
			SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(skinItem.graph_Skin);
		}
		skinItemList.Clear();
		IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
		if (!(currentPanel is FashionPanel fashionPanel))
		{
			if (currentPanel is HeroPanel heroPanel)
			{
				heroPanel.Show();
			}
		}
		else
		{
			fashionPanel.Show();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshPurchaseInfo);
	}

	public async UniTask ShowSkinSell(SkinSellData skinSellData)
	{
		_skinSellData = skinSellData;
		await TryShowAsync();
		RefreshMainInfo();
	}

	private void RefreshMainInfo()
	{
		if (base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow)
		{
			uIAnniversary_2ndWindow.bg.Background(_skinSellData.skinSellConfig.Background);
			uIAnniversary_2ndWindow.tab.selectedIndex = 0;
			RefreshMainInfo_1();
		}
	}

	private void RefreshMainInfo_1()
	{
		if (base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow)
		{
			uIAnniversary_2ndWindow.com_Main_1.Cut_in.Play();
			uIAnniversary_2ndWindow.com_Main_1.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
			RefreshMainSkinInfo(uIAnniversary_2ndWindow.com_Main_1.loader_Skin1, uIAnniversary_2ndWindow.com_Main_1.btn_ShowSkin1, 1);
			RefreshMainSkinInfo(uIAnniversary_2ndWindow.com_Main_1.loader_Skin2, uIAnniversary_2ndWindow.com_Main_1.btn_ShowSkin2, 2);
		}
	}

	private void RefreshMainSkinInfo(GLoader loader, GButton btn_ShowSkin, int index)
	{
		SkinSellInfoConfigureItem skinChestByIndex = _skinSellData.GetSkinChestByIndex(index);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.TryGetSkinInfoFromRandomChest(skinChestByIndex.Id, out var skinItemInfo, out var labelItemInfo);
		if (skinItemInfo != null && labelItemInfo != null)
		{
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinItemInfo.Id);
			loader.url = configStandingPainting.GetCharacter().Item1;
			btn_ShowSkin.onClick.Set((EventCallback0)delegate
			{
				base.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(skinItemInfo.Id, labelItemInfo.Id).Forget();
				base.onClick.Release();
			});
		}
	}

	private List<SkinSellData> PreparePackageData()
	{
		List<SkinSellData> list = new List<SkinSellData> { _skinSellData };
		List<SkinSellInfoConfigure> skinSellChestMore = _skinSellData.GetSkinSellChestMore();
		for (int i = 0; i < skinSellChestMore.Count; i++)
		{
			SkinSellInfoConfigure skinSellInfoConfigure = skinSellChestMore[i];
			SkinSellData skinComboInfoById = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfoById(skinSellInfoConfigure.Id);
			list.Add(skinComboInfoById);
		}
		return list;
	}

	private void RefreshSkinSellDetail()
	{
		if (base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow)
		{
			uIAnniversary_2ndWindow.com_Main_1.btn_buy.onClick.Retain();
			uIAnniversary_2ndWindow.tab.selectedIndex = 1;
			skinItemList.Clear();
			List<SkinSellData> list = PreparePackageData();
			RefreshPackageItem(uIAnniversary_2ndWindow.com_Goods.com_FirstGoods, list.GetSafeByIndex(0));
			RefreshPackageItem(uIAnniversary_2ndWindow.com_Goods.com_SecondGoods, list.GetSafeByIndex(1));
			RefreshPackageItem(uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods, list.GetSafeByIndex(2));
			uIAnniversary_2ndWindow.com_Main_1.btn_buy.onClick.Release();
		}
	}

	private void RefreshPackageItem(UIAnniversary_2nd_Com_Item com_PackageItem, SkinSellData subSellData)
	{
		com_PackageItem.Cut_in.Play();
		com_PackageItem.SkinSellData = subSellData;
		RefreshPriceButton(com_PackageItem);
		RenderSkinItems(com_PackageItem);
		RenderRewardItems(com_PackageItem);
		com_PackageItem.RefreshLabels();
		com_PackageItem.btn_Purchase.onClick.Set((EventCallback0)delegate
		{
			PurchaseGoods(com_PackageItem, subSellData);
		});
	}

	private void RenderSkinItems(UIAnniversary_2nd_Com_Item comItem)
	{
		comItem.list_Hero?.RemoveChildrenToPool();
		foreach (SkinSellInfoConfigureItem skinSellInfoConfigureItem in comItem.SkinSellData.skinSellConfig.SkinSellInfoConfigureItems)
		{
			if (skinSellInfoConfigureItem.ItemID != 0)
			{
				SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinSellInfoConfigureItem.ItemID);
				if (configStandingPainting != null)
				{
					InitSkinItem(comItem, skinSellInfoConfigureItem, configStandingPainting);
				}
			}
		}
	}

	private void RenderRewardItems(UIAnniversary_2nd_Com_Item comItem)
	{
		comItem.list_Goods?.RemoveChildrenToPool();
		List<SkinSellData.RewardItemData> rewardItems = comItem.SkinSellData.GetRewardItems();
		RenderRewardItemsUI(comItem, rewardItems);
	}

	private void RefreshPriceButton(UIAnniversary_2nd_Com_Item btn_Item)
	{
		btn_Item.btn_Purchase.title = 2024117.GetLocal(UIStringType.Collaboration);
		int num = 0;
		int num2 = 0;
		foreach (SkinSellInfoConfigureItem skinSellInfoConfigureItem in btn_Item.SkinSellData.skinSellConfig.SkinSellInfoConfigureItems)
		{
			if (!_skinSellData.IsOwnGoods(skinSellInfoConfigureItem.ItemID))
			{
				num += skinSellInfoConfigureItem.OriginalPrice;
				num2 += skinSellInfoConfigureItem.DiscountPrice;
			}
		}
		if (num == 0 || num2 == 0)
		{
			btn_Item.btn_Purchase.grayed = true;
			btn_Item.btn_Purchase.status.selectedIndex = 1;
			return;
		}
		btn_Item.btn_Purchase.grayed = false;
		btn_Item.btn_Purchase.status.selectedIndex = 0;
		string arg = GameSettings.SPECIAL_ITEM_STARDISC_PAY.GetItemInfoConfigure().Icon;
		btn_Item.btn_Purchase.txt_DisscountPrice.text = $"<img src='{arg}' width='45' height='45'/> {num2}";
		btn_Item.btn_Purchase.txt_OriginalPrice.text = $"<img src='{arg}' width='22' height='22'/> {num}";
		btn_Item.btn_Purchase.hasDiscount.selectedIndex = ((!num.Equals(num2)) ? 1 : 0);
		double num3 = 100.0 - Math.Round((double)num2 * 100.0 / (double)num);
		btn_Item.btn_Purchase.txt_Discount.text = $"{num3:f0}% OFF";
	}

	private async void PurchaseGoods(UIAnniversary_2nd_Com_Item comItem, SkinSellData subSellData)
	{
		if (!subSellData.HasPurchaseCombo())
		{
			comItem.btn_Purchase.onClick.Retain();
			if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType(subSellData.shopTabType) != GoodsPurchaseType.Recharge)
			{
				await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(subSellData);
			}
			else
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(subSellData, 1);
			}
			comItem.btn_Purchase.onClick.Release();
		}
	}

	private void RefreshPurchaseInfo()
	{
		if (!(base.contentPane is UIAnniversary_2ndWindow uIAnniversary_2ndWindow))
		{
			return;
		}
		RefreshPurchaseItemInfo(uIAnniversary_2ndWindow.com_Goods.com_FirstGoods);
		RefreshPurchaseItemInfo(uIAnniversary_2ndWindow.com_Goods.com_SecondGoods);
		RefreshPurchaseItemInfo(uIAnniversary_2ndWindow.com_Goods.com_ThirdGoods);
		for (int i = 0; i < skinItemList.Count; i++)
		{
			if (skinItemList[i].data is int itemId && SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemId))
			{
				FinishSkinItemStatus(skinItemList[i]);
			}
		}
	}

	private void RefreshPurchaseItemInfo(UIAnniversary_2nd_Com_Item comItem)
	{
		if (comItem?.packageItem != null)
		{
			bool flag = !comItem.SkinSellData.HasPurchaseCombo();
			comItem.btn_Purchase.grayed = flag;
			comItem.btn_Purchase.status.selectedIndex = (flag ? 1 : 0);
			RefreshPriceButton(comItem);
			comItem.RefreshLabels();
		}
	}

	private void FinishSkinItemStatus(UIAnniversary_2nd_Com_SkinItem skinItem)
	{
		if (skinItem.graph_Skin.displayObject is GoWrapper goWrapper && goWrapper.wrapTarget != null && goWrapper.wrapTarget.TryGetComponent<CharacterAnimator>(out var component))
		{
			component.grayed = true;
		}
		skinItem.txt_Get.visible = true;
	}

	private async UniTask InitSkinItem(UIAnniversary_2nd_Com_Item comItem, SkinSellInfoConfigureItem sellInfo, SkinStandingPaintingConfigureItem skinStanding)
	{
		GObject gObject = comItem.list_Hero?.AddItemFromPool();
		if (gObject is UIAnniversary_2nd_Com_SkinItem skinItem)
		{
			ItemInfoConfigure itemInfoConfigure = sellInfo.ItemID.GetItemInfoConfigure();
			skinItem.txt_Title.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
			skinItem.txt_Get.text = 1085.GetLocal(UIStringType.Message);
			skinItem.data = sellInfo.ItemID;
			CharacterAnimator characterAnimator = await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(skinStanding, "Walk", skinItem.graph_Skin, 10f);
			skinItem.visible = true;
			skinItem.data = sellInfo.ItemID;
			skinItemList.Add(skinItem);
			if (_skinSellData.IsOwnGoods(sellInfo.ItemID))
			{
				characterAnimator.grayed = true;
				skinItem.txt_Get.visible = true;
			}
			else
			{
				characterAnimator.grayed = false;
				skinItem.txt_Get.visible = false;
			}
		}
	}

	private void RenderRewardItemsUI(UIAnniversary_2nd_Com_Item comItem, List<SkinSellData.RewardItemData> rewardItems)
	{
		foreach (SkinSellData.RewardItemData rewardData in rewardItems)
		{
			GObject gObject = comItem.list_Goods?.AddItemFromPool();
			UIAnniversary_2nd_Com_CommonItem litItem = gObject as UIAnniversary_2nd_Com_CommonItem;
			if (litItem != null)
			{
				litItem.loader_Icon.url = rewardData.IconUrl;
				GTextField txt_title = litItem.txt_title;
				object obj;
				if (rewardData.Count <= 0)
				{
					obj = "";
				}
				else
				{
					int count = rewardData.Count;
					obj = count.ToString();
				}
				txt_title.text = (string)obj;
				litItem.onClick.Set((EventCallback0)delegate
				{
					litItem.onClick.Retain();
					SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(rewardData.ItemID, rewardData.Count, _Usable: false).Forget();
					litItem.onClick.Release();
				});
			}
		}
	}
}
