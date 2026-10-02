using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class NGOStoreWindow : BaseWindow
{
	private bool initHero;

	private Action CloseCallBack;

	private bool isHero;

	private CollaborationInfoConfigure InfoConfig;

	public async UniTask ShowNGOHero(CollaborationInfoConfigure infoConfig, Action _CloseCallBack = null)
	{
		CloseCallBack = _CloseCallBack;
		InfoConfig = infoConfig;
		await TryShow();
		GComponent gComponent = base.contentPane;
		UINGOStoreWindow win = gComponent as UINGOStoreWindow;
		if (win == null)
		{
			return;
		}
		CommonUIManager.TryAddVideoGraph(UIType.Window, (int)base.config.WindowType, win.loader_BG);
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(400.GetVideoKey(), win.loader_BG);
		win.page.selectedIndex = 1;
		if (!initHero)
		{
			initHero = true;
			win.com_Advert.Cut_in.Play();
			if (int.TryParse(GameSettings.GetDataForLanguage("1", "2", "0", "0"), out var result))
			{
				win.com_Advert.titleType.selectedIndex = result;
			}
			win.com_Advert.loader_Icon.url = GameSettings.GetDataForLanguage("UT_NGO_Title_EN", "UT_NGO_Title_JP", "UT_NGO_Title_CN", "UT_NGO_Title_EN");
			RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[0], win.com_Advert.loader_AngelChan, win.com_Advert.loader_AngelChan_Color, win.com_Advert.com_AngelChan, win.com_Advert.btn_Preview2, IsTangTang: false);
			RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[1], win.com_Advert.loader_TangTang, win.com_Advert.loader_TangTang_Color, win.com_Advert.com_TangTang, win.com_Advert.btn_Preview);
			RefreshSkinAdvertInfo(InfoConfig.PreviewIndex[4], win.com_Advert.loader_AngelChanSkin, win.com_Advert.loader_AngelChanSkin_Color, win.com_Advert.com_AngelChanSkin, win.com_Advert.btn_Preview4, IsTangTang: false);
			RefreshSkinAdvertInfo(InfoConfig.PreviewIndex[5], win.com_Advert.loader_TangTangSkin, win.com_Advert.loader_TangTangSkin_Color, win.com_Advert.com_TangTangSkin, win.com_Advert.btn_Preview3);
			win.com_Advert.btn_GoSkinStore.Txt_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, InfoConfig.EndTime.ToDateTime());
			win.com_Advert.btn_GoStore.Txt_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, InfoConfig.EndTime.ToDateTime());
			win.com_Advert.btn_GoStore.onClick.Set((EventCallback0)delegate
			{
				GOStore(win.com_Advert.btn_GoStore);
			});
			win.com_Advert.btn_GoSkinStore.onClick.Set((EventCallback0)delegate
			{
				GOSkinStore(win.com_Advert.btn_GoSkinStore);
			});
		}
	}

	private async void GOStore(GButton button)
	{
		button.onClick.Retain();
		await ShowNGOStore(InfoConfig.Id, IsHero: true);
		button.onClick.Release();
	}

	private async void GOSkinStore(GButton button)
	{
		button.onClick.Retain();
		await ShowNGOStore(InfoConfig.Id, IsHero: false);
		button.onClick.Release();
	}

	private void RefreshHeroAdvertInfo(int index, GLoader loaderHero, GLoader loaderColor, UINGOStore_Com_CharacterInfo comInfo, GButton previewButton, bool IsTangTang = true)
	{
		(int heroId, RepeatedField<int> photoId, RepeatedField<int> labelId) tuple = RefreshAdvertInfo(index);
		int item = tuple.heroId;
		RepeatedField<int> item2 = tuple.photoId;
		RepeatedField<int> item3 = tuple.labelId;
		SkinStandingPaintingConfigureItem heroConfig = GetHeroConfig(item);
		ItemInfoConfigure itemInfoConfigure = heroConfig.ItemID.GetItemInfoConfigure();
		RefreshAdvertLoaders(loaderHero, loaderColor, heroConfig.GetCharacter().Item1, IsTangTang);
		RefreshHeroTexts(comInfo, item);
		BindPreviewButton(previewButton, itemInfoConfigure.Id, item2[0], item3[0]);
	}

	private void RefreshSkinAdvertInfo(int index, GLoader loaderHero, GLoader loaderColor, UINGOStore_Com_CharacterInfo comInfo, GButton previewButton, bool IsTangTang = true)
	{
		var (num, repeatedField, repeatedField2) = RefreshAdvertInfo(index);
		if (num != 0 && repeatedField != null && repeatedField2 != null)
		{
			SkinStandingPaintingConfigureItem skinConfig = GetSkinConfig(num);
			ItemInfoConfigure itemInfoConfigure = skinConfig.ItemID.GetItemInfoConfigure();
			RefreshAdvertLoaders(loaderHero, loaderColor, skinConfig.GetCharacter().Item1, IsTangTang);
			RefreshSkinTexts(comInfo, itemInfoConfigure);
			BindPreviewButton(previewButton, itemInfoConfigure.Id, repeatedField[0], repeatedField2[0]);
		}
	}

	private (int heroId, RepeatedField<int> photoId, RepeatedField<int> labelId) RefreshAdvertInfo(int index)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = index.GetCollaborationGoodsConfigure();
		if (collaborationGoodsConfigure == null)
		{
			return (heroId: 0, photoId: null, labelId: null);
		}
		int item = collaborationGoodsConfigure.HeroID[0];
		RepeatedField<int> playerPhotoID = collaborationGoodsConfigure.PlayerPhotoID;
		RepeatedField<int> accountBackgroundID = collaborationGoodsConfigure.AccountBackgroundID;
		return (heroId: item, photoId: playerPhotoID, labelId: accountBackgroundID);
	}

	private SkinStandingPaintingConfigureItem GetSkinConfig(int heroId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroId);
	}

	private SkinStandingPaintingConfigureItem GetHeroConfig(int heroId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroId, 0, 0);
	}

	private async void RefreshAdvertLoaders(GLoader loaderHero, GLoader loaderColor, string _URL, bool IsTangTang)
	{
		Color32 Color = (IsTangTang ? new Color32(251, 66, byte.MaxValue, byte.MaxValue) : new Color32(43, 165, byte.MaxValue, byte.MaxValue));
		await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(_URL, delegate(NTexture texture)
		{
			loaderHero.texture = texture;
		}, null);
		if (loaderHero.texture != null)
		{
			loaderColor.texture = await ShadowTextureCreator.CreateShadowTexture(loaderHero.texture, Color, 1f, 0f, 0);
		}
	}

	private static void RefreshSkinTexts(UINGOStore_Com_CharacterInfo comInfo, ItemInfoConfigure skinItemInfo)
	{
		string local = skinItemInfo.NameID.GetLocal(UIStringType.Item);
		if (local != null)
		{
			string[] array = local.Split("-");
			if (array.Length >= 2)
			{
				comInfo.txt_Name.text = array[0];
				comInfo.txt_Nick.text = array[1];
			}
		}
	}

	private void RefreshHeroTexts(UINGOStore_Com_CharacterInfo comInfo, int heroId)
	{
		CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(heroId);
		comInfo.txt_Name.text = heroCharacterConfigure.NameID.GetLocal(UIStringType.Character);
		comInfo.txt_Nick.text = heroCharacterConfigure.NickID.GetLocal(UIStringType.Character);
	}

	private void BindPreviewButton(GButton previewButton, int skinId, int photoId, int labelId)
	{
		previewButton.onClick.Set((EventCallback0)delegate
		{
			PreviewSkin(previewButton, skinId, photoId, labelId);
		});
	}

	private async void PreviewSkin(GButton btnPreview, int SkinId, int photoId, int labelId)
	{
		btnPreview.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(SkinId, labelId, photoId);
		btnPreview.onClick.Release();
	}

	public async UniTask ShowNGOStore(int collaborateId, bool IsHero)
	{
		InfoConfig = collaborateId.GetCollaborationInfoConfigure();
		await TryShow();
		GComponent gComponent = base.contentPane;
		if (gComponent is UINGOStoreWindow win)
		{
			CommonUIManager.TryAddVideoGraph(UIType.Window, (int)base.config.WindowType, win.loader_BG);
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(400.GetVideoKey(), win.loader_BG);
			win.page.selectedIndex = 2;
			isHero = IsHero;
			if (isHero)
			{
				RefreshGoods(win.com_Goods.com_AngelChan, InfoConfig.PreviewIndex[0]);
				RefreshGoods(win.com_Goods.com_TangTang, InfoConfig.PreviewIndex[1]);
				RefreshGoods(win.com_Goods.com_Pack, InfoConfig.PreviewIndex[2]);
			}
			else
			{
				RefreshGoods(win.com_Goods.com_AngelChan, InfoConfig.PreviewIndex[4]);
				RefreshGoods(win.com_Goods.com_TangTang, InfoConfig.PreviewIndex[5]);
				RefreshGoods(win.com_Goods.com_Pack, InfoConfig.PreviewIndex[3]);
			}
			RefreshPurchaseButtons();
		}
	}

	private void RefreshGoods(UINGOStore_Com_GoodsItem com_Goods, int GoodsId)
	{
		CollaborationGoodsConfigure collaborationGoods = GoodsId.GetCollaborationGoodsConfigure();
		if (collaborationGoods == null)
		{
			return;
		}
		if (com_Goods.type.selectedIndex == 0 && collaborationGoods.HeroID.Count > 0)
		{
			SkinStandingPaintingConfigureItem standingPainting = (isHero ? SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0], 0, 0) : SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0]));
			RefreshHeroAnimation(standingPainting, com_Goods.graph_Skin);
		}
		else if (com_Goods.type.selectedIndex == 1 && collaborationGoods.HeroID.Count > 1)
		{
			SkinStandingPaintingConfigureItem standingPainting2 = (isHero ? SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0], 0, 0) : SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0]));
			RefreshHeroAnimation(standingPainting2, com_Goods.graph_FirstTheme);
			SkinStandingPaintingConfigureItem standingPainting3 = (isHero ? SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[1], 0, 0) : SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[1]));
			RefreshHeroAnimation(standingPainting3, com_Goods.graph_SecondTheme);
		}
		com_Goods.CollaborationGoods = collaborationGoods;
		com_Goods.RefreshLabels();
		if (collaborationGoods.OtherRewards.Count > 0)
		{
			com_Goods.list_Prop.itemRenderer = delegate(int index, GObject item)
			{
				KeyValuePair<int, int> keyValuePair = collaborationGoods.OtherRewards.ElementAt(index);
				CommonUIManager.RendererLitItem((UICom_LitItem)item, keyValuePair.Key, keyValuePair.Value);
			};
			com_Goods.list_Prop.numItems = collaborationGoods.OtherRewards.Count;
		}
		else
		{
			com_Goods.list_Prop.numItems = 0;
		}
		com_Goods.txt_RoleTitle.text = collaborationGoods.HeroName.GetLocal(UIStringType.Collaboration);
	}

	private void RefreshShelf(int shelf)
	{
		RefreshPurchaseButtons();
	}

	private void RefreshPurchaseButtons()
	{
		if (base.contentPane is UINGOStoreWindow uINGOStoreWindow)
		{
			CollaborationGoodsConfigure goodsData = GetGoodsData(isHero ? 1 : 10);
			RefreshPurchaseButton(uINGOStoreWindow.com_Goods.com_AngelChan.btn_Purchase, goodsData);
			CollaborationGoodsConfigure goodsData2 = GetGoodsData(isHero ? 2 : 11);
			RefreshPurchaseButton(uINGOStoreWindow.com_Goods.com_TangTang.btn_Purchase, goodsData2);
			CollaborationGoodsConfigure goodsData3 = GetGoodsData(isHero ? 3 : 9);
			RefreshPurchaseButton(uINGOStoreWindow.com_Goods.com_Pack.btn_Purchase, goodsData3);
		}
	}

	private CollaborationGoodsConfigure GetGoodsData(int index)
	{
		return index.GetCollaborationGoodsConfigure();
	}

	private void RefreshPurchaseButton(UINGOStore_Button_Purchase _button, CollaborationGoodsConfigure goodsConfigure)
	{
		RechargeGoods goodsData = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(27, goodsConfigure.GoodsId);
		_button.txt_Topic.text = goodsData.goodsConfig.Name.GetLocal(UIStringType.RechargeStore);
		_button.txt_Price.text = goodsData.GetDiscountPriceText();
		_button.txt_Price.AddCurrencySymbols(_button.txt_Price.text);
		string cutDown = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.GetCutDown(goodsData.goodsConfig.EndTime);
		_button.txt_Countdown.text = cutDown;
		_button.touchable = !goodsData.SellOut();
		_button.grayed = !_button.touchable;
		_button.onClick.Set((EventCallback0)delegate
		{
			if (!_button.grayed)
			{
				OnRequestPurchase(_button, goodsConfigure, goodsData);
			}
		});
	}

	private async void OnRequestPurchase(GButton _button, CollaborationGoodsConfigure goodsConfigure, RechargeGoods _Goods)
	{
		_button.onClick.Retain();
		List<string> list = (isHero ? GetOwnedHeroNames(goodsConfigure.HeroID) : GetOwnedSkinNames(goodsConfigure.HeroID));
		if (list.Count <= 0)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_Goods, 1);
		}
		else
		{
			string arg = string.Join("、", list);
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(2800001.GetLocal(UIStringType.Collaboration), arg), delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_Goods, 1).Forget();
			});
		}
		_button.onClick.Release();
	}

	private List<string> GetOwnedHeroNames(RepeatedField<int> heroIds)
	{
		List<string> list = new List<string>();
		List<HeroCardData> cardsData = GetCardsData(heroIds);
		for (int i = 0; i < cardsData.Count; i++)
		{
			if (cardsData[i].IsHas)
			{
				CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(heroIds[i]);
				list.Add(heroCharacterConfigure.NameID.GetLocal(UIStringType.Character));
			}
		}
		return list;
	}

	private List<string> GetOwnedSkinNames(RepeatedField<int> itemIds)
	{
		List<string> list = new List<string>();
		foreach (int itemId in itemIds)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemId))
			{
				list.Add(itemId.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item));
			}
		}
		return list;
	}

	private List<HeroCardData> GetCardsData(RepeatedField<int> HeroIDs)
	{
		List<HeroCardData> list = new List<HeroCardData>();
		foreach (int HeroID in HeroIDs)
		{
			list.Add(SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(HeroID));
		}
		return list;
	}

	public NGOStoreWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UINGOStoreWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
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
		if (base.contentPane is UINGOStoreWindow uINGOStoreWindow)
		{
			uINGOStoreWindow.btn_Close.onClick.Add(OnReturn);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			uINGOStoreWindow.com_Goods.com_Pack.InitComponent();
			uINGOStoreWindow.com_Goods.com_AngelChan.InitComponent();
			uINGOStoreWindow.com_Goods.com_TangTang.InitComponent();
			uINGOStoreWindow.com_Goods.com_Pack.AddEvent();
			uINGOStoreWindow.com_Goods.com_AngelChan.AddEvent();
			uINGOStoreWindow.com_Goods.com_TangTang.AddEvent();
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshShelf);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UINGOStoreWindow uINGOStoreWindow)
		{
			uINGOStoreWindow.btn_Close.onClick.Remove(OnReturn);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uINGOStoreWindow.com_Goods.com_Pack.RemoveEvent();
			uINGOStoreWindow.com_Goods.com_AngelChan.RemoveEvent();
			uINGOStoreWindow.com_Goods.com_TangTang.RemoveEvent();
			uINGOStoreWindow.com_Goods.com_Pack.Close();
			uINGOStoreWindow.com_Goods.com_AngelChan.Close();
			uINGOStoreWindow.com_Goods.com_TangTang.Close();
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshShelf);
			CloseCallBack?.Invoke();
			initHero = false;
			uINGOStoreWindow.com_Advert.loader_AngelChan_Color.texture?.Dispose();
			uINGOStoreWindow.com_Advert.loader_TangTang_Color.texture?.Dispose();
			uINGOStoreWindow.com_Advert.loader_AngelChanSkin_Color.texture?.Dispose();
			uINGOStoreWindow.com_Advert.loader_TangTangSkin_Color.texture?.Dispose();
			StopAllAnimation();
		}
	}

	private void StopAllAnimation()
	{
		if (base.contentPane is UINGOStoreWindow uINGOStoreWindow)
		{
			StopAnimation(uINGOStoreWindow.com_Goods.com_AngelChan.graph_Skin);
			StopAnimation(uINGOStoreWindow.com_Goods.com_TangTang.graph_Skin);
			StopAnimation(uINGOStoreWindow.com_Goods.com_Pack.graph_FirstTheme);
			StopAnimation(uINGOStoreWindow.com_Goods.com_Pack.graph_SecondTheme);
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UINoticeWindow uINoticeWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			uINoticeWindow.btn_Notice.FireClick(downEffect: true);
			OnReturn();
		}
	}

	private async void OnReturn()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UINGOStoreWindow win)
		{
			win.btn_Close.onClick.Retain();
			if (win.page.selectedIndex == 2)
			{
				await ShowNGOHero(InfoConfig, CloseCallBack);
			}
			else
			{
				Hide();
			}
			win.btn_Close.onClick.Release();
		}
	}

	private async void RefreshHeroAnimation(SkinStandingPaintingConfigureItem standingPainting, GGraph graph)
	{
		if (standingPainting == null)
		{
			graph.visible = false;
			return;
		}
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(standingPainting, "Walk", graph, 10f);
		graph.visible = true;
	}

	private void StopAnimation(GGraph graph)
	{
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(graph);
	}
}
