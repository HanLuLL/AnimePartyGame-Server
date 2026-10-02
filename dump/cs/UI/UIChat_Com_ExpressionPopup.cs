using System;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIChat_Com_ExpressionPopup : GComponent
{
	private const int SEND_EXPRESSION_INDEX = 0;

	private const int COLLECT_EXPRESSION_INDEX = 1;

	private Action<string> _onRequestSend;

	private List<CommunicateExpressionPack> _packs;

	private UIChat_Button_ExpressionTab _selectedExpressionTab;

	private float _currentSendExpressionRefreshTime;

	public Controller tab;

	public GTextField txt_CurrentTitle;

	public GTextField txt_Collected;

	public GList list_Expression;

	public GList list_ExpressionPageTab;

	public GList list_CollectExpression;

	public UIChat_Button_Pin btn_Pin;

	public GButton btn_Cancel;

	public GButton btn_Sure;

	public Transition curtIn_Send;

	public Transition cutIn_Collect;

	public const string URL = "ui://y0luzhk8ednm16";

	public void RefreshExpression(Action<string> onRequestSend)
	{
		_packs = SimpleSingletonProvider<GameLogicManager>.inst.communicate.GetSortedPackList();
		_onRequestSend = onRequestSend;
		RefreshExpressionTab();
		OpenExpressionByOperate(0);
	}

	private void OpenExpressionByOperate(int tabIndex)
	{
		tab.selectedIndex = tabIndex;
		list_ExpressionPageTab.ScrollToView(0);
		int index = list_ExpressionPageTab.ItemIndexToChildIndex(0);
		if (list_ExpressionPageTab.GetChildAt(index) is UIChat_Button_ExpressionTab uIChat_Button_ExpressionTab)
		{
			uIChat_Button_ExpressionTab.onClick.Call();
		}
	}

	private bool IsRedPoint(int packId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.communicate.IsPackShowRedPoint(packId);
	}

	private bool IsCollectedPage(int packId)
	{
		return packId == SimpleSingletonProvider<GameLogicManager>.inst.communicate.COLLECTED_PACK_ID;
	}

	private bool IsCollectedExpression(int expressionId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.communicate.IsCollected(expressionId);
	}

	private bool IsTop(int packId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.communicate.IsTop(packId);
	}

	private void RefreshExpressionTab()
	{
		list_ExpressionPageTab.SetVirtual();
		list_ExpressionPageTab.itemRenderer = delegate(int index, GObject item)
		{
			UIChat_Button_ExpressionTab btn_Tab = item as UIChat_Button_ExpressionTab;
			if (btn_Tab != null)
			{
				btn_Tab.type.selectedIndex = ((index == 0) ? 1 : 0);
				btn_Tab.com_Expression.PlayExpression(_packs[index].PageExpressionId);
				btn_Tab.btn_Pin.visible = IsTop(_packs[index].PackId);
				btn_Tab.image_RedPoint.visible = IsRedPoint(_packs[index].PackId);
				btn_Tab.onClick.Set((EventCallback0)delegate
				{
					list_ExpressionPageTab.touchable = false;
					if (_selectedExpressionTab != null)
					{
						_selectedExpressionTab.status.selectedIndex = 0;
					}
					_selectedExpressionTab = btn_Tab;
					btn_Tab.status.selectedIndex = 1;
					if (tab.selectedIndex == 0)
					{
						RefreshExpressions(index);
					}
					else
					{
						RefreshExpressionCollected(index);
					}
					SimpleSingletonProvider<GameLogicManager>.inst.communicate.RemovePackRedPoint(_packs[index].PackId);
					btn_Tab.image_RedPoint.visible = IsRedPoint(_packs[index].PackId);
					list_ExpressionPageTab.touchable = true;
				});
			}
		};
		list_ExpressionPageTab.numItems = _packs.Count;
	}

	private void RefreshExpressions(int selectedIndex)
	{
		if (_packs.Count <= selectedIndex)
		{
			return;
		}
		List<int> expressionList = _packs[selectedIndex].GetExpressionList();
		list_Expression.SetVirtual();
		bool isCollectedPage = IsCollectedPage(_packs[selectedIndex].PackId);
		list_Expression.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIChat_Button_ExpressionItem uIChat_Button_ExpressionItem)
			{
				uIChat_Button_ExpressionItem.image_Selected.visible = false;
				if (index == 0 && isCollectedPage && tab.selectedIndex == 0)
				{
					uIChat_Button_ExpressionItem.com_Expression.visible = false;
					uIChat_Button_ExpressionItem.image_CollectTip.visible = true;
					uIChat_Button_ExpressionItem.image_CollectStatus.visible = false;
					uIChat_Button_ExpressionItem.onClick.Set((EventCallback0)delegate
					{
						list_Expression.touchable = false;
						OpenExpressionByOperate(1);
						list_Expression.touchable = true;
					});
				}
				else
				{
					int index2 = ((!isCollectedPage) ? index : (--index));
					int expressionId = expressionList[index2];
					uIChat_Button_ExpressionItem.com_Expression.visible = true;
					uIChat_Button_ExpressionItem.com_Expression.PlayExpression(expressionId);
					uIChat_Button_ExpressionItem.image_CollectTip.visible = false;
					uIChat_Button_ExpressionItem.image_CollectStatus.visible = IsCollectedExpression(expressionId);
					uIChat_Button_ExpressionItem.onClick.Set((EventCallback0)delegate
					{
						list_Expression.touchable = false;
						if (Time.time - _currentSendExpressionRefreshTime > 1f)
						{
							_onRequestSend($"[e:{expressionId}]");
							_currentSendExpressionRefreshTime = Time.time;
						}
						else
						{
							SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1144);
						}
						list_Expression.touchable = true;
					});
				}
			}
		};
		list_Expression.numItems = expressionList.Count + (isCollectedPage ? 1 : 0);
		list_Expression.scrollPane.percY = 0f;
		if (isCollectedPage)
		{
			txt_CurrentTitle.text = 1300002.GetLocal(UIStringType.GUI);
			btn_Pin.visible = false;
			return;
		}
		int packId = _packs[selectedIndex].PackId;
		CharacterInfoConfigure characterConfigure = packId.GetCharacterConfigure();
		if (characterConfigure != null)
		{
			string local = characterConfigure.NickID.GetLocal(UIStringType.Character);
			txt_CurrentTitle.text = string.Format(1300001.GetLocal(UIStringType.GUI), local);
		}
		btn_Pin.visible = true;
		btn_Pin.pin.selectedIndex = (IsTop(packId) ? 1 : 0);
		btn_Pin.onClick.Set((EventCallback0)delegate
		{
			btn_Pin.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.ChangeExpressionPackPinState(packId);
			bool flag = IsTop(packId);
			btn_Pin.pin.selectedIndex = (flag ? 1 : 0);
			if (_selectedExpressionTab != null)
			{
				_selectedExpressionTab.btn_Pin.visible = flag;
			}
			btn_Pin.onClick.Release();
		});
	}

	private void RefreshExpressionCollected(int selectedIndex)
	{
		if (_packs.Count <= selectedIndex)
		{
			return;
		}
		List<int> expressionList = _packs[selectedIndex].GetExpressionList();
		list_CollectExpression.SetVirtual();
		list_CollectExpression.itemRenderer = delegate(int index, GObject item)
		{
			UIChat_Button_ExpressionItem com_ExpressionItem = item as UIChat_Button_ExpressionItem;
			if (com_ExpressionItem != null && expressionList.Count > index)
			{
				int expressionId = expressionList[index];
				com_ExpressionItem.com_Expression.PlayExpression(expressionId);
				bool flag = IsCollectedExpression(expressionId);
				com_ExpressionItem.image_CollectTip.visible = false;
				com_ExpressionItem.image_CollectStatus.visible = flag;
				com_ExpressionItem.image_Selected.visible = flag;
				com_ExpressionItem.onClick.Set((EventCallback0)delegate
				{
					CommunicateLogic communicate = SimpleSingletonProvider<GameLogicManager>.inst.communicate;
					int collectedCount = communicate.GetCollectedCount();
					if (!communicate.IsCollected(expressionId) && StaticGlobalData.MAX_STAR_EXPRESSION_COUNT <= collectedCount)
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1143);
					}
					else
					{
						list_CollectExpression.touchable = false;
						communicate.ChangeExpressionStarState(expressionId, out var collectStatus);
						com_ExpressionItem.image_Selected.visible = collectStatus;
						com_ExpressionItem.image_CollectStatus.visible = collectStatus;
						RefreshExpressionCollectedText();
						list_CollectExpression.touchable = true;
					}
				});
			}
		};
		list_CollectExpression.numItems = expressionList.Count;
		list_CollectExpression.scrollPane.percY = 0f;
		btn_Sure.onClick.Set((EventCallback0)delegate
		{
			SyncStarExpression(sync: true);
		});
		btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			SyncStarExpression(sync: false);
		});
		RefreshExpressionCollectedText();
	}

	private void RefreshExpressionCollectedText()
	{
		int collectedCount = SimpleSingletonProvider<GameLogicManager>.inst.communicate.GetCollectedCount();
		txt_Collected.text = string.Format(1300003.GetLocal(UIStringType.GUI), $"{collectedCount}/{StaticGlobalData.MAX_STAR_EXPRESSION_COUNT}");
	}

	private void SyncStarExpression(bool sync)
	{
		btn_Cancel.onClick.Retain();
		btn_Sure.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.SyncStarExpression(sync);
		OpenExpressionByOperate(0);
		btn_Sure.onClick.Release();
		btn_Cancel.onClick.Release();
	}

	public static UIChat_Com_ExpressionPopup CreateInstance()
	{
		return (UIChat_Com_ExpressionPopup)UIPackage.CreateObject("Chat", "Chat_Com_ExpressionPopup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		txt_CurrentTitle = (GTextField)GetChildAt(3);
		txt_Collected = (GTextField)GetChildAt(4);
		list_Expression = (GList)GetChildAt(5);
		list_ExpressionPageTab = (GList)GetChildAt(6);
		list_CollectExpression = (GList)GetChildAt(7);
		btn_Pin = (UIChat_Button_Pin)GetChildAt(8);
		btn_Cancel = (GButton)GetChildAt(9);
		btn_Sure = (GButton)GetChildAt(10);
		curtIn_Send = GetTransitionAt(0);
		cutIn_Collect = GetTransitionAt(1);
	}
}
