using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class RewardWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private readonly Dictionary<int, RewardsData> rewardsDataDic = new Dictionary<int, RewardsData>
	{
		[0] = new RewardsData
		{
			Type = 0
		},
		[2] = new RewardsData
		{
			Type = 2
		},
		[3] = new RewardsData
		{
			Type = 3
		},
		[4] = new RewardsData
		{
			Type = 4
		}
	};

	private List<KeyValuePair<int, int>> rendererItemList;

	private readonly List<KeyValuePair<int, int>> expiredItems = new List<KeyValuePair<int, int>>();

	private readonly List<KeyValuePair<int, int>> rewardItems = new List<KeyValuePair<int, int>>();

	private RewardsData needShowRewardData
	{
		get
		{
			foreach (KeyValuePair<int, RewardsData> item in rewardsDataDic)
			{
				if (item.Value.needShow())
				{
					return item.Value;
				}
			}
			return null;
		}
	}

	public bool HasNeedShowReward
	{
		get
		{
			bool result = false;
			foreach (KeyValuePair<int, RewardsData> item in rewardsDataDic)
			{
				if (item.Value.needShow())
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public RewardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRewardWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			uIRewardWindow.mohu.onClick.Add(OnMohuClick);
			uIRewardWindow.com_MonthCard.AddEvent();
			uIRewardWindow.com_MonthCard.mohu.onClick.Add(ShowMonthCardReward);
			ExpiredReward_Show();
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			blurBgCtrl.OnShown(this);
			uIRewardWindow.mohu.color = new Color(0f, 0f, 0f, 0.3f);
			uIRewardWindow.list_Prop.opaque = false;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (!(base.contentPane is UIRewardWindow uIRewardWindow))
		{
			return;
		}
		uIRewardWindow.mohu.onClick.Remove(OnMohuClick);
		uIRewardWindow.list_Prop.numItems = 0;
		uIRewardWindow.com_MonthCard.list_Prop.numItems = 0;
		uIRewardWindow.com_MonthCard.mohu.onClick.Remove(ShowMonthCardReward);
		foreach (KeyValuePair<int, RewardsData> item in rewardsDataDic)
		{
			item.Value.itemList.Clear();
			item.Value.autoTransformResult.Clear();
		}
		ExpiredReward_Hide();
		SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
		blurBgCtrl.OnHide();
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (!(base.contentPane is UIRewardWindow) || !base.isShowing || context.inputEvent.keyCode != KeyCode.Escape)
		{
			return;
		}
		Hide();
		foreach (KeyValuePair<int, RewardsData> item in rewardsDataDic)
		{
			item.Value.showQueue.Clear();
			item.Value.autoTransformResultQueue.Clear();
		}
	}

	private async UniTask TryShowAsync(int _type, int _titleId = 1008)
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
		if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			uIRewardWindow.type.selectedIndex = ((_type != 4) ? _type : 0);
			switch (_type)
			{
			case 0:
				ShowCommonReward(_titleId);
				break;
			case 2:
				ShowMonthCardReward();
				break;
			case 3:
				ShowExpiredTransform();
				break;
			case 4:
				ShowTransformReward(_titleId);
				break;
			}
		}
	}

	private void RendererReward(int index, GObject item)
	{
		if (item is UIReward_PropItem { btn_Item: UICom_LitItem btn_Item })
		{
			ItemInfoConfigure _config = rendererItemList[index].Key.GetItemInfoConfigure();
			btn_Item.qualityType.selectedIndex = (int)_config.QualityType;
			btn_Item.loader_Icon.url = _config.ShowIcon;
			btn_Item.txt_itemNum.text = rendererItemList[index].Value.ToString();
			btn_Item.Cut_in.Play();
			btn_Item.onClick.Set((EventCallback0)delegate
			{
				item.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(_config.Id, rendererItemList[index].Value, _Usable: false).Forget();
				item.onClick.Release();
			});
		}
	}

	private void RendererAutoTransformReward(int index, GObject item)
	{
		UICom_LitItem litItem = (UICom_LitItem)((UIReward_PropItem)item).btn_Item;
		if (index >= rewardsDataDic[4].autoTransformResult.Count)
		{
			return;
		}
		AutoTransformRewardsData autoTransformRewardsData = rewardsDataDic[4].autoTransformResult[index];
		if (autoTransformRewardsData == null)
		{
			Debug.LogError($"自动转换奖励 {rendererItemList[index].Key} 不存在AutoTransformRewardsData");
			return;
		}
		KeyValuePair<int, int> item2 = autoTransformRewardsData.Item;
		ItemInfoConfigure itemInfoConfigure = item2.Key.GetItemInfoConfigure();
		litItem.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		litItem.loader_Icon.url = itemInfoConfigure.ShowIcon;
		litItem.txt_itemNum.text = item2.Value.ToString();
		litItem.Cut_in.Stop();
		litItem.showReplace.Stop();
		litItem.LoopReplace.Stop();
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(litItem.graph_ReplaceEffect);
		KeyValuePair<int, int> detailItem = item2;
		if (autoTransformRewardsData.GainItem.Key != 0)
		{
			ItemInfoConfigure itemInfoConfigure2 = autoTransformRewardsData.GainItem.Key.GetItemInfoConfigure();
			litItem.loader_replaceItem.url = itemInfoConfigure2.ShowIcon;
			litItem.txt_itemNum_Replace.text = autoTransformRewardsData.GainItem.Value.ToString();
			litItem.showReplace.SetHook("replaceEffect", delegate
			{
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_Repeat_Ssr", litItem.graph_ReplaceEffect, 45f).Forget();
			});
			litItem.Cut_in.Play(delegate
			{
				litItem.showReplace.Play(delegate
				{
					litItem.LoopReplace.Play(-1, 1f, null);
				});
			});
			detailItem = autoTransformRewardsData.GainItem;
		}
		else
		{
			litItem.Cut_in.Play();
		}
		litItem.onClick.Set((EventCallback0)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(detailItem.Key, detailItem.Value, _Usable: false).Forget();
		});
	}

	private void OnMohuClick()
	{
		ShowCommonReward();
	}

	private async void ShowCommonReward(int _titleId = 1008)
	{
		RewardsData rewardsData = rewardsDataDic[0];
		if (!rewardsDataDic[0].needShow())
		{
			if (needShowRewardData != null)
			{
				await TryShowAsync(needShowRewardData.Type, _titleId);
			}
			else
			{
				Hide();
			}
		}
		else if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			rewardsData.itemList.Clear();
			rewardsData.itemList.AddRange(rewardsData.showQueue.Dequeue());
			rendererItemList = SimpleSingletonProvider<GameLogicManager>.inst.bag.PropItemsSort(rewardsData.itemList);
			uIRewardWindow.list_Prop.itemRenderer = RendererReward;
			uIRewardWindow.list_Prop.numItems = rendererItemList.Count;
			uIRewardWindow.Title.text = _titleId.GetLocal(UIStringType.GUI);
		}
	}

	private async void ShowTransformReward(int _titleId = 1008)
	{
		RewardsData rewardsData = rewardsDataDic[4];
		if (!rewardsData.needShow())
		{
			if (needShowRewardData != null)
			{
				await TryShowAsync(needShowRewardData.Type, _titleId);
			}
			else
			{
				Hide();
			}
		}
		else if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			rewardsData.itemList.Clear();
			rewardsData.itemList.AddRange(rewardsData.showQueue.Dequeue());
			rewardsData.autoTransformResult = rewardsData.autoTransformResultQueue.Dequeue();
			rendererItemList = rewardsData.itemList;
			uIRewardWindow.list_Prop.itemRenderer = RendererAutoTransformReward;
			uIRewardWindow.list_Prop.numItems = rendererItemList.Count;
			uIRewardWindow.Title.text = _titleId.GetLocal(UIStringType.GUI);
		}
	}

	public async void ShowSingleReward(int itemId, int num)
	{
		if (itemId != 0 && num != 0)
		{
			rewardsDataDic[0].showQueue.Enqueue(new List<KeyValuePair<int, int>>
			{
				new KeyValuePair<int, int>(itemId, num)
			});
			await TryShowAsync(0);
		}
	}

	public async void ShowReward(List<KeyValuePair<int, int>> _itemList, int _titleId = 1008)
	{
		if (_itemList != null && _itemList.Count != 0)
		{
			rewardsDataDic[0].showQueue.Enqueue(_itemList);
			if (!base.isShowing)
			{
				await TryShowAsync(0, _titleId);
			}
		}
	}

	public async UniTask ShowReward(BagItemChangeS2C protoData)
	{
		if (protoData == null || protoData.Item.Count == 0 || protoData.IsNotShow)
		{
			return;
		}
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		foreach (ItemEtc item in protoData.Item)
		{
			if (item.ItemId == 0)
			{
				continue;
			}
			ItemInfoConfigure itemInfoConfigure = item.ItemId.GetItemInfoConfigure();
			if (itemInfoConfigure.ItemType != ItemType.Chest || !itemInfoConfigure.IsAutoOpen)
			{
				int num = item.Count - SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(item.ItemId);
				if (num > 0)
				{
					list.Add(new KeyValuePair<int, int>(item.ItemId, num));
				}
			}
		}
		if (list.Count > 0)
		{
			rewardsDataDic[0].showQueue.Enqueue(list);
			if (!base.isShowing)
			{
				await TryShowAsync(0);
			}
		}
	}

	public async UniTask ShowAutoTransformReward(UseTreasureAutoTransformS2C protoData)
	{
		List<AutoTransformRewardsData> list = new List<AutoTransformRewardsData>();
		foreach (ItemEtc item in protoData.Items)
		{
			list.Add(new AutoTransformRewardsData(item.ItemId, item.Count));
		}
		if (protoData.Transforms != null && protoData.TransformItems != null)
		{
			foreach (ItemTransformPair transform in protoData.Transforms)
			{
				if (transform.Gains.Count != 1)
				{
					Debug.LogError($"重复获得道具转换对应关系错误：ItemId={transform.Replaced.ItemId}，Gains.Count={transform.Gains.Count}");
					foreach (ItemEtc gain in transform.Gains)
					{
						list.Add(new AutoTransformRewardsData(gain.ItemId, gain.Count));
					}
				}
				else
				{
					AutoTransformRewardsData autoTransformRewardsData = new AutoTransformRewardsData(transform.Replaced.ItemId, transform.Replaced.Count);
					ItemEtc itemEtc = transform.Gains[0];
					autoTransformRewardsData.GainItem = new KeyValuePair<int, int>(itemEtc.ItemId, itemEtc.Count);
					list.Add(autoTransformRewardsData);
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		list.Sort();
		List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
		foreach (AutoTransformRewardsData item2 in list)
		{
			list2.Add(item2.Item);
		}
		RewardsData rewardsData = rewardsDataDic[4];
		rewardsData.showQueue.Enqueue(list2);
		rewardsData.autoTransformResultQueue.Enqueue(list);
		if (!base.isShowing)
		{
			await TryShowAsync(4);
		}
	}

	private async void ShowMonthCardReward()
	{
		RewardsData rewardsData = rewardsDataDic[2];
		if (!rewardsData.needShow())
		{
			if (needShowRewardData != null)
			{
				await TryShowAsync(needShowRewardData.Type);
			}
			else
			{
				Hide();
			}
		}
		else if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			rewardsData.itemList.Clear();
			rewardsData.itemList.AddRange(rewardsData.showQueue.Dequeue());
			rendererItemList = rewardsData.itemList;
			uIRewardWindow.com_MonthCard.list_Prop.itemRenderer = RendererReward;
			uIRewardWindow.com_MonthCard.list_Prop.numItems = rendererItemList.Count;
			MonthlyCardData monthlyCardConfig = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig();
			uIRewardWindow.com_MonthCard.txt_Time.text = string.Format(1039.GetLocal(UIStringType.Message), monthlyCardConfig.deadlineDay);
		}
	}

	public async void ShowMonthCard()
	{
		if (!base.isShowing)
		{
			await TryShowAsync(2);
		}
	}

	public void ShowMonthCardAllReward()
	{
		MonthlyCardData monthlyCardConfig = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig();
		KeyValuePair<int, int> item = monthlyCardConfig.monthlyCardConfig.ImmediatelyReward.ElementAt(0);
		KeyValuePair<int, int> item2 = monthlyCardConfig.monthlyCardConfig.DailyReward.ElementAt(0);
		if (rewardsDataDic[2].needShow())
		{
			List<KeyValuePair<int, int>> list = rewardsDataDic[2].showQueue.Peek();
			list.Add(item);
			list.Add(item2);
		}
		else
		{
			rewardsDataDic[2].showQueue.Enqueue(new List<KeyValuePair<int, int>> { item, item2 });
		}
	}

	public void ShowMonthCardPurchaseReward()
	{
		KeyValuePair<int, int> item = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig().monthlyCardConfig.ImmediatelyReward.ElementAt(0);
		if (rewardsDataDic[2].needShow())
		{
			rewardsDataDic[2].showQueue.Peek().Add(item);
			return;
		}
		rewardsDataDic[2].showQueue.Enqueue(new List<KeyValuePair<int, int>> { item });
	}

	public async void ShowReward(BagExpiredTransformNotify model)
	{
		expiredItems.Clear();
		int value;
		int key;
		foreach (KeyValuePair<int, int> expiredItem in model.ExpiredItems)
		{
			expiredItem.Deconstruct(out value, out key);
			int num = value;
			int value2 = key;
			if (num != 0)
			{
				ItemInfoConfigure itemInfoConfigure = num.GetItemInfoConfigure();
				if (itemInfoConfigure.ItemType != ItemType.Chest || !itemInfoConfigure.IsAutoOpen)
				{
					expiredItems.Add(new KeyValuePair<int, int>(num, value2));
				}
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.bag.PropItemsSort(expiredItems);
		rewardItems.Clear();
		foreach (KeyValuePair<int, int> rewardItem in model.RewardItems)
		{
			rewardItem.Deconstruct(out key, out value);
			int num2 = key;
			int value3 = value;
			if (num2 != 0)
			{
				ItemInfoConfigure itemInfoConfigure2 = num2.GetItemInfoConfigure();
				if (itemInfoConfigure2.ItemType != ItemType.Chest || !itemInfoConfigure2.IsAutoOpen)
				{
					rewardItems.Add(new KeyValuePair<int, int>(num2, value3));
				}
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.bag.PropItemsSort(rewardItems);
		if (!base.isShowing)
		{
			await TryShowAsync(3);
		}
	}

	private void ExpiredReward_Show()
	{
		if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			uIRewardWindow.com_ExpiredTransform.mohu.onClick.Add(base.HideImmediately);
		}
	}

	private void ExpiredReward_Hide()
	{
		if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			uIRewardWindow.com_ExpiredTransform.mohu.onClick.Remove(base.HideImmediately);
			uIRewardWindow.com_ExpiredTransform.list_Prop_Expire.numItems = 0;
			uIRewardWindow.com_ExpiredTransform.list_Prop_Transform.numItems = 0;
			expiredItems.Clear();
			rewardItems.Clear();
		}
	}

	private void ShowExpiredTransform()
	{
		if (base.contentPane is UIRewardWindow uIRewardWindow)
		{
			uIRewardWindow.com_ExpiredTransform.list_Prop_Expire.itemRenderer = delegate(int index, GObject item)
			{
				ItemRenderer(index, item, expiredItems);
			};
			uIRewardWindow.com_ExpiredTransform.list_Prop_Expire.numItems = expiredItems.Count;
			uIRewardWindow.com_ExpiredTransform.list_Prop_Transform.itemRenderer = delegate(int index, GObject item)
			{
				ItemRenderer(index, item, rewardItems);
			};
			uIRewardWindow.com_ExpiredTransform.list_Prop_Transform.numItems = rewardItems.Count;
		}
	}

	private void ItemRenderer(int index, GObject item, List<KeyValuePair<int, int>> items)
	{
		if (item is UIReward_PropItem { btn_Item: UICom_LitItem btn_Item })
		{
			ItemInfoConfigure _config = items[index].Key.GetItemInfoConfigure();
			btn_Item.qualityType.selectedIndex = (int)_config.QualityType;
			btn_Item.loader_Icon.url = _config.ShowIcon;
			btn_Item.txt_itemNum.text = items[index].Value.ToString();
			btn_Item.onClick.Set((EventCallback0)delegate
			{
				item.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(_config.Id, items[index].Value, _Usable: false).Forget();
				item.onClick.Release();
			});
		}
	}
}
