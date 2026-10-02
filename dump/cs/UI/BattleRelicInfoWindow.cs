using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class BattleRelicInfoWindow : BaseWindow
{
	private const int RelicGroupSize = 10;

	private readonly List<BattlePlayerData> playersData = new List<BattlePlayerData>();

	private long currentPlayerId;

	private List<int> relicIds;

	private UIBattleRelicInfo_Button_Relic CurrentRelic;

	public BattleRelicInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattleRelicInfoWindow.CreateInstance();
		isAdapter = true;
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		ShowPopup();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.Cut_in.Play();
			ReadyRelicGroupList();
			uIBattleRelicInfoWindow.list_Tab.onClickItem.Add(ShowRelicItems);
			if (uIBattleRelicInfoWindow.list_RelicGroup.scrollPane != null)
			{
				uIBattleRelicInfoWindow.list_RelicGroup.scrollPane.onScroll.Add(OnRelicGroupScroll);
			}
			uIBattleRelicInfoWindow.btn_Left.onClick.Add(LeftRelic);
			uIBattleRelicInfoWindow.btn_Right.onClick.Add(RightRelic);
			uIBattleRelicInfoWindow.btn_Close.onClick.Add(CloseRelic);
			uIBattleRelicInfoWindow.btn_Chat.onClick.Add(ChatRelic);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.list_Tab.onClickItem.Remove(ShowRelicItems);
			if (uIBattleRelicInfoWindow.list_RelicGroup.scrollPane != null)
			{
				uIBattleRelicInfoWindow.list_RelicGroup.scrollPane.onScroll.Remove(OnRelicGroupScroll);
			}
			uIBattleRelicInfoWindow.btn_Left.onClick.Remove(LeftRelic);
			uIBattleRelicInfoWindow.btn_Right.onClick.Remove(RightRelic);
			uIBattleRelicInfoWindow.btn_Close.onClick.Remove(CloseRelic);
			uIBattleRelicInfoWindow.btn_Chat.onClick.Remove(ChatRelic);
			ClearCurrentRelic(uIBattleRelicInfoWindow);
			uIBattleRelicInfoWindow.list_RelicGroup.numItems = 0;
		}
	}

	private void CloseRelic(EventContext context)
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.btn_Close.onClick.Retain();
			uIBattleRelicInfoWindow.Cut_out.Play(base.Hide);
			uIBattleRelicInfoWindow.btn_Close.onClick.Release();
		}
	}

	public async void ShowRelic()
	{
		playersData.Clear();
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		int defaultIndex = 0;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].characterType == CharacterType.Hero)
			{
				playersData.Add(playerDatas[i]);
				if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerDatas[i].player.Id))
				{
					defaultIndex = playerDatas[i].player.Slot;
				}
			}
		}
		await TryShow();
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.btn_Left.vailStatus.selectedIndex = 1;
			uIBattleRelicInfoWindow.btn_Right.vailStatus.selectedIndex = 1;
			if (PlatformTarget.IsMobileTarget)
			{
				uIBattleRelicInfoWindow.list_Tab.defaultItem = "ui://ethkhr1hfcnul";
			}
			else
			{
				uIBattleRelicInfoWindow.list_Tab.defaultItem = "ui://ethkhr1hot0we";
			}
			uIBattleRelicInfoWindow.list_Tab.itemRenderer = delegate(int index, GObject item)
			{
				item.icon = playersData[index].player.characterConfig.CharacterMap;
			};
			uIBattleRelicInfoWindow.list_Tab.numItems = playersData.Count;
			uIBattleRelicInfoWindow.list_Tab.selectedIndex = Mathf.Min(defaultIndex, playersData.Count - 1);
			uIBattleRelicInfoWindow.list_Tab.onClickItem.Call();
		}
	}

	private void ReadyRelicGroupList()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.list_RelicGroup.defaultItem = "ui://ethkhr1hfcnum";
			if (!uIBattleRelicInfoWindow.list_RelicGroup.isVirtual)
			{
				uIBattleRelicInfoWindow.list_RelicGroup.SetVirtual();
			}
			uIBattleRelicInfoWindow.list_RelicGroup.itemRenderer = RendererRelicGroup;
		}
	}

	private void RendererRelicGroup(int groupIndex, GObject item)
	{
		if (item is UIBattleRelicInfo_Com_RelicGroup uIBattleRelicInfo_Com_RelicGroup)
		{
			int num = groupIndex * 10;
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_0, num);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_1, num + 1);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_2, num + 2);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_3, num + 3);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_4, num + 4);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_5, num + 5);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_6, num + 6);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_7, num + 7);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_8, num + 8);
			RendererRelicItem(uIBattleRelicInfo_Com_RelicGroup.btn_Relic_9, num + 9);
		}
	}

	private void ShowRelicItems()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			BattlePlayerData battlePlayerData = playersData[uIBattleRelicInfoWindow.list_Tab.selectedIndex];
			currentPlayerId = battlePlayerData.player.Id;
			relicIds = battlePlayerData.GetRelicIds();
			uIBattleRelicInfoWindow.btn_Chat.visible = false;
			RendererRelicGroups();
		}
	}

	private void RendererRelicGroups()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			ClearCurrentRelic(uIBattleRelicInfoWindow);
			int numItems = Mathf.Max(1, (relicIds.Count + 10 - 1) / 10);
			uIBattleRelicInfoWindow.list_RelicGroup.numItems = numItems;
			if (uIBattleRelicInfoWindow.list_RelicGroup.scrollPane != null)
			{
				uIBattleRelicInfoWindow.list_RelicGroup.scrollPane.percX = 0f;
			}
			RefreshRelicArrow();
		}
	}

	private void RendererRelicItem(UIBattleRelicInfo_Button_Relic relicItem, int relicIndex)
	{
		GComponent gComponent = base.contentPane;
		UIBattleRelicInfoWindow win = gComponent as UIBattleRelicInfoWindow;
		if (win == null)
		{
			return;
		}
		relicItem.graph_Hover.visible = false;
		relicItem.selected = false;
		if (relicIds.Count > relicIndex)
		{
			RelicInfoConfigure relicInfo = relicIds[relicIndex].GetRelicInfoConfigure();
			((UICom_Relic_Quality)relicItem.com_Quality).quality.selectedIndex = (int)relicInfo.RelicQualityType;
			relicItem.currentPlayerId = currentPlayerId;
			relicItem.data = relicInfo.Id;
			relicItem.touchable = true;
			relicItem.loader_Relic.url = relicInfo.Icon;
			relicItem.loader_Relic.visible = true;
			relicItem.onClick.Set((EventCallback0)delegate
			{
				if (CurrentRelic != null)
				{
					CurrentRelic.selected = false;
				}
				CurrentRelic = relicItem;
				CurrentRelic.selected = true;
				win.txt_Name.text = relicInfo.NameID.GetLocal(UIStringType.Relic);
				win.txt_Desc.text = relicInfo.DescID.GetLocal(UIStringType.Relic);
				win.btn_Chat.visible = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId);
			});
		}
		else
		{
			((UICom_Relic_Quality)relicItem.com_Quality).quality.selectedIndex = 0;
			relicItem.data = null;
			relicItem.touchable = false;
			relicItem.loader_Relic.visible = false;
			relicItem.onClick.Clear();
		}
	}

	private void ClearCurrentRelic(UIBattleRelicInfoWindow win)
	{
		if (CurrentRelic != null)
		{
			CurrentRelic.selected = false;
		}
		CurrentRelic = null;
		win.txt_Name.text = "";
		win.txt_Desc.text = "";
		win.btn_Chat.visible = false;
	}

	private void RefreshRelicArrow()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			ScrollPane scrollPane = uIBattleRelicInfoWindow.list_RelicGroup.scrollPane;
			bool flag = uIBattleRelicInfoWindow.list_RelicGroup.numItems > 1 && scrollPane != null;
			uIBattleRelicInfoWindow.btn_Left.vailStatus.selectedIndex = ((!flag || !(scrollPane.percX > 0.05f)) ? 1 : 0);
			uIBattleRelicInfoWindow.btn_Right.vailStatus.selectedIndex = ((!flag || !(scrollPane.percX < 0.95f)) ? 1 : 0);
		}
	}

	private void OnRelicGroupScroll()
	{
		if (base.contentPane is UIBattleRelicInfoWindow win)
		{
			ClearCurrentRelic(win);
			RefreshRelicArrow();
		}
	}

	private void LeftRelic()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			ScrollPane scrollPane = uIBattleRelicInfoWindow.list_RelicGroup.scrollPane;
			if (scrollPane != null && uIBattleRelicInfoWindow.btn_Left.vailStatus.selectedIndex != 1)
			{
				uIBattleRelicInfoWindow.btn_Left.onClick.Retain();
				ClearCurrentRelic(uIBattleRelicInfoWindow);
				scrollPane.ScrollLeft(1f, ani: true);
				uIBattleRelicInfoWindow.btn_Left.onClick.Release();
			}
		}
	}

	private void RightRelic()
	{
		if (base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			ScrollPane scrollPane = uIBattleRelicInfoWindow.list_RelicGroup.scrollPane;
			if (scrollPane != null && uIBattleRelicInfoWindow.btn_Right.vailStatus.selectedIndex != 1)
			{
				uIBattleRelicInfoWindow.btn_Right.onClick.Retain();
				ClearCurrentRelic(uIBattleRelicInfoWindow);
				scrollPane.ScrollRight(1f, ani: true);
				uIBattleRelicInfoWindow.btn_Right.onClick.Release();
			}
		}
	}

	public void ChatRelic()
	{
		if (CurrentRelic != null && CurrentRelic.selected && CurrentRelic.data is int relicId && base.contentPane is UIBattleRelicInfoWindow uIBattleRelicInfoWindow)
		{
			uIBattleRelicInfoWindow.btn_Chat.onClick.Retain();
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattleRelicMessage msgData = new BattleRelicMessage(selfPlayerData, relicId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, BattleRelicMessage.MarkId);
				uIBattleRelicInfoWindow.btn_Chat.onClick.Release();
			}
		}
	}
}
