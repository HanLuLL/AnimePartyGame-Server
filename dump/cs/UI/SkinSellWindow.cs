using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class SkinSellWindow : BaseWindow
{
	private SkinSellData _skinSellData;

	private List<SkinSellInfoConfigureItem> _sellInfos;

	private List<UISkinSell_Com_SkinItem> SkinItemList = new List<UISkinSell_Com_SkinItem>();

	private int _currentLabelIndex;

	public SkinSellWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISkinSellWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			uISkinSellWindow.com_Main_1.btn_ShowDetail.onClick.Add(RefreshSkinSellDetail);
			uISkinSellWindow.com_Main_1.btn_Close.onClick.Add(base.Hide);
			uISkinSellWindow.com_Goods.btn_ReturnMain.onClick.Add(RefreshMainInfo);
			uISkinSellWindow.com_Goods.btn_Purchase.onClick.Add(OpenGoodsDetail);
			uISkinSellWindow.com_Goods.btn_nextPage.onClick.Add(ShowNextPage);
			uISkinSellWindow.com_Goods.btn_prePage.onClick.Add(ShowPrePage);
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshPurchaseInfo);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (!(base.contentPane is UISkinSellWindow uISkinSellWindow))
		{
			return;
		}
		uISkinSellWindow.com_Main_1.btn_ShowDetail.onClick.Remove(RefreshSkinSellDetail);
		uISkinSellWindow.com_Main_1.btn_Close.onClick.Remove(base.Hide);
		uISkinSellWindow.com_Goods.btn_ReturnMain.onClick.Remove(RefreshMainInfo);
		uISkinSellWindow.com_Goods.btn_Purchase.onClick.Remove(OpenGoodsDetail);
		uISkinSellWindow.com_Goods.btn_nextPage.onClick.Remove(ShowNextPage);
		uISkinSellWindow.com_Goods.btn_prePage.onClick.Remove(ShowPrePage);
		foreach (UISkinSell_Com_SkinItem skinItem in SkinItemList)
		{
			SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(skinItem.graph_Skin);
		}
		SkinItemList.Clear();
		uISkinSellWindow.com_Main_1.loader_Skin1Shadow.texture?.Dispose();
		uISkinSellWindow.com_Main_1.loader_Skin2Shadow.texture?.Dispose();
		uISkinSellWindow.com_Main_1.loader_Skin3Shadow.texture?.Dispose();
		uISkinSellWindow.com_Goods.list_Hero.RemoveChildrenToPool();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshPurchaseInfo);
	}

	public async UniTask ShowSkinSell(SkinSellData skinSellData)
	{
		_skinSellData = skinSellData;
		await TryShow();
		RefreshMainInfo();
	}

	private void RefreshMainInfo()
	{
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			uISkinSellWindow.bg.url = _skinSellData.skinSellConfig.Background;
			uISkinSellWindow.tab.selectedIndex = 0;
			RefreshMainInfo_1();
		}
	}

	private void RefreshPriceButton(UISkinSell_Button_Price btn_Price, int originalPrice, int discountPrice)
	{
		if (originalPrice == 0 || discountPrice == 0)
		{
			btn_Price.grayed = true;
			btn_Price.status.selectedIndex = 1;
			return;
		}
		btn_Price.grayed = false;
		btn_Price.status.selectedIndex = 0;
		btn_Price.txt_Title.text = 1007.GetLocal(UIStringType.GUI);
		string arg = GameSettings.SPECIAL_ITEM_STARDISC_PAY.GetItemInfoConfigure().Icon;
		btn_Price.txt_DisscountPrice.text = $"<img src='{arg}' width='45' height='45'/> {discountPrice}";
		btn_Price.txt_OriginalPrice.text = $"<img src='{arg}' width='22' height='22'/> {originalPrice}";
	}

	private void RefreshMainInfo_1()
	{
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			uISkinSellWindow.com_Main_1.HeroCutin.Play();
			uISkinSellWindow.com_Main_1.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
			uISkinSellWindow.com_Main_1.txt_TimeTip.text = RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, _skinSellData.EndTime.ToDateTime());
			_sellInfos = new List<SkinSellInfoConfigureItem>();
			int originalPrice = 0;
			int discountPrice = 0;
			RefreshMainSkinInfo(uISkinSellWindow.com_Main_1.loader_Skin1, uISkinSellWindow.com_Main_1.loader_Skin1Shadow, uISkinSellWindow.com_Main_1.btn_ShowSkin1, 0, ref originalPrice, ref discountPrice);
			RefreshMainSkinInfo(uISkinSellWindow.com_Main_1.loader_Skin2, uISkinSellWindow.com_Main_1.loader_Skin2Shadow, uISkinSellWindow.com_Main_1.btn_ShowSkin2, 1, ref originalPrice, ref discountPrice);
			RefreshMainSkinInfo(uISkinSellWindow.com_Main_1.loader_Skin3, uISkinSellWindow.com_Main_1.loader_Skin3Shadow, uISkinSellWindow.com_Main_1.btn_ShowSkin3, 2, ref originalPrice, ref discountPrice);
			uISkinSellWindow.com_Main_1.btn_ShowDetail.touchable = _sellInfos.Count > 0;
			RefreshPriceButton(uISkinSellWindow.com_Main_1.btn_ShowDetail, originalPrice, discountPrice);
			uISkinSellWindow.com_Main_1.btn_ShowDetail.type.selectedIndex = 0;
			uISkinSellWindow.com_Main_1.txt_Explain.text = 2025071.GetLocal(UIStringType.SkinSell);
		}
	}

	private string RefreshTimeText(int messageId1, int messageId2, DateTime nowTime, DateTime endTime)
	{
		if (nowTime > endTime)
		{
			return "";
		}
		TimeSpan timeSpan = endTime - nowTime;
		if (timeSpan.Days > 0)
		{
			return string.Format(messageId1.GetLocal(UIStringType.Message), "[color=#c7f06c]" + timeSpan.Days.ToString().PadLeft(2, '0') + "[/color]", "[color=#c7f06c]" + (timeSpan.Hours + 1).ToString().PadLeft(2, '0') + "[/color]");
		}
		return string.Format(messageId2.GetLocal(UIStringType.Message), "[color=#c7f06c]" + timeSpan.Hours.ToString().PadLeft(2, '0') + "[/color]", "[color=#c7f06c]" + timeSpan.Minutes.ToString().PadLeft(2, '0') + "[/color]");
	}

	private void RefreshMainSkinInfo(GLoader loader, GLoader loaderShadow, GButton btn_ShowSkin, int index, ref int originalPrice, ref int discountPrice)
	{
		SkinSellInfoConfigureItem skinChestByIndex = _skinSellData.GetSkinChestByIndex(index);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.TryGetSkinInfoFromRandomChest(skinChestByIndex.Id, out var skinItemInfo, out var labelItemInfo);
		if (skinItemInfo != null && labelItemInfo != null)
		{
			creatShadow(loader, loaderShadow, skinItemInfo, index);
			btn_ShowSkin.onClick.Set((EventCallback0)delegate
			{
				base.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(skinItemInfo.Id, labelItemInfo.Id).Forget();
				base.onClick.Release();
			});
			_sellInfos.Add(skinChestByIndex);
			if (!_skinSellData.IsOwnGoods(skinItemInfo.Id))
			{
				originalPrice += skinChestByIndex.OriginalPrice;
				discountPrice += skinChestByIndex.DiscountPrice;
			}
		}
	}

	private async UniTask creatShadow(GLoader loader, GLoader loaderShadow, ItemInfoConfigure skinItemInfo, int index)
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinItemInfo.Id);
		await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(configStandingPainting.GetCharacter().Item1, delegate(NTexture texture)
		{
			loader.texture = texture;
		}, null);
		if (loader.texture != null)
		{
			loaderShadow.texture = await ShadowTextureCreator.CreateShadowTexture(loader.texture, new Color(0.05f, 0.08f, 0.22f, 0.6f));
		}
	}

	private void RefreshSkinSellDetail()
	{
		if (!(base.contentPane is UISkinSellWindow uISkinSellWindow))
		{
			return;
		}
		uISkinSellWindow.tab.selectedIndex = 1;
		int num = 0;
		int num2 = 0;
		GButton btn_nextPage = uISkinSellWindow.com_Goods.btn_nextPage;
		GButton btn_prePage = uISkinSellWindow.com_Goods.btn_prePage;
		List<SkinSellInfoConfigureItem> sellInfos = _sellInfos;
		bool flag = (btn_prePage.visible = sellInfos != null && sellInfos.Count > 1);
		btn_nextPage.visible = flag;
		uISkinSellWindow.com_Goods.txt_Explain.text = 2025071.GetLocal(UIStringType.SkinSell);
		RefreshLabel(0);
		if (_sellInfos == null || _sellInfos.Count == 0)
		{
			return;
		}
		foreach (SkinSellInfoConfigureItem sellInfo in _sellInfos)
		{
			if (!_skinSellData.IsOwnGoods(sellInfo.ItemID))
			{
				num += sellInfo.OriginalPrice;
				num2 += sellInfo.DiscountPrice;
			}
		}
		RefreshPriceButton(uISkinSellWindow.com_Goods.btn_Purchase, num, num2);
		uISkinSellWindow.com_Goods.btn_Purchase.visible = !HasPurchase();
		RefreshHeroItem(0).Forget();
		RefreshHeroItem(1).Forget();
		RefreshHeroItem(2).Forget();
		RefreshHeroItem(3).Forget();
		uISkinSellWindow.com_Goods.Cut_in.Play();
	}

	private async UniTask RefreshHeroItem(int index)
	{
		if (_sellInfos.Count <= index)
		{
			return;
		}
		SkinSellInfoConfigureItem sellInfo = _sellInfos[index];
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(sellInfo.ItemID);
		if (configStandingPainting == null)
		{
			return;
		}
		ItemInfoConfigure itemInfoConfigure = sellInfo.ItemID.GetItemInfoConfigure();
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			if (SkinItemList.Count == index)
			{
				GObject gObject = uISkinSellWindow.com_Goods.list_Hero.AddItemFromPool();
				SkinItemList.Add(gObject as UISkinSell_Com_SkinItem);
			}
			SkinItemList[index].txt_Title.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
			SkinItemList[index].txt_Get.text = 1085.GetLocal(UIStringType.Message);
			SkinItemList[index].data = sellInfo.ItemID;
			CharacterAnimator characterAnimator = await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(configStandingPainting, "Walk", SkinItemList[index].graph_Skin, 10f);
			SkinItemList[index].visible = true;
			if (_skinSellData.IsOwnGoods(sellInfo.ItemID))
			{
				characterAnimator.grayed = true;
				SkinItemList[index].txt_Get.visible = true;
			}
			else
			{
				characterAnimator.grayed = false;
				SkinItemList[index].txt_Get.visible = false;
			}
		}
	}

	private void RefreshLabel(int index)
	{
		if (!(base.contentPane is UISkinSellWindow uISkinSellWindow))
		{
			return;
		}
		_currentLabelIndex = (index + _sellInfos.Count) % _sellInfos.Count;
		SimpleSingletonProvider<GameLogicManager>.inst.bag.TryGetSkinInfoFromRandomChest(_sellInfos[_currentLabelIndex].Id, out var _, out var labelItemInfo);
		if (labelItemInfo != null)
		{
			UICom_PlayerLabel com_Label = (UICom_PlayerLabel)uISkinSellWindow.com_Goods.com_Label;
			string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
			int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
			ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
			(string, bool) playerLabel = labelItemInfo.SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
			CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
			CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, com_Label, playerLabel.Item1, playerLabel.Item2);
			string fashionAccountHeadShot = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
			if (_skinSellData.IsOwnGoods(labelItemInfo.Id))
			{
				uISkinSellWindow.com_Goods.txt_GetLabel.visible = true;
				uISkinSellWindow.com_Goods.graph_LabelGray.visible = true;
			}
			else
			{
				uISkinSellWindow.com_Goods.txt_GetLabel.visible = false;
				uISkinSellWindow.com_Goods.graph_LabelGray.visible = false;
			}
			uISkinSellWindow.com_Goods.txt_GetLabel.text = 1085.GetLocal(UIStringType.Message);
		}
	}

	private void ShowPrePage()
	{
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			uISkinSellWindow.com_Goods.btn_prePage.onClick.Retain();
			uISkinSellWindow.com_Goods.btn_nextPage.onClick.Retain();
			int index = _currentLabelIndex - 1;
			RefreshLabel(index);
			uISkinSellWindow.com_Goods.btn_prePage.onClick.Release();
			uISkinSellWindow.com_Goods.btn_nextPage.onClick.Release();
		}
	}

	private void ShowNextPage()
	{
		if (base.contentPane is UISkinSellWindow uISkinSellWindow)
		{
			uISkinSellWindow.com_Goods.btn_prePage.onClick.Retain();
			uISkinSellWindow.com_Goods.btn_nextPage.onClick.Retain();
			int index = _currentLabelIndex + 1;
			RefreshLabel(index);
			uISkinSellWindow.com_Goods.btn_prePage.onClick.Release();
			uISkinSellWindow.com_Goods.btn_nextPage.onClick.Release();
		}
	}

	private async void OpenGoodsDetail()
	{
		if (HasPurchase())
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (gComponent is UISkinSellWindow win)
		{
			win.com_Goods.btn_Purchase.onClick.Retain();
			if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType(_skinSellData.shopTabType) != GoodsPurchaseType.Recharge)
			{
				await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(_skinSellData);
			}
			else
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_skinSellData, 1);
			}
			win.com_Goods.btn_Purchase.onClick.Release();
		}
	}

	private bool HasPurchase()
	{
		if (_skinSellData.SellOut() || _skinSellData.IsOwn())
		{
			return true;
		}
		for (int i = 0; i < _sellInfos.Count; i++)
		{
			if (!_skinSellData.IsOwnGoods(_sellInfos[i].ItemID))
			{
				return false;
			}
		}
		return true;
	}

	private void RefreshPurchaseInfo(int obj)
	{
		if (!(base.contentPane is UISkinSellWindow uISkinSellWindow) || _skinSellData == null)
		{
			return;
		}
		_skinSellData.UpdatePurchaseRecord();
		if (!HasPurchase())
		{
			return;
		}
		uISkinSellWindow.com_Goods.btn_Purchase.visible = false;
		foreach (UISkinSell_Com_SkinItem skinItem in SkinItemList)
		{
			FinishHeroItemStatus(skinItem);
		}
		uISkinSellWindow.com_Goods.graph_LabelGray.visible = true;
		uISkinSellWindow.com_Goods.txt_GetLabel.visible = true;
	}

	private void FinishHeroItemStatus(UISkinSell_Com_SkinItem comHero)
	{
		if (comHero.graph_Skin.displayObject is GoWrapper goWrapper && goWrapper.wrapTarget != null && goWrapper.wrapTarget.TryGetComponent<CharacterAnimator>(out var component))
		{
			component.grayed = true;
		}
		comHero.txt_Get.visible = true;
	}
}
