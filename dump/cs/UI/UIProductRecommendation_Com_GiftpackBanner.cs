using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIProductRecommendation_Com_GiftpackBanner : GComponent
{
	private float autoScrolTime;

	private bool startScrollStatus;

	private readonly List<ProductRecommendationBannerConfigure> bannerConfigList = new List<ProductRecommendationBannerConfigure>();

	public GList list_Activity;

	public GList list_Page;

	public const string URL = "ui://z8uldgkyqdq54";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Activity.numItems > 1)
		{
			if (autoScrolTime >= 2f)
			{
				list_Activity.scrollPane.ScrollRight(1f, ani: true);
				autoScrolTime = 0f;
			}
			autoScrolTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Activity.onTouchEnd.Retain();
		autoScrolTime = 0f;
		startScrollStatus = true;
		list_Activity.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Activity.onTouchBegin.Retain();
		autoScrolTime = 0f;
		startScrollStatus = false;
		list_Activity.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Activity.SetVirtualAndLoop();
		list_Activity.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		bannerConfigList.Clear();
		foreach (ProductRecommendationBannerConfigure banner in StaticConfigure.ProductRecommendation.Banners)
		{
			if (TimeHelper.ValidityTime(banner.BeginTime, banner.EndTime))
			{
				bannerConfigList.Add(banner);
			}
		}
		bannerConfigList.Sort((ProductRecommendationBannerConfigure xBanner, ProductRecommendationBannerConfigure yBanner) => xBanner.OrderWeight.CompareTo(yBanner.OrderWeight));
		list_Activity.numItems = bannerConfigList.Count;
		list_Page.numItems = bannerConfigList.Count;
		startScrollStatus = true;
		list_Activity.scrollPane.onScroll.Call();
	}

	public void AddEvent()
	{
		list_Activity.scrollPane.onScroll.Add(ScrollActivity);
		list_Activity.onTouchBegin.Add(StopAutoScroll);
		list_Activity.onTouchEnd.Add(StartAutoScroll);
	}

	public void RemoveEvent()
	{
		list_Activity.scrollPane.onScroll.Remove(ScrollActivity);
		list_Activity.onTouchBegin.Remove(StopAutoScroll);
		list_Activity.onTouchEnd.Remove(StartAutoScroll);
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
		UIProductRecommendation_Button_GiftpackBanner btn = item as UIProductRecommendation_Button_GiftpackBanner;
		if (btn != null)
		{
			RepeatedField<string> bannerImages = bannerConfigList[index].BannerImages;
			if (GameSettings.angelMode && bannerConfigList[index].BannerImagesSfw.Count > 0)
			{
				bannerImages = bannerConfigList[index].BannerImagesSfw;
			}
			btn.loader_Image.url = GetBanner(bannerImages);
			if (bannerConfigList[index].DescId1 != 0)
			{
				btn.txt_Title_1.text = bannerConfigList[index].DescId1.GetLocal(UIStringType.ProductRecommendation);
				btn.txt_Title_3.text = bannerConfigList[index].DescId1.GetLocal(UIStringType.ProductRecommendation);
			}
			if (bannerConfigList[index].DescId2 != 0)
			{
				btn.txt_Title_2.text = bannerConfigList[index].DescId2.GetLocal(UIStringType.ProductRecommendation);
			}
			int selectedIndex = ((bannerConfigList[index].Style != 0) ? (bannerConfigList[index].Style - 1) : 0);
			btn.style.selectedIndex = selectedIndex;
			btn.onClick.Set((EventCallback0)delegate
			{
				SkipTargetPanel(btn, bannerConfigList[index]);
			});
		}
	}

	private string GetBanner(RepeatedField<string> BannerImages)
	{
		if (BannerImages.Count == 0)
		{
			return "";
		}
		if (BannerImages.Count == 1)
		{
			return BannerImages[0];
		}
		return GameSettings.GetDataForLanguage(BannerImages[1], BannerImages[2], BannerImages[0], BannerImages[3]);
	}

	private async void SkipTargetPanel(UIProductRecommendation_Button_GiftpackBanner btn, ProductRecommendationBannerConfigure _info)
	{
		btn.onClick.Retain();
		if (StaticConfigure.Way.DataDict.TryGetValue(_info.Way, out var _) && SimpleSingletonProvider<UIManager>.inst.GoWayAvailable(_info.Way))
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(_info.Way);
			btn.onClick.Release();
		}
	}

	public static UIProductRecommendation_Com_GiftpackBanner CreateInstance()
	{
		return (UIProductRecommendation_Com_GiftpackBanner)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Com_GiftpackBanner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Activity = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
	}
}
