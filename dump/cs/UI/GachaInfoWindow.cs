using System;
using System.Collections.Generic;
using Core;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class GachaInfoWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private List<GachaRecord> gahcaRecords;

	private const int recordMaxNum = 10;

	private int maxPage;

	private int recordPage;

	private uint _GachaSFXPlayingID;

	private Action onPrepareCompletedEvent;

	private Action onLoopPointReachedEvent;

	private List<GachaItem> _showGachaItems;

	private Action _showGachaResult;

	private int _showSkinIndex;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public GachaInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIGachaInfoWindow.CreateInstance();
		base.OnInit();
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
		if (base.contentPane is UIGachaInfoWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIGachaInfoWindow)
		{
			uIGachaInfoWindow.btn_CloseInfo.onClick.Add(base.Hide);
			bottom.closeButton.onClick.Add(base.Hide);
			uIGachaInfoWindow.com_Record.btn_Left.onClick.Add(RefreshRecord_Left);
			uIGachaInfoWindow.com_Record.btn_Right.onClick.Add(RefreshRecord_Right);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIGachaInfoWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIGachaInfoWindow)
		{
			uIGachaInfoWindow.type.selectedIndex = 0;
			uIGachaInfoWindow.btn_CloseInfo.onClick.Remove(base.Hide);
			bottom.closeButton.onClick.Remove(base.Hide);
			uIGachaInfoWindow.com_Record.btn_Left.onClick.Remove(RefreshRecord_Left);
			uIGachaInfoWindow.com_Record.btn_Right.onClick.Remove(RefreshRecord_Right);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape && (uIGachaInfoWindow.type.selectedIndex == 1 || uIGachaInfoWindow.type.selectedIndex == 2))
		{
			Hide();
		}
	}

	public async void ShowGachaPoolInfo(GachaPoolConfigure poolData, GachaTableType tableType = GachaTableType.None)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIGachaInfoWindow win))
		{
			return;
		}
		await blurBgCtrl.CreateBlurTex();
		win.type.selectedIndex = 1;
		string extraRewardName = "";
		foreach (KeyValuePair<int, int> item in poolData.ExtraReward)
		{
			item.Deconstruct(out var key, out var _);
			ItemInfoConfigure itemInfoConfigure = key.GetItemInfoConfigure();
			if (itemInfoConfigure != null)
			{
				extraRewardName = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
				break;
			}
		}
		win.bottom.text = poolData.NameID.GetLocal(UIStringType.Gacha);
		win.com_Info.txt_PoolDesc.text = string.Format(poolData.RuleDecriptionID.GetLocal(UIStringType.Gacha), extraRewardName);
		GachaPoolInfo poolInfo = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetGachaPoolInfo(poolData.PoolID);
		if (poolInfo == null)
		{
			return;
		}
		win.com_Info.list_groupInfo.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICachaInfo_Com_GroupInfo uICachaInfo_Com_GroupInfo)
			{
				KeyValuePair<int, List<GachaGroupItemData>> curGroupData = poolInfo.GetItemsByIndex(index);
				uICachaInfo_Com_GroupInfo.txt_groupDesc.text = string.Format(poolInfo.GetDesc(curGroupData.Key), extraRewardName);
				uICachaInfo_Com_GroupInfo.list_Item.itemRenderer = delegate(int i, GObject o)
				{
					UIGachaInfo_Button_PropItem infobtn = o as UIGachaInfo_Button_PropItem;
					if (infobtn != null)
					{
						ItemInfoConfigure itemConfig = curGroupData.Value[i].GachaGroupConfigureItem.ItemId.GetItemInfoConfigure();
						((UICom_LitItem)infobtn.com_LitItem).loader_Icon.url = itemConfig.ShowIcon;
						((UICom_LitItem)infobtn.com_LitItem).qualityType.selectedIndex = (int)itemConfig.QualityType;
						((UICom_LitItem)infobtn.com_LitItem).txt_itemNum.text = curGroupData.Value[i].GachaGroupConfigureItem.NumberMin.ToString();
						infobtn.isUp.selectedIndex = ((poolInfo.poolData.UpItem.Contains(itemConfig.Id) && tableType != GachaTableType.Skin) ? 1 : 0);
						infobtn.txt_Percentage.text = curGroupData.Value[i].ItemPercentage.ToString("P2");
						infobtn.onClick.Set((EventCallback0)delegate
						{
							infobtn.onClick.Retain();
							ShowPropInfo(itemConfig.Id, curGroupData.Value[i].GachaGroupConfigureItem.NumberMin);
							infobtn.onClick.Release();
						});
					}
				};
				uICachaInfo_Com_GroupInfo.list_Item.numItems = curGroupData.Value.Count;
				uICachaInfo_Com_GroupInfo.list_Item.ResizeToFit();
			}
		};
		win.com_Info.list_groupInfo.numItems = poolInfo.itemsDict.Count;
		win.com_Info.list_groupInfo.ResizeToFit();
		win.com_Info.scrollPane.percY = 0f;
		blurBgCtrl.OnShown(this);
	}

	private async void ShowPropInfo(int ItemId, int count)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(ItemId, count, _Usable: false);
	}

	public async void ShowGachaRecord(GachaPoolConfigure poolData)
	{
		await TryShowAsync();
		await blurBgCtrl.CreateBlurTex();
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			uIGachaInfoWindow.type.selectedIndex = 2;
			recordPage = 0;
			maxPage = Mathf.Min(3, SimpleSingletonProvider<GameLogicManager>.inst.gacha.gachaRecords.Count / 10);
			RefreshRecord();
			uIGachaInfoWindow.bottom.text = 1002.GetLocal(UIStringType.GUI);
			uIGachaInfoWindow.com_Record.txt_PoolName.text = poolData.NameID.GetLocal(UIStringType.Gacha);
			blurBgCtrl.OnShown(this);
		}
	}

	private void RefreshRecord()
	{
		if (!(base.contentPane is UIGachaInfoWindow uIGachaInfoWindow))
		{
			return;
		}
		uIGachaInfoWindow.com_Record.txt_Page.text = (recordPage + 1).ToString();
		gahcaRecords = SimpleSingletonProvider<GameLogicManager>.inst.gacha.gachaRecords;
		uIGachaInfoWindow.com_Record.list_Record.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIGachaInfo_Com_RecordItem uIGachaInfo_Com_RecordItem)
			{
				int num = recordPage * 10 + index;
				if (num < gahcaRecords.Count)
				{
					string text = gahcaRecords[num].itemInfo.NameID.GetLocal(UIStringType.Item);
					if (gahcaRecords[num].itemInfo.QualityType == QualityType.Orange)
					{
						text = "[color=#FF9900]" + text + "[/color]";
					}
					uIGachaInfo_Com_RecordItem.txt_Name.text = text;
					uIGachaInfo_Com_RecordItem.txt_type.text = ((int)gahcaRecords[num].itemInfo.ItemType).GetItemTagConfigure().NameID.GetLocal(UIStringType.Item);
					GTextField txt_Num = uIGachaInfo_Com_RecordItem.txt_Num;
					int itemCount = gahcaRecords[num].itemCount;
					txt_Num.text = itemCount.ToString();
					uIGachaInfo_Com_RecordItem.txt_Time.text = gahcaRecords[num]._FormatTime;
				}
				else
				{
					uIGachaInfo_Com_RecordItem.txt_Name.text = "";
					uIGachaInfo_Com_RecordItem.txt_type.text = "";
					uIGachaInfo_Com_RecordItem.txt_Num.text = "";
					uIGachaInfo_Com_RecordItem.txt_Time.text = "";
				}
			}
		};
		uIGachaInfoWindow.com_Record.list_Record.numItems = 10;
	}

	private void RefreshRecord_Left(EventContext context)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			uIGachaInfoWindow.com_Record.btn_Left.onClick.Retain();
			recordPage = Mathf.Max(0, recordPage - 1);
			RefreshRecord();
			uIGachaInfoWindow.com_Record.btn_Left.onClick.Release();
		}
	}

	private void RefreshRecord_Right(EventContext context)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			uIGachaInfoWindow.com_Record.btn_Right.onClick.Retain();
			recordPage = Mathf.Min(maxPage, recordPage + 1);
			RefreshRecord();
			uIGachaInfoWindow.com_Record.btn_Right.onClick.Release();
		}
	}

	private async UniTask<Player> ShowVideo(string videoKey, Action<Player, int> OnLoopPointReached, Action<Player, int> OnPrepareCompleted, Action _onPrepareCompletedEvent, Action _onLoopPointReachedEvent, bool PlayEndImmediatelyStop = true, bool showSkipButton = true)
	{
		await TryShowAsync();
		if (!(base.contentPane is UIGachaInfoWindow uIGachaInfoWindow))
		{
			return null;
		}
		uIGachaInfoWindow.type.selectedIndex = 3;
		uIGachaInfoWindow.ShowSkip.selectedIndex = ((!showSkipButton) ? 1 : 0);
		uIGachaInfoWindow.btn_Skip.visible = false;
		onPrepareCompletedEvent = _onPrepareCompletedEvent;
		onLoopPointReachedEvent = _onLoopPointReachedEvent;
		uIGachaInfoWindow.loader_Movie.FullScreen();
		return await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, uIGachaInfoWindow.loader_Movie, OnPrepareCompleted, OnLoopPointReached, null, 0, PlayEndImmediatelyStop);
	}

	public async UniTask ShowGachaVideo(Action _onPrepareCompletedEvent, Action _onLoopPointReachedEvent)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIGachaInfoWindow win = gComponent as UIGachaInfoWindow;
		if (win == null)
		{
			return;
		}
		string videoKey = (GameSettings.angelMode ? 101.GetVideoKey() : 100.GetVideoKey());
		Player gachaVideo = await ShowVideo(videoKey, OnLoopPointReached, OnPrepareCompleted, _onPrepareCompletedEvent, _onLoopPointReachedEvent);
		win.btn_Skip.onClick.Set((EventCallback0)delegate
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Invalid comparison between Unknown and I4
			win.btn_Skip.onClick.Retain();
			win.btn_Skip.visible = false;
			if (gachaVideo != null && (int)gachaVideo.status == 5)
			{
				gachaVideo.Stop();
				OnLoopPointReached(gachaVideo, 5);
			}
			GachaManager.inst.CancelThrowDice();
			win.btn_Skip.onClick.Release();
		});
	}

	private void OnLoopPointReached(Player source, int status)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			onLoopPointReachedEvent?.Invoke();
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(_GachaSFXPlayingID);
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIGachaInfoWindow.loader_Movie);
			uIGachaInfoWindow.loader_Movie.visible = false;
			GachaManager.inst.ThrowDice();
		}
	}

	private void OnPrepareCompleted(Player source, int status)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			onPrepareCompletedEvent?.Invoke();
			_GachaSFXPlayingID = SimpleSingletonProvider<AudioManager>.inst.SendEvent(25, Stage.inst.gameObject);
			GachaManager.inst.ActiveGacha();
			if (SimpleSingletonProvider<UIManager>.inst.backgroundPanel is BackgroundPanel backgroundPanel)
			{
				backgroundPanel.ChangeShowStatus(status: false);
			}
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: false);
			}
			uIGachaInfoWindow.btn_Skip.visible = true;
		}
	}

	public async UniTask ShowGachaVideo_Activity(Action _onPrepareCompletedEvent, Action _onLoopPointReachedEvent)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIGachaInfoWindow win = gComponent as UIGachaInfoWindow;
		if (win == null)
		{
			return;
		}
		Player gachaVideo = await ShowVideo(102.GetVideoKey(), OnLoopPointReached_Activity, OnPrepareCompleted_Activity, _onPrepareCompletedEvent, _onLoopPointReachedEvent);
		win.btn_Skip.onClick.Set((EventCallback0)delegate
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Invalid comparison between Unknown and I4
			win.btn_Skip.onClick.Retain();
			win.btn_Skip.visible = false;
			if (gachaVideo != null && (int)gachaVideo.status == 5)
			{
				gachaVideo.Stop();
				OnLoopPointReached_Activity(gachaVideo, 5);
			}
			win.btn_Skip.onClick.Release();
		});
	}

	private void OnLoopPointReached_Activity(Player source, int status)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIGachaInfoWindow.loader_Movie);
			uIGachaInfoWindow.loader_Movie.visible = false;
			onLoopPointReachedEvent?.Invoke();
		}
	}

	private void OnPrepareCompleted_Activity(Player source, int status)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			onPrepareCompletedEvent?.Invoke();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: false);
			}
			uIGachaInfoWindow.btn_Skip.visible = true;
		}
	}

	public async UniTask ShowCharacterVideo(string url, string videoKey, int voiceId, Action _onPrepareCompletedEvent, Action _onLoopPointReachedEvent, bool showSkipButton, bool showCharacter)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIGachaInfoWindow win = gComponent as UIGachaInfoWindow;
		if (win != null)
		{
			win.loader_Character.url = url;
			await ShowVideo(videoKey, OnLoopPointReached_TopVideo, OnPrepareCompleted_TopVideo, _onPrepareCompletedEvent, _onLoopPointReachedEvent, PlayEndImmediatelyStop: false, showSkipButton);
			win.ShowCharacter.SetHook("PlayVoice", delegate
			{
				win.loader_Character.visible = showCharacter;
				SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.FANFAREVOICE, voiceId);
			});
			win.ShowCharacter.Play(ShowCharacterComplete);
		}
	}

	private void ShowCharacterComplete()
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			onLoopPointReachedEvent?.Invoke();
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIGachaInfoWindow.loader_Movie);
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: true);
			}
			uIGachaInfoWindow.loader_Character.visible = false;
			uIGachaInfoWindow.loader_Movie.visible = false;
			Hide();
		}
	}

	private void OnLoopPointReached_TopVideo(Player source, int status)
	{
		source.Pause(true);
	}

	private void OnPrepareCompleted_TopVideo(Player source, int status)
	{
		if (base.contentPane is UIGachaInfoWindow uIGachaInfoWindow)
		{
			onPrepareCompletedEvent?.Invoke();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: false);
			}
			uIGachaInfoWindow.btn_Skip.visible = true;
		}
	}

	public async UniTask TryShowGachaResult(bool isCancelShow, Action showGachaResult)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIGachaInfoWindow win))
		{
			return;
		}
		_showGachaResult = showGachaResult;
		List<GachaItem> itemList = SimpleSingletonProvider<GameLogicManager>.inst.gacha.itemList;
		_showGachaItems = new List<GachaItem>();
		for (int i = 0; i < itemList.Count; i++)
		{
			if ((!isCancelShow || itemList[i].newAcquire) && itemList[i].itemInfo.ItemType == ItemType.Hero)
			{
				_showGachaItems.Add(itemList[i]);
			}
		}
		if (_showGachaItems.Count == 0)
		{
			_showGachaResult?.Invoke();
			Hide();
			return;
		}
		CommonUIManager.TryAddVideoGraph(UIType.Window, (int)base.config.WindowType, win.com_ShowSkin.loader_BG);
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(50.GetVideoKey(), win.com_ShowSkin.loader_BG);
		_showSkinIndex = 0;
		TryShowNewSkin();
		win.com_ShowSkin.btn_SkipSkin.onClick.Set(TryShowNewSkin);
		win.type.selectedIndex = 4;
	}

	private void TryShowNewSkin()
	{
		if (!(base.contentPane is UIGachaInfoWindow uIGachaInfoWindow))
		{
			return;
		}
		uIGachaInfoWindow.com_ShowSkin.btn_SkipSkin.onClick.Retain();
		if (_showSkinIndex - 1 >= 0 && _showGachaItems.Count > _showSkinIndex - 1)
		{
			_showGachaItems[_showSkinIndex - 1].StopSkinVoice();
			uIGachaInfoWindow.com_ShowSkin.Cut_in.Stop(setToComplete: true, processCallback: false);
		}
		if (_showGachaItems.Count > _showSkinIndex)
		{
			GachaItem gachaItem = _showGachaItems[_showSkinIndex];
			SkinStandingPaintingConfigureItem skinConfig = gachaItem.GetSkinConfig();
			(string, bool) character = skinConfig.GetCharacter();
			Vector2 customOffset = Vector2.zero;
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
			{
				customOffset = value.skinOffset;
			}
			uIGachaInfoWindow.com_ShowSkin.loader_Skin.customOffset = customOffset;
			uIGachaInfoWindow.com_ShowSkin.loader_Skin.customScale = Vector2.one;
			uIGachaInfoWindow.com_ShowSkin.loader_Skin.url = character.Item1;
			uIGachaInfoWindow.com_ShowSkin.loader_Speaker.url = skinConfig.ProfilePhoto;
			if (gachaItem.itemInfo.ItemType == ItemType.Hero)
			{
				CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(gachaItem.itemInfo.SubMeterID);
				if (ColorUtility.TryParseHtmlString(heroCharacterConfigure.NameBGColor, out var color))
				{
					uIGachaInfoWindow.com_ShowSkin.txt_Speaker.text = heroCharacterConfigure.NameID.GetLocal(UIStringType.Character);
					uIGachaInfoWindow.com_ShowSkin.graph_Speaker.color = color;
					uIGachaInfoWindow.com_ShowSkin.txt_Dialog.text = heroCharacterConfigure.LinesID.GetLocal(UIStringType.Character);
				}
			}
			uIGachaInfoWindow.com_ShowSkin.Cut_in.Play();
			gachaItem.PlaySkinVoice();
		}
		else
		{
			_showGachaResult?.Invoke();
			Hide();
		}
		_showSkinIndex++;
		uIGachaInfoWindow.com_ShowSkin.btn_SkipSkin.onClick.Release();
	}
}
