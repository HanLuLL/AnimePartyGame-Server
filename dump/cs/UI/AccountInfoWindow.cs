using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class AccountInfoWindow : BaseWindow
{
	private PlayerCareer playerCareer;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private readonly List<FriendOperator> operatorData = new List<FriendOperator>();

	private List<RecordRank> rankData;

	private const int RECORD_OPERATE_SAVE = 0;

	private const int RECORD_OPERATE_PLAY = 1;

	private const int RECORD_OPERATE_DELETE = 2;

	private const int RECORD_OPERATE_SAVED = 3;

	private int achieve_Slot;

	private int selectedAchieveId;

	private List<int> achieveIds;

	private UIAccountInfo_Button_SelectAchieve hasSelectedAcheveBtn;

	private List<HeroCardData> _HeroCards;

	private int hasHeroCount;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public AccountInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIAccountInfoWindow.CreateInstance();
		base.OnInit();
		InitComponents_HeroSelect();
		InitComponents_Achieve();
	}

	public async UniTask TryShowAsync(PlayerCareer _data)
	{
		playerCareer = _data;
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
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
		SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_RefreshSkin.visible = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId);
			uIAccountInfoWindow.com_Main.txt_PraiseNum.text = playerCareer.praiseNum.ToString();
			uIAccountInfoWindow.com_Main.btn_changeName.title = 28.GetLocal(UIStringType.Message);
			uIAccountInfoWindow.com_Main.btn_changeName.visible = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId);
			uIAccountInfoWindow.com_Main.btn_changeName.onClick.Add(OpenChangeNameWindow);
			uIAccountInfoWindow.mohu.onClick.Add(QuitInfoWindow);
			uIAccountInfoWindow.com_Main.btn_UID.onClick.Add(CopyUID);
			SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.AddListener(OnChangeName);
			SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.AddListener(OnChangeName);
			AddEvent_HeroSelect();
			AddEvent_AccountData();
			AddEvent_Achieve();
			ReadyHeroInfo();
			RefreshPlayerLabel();
			RefreshSkin();
			RefreshAchieve();
			TryOperatePlayer();
			RefreshData();
			RefreshChangeNameStatus();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_changeName.onClick.Remove(OpenChangeNameWindow);
			uIAccountInfoWindow.mohu.onClick.Remove(QuitInfoWindow);
			uIAccountInfoWindow.com_Main.btn_UID.onClick.Remove(CopyUID);
			RemoveEvent_HeroSelect();
			RemoveEvent_AccountData();
			RemoveEvent_Achieve();
			uIAccountInfoWindow.list_Operate.numItems = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.RemoveListener(OnChangeName);
			SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.RemoveListener(OnChangeName);
			blurBgCtrl.OnHide();
		}
	}

	private void OpenChangeNameWindow()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.CanChangeName())
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(30);
		}
		else if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_changeName.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.Display(intention: true).Forget();
			uIAccountInfoWindow.com_Main.btn_changeName.onClick.Release();
		}
	}

	private void OnChangeName(bool result)
	{
		if (result)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId))
			{
				playerCareer.UpdateNick(SimpleSingletonProvider<GameLogicManager>.inst.account.GetName());
				RefreshChangeNameStatus();
			}
			RefreshPlayerLabel();
		}
	}

	private void RefreshChangeNameStatus()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_changeName.grayed = !SimpleSingletonProvider<GameLogicManager>.inst.account.CanChangeName();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			if (uIAccountInfoWindow.showHero.selectedIndex == 1)
			{
				uIAccountInfoWindow.showHero.selectedIndex = 0;
			}
			else if (uIAccountInfoWindow.com_Main.showFight.selectedIndex == 1)
			{
				uIAccountInfoWindow.com_Main.showFight.selectedIndex = 0;
			}
			else if (uIAccountInfoWindow.showAchieve.selectedIndex == 1)
			{
				uIAccountInfoWindow.showAchieve.selectedIndex = 0;
			}
			else
			{
				QuitInfoWindow();
			}
		}
	}

	public void QuitInfoWindow()
	{
		if (base.contentPane is UIAccountInfoWindow)
		{
			Hide();
		}
	}

	private void RefreshPlayerLabel()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			UICom_PlayerLabel uICom_PlayerLabel = (UICom_PlayerLabel)uIAccountInfoWindow.com_Main.com_Label;
			uICom_PlayerLabel.visible = true;
			if (playerCareer.playerId == 0L || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId))
			{
				uIAccountInfoWindow.com_Main.progress_Exp.max = playerCareer._LV.GetPlayerLevelConfigure().NeedExp;
				uIAccountInfoWindow.com_Main.progress_Exp.value = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Exp;
				uIAccountInfoWindow.com_Main.progress_Exp.visible = true;
			}
			else
			{
				uIAccountInfoWindow.com_Main.progress_Exp.visible = false;
			}
			CommonUIManager.RendererLabelInfo(uICom_PlayerLabel, playerCareer.GetNick(showRemark: true), playerCareer._LV);
			CommonUIManager.RendererLabel(UIType.Window, (int)base.config.WindowType, uICom_PlayerLabel, playerCareer._LabelData.Item1, playerCareer._LabelData.Item2);
			CommonUIManager.RendererHeadShot(uICom_PlayerLabel, playerCareer._HeadIcon, isVideo: false);
			uIAccountInfoWindow.com_Main.btn_UID.data = playerCareer.playerId;
			uIAccountInfoWindow.com_Main.btn_UID.txt_UID.SetTextAdaptiveMaxWidth($"UID:{playerCareer.playerId}", 255f);
		}
	}

	private void RefreshSkin()
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return;
		}
		SkinStandingPaintingConfigureItem curSkinItemInfo = GetCurSkinItemInfo();
		if (curSkinItemInfo != null)
		{
			(string, bool) character = curSkinItemInfo.GetCharacter();
			Vector2 offset = Vector2.zero;
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
			{
				offset = value.skinOffset;
			}
			CommonUIManager.RendererSkin(UIType.Window, (int)base.config.WindowType, (UICom_HeroSkin)uIAccountInfoWindow.com_Main.com_Skin.com_Skin, character.Item1, character.Item2, offset, Vector2.one);
		}
	}

	private SkinStandingPaintingConfigureItem GetCurSkinItemInfo()
	{
		if (playerCareer.StandingPainting == 0)
		{
			SkinStandingPaintingConfigureItem standingPainting = _HeroCards[0].standingPainting;
			playerCareer.StandingPainting = standingPainting.ItemID;
			return standingPainting;
		}
		RepeatedField<SkinStandingPaintingConfigure> standingPaintings = StaticConfigure.Skin.StandingPaintings;
		for (int i = 0; i < standingPaintings.Count; i++)
		{
			RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems = standingPaintings[i].SkinStandingPaintingConfigureItems;
			for (int j = 0; j < skinStandingPaintingConfigureItems.Count; j++)
			{
				if (skinStandingPaintingConfigureItems[j].ItemID == playerCareer.StandingPainting)
				{
					return skinStandingPaintingConfigureItems[j];
				}
			}
		}
		return null;
	}

	private void RequestSetShowPlayer(System.Action _Complete)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.account.RequestSetShowPlayerC2S(playerCareer).OnFinishedOnly.AddOnce(_Complete);
	}

	private void CopyUID()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_UID.onClick.Retain();
			if (uIAccountInfoWindow.com_Main.btn_UID.data is long num)
			{
				GUIUtility.systemCopyBuffer = num.ToString();
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Spectate));
			}
			uIAccountInfoWindow.com_Main.btn_UID.onClick.Release();
		}
	}

	public void TryOperatePlayer()
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return;
		}
		operatorData.Clear();
		if (playerCareer.playerId != 0L && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId))
		{
			bool num = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(playerCareer.playerId);
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(playerCareer.playerId);
			bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsAddForMe(playerCareer.playerId);
			if (num)
			{
				operatorData.Add(new FriendOperator
				{
					msgId = 8,
					btnType = 3,
					clickAction = delegate
					{
						SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.ChangeFriendRemark(UpdFriendRmk: true, playerCareer.playerId).Forget();
					}
				});
				if (!flag && !SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
				{
					operatorData.Add(new FriendOperator
					{
						msgId = 7,
						btnType = 2,
						clickAction = delegate
						{
							QuitInfoWindow();
							SimpleSingletonProvider<UIManager>.inst.chat.TryShowChatWindow(playerCareer.playerId);
						}
					});
				}
				operatorData.Add(new FriendOperator
				{
					msgId = 5,
					btnType = 1,
					clickAction = delegate
					{
						SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1008.GetLocal(UIStringType.Friend), playerCareer.GetNick()), delegate
						{
							SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendOpC2S(playerCareer.playerId, playerCareer.GetNick(), 1, QuitInfoWindow);
						}).Forget();
					}
				});
				operatorData.Add(flag ? GetPutOutBlackOperator() : GetPutInBlackOperator());
			}
			else if (flag)
			{
				operatorData.Add(GetPutOutBlackOperator());
			}
			else if (flag2)
			{
				operatorData.Add(new FriendOperator
				{
					msgId = 2,
					btnType = 2,
					clickAction = delegate
					{
						SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyOpC2S(playerCareer.playerId, 1, allRefuse: false, QuitInfoWindow);
					}
				});
				operatorData.Add(new FriendOperator
				{
					msgId = 3,
					btnType = 1,
					clickAction = delegate
					{
						SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyOpC2S(playerCareer.playerId, 2, allRefuse: false, QuitInfoWindow);
					}
				});
				operatorData.Add(GetPutInBlackOperator());
			}
			else
			{
				operatorData.Add(new FriendOperator
				{
					msgId = 1,
					btnType = 1,
					clickAction = delegate
					{
						SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyC2S(playerCareer.playerId, QuitInfoWindow);
					}
				});
				operatorData.Add(GetPutInBlackOperator());
			}
		}
		uIAccountInfoWindow.list_Operate.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIAccount_Button_Operator uIAccount_Button_Operator)
			{
				uIAccount_Button_Operator.title = operatorData[index].msgId.GetLocal(UIStringType.Friend);
				uIAccount_Button_Operator.type.selectedIndex = operatorData[index].btnType;
				uIAccount_Button_Operator.onClick.Set((EventCallback0)delegate
				{
					operatorData[index].clickAction?.Invoke();
				});
			}
		};
		uIAccountInfoWindow.list_Operate.numItems = operatorData.Count;
		uIAccountInfoWindow.list_Operate.opaque = false;
	}

	public FriendOperator GetPutInBlackOperator()
	{
		return new FriendOperator
		{
			msgId = 4,
			btnType = 0,
			clickAction = delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendOpC2S(playerCareer.playerId, playerCareer.GetNick(), 2, QuitInfoWindow);
			}
		};
	}

	public FriendOperator GetPutOutBlackOperator()
	{
		return new FriendOperator
		{
			msgId = 6,
			btnType = 0,
			clickAction = delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendOpC2S(playerCareer.playerId, playerCareer.GetNick(), 3, QuitInfoWindow);
			}
		};
	}

	private void AddEvent_AccountData()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.dataType.onChanged.Add(ShowFightData);
			uIAccountInfoWindow.com_Main.btn_ShowDataToggle.onClick.Add(SetShowData);
			uIAccountInfoWindow.com_Main.btn_ShowFightToggle.onClick.Add(SetShowFight);
			uIAccountInfoWindow.com_Main.com_Fight.btn_Accuse.onClick.Add(AccusePlayer);
			uIAccountInfoWindow.com_Main.com_Fight.btn_Block.onClick.Add(OnBlackList);
		}
	}

	private void RemoveEvent_AccountData()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.dataType.onChanged.Remove(ShowFightData);
			uIAccountInfoWindow.com_Main.btn_ShowDataToggle.onClick.Remove(SetShowData);
			uIAccountInfoWindow.com_Main.btn_ShowFightToggle.onClick.Remove(SetShowFight);
			uIAccountInfoWindow.com_Main.com_Fight.btn_Accuse.onClick.Remove(AccusePlayer);
			uIAccountInfoWindow.com_Main.com_Fight.btn_Block.onClick.Remove(OnBlackList);
		}
	}

	private void AccusePlayer()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			playerCareer.AccusePlayer((long)uIAccountInfoWindow.com_Main.com_Fight.btn_Accuse.data);
			uIAccountInfoWindow.com_Main.com_Fight.btn_Accuse.data = 0L;
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1058).Forget();
			SetOperationVisible(active: false);
		}
	}

	private void CloseFightData()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.showFight.selectedIndex = 0;
			SetOperationVisible(active: false);
		}
	}

	private void SetShowData(EventContext context)
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_ShowDataToggle.onClick.Retain();
			uIAccountInfoWindow.com_Main.btn_ShowDataToggle.touchable = false;
			playerCareer.isShowData = !playerCareer.isShowData;
			RequestSetShowPlayer(ReleaseToggle);
		}
	}

	private void SetShowFight(EventContext context)
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_ShowFightToggle.onClick.Retain();
			uIAccountInfoWindow.com_Main.btn_ShowFightToggle.touchable = false;
			playerCareer.isShowFight = !playerCareer.isShowFight;
			RequestSetShowPlayer(ReleaseToggle);
		}
	}

	private async void ReleaseToggle()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIAccountInfoWindow win)
		{
			await UniTask.Delay(500);
			win.com_Main.btn_ShowDataToggle.touchable = true;
			win.com_Main.btn_ShowFightToggle.touchable = true;
			win.com_Main.btn_ShowDataToggle.onClick.Release();
			win.com_Main.btn_ShowFightToggle.onClick.Release();
		}
	}

	private async void ShowFightData()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIAccountInfoWindow win)
		{
			win.com_Main.dataType.onChanged.Retain();
			if (win.com_Main.dataType.selectedIndex == 0)
			{
				RefreshStatisticalData();
			}
			else if (win.com_Main.dataType.selectedIndex == 1)
			{
				RefreshFightRecord(playerCareer.Records);
			}
			else if (win.com_Main.dataType.selectedIndex == 2)
			{
				win.com_Main.list_FightData.numItems = 0;
				List<BattleShortRecord> list = await SimpleSingletonProvider<GameLogicManager>.inst.replay.ListLocalReplaySummariesAsync();
				List<BattleShortRecord> records = playerCareer.UpdateReplayRecord(list);
				RefreshFightRecord(records);
				win.com_Main.txt_ReplayTip.text = 1010.GetLocal(UIStringType.GUI);
				win.com_Main.txt_ReplayTip.visible = list.Count <= 0;
			}
			win.com_Main.dataType.onChanged.Release();
		}
	}

	private void RefreshData()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.showFight.selectedIndex = 0;
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId);
			if (!flag)
			{
				uIAccountInfoWindow.com_Main.btn_ShowDataToggle.visible = false;
				uIAccountInfoWindow.com_Main.btn_ShowFightToggle.visible = false;
			}
			else
			{
				uIAccountInfoWindow.com_Main.btn_ShowDataToggle.visible = true;
				uIAccountInfoWindow.com_Main.btn_ShowFightToggle.visible = true;
				uIAccountInfoWindow.com_Main.btn_ShowDataToggle.selected = !playerCareer.isShowData;
				uIAccountInfoWindow.com_Main.btn_ShowFightToggle.selected = !playerCareer.isShowFight;
			}
			bool flag2 = SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home;
			bool flag3 = !SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom;
			uIAccountInfoWindow.com_Main.btn_Replay.visible = flag && flag3 && flag2;
			uIAccountInfoWindow.com_Main.dataType.selectedIndex = 0;
			uIAccountInfoWindow.com_Main.dataType.onChanged.Call();
		}
	}

	private void RefreshStatisticalData()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId) || playerCareer.isShowData)
			{
				uIAccountInfoWindow.com_Main.group_Statistical.visible = true;
				uIAccountInfoWindow.com_Main.txt_FightCount.SetVar("data", playerCareer.statistics.FightCount.ToString()).FlushVars();
				uIAccountInfoWindow.com_Main.txt_WinCount.SetVar("data", playerCareer.statistics.WinFightCount.ToString()).FlushVars();
				uIAccountInfoWindow.com_Main.txt_HeroCardCount.SetVar("data", playerCareer.statistics.RoleCardCount.ToString()).FlushVars();
				int characterId = ((playerCareer.statistics.UseHero == 0) ? _HeroCards[0].HeroId : playerCareer.statistics.UseHero);
				uIAccountInfoWindow.com_Main.txt_Hero.SetVar("data", CharacterHandle.GetCharacterNickName(characterId)).FlushVars();
				uIAccountInfoWindow.com_Main.txt_AdornCount.SetVar("data", playerCareer.statistics.AdornCount.ToString()).FlushVars();
				uIAccountInfoWindow.com_Main.txt_SkinCount.SetVar("data", playerCareer.statistics.SkinCount.ToString()).FlushVars();
				uIAccountInfoWindow.com_Main.txt_StatisticalTip.visible = false;
			}
			else
			{
				uIAccountInfoWindow.com_Main.group_Statistical.visible = false;
				uIAccountInfoWindow.com_Main.txt_StatisticalTip.visible = true;
				uIAccountInfoWindow.com_Main.txt_StatisticalTip.text = 1035.GetLocal(UIStringType.Message);
			}
		}
	}

	private void RefreshFightRecord(List<BattleShortRecord> records)
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId) || playerCareer.isShowFight)
		{
			uIAccountInfoWindow.com_Main.list_FightData.itemRenderer = delegate(int index, GObject item)
			{
				if (records.Count > index)
				{
					UIAccount_Button_FightData btn_FightData = item as UIAccount_Button_FightData;
					if (btn_FightData != null)
					{
						btn_FightData.Refresh(records[index]);
						btn_FightData.visible = false;
						btn_FightData.Cut_in.Play(1, 0.01f * (float)index, delegate
						{
							btn_FightData.visible = true;
						}, null);
					}
				}
			};
			uIAccountInfoWindow.com_Main.list_FightData.onClickItem.Set(delegate(EventContext context)
			{
				if (context.data is UIAccount_Button_FightData uIAccount_Button_FightData)
				{
					if (uIAccount_Button_FightData.Record.Type == BattleShortRecordType.Battle)
					{
						ShowFightRank(uIAccount_Button_FightData, uIAccount_Button_FightData.Record);
					}
					else if (uIAccount_Button_FightData.Record.Type == BattleShortRecordType.Replay)
					{
						ShowReplayFightRank(uIAccount_Button_FightData, uIAccount_Button_FightData.Record);
					}
				}
			});
			uIAccountInfoWindow.com_Main.list_FightData.numItems = records.Count;
			uIAccountInfoWindow.com_Main.list_FightData.visible = true;
			uIAccountInfoWindow.com_Main.txt_FightDataTip.visible = false;
		}
		else
		{
			uIAccountInfoWindow.com_Main.list_FightData.visible = false;
			uIAccountInfoWindow.com_Main.txt_FightDataTip.visible = true;
			uIAccountInfoWindow.com_Main.txt_FightDataTip.text = 1035.GetLocal(UIStringType.Message);
		}
	}

	private void ShowReplayFightRank(UIAccount_Button_FightData btn_FightData, BattleShortRecord record)
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			btn_FightData.onClick.Retain();
			uIAccountInfoWindow.com_Main.list_FightData.touchable = false;
			FightRecordDetail recordDetail = record.GetRecordDetail();
			RefreshFightRank(record, recordDetail);
			uIAccountInfoWindow.com_Main.showFight.selectedIndex = 1;
			uIAccountInfoWindow.com_Main.list_FightData.touchable = true;
			btn_FightData.onClick.Release();
		}
	}

	private void ShowFightRank(UIAccount_Button_FightData btn_FightData, BattleShortRecord record)
	{
		GComponent gComponent = base.contentPane;
		UIAccountInfoWindow win = gComponent as UIAccountInfoWindow;
		if (win == null)
		{
			return;
		}
		btn_FightData.onClick.Retain();
		win.com_Main.list_FightData.touchable = false;
		SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetPlayerFightRecordC2S(playerCareer.playerId, record.Index).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			win.com_Main.showFight.selectedIndex = 1;
			btn_FightData.onClick.Release();
			win.com_Main.list_FightData.touchable = true;
			if (result.errId == 0)
			{
				RefreshFightRank(record, playerCareer.recordDetail);
			}
		});
	}

	private void RefreshFightRank(BattleShortRecord shortRecord, FightRecordDetail recordDetail)
	{
		if (recordDetail != null && base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			SetOperationVisible(active: false);
			rankData = recordDetail.rankData;
			uIAccountInfoWindow.com_Main.com_Fight.txt_Time.text = shortRecord.FormatTime;
			uIAccountInfoWindow.com_Main.com_Fight.txt_ReplayId.text = shortRecord.ReplayId;
			RendererRand(0, uIAccountInfoWindow.com_Main.com_Fight.com_FirstLabel, (MapModeType)shortRecord.MapType, shortRecord.Rank);
			RendererRand(1, uIAccountInfoWindow.com_Main.com_Fight.com_SecondLabel, (MapModeType)shortRecord.MapType, shortRecord.Rank);
			RendererRand(2, uIAccountInfoWindow.com_Main.com_Fight.com_ThirdLabel, (MapModeType)shortRecord.MapType, shortRecord.Rank);
			RendererRand(3, uIAccountInfoWindow.com_Main.com_Fight.com_ForthLabel, (MapModeType)shortRecord.MapType, shortRecord.Rank);
			RefreshRecordOperation(shortRecord);
		}
	}

	private void RendererRand(int index, UIAccountInfo_Button_RankInfo item, MapModeType mapModeType, int rank)
	{
		GComponent gComponent = base.contentPane;
		UIAccountInfoWindow win = gComponent as UIAccountInfoWindow;
		if (win == null)
		{
			return;
		}
		if (rankData.Count > index)
		{
			item.visible = true;
			item.GiveUp.selectedIndex = (rankData[index].data.IsGiveUp ? 1 : 0);
			if (!BattleConfig.IsPVE(mapModeType))
			{
				RefreshPlayerLabel(item.com_Rank, rankData[index]);
				GImage image_First = item.com_Rank.image_First;
				GImage image_Second = item.com_Rank.image_Second;
				GImage image_Third = item.com_Rank.image_Third;
				Color color = (item.com_Rank.image_Forth.color = GameConfig.slotColor[rankData[index].data.Slot]);
				Color color3 = (image_Third.color = color);
				Color color5 = (image_Second.color = color3);
				image_First.color = color5;
				item.GameMode.selectedIndex = 0;
				item.com_Rank.rand.selectedIndex = Mathf.Min(rankData[index].data.Rank - 1, 3);
			}
			else
			{
				item.PVEResult.selectedIndex = ((rank != 1) ? 1 : 0);
				item.GameMode.selectedIndex = 1;
			}
			if (rankData[index].data.PlayerId > 100000)
			{
				item.txt_PlayerNick.text = rankData[index].nick;
			}
			else
			{
				item.txt_PlayerNick.text = StaticConfigure.Player.CoverNames[index].CoverNameID.GetLocal(UIStringType.Player);
			}
			if (rankData[index].data.HeroId != 0)
			{
				item.txt_HeroNick.text = CharacterHandle.GetCharacterNickName(rankData[index].data.HeroId);
			}
			item.onClick.Set((EventCallback0)delegate
			{
				long playerId = rankData[index].data.PlayerId;
				if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
				{
					item.onClick.Retain();
					GButton btn_Accuse = win.com_Main.com_Fight.btn_Accuse;
					btn_Accuse.data = 0L;
					btn_Accuse.data = playerId;
					GButton btn_Block = win.com_Main.com_Fight.btn_Block;
					btn_Block.data = 0;
					if (rankData[index].data.PlayerId > 100000)
					{
						btn_Accuse.data = playerId;
						btn_Block.data = rankData[index].data;
						SetOperationVisible(active: true);
					}
					else
					{
						SetOperationVisible(active: false);
					}
					if (SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(rankData[index].data.PlayerId) || rankData[index].data.PlayerId <= 100000)
					{
						btn_Block.grayed = true;
					}
					else
					{
						btn_Block.grayed = false;
					}
					win.com_Main.com_Fight.group_Operation.SetXY(item.x - btn_Block.width, item.y - item.height / 5f);
					item.onClick.Release();
				}
			});
		}
		else
		{
			item.visible = false;
		}
	}

	private void RefreshPlayerLabel(UIAccountInfo_Com_Rank item, RecordRank data)
	{
		item.txt_Gold.text = data.data.Gold.ToString();
		item.list_Level.itemRenderer = delegate(int index, GObject star)
		{
			if (star is UIAccountInfoo_Com_PlayerLevel uIAccountInfoo_Com_PlayerLevel)
			{
				uIAccountInfoo_Com_PlayerLevel.slot.selectedIndex = Mathf.Min(data.data.Slot, 3);
				uIAccountInfoo_Com_PlayerLevel.avtiveLevel.selectedIndex = ((data.data.Lv > index) ? 1 : 0);
			}
		};
		item.list_Level.numItems = StaticConfigure.Upgrade.Datas[0].UpgradeDataConfigureItems.Count;
	}

	private void OnBlackList()
	{
		GComponent gComponent = base.contentPane;
		UIAccountInfoWindow win = gComponent as UIAccountInfoWindow;
		if (win != null && win.com_Main.com_Fight.btn_Block.data is PlayerFightData playerFightData && !SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(playerFightData.PlayerId))
		{
			win.com_Main.com_Fight.btn_Block.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendOpC2S(playerFightData.PlayerId, playerFightData.Name, 2, delegate
			{
				win.com_Main.com_Fight.btn_Block.grayed = true;
				SetOperationVisible(active: false);
				win.com_Main.com_Fight.btn_Block.onClick.Release();
			});
		}
	}

	private void SetOperationVisible(bool active)
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.com_Fight.btn_Accuse.visible = active;
			uIAccountInfoWindow.com_Main.com_Fight.btn_Block.visible = active;
		}
	}

	private void RefreshRecordOperation(BattleShortRecord shortRecord)
	{
		GComponent gComponent = base.contentPane;
		UIAccountInfoWindow win = gComponent as UIAccountInfoWindow;
		if (win == null)
		{
			return;
		}
		bool flag = shortRecord.IsValidReplay() && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId);
		bool flag2 = IsReplayCached(shortRecord.ReplayId);
		win.com_Main.com_Fight.btn_ReplayPlay.visible = flag;
		win.com_Main.com_Fight.btn_ReplaySave.visible = flag;
		win.com_Main.com_Fight.btn_CopyReplayId.visible = flag;
		win.com_Main.com_Fight.btn_ReplaySaving.visible = false;
		if (!flag)
		{
			return;
		}
		Controller saveButtonController = win.com_Main.com_Fight.btn_ReplaySave.type;
		if (flag2)
		{
			if (shortRecord.Type == BattleShortRecordType.Replay)
			{
				saveButtonController.selectedIndex = 2;
			}
			else
			{
				saveButtonController.selectedIndex = 3;
			}
		}
		else
		{
			saveButtonController.selectedIndex = 0;
		}
		bool flag3 = shortRecord.IsValidVersion();
		win.com_Main.com_Fight.btn_ReplayPlay.grayed = !flag3;
		win.com_Main.com_Fight.btn_ReplaySave.grayed = saveButtonController.selectedIndex == 0 && !flag3;
		win.com_Main.com_Fight.btn_ReplaySave.onClick.Set((EventCallback0)delegate
		{
			if (saveButtonController.selectedIndex == 0)
			{
				if (win.com_Main.com_Fight.btn_ReplaySave.grayed)
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1140);
				}
				else
				{
					win.com_Main.com_Fight.btn_ReplaySave.onClick.Retain();
					SaveReplayFile(shortRecord);
				}
			}
			else if (saveButtonController.selectedIndex == 2)
			{
				win.com_Main.com_Fight.btn_ReplaySave.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1139, delegate
				{
					if (SimpleSingletonProvider<GameLogicManager>.inst.replay.DeleteReplayFile(shortRecord.ReplayId))
					{
						playerCareer.RemoveReplayRecord(shortRecord.ReplayId);
					}
					RefreshRecordOperation(shortRecord);
				});
				win.com_Main.com_Fight.btn_ReplaySave.onClick.Release();
			}
		});
		win.com_Main.com_Fight.btn_ReplayPlay.onClick.Set((EventCallback0)delegate
		{
			if (win.com_Main.com_Fight.btn_ReplayPlay.grayed)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1140);
			}
			else
			{
				RequestPlayReplay(shortRecord.ReplayId);
			}
		});
		win.com_Main.com_Fight.btn_CopyReplayId.onClick.Set((EventCallback0)delegate
		{
			win.com_Main.com_Fight.btn_CopyReplayId.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Spectate));
			GUIUtility.systemCopyBuffer = win.com_Main.com_Fight.txt_ReplayId.text;
			win.com_Main.com_Fight.btn_CopyReplayId.onClick.Release();
		});
	}

	private async void SaveReplayFile(BattleShortRecord shortRecord)
	{
		if (!shortRecord.IsValidVersion())
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (gComponent is UIAccountInfoWindow win)
		{
			win.com_Main.com_Fight.btn_ReplayPlay.onClick.Retain();
			win.com_Main.com_Fight.btn_ReplaySave.onClick.Retain();
			win.com_Main.list_FightData.touchable = false;
			win.com_Main.com_Fight.btn_ReplaySaving.visible = true;
			ReplayLogic.ReplayLoadStatus item = (await SimpleSingletonProvider<GameLogicManager>.inst.replay.TryLoadReplayFile(shortRecord.ReplayId, needSave: true)).Item2;
			win.com_Main.com_Fight.btn_ReplaySaving.visible = false;
			if (item == ReplayLogic.ReplayLoadStatus.Success)
			{
				RefreshRecordOperation(shortRecord);
			}
			else
			{
				HandleReplayFileFail(item);
			}
			win.com_Main.com_Fight.btn_ReplaySave.onClick.Release();
			win.com_Main.com_Fight.btn_ReplayPlay.onClick.Release();
			win.com_Main.list_FightData.touchable = true;
		}
	}

	private async void RequestPlayReplay(string replayId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			return;
		}
		GComponent gComponent = base.contentPane;
		if (gComponent is UIAccountInfoWindow win && !string.IsNullOrEmpty(replayId))
		{
			win.com_Main.com_Fight.btn_ReplayPlay.onClick.Retain();
			win.com_Main.com_Fight.btn_ReplaySave.onClick.Retain();
			win.com_Main.list_FightData.touchable = false;
			var (bytes, replayLoadStatus) = await SimpleSingletonProvider<GameLogicManager>.inst.replay.TryLoadReplayFile(replayId, IsReplayCached(replayId));
			if (replayLoadStatus == ReplayLogic.ReplayLoadStatus.Success)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.replay.ShowReplayResultAsync(bytes);
				QuitInfoWindow();
			}
			else
			{
				HandleReplayFileFail(replayLoadStatus);
			}
			win.com_Main.list_FightData.touchable = true;
			win.com_Main.com_Fight.btn_ReplayPlay.onClick.Release();
			win.com_Main.com_Fight.btn_ReplaySave.onClick.Release();
		}
	}

	private bool IsReplayCached(string replayId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.replay.IsReplayCached(replayId);
	}

	private void HandleReplayFileFail(ReplayLogic.ReplayLoadStatus status)
	{
		switch (status)
		{
		case ReplayLogic.ReplayLoadStatus.SaveLimitReached:
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1145);
			break;
		case ReplayLogic.ReplayLoadStatus.DownloadFailed:
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1137);
			break;
		case ReplayLogic.ReplayLoadStatus.ReadFailed:
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1138);
			break;
		}
	}

	private void InitComponents_Achieve()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Achieve.list_Achieve.itemRenderer = RendererAchieveItem;
		}
	}

	private void AddEvent_Achieve()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Achieve.mohu.onClick.Add(CloseAchieve);
			uIAccountInfoWindow.com_Achieve.btn_Sure.onClick.Add(CloseAchieve);
			uIAccountInfoWindow.com_Main.btn_Achieve_0.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(0);
			});
			uIAccountInfoWindow.com_Main.btn_Achieve_1.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(1);
			});
			uIAccountInfoWindow.com_Main.btn_Achieve_2.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(2);
			});
			uIAccountInfoWindow.com_Main.btn_Achieve_3.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(3);
			});
			uIAccountInfoWindow.com_Main.btn_Achieve_4.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(4);
			});
			uIAccountInfoWindow.com_Main.btn_Achieve_5.onClick.Set((EventCallback0)delegate
			{
				RefreshAchieveComponent(5);
			});
		}
	}

	private void RemoveEvent_Achieve()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Achieve.mohu.onClick.Remove(CloseAchieve);
			uIAccountInfoWindow.com_Achieve.btn_Sure.onClick.Remove(CloseAchieve);
		}
	}

	private void CloseAchieve()
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return;
		}
		uIAccountInfoWindow.com_Achieve.mohu.onClick.Retain();
		if (selectedAchieveId != 0)
		{
			for (int i = 0; i < playerCareer.acheveIds.Count; i++)
			{
				if (playerCareer.acheveIds[i] == selectedAchieveId)
				{
					playerCareer.acheveIds[i] = 0;
					break;
				}
			}
		}
		playerCareer.acheveIds[achieve_Slot] = selectedAchieveId;
		RequestSetShowPlayer(RefreshAchieve);
	}

	private void RefreshAchieve()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.showAchieve.selectedIndex = 0;
			for (int i = 0; i < 6; i++)
			{
				UIAccountInfo_Button_ShowAchieve showAchieveItemBySlot = GetShowAchieveItemBySlot(i);
				RefreshShowAchieve(showAchieveItemBySlot, (playerCareer.acheveIds.Count > i) ? playerCareer.acheveIds[i] : 0);
			}
			uIAccountInfoWindow.com_Achieve.mohu.onClick.Release();
			uIAccountInfoWindow.com_Achieve.btn_Sure.onClick.Release();
		}
	}

	private void RefreshShowAchieve(UIAccountInfo_Button_ShowAchieve showAchieve, int achieveId)
	{
		showAchieve.isLoaded.selectedIndex = ((achieveId > 0) ? 1 : 0);
		if (achieveId > 0)
		{
			RefreshSelectAchieve(showAchieve.com_AchieveLoader, null, achieveId);
		}
	}

	private UIAccountInfo_Button_ShowAchieve GetShowAchieveItemBySlot(int slot)
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return null;
		}
		return slot switch
		{
			0 => uIAccountInfoWindow.com_Main.btn_Achieve_0, 
			1 => uIAccountInfoWindow.com_Main.btn_Achieve_1, 
			2 => uIAccountInfoWindow.com_Main.btn_Achieve_2, 
			3 => uIAccountInfoWindow.com_Main.btn_Achieve_3, 
			4 => uIAccountInfoWindow.com_Main.btn_Achieve_4, 
			5 => uIAccountInfoWindow.com_Main.btn_Achieve_5, 
			_ => null, 
		};
	}

	private void RefreshAchieveComponent(int index)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerCareer.playerId))
		{
			hasSelectedAcheveBtn = null;
			achieve_Slot = index;
			selectedAchieveId = ((playerCareer.acheveIds.Count > index) ? playerCareer.acheveIds[index] : 0);
			if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
			{
				achieveIds = SimpleSingletonProvider<GameLogicManager>.inst.task.GetReachAchieveids();
				uIAccountInfoWindow.com_Achieve.list_Achieve.numItems = achieveIds.Count;
				uIAccountInfoWindow.showAchieve.selectedIndex = 1;
			}
		}
	}

	private void RendererAchieveItem(int index, GObject item)
	{
		UIAccountInfo_Button_SelectAchieve btn_SelectAchieve = item as UIAccountInfo_Button_SelectAchieve;
		if (btn_SelectAchieve == null || achieveIds.Count <= index)
		{
			return;
		}
		int rendererAchieveId = achieveIds[index];
		btn_SelectAchieve.isShow.selectedIndex = (playerCareer.acheveIds.Contains(rendererAchieveId) ? 1 : 0);
		RefreshSelectAchieve(btn_SelectAchieve.com_AchieveLoader, btn_SelectAchieve.txt_Achieve, rendererAchieveId);
		btn_SelectAchieve.hasSelected.selectedIndex = 0;
		if (selectedAchieveId == rendererAchieveId)
		{
			hasSelectedAcheveBtn = btn_SelectAchieve;
			hasSelectedAcheveBtn.hasSelected.selectedIndex = 1;
		}
		btn_SelectAchieve.onClick.Set((EventCallback0)delegate
		{
			btn_SelectAchieve.onClick.Retain();
			if (hasSelectedAcheveBtn != null && hasSelectedAcheveBtn != btn_SelectAchieve)
			{
				hasSelectedAcheveBtn.hasSelected.selectedIndex = 0;
			}
			btn_SelectAchieve.hasSelected.selectedIndex = ((btn_SelectAchieve.hasSelected.selectedIndex == 0) ? 1 : 0);
			selectedAchieveId = ((btn_SelectAchieve.hasSelected.selectedIndex == 1) ? rendererAchieveId : 0);
			hasSelectedAcheveBtn = btn_SelectAchieve;
			btn_SelectAchieve.onClick.Release();
		});
	}

	private void RefreshSelectAchieve(UIAccountInfo_Com_AchieveLoader btn_SelectAchieve, GTextField textField, int achieveId)
	{
		if (StaticConfigure.Achieve.GlobalDict.TryGetValue(achieveId, out var value))
		{
			if (textField != null)
			{
				textField.text = value.NameID.GetLocal(UIStringType.Achieve);
			}
			btn_SelectAchieve.loader_Achieve.url = value.Icon.GetImageLocalization();
		}
	}

	private void ReadyHeroInfo()
	{
		HeroCardLogic heroCardLogic = SimpleSingletonProvider<GameLogicManager>.inst.heroCard;
		_HeroCards = heroCardLogic.GetHeroCards(checkStatus: false);
		_HeroCards.Sort((HeroCardData x, HeroCardData y) => heroCardLogic.CompareTo(x, y, ascending: true));
	}

	private void InitComponents_HeroSelect()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Hero.list_Hero.itemRenderer = RendererHero;
		}
	}

	private void ShowHeroSelect()
	{
		if (!(base.contentPane is UIAccountInfoWindow uIAccountInfoWindow))
		{
			return;
		}
		hasHeroCount = 0;
		for (int i = 0; i < _HeroCards.Count; i++)
		{
			if (_HeroCards[i].IsHas)
			{
				hasHeroCount++;
			}
		}
		uIAccountInfoWindow.com_Hero.list_Hero.numItems = hasHeroCount;
		uIAccountInfoWindow.showHero.selectedIndex = 1;
	}

	private void AddEvent_HeroSelect()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_RefreshSkin.onClick.Add(ShowHeroSelect);
			uIAccountInfoWindow.com_Hero.mohu.onClick.Add(FinishHeroSelect);
			uIAccountInfoWindow.com_Hero.btn_Sure.onClick.Add(FinishHeroSelect);
		}
	}

	private void RemoveEvent_HeroSelect()
	{
		if (base.contentPane is UIAccountInfoWindow uIAccountInfoWindow)
		{
			uIAccountInfoWindow.com_Main.btn_RefreshSkin.onClick.Remove(ShowHeroSelect);
			uIAccountInfoWindow.com_Hero.mohu.onClick.Remove(FinishHeroSelect);
			uIAccountInfoWindow.com_Hero.btn_Sure.onClick.Remove(FinishHeroSelect);
		}
	}

	private void FinishHeroSelect()
	{
		GComponent gComponent = base.contentPane;
		UIAccountInfoWindow win = gComponent as UIAccountInfoWindow;
		if (win != null && win.com_Hero.list_Hero.selectedIndex >= 0)
		{
			win.com_Hero.mohu.onClick.Retain();
			playerCareer.StandingPainting = _HeroCards[win.com_Hero.list_Hero.selectedIndex].standingPainting.ItemID;
			RequestSetShowPlayer(delegate
			{
				RefreshSkin();
				win.showHero.selectedIndex = 0;
				win.com_Hero.mohu.onClick.Release();
			});
		}
	}

	private void RendererHero(int index, GObject item)
	{
		if (item is UIAccountInfo_Button_Hero uIAccountInfo_Button_Hero)
		{
			HeroCardData heroCardData = _HeroCards[index];
			uIAccountInfo_Button_Hero.loader_Character.loader_Character.url = heroCardData.standingPainting.GetCharacterPhoto();
			uIAccountInfo_Button_Hero.grayed = !heroCardData.IsHas;
			uIAccountInfo_Button_Hero.txt_chrname.text = heroCardData.InfoConfig.NickID.GetLocal(UIStringType.Character);
			bool isBreakThrough = heroCardData.isBreakThrough;
			uIAccountInfo_Button_Hero.breakthrough.selectedIndex = (isBreakThrough ? 1 : 0);
			uIAccountInfo_Button_Hero.selected = heroCardData.standingPainting.ItemID == playerCareer.StandingPainting;
			uIAccountInfo_Button_Hero.btn_Collect.visible = heroCardData.CollectStatus;
			if (uIAccountInfo_Button_Hero.btn_Collect is UIButton_Collect uIButton_Collect)
			{
				uIButton_Collect.collected.selectedIndex = 1;
			}
		}
	}
}
