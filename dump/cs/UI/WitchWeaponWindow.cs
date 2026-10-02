using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Audio;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class WitchWeaponWindow : BaseWindow
{
	private Action CloseCallBack;

	private CollaborationInfoConfigure InfoConfig;

	private const int BankId = 23;

	private const int EventId = 199;

	public async UniTask ShowWitchWeaponSkin(int _collaborateId, Action _CloseCallBack = null)
	{
		CloseCallBack = _CloseCallBack;
		InfoConfig = _collaborateId.GetCollaborationInfoConfigure();
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIWitchWeaponWindow win = gComponent as UIWitchWeaponWindow;
		if (win != null)
		{
			win.page.selectedIndex = 0;
			RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[0], win.com_Advert.loader_XiuNv, win.com_Advert.com_XiuNv);
			RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[1], win.com_Advert.loader_TaiDao, win.com_Advert.com_TaiDao);
			win.com_Advert.btn_GoStore.onClick.Set((EventCallback0)delegate
			{
				GOStore(win.com_Advert.btn_GoStore);
			});
			win.com_Advert.btn_GoStore.txt_RemainingTime.text = GetCutDown(InfoConfig.EndTime);
			win.com_Advert.Cut_in.Play();
		}
	}

	private async void GOStore(GButton button)
	{
		button.onClick.Retain();
		await ShowWitchStore(InfoConfig.Id);
		button.onClick.Release();
	}

	private void RefreshHeroAdvertInfo(int index, GLoader loader_Hero, UIWitchWeapon_Com_CharacterInfo com_Info)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = index.GetCollaborationGoodsConfigure();
		if (collaborationGoodsConfigure == null)
		{
			return;
		}
		int num = collaborationGoodsConfigure.HeroID[0];
		RepeatedField<int> photoId = collaborationGoodsConfigure.PlayerPhotoID;
		RepeatedField<int> labelId = collaborationGoodsConfigure.AccountBackgroundID;
		SkinStandingPaintingConfigureItem skinConfig = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(num);
		if (skinConfig == null)
		{
			Debug.LogError($"无法找到皮肤{num}的配置！");
			return;
		}
		RefreshHeroAnimation(skinConfig, com_Info.loader_Animation);
		com_Info.btn_Preview.onClick.Set((EventCallback0)delegate
		{
			PreviewSkin(com_Info.btn_Preview, skinConfig.ItemID, photoId[0], labelId[0]);
		});
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

	public async UniTask ShowWitchStore(int collaborateId)
	{
		InfoConfig = collaborateId.GetCollaborationInfoConfigure();
		await TryShow();
		if (base.contentPane is UIWitchWeaponWindow uIWitchWeaponWindow)
		{
			uIWitchWeaponWindow.page.selectedIndex = 1;
			RefreshPurchaseButtons();
			RefreshGoods(uIWitchWeaponWindow.com_Goods.com_AngelChan, InfoConfig.PreviewIndex[0]);
			RefreshGoods(uIWitchWeaponWindow.com_Goods.com_TangTang, InfoConfig.PreviewIndex[1]);
			RefreshGoods(uIWitchWeaponWindow.com_Goods.com_Pack, InfoConfig.PreviewIndex[2]);
			uIWitchWeaponWindow.com_Goods.Cut_in.Play();
		}
	}

	private void RefreshGoods(UIWitchWeapon_Com_GoodsItem com_Goods, int index)
	{
		CollaborationGoodsConfigure collaborationGoods = index.GetCollaborationGoodsConfigure();
		if (collaborationGoods == null)
		{
			return;
		}
		if (com_Goods.type.selectedIndex == 0 && collaborationGoods.HeroID.Count > 0)
		{
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0]);
			RefreshHeroAnimation(configStandingPainting, com_Goods.graph_Skin);
		}
		else if (com_Goods.type.selectedIndex == 1 && collaborationGoods.HeroID.Count > 1)
		{
			SkinStandingPaintingConfigureItem configStandingPainting2 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0]);
			RefreshHeroAnimation(configStandingPainting2, com_Goods.graph_FirstTheme);
			SkinStandingPaintingConfigureItem configStandingPainting3 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[1]);
			RefreshHeroAnimation(configStandingPainting3, com_Goods.graph_SecondTheme);
			com_Goods.txt_Tips.text = 2700051.GetLocal(UIStringType.Collaboration);
		}
		if (com_Goods.type.selectedIndex == 0)
		{
			RepeatedField<int> playerPhotoID = collaborationGoods.PlayerPhotoID;
			RepeatedField<int> accountBackgroundID = collaborationGoods.AccountBackgroundID;
			RefreshPlayerLabel((UICom_PlayerLabel)com_Goods.com_PlayerLabel, com_Goods.txt_LabelTitle, accountBackgroundID[0], playerPhotoID[0], collaborationGoods.PhotoAndBackgroundName);
		}
		if (collaborationGoods.OtherRewards.Count > 0)
		{
			com_Goods.list_Prop.itemRenderer = delegate(int index2, GObject item)
			{
				KeyValuePair<int, int> keyValuePair = collaborationGoods.OtherRewards.ElementAt(index2);
				CommonUIManager.RendererLitItem((UICom_LitItem)item, keyValuePair.Key, keyValuePair.Value);
			};
			com_Goods.list_Prop.numItems = collaborationGoods.OtherRewards.Count;
		}
		com_Goods.txt_RoleTitle.text = collaborationGoods.HeroName.GetLocal(UIStringType.Collaboration);
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
		if (base.contentPane is UIWitchWeaponWindow uIWitchWeaponWindow)
		{
			RefreshPurchaseButton(uIWitchWeaponWindow.com_Goods.com_AngelChan.btn_Purchase, InfoConfig.PreviewIndex[0]);
			RefreshPurchaseButton(uIWitchWeaponWindow.com_Goods.com_TangTang.btn_Purchase, InfoConfig.PreviewIndex[1]);
			RefreshPurchaseButton(uIWitchWeaponWindow.com_Goods.com_Pack.btn_Purchase, InfoConfig.PreviewIndex[2]);
		}
	}

	private void RefreshPurchaseButton(UIWitchWeapon_Button_Purchase _button, int index)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = index.GetCollaborationGoodsConfigure();
		RechargeGoods _Goods = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(27, collaborationGoodsConfigure.GoodsId);
		RepeatedField<int> items = collaborationGoodsConfigure.HeroID;
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
				foreach (int item in items)
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(item))
					{
						SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(2025071.GetLocal(UIStringType.Collaboration), delegate
						{
							OnRequestPurchase(_button, _Goods);
						}).Forget();
						return;
					}
				}
				OnRequestPurchase(_button, _Goods);
			}
		});
	}

	private async void OnRequestPurchase(GButton _button, RechargeGoods _Goods)
	{
		_button.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(_Goods);
		_button.onClick.Release();
	}

	private void Test()
	{
	}

	public WitchWeaponWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIWitchWeaponWindow.CreateInstance();
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
		if (base.contentPane is UIWitchWeaponWindow uIWitchWeaponWindow)
		{
			uIWitchWeaponWindow.btn_Close.onClick.Add(OnReturn);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshShelf);
			LoadAudioBank().Forget();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIWitchWeaponWindow uIWitchWeaponWindow)
		{
			uIWitchWeaponWindow.btn_Close.onClick.Remove(OnReturn);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshShelf);
			CloseCallBack?.Invoke();
			StopAllAnimation();
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.PlayBGM();
			UnloadAudioBank();
		}
	}

	private void StopAllAnimation()
	{
		if (base.contentPane is UIWitchWeaponWindow uIWitchWeaponWindow)
		{
			StopAnimation(uIWitchWeaponWindow.com_Advert.com_XiuNv.loader_Animation);
			StopAnimation(uIWitchWeaponWindow.com_Advert.com_TaiDao.loader_Animation);
			StopAnimation(uIWitchWeaponWindow.com_Goods.com_AngelChan.graph_Skin);
			StopAnimation(uIWitchWeaponWindow.com_Goods.com_TangTang.graph_Skin);
			StopAnimation(uIWitchWeaponWindow.com_Goods.com_Pack.graph_FirstTheme);
			StopAnimation(uIWitchWeaponWindow.com_Goods.com_Pack.graph_SecondTheme);
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
		if (gComponent is UIWitchWeaponWindow win)
		{
			win.btn_Close.onClick.Retain();
			if (win.page.selectedIndex == 1)
			{
				await ShowWitchWeaponSkin(InfoConfig.Id, CloseCallBack);
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
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(standingPainting, "Idle", graph, 10f);
		graph.visible = true;
	}

	private void StopAnimation(GGraph graph)
	{
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(graph);
	}

	private async UniTaskVoid LoadAudioBank()
	{
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(23);
		BGMHelper.TryPlayBGM(199);
	}

	private void UnloadAudioBank()
	{
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(23);
	}
}
