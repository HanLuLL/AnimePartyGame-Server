using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class BattlePassPanel : BasePanel<UIBattlePassPanel>
{
	private int mustShowLV;

	private RepeatedField<BattlePassRewardAdsConfigureItem> RewardAds;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private GLoader _bgLoader;

	private GTweener fadeInTweener;

	private GTweener fadeOutTweener;

	private List<int> dailyTaskIds;

	private List<int> weeklyTaskIds;

	private BattlePassData battlePassData => SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData;

	private RepeatedField<BattlePassRewardConfigureItem> BattlePassRewardConfigure => battlePassData.BattlePassInfo.BattlePassRewardConfig.BattlePassRewardConfigureItems;

	private GLoader bgLoader
	{
		get
		{
			if (_bgLoader == null)
			{
				_bgLoader = new GLoader();
				base.ui.com_RewardInfo.AddChildAt(_bgLoader, 0);
				_bgLoader.MakeFullScreen();
				_bgLoader.AddRelation(base.ui.com_RewardInfo, FairyGUI.RelationType.Height);
				_bgLoader.AddRelation(base.ui.com_RewardInfo, FairyGUI.RelationType.Width);
				_bgLoader.touchable = false;
				_bgLoader.fill = FillType.ScaleFree;
			}
			return _bgLoader;
		}
	}

	private BattlePassRewardDetailConfigure RewardDetailConfig => battlePassData.BattlePassInfo.BattlePassRewardDetailConfig;

	public BattlePassPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBattlePassPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null)
		{
			mustShowLV = battlePassData.LV;
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.tab.onChanged.Call();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponents_Task();
		InitComponents_Goods();
		InitComponents_Main();
		InitComponents_RewardInfo();
	}

	public override void Refresh()
	{
		base.Refresh();
		ShowEffect();
		RefreshBG();
		RefreshSkin();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.tab.onChanged.Add(SwitchTab);
		base.ui.btn_PreviewSkin.onClick.Add(ShowSkinDetail);
		AddEvent_Task();
		AddEvent_Goods();
		AddEvent_Main();
		AddEvent_RewardInfo();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.tab.onChanged.Remove(SwitchTab);
		base.ui.btn_PreviewSkin.onClick.Remove(ShowSkinDetail);
		RemoveEvent_Task();
		RemoveEvent_Goods();
		RemoveEvent_Main();
		RemoveEvent_RewardInfo();
	}

	protected override void AddListener()
	{
		base.AddListener();
		AddListener_Task();
		AddListener_Goods();
		AddListener_Main();
		AddListener_RewardInfo();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		RemoveListener_Task();
		RemoveListener_Goods();
		RemoveListener_Main();
		RemoveListener_RewardInfo();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		if (base.ui.tab.selectedIndex == 2)
		{
			base.ui.tab.selectedIndex = 0;
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
			base.ui.tab.selectedIndex = 0;
		}
		base.ui.btn_Return.onClick.Release();
	}

	private void SwitchTab()
	{
		base.ui.tab.onChanged.Retain();
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		if (base.ui.tab.selectedIndex == 0)
		{
			Refresh_Main();
		}
		else if (base.ui.tab.selectedIndex == 1)
		{
			Refresh_Task();
		}
		else if (base.ui.tab.selectedIndex == 2)
		{
			Refresh_Goods();
		}
		base.ui.tab.onChanged.Release();
	}

	private void RefreshBG()
	{
		base.ui.loader_Main.Background(battlePassData.BattlePassInfo.MainBG);
		base.ui.loader_Task.Background(battlePassData.BattlePassInfo.TaskBG);
	}

	private void RefreshSkin()
	{
		int skinID = battlePassData.BattlePassInfo.SkinID;
		(string, bool) character = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(skinID).GetCharacter();
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
		{
			customOffset = value.skinOffset;
		}
		base.ui.loader_Skin.customOffset = customOffset;
		base.ui.loader_Skin.customScale = Vector2.one;
		base.ui.loader_Skin.url = character.Item1;
		base.ui.txt_SkinName.text = battlePassData.BattlePassInfo.SkinID.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item);
	}

	private async void ShowSkinDetail()
	{
		base.ui.btn_PreviewSkin.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(battlePassData.BattlePassInfo.SkinID, battlePassData.BattlePassInfo.AccountBackgroundID);
		base.ui.btn_PreviewSkin.onClick.Release();
	}

	private async void OpenPropDetail(int itemId, int itemNum)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, itemNum, _Usable: false);
	}

	private void ShowEffect()
	{
	}

	protected void InitComponents_Goods()
	{
		base.ui.com_Goods.list_NormalGoods.itemRenderer = RendererNormalGoods;
		base.ui.com_Goods.list_NormalGoods.itemProvider = ProviderNormalGoodsItem;
		base.ui.com_Goods.list_PremiumGoods.itemRenderer = RendererPremiumGoods;
		base.ui.com_Goods.list_PremiumGoods.itemProvider = ProviderPremiumGoodsItem;
	}

	public void Refresh_Goods()
	{
		RewardAds = battlePassData.BattlePassInfo.BattlePassRewardAdsConfig.BattlePassRewardAdsConfigureItems;
		if (battlePassData.gearType == BattlePassGearType.FREE)
		{
			RechargeGoods normalGoods = battlePassData.GetNormalGoods();
			RefreshPurchaseButton(base.ui.com_Goods.btn_PurchaseNormalGoods, normalGoods, battlePassData.BattlePassInfo.NormalName.GetLocal(UIStringType.BattlePass));
			RechargeGoods premiumGoods = battlePassData.GetPremiumGoods();
			RefreshPurchaseButton(base.ui.com_Goods.btn_PurchasePremiumGoods, premiumGoods, "[color=#FF09B9]" + battlePassData.BattlePassInfo.PremiumName.GetLocal(UIStringType.BattlePass) + "[/color]");
			base.ui.com_Goods.status.selectedIndex = 0;
			base.ui.com_Goods.list_NormalGoods.numItems = RewardAds.Count;
			base.ui.com_Goods.list_PremiumGoods.numItems = RewardAds.Count;
		}
		else
		{
			RechargeGoods premiumGoods2 = battlePassData.GetPremiumGoods();
			RefreshPurchaseButton(base.ui.com_Goods.btn_PurchasePremiumGoods, premiumGoods2, "[color=#FF09B9]" + battlePassData.BattlePassInfo.PremiumName.GetLocal(UIStringType.BattlePass) + "[/color]");
			base.ui.com_Goods.status.selectedIndex = 1;
			base.ui.com_Goods.list_PremiumGoods.numItems = RewardAds.Count;
		}
	}

	protected void AddEvent_Goods()
	{
		base.ui.com_Goods.btn_PurchaseNormalGoods.onClick.Add(OnRequestPurchaseNormalBattlePass);
		base.ui.com_Goods.btn_PurchasePremiumGoods.onClick.Add(OnRequestPurchasePremiumBattlePass);
	}

	protected void RemoveEvent_Goods()
	{
		base.ui.com_Goods.btn_PurchaseNormalGoods.onClick.Remove(OnRequestPurchaseNormalBattlePass);
		base.ui.com_Goods.btn_PurchasePremiumGoods.onClick.Remove(OnRequestPurchasePremiumBattlePass);
	}

	protected void AddListener_Goods()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateGear.AddListener(UpdateBattlePassGear);
	}

	protected void RemoveListener_Goods()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateGear.RemoveListener(UpdateBattlePassGear);
	}

	private void RefreshPurchaseButton(UIBattlePass_Button_Purchase _button, RechargeGoods _Goods, string _Name)
	{
		_button.txt_Topic.text = _Name;
		_button.txt_Price.text = _Goods.GetDiscountPriceText();
		_button.txt_Price.AddCurrencySymbols(_button.txt_Price.text);
		_button.txt_Countdown.text = battlePassData.GetCutDown();
		_button.loader_HeroIcon.url = battlePassData.BattlePassInfo.EntranceUnfold;
		_button.loader_BG.url = battlePassData.BattlePassInfo.ButtonTexture;
	}

	private async void OnRequestPurchaseNormalBattlePass()
	{
		BattlePassGearType gearType = battlePassData.gearType;
		if (gearType != BattlePassGearType.NORMAL && gearType != BattlePassGearType.PREMIUM)
		{
			base.ui.com_Goods.btn_PurchaseNormalGoods.onClick.Retain();
			RechargeGoods normalGoods = battlePassData.GetNormalGoods();
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(normalGoods, 1);
			base.ui.com_Goods.btn_PurchaseNormalGoods.onClick.Release();
		}
	}

	private async void OnRequestPurchasePremiumBattlePass()
	{
		if (battlePassData.gearType != BattlePassGearType.PREMIUM)
		{
			RechargeGoods premiumGoods = battlePassData.GetPremiumGoods();
			base.ui.com_Goods.btn_PurchasePremiumGoods.onClick.Retain();
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(premiumGoods, 1);
			base.ui.com_Goods.btn_PurchasePremiumGoods.onClick.Release();
		}
	}

	private void UpdateBattlePassGear()
	{
		if (base.ui.tab.selectedIndex == 0)
		{
			Refresh_Main();
		}
		else
		{
			base.ui.tab.selectedIndex = 0;
		}
	}

	private string ProviderNormalGoodsItem(int index)
	{
		return ProviderGoodsItem(RewardAds[index].NormalUIType);
	}

	private void RendererNormalGoods(int index, GObject item)
	{
		item.visible = RewardAds[index].NormalUIType != BattlePassUIType.None;
		RendererGoods(item, RewardAds[index].NormalItemID, RewardAds[index].NormalDescription, RewardAds[index].NormalResource);
	}

	private string ProviderPremiumGoodsItem(int index)
	{
		return ProviderGoodsItem(RewardAds[index].PremiumUIType);
	}

	private void RendererPremiumGoods(int index, GObject item)
	{
		if (RewardAds[index].PremiumUIType != BattlePassUIType.None)
		{
			item.visible = true;
			RendererGoods(item, RewardAds[index].PremiumItemID, RewardAds[index].PremiumDescription, RewardAds[index].PremiumResource);
		}
		else
		{
			item.visible = false;
		}
	}

	private string ProviderGoodsItem(BattlePassUIType uiType)
	{
		return uiType switch
		{
			BattlePassUIType.Skin => "ui://ssf8xg9njz2422", 
			BattlePassUIType.Topic => "ui://ssf8xg9njz2426", 
			BattlePassUIType.AccountBackground => "ui://ssf8xg9njz2423", 
			BattlePassUIType.Common => "ui://ssf8xg9njz2424", 
			BattlePassUIType.Medium => "ui://ssf8xg9njz2425", 
			BattlePassUIType.Large => "ui://ssf8xg9njz2427", 
			_ => "ui://ssf8xg9njz2424", 
		};
	}

	private void RendererGoods(GObject item, int ItemID, int Description, string Resource)
	{
		if (item is UIBattlePass_Com_SkinItem uIBattlePass_Com_SkinItem)
		{
			uIBattlePass_Com_SkinItem.txt_title.text = Description.GetLocal(UIStringType.BattlePass);
			RendererSkin(uIBattlePass_Com_SkinItem.graph_Skin, ItemID.GetItemInfoConfigure()).Forget();
		}
		else if (item is UIBattlePass_Com_TopicItem uIBattlePass_Com_TopicItem)
		{
			uIBattlePass_Com_TopicItem.loader_Theme.url = Resource;
			uIBattlePass_Com_TopicItem.txt_title.text = Description.GetLocal(UIStringType.BattlePass);
		}
		else if (item is UIBattlePass_Com_PlayerLabelItem uIBattlePass_Com_PlayerLabelItem)
		{
			uIBattlePass_Com_PlayerLabelItem.txt_Title.text = Description.GetLocal(UIStringType.BattlePass);
			UICom_PlayerLabel com_Label = (UICom_PlayerLabel)uIBattlePass_Com_PlayerLabelItem.com_PlayerLabel;
			string name = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
			int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
			ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
			(string, bool) playerLabel = ItemID.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
			string fashionAccountHeadShot = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererLabelInfo(com_Label, name, level);
			CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_Label, playerLabel.Item1, playerLabel.Item2);
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
		}
		else if (item is UIBattlePass_Com_CommonItem uIBattlePass_Com_CommonItem)
		{
			uIBattlePass_Com_CommonItem.txt_title.text = Description.GetLocal(UIStringType.BattlePass);
			uIBattlePass_Com_CommonItem.loader_Icon.url = ItemID.GetItemInfoConfigure().ShowIcon;
		}
		else if (item is UIBattlePass_Com_MediumItem uIBattlePass_Com_MediumItem)
		{
			uIBattlePass_Com_MediumItem.txt_title.text = Description.GetLocal(UIStringType.BattlePass);
			ItemInfoConfigure itemInfoConfigure = ItemID.GetItemInfoConfigure();
			if (itemInfoConfigure.ItemType == ItemType.HeroExpression)
			{
				RendererExpression(uIBattlePass_Com_MediumItem.loader_Image, uIBattlePass_Com_MediumItem.loader_Video, uIBattlePass_Com_MediumItem.type, itemInfoConfigure).Forget();
				return;
			}
			uIBattlePass_Com_MediumItem.loader_Image.url = ((!string.IsNullOrEmpty(Resource)) ? Resource : ItemID.GetItemInfoConfigure().ShowIcon);
			uIBattlePass_Com_MediumItem.type.selectedIndex = 0;
		}
		else if (item is UIBattlePass_Com_LargeItem uIBattlePass_Com_LargeItem)
		{
			uIBattlePass_Com_LargeItem.txt_title.text = Description.GetLocal(UIStringType.BattlePass);
			if (!string.IsNullOrEmpty(Resource))
			{
				uIBattlePass_Com_LargeItem.loader_Image.url = Resource;
				uIBattlePass_Com_LargeItem.type.selectedIndex = 0;
			}
		}
	}

	private void InitComponents_Main()
	{
		base.ui.list_BattlePass.SetVirtual();
		base.ui.list_BattlePass.itemRenderer = RendererBattlePassItem;
		base.ui.list_BattlePass.itemProvider = ProviderBattlePassItem;
	}

	private void Refresh_Main()
	{
		base.ui.btn_Task.redStatus.selectedIndex = (battlePassData.GetTaskSystemStatus() ? 1 : 0);
		BattlePassReward popupRewardData = battlePassData.GetPopupRewardData();
		base.ui.btn_battlePass.redStatus.selectedIndex = (popupRewardData.VailReward ? 1 : 0);
		base.ui.com_Progress.progress_Exp.max = battlePassData.BattlePassInfo.ExpPerLv;
		base.ui.com_Progress.progress_Exp.value = battlePassData.Exp;
		int num = Mathf.Min(battlePassData.LV + 1, battlePassData.MaxLevel);
		base.ui.com_Progress.txt_NextLevel.text = num.ToString();
		base.ui.com_Progress.status.selectedIndex = ((battlePassData.LV + 1 > battlePassData.MaxLevel) ? 1 : 0);
		if (base.ui.tab.selectedIndex != 0)
		{
			return;
		}
		if (mustShowLV == 0)
		{
			mustShowLV = battlePassData.LV;
		}
		base.ui.list_BattlePass.numItems = BattlePassRewardConfigure.Count + 1;
		base.ui.list_BattlePass.ScrollToView(Mathf.Clamp(mustShowLV - 1, 0, BattlePassRewardConfigure.Count));
		mustShowLV = 0;
		if (battlePassData.gearType == BattlePassGearType.PREMIUM)
		{
			base.ui.btn_GoPurchase.visible = false;
			return;
		}
		BattlePassGearType gearType = battlePassData.gearType;
		if (gearType == BattlePassGearType.NONE || gearType == BattlePassGearType.FREE)
		{
			RechargeGoods normalGoods = battlePassData.GetNormalGoods();
			string local = battlePassData.BattlePassInfo.NormalName.GetLocal(UIStringType.BattlePass);
			RefreshPurchaseButton(base.ui.btn_GoPurchase, normalGoods, local);
		}
		else
		{
			RechargeGoods premiumGoods = battlePassData.GetPremiumGoods();
			string name = "[color=#FF09B9]" + battlePassData.BattlePassInfo.PremiumName.GetLocal(UIStringType.BattlePass) + "[/color]";
			RefreshPurchaseButton(base.ui.btn_GoPurchase, premiumGoods, name);
		}
		base.ui.btn_GoPurchase.visible = battlePassData.gearType < BattlePassGearType.PREMIUM;
	}

	private void AddEvent_Main()
	{
		base.ui.btn_GoPurchase.onClick.Add(OpenGoodsTab);
	}

	private void RemoveEvent_Main()
	{
		base.ui.btn_GoPurchase.onClick.Remove(OpenGoodsTab);
	}

	private void AddListener_Main()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateBattlePass.AddListener(Refresh_Main);
	}

	private void RemoveListener_Main()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateBattlePass.RemoveListener(Refresh_Main);
	}

	private void OpenGoodsTab()
	{
		base.ui.btn_GoPurchase.onClick.Retain();
		base.ui.tab.selectedIndex = 2;
		base.ui.btn_GoPurchase.onClick.Release();
	}

	private string ProviderBattlePassItem(int index)
	{
		if (index < BattlePassRewardConfigure.Count)
		{
			int level = BattlePassRewardConfigure[index].Level;
			if (battlePassData.battlePassRewardDict[level].PremiumReward != null)
			{
				return "ui://ssf8xg9njz241m";
			}
			return "ui://ssf8xg9njz2418";
		}
		return "ui://ssf8xg9nb93d2a";
	}

	private void RendererBattlePassItem(int index, GObject item)
	{
		if (index < BattlePassRewardConfigure.Count)
		{
			int level = BattlePassRewardConfigure[index].Level;
			BattlePassReward battlePassReward = battlePassData.battlePassRewardDict[level];
			bool gearStatus = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.NORMAL);
			if (battlePassReward.PremiumReward != null)
			{
				if (item is UIBattlePass_Com_DoubleReward uIBattlePass_Com_DoubleReward)
				{
					RendererProgress(uIBattlePass_Com_DoubleReward.progress_Exp, battlePassReward);
					RendererFreeItem(uIBattlePass_Com_DoubleReward.btn_FreeItem, battlePassReward);
					RendererSaleItem(uIBattlePass_Com_DoubleReward.com_DoubleReward.btn_NormalReward, battlePassReward.NormalReward, gearStatus, battlePassReward.IsFinishNormalReward());
					bool gearStatus2 = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.PREMIUM);
					RendererSaleItem(uIBattlePass_Com_DoubleReward.com_DoubleReward.btn_PremiumReward, battlePassReward.PremiumReward, gearStatus2, battlePassReward.IsFinishPremiumReward());
					RegisterSwitch(uIBattlePass_Com_DoubleReward);
				}
			}
			else if (item is UIBattlePass_Com_Reward uIBattlePass_Com_Reward)
			{
				RendererProgress(uIBattlePass_Com_Reward.progress_Exp, battlePassReward);
				RendererFreeItem(uIBattlePass_Com_Reward.btn_FreeItem, battlePassReward);
				RendererSaleItem(uIBattlePass_Com_Reward.btn_NormalReward, battlePassReward.NormalReward, gearStatus, battlePassReward.IsFinishNormalReward());
			}
		}
		else if (item is UIBattlePass_Button_SurpassReward uIBattlePass_Button_SurpassReward)
		{
			RendererSurpassItem(uIBattlePass_Button_SurpassReward.btn_SurpassReward, battlePassData.surpassReward);
		}
	}

	private void RegisterSwitch(UIBattlePass_Com_DoubleReward premiumRewardItem)
	{
		premiumRewardItem.com_DoubleReward.switchTrans.SetHook("SetDescent", delegate
		{
			premiumRewardItem.com_DoubleReward.childrenRenderOrder = ChildrenRenderOrder.Descent;
		});
		premiumRewardItem.com_DoubleReward.switchTrans.SetHook("SetAscent", delegate
		{
			premiumRewardItem.com_DoubleReward.childrenRenderOrder = ChildrenRenderOrder.Ascent;
		});
		premiumRewardItem.com_DoubleReward.switchTrans.Play(-1, 0f, null);
	}

	private void RendererProgress(UIBattlePass_Progress_Item progressCom, BattlePassReward rewardData)
	{
		progressCom.txt_Level.text = rewardData.LV.ToString();
		progressCom.status.selectedIndex = ((rewardData.LV <= battlePassData.LV) ? 1 : 0);
		progressCom.max = battlePassData.BattlePassInfo.ExpPerLv;
		progressCom.value = ((rewardData.LV < battlePassData.LV) ? battlePassData.BattlePassInfo.ExpPerLv : ((rewardData.LV == battlePassData.LV) ? battlePassData.Exp : 0));
		if (battlePassData.LV == rewardData.LV && rewardData.LV < battlePassData.MaxLevel)
		{
			MapField<int, int> costPerLv = battlePassData.BattlePassInfo.CostPerLv;
			progressCom.btn_PurchaseLv.visible = true;
			ItemInfoConfigure itemInfoConfigure = costPerLv.ElementAt(0).Key.GetItemInfoConfigure();
			progressCom.btn_PurchaseLv.txt_CostPerLv.text = costPerLv.ElementAt(0).Value.ToString();
			progressCom.btn_PurchaseLv.loader_CostCurrency.url = itemInfoConfigure.ShowIcon;
			progressCom.btn_PurchaseLv.onClick.Set((EventCallback0)delegate
			{
				TryOpenPurchaseBattlePassLevel(progressCom).Forget();
			});
		}
		else
		{
			progressCom.btn_PurchaseLv.visible = false;
		}
	}

	private async UniTask TryOpenPurchaseBattlePassLevel(UIBattlePass_Progress_Item progressCom)
	{
		progressCom.btn_PurchaseLv.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseBattlePassLevel();
		progressCom.btn_PurchaseLv.onClick.Release();
	}

	private void RendererFreeItem(UIBattlePass_Button_FreeItem btnFreeItem, BattlePassReward rewardData)
	{
		BattlePassRewardData freeRewardData = rewardData.FreeReward;
		btnFreeItem.loader_Item.url = freeRewardData.itemConfig.ShowIcon;
		btnFreeItem.txt_ItemFreeNum.text = $"x{freeRewardData.rewardCount}";
		btnFreeItem.isFinish.selectedIndex = (rewardData.IsFinishFreeReward() ? 1 : 0);
		btnFreeItem.vailReward.selectedIndex = (rewardData.FreeVailReward ? 1 : 0);
		btnFreeItem.onClick.Set((EventCallback0)delegate
		{
			if (!rewardData.FreeVailReward)
			{
				OpenPropDetail(freeRewardData.itemConfig.Id, freeRewardData.rewardCount);
			}
			else
			{
				btnFreeItem.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassGetRewardC2S().OnFinishedOnly.AddOnce(delegate
				{
					btnFreeItem.onClick.Release();
				});
			}
		});
	}

	private void RendererSurpassItem(UIBattlePass_Button_SurpassItem btnSurpassReward, BattlePassReward surpassReward)
	{
		BattlePassRewardData surpassRewardData = surpassReward.FreeReward;
		btnSurpassReward.LockStatus.selectedIndex = 1;
		btnSurpassReward.loader_Icon.url = surpassRewardData.itemConfig.ShowIcon;
		btnSurpassReward.txt_Name.text = surpassRewardData.itemConfig.NameID.GetLocal(UIStringType.Item);
		btnSurpassReward.txt_Count.text = $"x{surpassReward.vailSurpassRewardTime}";
		btnSurpassReward.isFinish.selectedIndex = (surpassReward.IsFinishSurpassReward() ? 1 : 0);
		btnSurpassReward.vailReward.selectedIndex = (surpassReward.SurpassVailReward ? 1 : 0);
		btnSurpassReward.onClick.Set((EventCallback0)delegate
		{
			if (!surpassReward.SurpassVailReward)
			{
				OpenPropDetail(surpassRewardData.itemConfig.Id, surpassRewardData.rewardCount);
			}
			else
			{
				btnSurpassReward.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassGetRewardC2S().OnFinishedOnly.AddOnce(delegate
				{
					btnSurpassReward.onClick.Release();
				});
			}
		});
	}

	private void RendererSaleItem(UIBattlePass_Button_SaleItem item, BattlePassRewardData rewardData, bool unlock, bool isFinish)
	{
		item.txt_Name.text = rewardData.itemConfig.NameID.GetLocal(UIStringType.Item);
		item.LockStatus.selectedIndex = (unlock ? 1 : 0);
		item.vailReward.selectedIndex = ((unlock && rewardData.vailLV && !isFinish) ? 1 : 0);
		item.isFinish.selectedIndex = ((unlock && isFinish) ? 1 : 0);
		item.onClick.Set((EventCallback0)delegate
		{
			if (!(!isFinish && unlock) || !rewardData.vailLV)
			{
				OpenPropDetail(rewardData.itemConfig.Id, rewardData.rewardCount);
			}
			else
			{
				item.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassGetRewardC2S().OnFinishedOnly.AddOnce(delegate
				{
					item.onClick.Release();
				});
			}
		});
		ItemType itemType = rewardData.itemConfig.ItemType;
		if (itemType == ItemType.HeroStandingPainting || itemType == ItemType.HeroExpression || itemType == ItemType.AccountBackground || itemType == ItemType.CardBack)
		{
			if (rewardData.itemConfig.ItemType == ItemType.HeroStandingPainting)
			{
				item.showItemType.selectedIndex = 1;
				RendererSkin(item.graph_Skin, rewardData.itemConfig).Forget();
			}
			else
			{
				SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(item.graph_Skin);
			}
			if (rewardData.itemConfig.ItemType == ItemType.HeroExpression)
			{
				item.showItemType.selectedIndex = 3;
				UICom_Expression uICom_Expression = (UICom_Expression)item.com_expression;
				RendererExpression(uICom_Expression.loader_Expression_Image, uICom_Expression.loader_Expression_Video, uICom_Expression.type, rewardData.itemConfig).Forget();
			}
			else
			{
				UICom_Expression uICom_Expression2 = (UICom_Expression)item.com_expression;
				SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uICom_Expression2.loader_Expression_Video);
			}
			if (rewardData.itemConfig.ItemType == ItemType.AccountBackground)
			{
				item.showItemType.selectedIndex = 2;
				RendererPlayerLabel((UICom_PlayerLabel_Loader)item.com_PlayerLabel.com_PlayerLabel, rewardData);
			}
			else
			{
				UICom_Loader_PlayerLabel com_Loader = ((UICom_PlayerLabel_Loader)item.com_PlayerLabel.com_PlayerLabel).com_Loader;
				SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(com_Loader.loader_Video);
			}
			if (rewardData.itemConfig.ItemType == ItemType.CardBack)
			{
				item.showItemType.selectedIndex = 4;
				CommonUIManager.RendererCardBack((UICom_CardBack)item.com_CardBack, rewardData.itemConfig.SubMeterID.GetFashionCardBackConfigure()).Forget();
			}
			item.infoType.selectedIndex = 0;
		}
		else
		{
			item.showItemType.selectedIndex = 0;
			RendererCommonPropInfo(item, rewardData);
			SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(item.graph_Skin);
			UICom_Expression uICom_Expression3 = (UICom_Expression)item.com_expression;
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uICom_Expression3.loader_Expression_Video);
			UICom_Loader_PlayerLabel com_Loader2 = ((UICom_PlayerLabel_Loader)item.com_PlayerLabel.com_PlayerLabel).com_Loader;
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(com_Loader2.loader_Video);
		}
	}

	private void RendererCommonPropInfo(UIBattlePass_Button_SaleItem item, BattlePassRewardData rewardData)
	{
		item.loader_Icon.url = (string.IsNullOrEmpty(rewardData.specialIcon) ? rewardData.itemConfig.ShowIcon : rewardData.specialIcon);
		item.infoType.selectedIndex = rewardData.infoType;
		item.loader_PropItem.url = rewardData.itemConfig.ShowIcon;
		item.txt_Count.text = $"x{rewardData.rewardCount}";
	}

	private async UniTask RendererSkin(GGraph graph_Skin, ItemInfoConfigure itemConfig)
	{
		foreach (KeyValuePair<int, SkinStandingPaintingConfigure> item in StaticConfigure.Skin.StandingPaintingDict)
		{
			RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems = item.Value.SkinStandingPaintingConfigureItems;
			for (int i = 0; i < skinStandingPaintingConfigureItems.Count; i++)
			{
				if (skinStandingPaintingConfigureItems[i].ItemID == itemConfig.Id)
				{
					await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(skinStandingPaintingConfigureItems[i], "Walk", graph_Skin, 10f);
					graph_Skin.visible = true;
					return;
				}
			}
		}
	}

	private async UniTask RendererExpression(GLoader image, GGraph video, Controller type, ItemInfoConfigure itemConfig)
	{
		foreach (KeyValuePair<int, CharacterExpressionPackConfigure> item in StaticConfigure.Character.ExpressionPackDict)
		{
			RepeatedField<CharacterExpressionPackConfigureItem> characterExpressionPackConfigureItems = item.Value.CharacterExpressionPackConfigureItems;
			for (int i = 0; i < characterExpressionPackConfigureItems.Count; i++)
			{
				if (characterExpressionPackConfigureItems[i].ItemID == itemConfig.Id)
				{
					string videoKey = characterExpressionPackConfigureItems[i].VideoKey;
					if (!string.IsNullOrEmpty(videoKey))
					{
						CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, video);
						await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, video);
						type.selectedIndex = 1;
					}
					else
					{
						image.url = itemConfig.ShowIcon;
						type.selectedIndex = 0;
					}
					return;
				}
			}
		}
	}

	private void RendererPlayerLabel(UICom_PlayerLabel_Loader com_PlayerLabel, BattlePassRewardData rewardData)
	{
		(string, bool) playerLabel = rewardData.itemConfig.SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_PlayerLabel, playerLabel.Item1, playerLabel.Item2);
	}

	protected void InitComponents_RewardInfo()
	{
		base.ui.com_RewardInfo.mohu.relations.ClearAll();
		base.ui.com_RewardInfo.mohu.AddRelation(base.ui.com_RewardInfo, FairyGUI.RelationType.Center_Center);
		base.ui.com_RewardInfo.mohu.SetSize(GRoot.inst.width, GRoot.inst.height);
		base.ui.com_RewardInfo.list_Rewards.itemRenderer = RendererRewardInfo;
	}

	public void Refresh_RewardInfo()
	{
		base.ui.com_RewardInfo.txt_NormalTitle.text = battlePassData.BattlePassInfo.NormalName.GetLocal(UIStringType.BattlePass);
		base.ui.com_RewardInfo.txt_PremiumTitle.text = battlePassData.BattlePassInfo.PremiumName.GetLocal(UIStringType.BattlePass);
		GLoader loader_Normal = base.ui.com_RewardInfo.loader_Normal;
		string url = (base.ui.com_RewardInfo.loader_Premium.url = battlePassData.BattlePassInfo.BattlePassIcon);
		loader_Normal.url = url;
		base.ui.com_RewardInfo.list_Rewards.numItems = RewardDetailConfig.BattlePassRewardDetailConfigureItems.Count;
	}

	protected void AddEvent_RewardInfo()
	{
		base.ui.com_Goods.btn_RewardInfo.onClick.Add(OpenRewardInfo);
		base.ui.com_RewardInfo.mohu.onClick.Add(CloseRewardInfo);
		base.ui.com_RewardInfo.btn_Close.onClick.Add(CloseRewardInfo);
	}

	protected void RemoveEvent_RewardInfo()
	{
		base.ui.com_Goods.btn_RewardInfo.onClick.Remove(OpenRewardInfo);
		base.ui.com_RewardInfo.mohu.onClick.Remove(CloseRewardInfo);
		base.ui.com_RewardInfo.btn_Close.onClick.Remove(CloseRewardInfo);
	}

	protected void AddListener_RewardInfo()
	{
	}

	protected void RemoveListener_RewardInfo()
	{
	}

	private void RendererRewardInfo(int index, GObject item)
	{
		if (item is UIBattlePass_Button_RewardInfoItem uIBattlePass_Button_RewardInfoItem)
		{
			BattlePassRewardDetailConfigureItem battlePassRewardDetailConfigureItem = RewardDetailConfig.BattlePassRewardDetailConfigureItems[index];
			if (string.IsNullOrEmpty(battlePassRewardDetailConfigureItem.NormalIcon))
			{
				uIBattlePass_Button_RewardInfoItem.showNormal.selectedIndex = 1;
			}
			else
			{
				uIBattlePass_Button_RewardInfoItem.showNormal.selectedIndex = 0;
				uIBattlePass_Button_RewardInfoItem.loader_Normal.url = battlePassRewardDetailConfigureItem.NormalIcon;
				uIBattlePass_Button_RewardInfoItem.txt_NormalContent.text = battlePassRewardDetailConfigureItem.NormalDescription.GetLocal(UIStringType.BattlePass);
				uIBattlePass_Button_RewardInfoItem.normalStatus.selectedIndex = (battlePassRewardDetailConfigureItem.IsNormalSpecial ? 1 : 0);
			}
			if (string.IsNullOrEmpty(RewardDetailConfig.BattlePassRewardDetailConfigureItems[index].PremiumIcon))
			{
				uIBattlePass_Button_RewardInfoItem.showPremium.selectedIndex = 1;
				return;
			}
			uIBattlePass_Button_RewardInfoItem.showPremium.selectedIndex = 0;
			uIBattlePass_Button_RewardInfoItem.loader_Premium.url = battlePassRewardDetailConfigureItem.PremiumIcon;
			uIBattlePass_Button_RewardInfoItem.txt_PremiumContent.text = battlePassRewardDetailConfigureItem.PremiumDescription.GetLocal(UIStringType.BattlePass);
			uIBattlePass_Button_RewardInfoItem.premiumStatus.selectedIndex = (battlePassRewardDetailConfigureItem.IsPremiumSpecial ? 1 : 0);
		}
	}

	private void CloseRewardInfo()
	{
		base.ui.com_RewardInfo.mohu.onClick.Retain();
		base.ui.com_RewardInfo.mohu.onClick.Release();
		HideRewardInfo(delegate
		{
			base.ui.showRewardInfo.selectedIndex = 0;
		});
	}

	private void OpenRewardInfo()
	{
		base.ui.com_Goods.btn_RewardInfo.onClick.Retain();
		Refresh_RewardInfo();
		base.ui.showRewardInfo.selectedIndex = 1;
		base.ui.com_Goods.btn_RewardInfo.onClick.Release();
		ShowRewardInfo().Forget();
	}

	private async UniTask ShowRewardInfo()
	{
		base.ui.touchableAll = false;
		base.ui.com_RewardInfo.alpha = 0f;
		await blurBgCtrl.CreateBlurTex();
		blurBgCtrl.OnShown(bgLoader);
		if (fadeInTweener != null && !fadeInTweener._killed)
		{
			fadeInTweener.Kill();
			fadeInTweener = null;
		}
		fadeInTweener = GTween.To(0f, 1f, 0.3f).SetTarget(base.ui.com_RewardInfo, TweenPropType.Alpha).OnComplete((GTweenCallback)delegate
		{
			base.ui.touchableAll = true;
		});
	}

	private void HideRewardInfo(Action onHide)
	{
		base.ui.touchableAll = false;
		if (fadeOutTweener != null && !fadeOutTweener._killed)
		{
			fadeOutTweener.Kill();
			fadeOutTweener = null;
		}
		fadeOutTweener = GTween.To(base.ui.com_RewardInfo.alpha, 0f, 0.3f).SetTarget(base.ui.com_RewardInfo, TweenPropType.Alpha).OnComplete((GTweenCallback)delegate
		{
			blurBgCtrl.OnHide();
			base.ui.touchableAll = true;
			onHide?.Invoke();
		});
	}

	protected void InitComponents_Task()
	{
		base.ui.list_DailyTask.itemRenderer = RendererDailyTask;
		base.ui.list_WeeklyTask.itemRenderer = RendererWeeklyTask;
	}

	public void Refresh_Task()
	{
		base.ui.btn_Task.redStatus.selectedIndex = (battlePassData.GetTaskSystemStatus() ? 1 : 0);
		if (base.ui.tab.selectedIndex == 1)
		{
			dailyTaskIds = battlePassData.GetTaskIdsByType(TaskRefreshType.Daily);
			weeklyTaskIds = battlePassData.GetTaskIdsByType(TaskRefreshType.Weekly);
			base.ui.txt_DailyTime.text = TimeHelper.GetDailyTime();
			base.ui.txt_WeeklyTime.text = TimeHelper.GetWeeklyTime();
			base.ui.list_DailyTask.numItems = dailyTaskIds.Count;
			base.ui.list_WeeklyTask.numItems = weeklyTaskIds.Count;
		}
	}

	protected void AddEvent_Task()
	{
	}

	protected void RemoveEvent_Task()
	{
	}

	protected void AddListener_Task()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateTask.AddListener(Refresh_Task);
	}

	protected void RemoveListener_Task()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateTask.RemoveListener(Refresh_Task);
	}

	private void RendererDailyTask(int index, GObject item)
	{
		if (item is UIBattlePass_Com_TaskLabel label)
		{
			RendererTaskLabel(label, battlePassData.taskDataDict[dailyTaskIds[index]]);
		}
	}

	private void RendererWeeklyTask(int index, GObject item)
	{
		if (item is UIBattlePass_Com_TaskLabel label)
		{
			RendererTaskLabel(label, battlePassData.taskDataDict[weeklyTaskIds[index]]);
		}
	}

	private void RendererTaskLabel(UIBattlePass_Com_TaskLabel label, BattlePassTaskData taskData)
	{
		((UICom_LitItem)label.btn_Reward).loader_Icon.url = taskData.taskConfig.Icon;
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_Title.text = taskData.taskConfig.NameID.GetLocal(UIStringType.BattlePass);
		((UICom_LitItem)label.btn_Reward).txt_itemNum.text = taskData.taskConfig.Exp.ToString();
		RefreshBar(label.progress_task, taskData.taskConfig.Param, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassTaskRewardC2S(taskData.taskConfig.Id).OnFinishedOnly.AddOnce(delegate
				{
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}
}
