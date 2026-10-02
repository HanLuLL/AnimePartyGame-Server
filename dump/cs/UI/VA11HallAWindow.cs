using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class VA11HallAWindow : BaseWindow
{
	private Action CloseCallBack;

	private GTweener videoFadeTweener;

	private const int BankId = 24;

	private const int EventId = 198;

	private CollaborationInfoConfigure InfoConfig;

	public async UniTask ShowVA11HallASkin(CollaborationInfoConfigure infoConfig, Action _CloseCallBack = null)
	{
		CloseCallBack = _CloseCallBack;
		InfoConfig = infoConfig;
		string videoKey = 403.GetVideoKey();
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(videoKey);
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIVA11HallAWindow win = gComponent as UIVA11HallAWindow;
		if (win != null)
		{
			win.com_Advert.btn_GoActivityStore.title = 2024116.GetLocal(UIStringType.Collaboration);
			win.com_Advert.btn_GoStore.title = 2024117.GetLocal(UIStringType.Collaboration);
			win.com_Advert.txt_GoActivityStore.text = 2024116.GetLocal(UIStringType.Collaboration);
			win.com_Advert.txt_GoStore.text = 2024117.GetLocal(UIStringType.Collaboration);
			win.graph_Video.alpha = 1f;
			win.com_Advert.visible = false;
			win.page.selectedIndex = 0;
			CommonUIManager.TryAddVideoGraph(UIType.Window, 335, win.graph_Video);
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, win.graph_Video, delegate
			{
				win.page.selectedIndex = 1;
			});
			RefreshVA11HallASkin(0.8f);
		}
	}

	private void RefreshVA11HallASkin(float transitionDelayTime)
	{
		GComponent gComponent = base.contentPane;
		UIVA11HallAWindow win = gComponent as UIVA11HallAWindow;
		if (win == null)
		{
			return;
		}
		RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[0], win.com_Advert.loader_303, win.com_Advert.btn_Preview_303);
		RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[1], win.com_Advert.loader_304, win.com_Advert.btn_Preview_304);
		win.com_Advert.btn_GoStore.onClick.Set(GOStore);
		win.com_Advert.btn_GoActivityStore.onClick.Set(GOActivityStore);
		win.com_Advert.btn_TimeTip_303.txt_Title.text = GetCutDown(InfoConfig.EndTime);
		win.com_Advert.btn_TimeTip_304.txt_Title.text = GetCutDown(InfoConfig.EndTime);
		win.com_Advert.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 3);
		win.com_Advert.Cut_In.Play(1, transitionDelayTime, delegate
		{
			win.page.selectedIndex = 1;
			win.com_Advert.visible = true;
			videoFadeTweener = win.graph_Video.TweenFade(0f, 0.3f).OnComplete((GTweenCallback)delegate
			{
				win.graph_Video.visible = false;
			});
		}, delegate
		{
			win.com_Advert.visible = true;
			win.graph_Video.visible = false;
			win.graph_Video.color = Color.clear;
			RefreshHeroAdvertHeroAnimation(InfoConfig.PreviewIndex[0], win.com_Advert.loader_Animation_303);
			RefreshHeroAdvertHeroAnimation(InfoConfig.PreviewIndex[1], win.com_Advert.loader_Animation_304);
		});
	}

	private async void GOStore()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIVA11HallAWindow win)
		{
			win.com_Advert.btn_GoStore.onClick.Retain();
			await ShowVA11HallAStore(InfoConfig.Id);
			win.com_Advert.btn_GoStore.onClick.Release();
		}
	}

	private void GOActivityStore()
	{
		if (base.contentPane is UIVA11HallAWindow uIVA11HallAWindow)
		{
			uIVA11HallAWindow.com_Advert.btn_GoActivityStore.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.GoWayPanel(InfoConfig.Way).Forget();
			Hide();
			uIVA11HallAWindow.com_Advert.btn_GoActivityStore.onClick.Release();
		}
	}

	private void RefreshHeroAdvertInfo(int index, GLoader loader_Character, UIVA11HallA_Button_PreviewSkin btn_Preview)
	{
		CollaborationGoodsConfigure goodsConfigure = index.GetCollaborationGoodsConfigure();
		if (goodsConfigure == null)
		{
			return;
		}
		int num = goodsConfigure.HeroID[0];
		SkinStandingPaintingConfigureItem skinConfig = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(num, 0, 0);
		if (skinConfig == null)
		{
			Debug.LogError($"无法找到皮肤{num}的配置！");
			return;
		}
		loader_Character.url = skinConfig.GetCharacter().Item1;
		btn_Preview.onClick.Set((EventCallback0)delegate
		{
			int photoId = ((goodsConfigure.PlayerPhotoID.Count > 0) ? goodsConfigure.PlayerPhotoID[0] : 0);
			int labelId = ((goodsConfigure.AccountBackgroundID.Count > 0) ? goodsConfigure.AccountBackgroundID[0] : 0);
			PreviewSkin(btn_Preview, skinConfig.ItemID, photoId, labelId);
		});
		btn_Preview.txt_Title.text = 2700064.GetLocal(UIStringType.Collaboration);
	}

	private void RefreshHeroAdvertHeroAnimation(int index, GGraph graph_Animation)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = index.GetCollaborationGoodsConfigure();
		if (collaborationGoodsConfigure != null)
		{
			int heroId = collaborationGoodsConfigure.HeroID[0];
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroId, 0, 0);
			RefreshHeroAnimation(configStandingPainting, graph_Animation);
		}
	}

	private async void PreviewSkin(GButton btnPreview, int SkinId, int photoId, int labelId)
	{
		btnPreview.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(SkinId, labelId, photoId);
		btnPreview.onClick.Release();
	}

	private string GetCutDown(Timestamp EndTime)
	{
		if ((object)EndTime == null)
		{
			return null;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime endTime = EndTime.ToDateTime();
		return TimeHelper.RefreshTimeText(1033, 1034, serverTime, endTime);
	}

	private async UniTask ShowVA11HallAStore(int collaborateId)
	{
		InfoConfig = collaborateId.GetCollaborationInfoConfigure();
		await TryShow();
		if (!(base.contentPane is UIVA11HallAWindow uIVA11HallAWindow))
		{
			return;
		}
		uIVA11HallAWindow.page.selectedIndex = 2;
		RefreshPurchaseButtons();
		CollaborationGoodsConfigure collaborationGoods = InfoConfig.PreviewIndex[1].GetCollaborationGoodsConfigure();
		if (collaborationGoods == null)
		{
			return;
		}
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0], 0, 0);
		RefreshHeroAnimation(configStandingPainting, uIVA11HallAWindow.com_Goods.graph_Skin);
		uIVA11HallAWindow.com_Goods.txt_RoleTitle.text = collaborationGoods.HeroName.GetLocal(UIStringType.Collaboration);
		RefreshPlayerLabel((UICom_PlayerLabel)uIVA11HallAWindow.com_Goods.com_DynamicLabel, uIVA11HallAWindow.com_Goods.txt_DynamicLabelTitle, collaborationGoods.AccountBackgroundID[0], collaborationGoods.PlayerPhotoID[0], collaborationGoods.PhotoAndBackgroundName);
		RefreshPlayerLabel((UICom_PlayerLabel)uIVA11HallAWindow.com_Goods.com_StaticLabel, uIVA11HallAWindow.com_Goods.txt_StaticLabelTitle, collaborationGoods.AccountBackgroundID[1], collaborationGoods.PlayerPhotoID[1], collaborationGoods.PhotoAndBackgroundName);
		if (collaborationGoods.OtherRewards.Count > 0)
		{
			uIVA11HallAWindow.com_Goods.list_Prop.itemRenderer = delegate(int index, GObject item)
			{
				KeyValuePair<int, int> keyValuePair = collaborationGoods.OtherRewards.ElementAt(index);
				CommonUIManager.RendererLitItem((UICom_LitItem)item, keyValuePair.Key, keyValuePair.Value);
			};
			uIVA11HallAWindow.com_Goods.list_Prop.numItems = collaborationGoods.OtherRewards.Count;
		}
	}

	private void RefreshPlayerLabel(UICom_PlayerLabel com_Label, GTextField titleText, int labelItemId, int photoId, int labelName)
	{
		string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
		int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
		titleText.text = labelName.GetLocal(UIStringType.Collaboration);
		(string, bool) playerLabel = labelItemId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
		CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, com_Label, playerLabel.Item1, playerLabel.Item2);
		if (photoId != 0)
		{
			string fashionAccountHeadShot = photoId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
		}
	}

	private void RefreshShelf(int shelf)
	{
		RefreshPurchaseButtons();
	}

	private void RefreshPurchaseButtons()
	{
		if (base.contentPane is UIVA11HallAWindow uIVA11HallAWindow)
		{
			CollaborationGoodsConfigure collaborationGoodsConfigure = InfoConfig.PreviewIndex[1].GetCollaborationGoodsConfigure();
			RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(27, collaborationGoodsConfigure.GoodsId);
			RefreshPurchaseButton(uIVA11HallAWindow.com_Goods.btn_Purchase, rechargeGoodsByShopTypeAndGoodsID);
		}
	}

	private void RefreshPurchaseButton(UIVA11HallA_Button_Purchase _button, RechargeGoods _Goods)
	{
		_button.txt_Topic.text = _Goods.goodsConfig.Name.GetLocal(UIStringType.RechargeStore);
		_button.txt_Price.text = _Goods.GetDiscountPriceText();
		_button.txt_Price.AddCurrencySymbols(_button.txt_Price.text);
		string cutDown = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.GetCutDown(_Goods.goodsConfig.EndTime);
		_button.txt_Countdown.text = cutDown;
		_button.touchable = !_Goods.SellOut();
		_button.grayed = !_button.touchable;
		_button.onClick.Set((EventCallback0)delegate
		{
			if (!_button.grayed)
			{
				OnRequestPurchase(_button, _Goods).Forget();
			}
		});
	}

	private async UniTask OnRequestPurchase(GButton _button, RechargeGoods _Goods)
	{
		_button.onClick.Retain();
		await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_Goods, 1);
		_button.onClick.Release();
	}

	public VA11HallAWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIVA11HallAWindow.CreateInstance();
		base.OnInit();
	}

	public override void Dispose()
	{
		base.Dispose();
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
		if (base.contentPane is UIVA11HallAWindow uIVA11HallAWindow)
		{
			uIVA11HallAWindow.btn_Close.scale = Vector2.one;
			uIVA11HallAWindow.btn_Close.onClick.Add(OnReturn);
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshPurchaseInfo);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIVA11HallAWindow uIVA11HallAWindow)
		{
			uIVA11HallAWindow.btn_Close.onClick.Remove(OnReturn);
			CloseCallBack?.Invoke();
			if (videoFadeTweener != null && !videoFadeTweener._killed)
			{
				videoFadeTweener.Kill();
				videoFadeTweener = null;
			}
			StopAnimation(uIVA11HallAWindow.com_Advert.loader_Animation_303);
			StopAnimation(uIVA11HallAWindow.com_Advert.loader_Animation_304);
			StopAnimation(uIVA11HallAWindow.com_Goods.graph_Skin);
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshPurchaseInfo);
		}
	}

	private void OnReturn()
	{
		if (base.contentPane is UIVA11HallAWindow uIVA11HallAWindow)
		{
			uIVA11HallAWindow.btn_Close.onClick.Retain();
			if (uIVA11HallAWindow.page.selectedIndex == 2)
			{
				RefreshVA11HallASkin(0f);
				uIVA11HallAWindow.page.selectedIndex = 1;
			}
			else
			{
				Hide();
			}
			uIVA11HallAWindow.btn_Close.onClick.Release();
		}
	}

	private async void RefreshHeroAnimation(SkinStandingPaintingConfigureItem standingPainting, GGraph graph)
	{
		if (standingPainting == null)
		{
			graph.visible = false;
			return;
		}
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(standingPainting, "Idle", graph, 10f);
		graph.visible = true;
	}

	private void StopAnimation(GGraph graph)
	{
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(graph);
	}

	private void RefreshPurchaseInfo(int obj)
	{
		if (base.contentPane is UIVA11HallAWindow)
		{
			RefreshPurchaseButtons();
		}
	}
}
