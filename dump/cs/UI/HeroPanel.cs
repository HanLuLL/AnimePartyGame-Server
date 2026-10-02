using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class HeroPanel : BasePanel<UIHeroPanel>
{
	private enum Guide200200Source
	{
		Auto,
		Manual
	}

	private CharacterInfoConfigure heroInfo;

	private SkinStandingPaintingConfigureItem skinConfigItem;

	private int mustHeroID;

	private int mustMaterialTypeIndex;

	private int mustOperateSelectIndex;

	private int GuideHeroID = 108;

	private SwipeGesture _swipeGesture;

	private WayType currentWayType;

	private Guide200200Source _guide200200Source;

	private const int _maxRelationLv = 5;

	private const int GIFTTABINDEX = 1;

	private const int EXPRESSIONTABINDEX = 3;

	private SkinStandingPaintingConfigureItem _CurStandingPaintingItem;

	private List<SkinStandingPaintingConfigureItem> _StandingPaintingData;

	private HeroCardData _HeroCardData;

	private uint _breakThroughVoiceId;

	private readonly List<int> favorGifts = new List<int>();

	private readonly List<ItemInfoConfigure> giftConfigures = new List<ItemInfoConfigure>();

	private int curSelectCount;

	private ItemInfoConfigure curItemConfig;

	private readonly List<MapField<int, int>> levelRewards = new List<MapField<int, int>>();

	private readonly List<KeyValuePair<int, int>> breakthroughs = new List<KeyValuePair<int, int>>();

	private readonly List<KeyValuePair<int, int>> breakthroughRewards = new List<KeyValuePair<int, int>>();

	private bool _GiftUpdateLock;

	private UniTaskCompletionSource favorChangeTsc;

	private bool isSuperBadge;

	private SkinStandingPaintingConfigureItem _breakThroughStandingPaintingConfigureItem;

	private UniTaskCompletionSource breakThroughTsc;

	private int UnlockSkinCount;

	private UIHero_Button_SkinItem selectedItem;

	private List<ExpressionData> expressionData;

	private int UnlockExpressionCount;

	private List<PveExpItemData> PVEExpItems;

	private float _currentRefreshHeroTime;

	private const int SortDefault = 0;

	private const int SortFavor = 1;

	private const int SortPve = 2;

	private const int SortOrderAsc = 0;

	private const int SortOrderDesc = 1;

	private static readonly string[] HeroSortLabels = new string[3]
	{
		1070001.GetLocal(UIStringType.GUI),
		1070002.GetLocal(UIStringType.GUI),
		1070003.GetLocal(UIStringType.GUI)
	};

	private List<HeroCardData> heroCards;

	private int UnlockCharacterCount;

	private PVENurturanceBreakConfigure _pveNurturanceBreakConfigure;

	private readonly List<KeyValuePair<int, int>> _pveTalentMaterials = new List<KeyValuePair<int, int>>();

	public HeroPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIHeroPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_swipeGesture = new SwipeGesture(base.ui.loader_Skin_Image);
		if (objs != null && objs.Length != 0)
		{
			if (objs[0] is RepeatedField<int> repeatedField)
			{
				mustHeroID = repeatedField[0];
			}
			else
			{
				mustHeroID = (int)objs[0];
			}
			if (objs.Length > 1)
			{
				WayType num = (WayType)objs[1];
				currentWayType = (WayType)objs[1];
				if (num == WayType.BondForged)
				{
					mustMaterialTypeIndex = 1;
					mustOperateSelectIndex = 2;
				}
				else
				{
					mustMaterialTypeIndex = 0;
					mustOperateSelectIndex = 0;
				}
			}
			else
			{
				mustMaterialTypeIndex = 0;
				mustOperateSelectIndex = 0;
			}
		}
		ReadyHeroInfo();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RefreshHero();
		base.ui.loader_Effect.visible = false;
		if (base.ui.tab.selectedIndex == 0)
		{
			Refresh_Selector();
		}
		else
		{
			Refresh_Detail(mustMaterialTypeIndex, mustOperateSelectIndex, 0);
			mustMaterialTypeIndex = 0;
			mustOperateSelectIndex = 0;
		}
		PvEGuide();
	}

	private void RefreshHero()
	{
		if (mustHeroID == 0)
		{
			base.ui.tab.selectedIndex = 0;
			return;
		}
		ReadyHeroDetailInfo(mustHeroID);
		GuideHeroID = mustHeroID;
		mustHeroID = 0;
		base.ui.tab.selectedIndex = 1;
		RefreshNickName();
		RefreshRelationLv();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponents_Selector();
		InitComponents_Detail();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		AddEvent_Selector();
		AddEvent_Detail();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		RemoveEvent_Selector();
		RemoveEvent_Detail();
	}

	protected override void AddListener()
	{
		base.AddListener();
		AddListener_Detail();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		RemoveListener_Detail();
	}

	public override void Close()
	{
		Close_Detail();
		base.Close();
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
		if (_swipeGesture != null)
		{
			_swipeGesture.Dispose();
			_swipeGesture = null;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		if (base.ui.tab.selectedIndex == 0 || heroCards == null || heroCards.Count == 0)
		{
			mustHeroID = 0;
			await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		}
		else
		{
			base.ui.com_HeroList.list_Hero.scrollPane.touchEffect = true;
			base.ui.com_HeroList.list_Hero.numItems = heroCards.Count;
			base.ui.com_HeroList.list_Hero.selectedIndex = CurrentHeroIndex();
			base.ui.com_HeroList.list_Hero.onClickItem.Call();
			base.ui.tab.selectedIndex = 0;
			Close_Detail();
		}
		base.ui.btn_Return.onClick.Release();
	}

	private async void OpenSkinPreview()
	{
		if (_CurStandingPaintingItem == null)
		{
			return;
		}
		base.ui.btn_Preview.onClick.Retain();
		foreach (KeyValuePair<int, int> breakthroughReward in breakthroughRewards)
		{
			ItemInfoConfigure itemInfoConfigure = breakthroughReward.Key.GetItemInfoConfigure();
			if (itemInfoConfigure.ItemType == ItemType.AccountBackground)
			{
				await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(skinConfigItem.ItemID, itemInfoConfigure.Id);
				break;
			}
		}
		base.ui.btn_Preview.onClick.Release();
	}

	private void RefreshHeroBaseInfo()
	{
		RefreshNickName();
		RefreshRelationLv();
		RefreshSkin(_CurStandingPaintingItem);
	}

	private void RefreshNickName()
	{
		base.ui.txt_Name.text = heroInfo.NameID.GetLocal(UIStringType.Character);
		base.ui.txt_Nick.text = heroInfo.NickID.GetLocal(UIStringType.Character);
	}

	private void UpdateCollectStatus(HeroCardData heroCard)
	{
		GButton btn_Collect = base.ui.com_File.btn_Collect;
		UIButton_Collect btn = btn_Collect as UIButton_Collect;
		if (btn == null)
		{
			return;
		}
		GTextField txt_CollectTip = base.ui.com_File.txt_CollectTip;
		bool visible = (btn.visible = heroCard.heroStatus == HeroStatus.Activate);
		txt_CollectTip.visible = visible;
		btn.collected.selectedIndex = (heroCard.CollectStatus ? 1 : 0);
		UpdateCollectTipColor(heroCard.CollectStatus);
		base.ui.com_File.btn_Collect.onClick.Set((EventCallback0)delegate
		{
			btn.collected.selectedIndex = ((!heroCard.CollectStatus) ? 1 : 0);
			UpdateCollectTipColor(!heroCard.CollectStatus);
			SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestRoleCardCollectC2S(heroCard.HeroId, heroCard.CollectStatus).OnFinished.AddListener(delegate(RPCAsyncResult result)
			{
				btn.collected.selectedIndex = (heroCard.CollectStatus ? 1 : 0);
				if (result.errId == 0)
				{
					GList list_Hero = base.ui.com_HeroList.list_Hero;
					for (int i = 0; i < list_Hero._children.Count; i++)
					{
						if (list_Hero._children[i] is UIHero_Button_Hero uIHero_Button_Hero && uIHero_Button_Hero.HeroId == heroCard.HeroId)
						{
							uIHero_Button_Hero.UpdateCollectStatus(heroCard.CollectStatus);
						}
					}
					SortHeroCards(LocalCache.GetHeroSortCache(), LocalCache.GetHeroSortOrderCache());
					base.ui.com_HeroList.list_Hero.numItems = heroCards.Count;
				}
			});
		});
	}

	private void UpdateCollectTipColor(bool collectStatus)
	{
		if (collectStatus)
		{
			if (ColorUtility.TryParseHtmlString("#FF1796", out var color))
			{
				base.ui.com_File.txt_CollectTip.color = color;
			}
		}
		else
		{
			base.ui.com_File.txt_CollectTip.color = Color.white;
		}
	}

	private void RefreshSkin(SkinStandingPaintingConfigureItem _SkinConfigItem, int labelItemId = 0)
	{
		skinConfigItem = _SkinConfigItem;
		(string, bool) character = skinConfigItem.GetCharacter();
		base.ui.btn_Preview.visible = !string.IsNullOrEmpty(skinConfigItem.PreviewVideo);
		Vector2 offset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
		{
			offset = value.skinOffset;
		}
		RendererSkin(character.Item1, character.Item2, offset, Vector2.one);
		base.ui.btn_Preview.onClick.Set((EventCallback0)delegate
		{
			base.ui.btn_Preview.onClick.Retain();
			mustHeroID = heroInfo.Id;
			mustMaterialTypeIndex = base.ui.MaterialType.selectedIndex;
			mustOperateSelectIndex = base.ui.com_Gift.operateSelect.selectedIndex;
			SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(skinConfigItem.ItemID, labelItemId).Forget();
			base.ui.btn_Preview.onClick.Release();
		});
	}

	public async void RendererSkin(string _URL, bool isVideo, Vector2 _offset, Vector2 _scale)
	{
		if (isVideo)
		{
			base.ui.standingPaintType.selectedIndex = 1;
			if (!((string)base.ui.loader_Skin_Video.data == _URL))
			{
				base.ui.loader_Skin_Video.data = _URL;
				CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, base.ui.loader_Skin_Video);
				await SimpleSingletonProvider<CriMovieManager>.inst.Play(_URL, base.ui.loader_Skin_Video);
			}
			return;
		}
		base.ui.standingPaintType.selectedIndex = 0;
		base.ui.loader_Skin_Video.data = "";
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.loader_Skin_Video);
		if (!(base.ui.loader_Skin_Image.url == _URL))
		{
			base.ui.loader_Skin_Image.customOffset = _offset;
			base.ui.loader_Skin_Image.customScale = _scale;
			base.ui.loader_Skin_Image.url = _URL;
		}
	}

	private void RefreshRelationLv()
	{
		if (!heroInfo.HasKizuna)
		{
			base.ui.slider_Favor.barStatus.selectedIndex = 1;
			UIHero_bar_FavorValue slider_Favor = base.ui.slider_Favor;
			double max = (base.ui.slider_Favor.value = 100.0);
			slider_Favor.max = max;
			for (int i = 0; i < 5; i++)
			{
				GProgressBar gProgressBar = GeProgressComByLevel(base.ui.group_Progress, i);
				max = (gProgressBar.value = 100.0);
				gProgressBar.max = max;
			}
			return;
		}
		HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
		FavorLevelConfigure favorLevelConfigure = cardData.LV.GetFavorLevelConfigure();
		base.ui.slider_Favor.barStatus.selectedIndex = ((5 == cardData.LV) ? 1 : 0);
		if (cardData.LV < 5)
		{
			base.ui.slider_Favor.max = favorLevelConfigure.NeedExp;
			base.ui.slider_Favor.value = cardData.Exp;
		}
		else
		{
			base.ui.slider_Favor.barStatus.selectedIndex = 1;
			UIHero_bar_FavorValue slider_Favor2 = base.ui.slider_Favor;
			double max = (base.ui.slider_Favor.value = 100.0);
			slider_Favor2.max = max;
		}
		for (int j = 0; j < 5; j++)
		{
			FavorLevelConfigure favorLevelConfigure2 = StaticConfigure.Favor.Levels[j];
			GProgressBar gProgressBar2 = GeProgressComByLevel(base.ui.group_Progress, j);
			gProgressBar2.max = favorLevelConfigure2.NeedExp;
			if (favorLevelConfigure2.Id == cardData.LV)
			{
				gProgressBar2.value = cardData.Exp;
			}
			else if (favorLevelConfigure2.Id < cardData.LV)
			{
				gProgressBar2.value = favorLevelConfigure2.NeedExp;
			}
			else
			{
				gProgressBar2.value = 0.0;
			}
		}
	}

	private GProgressBar GeProgressComByLevel(UIHero_Com_Progress group_Progress, int lv)
	{
		return lv switch
		{
			0 => group_Progress.progress_Heart_0, 
			1 => group_Progress.progress_Heart_1, 
			2 => group_Progress.progress_Heart_2, 
			3 => group_Progress.progress_Heart_3, 
			4 => group_Progress.progress_Heart_4, 
			_ => group_Progress.progress_Heart_0, 
		};
	}

	private async void PvEGuide()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.guide.GuideStatus(GuideType.PveNurturanceGuide))
		{
			TriggerGuide();
		}
		else
		{
			if (base.ui.tab.selectedIndex != 0 && currentWayType != WayType.CharacterProfile)
			{
				return;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(78101) <= 0)
			{
				TriggerGuide();
			}
			else
			{
				if (GuideHeroID != 108)
				{
					return;
				}
				if (heroCards != null && heroCards.Count > 0)
				{
					foreach (HeroCardData heroCard in heroCards)
					{
						if (heroCard.IsHas)
						{
							int num = heroCard.PveData?.Exp ?? 0;
							int num2 = heroCard.PveData?.Level ?? 0;
							if (num > 0 || num2 >= 1)
							{
								SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(24, 1);
								TriggerGuide();
								return;
							}
						}
					}
				}
				await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
				SimpleSingletonProvider<GameLogicManager>.inst.guide.TriggerGuide(200150);
				if (currentWayType == WayType.CharacterProfile)
				{
					await UniTask.Yield();
					SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
				}
			}
		}
	}

	private async void TriggerGuide()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.guide.GuideStatus(GuideType.HeroFavor) || base.ui.tab.selectedIndex != 0)
		{
			return;
		}
		List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.Gift);
		if (itemsByType == null || itemsByType.Count == 0)
		{
			return;
		}
		for (int i = 0; i < heroCards.Count; i++)
		{
			HeroCardData heroCardData = heroCards[i];
			if (heroCardData.LV != 0 || heroCardData.Exp != 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(23, 1);
				return;
			}
		}
		_guide200200Source = Guide200200Source.Manual;
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.TriggerGuide(200200);
	}

	public async void OnGuide200200Step1()
	{
		if (_guide200200Source == Guide200200Source.Manual)
		{
			_guide200200Source = Guide200200Source.Auto;
			GuideSelectHero();
			return;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.guide.GuideStatus(GuideType.HeroFavor))
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
			return;
		}
		List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.Gift);
		if (itemsByType == null || itemsByType.Count == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
			return;
		}
		for (int i = 0; i < heroCards.Count; i++)
		{
			HeroCardData heroCardData = heroCards[i];
			if (heroCardData.LV != 0 || heroCardData.Exp != 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(23, 1);
				SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
				return;
			}
		}
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
		SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
	}

	public async void GuideSelectHero()
	{
		base.ui.com_HeroList.list_Hero.scrollPane.touchEffect = false;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cutin.playing, SimpleSingletonProvider<UIManager>.inst.guide.Hide);
		if (base.ui.com_MaskHero.onStage)
		{
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_MaskHero, needTransparentMask: false, isRect: true);
			SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_MaskHero, base.ui.com_MaskHero.width / 3f, base.ui.com_MaskHero.height);
		}
	}

	private void ReadyHeroDetailInfo(int heroId)
	{
		heroInfo = CharacterHandle.GetHeroCharacterConfigure(heroId);
		int standingPainting = heroInfo.StandingPainting;
		_StandingPaintingData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetStandingPaintingsById(standingPainting);
		_CurStandingPaintingItem = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCurStandingPainting(heroInfo.Id);
		_HeroCardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
		base.ui.btn_GiftTab.grayed = !heroInfo.HasKizuna;
		base.ui.btn_GiftTab.touchable = heroInfo.HasKizuna;
		_HeroCardData.PveData.talentChanged.AddListener(RefreshPveTalentStatus);
		InitPveTalent();
	}

	private void InitComponents_Detail()
	{
		base.ui.com_Gift.list_Gifts.itemProvider = ProviderGiftsItem;
		base.ui.com_Gift.list_Gifts.itemRenderer = RendererGifts;
		base.ui.com_Gift.list_Relation.itemRenderer = RendererRelation;
		base.ui.com_Gift.list_BreakThrough.itemRenderer = RendererBreakThrough;
		base.ui.com_Gift.list_BreakThroughReward.itemRenderer = RendererBreakThroughReward;
		base.ui.com_Skin.com_StandingPaintingList.list_StandingPainting.itemRenderer = RendererStandingPainting;
		base.ui.com_Emoji.list_Expression.itemRenderer = RendererExpression;
		base.ui.com_File.com_FileUpgrade.list_Prop.itemRenderer = RendererPVEExpItems;
		base.ui.com_File.com_FileBreakThough.list_talentMaterials.itemRenderer = RendererTalentMaterials;
	}

	private void Refresh_Detail(int _MaterialTypeIndex, int _operateSelectIndex, int _skillSelectIndex)
	{
		if (base.ui.btn_GiftTab.grayed && _MaterialTypeIndex == 1)
		{
			_MaterialTypeIndex = 0;
			_operateSelectIndex = 0;
		}
		base.ui.MaterialType.selectedIndex = _MaterialTypeIndex;
		base.ui.com_Gift.operateSelect.selectedIndex = _operateSelectIndex;
		base.ui.com_File.tab.selectedIndex = _skillSelectIndex;
		base.ui.MaterialType.onChanged.Call();
		base.ui.com_File.tab.onChanged.Call();
	}

	private void AddEvent_Detail()
	{
		if (_swipeGesture != null)
		{
			_swipeGesture.onAction.Add(OnSwipeGestureSwitchHero);
		}
		base.ui.MaterialType.onChanged.Add(ChangeMaterial);
		base.ui.com_Gift.list_Gifts.onClickItem.Add(SelectGift);
		base.ui.com_Gift.btn_giveGift.onClick.Add(OnRequestChangeFavorValue);
		base.ui.com_Gift.btn_addNum.onClick.Add(OnAddOne);
		base.ui.com_Gift.btn_delNum.onClick.Add(OnDelOne);
		base.ui.com_Gift.btn_Max.onClick.Add(OnSetMaxSelect);
		base.ui.com_Gift.btn_Min.onClick.Add(OnSetMinSelect);
		base.ui.com_Gift.btn_breakThrough.onClick.Add(OnBreakThrough);
		base.ui.com_Gift.operateSelect.onChanged.Add(OperateChange);
		base.ui.com_Gift.btn_Switch.onClick.Add(SwitchBreakthroughProp);
		base.ui.com_File.com_FileSkill.btn_Switch.onClick.Add(ShowSkill);
		base.ui.com_Gift.btn_showBreakThrough.onClick.Add(OnClickShowBreakThrough);
		base.ui.com_File.com_FileUpgrade.btn_Upgrade.onClick.Add(OnPveUpgrade);
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.onClick.Add(AutoUpgrade);
		base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.onClick.Add(CancelUpgrade);
		base.ui.com_File.com_FileBreakThough.btn_talentBreak.onClick.Add(RequestTalentBreakThrough);
		base.ui.com_File.tab.onChanged.Add(OnTabChanged);
		base.ui.btn_prePage.onClick.Add(ShowPrePage);
		base.ui.btn_nextPage.onClick.Add(ShowNextPage);
	}

	private void RemoveEvent_Detail()
	{
		if (_swipeGesture != null)
		{
			_swipeGesture.onAction.Remove(OnSwipeGestureSwitchHero);
		}
		base.ui.MaterialType.onChanged.Remove(ChangeMaterial);
		base.ui.com_Gift.list_Gifts.onClickItem.Remove(SelectGift);
		base.ui.com_Gift.btn_giveGift.onClick.Remove(OnRequestChangeFavorValue);
		base.ui.com_Gift.btn_addNum.onClick.Remove(OnAddOne);
		base.ui.com_Gift.btn_delNum.onClick.Remove(OnDelOne);
		base.ui.com_Gift.btn_Max.onClick.Remove(OnSetMaxSelect);
		base.ui.com_Gift.btn_Min.onClick.Remove(OnSetMinSelect);
		base.ui.com_Gift.btn_breakThrough.onClick.Remove(OnBreakThrough);
		base.ui.com_Gift.operateSelect.onChanged.Remove(OperateChange);
		base.ui.com_Gift.btn_Switch.onClick.Remove(SwitchBreakthroughProp);
		base.ui.com_File.com_FileSkill.btn_Switch.onClick.Remove(ShowSkill);
		base.ui.com_Gift.btn_showBreakThrough.onClick.Remove(OnClickShowBreakThrough);
		base.ui.com_File.com_FileUpgrade.btn_Upgrade.onClick.Remove(OnPveUpgrade);
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.onClick.Remove(AutoUpgrade);
		base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.onClick.Remove(CancelUpgrade);
		base.ui.com_File.com_FileBreakThough.btn_talentBreak.onClick.Remove(RequestTalentBreakThrough);
		base.ui.com_File.tab.onChanged.Remove(OnTabChanged);
		base.ui.btn_prePage.onClick.Remove(ShowPrePage);
		base.ui.btn_nextPage.onClick.Remove(ShowNextPage);
	}

	private void AddListener_Detail()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshGiftFromBag);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshTalentMaterials);
	}

	private void RemoveListener_Detail()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshGiftFromBag);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshTalentMaterials);
	}

	private void Close_Detail()
	{
		_HeroCardData?.PveData.talentChanged.RemoveListener(RefreshPveTalentStatus);
		UnLoadExpression();
		CancelUpgrade();
		CancelLongPress();
		_pveTalentMaterials.Clear();
		base.ui.com_File.com_FileUpgrade.progress_Exp.maxStatus.selectedIndex = 0;
		base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex = 0;
		CloseBreakThrough();
	}

	private void ChangeMaterial(EventContext context)
	{
		switch (base.ui.MaterialType.selectedIndex)
		{
		case 0:
			RefreshHeroFile();
			break;
		case 1:
			if (heroInfo.HasKizuna)
			{
				base.ui.com_Gift.operateSelect.onChanged.Call();
				if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
				}
			}
			break;
		case 2:
			RefreshSkin();
			break;
		case 3:
			RefreshEmoji().Forget();
			break;
		}
	}

	private void OperateChange(EventContext context)
	{
		base.ui.com_Gift.operateSelect.onChanged.Retain();
		_HeroCardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
		if (base.ui.com_Gift.operateSelect.selectedIndex == 0)
		{
			RefreshSkin(_CurStandingPaintingItem);
			UpdateRelationData();
		}
		else if (base.ui.com_Gift.operateSelect.selectedIndex == 1)
		{
			RefreshSkin(_CurStandingPaintingItem);
			UpdateGiftData();
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
		}
		else if (base.ui.com_Gift.operateSelect.selectedIndex == 2)
		{
			UpdateBreakThroughData();
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
		}
		base.ui.com_Gift.operateSelect.onChanged.Release();
	}

	private void RefreshHeroFile()
	{
		if (heroInfo == null)
		{
			return;
		}
		_HeroCardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
		UpdateCollectStatus(_HeroCardData);
		base.ui.com_File.txt_CharaterName.text = heroInfo.NameID.GetLocal(UIStringType.Character);
		base.ui.com_File.txt_CharaterNick.text = heroInfo.NickID.GetLocal(UIStringType.Character);
		base.ui.com_File.com_FileSkill.txt_ATK.text = heroInfo.Attack.ToString();
		base.ui.com_File.com_FileSkill.txt_DEF.text = heroInfo.Defense.ToString();
		base.ui.com_File.com_FileSkill.txt_HP.text = heroInfo.Blood.ToString();
		base.ui.com_File.com_FileSkill.btn_Switch.visible = GetSkillSwitchButtonStatus(heroInfo);
		base.ui.com_File.com_FileSkill.btn_Switch.SwitchPVE.SetHook("Finish", delegate
		{
			base.ui.com_File.com_FileSkill.btn_Switch.onClick.Release();
		});
		base.ui.com_File.com_FileSkill.btn_Switch.SwitchPVP.SetHook("Finish", delegate
		{
			base.ui.com_File.com_FileSkill.btn_Switch.onClick.Release();
		});
		base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex = 0;
		base.ui.com_File.com_FileSkill.Talent.selectedIndex = ((_HeroCardData.PveData.Talent.Count > 0) ? 1 : 0);
		base.ui.com_File.com_FileSkill.com_Level.txt_level.text = _HeroCardData.PveData.Level.ToString();
		ShowSkillInfo(heroInfo.PveActiveSkill, heroInfo.PvePassiveSkills);
		RefreshSkin(_CurStandingPaintingItem);
		RefreshPveExpInfo();
		RefreshPveTalentStatus();
		if (_HeroCardData.PveData.CanTalentConfig())
		{
			base.ui.com_File.hasTalent.selectedIndex = 1;
		}
		else
		{
			base.ui.com_File.hasTalent.selectedIndex = 0;
			if (base.ui.com_File.tab.selectedIndex == 2)
			{
				base.ui.com_File.tab.selectedIndex = 0;
			}
		}
		base.ui.com_File.btn_Skill.loader_icon.url = heroInfo.CharacterMap;
		base.ui.com_File.btn_Skill.tab.selectedIndex = 0;
		base.ui.com_File.btn_Upgrade.tab.selectedIndex = 1;
		base.ui.com_File.btn_Talent.tab.selectedIndex = 2;
		base.ui.com_File.btn_Archive.tab.selectedIndex = 3;
		RefreshHeroInfoArchive();
	}

	private void RefreshHeroInfoArchive()
	{
		base.ui.com_File.com_InfoArchive.txt_CharacterName.text = heroInfo.NameID.GetLocal(UIStringType.Character);
		base.ui.com_File.com_InfoArchive.txt_NickName.text = heroInfo.NickID.GetLocal(UIStringType.Character);
		CommonUIManager.RefreshHyperlinkDesc(heroInfo.BiographyID.GetLocal(UIStringType.Character), base.ui.com_File.com_InfoArchive.com_Desc.title);
	}

	private void ShowSkill()
	{
		base.ui.com_File.com_FileSkill.btn_Switch.onClick.Retain();
		if (base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex == 0)
		{
			base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex = 1;
			ShowSkillInfo(heroInfo.ActiveSkill, heroInfo.PassiveSkills);
		}
		else if (base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex == 1)
		{
			base.ui.com_File.com_FileSkill.btn_Switch.GameMode.selectedIndex = 0;
			ShowSkillInfo(heroInfo.PveActiveSkill, heroInfo.PvePassiveSkills);
		}
	}

	private void ShowSkillInfo(int ActiveSkill, RepeatedField<int> PassiveSkills)
	{
		List<SkillInfoConfigure> skillInfos = UIHelper.GetSkillConfigs(ActiveSkill, PassiveSkills);
		base.ui.com_File.com_FileSkill.list_Skills.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIHero_Com_Skill uIHero_Com_Skill && index >= 0 && index <= skillInfos.Count - 1)
			{
				uIHero_Com_Skill.skillType.selectedIndex = ((skillInfos[index].SkillType != SkillType.Active) ? 1 : 0);
				if (skillInfos[index].SkillType == SkillType.Active)
				{
					uIHero_Com_Skill.txt_CD.text = skillInfos[index].Round.ToString();
				}
				uIHero_Com_Skill.txt_SkillName.SetVar("skillName", skillInfos[index].NameID.GetLocal(UIStringType.Skill)).FlushVars();
				skillInfos[index].DescID.RefreshCharacterSkillHyperlinkDesc(uIHero_Com_Skill.txt_SkillDesc);
			}
		};
		base.ui.com_File.com_FileSkill.list_Skills.numItems = 0;
		base.ui.com_File.com_FileSkill.list_Skills.numItems = skillInfos.Count;
	}

	private void RefreshGiftFromBag()
	{
		if (base.ui.tab.selectedIndex != 0)
		{
			UpdatePveDropItem();
			if (_GiftUpdateLock)
			{
				UpdateGiftData();
			}
		}
	}

	private void UpdateGiftData()
	{
		_GiftUpdateLock = false;
		curItemConfig = null;
		GButton btn_giveGift = base.ui.com_Gift.btn_giveGift;
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		btn_giveGift.touchable = levels[levels.Count - 1].Id != _HeroCardData.LV;
		base.ui.com_Gift.btn_giveGift.grayed = !base.ui.com_Gift.btn_giveGift.touchable;
		base.ui.com_Gift.btn_giveGift.visible = _HeroCardData.IsHas;
		favorGifts.Clear();
		foreach (CharacterHeroFavorGiftConfigureItem characterHeroFavorGiftConfigureItem in CharacterHandle.GetCharacterHeroFavorGiftConfigure(heroInfo.Id).CharacterHeroFavorGiftConfigureItems)
		{
			favorGifts.Add(characterHeroFavorGiftConfigureItem.ItemID);
		}
		List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.Gift);
		giftConfigures.Clear();
		if (itemsByType != null && itemsByType.Count != 0)
		{
			for (int i = 0; i < itemsByType.Count; i++)
			{
				giftConfigures.Add(itemsByType[i].GetItemInfoConfigure());
			}
			giftConfigures.Sort(GiftsCompare);
		}
		base.ui.com_Gift.list_Gifts.numItems = giftConfigures.Count + 1;
		base.ui.com_Gift.selectGift.selectedIndex = 0;
		RefreshSelectGroup(1);
		PreRefreshRelationLv(0L);
		base.ui.com_Gift.btn_giveGift.onClick.Release();
	}

	private int GiftsCompare(ItemInfoConfigure x, ItemInfoConfigure y)
	{
		bool flag = favorGifts.Contains(x.Id);
		bool flag2 = favorGifts.Contains(y.Id);
		if (flag == flag2)
		{
			return x.QualityType.CompareTo(y.QualityType);
		}
		return -flag.CompareTo(flag2);
	}

	private void RendererGifts(int index, GObject item)
	{
		UIHero_Button_GiftItem btn = item as UIHero_Button_GiftItem;
		if (btn != null)
		{
			btn.visible = false;
			((UICom_LitItem)btn.com_Item).Cut_in.Play(1, 0.01f * (float)index, delegate
			{
				btn.visible = true;
			}, null);
			ItemInfoConfigure itemConfig = giftConfigures[index - 1];
			RefreshGiftItem(btn, itemConfig);
			item.visible = true;
		}
	}

	private void RefreshGiftItem(UIHero_Button_GiftItem giftItem, ItemInfoConfigure itemConfig, bool showCount = true)
	{
		if (giftItem.com_Item is UICom_LitItem uICom_LitItem)
		{
			uICom_LitItem.qualityType.selectedIndex = (int)itemConfig.QualityType;
			uICom_LitItem.loader_Icon.url = itemConfig.ShowIcon;
			uICom_LitItem.isShowNum.selectedIndex = ((!showCount) ? 1 : 0);
			uICom_LitItem.txt_itemNum.text = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemConfig.Id).ToString();
			giftItem.like.selectedIndex = (favorGifts.Contains(itemConfig.Id) ? 1 : 0);
			giftItem.selected = false;
			if (itemConfig.Id == StaticGlobalData.SPECIAL_ITEM_SUPER_GIFT)
			{
				HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
				uICom_LitItem.grayed = cardData.LV >= 5;
			}
			else
			{
				uICom_LitItem.grayed = false;
			}
		}
	}

	private void SelectGift(EventContext context)
	{
		if (base.ui.com_Gift.list_Gifts.selectedIndex == 0)
		{
			base.ui.com_Gift.list_Gifts.onClickItem.Retain();
			curItemConfig = null;
			base.ui.com_Gift.selectGift.selectedIndex = 0;
			RefreshSelectGroup(0);
			mustHeroID = heroInfo.Id;
			mustMaterialTypeIndex = base.ui.MaterialType.selectedIndex;
			mustOperateSelectIndex = base.ui.com_Gift.operateSelect.selectedIndex;
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowGiftWayWin();
			base.ui.com_Gift.list_Gifts.onClickItem.Release();
			return;
		}
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id != _HeroCardData.LV)
		{
			base.ui.com_Gift.list_Gifts.onClickItem.Retain();
			curItemConfig = giftConfigures[base.ui.com_Gift.list_Gifts.selectedIndex - 1];
			RefreshSelectGroup(1);
			RefreshGiftItem(base.ui.com_Gift.com_SelectedGift, curItemConfig, showCount: false);
			base.ui.com_Gift.selectGift.selectedIndex = 1;
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			base.ui.com_Gift.list_Gifts.onClickItem.Release();
		}
	}

	private void PreRefreshRelationLv(long changeExp)
	{
		HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
		FavorLevelConfigure favorLevelConfigure = cardData.LV.GetFavorLevelConfigure();
		string value = ((changeExp > 0) ? "[color=#00ff00]" : "[color=#ffffff]");
		long num = Math.Max(0L, cardData.Exp + changeExp);
		int num2 = Math.Max(0, favorLevelConfigure.NeedExp);
		base.ui.com_Gift.txt_CurFavorValue.SetVar("color", value).SetVar("cur", num.ToString()).SetVar("max", favorLevelConfigure.NeedExp.ToString())
			.FlushVars();
		if (cardData.LV >= 5)
		{
			SetProgressSafe(base.ui.com_Gift.progress_ReleasePowerUp, num2, num, fillWhenZeroMax: true);
		}
		else
		{
			SetProgressSafe(base.ui.com_Gift.progress_ReleasePowerUp, 1L, 0L);
		}
		long num3 = num - num2;
		int count = StaticConfigure.Favor.Levels.Count;
		for (int i = 0; i < count; i++)
		{
			FavorLevelConfigure favorLevelConfigure2 = StaticConfigure.Favor.Levels[i];
			if (i < 5)
			{
				GProgressBar progressBar = GeProgressComByLevel(base.ui.com_Gift.group_Progress, i);
				int num4 = Math.Max(0, favorLevelConfigure2.NeedExp);
				long value2;
				if (favorLevelConfigure2.Id == cardData.LV)
				{
					value2 = ((num3 > 0) ? num4 : num);
				}
				else if (favorLevelConfigure2.Id < cardData.LV)
				{
					value2 = num4;
				}
				else if (num3 >= 0)
				{
					value2 = Math.Min(num3, num4);
					num3 -= num4;
				}
				else
				{
					value2 = 0L;
				}
				SetProgressSafe(progressBar, num4, value2, favorLevelConfigure2.Id <= cardData.LV);
			}
		}
		bool flag = cardData.LV >= 5 || (cardData.LV < 5 && num3 > 0);
		base.ui.com_Gift.releationPowerUp.selectedIndex = (flag ? 1 : 0);
		if (flag)
		{
			base.ui.com_Gift.image_CN.visible = GameSettings.languageType == LanguageType.SimplifiedChinese;
			base.ui.com_Gift.image_EN.visible = !base.ui.com_Gift.image_CN.visible;
		}
	}

	private void SetProgressSafe(GProgressBar progressBar, long max, long value, bool fillWhenZeroMax = false)
	{
		long num = Math.Max(1L, max);
		progressBar.max = num;
		if (max <= 0)
		{
			progressBar.value = (fillWhenZeroMax ? num : 0);
		}
		else
		{
			progressBar.value = Math.Min(Math.Max(value, 0L), max);
		}
	}

	private async void OnRequestChangeFavorValue(EventContext context)
	{
		if (curItemConfig == null)
		{
			return;
		}
		if (curItemConfig.Id == StaticGlobalData.SPECIAL_ITEM_SUPER_GIFT)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id).LV < 5)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1055.GetLocal(UIStringType.Message), curItemConfig.NameID.GetLocal(UIStringType.Item)), delegate
				{
					RequestUpLv(heroInfo.Id, curItemConfig.Id, curSelectCount);
				});
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1128);
			}
		}
		else
		{
			RequestUpLv(heroInfo.Id, curItemConfig.Id, curSelectCount);
		}
	}

	private void RequestUpLv(int heroId, int itemId, int Count)
	{
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curItemConfig.Id);
		if (itemCount < curSelectCount)
		{
			return;
		}
		int resideNum = itemCount - curSelectCount;
		GObject childAt = base.ui.com_Gift.list_Gifts.GetChildAt(base.ui.com_Gift.list_Gifts.selectedIndex);
		if (resideNum == 0)
		{
			childAt.visible = false;
			_GiftUpdateLock = true;
		}
		else if (childAt is UIHero_Button_GiftItem { com_Item: UICom_LitItem com_Item })
		{
			com_Item.txt_itemNum.text = resideNum.ToString();
		}
		base.ui.com_Gift.btn_giveGift.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestRoleCardUpLvC2S(heroId, itemId, Count).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(23, 1);
			}
			if (resideNum > 0)
			{
				if (curItemConfig != null && curItemConfig.Id == StaticGlobalData.SPECIAL_ITEM_SUPER_GIFT)
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id).LV < 5)
					{
						RefreshSelectGroup(1);
					}
					else
					{
						ItemInfoConfigure safeByIndex = giftConfigures.GetSafeByIndex(0);
						if (safeByIndex != null && safeByIndex.Id != curItemConfig.Id)
						{
							base.ui.com_Gift.list_Gifts.selectedIndex = 1;
							base.ui.com_Gift.list_Gifts.onClickItem.Call();
						}
					}
				}
				else
				{
					RefreshSelectGroup(1);
				}
			}
			if (_.errId != 0)
			{
				UpdateGiftData();
				base.ui.com_Gift.btn_giveGift.onClick.Release();
			}
		});
	}

	public async UniTask HeroCardFavorUp(bool lvUp)
	{
		CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, base.ui.loader_Effect);
		if (lvUp)
		{
			base.ui.touchableAll = false;
			favorChangeTsc = new UniTaskCompletionSource();
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(152.GetVideoKey(), base.ui.loader_Effect, null, FinishShowFavorChange);
			Stage.inst.PlayOneShotSound(27);
			base.ui.HeartLvUP.Play();
			await favorChangeTsc.Task;
			base.ui.loader_Effect.visible = false;
			base.ui.touchableAll = true;
		}
		else
		{
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(151.GetVideoKey(), base.ui.loader_Effect, null, delegate
			{
				SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.loader_Effect);
				base.ui.loader_Effect.visible = false;
			});
			Stage.inst.PlayOneShotSound(27);
			base.ui.Heart.Play();
			RefreshRelationLv();
			base.ui.com_Gift.btn_giveGift.onClick.Release();
		}
	}

	private void FinishShowFavorChange(Player source, int status)
	{
		RefreshRelationLv();
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id == _HeroCardData.LV)
		{
			base.ui.com_Gift.operateSelect.selectedIndex = 0;
			base.ui.com_Gift.list_Relation.scrollPane.ScrollBottom();
		}
		if (5 == _HeroCardData.LV && !_HeroCardData.isBreakThrough)
		{
			base.ui.com_Gift.operateSelect.selectedIndex = 2;
		}
		base.ui.com_Gift.btn_giveGift.onClick.Release();
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.loader_Effect);
		favorChangeTsc.TrySetResult();
	}

	private void OnAddOne()
	{
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id != _HeroCardData.LV && curItemConfig != null)
		{
			base.ui.com_Gift.btn_addNum.onClick.Retain();
			RefreshSelectGroup(Mathf.Min(curSelectCount + 1, GetCurMaxPurchaseNum()));
			base.ui.com_Gift.btn_addNum.onClick.Release();
		}
	}

	private void OnDelOne()
	{
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id != _HeroCardData.LV && curItemConfig != null)
		{
			base.ui.com_Gift.btn_delNum.onClick.Retain();
			RefreshSelectGroup(Mathf.Max(curSelectCount - 1, 1));
			base.ui.com_Gift.btn_delNum.onClick.Release();
		}
	}

	private void OnSetMaxSelect(EventContext context)
	{
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id != _HeroCardData.LV && curItemConfig != null)
		{
			base.ui.com_Gift.btn_Max.onClick.Retain();
			RefreshSelectGroup(Mathf.Max(1, GetCurMaxPurchaseNum()));
			base.ui.com_Gift.btn_Max.onClick.Release();
		}
	}

	private void OnSetMinSelect(EventContext context)
	{
		RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
		if (levels[levels.Count - 1].Id != _HeroCardData.LV && curItemConfig != null)
		{
			base.ui.com_Gift.btn_Min.onClick.Retain();
			RefreshSelectGroup(1);
			base.ui.com_Gift.btn_Min.onClick.Release();
		}
	}

	private void RefreshSelectGroup(int curNum)
	{
		if (curItemConfig == null)
		{
			base.ui.com_Gift.txt_itemSelectNum.text = "1";
			PreRefreshRelationLv(0L);
			return;
		}
		curSelectCount = curNum;
		base.ui.com_Gift.txt_itemSelectNum.text = curNum.ToString();
		FavorGiftConfigure favorGiftConfigure = curItemConfig.Id.GetFavorGiftConfigure();
		long num = (favorGifts.Contains(curItemConfig.Id) ? favorGiftConfigure.LikeFavor : favorGiftConfigure.NormalFavor) * curNum;
		if (curItemConfig.Id == StaticGlobalData.SPECIAL_ITEM_SUPER_GIFT && StaticConfigure.Favor.LevelDict.TryGetValue(4, out var value))
		{
			HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroInfo.Id);
			if (cardData.LV < 5)
			{
				FavorLevelConfigure favorLevelConfigure = cardData.LV.GetFavorLevelConfigure();
				int num2 = favorLevelConfigure.NeedExp - cardData.Exp;
				int num3 = value.TotalExp - favorLevelConfigure.TotalExp + num2;
				num = Math.Min(num, num3);
			}
			else
			{
				num = 0L;
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1128);
			}
		}
		PreRefreshRelationLv(num);
	}

	private int GetCurMaxPurchaseNum()
	{
		if (curItemConfig == null)
		{
			return 1;
		}
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curItemConfig.Id);
		int num = _HeroCardData.LV.GetFavorLevelConfigure().NeedExp - _HeroCardData.Exp;
		FavorGiftConfigure favorGiftConfigure = curItemConfig.Id.GetFavorGiftConfigure();
		uint num2 = (favorGifts.Contains(curItemConfig.Id) ? favorGiftConfigure.LikeFavor : favorGiftConfigure.NormalFavor);
		return Mathf.Min(itemCount, Mathf.CeilToInt(1f * (float)num / (float)num2));
	}

	private void UpdateRelationData()
	{
		levelRewards.Clear();
		foreach (FavorLevelRewardConfigureItem favorLevelRewardConfigureItem in heroInfo.FavorLevelReward.GetFavorLevelRewardConfigure().FavorLevelRewardConfigureItems)
		{
			levelRewards.Add(favorLevelRewardConfigureItem.Rewards);
		}
		base.ui.com_Gift.list_Relation.numItems = levelRewards.Count;
	}

	private void RendererRelation(int index, GObject item)
	{
		UIHero_Com_Relation com_Relation = item as UIHero_Com_Relation;
		if (com_Relation == null)
		{
			return;
		}
		com_Relation.visible = false;
		com_Relation.Cut_in.Play(1, 0.1f * (float)index, delegate
		{
			com_Relation.visible = true;
		}, null);
		if (!(com_Relation.com_Item_0 is UICom_Item uICom_Item) || !(com_Relation.com_Item_1 is UICom_Item uICom_Item2))
		{
			return;
		}
		com_Relation.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
		com_Relation.showLock.selectedIndex = ((_HeroCardData.LV > index) ? 1 : 0);
		com_Relation.progress_Relation.value = ((_HeroCardData.LV > index) ? com_Relation.progress_Relation.max : 0.0);
		com_Relation.rewardNum.selectedIndex = ((levelRewards[index].Count > 1) ? 1 : 0);
		com_Relation.releationPowerUp.selectedIndex = ((index >= 5) ? 1 : 0);
		int num = 0;
		foreach (KeyValuePair<int, int> item2 in levelRewards[index])
		{
			if (com_Relation.rewardNum.selectedIndex == 0)
			{
				RendererRelationItem(uICom_Item2, item2.Key, item2.Value);
				continue;
			}
			RendererRelationItem((num == 0) ? uICom_Item : uICom_Item2, item2.Key, item2.Value);
			num++;
		}
		com_Relation.txt_Level.text = (index + 1).ToString();
	}

	private void RendererRelationItem(UICom_Item item, int itemId, int count)
	{
		ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
		item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		item.loader_Icon.url = itemInfoConfigure.ShowIcon;
		item.txt_itemNum.text = count.ToString();
		item.onClick.Set((EventCallback0)delegate
		{
			OpenPropDetail(itemId, count);
		});
	}

	private async void OpenPropDetail(int itemId, int count)
	{
		mustHeroID = heroInfo.Id;
		mustMaterialTypeIndex = base.ui.MaterialType.selectedIndex;
		mustOperateSelectIndex = base.ui.com_Gift.operateSelect.selectedIndex;
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, count, _Usable: false);
	}

	private string ProviderGiftsItem(int index)
	{
		if (index == 0)
		{
			return "ui://7qkd4lqxl6o6q1s";
		}
		return "ui://7qkd4lqxg1lkv";
	}

	private void UpdateBreakThroughData()
	{
		breakthroughRewards.Clear();
		FavorBreakthroughConfigure favorBreakthroughConfigure = heroInfo.FavorBreakthroughConfigure;
		foreach (KeyValuePair<int, int> reward in favorBreakthroughConfigure.Rewards)
		{
			breakthroughRewards.Add(new KeyValuePair<int, int>(reward.Key, reward.Value));
			ItemInfoConfigure itemInfoConfigure = reward.Key.GetItemInfoConfigure();
			if (itemInfoConfigure.ItemType != ItemType.HeroStandingPainting)
			{
				continue;
			}
			int breakthroughItemId = GetBreakthroughItemId(favorBreakthroughConfigure.Rewards, ItemType.AccountBackground);
			foreach (SkinStandingPaintingConfigureItem standingPaintingDatum in _StandingPaintingData)
			{
				if (standingPaintingDatum.ItemID == itemInfoConfigure.Id)
				{
					_breakThroughStandingPaintingConfigureItem = standingPaintingDatum;
					RefreshSkin(standingPaintingDatum, breakthroughItemId);
					break;
				}
			}
		}
		breakthroughRewards.Sort(CompareRewardToById);
		base.ui.com_Gift.list_BreakThroughReward.numItems = breakthroughRewards.Count;
		base.ui.com_Gift.list_BreakThroughReward.scrollPane.touchEffect = false;
		RefreshBreakThroughStatus();
		RefreshBadgeItem(isSuper: false);
	}

	private int GetBreakthroughItemId(MapField<int, int> Rewards, ItemType itemType)
	{
		foreach (KeyValuePair<int, int> Reward in Rewards)
		{
			ItemInfoConfigure itemInfoConfigure = Reward.Key.GetItemInfoConfigure();
			if (itemInfoConfigure.ItemType == itemType)
			{
				return itemInfoConfigure.Id;
			}
		}
		return 0;
	}

	private int CompareRewardToById(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
	{
		if (x.Key <= y.Key)
		{
			return 1;
		}
		return -1;
	}

	private void RendererBreakThroughReward(int index, GObject item)
	{
		if (!(item is UICom_Item uICom_Item))
		{
			return;
		}
		KeyValuePair<int, int> rewardsElement = breakthroughRewards[index];
		ItemInfoConfigure itemInfoConfigure = rewardsElement.Key.GetItemInfoConfigure();
		uICom_Item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		uICom_Item.loader_Icon.url = itemInfoConfigure.ShowIcon;
		uICom_Item.txt_itemNum.text = rewardsElement.Value.ToString();
		item.onClick.Set((EventCallback0)delegate
		{
			if (!SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				OpenPropDetail(rewardsElement.Key, rewardsElement.Value);
			}
		});
	}

	private void RendererBreakThrough(int index, GObject item)
	{
		if (item is UIHero_Com_BreakThrough { com_Item: UICom_Item com_Item } uIHero_Com_BreakThrough)
		{
			KeyValuePair<int, int> itemData = breakthroughs[index];
			int curNum = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemData.Key);
			uIHero_Com_BreakThrough.txt_Num.SetVar("color", (itemData.Value > curNum) ? "FF0000" : "00FF00").SetVar("curNum", curNum.ToString()).SetVar("target", itemData.Value.ToString())
				.FlushVars();
			ItemInfoConfigure itemInfoConfigure = itemData.Key.GetItemInfoConfigure();
			com_Item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
			com_Item.loader_Icon.url = itemInfoConfigure.ShowIcon;
			com_Item.isShowNum.selectedIndex = 1;
			item.onClick.Set((EventCallback0)delegate
			{
				OpenPropDetail(itemData.Key, curNum);
			});
		}
	}

	private async void OnBreakThrough(EventContext context)
	{
		if (5 > _HeroCardData.LV || _HeroCardData.isBreakThrough)
		{
			return;
		}
		foreach (KeyValuePair<int, int> breakthrough in breakthroughs)
		{
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(breakthrough.Key);
			if (breakthrough.Value > itemCount)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1054);
				return;
			}
		}
		if (isSuperBadge)
		{
			ItemInfoConfigure itemInfoConfigure = StaticGlobalData.SPECIAL_ITEM_SUPER_BADGE.GetItemInfoConfigure();
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1056.GetLocal(UIStringType.Message), itemInfoConfigure.NameID.GetLocal(UIStringType.Item)), delegate
			{
				RequestBreakThrough(heroInfo.Id, isSuper: true);
			});
		}
		else
		{
			RequestBreakThrough(heroInfo.Id, isSuper: false);
		}
	}

	private void RequestBreakThrough(int heroId, bool isSuper)
	{
		base.ui.com_Gift.btn_breakThrough.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestRoleCardBreakThroughC2S(heroId, isSuper).OnFinishedOnly.AddOnce(delegate
		{
			RefreshBreakThroughStatus();
			base.ui.com_Gift.btn_breakThrough.onClick.Release();
		});
	}

	private void RefreshBreakThroughStatus()
	{
		bool flag = 5 <= _HeroCardData.LV && _HeroCardData.isBreakThrough;
		base.ui.com_Gift.breakThroughStatus.selectedIndex = (flag ? 1 : 0);
		base.ui.com_Gift.LV5.selectedIndex = ((5 <= _HeroCardData.LV) ? 1 : 0);
	}

	private void OnClickShowBreakThrough()
	{
		ShowBreakThrough().Forget();
	}

	public async UniTask ShowBreakThrough()
	{
		breakThroughTsc = new UniTaskCompletionSource();
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(150.GetVideoKey());
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroInfo.Id, 0, 0);
		RefreshBreakThrough(configStandingPainting, base.ui.btn_BreakThroughShow.loader_Initial);
		RefreshBreakThrough(_breakThroughStandingPaintingConfigureItem, base.ui.btn_BreakThroughShow.loader_BreakThrough);
		base.ui.btn_BreakThroughShow.showBreakThrough.SetHook("HeroCutOut", delegate
		{
			BreakThroughVideo();
			GButton btn_nextPage = base.ui.btn_nextPage;
			bool visible = (base.ui.btn_prePage.visible = false);
			btn_nextPage.visible = visible;
			base.ui.Cutin.PlayReverse();
		});
		base.ui.btn_BreakThroughShow.showBreakThrough.SetHook("PlaySound", PlayBreakThroughVoice);
		base.ui.btn_BreakThroughShow.showBreakThrough.SetHook("Finish", delegate
		{
			base.ui.btn_BreakThroughShow.onClick.Release();
		});
		base.ui.btn_BreakThroughShow.onClick.Retain();
		base.ui.btn_BreakThroughShow.visible = true;
		base.ui.btn_BreakThroughShow.showBreakThrough.Play();
		await breakThroughTsc.Task;
	}

	private void CloseBreakThrough()
	{
		breakThroughTsc?.TrySetResult();
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.btn_BreakThroughShow.loader_Video);
		FinishBreakThough();
		base.ui.btn_BreakThroughShow.onClick.Release();
		if (_breakThroughVoiceId != 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(_breakThroughVoiceId);
		}
		_breakThroughVoiceId = 0u;
	}

	private void BreakThroughVideo()
	{
		base.ui.btn_BreakThroughShow.loader_Video.FullScreen();
		CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, base.ui.btn_BreakThroughShow.loader_Video);
		SimpleSingletonProvider<CriMovieManager>.inst.Play(150.GetVideoKey(), base.ui.btn_BreakThroughShow.loader_Video, null, delegate
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.btn_BreakThroughShow.loader_Video);
			FinishBreakThough();
		}).Forget();
	}

	private void PlayBreakThroughVoice()
	{
		int voice = _breakThroughStandingPaintingConfigureItem.Voice;
		if (voice != 0)
		{
			CharacterVoiceConfigure voiceConfigure = voice.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				_breakThroughVoiceId = SimpleSingletonProvider<AudioManager>.inst.SendEvent(voiceConfigure.AwakeVoice, Stage.inst.gameObject);
			}
		}
	}

	private void FinishBreakThough()
	{
		base.ui.btn_BreakThroughShow.onClick.Retain();
		base.ui.Cutin.Play();
		base.ui.btn_BreakThroughShow.visible = false;
		breakThroughTsc?.TrySetResult();
		base.ui.btn_BreakThroughShow.onClick.Release();
		GButton btn_nextPage = base.ui.btn_nextPage;
		bool visible = (base.ui.btn_prePage.visible = true);
		btn_nextPage.visible = visible;
	}

	private void RefreshBreakThrough(SkinStandingPaintingConfigureItem StandingPaintingItem, GLoader loader)
	{
		(string, bool) character = StandingPaintingItem.GetCharacter();
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
		{
			customOffset = value.skinOffset;
		}
		loader.customOffset = customOffset;
		loader.customScale = Vector2.one;
		(loader.url, _) = character;
	}

	private void SwitchBreakthroughProp()
	{
		base.ui.com_Gift.btn_Switch.onClick.Retain();
		RefreshBadgeItem(!isSuperBadge);
		base.ui.com_Gift.btn_Switch.onClick.Release();
	}

	private void RefreshBadgeItem(bool isSuper)
	{
		if (base.ui.com_Gift.breakThroughStatus.selectedIndex != 0)
		{
			return;
		}
		breakthroughs.Clear();
		base.ui.com_Gift.btn_breakThrough.onClick.Retain();
		isSuperBadge = isSuper;
		if (isSuperBadge)
		{
			breakthroughs.Add(new KeyValuePair<int, int>(StaticGlobalData.SPECIAL_ITEM_SUPER_BADGE, 1));
		}
		else
		{
			foreach (KeyValuePair<int, int> needMaterial in heroInfo.FavorBreakthroughConfigure.NeedMaterials)
			{
				breakthroughs.Add(new KeyValuePair<int, int>(needMaterial.Key, needMaterial.Value));
			}
		}
		base.ui.com_Gift.list_BreakThrough.numItems = breakthroughs.Count;
		base.ui.com_Gift.btn_breakThrough.onClick.Release();
	}

	private void RefreshSkin()
	{
		UnlockSkinCount = 0;
		RefreshSkin(_CurStandingPaintingItem);
		base.ui.com_Skin.com_StandingPaintingList.list_StandingPainting.numItems = _StandingPaintingData.Count;
		base.ui.com_Skin.com_StandingPaintingList.list_StandingPainting.scrollPane.percX = 0f;
		base.ui.com_Skin.txt_SkinProgress.text = $"{1061.GetLocal(UIStringType.Message)}{UnlockSkinCount}/{_StandingPaintingData.Count}";
	}

	private void RendererStandingPainting(int index, GObject item)
	{
		UIHero_Button_SkinItem btn_Item = item as UIHero_Button_SkinItem;
		if (btn_Item == null)
		{
			return;
		}
		btn_Item.visible = false;
		btn_Item.Cut_in.Play(1, 0.1f * (float)index, delegate
		{
			btn_Item.visible = true;
		}, null);
		btn_Item.Refresh(_StandingPaintingData[index]);
		if (btn_Item.IsHas)
		{
			UnlockSkinCount++;
		}
		if (_CurStandingPaintingItem.ItemID == _StandingPaintingData[index].ItemID)
		{
			selectedItem = btn_Item;
			btn_Item.selected = true;
			btn_Item.Using.selectedIndex = (btn_Item.IsHas ? 1 : 0);
		}
		else
		{
			btn_Item.Using.selectedIndex = 0;
			btn_Item.selected = false;
		}
		btn_Item.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_Skin.com_StandingPaintingList.list_StandingPainting.ScrollToView(index, ani: true);
			if (!btn_Item.IsHas || !_HeroCardData.IsHas)
			{
				btn_Item.onClick.Retain();
				RefreshSkin(_StandingPaintingData[index]);
				btn_Item.onClick.Release();
			}
			else
			{
				btn_Item.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestRoleCardChoiceResC2S(heroInfo.Id, _StandingPaintingData[index].ItemID).OnFinishedOnly.AddOnce(delegate
				{
					selectedItem.selected = false;
					selectedItem.Using.selectedIndex = 0;
					btn_Item.selected = true;
					btn_Item.Using.selectedIndex = 1;
					selectedItem = btn_Item;
					_CurStandingPaintingItem = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCurStandingPainting(heroInfo.Id);
					RefreshSkin(_CurStandingPaintingItem);
					btn_Item.onClick.Release();
				});
			}
		});
	}

	private async UniTaskVoid RefreshEmoji()
	{
		expressionData = await SimpleSingletonProvider<GameLogicManager>.inst.communicate.LoadExpression(heroInfo);
		UnlockExpressionCount = 0;
		base.ui.com_Emoji.list_Expression.numItems = Mathf.Max(expressionData.Count, 20);
		base.ui.com_Emoji.list_Expression.scrollPane.percY = 0f;
		base.ui.com_Emoji.txt_SkinProgress.text = $"{1062.GetLocal(UIStringType.Message)}{UnlockExpressionCount}/{expressionData.Count}";
	}

	private void RendererExpression(int index, GObject item)
	{
		UIHero_Com_Expression _item = item as UIHero_Com_Expression;
		if (_item == null)
		{
			return;
		}
		_item.visible = false;
		_item.Cut_in.Play(1, 0.01f * (float)index, delegate
		{
			_item.visible = true;
		}, null);
		if (expressionData.Count <= index)
		{
			_item.grayed = false;
			_item.empty.selectedIndex = 1;
			return;
		}
		ItemInfoConfigure itemConfig = expressionData[index].expressionItemID.GetItemInfoConfigure();
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemConfig.Id);
		_item.grayed = !flag;
		if (flag)
		{
			UnlockExpressionCount++;
		}
		_item.empty.selectedIndex = 0;
		if (expressionData[index].isVideo)
		{
			_item.type.selectedIndex = 1;
			CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, _item.loader_Expression_Video);
			SimpleSingletonProvider<CriMovieManager>.inst.Play(expressionData[index].expressionConfig.VideoKey, _item.loader_Expression_Video).Forget();
		}
		else
		{
			_item.type.selectedIndex = 0;
			_item.loader_Expression_Image.url = expressionData[index].textureUrl;
		}
		_item.onClick.Set((EventCallback0)delegate
		{
			if (!SimpleSingletonProvider<UIManager>.inst.guide.isShowing && _item.empty.selectedIndex != 1)
			{
				OpenPropDetail(itemConfig.Id, 1);
			}
		});
	}

	private void UnLoadExpression()
	{
		base.ui.com_Emoji.list_Expression.numItems = 0;
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.UnLoadExpression();
	}

	private bool GetSkillSwitchButtonStatus(CharacterInfoConfigure characterInfoConfigure)
	{
		if (heroInfo.ActiveSkill != heroInfo.PveActiveSkill)
		{
			return true;
		}
		RepeatedField<int> passiveSkills = heroInfo.PassiveSkills;
		RepeatedField<int> pvePassiveSkills = heroInfo.PvePassiveSkills;
		if (passiveSkills.Count != pvePassiveSkills.Count)
		{
			return true;
		}
		for (int i = 0; i < pvePassiveSkills.Count; i++)
		{
			if (!passiveSkills.Contains(pvePassiveSkills[i]))
			{
				return true;
			}
		}
		return false;
	}

	private void RefreshCurPVELv(bool playAnimation = false)
	{
		RefreshFileUpgradeButtonStatus();
		int level = _HeroCardData.PveData.Level;
		RepeatedField<PVENurturanceEnhancementConfigureItem> EnhancementInfos = _HeroCardData.PveData.EnhancementConfig.PVENurturanceEnhancementConfigureItems;
		base.ui.com_File.com_FileUpgrade.list_LvInfo.itemRenderer = delegate(int index, GObject item)
		{
			UIHero_Com_PveLvDesc com_LvDesc = item as UIHero_Com_PveLvDesc;
			if (com_LvDesc != null)
			{
				com_LvDesc.LvSDi.selectedIndex = 0;
				com_LvDesc.txt_LvDesc.text = EnhancementInfos[index].DescriptionID.GetLocal(UIStringType.PVENurturance);
				com_LvDesc.txt_level.text = EnhancementInfos[index].Lv.ToString();
				com_LvDesc.status.selectedIndex = ((level >= EnhancementInfos[index].Lv) ? 1 : 0);
				if (playAnimation)
				{
					com_LvDesc.group_root.visible = false;
					com_LvDesc.Cut_in.Play(1, (float)index * 0.05f, delegate
					{
						com_LvDesc.group_root.visible = true;
					}, null);
				}
			}
		};
		base.ui.com_File.com_FileUpgrade.list_LvInfo.numItems = EnhancementInfos.Count;
		base.ui.com_File.com_FileSkill.com_Level.txt_level.text = _HeroCardData.PveData.Level.ToString();
	}

	private void RefreshPveExpInfo()
	{
		UpdatePveProgress(0);
		UpdatePveDropItem();
		base.ui.com_File.com_FileUpgrade.operatetype.selectedIndex = 0;
		RefreshFileUpgradeButtonStatus();
	}

	private void RefreshFileUpgradeButtonStatus()
	{
		if (_HeroCardData.IsHas)
		{
			RepeatedField<PVENurturanceLevelupConfigure> levelups = StaticConfigure.PVENurturance.Levelups;
			if (levelups[levelups.Count - 1].Id != _HeroCardData.PveData.Level)
			{
				base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.grayed = false;
				base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.grayed = false;
				base.ui.com_File.com_FileUpgrade.btn_Upgrade.grayed = false;
				base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.touchable = true;
				base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.touchable = true;
				base.ui.com_File.com_FileUpgrade.btn_Upgrade.touchable = true;
				return;
			}
		}
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.grayed = true;
		base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.grayed = true;
		base.ui.com_File.com_FileUpgrade.btn_Upgrade.grayed = true;
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.touchable = false;
		base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.touchable = false;
		base.ui.com_File.com_FileUpgrade.btn_Upgrade.touchable = false;
	}

	private void UpdatePveDropItem()
	{
		CancelLongPress();
		PVEExpItems = _HeroCardData.PVEExpItems;
		base.ui.com_File.com_FileUpgrade.list_Prop.numItems = PVEExpItems.Count;
		base.ui.com_File.com_FileUpgrade.list_Prop.scrollPane.touchEffect = PVEExpItems.Count > 4;
		base.ui.com_File.com_FileUpgrade.list_Prop.scrollPane.mouseWheelEnabled = PVEExpItems.Count > 4;
	}

	private void RendererPVEExpItems(int index, GObject item)
	{
		UIHero_Com_PveDrop com_PveDrop = item as UIHero_Com_PveDrop;
		if (com_PveDrop == null || !(com_PveDrop.btn_Item is UICom_LitItem uICom_LitItem))
		{
			return;
		}
		PveExpItemData expItemData = PVEExpItems[index];
		com_PveDrop.txt_Count.text = $"0/{expItemData.Count}";
		com_PveDrop.showDelete.selectedIndex = ((expItemData.selectCount > 0) ? 1 : 0);
		com_PveDrop.showWay.selectedIndex = ((expItemData.Count <= 0) ? 1 : 0);
		com_PveDrop.btn_Delete.onClick.Set((EventCallback0)delegate
		{
			DeletePveExpDrop(expItemData);
		});
		com_PveDrop.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			if (expItemData.Count == 0)
			{
				com_PveDrop.btn_GoWay.onClick.Retain();
				mustHeroID = heroInfo.Id;
				mustMaterialTypeIndex = base.ui.MaterialType.selectedIndex;
				mustOperateSelectIndex = base.ui.com_Gift.operateSelect.selectedIndex;
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(expItemData.Id, expItemData.Count, _Usable: false);
				com_PveDrop.btn_GoWay.onClick.Release();
			}
		});
		expItemData.dropItem = com_PveDrop;
		uICom_LitItem.qualityType.selectedIndex = (int)expItemData.itemInfo.QualityType;
		uICom_LitItem.loader_Icon.url = expItemData.itemInfo.ShowIcon;
		uICom_LitItem.RegisterLongPressEvent(delegate
		{
			SelectPveExpDrop(expItemData);
		}, null, delegate
		{
			SelectPveExpDrop(expItemData);
		}, 0f, 0.1f, once: false);
	}

	private void SelectPveExpDrop(PveExpItemData expItemData)
	{
		if (_HeroCardData.IsHas && expItemData.selectCount < expItemData.Count && IsCanAddDrop(expItemData))
		{
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			expItemData.selectCount = Mathf.Min(expItemData.selectCount + 1, expItemData.Count);
			RefreshPveExpDropEffect(expItemData);
		}
	}

	private void DeletePveExpDrop(PveExpItemData expItemData)
	{
		if (expItemData.selectCount > 0)
		{
			expItemData.selectCount = Mathf.Max(expItemData.selectCount - 1, 0);
			RefreshPveExpDropEffect(expItemData);
		}
	}

	private bool IsCanAddDrop(PveExpItemData expItemData)
	{
		int level = _HeroCardData.PveData.Level;
		int num = GetPveTotalDropExp() + expItemData.Exp + _HeroCardData.PveData.Exp;
		RepeatedField<PVENurturanceLevelupConfigure> levelups = StaticConfigure.PVENurturance.Levelups;
		for (int i = 0; i < levelups.Count; i++)
		{
			if (levelups[i].Id >= level)
			{
				if (num - levelups[i].Exp < expItemData.Exp)
				{
					return true;
				}
				num -= levelups[i].Exp;
			}
		}
		return false;
	}

	private void RefreshPveExpDropEffect(PveExpItemData expItemData)
	{
		expItemData.dropItem.txt_Count.text = $"{expItemData.selectCount}/{expItemData.Count}";
		expItemData.dropItem.showDelete.selectedIndex = ((expItemData.selectCount > 0) ? 1 : 0);
		base.ui.com_File.com_FileUpgrade.operatetype.selectedIndex = ((expItemData.selectCount > 0) ? 1 : 0);
		int pveTotalDropExp = GetPveTotalDropExp();
		UpdatePveProgress(pveTotalDropExp);
	}

	private int GetPveTotalDropExp()
	{
		int totalExp = 0;
		PVEExpItems.ForEach(delegate(PveExpItemData x)
		{
			totalExp += x.Exp * x.selectCount;
		});
		return totalExp;
	}

	private void UpdatePveProgress(int addExp)
	{
		RepeatedField<PVENurturanceLevelupConfigure> levelups = StaticConfigure.PVENurturance.Levelups;
		int level = _HeroCardData.PveData.Level;
		int num = _HeroCardData.PveData.Exp + addExp;
		PVENurturanceLevelupConfigure pVENurturanceLevelupConfigure = levelups[levelups.Count - 1];
		if (level == pVENurturanceLevelupConfigure.Id)
		{
			base.ui.com_File.com_FileUpgrade.progress_Exp.min = 0.0;
			base.ui.com_File.com_FileUpgrade.progress_Exp.max = 100.0;
			base.ui.com_File.com_FileUpgrade.progress_Exp.value = 100.0;
			base.ui.com_File.com_FileUpgrade.progress_Exp.maxStatus.selectedIndex = 1;
			return;
		}
		base.ui.com_File.com_FileUpgrade.progress_Exp.maxStatus.selectedIndex = 0;
		for (int i = 0; i < levelups.Count; i++)
		{
			if (levelups[i].Id >= level)
			{
				if (num - levelups[i].Exp <= 0)
				{
					pVENurturanceLevelupConfigure = levelups[i];
					break;
				}
				num -= levelups[i].Exp;
			}
		}
		base.ui.com_File.com_FileUpgrade.progress_Exp.lvVail.selectedIndex = ((addExp > 0) ? 1 : 0);
		base.ui.com_File.com_FileUpgrade.progress_Exp.txt_Cur.SetVar("Lv", level.ToString()).FlushVars();
		base.ui.com_File.com_FileUpgrade.progress_Exp.txt_Next.SetVar("Lv", Mathf.Min(levelups[levelups.Count - 1].Id, (pVENurturanceLevelupConfigure.Exp == num) ? (pVENurturanceLevelupConfigure.Id + 1) : pVENurturanceLevelupConfigure.Id).ToString()).FlushVars();
		base.ui.com_File.com_FileUpgrade.progress_Exp.min = 0.0;
		base.ui.com_File.com_FileUpgrade.progress_Exp.max = pVENurturanceLevelupConfigure.Exp;
		if (addExp == 0)
		{
			base.ui.com_File.com_FileUpgrade.progress_Exp.value = num;
		}
		else
		{
			base.ui.com_File.com_FileUpgrade.progress_Exp.TweenValue(num, 0.1f);
		}
	}

	private void OnPveUpgrade()
	{
		if (!_HeroCardData.IsHas)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (PveExpItemData pVEExpItem in PVEExpItems)
		{
			if (pVEExpItem.selectCount > 0)
			{
				dictionary.TryAdd(pVEExpItem.Id, pVEExpItem.selectCount);
			}
		}
		if (dictionary.Count != 0)
		{
			base.ui.com_File.com_FileUpgrade.btn_Upgrade.onClick.Retain();
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestPveHeroUpLvC2S(_HeroCardData.HeroId, dictionary).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.com_File.com_FileUpgrade.btn_Upgrade.onClick.Release();
				RefreshCurPVELv();
				UpdatePveProgress(0);
				base.ui.com_File.com_FileUpgrade.operatetype.selectedIndex = 0;
				RefreshPveTalentStatus();
			});
		}
	}

	private void AutoUpgrade()
	{
		if (!_HeroCardData.IsHas)
		{
			return;
		}
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.onClick.Retain();
		RepeatedField<PVENurturanceLevelupConfigure> levelups = StaticConfigure.PVENurturance.Levelups;
		int level = _HeroCardData.PveData.Level;
		int num = -_HeroCardData.PveData.Exp;
		for (int i = 0; i < levelups.Count; i++)
		{
			if (levelups[i].Id >= level)
			{
				num += levelups[i].Exp;
			}
		}
		for (int j = 0; j < PVEExpItems.Count; j++)
		{
			int count = PVEExpItems[j].Count;
			if (count <= 0)
			{
				continue;
			}
			for (int k = 0; k < count; k++)
			{
				int pveTotalDropExp = GetPveTotalDropExp();
				if (num == pveTotalDropExp || pveTotalDropExp > num)
				{
					break;
				}
				SelectPveExpDrop(PVEExpItems[j]);
			}
		}
		base.ui.com_File.com_FileUpgrade.btn_AutoUpgrade.onClick.Release();
	}

	private void CancelUpgrade()
	{
		if (_HeroCardData != null && _HeroCardData.IsHas)
		{
			base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.onClick.Retain();
			RefreshPveExpInfo();
			base.ui.com_File.com_FileUpgrade.btn_CancelUpgrade.onClick.Release();
		}
	}

	private void CancelLongPress()
	{
		if (PVEExpItems != null)
		{
			for (int i = 0; i < PVEExpItems.Count; i++)
			{
				PVEExpItems[i].dropItem?.btn_Item.UnRegisterLongPressEvent();
			}
		}
	}

	public async void GuideOpenHeroContract()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_MaskGiftTab, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_MaskGiftTab, base.ui.com_MaskGiftTab.width / 3f, base.ui.com_MaskGiftTab.height);
	}

	public async void GuideOpenHeroGiftTab()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_Gift.btn_GiftTab, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_Gift.btn_GiftTab, base.ui.com_Gift.btn_GiftTab.width / 3f, base.ui.com_Gift.btn_GiftTab.height);
	}

	public async void GuideSelectGift()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_Gift.com_MaskGift, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_Gift.com_MaskGift, base.ui.com_Gift.com_MaskGift.width / 3f, base.ui.com_Gift.com_MaskGift.height);
	}

	public async void GuideSureGift()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_Gift.btn_giveGift, needTransparentMask: false, isRect: true, ignorePivot: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_Gift.btn_giveGift, base.ui.com_Gift.btn_giveGift.width / 3f, base.ui.com_Gift.btn_giveGift.height, 0f, ignorePivot: true);
	}

	public async void GuideLockFavorLV()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.group_Progress, needTransparentMask: true, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.group_Progress, base.ui.group_Progress.width / 3f, base.ui.group_Progress.height + base.ui.slider_Favor.height);
	}

	public async void GuideOpenContractTab()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_Gift.btn_OperateBreak, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_Gift.btn_OperateBreak, base.ui.com_Gift.btn_OperateBreak.width / 3f, base.ui.com_Gift.btn_OperateBreak.height);
	}

	public async void GuideShowContractReward()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_Gift.list_BreakThroughReward, needTransparentMask: true, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_Gift.list_BreakThroughReward, base.ui.com_Gift.list_BreakThroughReward.width / 3f, base.ui.com_Gift.list_BreakThroughReward.height);
	}

	public async void GuideShowContractUP()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
		UIHero_Button_FileSelect btn = base.ui.com_File.btn_Upgrade;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cutin.playing, SimpleSingletonProvider<UIManager>.inst.guide.Hide);
		if (!btn.onStage)
		{
			return;
		}
		btn.onClick.Set((EventCallback0)delegate
		{
			btn.onClick.Retain();
			btn.tab.selectedIndex = 1;
			btn.onChanged.Call();
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			btn.onClick.Release();
		});
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(btn, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(btn, btn.width / 3f, btn.height);
	}

	public async void GuideShowContractMaterial()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_File.com_MaskMaterial, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowRotateByTarget(base.ui.com_File.com_MaskMaterial, base.ui.com_File.com_MaskMaterial.width / 5f, base.ui.com_File.com_MaskMaterial.height / 20f);
	}

	public async void GuideShowContractUPgrade()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_File.com_FileUpgrade.com_MaskUPbtn, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowRotateByTarget(base.ui.com_File.com_FileUpgrade.com_MaskUPbtn, base.ui.com_File.com_FileUpgrade.com_MaskUPbtn.width / 5f, base.ui.com_File.com_FileUpgrade.com_MaskUPbtn.height / 20f);
	}

	public async void GuideShowContractLVInfo()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.com_File.com_FileUpgrade.com_MaskText, needTransparentMask: true, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.com_File.com_FileUpgrade.com_MaskText, base.ui.com_File.com_FileUpgrade.com_MaskText.width / 3f, base.ui.com_File.com_FileUpgrade.com_MaskText.height);
	}

	private void OnTabChanged(EventContext context)
	{
		switch (base.ui.com_File.tab.selectedIndex)
		{
		case 0:
			base.ui.com_File.Skill_Cut_in.Play();
			break;
		case 1:
			RefreshCurPVELv(playAnimation: true);
			base.ui.com_File.Upgrade_Cut_in.Play();
			break;
		case 2:
			base.ui.com_File.Potential_Cut_in.Play();
			break;
		case 3:
			base.ui.com_File.Info_Cut_in.Play();
			break;
		}
	}

	private void ShowPrePage()
	{
		if (base.ui.MaterialType.selectedIndex != 3)
		{
			_currentRefreshHeroTime = 0f;
		}
		if ((double)(Time.time - _currentRefreshHeroTime) > 1.5)
		{
			_currentRefreshHeroTime = Time.time;
			base.ui.btn_prePage.onClick.Retain();
			base.ui.btn_nextPage.onClick.Retain();
			int index = CurrentHeroIndex() - 1;
			SwitchHeroCard(index);
			base.ui.btn_prePage.onClick.Release();
			base.ui.btn_nextPage.onClick.Release();
		}
	}

	private void ShowNextPage()
	{
		if (base.ui.MaterialType.selectedIndex != 3)
		{
			_currentRefreshHeroTime = 0f;
		}
		if ((double)(Time.time - _currentRefreshHeroTime) > 1.5)
		{
			_currentRefreshHeroTime = Time.time;
			base.ui.btn_prePage.onClick.Retain();
			base.ui.btn_nextPage.onClick.Retain();
			int index = CurrentHeroIndex() + 1;
			SwitchHeroCard(index);
			base.ui.btn_prePage.onClick.Release();
			base.ui.btn_nextPage.onClick.Release();
		}
	}

	private int CurrentHeroIndex()
	{
		return heroCards.FindIndex((HeroCardData card) => card.HeroId == heroInfo.Id);
	}

	private void SwitchHeroCard(int index)
	{
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			return;
		}
		int index2 = (index + heroCards.Count) % heroCards.Count;
		Close_Detail();
		CommonUIManager.StopVideo(UIType.Panel, (int)base.config.PanelType);
		ReadyHeroDetailInfo(heroCards[index2].HeroId);
		if (base.ui.com_File.btn_Collect is UIButton_Collect uIButton_Collect)
		{
			uIButton_Collect.collected.selectedIndex = (heroCards[index2].CollectStatus ? 1 : 0);
			UpdateCollectTipColor(heroCards[index2].CollectStatus);
			if (base.ui.MaterialType.selectedIndex == 3)
			{
				RefreshHeroBaseInfo();
			}
			else
			{
				RefreshNickName();
				RefreshRelationLv();
			}
			Refresh_Detail(base.ui.MaterialType.selectedIndex, base.ui.com_Gift.operateSelect.selectedIndex, base.ui.com_File.tab.selectedIndex);
		}
	}

	private void OnSwipeGestureSwitchHero(EventContext context)
	{
		if (base.ui.tab.selectedIndex != 0 && _swipeGesture != null && Mathf.Abs(Vector2.Angle(_swipeGesture.velocity, Vector2.right) - 90f) > 30f)
		{
			if (_swipeGesture.velocity.x < 0f)
			{
				ShowNextPage();
			}
			else if (_swipeGesture.velocity.x > 0f)
			{
				ShowPrePage();
			}
		}
	}

	private void ReadyHeroInfo()
	{
		heroCards = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetHeroCards(checkStatus: true);
		InitSortOptions();
		ApplySort(LocalCache.GetHeroSortCache(), LocalCache.GetHeroSortOrderCache());
	}

	private void InitComponents_Selector()
	{
		base.ui.com_HeroList.list_Hero.itemRenderer = RendererHero;
		InitSortOptions();
	}

	private void InitSortOptions()
	{
		base.ui.com_HeroSort.items = HeroSortLabels;
		base.ui.com_HeroSort.values = new string[3]
		{
			0.ToString(),
			1.ToString(),
			2.ToString()
		};
	}

	private void Refresh_Selector()
	{
		UnlockCharacterCount = 0;
		base.ui.com_HeroList.list_Hero.numItems = heroCards.Count;
		base.ui.com_HeroList.list_Hero.GetChildAt(0).onClick.Call();
		base.ui.txt_CharacterProgress.text = $"{1060.GetLocal(UIStringType.Message)}{UnlockCharacterCount}/{heroCards.Count}";
	}

	private void AddEvent_Selector()
	{
		base.ui.btn_Detail.onClick.Add(OpenHeroDetail);
		base.ui.com_HeroList.list_Hero.onClickItem.Add(ReadyRefreshPaint);
		base.ui.com_HeroSort.onChanged.Add(OnSortTypeChanged);
		base.ui.Btn_sort.onClick.Add(OnSortOrderClicked);
	}

	private void RemoveEvent_Selector()
	{
		base.ui.btn_Detail.onClick.Remove(OpenHeroDetail);
		base.ui.com_HeroList.list_Hero.onClickItem.Remove(ReadyRefreshPaint);
		base.ui.com_HeroSort.onChanged.Remove(OnSortTypeChanged);
		base.ui.Btn_sort.onClick.Remove(OnSortOrderClicked);
	}

	private void OpenHeroDetail(EventContext context)
	{
		base.ui.btn_Detail.onClick.Retain();
		int heroId = heroCards[base.ui.com_HeroList.list_Hero.selectedIndex].HeroId;
		ReadyHeroDetailInfo(heroId);
		Refresh_Detail(0, 0, 0);
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
		base.ui.btn_Detail.onClick.Release();
	}

	private void ReadyRefreshPaint(EventContext context)
	{
		base.ui.com_HeroList.list_Hero.onClickItem.Retain();
		HeroCardData heroCardData = heroCards[base.ui.com_HeroList.list_Hero.selectedIndex];
		_CurStandingPaintingItem = heroCardData.standingPainting;
		heroInfo = heroCardData.InfoConfig;
		UpdateCollectStatus(heroCardData);
		RefreshHeroBaseInfo();
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
		}
		base.ui.com_HeroList.list_Hero.onClickItem.Release();
	}

	private void RendererHero(int index, GObject item)
	{
		UIHero_Button_Hero btn = item as UIHero_Button_Hero;
		if (btn != null)
		{
			btn.visible = false;
			btn.Cut_in.Play(1, 0.01f * (float)index, delegate
			{
				btn.visible = true;
			}, null);
			HeroCardData heroCardData = heroCards[index];
			btn.Refresh(heroCardData, base.ui.com_HeroSort.selectedIndex);
			btn.com_show.releationPowerUp.selectedIndex = ((!heroCardData.InfoConfig.HasKizuna || heroCardData.LV > 5) ? 1 : 0);
			if (heroCardData.IsHas)
			{
				UnlockCharacterCount++;
			}
		}
	}

	private void OnSortTypeChanged(EventContext context)
	{
		ApplySort(base.ui.com_HeroSort.selectedIndex, GetCurrentSortOrder());
	}

	private void OnSortOrderClicked(EventContext context)
	{
		int sortOrder = ((GetCurrentSortOrder() == 0) ? 1 : 0);
		ApplySort(GetCurrentSortType(), sortOrder);
	}

	private void ApplySort(int sortType, int sortOrder)
	{
		sortType = NormalizeSortType(sortType);
		sortOrder = NormalizeSortOrder(sortOrder);
		LocalCache.UpdateHeroSortCache(sortType);
		LocalCache.UpdateHeroSortOrderCache(sortOrder);
		base.ui.com_HeroSort.selectedIndex = sortType;
		base.ui.Btn_sort.sort.selectedIndex = sortOrder;
		SortHeroCards(sortType, sortOrder);
		RefreshSortList();
	}

	private void RefreshSortList()
	{
		if (heroCards != null && heroCards.Count != 0)
		{
			base.ui.com_HeroList.list_Hero.numItems = heroCards.Count;
			base.ui.com_HeroList.list_Hero.GetChildAt(0).onClick.Call();
		}
	}

	private int GetCurrentSortType()
	{
		return NormalizeSortType(LocalCache.GetHeroSortCache());
	}

	private int GetCurrentSortOrder()
	{
		return NormalizeSortOrder(LocalCache.GetHeroSortOrderCache());
	}

	private static int NormalizeSortType(int sortType)
	{
		return sortType switch
		{
			1 => 1, 
			2 => 2, 
			_ => 0, 
		};
	}

	private static int NormalizeSortOrder(int sortOrder)
	{
		if (sortOrder != 1)
		{
			return 0;
		}
		return 1;
	}

	private void SortHeroCards(int sortType, int sortOrder)
	{
		HeroCardLogic heroCardLogic = SimpleSingletonProvider<GameLogicManager>.inst.heroCard;
		bool asc = sortOrder == 0;
		switch (sortType)
		{
		case 1:
			heroCards.Sort((HeroCardData x, HeroCardData y) => heroCardLogic.CompareToLVAndExp(x, y, asc));
			break;
		case 2:
			heroCards.Sort((HeroCardData x, HeroCardData y) => heroCardLogic.CompareToPve(x, y, asc));
			break;
		default:
			heroCards.Sort((HeroCardData x, HeroCardData y) => heroCardLogic.CompareTo(x, y, asc));
			break;
		}
	}

	public async void GuideOpenHeroDetail()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.btn_Detail, needTransparentMask: false, isRect: true);
		SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowRotateByTarget(base.ui.btn_Detail, base.ui.btn_Detail.width / 5f, base.ui.btn_Detail.height / 20f);
	}

	public async void GuideOpenHeroDetailStable()
	{
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cutin.playing, SimpleSingletonProvider<UIManager>.inst.guide.Hide);
		if (base.ui.btn_Detail.onStage)
		{
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMaskByTarget(base.ui.btn_Detail, needTransparentMask: false, isRect: true);
			SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrowByTarget(base.ui.btn_Detail, base.ui.btn_Detail.width / 5f, base.ui.btn_Detail.height);
			SimpleSingletonProvider<UIManager>.inst.guide.ForceShowDialog();
		}
	}

	private void InitPveTalent()
	{
		if (!_HeroCardData.PveData.CanTalentConfig())
		{
			base.ui.com_File.btn_Talent.visible = false;
			return;
		}
		_pveNurturanceBreakConfigure = _HeroCardData.PveData.GetCurrentTalentConfigure();
		if (_pveNurturanceBreakConfigure == null)
		{
			return;
		}
		base.ui.com_File.com_FileBreakThough.txt_DescTitle.SetVar("name", _HeroCardData.InfoConfig.NameID.GetLocal(UIStringType.Character)).FlushVars();
		CommonUIManager.RefreshHyperlinkDesc(_pveNurturanceBreakConfigure.DescriptionID.GetLocal(UIStringType.PVENurturance), base.ui.com_File.com_FileBreakThough.com_Desc.title);
		base.ui.com_File.com_FileBreakThough.txt_Tips.SetVar("lv", _pveNurturanceBreakConfigure.UnlockNeedPVELevel.ToString()).FlushVars();
		base.ui.com_File.btn_Talent.visible = true;
		base.ui.com_File.com_FileBreakThough.language.selectedIndex = ((GameSettings.languageType == LanguageType.English) ? 1 : 0);
		_pveTalentMaterials.Clear();
		foreach (KeyValuePair<int, int> needMaterial in _pveNurturanceBreakConfigure.NeedMaterials)
		{
			_pveTalentMaterials.Add(new KeyValuePair<int, int>(needMaterial.Key, needMaterial.Value));
		}
		base.ui.com_File.com_FileBreakThough.list_talentMaterials.numItems = _pveTalentMaterials.Count;
	}

	private void RequestTalentBreakThrough()
	{
		if (_pveNurturanceBreakConfigure != null && _pveNurturanceBreakConfigure.UnlockNeedPVELevel == _HeroCardData.PveData.Level && CheckMaterialsMeet() && !_HeroCardData.PveData.IsTalentUnlock(_pveNurturanceBreakConfigure.Id))
		{
			base.ui.com_File.com_FileBreakThough.list_talentMaterials.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.heroCard.RequestRoleTalentUpC2S(heroInfo.Id, _pveNurturanceBreakConfigure.Id).OnFinishedOnly.AddOnce(delegate
			{
				base.ui.com_File.com_FileBreakThough.btn_talentBreak.onClick.Release();
				base.ui.com_File.com_FileBreakThough.Finish.Play();
			});
		}
	}

	private void RefreshPveTalentStatus()
	{
		if (_pveNurturanceBreakConfigure != null)
		{
			if (_HeroCardData.PveData.GetCurrentTalentConfigure() != null && _pveNurturanceBreakConfigure.Id != _HeroCardData.PveData.GetCurrentTalentConfigure().Id)
			{
				InitPveTalent();
			}
			bool flag = _pveNurturanceBreakConfigure.UnlockNeedPVELevel == _HeroCardData.PveData.Level && _HeroCardData.PveData.IsTalentUnlock(_pveNurturanceBreakConfigure.Id);
			base.ui.com_File.com_FileBreakThough.talentStatus.selectedIndex = (flag ? 1 : 0);
			base.ui.com_File.com_FileBreakThough.ConditionsMet.selectedIndex = ((_pveNurturanceBreakConfigure.UnlockNeedPVELevel == _HeroCardData.PveData.Level) ? 1 : 0);
			base.ui.com_File.com_FileSkill.Talent.selectedIndex = (flag ? 1 : 0);
			base.ui.com_File.com_FileBreakThough.btn_talentBreak.grayed = !CheckMaterialsMeet();
		}
	}

	private void RendererTalentMaterials(int index, GObject item)
	{
		if (item is UIHero_Com_TalentItem { com_Item: UICom_Item com_Item } uIHero_Com_TalentItem)
		{
			KeyValuePair<int, int> itemData = _pveTalentMaterials[index];
			int curNum = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemData.Key);
			uIHero_Com_TalentItem.txt_Num.SetVar("color", (itemData.Value > curNum) ? "FF0000" : "00FF00").SetVar("curNum", curNum.ToString()).SetVar("target", itemData.Value.ToString())
				.FlushVars();
			ItemInfoConfigure itemInfoConfigure = itemData.Key.GetItemInfoConfigure();
			com_Item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
			com_Item.loader_Icon.url = itemInfoConfigure.ShowIcon;
			com_Item.isShowNum.selectedIndex = 1;
			item.onClick.Set((EventCallback0)delegate
			{
				OpenPropDetail(itemData.Key, curNum);
			});
		}
	}

	private void RefreshTalentMaterials()
	{
		if (base.ui.tab.selectedIndex != 0)
		{
			base.ui.com_File.com_FileBreakThough.list_talentMaterials.numItems = _pveTalentMaterials.Count;
			base.ui.com_File.com_FileBreakThough.btn_talentBreak.grayed = !CheckMaterialsMeet();
		}
	}

	private bool CheckMaterialsMeet()
	{
		foreach (KeyValuePair<int, int> pveTalentMaterial in _pveTalentMaterials)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(pveTalentMaterial.Key) < pveTalentMaterial.Value)
			{
				return false;
			}
		}
		return true;
	}
}
