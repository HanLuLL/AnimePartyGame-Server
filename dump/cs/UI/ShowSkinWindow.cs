using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ShowSkinWindow : BaseWindow
{
	private BaseGoodsData goodsData;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	protected override bool isGeneralFadeOut => true;

	public ShowSkinWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIShowSkinWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.Cut_in.Play();
			uIShowSkinWindow.mohu.onClick.Add(base.Hide);
			uIShowSkinWindow.btn_Return.onClick.Add(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			uIShowSkinWindow.com_Preview.btn_Video.AddEvent();
			uIShowSkinWindow.btn_Purchase.onClick.Add(OpenGoodsDetail);
			uIShowSkinWindow.btn_Purchase.onClick.Release();
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshPurchaseButton);
			uIShowSkinWindow.com_Preview.scrollPane.posY = 0f;
			uIShowSkinWindow.btn_Select.onClick.Add(SwitchInfo);
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.mohu.onClick.Remove(base.Hide);
			uIShowSkinWindow.btn_Return.onClick.Remove(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uIShowSkinWindow.com_Preview.btn_Video.Close();
			uIShowSkinWindow.btn_Purchase.onClick.Remove(OpenGoodsDetail);
			SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshPurchaseButton);
			uIShowSkinWindow.btn_Select.onClick.Remove(SwitchInfo);
			blurBgCtrl.OnHide();
		}
	}

	private void SwitchInfo()
	{
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.btn_Select.onClick.Retain();
			int selectedIndex = uIShowSkinWindow.infoType.selectedIndex;
			uIShowSkinWindow.infoType.selectedIndex = ((selectedIndex == 0) ? 1 : 0);
			uIShowSkinWindow.btn_Select.type.selectedIndex = ((selectedIndex == 0) ? 1 : 0);
			uIShowSkinWindow.btn_Select.onClick.Release();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			uIShowSkinWindow.btn_Return.FireClick(downEffect: true);
			Hide();
		}
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async UniTask ShowSkinGoods(BaseGoodsData GoodsData)
	{
		goodsData = GoodsData;
		await TryShowAsync();
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.Type.selectedIndex = 1;
		}
		RefreshPurchaseButton(14);
		ItemInfoConfigure itemConfig = goodsData.itemConfig;
		if (itemConfig != null)
		{
			RepeatedField<int> randomReward = itemConfig.SubMeterID.GetChestInfoConfigure().RandomReward;
			int itemID = randomReward[0].GetChestRandomRewardConfigure().ChestRandomRewardConfigureItems[0].ItemID;
			RefreshSkin(itemID);
			int itemID2 = randomReward[1].GetChestRandomRewardConfigure().ChestRandomRewardConfigureItems[0].ItemID;
			RefreshPlayerLabel(itemID2, 0);
			RefreshDescription(itemID);
		}
	}

	private void RefreshPurchaseButton(int shelf)
	{
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow && goodsData != null)
		{
			goodsData.UpdatePurchaseRecord();
			if (goodsData.IsOwn() || goodsData.SellOut())
			{
				uIShowSkinWindow.btn_Purchase.visible = false;
				return;
			}
			uIShowSkinWindow.btn_Purchase.visible = true;
			uIShowSkinWindow.btn_Purchase.title = goodsData.SalePrice.ToString();
			uIShowSkinWindow.btn_Purchase.loader_TokenSymbol.url = GameSettings.SPECIAL_ITEM_STARDISC_PAY.GetItemInfoConfigure().ShowIcon;
		}
	}

	public async UniTask PreviewSkin(int skinItemId, int labelItemId)
	{
		await TryShowAsync();
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.Type.selectedIndex = ((!(SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel)) ? 1 : 0);
			uIShowSkinWindow.btn_Purchase.visible = false;
			RefreshSkin(skinItemId);
			RefreshPlayerLabel(labelItemId, 0);
			RefreshDescription(skinItemId);
		}
	}

	public async UniTask PreviewSkin(int skinItemId, int labelItemId, int photoId)
	{
		await TryShowAsync();
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.Type.selectedIndex = ((!(SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel)) ? 1 : 0);
			uIShowSkinWindow.btn_Purchase.visible = false;
			RefreshSkin(skinItemId);
			RefreshPlayerLabel(labelItemId, photoId);
			RefreshDescription(skinItemId);
		}
	}

	private void RefreshSkin(int skinItemId)
	{
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinItemId);
			(string, bool) character = configStandingPainting.GetCharacter();
			Vector2 offset = Vector2.zero;
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
			{
				offset = value.skinOffset;
			}
			CommonUIManager.RendererSkin(UIType.Window, (int)base.config.WindowType, (UICom_HeroSkin)uIShowSkinWindow.com_Skin.com_Skin, character.Item1, character.Item2, offset, Vector2.one);
			CommonUIManager.TryAddVideoGraph(UIType.Window, 302, uIShowSkinWindow.com_Preview.btn_Video.loader_Video);
			uIShowSkinWindow.com_Preview.btn_Video.RefreshVideo(configStandingPainting.PreviewVideo);
		}
	}

	private void RefreshPlayerLabel(int labelItemId, int photoId)
	{
		if (base.contentPane is UIShowSkinWindow uIShowSkinWindow)
		{
			uIShowSkinWindow.com_Preview.LabelStatus.selectedIndex = 0;
			if (labelItemId == 0)
			{
				uIShowSkinWindow.com_Preview.LabelStatus.selectedIndex = 1;
				return;
			}
			UICom_PlayerLabel com_Label = (UICom_PlayerLabel)uIShowSkinWindow.com_Preview.com_PlayerLabel;
			string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
			int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
			ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
			(string, bool) playerLabel = labelItemId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
			CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
			CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, com_Label, playerLabel.Item1, playerLabel.Item2);
			string fashionAccountHeadShot = ((photoId == 0) ? runningFashion.headShotId : photoId).GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
		}
	}

	private void RefreshDescription(int skinItemId)
	{
		if (!(base.contentPane is UIShowSkinWindow uIShowSkinWindow))
		{
			return;
		}
		uIShowSkinWindow.infoType.selectedIndex = 0;
		uIShowSkinWindow.btn_Select.type.selectedIndex = 0;
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinItemId);
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel)
		{
			if (configStandingPainting.LongTextID != 0)
			{
				UIShowSkin_Button_SelectInfo btn_Select = uIShowSkinWindow.btn_Select;
				bool flag = (uIShowSkinWindow.com_Story.visible = true);
				btn_Select.visible = flag;
				uIShowSkinWindow.com_Story.txt_Story.text = configStandingPainting.LongTextID.GetLocal(UIStringType.Skin);
			}
			else
			{
				UIShowSkin_Button_SelectInfo btn_Select2 = uIShowSkinWindow.btn_Select;
				bool flag = (uIShowSkinWindow.com_Story.visible = false);
				btn_Select2.visible = flag;
			}
			ItemInfoConfigure itemConfig = skinItemId.GetItemInfoConfigure();
			if (itemConfig != null && itemConfig.WayList.Count > 0)
			{
				uIShowSkinWindow.com_Preview.group_Way.visible = true;
				uIShowSkinWindow.com_Preview.list_Way.itemRenderer = delegate(int index, GObject item)
				{
					if (item is UIButton_Way uIButton_Way)
					{
						uIButton_Way.Refresh(itemConfig.WayList[index]);
					}
				};
				uIShowSkinWindow.com_Preview.list_Way.numItems = itemConfig.WayList.Count;
				uIShowSkinWindow.com_Preview.list_Way.ResizeToFit();
			}
			else
			{
				uIShowSkinWindow.com_Preview.group_Way.visible = false;
			}
		}
		else
		{
			if (configStandingPainting.ShortTextID != 0)
			{
				UIShowSkin_Button_SelectInfo btn_Select3 = uIShowSkinWindow.btn_Select;
				bool flag = (uIShowSkinWindow.com_Story.visible = true);
				btn_Select3.visible = flag;
				uIShowSkinWindow.com_Story.txt_Story.text = configStandingPainting.ShortTextID.GetLocal(UIStringType.Skin);
			}
			else
			{
				UIShowSkin_Button_SelectInfo btn_Select4 = uIShowSkinWindow.btn_Select;
				bool flag = (uIShowSkinWindow.com_Story.visible = false);
				btn_Select4.visible = flag;
			}
			uIShowSkinWindow.com_Preview.group_Way.visible = false;
		}
		uIShowSkinWindow.com_Story.scrollPane.percY = 0f;
	}

	private void OpenGoodsDetail(EventContext context)
	{
		if (goodsData.SellOut() || goodsData.IsOwn() || !(base.contentPane is UIShowSkinWindow uIShowSkinWindow))
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsPurchaseType(goodsData.shopTabType) == GoodsPurchaseType.Recharge)
		{
			SimpleSingletonProvider<UIManager>.inst.discount.ShowSkinDiscount(goodsData).Forget();
		}
		else
		{
			List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.Coupons);
			if (itemsByType == null || itemsByType.Count == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(goodsData).Forget();
				return;
			}
			SimpleSingletonProvider<UIManager>.inst.discount.ShowEXSkinDiscount(goodsData, itemsByType).Forget();
		}
		uIShowSkinWindow.btn_Purchase.onClick.Release();
	}
}
