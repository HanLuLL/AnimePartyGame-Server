using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class AssistVoteWindow : AssistVoteBaseWindow
{
	public AssistVoteWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIAssistVoteWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
		{
			GButton btn_SelectLeft = uIAssistVoteWindow.btn_SelectLeft;
			GButton btn_SelectRight = uIAssistVoteWindow.btn_SelectRight;
			GButton btn_SureVote = uIAssistVoteWindow.btn_SureVote;
			bool flag = (uIAssistVoteWindow.progress_Time.visible = false);
			bool flag3 = (btn_SureVote.visible = flag);
			bool flag5 = (btn_SelectRight.visible = flag3);
			btn_SelectLeft.visible = flag5;
			uIAssistVoteWindow.btn_SureVote.onClick.Retain();
			uIAssistVoteWindow.btn_SureVote.onClick.Add(SureVote);
			uIAssistVoteWindow.btn_SelectLeft.onClick.Add(SelectLeft);
			uIAssistVoteWindow.btn_SelectRight.onClick.Add(SelectRight);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
		{
			uIAssistVoteWindow.btn_SureVote.onClick.Remove(SureVote);
			ActionSn = 0L;
		}
	}

	private void SelectLeft()
	{
		PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
		AssistVoteStatus voteStatus = selfVoteData.VoteStatus;
		if ((voteStatus == AssistVoteStatus.None || voteStatus == AssistVoteStatus.Vote) && !selfVoteData.Affirm)
		{
			PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
			if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
			{
				uIAssistVoteWindow.btn_SelectLeft.onClick.Retain();
				uIAssistVoteWindow.SwapLi.Play();
				uIAssistVoteWindow.btn_SureVote.onClick.Release();
				SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RequestVoteSelectC2S(leftMonster.Id).OnFinishedOnly.AddOnce(RefreshSelectStatus);
			}
		}
	}

	private void SelectRight()
	{
		PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
		AssistVoteStatus voteStatus = selfVoteData.VoteStatus;
		if ((voteStatus == AssistVoteStatus.None || voteStatus == AssistVoteStatus.Vote) && !selfVoteData.Affirm)
		{
			PVEMissionVoteConfigure rightMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster;
			if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
			{
				uIAssistVoteWindow.btn_SelectRight.onClick.Retain();
				uIAssistVoteWindow.SwapJiao.Play();
				uIAssistVoteWindow.btn_SureVote.onClick.Release();
				SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RequestVoteSelectC2S(rightMonster.Id).OnFinishedOnly.AddOnce(RefreshSelectStatus);
			}
		}
	}

	private void SureVote()
	{
		GComponent gComponent = base.contentPane;
		UIAssistVoteWindow win = gComponent as UIAssistVoteWindow;
		if (win == null)
		{
			return;
		}
		win.btn_SureVote.onClick.Retain();
		if (ActionSn != 0L)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RequestVoteC2S(ActionSn).OnFinishedOnly.AddOnce(delegate
			{
				win.progress_Time.visible = false;
				win.btn_SureVote.onClick.Release();
			});
			GButton btn_SelectLeft = win.btn_SelectLeft;
			GButton btn_SelectRight = win.btn_SelectRight;
			bool flag = (win.btn_SureVote.visible = false);
			bool flag3 = (btn_SelectRight.visible = flag);
			btn_SelectLeft.visible = flag3;
			ActionSn = 0L;
		}
	}

	public override async UniTask TryShowAssistVote(Action action)
	{
		await TryShowAsync();
		if (ActionSn == action.Sn || SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().player.Id != action.PlayerId)
		{
			return;
		}
		ActionSn = action.Sn;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			OperationTimer.ActionDownTime(action.Sn, 5309, SureVote, null, RefreshTimeDown, operateCard: false, showTimerToPlayer: false);
			if (!(base.contentPane is UIAssistVoteWindow uIAssistVoteWindow))
			{
				return;
			}
			GButton btn_SelectLeft = uIAssistVoteWindow.btn_SelectLeft;
			GButton btn_SelectRight = uIAssistVoteWindow.btn_SelectRight;
			bool flag = (uIAssistVoteWindow.btn_SureVote.visible = true);
			bool flag3 = (btn_SelectRight.visible = flag);
			btn_SelectLeft.visible = flag3;
		}
		InitAssistVoteWin();
	}

	private void RefreshSelectStatus()
	{
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
		{
			uIAssistVoteWindow.btn_SelectLeft.onClick.Release();
			uIAssistVoteWindow.btn_SelectRight.onClick.Release();
			PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
			bool flag = selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id;
			uIAssistVoteWindow.group_LeftRelic.visible = flag;
			uIAssistVoteWindow.com_LeftInfo.status.selectedIndex = (flag ? 1 : 0);
			uIAssistVoteWindow.loader_Left.grayed = !flag;
			uIAssistVoteWindow.com_LeftInfo.Choose.Play();
			bool flag2 = selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id;
			uIAssistVoteWindow.group_RightRelic.visible = flag2;
			uIAssistVoteWindow.com_RightInfo.status.selectedIndex = (flag2 ? 1 : 0);
			uIAssistVoteWindow.loader_Right.grayed = !flag2;
			uIAssistVoteWindow.com_RightInfo.Choose.Play();
			RefreshTip(1);
		}
	}

	private void RefreshStandingPaint(int monsterId, GLoader loader)
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(monsterId, 0, 0);
		if (configStandingPainting == null)
		{
			Debug.LogError($"{monsterId} does not have a standing painting set.");
			return;
		}
		string item = configStandingPainting.GetCharacter().Item1;
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(item, out var value))
		{
			customOffset = value.skinOffset;
		}
		loader.customOffset = customOffset;
		loader.url = item;
	}

	protected override void InitAssistVoteWin()
	{
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
		{
			uIAssistVoteWindow.Cut_in.Play();
			PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
			PVEMissionVoteConfigure rightMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster;
			string characterNickName = CharacterHandle.GetCharacterNickName(leftMonster.Id);
			string characterNickName2 = CharacterHandle.GetCharacterNickName(rightMonster.Id);
			uIAssistVoteWindow.com_LeftInfo.type.selectedIndex = 0;
			uIAssistVoteWindow.com_LeftInfo.status.selectedIndex = 0;
			GList list_SelectLeft = uIAssistVoteWindow.com_LeftInfo.list_SelectLeft;
			bool flag = (uIAssistVoteWindow.com_LeftInfo.txt_PointTotal.visible = false);
			list_SelectLeft.visible = flag;
			uIAssistVoteWindow.com_LeftInfo.txt_Title.text = characterNickName;
			uIAssistVoteWindow.group_LeftRelic.visible = false;
			uIAssistVoteWindow.com_LeftRelic.type.selectedIndex = 0;
			uIAssistVoteWindow.com_LeftRelic.txt_Target.SetVar("target", characterNickName2).FlushVars();
			RefreshRelicInfo(uIAssistVoteWindow.com_LeftRelic, leftMonster.RelicId);
			uIAssistVoteWindow.com_RightInfo.type.selectedIndex = 1;
			uIAssistVoteWindow.com_RightInfo.status.selectedIndex = 0;
			GList list_SelectLeft2 = uIAssistVoteWindow.com_RightInfo.list_SelectLeft;
			flag = (uIAssistVoteWindow.com_RightInfo.txt_PointTotal.visible = false);
			list_SelectLeft2.visible = flag;
			uIAssistVoteWindow.com_RightInfo.txt_Title.text = characterNickName2;
			uIAssistVoteWindow.group_RightRelic.visible = false;
			uIAssistVoteWindow.com_RightRelic.type.selectedIndex = 1;
			uIAssistVoteWindow.com_RightRelic.txt_Target.SetVar("target", characterNickName).FlushVars();
			RefreshRelicInfo(uIAssistVoteWindow.com_RightRelic, rightMonster.RelicId);
			RefreshTip(0);
		}
	}

	private void RefreshRelicInfo(UIAssistVote_Com_RelicInfo comLeftRelic, int RelicId)
	{
		RelicInfoConfigure relicInfoConfigure = RelicId.GetRelicInfoConfigure();
		if (relicInfoConfigure != null)
		{
			((UICom_Relic_Quality)comLeftRelic.btn_Relic.com_Quality).quality.selectedIndex = (int)relicInfoConfigure.RelicQualityType;
			comLeftRelic.btn_Relic.loader_Relic.url = relicInfoConfigure.Icon;
			comLeftRelic.txt_RelicName.text = relicInfoConfigure.NameID.GetLocal(UIStringType.Relic);
			comLeftRelic.txt_RelicDesc.text = relicInfoConfigure.DescID.GetLocal(UIStringType.Relic);
		}
	}

	private void RefreshTimeDown(float curTime, float operateTime)
	{
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow)
		{
			uIAssistVoteWindow.progress_Time.visible = operateTime - curTime > 0f;
			uIAssistVoteWindow.progress_Time.max = operateTime;
			uIAssistVoteWindow.progress_Time.value = operateTime - curTime;
		}
	}

	private void RefreshTip(int index)
	{
		if (!(base.contentPane is UIAssistVoteWindow uIAssistVoteWindow))
		{
			return;
		}
		uIAssistVoteWindow.tipStep.selectedIndex = Mathf.Clamp(index, 0, 3);
		if (index == 1)
		{
			PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
			if (selfVoteData != null && selfVoteData.SelectVoteCharacterInfo != null)
			{
				uIAssistVoteWindow.txt_Tip2.SetVar("target", CharacterHandle.GetCharacterNickName(selfVoteData.SelectVoteCharacterInfo.Id, selfVoteData.SelectVoteCharacterInfo.CharacterType)).FlushVars();
			}
		}
	}

	public override async UniTask RefreshVoteData(bool showPoint)
	{
		Dictionary<long, PlayerVoteData> playerVoteDict = base.AssistVote.PlayerVoteDict;
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIAssistVoteWindow win))
		{
			return;
		}
		LeftVoteData.Clear();
		RightVoteData.Clear();
		int num = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id;
		int num2 = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id;
		foreach (PlayerVoteData value in playerVoteDict.Values)
		{
			if (value.SelectId == num)
			{
				LeftVoteData.Add(value);
			}
			else if (value.SelectId == num2)
			{
				RightVoteData.Add(value);
			}
		}
		if (showPoint)
		{
			GLoader loader_Left = win.loader_Left;
			bool flag = (win.loader_Right.grayed = false);
			loader_Left.grayed = flag;
			win.PK.Play();
			RefreshTip(2);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		}
		win.com_LeftInfo.txt_PointTotal.visible = false;
		win.com_RightInfo.txt_PointTotal.visible = false;
		RefreshVoteList(win.com_LeftInfo.list_SelectLeft, LeftVoteData, showPoint);
		RefreshVoteList(win.com_RightInfo.list_SelectLeft, RightVoteData, showPoint);
		if (showPoint)
		{
			RefreshTip(3);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
			int num3 = RefreshVoteTotal(win.com_LeftInfo, LeftVoteData);
			int num4 = RefreshVoteTotal(win.com_RightInfo, RightVoteData);
			win.com_LeftInfo.diceScale.selectedIndex = ((num3 > num4) ? 1 : 0);
			win.com_RightInfo.diceScale.selectedIndex = ((num3 <= num4) ? 1 : 0);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
			await VoteOver();
		}
	}

	private int RefreshVoteTotal(UIAssistVote_Com_NPCInfo comInfo, List<PlayerVoteData> VoteData)
	{
		int num = 0;
		for (int i = 0; i < VoteData.Count; i++)
		{
			num += VoteData[i].Point;
		}
		comInfo.txt_PointTotal.text = num.ToString();
		comInfo.txt_PointTotal.visible = true;
		comInfo.Number.Play();
		List<GObject> children = comInfo.list_SelectLeft._children;
		if (children != null && children.Count > 0)
		{
			for (int j = 0; j < children.Count; j++)
			{
				if (children[j] is UIAssistVote_Com_VoteItem uIAssistVote_Com_VoteItem)
				{
					uIAssistVote_Com_VoteItem.com_Point.visible = false;
				}
			}
		}
		return num;
	}

	private void RefreshVoteList(GList list_Select, List<PlayerVoteData> VoteData, bool showPoint)
	{
		VoteData.Sort(delegate(PlayerVoteData voteX, PlayerVoteData voteY)
		{
			RoomPlayer roomPlayer = voteX.PlayerData?.player;
			RoomPlayer roomPlayer2 = voteY.PlayerData?.player;
			return (roomPlayer != null && roomPlayer2 != null) ? roomPlayer.Slot.CompareTo(roomPlayer2.Slot) : 0;
		});
		list_Select.visible = true;
		list_Select.itemRenderer = delegate(int index, GObject item)
		{
			if (VoteData.Count > index && item is UIAssistVote_Com_VoteItem uIAssistVote_Com_VoteItem)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(VoteData[index].PlayerId);
				if (playerDataById != null)
				{
					string profilePhoto = playerDataById.player.standingPainting.ProfilePhoto;
					if (uIAssistVote_Com_VoteItem.loader_Icon.url == null || !uIAssistVote_Com_VoteItem.loader_Icon.url.Equals(profilePhoto))
					{
						uIAssistVote_Com_VoteItem.loader_Icon.url = profilePhoto;
						if (VoteData[index].VoteStatus == AssistVoteStatus.Vote)
						{
							uIAssistVote_Com_VoteItem.Cut_in.Play();
						}
					}
					if (VoteData[index].VoteStatus == AssistVoteStatus.WaitVoteResult && uIAssistVote_Com_VoteItem.status.selectedIndex == 0)
					{
						uIAssistVote_Com_VoteItem.OK.Play();
					}
					uIAssistVote_Com_VoteItem.status.selectedIndex = (VoteData[index].Affirm ? ((VoteData[index].Point <= 0) ? 1 : 2) : 0);
					UICom_Point com_Point = (UICom_Point)uIAssistVote_Com_VoteItem.com_Point;
					Controller player = uIAssistVote_Com_VoteItem.Player;
					int selectedIndex = (com_Point.player.selectedIndex = playerDataById.player.Slot);
					player.selectedIndex = selectedIndex;
					uIAssistVote_Com_VoteItem.com_Point.visible = showPoint;
					if (showPoint)
					{
						uIAssistVote_Com_VoteItem.Roll.Play();
						com_Point.txt_Point.text = VoteData[index].Point.ToString();
						com_Point.aMovie_Dice.onPlayEnd.Set((EventCallback0)delegate
						{
							com_Point.PointChange.Play();
							com_Point.aMovie_Dice.playing = false;
						});
						com_Point.aMovie_Dice.SetPlaySettings(0, -1, 1, VoteData[index].Point - 1);
						com_Point.group_Point.visible = false;
						com_Point.aMovie_Dice.playing = true;
						VoteData[index].VoteStatus = AssistVoteStatus.Over;
					}
				}
			}
		};
		list_Select.numItems = VoteData.Count;
	}

	public override async UniTask TryShowAssistVoteAfterReconnect(bool result)
	{
		await TryShowAsync();
		InitAssistVoteWin();
		await RefreshVoteData(result);
	}

	public override async UniTask VoteOver()
	{
		if (!(base.contentPane is UIAssistVoteWindow uIAssistVoteWindow))
		{
			return;
		}
		PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
		PVEMissionVoteConfigure rightMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster;
		Dictionary<long, PlayerVoteData> playerVoteDict = base.AssistVote.PlayerVoteDict;
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<long, PlayerVoteData> item in playerVoteDict)
		{
			int point = item.Value.Point;
			if (leftMonster.Id == item.Value.SelectId)
			{
				num += ((point == 0) ? 1 : point);
			}
			else if (rightMonster.Id == item.Value.SelectId)
			{
				num2 += ((point == 0) ? 1 : point);
			}
		}
		uIAssistVoteWindow.PK.Stop();
		uIAssistVoteWindow.SwapJiao.Stop();
		uIAssistVoteWindow.SwapLi.Stop();
		uIAssistVoteWindow.BackJiao.Stop();
		uIAssistVoteWindow.BackLi.Stop();
		if (num > num2)
		{
			uIAssistVoteWindow.PickLi.Play();
		}
		else
		{
			uIAssistVoteWindow.PickJiao.Play();
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
		await base.VoteOver();
	}

	public override void ResetUI(long playerId, int selectId)
	{
		if (base.contentPane is UIAssistVoteWindow uIAssistVoteWindow && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
			if (selectId == leftMonster.Id)
			{
				uIAssistVoteWindow.BackLi.Play();
			}
			else
			{
				uIAssistVoteWindow.BackJiao.Play();
			}
		}
	}
}
