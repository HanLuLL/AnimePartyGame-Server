using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class GachaPanel : BasePanel<UIGachaPanel>
{
	private int _CurrentShowBackstageType;

	private readonly List<GachaBackstageConfigure> _BackstageConfigList = new List<GachaBackstageConfigure>();

	private ExchangeGoods _CostItemGoods;

	private GachaPoolConfigure _PoolConfigData;

	private GachaBackstageConfigure _BackstageConfigure;

	private GachaBackstageConfigure runningPoolData;

	private List<GachaItem> resultItems;

	private bool _Single;

	private readonly List<UIGacha_Button_SliderItem> _progressItems = new List<UIGacha_Button_SliderItem>();

	private int _skinItemId;

	private GachaProgressConfigureItem _showItem;

	public GachaPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIGachaPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0)
		{
			_CurrentShowBackstageType = ((RepeatedField<int>)objs[0])[0];
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.Cut_in.Play();
		base.ui.mohu.SetSize(GRoot.inst.width, base.ui.mohu.height);
		GachaManager.inst.CloseGachaProcess();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_PoolButton.itemRenderer = RendererPoolButton;
		Init_Skin();
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.poolUIStatus.selectedIndex = 0;
		base.ui.gachaProcess.selectedIndex = 0;
		RefreshTab();
		CloseGachaProcess();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_Excharge.onClick.Add(ShowExchangeGoods);
		base.ui.list_PoolButton.onClickItem.Add(RefreshPoolUI);
		base.ui.btn_GachaOne.onClick.Add(OnRequestOneGacha);
		base.ui.btn_GachaMulti.onClick.Add(OnRequestMultiGacha);
		base.ui.btn_GachaMulti_Rookie.onClick.Add(OnRequestMultiGacha);
		base.ui.btn_rookieReward.onClick.Add(ShowGachaRookieReward);
		base.ui.com_Result.btn_GachaAgain.onClick.Add(OnRequestGachaAgain);
		base.ui.com_Result.btn_Sure_Again.onClick.Add(CloseGachaProcess);
		base.ui.com_Result.btn_Sure_NoAgain.onClick.Add(CloseGachaProcess);
		base.ui.btn_Info.onClick.Add(ShowPoolInfo);
		base.ui.btn_Record.onClick.Add(RequestGetRecard);
		AddEvent_Skin();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_Excharge.onClick.Remove(ShowExchangeGoods);
		base.ui.list_PoolButton.onClickItem.Remove(RefreshPoolUI);
		base.ui.btn_GachaOne.onClick.Remove(OnRequestOneGacha);
		base.ui.btn_GachaMulti.onClick.Remove(OnRequestMultiGacha);
		base.ui.btn_GachaMulti_Rookie.onClick.Remove(OnRequestMultiGacha);
		base.ui.btn_rookieReward.onClick.Remove(ShowGachaRookieReward);
		base.ui.com_Result.btn_GachaAgain.onClick.Remove(OnRequestGachaAgain);
		base.ui.com_Result.btn_Sure_Again.onClick.Remove(CloseGachaProcess);
		base.ui.com_Result.btn_Sure_NoAgain.onClick.Remove(CloseGachaProcess);
		base.ui.btn_Info.onClick.Remove(ShowPoolInfo);
		base.ui.btn_Record.onClick.Remove(RequestGetRecard);
		RemoveEvent_Skin();
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.signal.finishGacha.AddListener(ShowItemInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.signal.gachaProgress.AddListener(RefreshProgressByServer);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.signal.finishGacha.RemoveListener(ShowItemInfo);
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.signal.gachaProgress.RemoveListener(RefreshProgressByServer);
	}

	public override void Close()
	{
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(base.ui.loader_UpAnimation);
		CloseGachaProcess();
		_BackstageConfigure = null;
		if (SimpleSingletonProvider<UIManager>.inst.gachaInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.gachaInfo.Hide();
		}
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public override void AdultMode(bool inAdultMode)
	{
		GachaPoolConfigure gachaPoolConfigure = _BackstageConfigList[base.ui.list_PoolButton.selectedIndex].PoolID.GetGachaPoolConfigure();
		base.ui.loader_Pool.url = GetPoolBG(gachaPoolConfigure);
	}

	public bool IsIdleStatus()
	{
		if (base.ui == null)
		{
			return true;
		}
		return base.ui.poolUIStatus.selectedIndex == 0;
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		_CurrentShowBackstageType = 0;
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void RequestGetRecard(EventContext context)
	{
		base.ui.btn_Record.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaRecordS2C((int)_BackstageConfigure.GachaType).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaRecord(_PoolConfigData);
			}
		});
		base.ui.btn_Record.onClick.Release();
	}

	private void ShowGachaRookieReward()
	{
		if (_BackstageConfigure != null)
		{
			SimpleSingletonProvider<UIManager>.inst.GachaRookieReward.ShowGachaRookieReward(_BackstageConfigure.PoolID);
		}
	}

	private async void ShowExchangeGoods(EventContext context)
	{
		base.ui.btn_Excharge.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(_PoolConfigData.Way);
		base.ui.btn_Excharge.onClick.Release();
	}

	private void RefreshTab()
	{
		Dictionary<int, List<int>> BackstageDict = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetBackstageTypesForTable();
		if (!BackstageDict.TryGetValue(1, out var value) || value.Count == 0)
		{
			Debug.LogError("尝试取出角色池失败，需要检查配置");
			return;
		}
		int targetGachaType = GetTargetGachaType(BackstageDict);
		if (IsShowGachaTypeList(BackstageDict))
		{
			base.ui.list_GachaType.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIGacha_Button_TabSelect uIGacha_Button_TabSelect)
				{
					KeyValuePair<int, List<int>> keyValuePair = BackstageDict.ElementAt(index);
					uIGacha_Button_TabSelect.title = keyValuePair.Key.GetLocal(UIStringType.Gacha);
					uIGacha_Button_TabSelect.txt_SubTitle.text = (keyValuePair.Key * 10).GetLocal(UIStringType.Gacha);
					uIGacha_Button_TabSelect.selected = keyValuePair.Key == GetTargetGachaType(BackstageDict);
					List<GachaBackstageConfigure> list = new List<GachaBackstageConfigure>();
					foreach (int item in keyValuePair.Value)
					{
						GachaBackstageConfigure gachaBackstageConfigure = item.GetGachaBackstageConfigure();
						list.Add(gachaBackstageConfigure);
					}
					uIGacha_Button_TabSelect.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetGachaTabRedPoint(list) ? 1 : 0);
				}
			};
			base.ui.list_GachaType.onClickItem.Set((EventCallback0)delegate
			{
				int selectedIndex = base.ui.list_GachaType.selectedIndex;
				if (selectedIndex >= 0 && selectedIndex < BackstageDict.Count)
				{
					KeyValuePair<int, List<int>> keyValuePair = BackstageDict.ElementAt(selectedIndex);
					base.ui.list_GachaType.onClickItem.Retain();
					RefreshGachaByType(keyValuePair.Value);
					base.ui.list_GachaType.onClickItem.Release();
				}
			});
			base.ui.list_GachaType.numItems = BackstageDict.Count;
		}
		else
		{
			base.ui.list_GachaType.numItems = 0;
		}
		RefreshGachaByType(BackstageDict[targetGachaType]);
	}

	private bool IsShowGachaTypeList(Dictionary<int, List<int>> BackstageDict)
	{
		foreach (KeyValuePair<int, List<int>> item in BackstageDict)
		{
			if (item.Key != 1 && item.Value.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	private int GetTargetGachaType(Dictionary<int, List<int>> BackstageDict)
	{
		foreach (KeyValuePair<int, List<int>> item in BackstageDict)
		{
			if (item.Value.Contains(_CurrentShowBackstageType))
			{
				return item.Key;
			}
		}
		return 1;
	}

	private void RefreshGachaByType(List<int> backstageIds)
	{
		_BackstageConfigList.Clear();
		foreach (int backstageId in backstageIds)
		{
			GachaBackstageConfigure gachaBackstageConfigure = backstageId.GetGachaBackstageConfigure();
			GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(gachaBackstageConfigure.PoolID);
			if (gachaBackstageConfigure.GachaType != GachaType.Rookie || !poolProgress.IsFinishRewardByProgress(poolProgress.Progress))
			{
				_BackstageConfigList.Add(gachaBackstageConfigure);
			}
		}
		_BackstageConfigList.Sort((GachaBackstageConfigure x, GachaBackstageConfigure y) => x.TabOrder.CompareTo(y.TabOrder));
		base.ui.list_PoolButton.numItems = _BackstageConfigList.Count;
		GObject[] children = base.ui.list_PoolButton.GetChildren();
		for (int num = 0; num < children.Length && children[num] is UICacha_Button_Pool uICacha_Button_Pool; num++)
		{
			if ((int)uICacha_Button_Pool.data == _CurrentShowBackstageType)
			{
				uICacha_Button_Pool.onClick.Call();
				return;
			}
		}
		base.ui.list_PoolButton.GetChildAt(0).onClick.Call();
	}

	private void RendererPoolButton(int index, GObject item)
	{
		if (item is UICacha_Button_Pool uICacha_Button_Pool)
		{
			uICacha_Button_Pool.data = (int)_BackstageConfigList[index].GachaType;
			GachaPoolConfigure gachaPoolConfigure = _BackstageConfigList[index].PoolID.GetGachaPoolConfigure();
			uICacha_Button_Pool.title = gachaPoolConfigure.NameID.GetLocal(UIStringType.Gacha);
			if (gachaPoolConfigure != null && StaticConfigure.Gacha.ProgressDict.TryGetValue(gachaPoolConfigure.PoolID, out var value))
			{
				uICacha_Button_Pool.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolRedPointStatus(value) ? 1 : 0);
			}
			else
			{
				uICacha_Button_Pool.redPoint.selectedIndex = 0;
			}
		}
	}

	private void RefreshPoolUI(EventContext context)
	{
		GachaBackstageConfigure gachaBackstageConfigure = _BackstageConfigList[base.ui.list_PoolButton.selectedIndex];
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(gachaBackstageConfigure.CurrencyBar);
		if (_BackstageConfigure != null && gachaBackstageConfigure.PoolID == _BackstageConfigure.PoolID)
		{
			return;
		}
		_BackstageConfigure = gachaBackstageConfigure;
		base.ui.list_PoolButton.onClickItem.Retain();
		_CurrentShowBackstageType = (int)_BackstageConfigure.GachaType;
		string showIcon = _BackstageConfigure.CostItem.GetItemInfoConfigure().ShowIcon;
		if (_BackstageConfigure.GachaTableType == GachaTableType.Role)
		{
			base.ui.btn_GachaOne.loader_Icon.url = showIcon;
			base.ui.btn_GachaMulti.loader_Icon.texture = base.ui.btn_GachaMulti.Image_90001_Ten.texture;
			base.ui.btn_GachaOne.txt_Name.SetVar("time", "1").FlushVars();
			base.ui.btn_GachaMulti.txt_Name.SetVar("time", "10").FlushVars();
			base.ui.btn_GachaOne.txt_Count.text = "1";
			base.ui.btn_GachaMulti.txt_Count.text = "10";
			if (_BackstageConfigure.GachaType == GachaType.Rookie)
			{
				base.ui.btn_GachaMulti_Rookie.txt_Name.SetVar("time", "10").FlushVars();
				base.ui.btn_GachaMulti_Rookie.txt_Count.text = "8";
			}
		}
		else if (_BackstageConfigure.GachaTableType == GachaTableType.Skin)
		{
			base.ui.com_Skin.btn_GachaOne.loader_Icon.url = showIcon;
			base.ui.com_Skin.btn_GachaMulti.loader_Icon.url = showIcon;
			base.ui.com_Skin.btn_GachaOne.txt_Name.SetVar("time", "1").FlushVars();
			base.ui.com_Skin.btn_GachaMulti.txt_Name.SetVar("time", "10").FlushVars();
			base.ui.com_Skin.btn_GachaOne.txt_Count.text = "X1";
			base.ui.com_Skin.btn_GachaMulti.txt_Count.text = "X10";
		}
		else if (_BackstageConfigure.GachaTableType == GachaTableType.Replica)
		{
			base.ui.com_Skin_Replica.btn_GachaOne.loader_Icon.url = showIcon;
			base.ui.com_Skin_Replica.btn_GachaMulti.loader_Icon.url = showIcon;
			base.ui.com_Skin_Replica.btn_GachaOne.txt_Name.SetVar("time", "1").FlushVars();
			base.ui.com_Skin_Replica.btn_GachaMulti.txt_Name.SetVar("time", "10").FlushVars();
			base.ui.com_Skin_Replica.btn_GachaOne.txt_Count.text = "X1";
			base.ui.com_Skin_Replica.btn_GachaMulti.txt_Count.text = "X10";
		}
		bool flag = _PoolConfigData != null && _PoolConfigData.Equals(_BackstageConfigure.PoolID.GetGachaPoolConfigure());
		_PoolConfigData = _BackstageConfigure.PoolID.GetGachaPoolConfigure();
		base.ui.loader_Pool.url = GetPoolBG(_PoolConfigData);
		base.ui.txt_PoolTitile.text = _PoolConfigData.NameID.GetLocal(UIStringType.Gacha);
		base.ui.txt_Desc.text = _PoolConfigData.PublicityID.GetLocal(UIStringType.Gacha);
		base.ui.poolType.selectedIndex = 0;
		base.ui.com_Skin.poolType.selectedIndex = 0;
		GachaType gachaType = _BackstageConfigure.GachaType;
		if (gachaType == GachaType.Expansion || gachaType == GachaType.LimitedUp || gachaType == GachaType.LimitedUp2 || gachaType == GachaType.Expansion2 || gachaType == GachaType.Expansion3)
		{
			if (_PoolConfigData.UpItem.Count > 0)
			{
				int num = _PoolConfigData.UpItem[0];
				base.ui.com_UpDesc.txt_UpNick.text = CharacterHandle.GetCharacterNickName(num);
				base.ui.com_UpDesc.txt_UpName.text = CharacterHandle.GetCharacterName(num);
				base.ui.com_UpDesc.txt_UpDesc.text = 11.GetLocal(UIStringType.Gacha);
				RefreshHeroAnimation(num);
			}
			base.ui.poolType.selectedIndex = 1;
		}
		else
		{
			gachaType = _BackstageConfigure.GachaType;
			if (gachaType == GachaType.PreviousSkin || gachaType == GachaType.PreviousSkin2 || gachaType == GachaType.PreviousSkin3)
			{
				base.ui.com_Skin.com_UpDesc.txt_UpNick.text = _PoolConfigData.SkinCharacter.GetLocal(UIStringType.Gacha);
				base.ui.com_Skin.com_UpDesc.txt_UpName.text = _PoolConfigData.SkinName.GetLocal(UIStringType.Gacha);
				base.ui.com_Skin.com_UpDesc.txt_UpDesc.text = 21.GetLocal(UIStringType.Gacha);
				RefreshHeroAnimationByStandingPainting(_PoolConfigData.UpItem[0]);
				base.ui.com_Skin.poolType.selectedIndex = 1;
				if (!flag)
				{
					base.ui.com_Skin.Cut_in.Play();
				}
			}
			else if (_BackstageConfigure.GachaType == GachaType.Rookie)
			{
				base.ui.poolType.selectedIndex = 2;
			}
			else if (_BackstageConfigure.GachaType == GachaType.SeasonSkin)
			{
				base.ui.com_Skin_Replica.skin_DescUp.txt_UpNick.text = _PoolConfigData.SkinCharacter.GetLocal(UIStringType.Gacha);
				base.ui.com_Skin_Replica.skin_DescUp.txt_UpName.text = _PoolConfigData.SkinName.GetLocal(UIStringType.Gacha);
				base.ui.com_Skin_Replica.skin_DescUp.txt_UpDesc.text = 705.GetLocal(UIStringType.Gacha);
				base.ui.com_Skin_Replica.skin_DescUp.txt_Up.text = 706.GetLocal(UIStringType.Gacha);
				if (_PoolConfigData.SeasonSkinRerunNotUpItem != null && _PoolConfigData.SeasonSkinRerunNotUpItem.Count > 0)
				{
					base.ui.com_Skin_Replica.skin_Desc1.visible = true;
					base.ui.com_Skin_Replica.skin_Desc1.txt_UpNick.text = _PoolConfigData.SeasonSkinRerunCharacter[0].GetLocal(UIStringType.Gacha);
					base.ui.com_Skin_Replica.skin_Desc1.txt_UpName.text = _PoolConfigData.SeasonSkinRerunName[0].GetLocal(UIStringType.Gacha);
					base.ui.com_Skin_Replica.skin_Desc1.txt_UpDesc.text = 705.GetLocal(UIStringType.Gacha);
				}
				else
				{
					base.ui.com_Skin_Replica.skin_Desc1.visible = false;
				}
				if (_PoolConfigData.SeasonSkinRerunNotUpItem != null && _PoolConfigData.SeasonSkinRerunNotUpItem.Count > 1)
				{
					base.ui.com_Skin_Replica.skin_Desc2.visible = true;
					base.ui.com_Skin_Replica.skin_Desc2.txt_UpNick.text = _PoolConfigData.SeasonSkinRerunCharacter[1].GetLocal(UIStringType.Gacha);
					base.ui.com_Skin_Replica.skin_Desc2.txt_UpName.text = _PoolConfigData.SeasonSkinRerunName[1].GetLocal(UIStringType.Gacha);
					base.ui.com_Skin_Replica.skin_Desc2.txt_UpDesc.text = 705.GetLocal(UIStringType.Gacha);
				}
				else
				{
					base.ui.com_Skin_Replica.skin_Desc2.visible = false;
				}
			}
			else
			{
				base.ui.poolType.selectedIndex = 0;
				base.ui.com_Skin.poolType.selectedIndex = 0;
			}
		}
		RefreshTabTime();
		RefreshGachaProgress(!flag);
		_skinItemId = GetSkinItemId();
		if (_skinItemId == 0)
		{
			base.ui.com_UpDesc.btn_preview.visible = false;
			base.ui.com_Skin.com_UpDesc.btn_preview.visible = false;
		}
		else
		{
			base.ui.com_Skin.com_UpDesc.btn_preview.visible = true;
			base.ui.com_UpDesc.btn_preview.visible = true;
		}
		base.ui.com_Skin.visible = _BackstageConfigure.GachaTableType == GachaTableType.Skin;
		base.ui.com_Skin_Replica.visible = _BackstageConfigure.GachaTableType == GachaTableType.Replica;
		_CostItemGoods = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndItemID(10, _BackstageConfigure.CostItem);
		if (_BackstageConfigure.GachaTableType == GachaTableType.Skin && _PoolConfigData.UpItem.Count > 0 && !SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(_PoolConfigData.UpItem[0]))
		{
			TryShowGachaSkinTip(_PoolConfigData.UpItem[0], delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(_BackstageConfigure.PoolID).OnFinishedOnly.AddOnce(delegate
				{
					RefreshGachaProgress(refresh: false);
				});
			}, null, null, null);
		}
		base.ui.list_PoolButton.onClickItem.Release();
	}

	public string GetPoolBG(GachaPoolConfigure poolData)
	{
		return GameSettings.GetDataForLanguage(GameSettings.angelMode ? poolData.BackgroundENSFW : poolData.BackgroundEN, GameSettings.angelMode ? poolData.BackgroundJPSFW : poolData.BackgroundJP, GameSettings.angelMode ? poolData.BackgroundCNSFW : poolData.BackgroundCN, GameSettings.angelMode ? poolData.BackgroundTCSFW : poolData.BackgroundTC);
	}

	private async void RefreshHeroAnimation(int heroId = 0)
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroId, 0, 0);
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(configStandingPainting, "Walk", base.ui.loader_UpAnimation, 10f);
		base.ui.loader_UpAnimation.visible = true;
	}

	private void RefreshTabTime()
	{
		GachaBackstageConfigure gachaBackstageConfigure = _BackstageConfigList[base.ui.list_PoolButton.selectedIndex];
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		Timestamp endDateTime = gachaBackstageConfigure.EndDateTime;
		if ((object)endDateTime != null)
		{
			DateTime dateTime = endDateTime.ToDateTime();
			if (serverTime > dateTime)
			{
				base.ui.showTime.selectedIndex = 0;
				return;
			}
			base.ui.showTime.selectedIndex = 1;
			base.ui.txt_timeTip.text = TimeHelper.RefreshTimeText(1010, 1011, serverTime, dateTime);
		}
		else
		{
			base.ui.showTime.selectedIndex = 0;
		}
	}

	private void ShowPoolInfo(EventContext context)
	{
		base.ui.btn_Info.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaPoolInfo(_PoolConfigData, _BackstageConfigure.GachaTableType);
		base.ui.btn_Info.onClick.Release();
	}

	private void OnRequestOneGacha(EventContext context)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch() && (_BackstageConfigure == null || _BackstageConfigure.GachaTableType != GachaTableType.Skin || !TryShowGachaSkinTip(_PoolConfigData.UpItem[0], delegate
		{
			SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(_BackstageConfigure.PoolID).OnFinishedOnly.AddOnce(delegate
			{
				RefreshGachaProgress(refresh: false);
			});
		}, delegate
		{
			if (LocalCache.GetGachaTipsStatus())
			{
				RequestOneGacha();
			}
		}, RequestOneGacha, null)))
		{
			RequestOneGacha();
		}
	}

	private void RequestOneGacha()
	{
		GachaBackstageConfigure configData = _BackstageConfigList[base.ui.list_PoolButton.selectedIndex];
		List<int> list = new List<int>();
		if (configData != null)
		{
			if (configData.TimeLimit != 0)
			{
				list.Add(configData.TimeLimit);
			}
			if (configData.CostItem != 0)
			{
				list.Add(configData.CostItem);
			}
		}
		SimpleSingletonProvider<UIManager>.inst.ShowTipsBeforeGacha(list, 1, delegate
		{
			int num = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(configData.CostItem) + SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(configData.TimeLimit);
			if (num < configData.CostOnce)
			{
				OpenRechargeTipWin(configData.CostOnce - num);
			}
			else
			{
				base.ui.btn_GachaOne.onClick.Retain();
				base.ui.btn_Return.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCSC((int)configData.GachaType, 1).OnFinished.AddOnce(delegate(RPCAsyncResult result)
				{
					if (result.errId != 0)
					{
						base.ui.btn_GachaOne.onClick.Release();
						base.ui.btn_Return.onClick.Release();
					}
					else
					{
						GachaShow(configData, single: true);
					}
				});
			}
		});
	}

	private void OnRequestMultiGacha(EventContext context)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch() && (_BackstageConfigure == null || _BackstageConfigure.GachaTableType != GachaTableType.Skin || !TryShowGachaSkinTip(_PoolConfigData.UpItem[0], delegate
		{
			SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(_BackstageConfigure.PoolID).OnFinishedOnly.AddOnce(delegate
			{
				RefreshGachaProgress(refresh: false);
			});
		}, delegate
		{
			if (LocalCache.GetGachaTipsStatus())
			{
				RequestMultiGacha();
			}
		}, RequestMultiGacha, null)))
		{
			RequestMultiGacha();
		}
	}

	private void RequestMultiGacha()
	{
		GachaBackstageConfigure configData = _BackstageConfigList[base.ui.list_PoolButton.selectedIndex];
		int NTimes = configData.NTimes;
		float CostOnce = configData.CostOnce;
		if (configData.GachaType == GachaType.Rookie)
		{
			NTimes = 10;
			CostOnce = 0.8000001f;
		}
		List<int> list = new List<int>();
		if (configData != null)
		{
			if (configData.TimeLimit != 0)
			{
				list.Add(configData.TimeLimit);
			}
			if (configData.CostItem != 0)
			{
				list.Add(configData.CostItem);
			}
		}
		SimpleSingletonProvider<UIManager>.inst.ShowTipsBeforeGacha(list, (int)((float)NTimes * CostOnce), delegate
		{
			int num = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(configData.CostItem) + SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(configData.TimeLimit);
			if (num < (int)(CostOnce * (float)NTimes))
			{
				OpenRechargeTipWin((int)(CostOnce * (float)NTimes - (float)num));
			}
			else
			{
				base.ui.btn_GachaMulti_Rookie.onClick.Retain();
				base.ui.btn_GachaMulti.onClick.Retain();
				base.ui.btn_Return.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCSC((int)configData.GachaType, configData.NTimes).OnFinished.AddOnce(delegate(RPCAsyncResult result)
				{
					if (result.errId != 0)
					{
						base.ui.btn_GachaMulti_Rookie.onClick.Release();
						base.ui.btn_GachaMulti.onClick.Release();
						base.ui.btn_Return.onClick.Release();
					}
					else
					{
						GachaShow(configData, single: false);
					}
				});
			}
		});
	}

	private void OpenRechargeTipWin(int needGachaTicket)
	{
		int needCount = needGachaTicket * _CostItemGoods.goodsConfig.DiscountPrice;
		SimpleSingletonProvider<UIManager>.inst.rechargeTip.TryExchangeItem(_CostItemGoods.goodsConfig.CurrencyID, needCount, ShopTabType.Hide, _CostItemGoods.goodsConfig.GoodsID, needGachaTicket, _CostItemGoods.itemConfig).Forget();
	}

	private void RefreshProgressByServer(int poolId)
	{
		if (_PoolConfigData != null && _PoolConfigData.PoolID == poolId)
		{
			RefreshGachaProgress(refresh: false);
		}
	}

	private void RefreshGachaProgress(bool refresh = true)
	{
		if (_BackstageConfigure == null || _PoolConfigData == null || !StaticConfigure.Gacha.ProgressDict.TryGetValue(_PoolConfigData.PoolID, out var progressConfig))
		{
			base.ui.com_Skin.progress_reward.visible = false;
			base.ui.com_Skin.btn_GachaOne.visible = false;
			base.ui.com_Skin.btn_GachaMulti.visible = false;
			base.ui.btn_GachaOne.visible = true;
			base.ui.btn_GachaMulti.visible = true;
			return;
		}
		GachaPoolProgress progressData = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(progressConfig.PoolID);
		if (_BackstageConfigure.GachaTableType != GachaTableType.Skin && _BackstageConfigure.GachaTableType != GachaTableType.Replica)
		{
			if (_BackstageConfigure.GachaType == GachaType.Rookie)
			{
				RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems = progressConfig.GachaProgressConfigureItems;
				if (gachaProgressConfigureItems != null && gachaProgressConfigureItems.Count > 0)
				{
					int num = (gachaProgressConfigureItems[0].Count - progressData.Progress) / 10;
					base.ui.txt_rookieGachaCount.SetVar("count", num.ToString()).FlushVars();
					base.ui.txt_rookieGachaProcess.SetVar("curCount", progressData.Progress.ToString()).FlushVars();
					base.ui.txt_rookieGachaProcess.SetVar("maxCount", gachaProgressConfigureItems[0].Count.ToString()).FlushVars();
					bool flag = progressData.IsFinishRewardByProgress(gachaProgressConfigureItems.Count);
					base.ui.btn_rookieReward.isReview.selectedIndex = (flag ? 2 : ((progressData.Progress >= gachaProgressConfigureItems[0].Count) ? 1 : 0));
					base.ui.btn_GachaMulti_Rookie.enabled = progressData.Progress < gachaProgressConfigureItems[0].Count;
					base.ui.btn_GachaMulti_Rookie.grayed = progressData.Progress >= gachaProgressConfigureItems[0].Count;
				}
			}
			return;
		}
		RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems2 = progressConfig.GachaProgressConfigureItems;
		base.ui.com_Skin.btn_GachaOne.visible = true;
		base.ui.com_Skin.btn_GachaMulti.visible = true;
		base.ui.btn_GachaOne.visible = false;
		base.ui.btn_GachaMulti.visible = false;
		base.ui.list_PoolButton.numItems = _BackstageConfigList.Count;
		Dictionary<int, List<int>> backstageTypesForTable = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetBackstageTypesForTable();
		base.ui.list_GachaType.numItems = backstageTypesForTable.Count;
		if (_BackstageConfigure.GachaTableType == GachaTableType.Replica)
		{
			base.ui.com_Skin_Replica.btn_SupportPackage.loader_Icon.url = _BackstageConfigure.GiftID.GetItemInfoConfigure().Icon;
			_showItem = null;
			foreach (GachaProgressConfigureItem item in gachaProgressConfigureItems2)
			{
				if (!progressData.IsFinishRewardByProgress(item.Count))
				{
					if (item.Count <= progressData.Progress)
					{
						_showItem = item;
					}
					if (_showItem == null)
					{
						_showItem = item;
						break;
					}
				}
			}
			if (_showItem != null)
			{
				base.ui.com_Skin_Replica.progress_tab.btn_item.icon_loader.url = _showItem.Reward.FirstOrDefault().Key.GetItemInfoConfigure().Icon;
				base.ui.com_Skin_Replica.progress_tab.btn_item.num.text = $"x{_showItem.Reward.FirstOrDefault().Value}";
				if (_showItem.Count <= progressData.Progress)
				{
					base.ui.com_Skin_Replica.progress_tab.getType.selectedIndex = 1;
				}
				else
				{
					int num2 = _showItem.Count - progressData.Progress;
					base.ui.com_Skin_Replica.progress_tab.txt_title.SetVar("count", num2.ToString()).FlushVars();
					base.ui.com_Skin_Replica.progress_tab.getType.selectedIndex = 0;
				}
				base.ui.com_Skin_Replica.progress_tab.visible = true;
			}
			else
			{
				base.ui.com_Skin_Replica.progress_tab.visible = false;
			}
			return;
		}
		for (int i = 0; i < _progressItems.Count; i++)
		{
			if (refresh)
			{
				_progressItems[i].visible = false;
				int j = i;
				_progressItems[i].Cut_in.Play(1, 0.05f * (float)j, delegate
				{
					_progressItems[j].visible = true;
				}, null);
			}
			GachaProgressConfigureItem rewardConfig = gachaProgressConfigureItems2[i];
			if (rewardConfig.Reward.Keys.Count == 0)
			{
				_progressItems[i].visible = false;
				continue;
			}
			bool isFinish = progressData.IsFinishRewardByProgress(rewardConfig.Count);
			_progressItems[i].title = rewardConfig.Count.ToString();
			_progressItems[i].txt_count.text = "x" + rewardConfig.Reward.FirstOrDefault().Value;
			ItemInfoConfigure itemInfoConfigure = rewardConfig.Reward.FirstOrDefault().Key.GetItemInfoConfigure();
			if (itemInfoConfigure != null)
			{
				_progressItems[i].loader_icon.url = itemInfoConfigure.ShowIcon;
			}
			if (i == _progressItems.Count - 1 && rewardConfig.Reward.Count > 1)
			{
				ItemInfoConfigure itemInfoConfigure2 = rewardConfig.Reward.Keys.ToList()[1].GetItemInfoConfigure();
				if (itemInfoConfigure2 != null)
				{
					_progressItems[i].loader_icon2.url = itemInfoConfigure2.ShowIcon;
				}
			}
			if (isFinish)
			{
				_progressItems[i].status.selectedIndex = 2;
				SimpleSingletonProvider<GameObjectManager>.inst.Stop(_progressItems[i].graph_ReplaceEffect_Bottom);
				SimpleSingletonProvider<GameObjectManager>.inst.Stop(_progressItems[i].graph_ReplaceEffect_Top);
			}
			else
			{
				_progressItems[i].status.selectedIndex = ((progressData.Progress >= rewardConfig.Count) ? 1 : 0);
				if (progressData.Progress >= rewardConfig.Count)
				{
					if (i == _progressItems.Count - 1)
					{
						EffectInfoConfigure effectDataConfigure = 1008.GetEffectDataConfigure();
						SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, _progressItems[i].graph_ReplaceEffect_Bottom).Forget();
						EffectInfoConfigure effectDataConfigure2 = 1009.GetEffectDataConfigure();
						SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure2.EffectName, _progressItems[i].graph_ReplaceEffect_Top).Forget();
					}
					else
					{
						EffectInfoConfigure effectDataConfigure3 = 1006.GetEffectDataConfigure();
						SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure3.EffectName, _progressItems[i].graph_ReplaceEffect_Bottom).Forget();
						EffectInfoConfigure effectDataConfigure4 = 1007.GetEffectDataConfigure();
						SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure4.EffectName, _progressItems[i].graph_ReplaceEffect_Top).Forget();
					}
				}
				else
				{
					SimpleSingletonProvider<GameObjectManager>.inst.Stop(_progressItems[i].graph_ReplaceEffect_Bottom);
					SimpleSingletonProvider<GameObjectManager>.inst.Stop(_progressItems[i].graph_ReplaceEffect_Top);
				}
			}
			int index = i;
			_progressItems[i].onClick.Set((EventCallback0)delegate
			{
				_progressItems[index].onClick.Retain();
				if (!isFinish && progressData.Progress >= rewardConfig.Count)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(progressConfig.PoolID).OnFinishedOnly.AddOnce(delegate
					{
						RefreshGachaProgress(refresh: false);
					});
				}
				else
				{
					SimpleSingletonProvider<UIManager>.inst.boxProp.ShowGift(rewardConfig.Reward).Forget();
				}
				_progressItems[index].onClick.Release();
			});
		}
		float num3 = CalculateProgress(progressData.Progress) * 100f;
		base.ui.com_Skin.progress_reward.com_arrow.txt_progressValue.text = Mathf.Clamp(progressData.Progress, 0, 100).ToString();
		base.ui.com_Skin.progress_reward.value = num3;
		base.ui.com_Skin.progress_reward.progress.value = num3;
		base.ui.com_Skin.progress_reward.visible = true;
	}

	public override void InitTouchable()
	{
		base.InitTouchable();
		GachaBackstageConfigure backstageConfigure = _BackstageConfigure;
		if (backstageConfigure != null && backstageConfigure.GachaType == GachaType.Rookie && _PoolConfigData != null && StaticConfigure.Gacha.ProgressDict.TryGetValue(_PoolConfigData.PoolID, out var value))
		{
			GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(value.PoolID);
			RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems = value.GachaProgressConfigureItems;
			if (gachaProgressConfigureItems != null && gachaProgressConfigureItems.Count > 0)
			{
				base.ui.btn_GachaMulti_Rookie.enabled = poolProgress.Progress < gachaProgressConfigureItems[0].Count;
				base.ui.btn_GachaMulti_Rookie.grayed = poolProgress.Progress >= gachaProgressConfigureItems[0].Count;
			}
		}
	}

	private async void GachaShow(GachaBackstageConfigure configData, bool single)
	{
		base.ui.btn_Return.onClick.Retain();
		_FocusStatus = false;
		_Single = single;
		runningPoolData = configData;
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(GameSettings.angelMode ? 101.GetVideoKey() : 100.GetVideoKey());
		ReadyGachaResult(single);
	}

	public async void ReadyGachaResult(bool single)
	{
		_FocusStatus = false;
		ReadyOneGachaResult();
		base.ui.gachaProcess.selectedIndex = 1;
		await SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaVideo(OnPrepareCompleted, null);
	}

	private void OnPrepareCompleted()
	{
		base.ui.poolUIStatus.selectedIndex = 1;
		base.ui.btn_GachaMulti_Rookie.onClick.Release();
		base.ui.btn_GachaMulti.onClick.Release();
		base.ui.btn_GachaOne.onClick.Release();
		base.ui.com_Result.btn_GachaAgain.onClick.Release();
		base.ui.com_Result.btn_Sure_Again.onClick.Release();
	}

	private void ShowItemInfo(bool isCancelShow)
	{
		SimpleSingletonProvider<UIManager>.inst.gachaInfo.TryShowGachaResult(isCancelShow, ShowGachaResult).Forget();
	}

	private void OnRequestGachaAgain(EventContext context)
	{
		if (_BackstageConfigure == null || _BackstageConfigure.GachaTableType != GachaTableType.Skin || !TryShowGachaSkinTip(_PoolConfigData.UpItem[0], delegate
		{
			CloseGachaProcess();
			SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(_BackstageConfigure.PoolID).OnFinishedOnly.AddOnce(delegate
			{
				RefreshGachaProgress(refresh: false);
			});
		}, null, GachaAgain, CloseGachaProcess))
		{
			GachaAgain();
		}
	}

	private void GachaAgain()
	{
		base.ui.com_Result.btn_GachaAgain.onClick.Retain();
		base.ui.com_Result.btn_Sure_Again.onClick.Retain();
		int count = (_Single ? 1 : runningPoolData.NTimes);
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCSC((int)runningPoolData.GachaType, count).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId != 0)
			{
				base.ui.com_Result.btn_GachaAgain.onClick.Release();
				base.ui.com_Result.btn_Sure_Again.onClick.Release();
			}
			else
			{
				ReadyGachaResult(_Single);
			}
		});
	}

	private void CloseGachaProcess()
	{
		base.ui.com_Result.btn_Sure_Again.onClick.Retain();
		base.ui.com_Result.btn_Sure_NoAgain.onClick.Retain();
		GachaManager.inst.CancelThrowDice();
		GachaManager.inst.CloseGachaProcess();
		if (SimpleSingletonProvider<UIManager>.inst.backgroundPanel is BackgroundPanel backgroundPanel)
		{
			backgroundPanel.ChangeShowStatus(status: true);
		}
		if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
		{
			bottomMenuPanel.ChangeShowStatus(status: true);
		}
		base.ui.poolUIStatus.selectedIndex = 0;
		base.ui.gachaProcess.selectedIndex = 0;
		base.ui.com_Result.btn_Sure_Again.onClick.Release();
		base.ui.com_Result.btn_Sure_NoAgain.onClick.Release();
		base.ui.btn_Return.onClick.Release();
	}

	public void ReadyOneGachaResult()
	{
		resultItems = SimpleSingletonProvider<GameLogicManager>.inst.gacha.itemList;
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(runningPoolData.CostItem);
		int itemCount2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(runningPoolData.TimeLimit);
		int num = itemCount + itemCount2;
		if (_BackstageConfigure == null || _PoolConfigData == null)
		{
			base.ui.com_Result.againGacha.selectedIndex = 1;
			return;
		}
		int num2 = (_Single ? runningPoolData.CostOnce : ((_BackstageConfigure.GachaType == GachaType.Rookie) ? 8 : (runningPoolData.CostOnce * runningPoolData.NTimes)));
		bool flag = num >= num2;
		if (flag && _BackstageConfigure.GachaType == GachaType.Rookie && StaticConfigure.Gacha.ProgressDict.TryGetValue(_PoolConfigData.PoolID, out var value))
		{
			GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(value.PoolID);
			RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems = value.GachaProgressConfigureItems;
			if (gachaProgressConfigureItems != null && gachaProgressConfigureItems.Count > 0)
			{
				flag = poolProgress.Progress < gachaProgressConfigureItems[0].Count;
			}
		}
		base.ui.com_Result.againGacha.selectedIndex = ((!flag) ? 1 : 0);
	}

	private void ShowGachaResult()
	{
		if (resultItems.Count == 1)
		{
			RendererGachaResult(resultItems[0], base.ui.com_Result.com_Result_Item);
		}
		else
		{
			for (int i = 0; i < 10; i++)
			{
				RendererGachaResult(resultItems[9 - i], GetResultItem(i));
			}
		}
		base.ui.gachaProcess.selectedIndex = 3;
		GachaManager.inst.Release();
		if (resultItems.Count == 1)
		{
			base.ui.com_Result.showResult_Single.Play();
		}
		else
		{
			base.ui.com_Result.showResult_Multi.Play();
		}
		_FocusStatus = true;
	}

	private void RendererGachaResult(GachaItem _info, UIGacha_Button_ResultItem com_ResultItem)
	{
		ItemInfoConfigure infoItemInfo = _info.itemInfo;
		com_ResultItem.loader_Icon.url = infoItemInfo.ShowIcon;
		((UICom_QualityType)com_ResultItem.com_QualityType).qualityType.selectedIndex = (int)infoItemInfo.QualityType;
		com_ResultItem.newAcquire_.selectedIndex = (_info.newAcquire ? 1 : 0);
		com_ResultItem.replaceStatus.selectedIndex = 0;
		CloseEffect(com_ResultItem.graph_DisplayEffect);
		if (_info.Count > 1)
		{
			com_ResultItem.showcount.selectedIndex = 1;
			com_ResultItem.txt_count.text = _info.Count.ToString();
		}
		else
		{
			com_ResultItem.showcount.selectedIndex = 0;
		}
		com_ResultItem.showResult.SetHook("displayEffect", delegate
		{
			QualityType qualityType = infoItemInfo.QualityType;
			if (qualityType == QualityType.Orange || qualityType == QualityType.Purple)
			{
				ShowDisplayEffect(com_ResultItem.graph_DisplayEffect, infoItemInfo.QualityType);
			}
		});
		CloseEffect(com_ResultItem.graph_qualityEffect);
		com_ResultItem.showResult.SetHook("qualityEffect", delegate
		{
			QualityType qualityType = infoItemInfo.QualityType;
			if (qualityType == QualityType.Orange || qualityType == QualityType.Purple)
			{
				ShowQualityEffect(com_ResultItem.graph_qualityEffect, infoItemInfo.QualityType);
			}
		});
		com_ResultItem.showResult.SetHook("Replace", delegate
		{
			if (_info.replaceItemIds != null && _info.replaceItemIds.Count != 0)
			{
				com_ResultItem.showReplace.Play(delegate
				{
					com_ResultItem.LoopReplace.Play(-1, 1f, null);
				});
			}
		});
		CloseEffect(com_ResultItem.graph_ReplaceEffect);
		if (_info.replaceItemIds == null || _info.replaceItemIds.Count == 0)
		{
			return;
		}
		com_ResultItem.replaceStatus.selectedIndex = 1;
		KeyValuePair<int, int> keyValuePair = _info.replaceItemIds[0];
		ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
		com_ResultItem.loader_replaceItem.url = itemInfoConfigure.ShowIcon;
		com_ResultItem.txt_replaceNum.text = keyValuePair.Value.ToString();
		com_ResultItem.showReplace.SetHook("replaceEffect", delegate
		{
			if (_info.replaceItemIds != null && _info.replaceItemIds.Count != 0)
			{
				ShowReplaceEffect(com_ResultItem.graph_ReplaceEffect);
			}
		});
	}

	private UIGacha_Button_ResultItem GetResultItem(int index)
	{
		return index switch
		{
			0 => base.ui.com_Result.com_Result_Item_0, 
			1 => base.ui.com_Result.com_Result_Item_1, 
			2 => base.ui.com_Result.com_Result_Item_2, 
			3 => base.ui.com_Result.com_Result_Item_3, 
			4 => base.ui.com_Result.com_Result_Item_4, 
			5 => base.ui.com_Result.com_Result_Item_5, 
			6 => base.ui.com_Result.com_Result_Item_6, 
			7 => base.ui.com_Result.com_Result_Item_7, 
			8 => base.ui.com_Result.com_Result_Item_8, 
			9 => base.ui.com_Result.com_Result_Item_9, 
			_ => base.ui.com_Result.com_Result_Item, 
		};
	}

	private async void ShowDisplayEffect(GGraph _graph_Effect, QualityType type)
	{
		switch (type)
		{
		case QualityType.Orange:
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_Ssr", _graph_Effect, 70f);
			break;
		case QualityType.Purple:
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_Sr", _graph_Effect, 70f);
			break;
		}
	}

	private async void ShowQualityEffect(GGraph _graph_Effect, QualityType type)
	{
		switch (type)
		{
		case QualityType.Orange:
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_loop_Ssr", _graph_Effect, 45f);
			break;
		case QualityType.Purple:
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_loop_Sr", _graph_Effect, 45f);
			break;
		}
	}

	private async void ShowReplaceEffect(GGraph _graph_Effect)
	{
		await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI("GachaFx_Repeat_Ssr", _graph_Effect, 45f);
	}

	private void CloseEffect(GGraph _graph_Effect)
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(_graph_Effect);
	}

	private void Init_Skin()
	{
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item0);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item1);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item2);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item3);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item4);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item5);
		_progressItems.Add(base.ui.com_Skin.progress_reward.btn_item6);
	}

	private void AddEvent_Skin()
	{
		base.ui.com_Skin.btn_GachaOne.onClick.Add(OnRequestOneGacha);
		base.ui.com_Skin.btn_GachaMulti.onClick.Add(OnRequestMultiGacha);
		base.ui.com_Skin.com_UpDesc.btn_preview.onClick.Add(ShowSkin);
		base.ui.com_Skin_Replica.progress_tab.btn_seeall.onClick.Add(OnProgressTabShowAll);
		base.ui.com_Skin_Replica.progress_tab.btn_get.onClick.Add(OnProgressTabGetRewardClick);
		base.ui.com_Skin_Replica.progress_tab.btn_item.onClick.Add(OnProgressTabGetItemClick);
		base.ui.com_Skin_Replica.btn_SupportPackage.onClick.Add(OnSupportPackageClick);
		base.ui.com_Skin_Replica.btn_GachaOne.onClick.Add(OnRequestOneGacha);
		base.ui.com_Skin_Replica.btn_GachaMulti.onClick.Add(OnRequestMultiGacha);
		base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Add(ShowSkin);
		base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Add(ShowReplicaUpSkin);
		base.ui.com_Skin_Replica.skin_Desc1.btn_preview.onClick.Add(ShowReplicaSkin1);
		base.ui.com_Skin_Replica.skin_Desc2.btn_preview.onClick.Add(ShowReplicaSkin2);
	}

	private void RemoveEvent_Skin()
	{
		base.ui.com_Skin.btn_GachaOne.onClick.Remove(OnRequestOneGacha);
		base.ui.com_Skin.btn_GachaMulti.onClick.Remove(OnRequestMultiGacha);
		base.ui.com_Skin.com_UpDesc.btn_preview.onClick.Remove(ShowSkin);
		base.ui.com_Skin_Replica.progress_tab.btn_seeall.onClick.Remove(OnProgressTabShowAll);
		base.ui.com_Skin_Replica.progress_tab.btn_get.onClick.Remove(OnProgressTabGetRewardClick);
		base.ui.com_Skin_Replica.progress_tab.btn_item.onClick.Remove(OnProgressTabGetItemClick);
		base.ui.com_Skin_Replica.btn_SupportPackage.onClick.Remove(OnSupportPackageClick);
		base.ui.com_Skin_Replica.btn_GachaOne.onClick.Remove(OnRequestOneGacha);
		base.ui.com_Skin_Replica.btn_GachaMulti.onClick.Remove(OnRequestMultiGacha);
		base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Remove(ShowSkin);
		base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Remove(ShowReplicaUpSkin);
		base.ui.com_Skin_Replica.skin_Desc1.btn_preview.onClick.Remove(ShowReplicaSkin1);
		base.ui.com_Skin_Replica.skin_Desc2.btn_preview.onClick.Remove(ShowReplicaSkin2);
	}

	private async void RefreshHeroAnimationByStandingPainting(int standingPaintingId)
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(standingPaintingId);
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(configStandingPainting, "Walk", base.ui.com_Skin.loader_UpAnimation, 10f);
		base.ui.com_Skin.loader_UpAnimation.visible = true;
	}

	private int GetSkinItemId()
	{
		if (!StaticConfigure.Gacha.CombDict.TryGetValue(_PoolConfigData.CombDefault, out var value))
		{
			return 0;
		}
		foreach (GachaCombConfigureItem gachaCombConfigureItem in value.GachaCombConfigureItems)
		{
			if (!StaticConfigure.Gacha.GroupDict.TryGetValue(gachaCombConfigureItem.GroupID, out var value2))
			{
				return 0;
			}
			foreach (GachaGroupConfigureItem gachaGroupConfigureItem in value2.GachaGroupConfigureItems)
			{
				ItemInfoConfigure itemInfoConfigure = gachaGroupConfigureItem.ItemId.GetItemInfoConfigure();
				if (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.HeroStandingPainting)
				{
					return gachaGroupConfigureItem.ItemId;
				}
			}
		}
		return 0;
	}

	private async void ShowSkin()
	{
		if (_skinItemId != 0)
		{
			base.ui.com_UpDesc.btn_preview.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(_skinItemId, 0);
			base.ui.com_UpDesc.btn_preview.onClick.Release();
		}
	}

	private async void ShowReplicaUpSkin()
	{
		if (_skinItemId != 0)
		{
			base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(_skinItemId, 0);
			base.ui.com_Skin_Replica.skin_DescUp.btn_preview.onClick.Release();
		}
	}

	private async void ShowReplicaSkin1()
	{
		if (_PoolConfigData != null && _PoolConfigData.SeasonSkinRerunNotUpItem != null && _PoolConfigData.SeasonSkinRerunNotUpItem.Count != 0 && _PoolConfigData.SeasonSkinRerunNotUpItem[0] != 0)
		{
			base.ui.com_Skin_Replica.skin_Desc1.btn_preview.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(_PoolConfigData.SeasonSkinRerunNotUpItem[0], 0);
			base.ui.com_Skin_Replica.skin_Desc1.btn_preview.onClick.Release();
		}
	}

	private async void ShowReplicaSkin2()
	{
		if (_PoolConfigData != null && _PoolConfigData.SeasonSkinRerunNotUpItem != null && _PoolConfigData.SeasonSkinRerunNotUpItem.Count != 0 && _PoolConfigData.SeasonSkinRerunNotUpItem[1] != 0)
		{
			base.ui.com_Skin_Replica.skin_Desc2.btn_preview.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(_PoolConfigData.SeasonSkinRerunNotUpItem[1], 0);
			base.ui.com_Skin_Replica.skin_Desc2.btn_preview.onClick.Release();
		}
	}

	private float CalculateProgress(int x)
	{
		x = Mathf.Clamp(x, 1, 100);
		if (x <= 10)
		{
			return (float)(x - 1) / 9f * (1f / 6f);
		}
		if (x <= 20)
		{
			return (float)(x - 11) / 9f * (1f / 6f) + 1f / 6f;
		}
		if (x <= 30)
		{
			return (float)(x - 21) / 9f * (1f / 6f) + 1f / 3f;
		}
		if (x <= 50)
		{
			return (float)(x - 31) / 19f * (1f / 6f) + 0.5f;
		}
		if (x <= 70)
		{
			return (float)(x - 51) / 19f * (1f / 6f) + 2f / 3f;
		}
		return (float)(x - 71) / 29f * (1f / 6f) + 5f / 6f;
	}

	private void OnProgressTabShowAll()
	{
		base.ui.com_Skin_Replica.progress_tab.btn_seeall.onClick.Retain();
		string local = 1200001.GetLocal(UIStringType.GUI);
		string local2 = 1200002.GetLocal(UIStringType.GUI);
		string local3 = 1200003.GetLocal(UIStringType.GUI);
		string local4 = 1200004.GetLocal(UIStringType.GUI);
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		if (StaticConfigure.Gacha.ProgressDict.TryGetValue(_PoolConfigData.PoolID, out var value))
		{
			foreach (GachaProgressConfigureItem gachaProgressConfigureItem in value.GachaProgressConfigureItems)
			{
				string key = gachaProgressConfigureItem.Count.ToString();
				string value2 = gachaProgressConfigureItem.Reward.FirstOrDefault().Key.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item) + $"x{gachaProgressConfigureItem.Reward.FirstOrDefault().Value}";
				list.Add(new KeyValuePair<string, string>(key, value2));
			}
		}
		SimpleSingletonProvider<UIManager>.inst.rule.TryShowExcel01(local, local2, local3, local4, list).Forget();
		base.ui.com_Skin_Replica.progress_tab.btn_seeall.onClick.Release();
	}

	private void OnProgressTabGetRewardClick()
	{
		base.ui.com_Skin_Replica.progress_tab.btn_get.onClick.Retain();
		if (StaticConfigure.Gacha.ProgressDict.TryGetValue(_PoolConfigData.PoolID, out var value))
		{
			GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(value.PoolID);
			if (poolProgress != null && poolProgress.Progress >= _showItem.Count)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCountRewardS2C(value.PoolID).OnFinishedOnly.AddOnce(delegate
				{
					RefreshGachaProgress(refresh: false);
				});
			}
		}
		base.ui.com_Skin_Replica.progress_tab.btn_get.onClick.Release();
	}

	private void OnProgressTabGetItemClick()
	{
		base.ui.com_Skin_Replica.progress_tab.btn_item.onClick.Retain();
		if (_showItem != null)
		{
			ShowPropInfo(_showItem.Reward.FirstOrDefault().Key, _showItem.Reward.FirstOrDefault().Value);
		}
		base.ui.com_Skin_Replica.progress_tab.btn_item.onClick.Release();
	}

	private async void ShowPropInfo(int ItemId, int count)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(ItemId, count, _Usable: false);
	}

	private async void OnSupportPackageClick()
	{
		base.ui.com_Skin_Replica.btn_SupportPackage.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(_BackstageConfigure.Way);
		base.ui.com_Skin_Replica.btn_SupportPackage.onClick.Release();
	}

	private bool TryShowGachaSkinTip(int skinItemId, Action okRewardEvent, Action cancelRewardEvent, Action okGachaEvent, Action cancelGachaEvent)
	{
		if (_PoolConfigData == null)
		{
			return false;
		}
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(_PoolConfigData.UpItem[0]);
		GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(_BackstageConfigure.PoolID);
		if (!flag && poolProgress.Progress < 100)
		{
			return false;
		}
		if (flag)
		{
			if (!LocalCache.GetGachaSkinFinishTipStatus())
			{
				return false;
			}
			ShowGachaSkinRewardTip(1088, 1091, 1092, LocalStore.GachaSkinFinishTip, skinItemId, okGachaEvent, cancelGachaEvent);
		}
		else
		{
			if (!LocalCache.GetGachaSkinRewardTipStatus())
			{
				return false;
			}
			ShowGachaSkinRewardTip(1087, 1089, 1090, LocalStore.GachaSkinRewardTip, skinItemId, okRewardEvent, cancelRewardEvent);
		}
		return true;
	}

	private void ShowGachaSkinRewardTip(int msgId, int okTitle, int cancelTitle, string cacheKey, int skinItemId, Action okEvent, Action cancelEvent)
	{
		ItemInfoConfigure itemInfoConfigure = skinItemId.GetItemInfoConfigure();
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(msgId.GetLocal(UIStringType.Message), itemInfoConfigure.NameID.GetLocal(UIStringType.Item)), 1072, okTitle, cancelTitle, delegate
		{
			LocalCache.UpdateGachaSkinTipStatus(cacheKey, SimpleSingletonProvider<UIManager>.inst.messageBox.GetDoubleStatus());
			okEvent?.Invoke();
		}, delegate
		{
			LocalCache.UpdateGachaSkinTipStatus(cacheKey, SimpleSingletonProvider<UIManager>.inst.messageBox.GetDoubleStatus());
			cancelEvent?.Invoke();
		}).Forget();
	}
}
