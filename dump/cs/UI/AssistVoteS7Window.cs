using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class AssistVoteS7Window : AssistVoteBaseWindow
{
	private RepeatedField<int> _voteIds;

	protected readonly List<PlayerVoteData> CenterVoteData = new List<PlayerVoteData>();

	public AssistVoteS7Window(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIAssistVoteS7Window.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
		{
			GButton btn_SelectLeft = uIAssistVoteS7Window.btn_SelectLeft;
			GButton btn_SelectRight = uIAssistVoteS7Window.btn_SelectRight;
			GButton btn_SelectCenter = uIAssistVoteS7Window.btn_SelectCenter;
			GButton btn_SureVote = uIAssistVoteS7Window.btn_SureVote;
			bool flag = (uIAssistVoteS7Window.progress_Time.visible = false);
			bool flag3 = (btn_SureVote.visible = flag);
			bool flag5 = (btn_SelectCenter.visible = flag3);
			bool flag7 = (btn_SelectRight.visible = flag5);
			btn_SelectLeft.visible = flag7;
			uIAssistVoteS7Window.btn_SureVote.onClick.Retain();
			uIAssistVoteS7Window.btn_SureVote.onClick.Add(SureVote);
			uIAssistVoteS7Window.btn_SelectLeft.onClick.Add(SelectLeft);
			uIAssistVoteS7Window.btn_SelectRight.onClick.Add(SelectRight);
			uIAssistVoteS7Window.btn_SelectCenter.onClick.Add(SelectCenter);
			uIAssistVoteS7Window.select.selectedIndex = 0;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
		{
			uIAssistVoteS7Window.btn_SureVote.onClick.Remove(SureVote);
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
			if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
			{
				uIAssistVoteS7Window.btn_SelectLeft.onClick.Retain();
				uIAssistVoteS7Window.select.selectedIndex = 1;
				uIAssistVoteS7Window.SwapBlue.Play();
				uIAssistVoteS7Window.btn_SureVote.onClick.Release();
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
			if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
			{
				uIAssistVoteS7Window.btn_SelectRight.onClick.Retain();
				uIAssistVoteS7Window.select.selectedIndex = 2;
				uIAssistVoteS7Window.SwapRed.Play();
				uIAssistVoteS7Window.btn_SureVote.onClick.Release();
				SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RequestVoteSelectC2S(rightMonster.Id).OnFinishedOnly.AddOnce(RefreshSelectStatus);
			}
		}
	}

	private void SelectCenter()
	{
		PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
		AssistVoteStatus voteStatus = selfVoteData.VoteStatus;
		if ((voteStatus == AssistVoteStatus.None || voteStatus == AssistVoteStatus.Vote) && !selfVoteData.Affirm)
		{
			PVEMissionVoteConfigure centerMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.CenterMonster;
			if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
			{
				uIAssistVoteS7Window.btn_SelectCenter.onClick.Retain();
				uIAssistVoteS7Window.select.selectedIndex = 3;
				uIAssistVoteS7Window.SwapMid.Play();
				uIAssistVoteS7Window.btn_SureVote.onClick.Release();
				SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RequestVoteSelectC2S(centerMonster.Id).OnFinishedOnly.AddOnce(RefreshSelectStatus);
			}
		}
	}

	private void SureVote()
	{
		GComponent gComponent = base.contentPane;
		UIAssistVoteS7Window win = gComponent as UIAssistVoteS7Window;
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
			GButton btn_SelectCenter = win.btn_SelectCenter;
			bool flag = (win.btn_SureVote.visible = false);
			bool flag3 = (btn_SelectCenter.visible = flag);
			bool flag5 = (btn_SelectRight.visible = flag3);
			btn_SelectLeft.visible = flag5;
			ActionSn = 0L;
		}
	}

	public override async UniTask TryShowAssistVote(Action action)
	{
		await TryShowAsync();
		if (ActionSn == action.Sn)
		{
			return;
		}
		VoteC2S voteC2S = ByteBuf.ReadObject<VoteC2S>(action.Data.ToByteArray());
		if (voteC2S?.VoteIds == null || voteC2S.VoteIds.Count == 0)
		{
			Debug.LogError("VoteC2S 数据为空！");
		}
		else
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().player.Id != action.PlayerId)
			{
				return;
			}
			ActionSn = action.Sn;
			_voteIds = voteC2S.VoteIds;
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
			{
				OperationTimer.ActionDownTime(action.Sn, 5309, SureVote, null, RefreshTimeDown, operateCard: false, showTimerToPlayer: false);
				if (!(base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window))
				{
					return;
				}
				GButton btn_SelectLeft = uIAssistVoteS7Window.btn_SelectLeft;
				bool flag = (uIAssistVoteS7Window.btn_SelectRight.visible = false);
				btn_SelectLeft.visible = flag;
				GButton btn_SelectLeft2 = uIAssistVoteS7Window.btn_SelectLeft;
				flag = (uIAssistVoteS7Window.btn_SelectRight.grayed = true);
				btn_SelectLeft2.grayed = flag;
				UILanSeZuoBian loader_Left = uIAssistVoteS7Window.loader_Left;
				flag = (uIAssistVoteS7Window.loader_Right.grayed = true);
				loader_Left.grayed = flag;
				foreach (int voteId in voteC2S.VoteIds)
				{
					if (voteId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id)
					{
						uIAssistVoteS7Window.btn_SelectLeft.visible = true;
						uIAssistVoteS7Window.btn_SelectLeft.grayed = false;
						uIAssistVoteS7Window.loader_Left.grayed = false;
					}
					else if (voteId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id)
					{
						uIAssistVoteS7Window.btn_SelectRight.visible = true;
						uIAssistVoteS7Window.btn_SelectRight.grayed = false;
						uIAssistVoteS7Window.loader_Right.grayed = false;
					}
				}
				uIAssistVoteS7Window.btn_SelectCenter.visible = true;
				uIAssistVoteS7Window.btn_SureVote.visible = true;
			}
			InitAssistVoteWin();
		}
	}

	private void RefreshSelectStatus()
	{
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
		{
			uIAssistVoteS7Window.btn_SelectLeft.onClick.Release();
			uIAssistVoteS7Window.btn_SelectRight.onClick.Release();
			uIAssistVoteS7Window.btn_SelectCenter.onClick.Release();
			PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
			bool flag = selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id;
			uIAssistVoteS7Window.group_LeftRelic.visible = flag;
			uIAssistVoteS7Window.com_LeftInfo.status.selectedIndex = (flag ? 1 : 0);
			uIAssistVoteS7Window.loader_Left.grayed = !flag;
			uIAssistVoteS7Window.com_LeftInfo.Choose.Play();
			bool flag2 = selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id;
			uIAssistVoteS7Window.group_RightRelic.visible = flag2;
			uIAssistVoteS7Window.com_RightInfo.status.selectedIndex = (flag2 ? 1 : 0);
			uIAssistVoteS7Window.loader_Right.grayed = !flag2;
			uIAssistVoteS7Window.com_RightInfo.Choose.Play();
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
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
		{
			uIAssistVoteS7Window.Cut_in.Play();
			uIAssistVoteS7Window.com_LeftInfo.type.selectedIndex = 0;
			uIAssistVoteS7Window.com_LeftInfo.status.selectedIndex = 0;
			GList list_SelectLeft = uIAssistVoteS7Window.com_LeftInfo.list_SelectLeft;
			bool flag = (uIAssistVoteS7Window.com_LeftInfo.txt_PointTotal.visible = false);
			list_SelectLeft.visible = flag;
			uIAssistVoteS7Window.com_LeftInfo.txt_Title.text = 8201501.GetLocal(UIStringType.GUI);
			uIAssistVoteS7Window.group_LeftRelic.visible = false;
			uIAssistVoteS7Window.com_LeftRelic.type.selectedIndex = 0;
			uIAssistVoteS7Window.com_LeftRelic.txt_Target.text = 8201503.GetLocal(UIStringType.GUI);
			uIAssistVoteS7Window.com_LeftRelic.txt_Target.FlushVars();
			string local = 8201502.GetLocal(UIStringType.GUI);
			uIAssistVoteS7Window.com_RightInfo.type.selectedIndex = 1;
			uIAssistVoteS7Window.com_RightInfo.status.selectedIndex = 0;
			GList list_SelectLeft2 = uIAssistVoteS7Window.com_RightInfo.list_SelectLeft;
			flag = (uIAssistVoteS7Window.com_RightInfo.txt_PointTotal.visible = false);
			list_SelectLeft2.visible = flag;
			uIAssistVoteS7Window.com_RightInfo.txt_Title.text = local;
			uIAssistVoteS7Window.group_RightRelic.visible = false;
			uIAssistVoteS7Window.com_RightRelic.type.selectedIndex = 1;
			uIAssistVoteS7Window.com_RightRelic.txt_Target.text = 8201504.GetLocal(UIStringType.GUI);
			uIAssistVoteS7Window.com_RightRelic.txt_Target.FlushVars();
			RepeatedField<int> voteIds = _voteIds;
			string text = ((voteIds != null && voteIds.Count == 3) ? 105302.GetLocal(UIStringType.PVEMission) : 105301.GetLocal(UIStringType.PVEMission));
			uIAssistVoteS7Window.com_CenterInfo.type.selectedIndex = 2;
			uIAssistVoteS7Window.com_CenterInfo.status.selectedIndex = 0;
			GList list_SelectLeft3 = uIAssistVoteS7Window.com_CenterInfo.list_SelectLeft;
			flag = (uIAssistVoteS7Window.com_CenterInfo.txt_PointTotal.visible = false);
			list_SelectLeft3.visible = flag;
			uIAssistVoteS7Window.com_CenterInfo.txt_Title.text = text;
			RefreshTip(0);
		}
	}

	private void RefreshTimeDown(float curTime, float operateTime)
	{
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window)
		{
			uIAssistVoteS7Window.progress_Time.visible = operateTime - curTime > 0f;
			uIAssistVoteS7Window.progress_Time.max = operateTime;
			uIAssistVoteS7Window.progress_Time.value = operateTime - curTime;
		}
	}

	private void RefreshTip(int index)
	{
		if (!(base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window))
		{
			return;
		}
		uIAssistVoteS7Window.tipStep.selectedIndex = Mathf.Clamp(index, 0, 3);
		if (index == 1)
		{
			PlayerVoteData selfVoteData = base.AssistVote.GetSelfVoteData();
			if (selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id)
			{
				uIAssistVoteS7Window.txt_Tip2.SetVar("target", 8201501.GetLocal(UIStringType.GUI)).FlushVars();
			}
			else if (selfVoteData.SelectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id)
			{
				uIAssistVoteS7Window.txt_Tip2.SetVar("target", 8201502.GetLocal(UIStringType.GUI)).FlushVars();
			}
			else
			{
				uIAssistVoteS7Window.txt_Tip2.text = "";
			}
		}
	}

	public override async UniTask RefreshVoteData(bool showPoint)
	{
		Dictionary<long, PlayerVoteData> playerVoteDict = base.AssistVote.PlayerVoteDict;
		GComponent gComponent = base.contentPane;
		UIAssistVoteS7Window win = gComponent as UIAssistVoteS7Window;
		if (win == null)
		{
			return;
		}
		LeftVoteData.Clear();
		RightVoteData.Clear();
		CenterVoteData.Clear();
		int num = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id;
		int num2 = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id;
		int num3 = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.CenterMonster.Id;
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
			else if (value.SelectId == num3)
			{
				CenterVoteData.Add(value);
			}
		}
		if (showPoint)
		{
			UILanSeZuoBian loader_Left = win.loader_Left;
			bool flag = (win.loader_Right.grayed = false);
			loader_Left.grayed = flag;
			if (win.select.selectedIndex == 1)
			{
				win.BackBlue.Play(delegate
				{
					win.PK.Play();
				});
			}
			else if (win.select.selectedIndex == 2)
			{
				win.BackRed.Play(delegate
				{
					win.PK.Play();
				});
			}
			else if (win.select.selectedIndex == 3)
			{
				win.BackMid.Play(delegate
				{
					win.PK.Play();
				});
			}
			RefreshTip(2);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		}
		win.com_LeftInfo.txt_PointTotal.visible = false;
		win.com_RightInfo.txt_PointTotal.visible = false;
		win.com_CenterInfo.txt_PointTotal.visible = false;
		RefreshVoteList(win.com_LeftInfo.list_SelectLeft, LeftVoteData, showPoint);
		RefreshVoteList(win.com_RightInfo.list_SelectLeft, RightVoteData, showPoint);
		RefreshVoteList(win.com_CenterInfo.list_SelectLeft, CenterVoteData, showPoint);
		if (showPoint)
		{
			RefreshTip(3);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
			int num4 = RefreshVoteTotal(win.com_LeftInfo, LeftVoteData);
			int num5 = RefreshVoteTotal(win.com_RightInfo, RightVoteData);
			int num6 = RefreshVoteTotal(win.com_CenterInfo, CenterVoteData);
			win.com_LeftInfo.diceScale.selectedIndex = ((num4 > num5 && num4 > num6) ? 1 : 0);
			win.com_RightInfo.diceScale.selectedIndex = ((num5 > num4 && num5 > num6) ? 1 : 0);
			win.com_CenterInfo.diceScale.selectedIndex = ((num6 > num4 && num6 > num5) ? 1 : 0);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
			await VoteOver();
		}
	}

	private int RefreshVoteTotal(UIAssistVoteS7_Com_NPCInfo comInfo, List<PlayerVoteData> VoteData)
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
				if (children[j] is UIAssistVoteS7_Com_VoteItem uIAssistVoteS7_Com_VoteItem)
				{
					uIAssistVoteS7_Com_VoteItem.com_Point.visible = false;
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
			if (VoteData.Count > index && item is UIAssistVoteS7_Com_VoteItem uIAssistVoteS7_Com_VoteItem)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(VoteData[index].PlayerId);
				if (playerDataById != null)
				{
					string profilePhoto = playerDataById.player.standingPainting.ProfilePhoto;
					if (uIAssistVoteS7_Com_VoteItem.loader_Icon.url == null || !uIAssistVoteS7_Com_VoteItem.loader_Icon.url.Equals(profilePhoto))
					{
						uIAssistVoteS7_Com_VoteItem.loader_Icon.url = profilePhoto;
						if (VoteData[index].VoteStatus == AssistVoteStatus.Vote)
						{
							uIAssistVoteS7_Com_VoteItem.Cut_in.Play();
						}
					}
					if (VoteData[index].VoteStatus == AssistVoteStatus.WaitVoteResult && uIAssistVoteS7_Com_VoteItem.status.selectedIndex == 0)
					{
						uIAssistVoteS7_Com_VoteItem.OK.Play();
					}
					uIAssistVoteS7_Com_VoteItem.status.selectedIndex = (VoteData[index].Affirm ? ((VoteData[index].Point <= 0) ? 1 : 2) : 0);
					UICom_Point com_Point = (UICom_Point)uIAssistVoteS7_Com_VoteItem.com_Point;
					Controller player = uIAssistVoteS7_Com_VoteItem.Player;
					int selectedIndex = (com_Point.player.selectedIndex = playerDataById.player.Slot);
					player.selectedIndex = selectedIndex;
					uIAssistVoteS7_Com_VoteItem.com_Point.visible = showPoint;
					if (showPoint)
					{
						uIAssistVoteS7_Com_VoteItem.Roll.Play();
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
		if (!(base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window))
		{
			return;
		}
		PVEMissionVoteConfigure leftMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster;
		PVEMissionVoteConfigure rightMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster;
		PVEMissionVoteConfigure centerMonster = SimpleSingletonProvider<GameLogicManager>.inst.assistVote.CenterMonster;
		Dictionary<long, PlayerVoteData> playerVoteDict = base.AssistVote.PlayerVoteDict;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
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
			else if (centerMonster.Id == item.Value.SelectId)
			{
				num3 += ((point == 0) ? 1 : point);
			}
		}
		uIAssistVoteS7Window.PK.Stop();
		uIAssistVoteS7Window.SwapRed.Stop();
		uIAssistVoteS7Window.SwapBlue.Stop();
		uIAssistVoteS7Window.SwapMid.Stop();
		uIAssistVoteS7Window.BackRed.Stop();
		uIAssistVoteS7Window.BackBlue.Stop();
		uIAssistVoteS7Window.BackMid.Stop();
		if (num3 > num && num3 > num2)
		{
			uIAssistVoteS7Window.PickMid.Play();
			uIAssistVoteS7Window.select.selectedIndex = 3;
		}
		else if (num > num2)
		{
			uIAssistVoteS7Window.PickBlue.Play();
			uIAssistVoteS7Window.select.selectedIndex = 1;
		}
		else
		{
			uIAssistVoteS7Window.PickRed.Play();
			uIAssistVoteS7Window.select.selectedIndex = 2;
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
		await base.VoteOver();
	}

	public override void ResetUI(long playerId, int selectId)
	{
		if (base.contentPane is UIAssistVoteS7Window uIAssistVoteS7Window && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			if (selectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.LeftMonster.Id)
			{
				uIAssistVoteS7Window.select.selectedIndex = 1;
			}
			else if (selectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.RightMonster.Id)
			{
				uIAssistVoteS7Window.select.selectedIndex = 2;
			}
			else if (selectId == SimpleSingletonProvider<GameLogicManager>.inst.assistVote.CenterMonster.Id)
			{
				uIAssistVoteS7Window.select.selectedIndex = 3;
			}
		}
	}
}
