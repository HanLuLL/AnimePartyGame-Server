using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIHome_Com_ActivityBanner : GComponent
{
	private float autoScrollTime;

	public bool startScrollStatus;

	private HashSet<UIHome_Button_ActivityBannerItem> cacheGradientBtns = new HashSet<UIHome_Button_ActivityBannerItem>();

	private readonly List<BannerData> bannerDataList = new List<BannerData>();

	public GList list_Activity;

	public GList list_Page;

	public const string URL = "ui://u7xbdcguheywq3n";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Activity.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Activity.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Activity.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		list_Activity.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Activity.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		list_Activity.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Activity.SetVirtualAndLoop();
		list_Activity.scrollPane.decelerationRate = 0.05f;
		list_Activity.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		bannerDataList.Clear();
		ReadyBannerData();
		list_Activity.numItems = bannerDataList.Count;
		if (bannerDataList.Count > 1)
		{
			list_Activity.scrollPane.touchEffect = true;
			list_Page.numItems = bannerDataList.Count;
			startScrollStatus = true;
			list_Activity.scrollPane.onScroll.Call();
		}
		else
		{
			list_Page.numItems = 0;
			startScrollStatus = false;
			list_Activity.scrollPane.touchEffect = false;
		}
		base.visible = bannerDataList.Count > 0;
	}

	private async UniTask ChangeTexMaterial(UIHome_Button_ActivityBannerItem btn)
	{
		Material mat_1 = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial("GradientTex_1");
		Material original = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial("GradientTex_SDF_1");
		if (!cacheGradientBtns.Contains(btn))
		{
			cacheGradientBtns.Add(btn);
			btn.txt_Time.displayObject.material = Object.Instantiate(original);
			btn.GetChildAt(4).asTextField.displayObject.material = Object.Instantiate(mat_1);
		}
	}

	public void AddEvent()
	{
		list_Activity.scrollPane.onScroll.Add(ScrollActivity);
		base.onRollOver.Add(StopAutoScroll);
		base.onRollOut.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Activity.scrollPane.onScroll.Remove(ScrollActivity);
		base.onRollOver.Remove(StopAutoScroll);
		base.onRollOut.Remove(StartAutoScroll);
	}

	private void ScrollActivity(EventContext context)
	{
		if (list_Activity.numItems > 0)
		{
			int selectedIndex = list_Activity.scrollPane.currentPageX % list_Activity.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private void RefreshActivityButton(int index, GObject item)
	{
		UIHome_Button_ActivityBannerItem btn = item as UIHome_Button_ActivityBannerItem;
		if (btn == null)
		{
			return;
		}
		btn.title = bannerDataList[index].Title;
		btn.loader_Icon.url = bannerDataList[index].Url;
		btn.txt_Time.text = bannerDataList[index].TxtTime;
		btn.onClick.Set((EventCallback0)delegate
		{
			btn.onClick.Retain();
			if (bannerDataList[index].BannerType == ActivityBannerType.Collaborate)
			{
				OpenCollaborate().Forget();
			}
			else if (bannerDataList[index].BannerType == ActivityBannerType.SkinSell)
			{
				OpenSkinSell().Forget();
			}
			else if (bannerDataList[index].BannerType == ActivityBannerType.Light)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.home.OpenActivity(bannerDataList[index].ActivityEntrance).Forget();
			}
			btn.onClick.Release();
		});
	}

	private void ReadyBannerData()
	{
		CollaborationInfoConfigure collaborationInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
		if (collaborationInfoConfigure != null)
		{
			BannerData item = new BannerData
			{
				Title = collaborationInfoConfigure.EntranceTitle.GetLocal(UIStringType.Collaboration),
				Url = collaborationInfoConfigure.Entrance,
				TxtTime = TimeHelper.GetDurationText(collaborationInfoConfigure.BeginTime, collaborationInfoConfigure.EndTime, OnlyDuration: true),
				BannerType = ActivityBannerType.Collaborate
			};
			bannerDataList.Add(item);
		}
		SkinSellData skinComboInfo = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo();
		if (skinComboInfo != null)
		{
			BannerData item2 = new BannerData
			{
				Title = skinComboInfo.skinSellConfig.EntranceTitle.GetLocal(UIStringType.SkinSell),
				Url = skinComboInfo.skinSellConfig.Entrance,
				TxtTime = TimeHelper.GetDurationText(skinComboInfo.BeginTime, skinComboInfo.EndTime, OnlyDuration: true),
				BannerType = ActivityBannerType.SkinSell
			};
			bannerDataList.Add(item2);
		}
		List<ActivityActivityEntrance2Configure> activityInfos = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfos(3);
		if (activityInfos.Count <= 0)
		{
			return;
		}
		foreach (ActivityActivityEntrance2Configure item4 in activityInfos)
		{
			LightActivityData lightGiftActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetLightGiftActivityData(item4.InfoConfig.Id);
			BannerData item3 = new BannerData
			{
				Title = item4.Title.GetLocal(UIStringType.Activity),
				Url = item4.Icon,
				TxtTime = lightGiftActivityData.GetDurationText(),
				ActivityEntrance = item4,
				BannerType = ActivityBannerType.Light
			};
			bannerDataList.Add(item3);
		}
	}

	private async UniTask OpenSkinSell()
	{
		SkinSellData skinComboInfo = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo();
		if (skinComboInfo != null)
		{
			if (skinComboInfo.skinSellConfig.SplitSale.Count > 0)
			{
				await SimpleSingletonProvider<UIManager>.inst.Anniversary_2nd.ShowSkinSell(skinComboInfo);
			}
			else
			{
				await SimpleSingletonProvider<UIManager>.inst.skinSell.ShowSkinSell(skinComboInfo);
			}
		}
	}

	private async UniTask OpenCollaborate()
	{
		CollaborationInfoConfigure collaborationInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
		if (collaborationInfoConfigure.UIWindowType == UIWindowType.WitchWeapon)
		{
			await SimpleSingletonProvider<UIManager>.inst.witchWeapon.ShowWitchWeaponSkin(collaborationInfoConfigure.Id);
		}
		else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Va11HallA)
		{
			await SimpleSingletonProvider<UIManager>.inst.VA11HallA.ShowVA11HallASkin(collaborationInfoConfigure);
		}
		else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Ngostore)
		{
			await SimpleSingletonProvider<UIManager>.inst.ngoStore.ShowNGOHero(collaborationInfoConfigure);
		}
		else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Mgwtstore)
		{
			await SimpleSingletonProvider<UIManager>.inst.mgwtStoreWindow.ShowMGWTStore(collaborationInfoConfigure);
		}
	}

	public static UIHome_Com_ActivityBanner CreateInstance()
	{
		return (UIHome_Com_ActivityBanner)UIPackage.CreateObject("Home", "Home_Com_ActivityBanner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Activity = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
	}
}
