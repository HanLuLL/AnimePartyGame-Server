using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class MailPanel : BasePanel<UIMailPanel>
{
	private List<MailData> ShowMails;

	private int _mailSelectedIndex = -1;

	private MailData selectedMail;

	public MailPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIMailPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Mail.SetVirtual();
		base.ui.list_Mail.itemRenderer = RendererMail;
		base.ui.list_Reward.itemRenderer = RendererRewards;
	}

	public override void Refresh()
	{
		_mailSelectedIndex = 0;
		ShowMails = SimpleSingletonProvider<GameLogicManager>.inst.mail._container.TryGetMails();
		base.ui.list_Mail.numItems = ShowMails.Count;
		base.ui.list_Mail.scrollPane.percY = 0f;
		base.ui.Status.selectedIndex = ((ShowMails.Count > 0) ? 1 : 0);
		if (ShowMails.Count > 0)
		{
			selectedMail = ShowMails[0];
			RefreshMailInfo();
		}
		base.ui.txt_MailNum.SetVar("cur", ShowMails.Count.ToString()).SetVar("max", "200").FlushVars();
	}

	public override void InitTouchable()
	{
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.mail._container.MailRewardStatus();
		base.ui.btn_All.touchable = flag;
		base.ui.btn_All.grayed = !flag;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_All.onClick.Add(OnRequestAllMail);
		base.ui.btn_Reward.onClick.Add(OnRequestReward);
		base.ui.btn_Delete.onClick.Add(OnRequestDeleteMail);
		base.ui.com_mailDesc.GetTextField().onClickLink.Add(OpenURL);
		base.ui.btn_Collect.onClick.Add(OnChangeCollected);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_All.onClick.Remove(OnRequestAllMail);
		base.ui.btn_Reward.onClick.Remove(OnRequestReward);
		base.ui.btn_Delete.onClick.Remove(OnRequestDeleteMail);
		base.ui.com_mailDesc.GetTextField().onClickLink.Remove(OpenURL);
		base.ui.btn_Collect.onClick.Remove(OnChangeCollected);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
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
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void OnRequestAllMail(EventContext context)
	{
		base.ui.btn_All.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.mail.RequestMailGetRewardC2S(0).OnFinishedOnly.AddOnce(delegate
		{
			InitTouchable();
			Refresh();
			base.ui.btn_All.onClick.Release();
		});
	}

	private void OnRequestReward(EventContext context)
	{
		if (!selectedMail.Available)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1043);
			Refresh();
			return;
		}
		base.ui.btn_Reward.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.mail.RequestMailGetRewardC2S(selectedMail.Id).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			InitTouchable();
			if (_.errId != 0)
			{
				Refresh();
				base.ui.btn_Reward.onClick.Release();
			}
			else
			{
				base.ui.list_Mail.RefreshVirtualList();
				RefreshMailInfo();
				base.ui.btn_Reward.onClick.Release();
			}
		});
	}

	private void OnRequestDeleteMail(EventContext context)
	{
		base.ui.btn_Delete.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.mail.RequestMailDelReadC2S(0).OnFinishedOnly.AddOnce(delegate
		{
			Refresh();
			base.ui.btn_Delete.onClick.Release();
		});
	}

	private void OpenURL(EventContext context)
	{
		base.ui.com_mailDesc.GetTextField().onClickLink.Retain();
		if (context.data is string text)
		{
			string[] array = text.Split('_');
			if (array.Length == 3 && array[0].Equals("Item"))
			{
				if (int.TryParse(array[1], out var result) && int.TryParse(array[2], out var result2))
				{
					SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(result, result2, _Usable: false).Forget();
				}
			}
			else if (array.Length == 2 && array[0].Equals("Survey"))
			{
				if (int.TryParse(array[1], out var result3))
				{
					SimpleSingletonProvider<UIManager>.inst.SurveyCenter.ShowSurveyCenter(result3).Forget();
				}
			}
			else
			{
				Application.OpenURL(text);
			}
		}
		base.ui.com_mailDesc.GetTextField().onClickLink.Release();
	}

	private void OnChangeCollected(EventContext context)
	{
		if (selectedMail == null)
		{
			return;
		}
		base.ui.btn_Collect.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.mail.RequestMailStarS2C(selectedMail.Id, !selectedMail.CollectedStatus).OnFinishedOnly.AddOnce(delegate
		{
			ChangeCollectedStatus(base.ui.btn_Collect, selectedMail.CollectedStatus);
			int index = base.ui.list_Mail.ItemIndexToChildIndex(_mailSelectedIndex);
			if (base.ui.list_Mail.GetChildAt(index) is UIMail_Button_Label btn_Mail)
			{
				RefreshMailItemCollectedStatus(btn_Mail, selectedMail.CollectedStatus);
			}
			base.ui.btn_Collect.onClick.Release();
		});
	}

	private void RendererMail(int index, GObject item)
	{
		UIMail_Button_Label btn_Mail = item as UIMail_Button_Label;
		if (btn_Mail == null)
		{
			return;
		}
		MailData mailData = ShowMails[index];
		btn_Mail.customGray.selectedIndex = (mailData.IsFinish() ? 1 : 0);
		btn_Mail.txt_mailTitle.text = mailData.Title;
		btn_Mail.txt_mailSender.text = mailData.SendName;
		btn_Mail.txt_CreateTime.text = mailData.FormatTime;
		btn_Mail.txt_deadlineTime.text = mailData.DeadTimeDesc;
		btn_Mail.selected = index == _mailSelectedIndex;
		if (mailData.Rewards != null && mailData.Rewards.Count > 0)
		{
			KeyValuePair<int, int> keyValuePair = mailData.Rewards[0];
			ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
			if (itemInfoConfigure == null)
			{
				return;
			}
			((UICom_Item)btn_Mail.com_Item).loader_Icon.url = itemInfoConfigure.ShowIcon;
			((UICom_Item)btn_Mail.com_Item).qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
			((UICom_Item)btn_Mail.com_Item).txt_itemNum.text = keyValuePair.Value.ToString();
			btn_Mail.reward.selectedIndex = 0;
		}
		else
		{
			btn_Mail.reward.selectedIndex = 1;
		}
		RefreshMailItemCollectedStatus(btn_Mail, mailData.CollectedStatus);
		btn_Mail.selected = index == _mailSelectedIndex;
		btn_Mail.onClick.Set((EventCallback0)delegate
		{
			btn_Mail.onClick.Retain();
			_mailSelectedIndex = index;
			selectedMail = mailData;
			RefreshMailInfo();
			if (mailData.IsNeedRead())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.mail.RequestMailReadC2S(mailData.Id).OnFinishedOnly.AddOnce(delegate
				{
					selectedMail.UpdateIsReadStatus();
					btn_Mail.customGray.selectedIndex = (selectedMail.IsFinish() ? 1 : 0);
				});
			}
			btn_Mail.onClick.Release();
			base.ui.list_Mail.RefreshVirtualList();
		});
	}

	private void RendererRewards(int index, GObject item)
	{
		UICom_Item btn_item = item as UICom_Item;
		if (btn_item == null)
		{
			return;
		}
		btn_item.grayed = selectedMail.IsFinish();
		KeyValuePair<int, int> elements = selectedMail.Rewards[index];
		ItemInfoConfigure _ItemConfig = elements.Key.GetItemInfoConfigure();
		if (_ItemConfig != null)
		{
			btn_item.Cut_in.Play();
			btn_item.loader_Icon.url = _ItemConfig.ShowIcon;
			btn_item.txt_itemNum.text = elements.Value.ToString();
			btn_item.qualityType.selectedIndex = (int)_ItemConfig.QualityType;
			btn_item.onClick.Set((EventCallback0)delegate
			{
				btn_item.onClick.Retain();
				OpenPropDetail(_ItemConfig.Id, elements.Value);
				btn_item.onClick.Release();
			});
		}
	}

	private async void OpenPropDetail(int itemId, int count)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, count, _Usable: false);
	}

	private void RefreshMailInfo()
	{
		base.ui.txt_mailTitle.text = selectedMail.Title;
		base.ui.com_mailDesc.title = selectedMail.Context;
		base.ui.com_mailDesc.scrollPane.percY = 0f;
		base.ui.txt_MailSender.text = selectedMail.SendName;
		DateTime dateTime = (((selectedMail.StartTime == 0L) ? selectedMail.CreateTime : selectedMail.StartTime) * 1000).StampMillisecondsToDateTime();
		base.ui.txt_MailTime.text = $"{dateTime.Year:D}-{dateTime.Month:D2}-{dateTime.Day:D2}";
		base.ui.list_Reward.numItems = selectedMail.Rewards.Count;
		base.ui.mailType.selectedIndex = ((selectedMail.Rewards.Count > 0) ? 1 : 0);
		base.ui.btn_Reward.touchable = !selectedMail.IsFinish();
		base.ui.btn_Reward.grayed = selectedMail.IsFinish();
		base.ui.com_mailDesc.GetTextField().SetTextAdaptiveMinHeight(base.ui.com_mailDesc.height);
		ChangeCollectedStatus(base.ui.btn_Collect, selectedMail.CollectedStatus);
		base.ui.btn_Collect.onClick.Release();
	}

	private void RefreshMailItemCollectedStatus(UIMail_Button_Label btn_Mail, bool collectedStatus)
	{
		btn_Mail.btn_Collect.touchable = false;
		if (collectedStatus)
		{
			ChangeCollectedStatus(btn_Mail.btn_Collect, collected: true);
			btn_Mail.btn_Collect.visible = true;
		}
		else
		{
			btn_Mail.btn_Collect.visible = false;
		}
	}

	private void ChangeCollectedStatus(GButton btn, bool collected)
	{
		if (btn is UIButton_Collect uIButton_Collect)
		{
			uIButton_Collect.collected.selectedIndex = (collected ? 1 : 0);
		}
	}
}
