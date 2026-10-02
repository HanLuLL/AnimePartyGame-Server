using System;
using System.Collections.Generic;
using Core.Audio;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class MGWTStoreWindow : BaseWindow
{
	private CollaborationInfoConfigure InfoConfig;

	private Action CloseCallBack;

	private List<int> PreviewIndex = new List<int>();

	private const int EventId = 178;

	public MGWTStoreWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIMGWTStoreWindow.CreateInstance();
		base.OnInit();
		if (base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow)
		{
			uIMGWTStoreWindow.com_Goods._extraAward = UIMGWT_Com_ExtraAward.CreateInstance();
			uIMGWTStoreWindow.com_Goods._extraAward.Init();
		}
	}

	public async UniTask ShowMGWTStore(CollaborationInfoConfigure infoConfig, Action _CloseCallBack = null)
	{
		CloseCallBack = _CloseCallBack;
		InfoConfig = infoConfig;
		await TryShowAsync();
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
		if (base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow)
		{
			uIMGWTStoreWindow.com_Advent.Init(InfoConfig, ShowGoodsItem);
			uIMGWTStoreWindow.com_Advent.AddEvent();
			uIMGWTStoreWindow.com_Goods.Init(InfoConfig);
			uIMGWTStoreWindow.com_Goods.AddEvent();
			uIMGWTStoreWindow.btn_Close.onClick.Add(OnReturn);
			ShowHero();
			BGMHelper.TryPlayBGM(178);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow)
		{
			uIMGWTStoreWindow.btn_Close.onClick.Remove(OnReturn);
			uIMGWTStoreWindow.com_Advent.RemoveEvent();
			uIMGWTStoreWindow.com_Goods.RemoveEvent();
			CloseCallBack?.Invoke();
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.PlayBGM();
		}
	}

	private void ShowHero()
	{
		if (base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow)
		{
			uIMGWTStoreWindow.page.selectedIndex = 0;
			uIMGWTStoreWindow.com_Advent.OnShow();
		}
	}

	private void ShowGoodsItem()
	{
		if (!(base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow))
		{
			return;
		}
		uIMGWTStoreWindow.page.selectedIndex = 1;
		PreviewIndex.Clear();
		List<int> list = new List<int>();
		if (InfoConfig == null || InfoConfig.PreviewIndex == null)
		{
			return;
		}
		for (int i = 0; i < InfoConfig.PreviewIndex.Count; i++)
		{
			CollaborationGoodsConfigure collaborationGoodsConfigure = InfoConfig.PreviewIndex[i].GetCollaborationGoodsConfigure();
			bool flag = true;
			foreach (int item in collaborationGoodsConfigure.HeroID)
			{
				HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(item);
				if (cardData == null || !cardData.IsHas)
				{
					PreviewIndex.Add(InfoConfig.PreviewIndex[i]);
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(InfoConfig.PreviewIndex[i]);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			PreviewIndex.Add(list[j]);
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

	private void OnReturn()
	{
		if (base.contentPane is UIMGWTStoreWindow uIMGWTStoreWindow)
		{
			uIMGWTStoreWindow.btn_Close.onClick.Retain();
			if (uIMGWTStoreWindow.page.selectedIndex == 1)
			{
				ShowHero();
			}
			else
			{
				Hide();
			}
			uIMGWTStoreWindow.btn_Close.onClick.Release();
		}
	}
}
