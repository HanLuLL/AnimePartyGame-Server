using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class BattlePlayerInfoWindow : BaseWindow
{
	private struct SkillInfo
	{
		public SkillType SkillType;

		public int SkillNameId;

		public int SkillDescId;

		public int SkillCD;

		public int Round;
	}

	private BattlePlayerData ShowPlayer;

	private const string _skillDefaultIcon = "ui://1ov1i0v9nbg68u";

	private const string _skillTalentIcon = "ui://1ov1i0v9m7gv8c";

	private readonly List<UIBattlePlayerInfo_Button_Relic> relicItems = new List<UIBattlePlayerInfo_Button_Relic>(10);

	private List<int> relicIds;

	private UIBattlePlayerInfo_Button_Relic CurrentRelic;

	private int currentRelicStartIndex;

	private List<Buff> showBuffs;

	private List<PropertyData<int>> propertyBuffList;

	private const float MonsterInfoBottomPadding = 10f;

	public BattlePlayerInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattlePlayerInfoWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask<BattlePlayerInfoWindow> TryShow()
	{
		if (!base.isShowing)
		{
			ShowPopup();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.FinishPlayerInfoStepGuide();
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Quit.onClick.Add(base.Hide);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.AddListener(base.Hide);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Quit.onClick.Remove(base.Hide);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.RemoveListener(base.Hide);
		}
	}

	public async UniTask TryShowHeroInfo(BattlePlayerData playerData)
	{
		ShowPlayer = playerData;
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return;
		}
		await TryShow();
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.Cut_in.Play();
			uIBattlePlayerInfoWindow.type.selectedIndex = 0;
			CharacterHandle characterConfig = playerData.player.characterConfig;
			string local = 1000001.GetLocal(UIStringType.GUI);
			uIBattlePlayerInfoWindow.com_Hero.btn_Attr.txt_title.text = "<img src='" + characterConfig.CharacterMap + "' width='70' height='70'/>" + local;
			uIBattlePlayerInfoWindow.com_Hero.txt_CharaterName.text = CharacterHandle.GetCharacterName(characterConfig.Id, characterConfig.CharacterType);
			uIBattlePlayerInfoWindow.com_Hero.txt_CharaterNick.text = CharacterHandle.GetCharacterNickName(characterConfig.Id, characterConfig.CharacterType);
			uIBattlePlayerInfoWindow.com_Hero.txt_ATK.text = playerData.Property.ATK.Value.ToString();
			uIBattlePlayerInfoWindow.com_Hero.txt_DEF.text = playerData.Property.DEF.Value.ToString();
			uIBattlePlayerInfoWindow.com_Hero.txt_Card.text = playerData.cardContainer.CardCount.ToString();
			uIBattlePlayerInfoWindow.com_Hero.txt_BuffDesc.text = "";
			RefreshHeroBuffInfo(playerData, uIBattlePlayerInfoWindow.com_Hero.list_Buff);
			if (curRoomInfo.IsPVE())
			{
				uIBattlePlayerInfoWindow.com_Hero.type.selectedIndex = 0;
				RefreshHeroInfo_PVE(playerData);
			}
			else
			{
				uIBattlePlayerInfoWindow.com_Hero.type.selectedIndex = 1;
				RefreshHeroInfo_PVP(playerData);
			}
		}
	}

	private bool IsVailGetInfo()
	{
		bool num = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 2;
		bool isReplay = SimpleSingletonProvider<GameLogicManager>.inst.replay.Session.IsReplay;
		if (!num)
		{
			return !isReplay;
		}
		return false;
	}

	private void RefreshHeroInfo_PVP(BattlePlayerData playerData)
	{
		if (!(base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow))
		{
			return;
		}
		string local = 1000002.GetLocal(UIStringType.GUI);
		uIBattlePlayerInfoWindow.com_Hero.btn_Skill.txt_title.text = "<img src='ui://1ov1i0v9nbg68u' width='70' height='70'/>" + local;
		List<SkillInfo> skillInfo = GetSkillInfo(playerData);
		RefreshSkillInfo(skillInfo, uIBattlePlayerInfoWindow.com_Hero.list_Skill);
		if (ShowPlayer != null)
		{
			RefreshBattleData();
			if (IsVailGetInfo())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestGetHeroInfoC2S(ShowPlayer.player.Id).OnFinishedOnly.AddOnce(RefreshBattleData);
			}
		}
	}

	private void RefreshBattleData()
	{
		if (ShowPlayer != null && base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			RefreshBattleDataItem(3001, ShowPlayer.KillCount, uIBattlePlayerInfoWindow.com_Hero.com_killCount);
			RefreshBattleDataItem(3002, ShowPlayer.TotalDie, uIBattlePlayerInfoWindow.com_Hero.com_TotalDie);
			RefreshBattleDataItem(3003, ShowPlayer.TotalDamage, uIBattlePlayerInfoWindow.com_Hero.com_TotalDamage);
			RefreshBattleDataItem(3004, ShowPlayer.TotalInjured, uIBattlePlayerInfoWindow.com_Hero.com_TotalInjured);
			RefreshBattleDataItem(3005, ShowPlayer.TreatmentScore, uIBattlePlayerInfoWindow.com_Hero.com_TreatmentScore);
		}
	}

	private void RefreshBattleDataItem(int achieveId, int value, UIBattlePlayerInfo_Com_BattleData com_Item)
	{
		com_Item.txt_Name.text = achieveId.GetLocal(UIStringType.Achieve);
		com_Item.txt_Count.text = value.ToString();
	}

	private void RefreshHeroInfo_PVE(BattlePlayerData playerData)
	{
		GComponent gComponent = base.contentPane;
		UIBattlePlayerInfoWindow win = gComponent as UIBattlePlayerInfoWindow;
		if (win == null)
		{
			return;
		}
		win.com_Hero.txt_PVELv.text = playerData.player.PVEHeroLV.ToString();
		List<SkillInfo> skillInfo = GetSkillInfo(playerData);
		if (playerData.player.GetTalentId() > 0)
		{
			win.com_Hero.loader_Talent.visible = true;
			foreach (int item in playerData.player.characterConfig.PveBreak)
			{
				if (StaticConfigure.PVENurturance.BreakDict.TryGetValue(item, out var value))
				{
					skillInfo.Add(new SkillInfo
					{
						SkillType = SkillType.None,
						SkillNameId = 0,
						SkillDescId = value.DescriptionID
					});
				}
			}
			string local = 1000002.GetLocal(UIStringType.GUI);
			win.com_Hero.btn_Skill.txt_title.text = "<img src='ui://1ov1i0v9m7gv8c' width='65' height='65'/>" + local;
		}
		else
		{
			win.com_Hero.loader_Talent.visible = false;
			string local2 = 1000002.GetLocal(UIStringType.GUI);
			win.com_Hero.btn_Skill.txt_title.text = "<img src='ui://1ov1i0v9nbg68u' width='65' height='65'/>" + local2;
		}
		RefreshSkillInfo(skillInfo, win.com_Hero.list_Skill);
		if (IsVailGetInfo())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestGetHeroInfoC2S(ShowPlayer.player.Id).OnFinishedOnly.AddOnce(delegate
			{
				RefreshActiveSkillCD(win.com_Hero.list_Skill, ShowPlayer.SkillCD);
			});
		}
		else
		{
			ReadOnlyReactiveProperty<int> readOnlyReactiveProperty = ShowPlayer?.player?.Property?.activeSkillCD;
			if (readOnlyReactiveProperty != null)
			{
				RefreshActiveSkillCD(win.com_Hero.list_Skill, readOnlyReactiveProperty.Value);
			}
		}
		RefreshRelicInfo(playerData);
	}

	private void RefreshSkillInfo(List<SkillInfo> skillInfos, GList list_Skill)
	{
		list_Skill.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIBattlePlayerInfo_Com_Skill uIBattlePlayerInfo_Com_Skill && index >= 0 && index <= skillInfos.Count - 1)
			{
				uIBattlePlayerInfo_Com_Skill.data = skillInfos[index];
				if (skillInfos[index].SkillType == SkillType.Active)
				{
					RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
					if (curRoomInfo != null && curRoomInfo.IsPVE())
					{
						uIBattlePlayerInfo_Com_Skill.showCD.selectedIndex = 0;
					}
					else
					{
						uIBattlePlayerInfo_Com_Skill.showCD.selectedIndex = 1;
						uIBattlePlayerInfo_Com_Skill.txt_CD.text = skillInfos[index].Round.ToString();
					}
					string local = 1000003.GetLocal(UIStringType.GUI);
					string local2 = skillInfos[index].SkillNameId.GetLocal(UIStringType.Skill);
					uIBattlePlayerInfo_Com_Skill.txt_Title.text = "[color=#ECA03E]" + local + local2 + "[/color]";
					skillInfos[index].SkillDescId.RefreshCharacterSkillHyperlinkDesc(uIBattlePlayerInfo_Com_Skill.txt_Desc);
				}
				else if (skillInfos[index].SkillType == SkillType.Passive)
				{
					uIBattlePlayerInfo_Com_Skill.showCD.selectedIndex = 0;
					string local3 = skillInfos[index].SkillNameId.GetLocal(UIStringType.Skill);
					string local4 = 1000004.GetLocal(UIStringType.GUI);
					uIBattlePlayerInfo_Com_Skill.txt_Title.text = "[color=#79E7E7]" + local4 + local3 + "[/color]";
					skillInfos[index].SkillDescId.RefreshCharacterSkillHyperlinkDesc(uIBattlePlayerInfo_Com_Skill.txt_Desc);
				}
				else if (skillInfos[index].SkillType == SkillType.None)
				{
					uIBattlePlayerInfo_Com_Skill.showCD.selectedIndex = 0;
					string local5 = 1000005.GetLocal(UIStringType.GUI);
					uIBattlePlayerInfo_Com_Skill.txt_Title.text = "[color=#B863FE]" + local5 + "[/color]";
					RefreshHyperlinkDesc(skillInfos[index].SkillDescId.GetLocal(UIStringType.PVENurturance), uIBattlePlayerInfo_Com_Skill.txt_Desc);
				}
			}
		};
		list_Skill.numItems = skillInfos.Count;
	}

	private void RefreshActiveSkillCD(GList list, int cd)
	{
		if (!(list.GetChildAt(0) is UIBattlePlayerInfo_Com_Skill { data: var obj } uIBattlePlayerInfo_Com_Skill) || !(obj is SkillInfo skillInfo))
		{
			return;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && curRoomInfo.IsPVE() && skillInfo.SkillType == SkillType.Active)
		{
			string arg = "[color=#FFFFFF]";
			if (cd != skillInfo.Round)
			{
				arg = "[color=#7FFD37]";
			}
			uIBattlePlayerInfo_Com_Skill.txt_CD.text = $"{arg}{cd}[/color]/{skillInfo.Round}";
			uIBattlePlayerInfo_Com_Skill.showCD.selectedIndex = 1;
		}
	}

	private static List<SkillInfo> GetSkillInfo(BattlePlayerData playerData)
	{
		List<SkillInfo> list = new List<SkillInfo>();
		int battleActiveSkillId = playerData.player.GetBattleActiveSkillId();
		RepeatedField<int> battlePassiveSkillId = playerData.player.GetBattlePassiveSkillId();
		List<SkillInfoConfigure> skillConfigs = UIHelper.GetSkillConfigs(battleActiveSkillId, battlePassiveSkillId);
		for (int i = 0; i < skillConfigs.Count; i++)
		{
			SkillInfo item = new SkillInfo
			{
				SkillType = skillConfigs[i].SkillType,
				SkillNameId = skillConfigs[i].NameID,
				SkillDescId = skillConfigs[i].DescID,
				Round = skillConfigs[i].Round,
				SkillCD = playerData.Property.activeSkillCD.Value
			};
			list.Add(item);
		}
		return list;
	}

	private void RefreshRelicInfo(BattlePlayerData playerData)
	{
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			ReadyRelicItems();
			uIBattlePlayerInfoWindow.com_Hero.btn_Left.onClick.Set(LeftRelic);
			uIBattlePlayerInfoWindow.com_Hero.btn_Right.onClick.Set(RightRelic);
			uIBattlePlayerInfoWindow.com_Hero.btn_Chat.onClick.Set(ChatRelic);
			relicIds = playerData.GetRelicIds();
			uIBattlePlayerInfoWindow.com_Hero.btn_Chat.visible = false;
			RendererRelicItems(0);
		}
	}

	private void ReadyRelicItems()
	{
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			relicItems.Clear();
			int childIndex = uIBattlePlayerInfoWindow.com_Hero.GetChildIndex(uIBattlePlayerInfoWindow.com_Hero.btn_Relic_0);
			for (int i = 0; i < 10; i++)
			{
				UIBattlePlayerInfo_Button_Relic uIBattlePlayerInfo_Button_Relic = (UIBattlePlayerInfo_Button_Relic)uIBattlePlayerInfoWindow.com_Hero.GetChildAt(childIndex + i);
				uIBattlePlayerInfo_Button_Relic.selected = false;
				relicItems.Add(uIBattlePlayerInfo_Button_Relic);
			}
		}
	}

	private void RendererRelicItems(int StartIndex)
	{
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Right.vailStatus.selectedIndex = ((relicIds.Count <= StartIndex + 10) ? 1 : 0);
			uIBattlePlayerInfoWindow.com_Hero.btn_Left.vailStatus.selectedIndex = ((StartIndex - 10 < 0) ? 1 : 0);
			currentRelicStartIndex = StartIndex;
			uIBattlePlayerInfoWindow.com_Hero.txt_Name.text = "";
			uIBattlePlayerInfoWindow.com_Hero.txt_Desc.text = "";
			for (int i = 0; i < relicItems.Count; i++)
			{
				RendererRelicItem(relicItems[i], i);
			}
			if (relicItems[0].touchable)
			{
				relicItems[0].onClick.Call();
			}
		}
	}

	private void RendererRelicItem(UIBattlePlayerInfo_Button_Relic relicItem, int index)
	{
		GComponent gComponent = base.contentPane;
		UIBattlePlayerInfoWindow win = gComponent as UIBattlePlayerInfoWindow;
		if (win == null)
		{
			return;
		}
		if (relicIds.Count > currentRelicStartIndex + index)
		{
			RelicInfoConfigure relicInfo = relicIds[currentRelicStartIndex + index].GetRelicInfoConfigure();
			((UICom_Relic_Quality)relicItem.com_Quality).quality.selectedIndex = (int)relicInfo.RelicQualityType;
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
				win.com_Hero.txt_Name.text = relicInfo.NameID.GetLocal(UIStringType.Relic);
				win.com_Hero.txt_Desc.text = relicInfo.DescID.GetLocal(UIStringType.Relic);
				win.com_Hero.btn_Chat.visible = ShowPlayer != null && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(ShowPlayer.player.Id);
			});
		}
		else
		{
			((UICom_Relic_Quality)relicItem.com_Quality).quality.selectedIndex = 0;
			relicItem.touchable = false;
			relicItem.loader_Relic.visible = false;
		}
		relicItem.selected = false;
	}

	private void LeftRelic()
	{
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow && relicIds != null && relicIds.Count != 0 && uIBattlePlayerInfoWindow.com_Hero.btn_Left.vailStatus.selectedIndex != 1)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Left.onClick.Retain();
			RendererRelicItems(currentRelicStartIndex - 10);
			uIBattlePlayerInfoWindow.com_Hero.btn_Left.onClick.Release();
		}
	}

	private void RightRelic()
	{
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow && relicIds != null && relicIds.Count != 0 && uIBattlePlayerInfoWindow.com_Hero.btn_Right.vailStatus.selectedIndex != 1 && relicIds.Count - currentRelicStartIndex > 10)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Right.onClick.Retain();
			RendererRelicItems(currentRelicStartIndex + 10);
			uIBattlePlayerInfoWindow.com_Hero.btn_Right.onClick.Release();
		}
	}

	private void ChatRelic()
	{
		if (CurrentRelic?.data is int relicId && base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.com_Hero.btn_Chat.onClick.Retain();
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattleRelicMessage msgData = new BattleRelicMessage(selfPlayerData, relicId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, BattleRelicMessage.MarkId);
				uIBattlePlayerInfoWindow.com_Hero.btn_Chat.onClick.Release();
			}
		}
	}

	private void RefreshHeroBuffInfo(BattlePlayerData playerData, GList list_Buff)
	{
		GComponent gComponent = base.contentPane;
		UIBattlePlayerInfoWindow win = gComponent as UIBattlePlayerInfoWindow;
		if (win == null)
		{
			return;
		}
		(List<Buff>, List<PropertyData<int>>) tuple = playerData.buffContainer.GetShowBuffs(playerData);
		showBuffs = tuple.Item1;
		propertyBuffList = tuple.Item2;
		list_Buff.itemRenderer = RendererBuff;
		list_Buff.numItems = showBuffs.Count + propertyBuffList.Count;
		int itemCount = Mathf.Clamp(list_Buff.numItems, 1, 16);
		list_Buff.ResizeToFit(itemCount);
		list_Buff.onClickItem.Set(delegate(EventContext context)
		{
			if (context.data is UIBattlePlayerInfo_Button_BuffItem { btn_Buff: UIButton_Buff { BuffConfig: not null } btn_Buff } uIBattlePlayerInfo_Button_BuffItem)
			{
				uIBattlePlayerInfo_Button_BuffItem.onClick.Retain();
				string local = btn_Buff.BuffConfig.DescId.GetLocal(UIStringType.Buff);
				int nameId = btn_Buff.BuffConfig.NameId;
				string text = "";
				if (nameId != 0)
				{
					text = nameId.GetLocal(UIStringType.Buff) + "\n";
				}
				RefreshHyperlinkDesc(text + local, win.com_Hero.txt_BuffDesc);
				uIBattlePlayerInfo_Button_BuffItem.onClick.Release();
			}
		});
		float hv = Mathf.Abs(list_Buff.y + list_Buff.height - win.com_Hero.middle_line.y);
		win.com_Hero.txt_BuffDesc.SetSize(win.com_Hero.txt_BuffDesc.width, hv);
		if (list_Buff._children.Count <= 0)
		{
			return;
		}
		for (int num = 0; num < list_Buff._children.Count; num++)
		{
			if (list_Buff._children[num].visible)
			{
				list_Buff._children[num].onClick.Call();
				break;
			}
		}
	}

	private void RendererBuff(int index, GObject item)
	{
		if (!(item is UIBattlePlayerInfo_Button_BuffItem { btn_Buff: UIButton_Buff btn_Buff } uIBattlePlayerInfo_Button_BuffItem))
		{
			return;
		}
		if (propertyBuffList.Count > index)
		{
			btn_Buff.RefreshProperty(propertyBuffList[index], dynamic: false);
		}
		else
		{
			int num = index - propertyBuffList.Count;
			if (showBuffs.Count > num)
			{
				btn_Buff.RefreshBuff(showBuffs[num]);
			}
		}
		uIBattlePlayerInfo_Button_BuffItem.visible = btn_Buff.visible;
		uIBattlePlayerInfo_Button_BuffItem.selected = false;
	}

	private void RefreshHyperlinkDesc(string buffDesc, GRichTextField comBuffTxtDesc)
	{
		int difficulty = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.Difficulty ?? 0;
		CommonUIManager.RefreshHyperlinkDesc(buffDesc, comBuffTxtDesc, difficulty);
	}

	public async UniTask RefreshMonsterInfo(List<Buff> buffs, List<PropertyData<int>> propertyBuffs, GObject anchorTarget, bool? showOnLeft = null)
	{
		await TryShow();
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			Rect boundsInRoot = GetBoundsInRoot(anchorTarget);
			bool showOnLeft2 = showOnLeft ?? (boundsInRoot.center.x > GRoot.inst.width * 0.5f);
			uIBattlePlayerInfoWindow.type.selectedIndex = 1;
			RefreshMonsterBuffList((UICom_BuffInfo)uIBattlePlayerInfoWindow.com_Monster, buffs, propertyBuffs);
			SetMonsterInfoPosition(uIBattlePlayerInfoWindow.com_Monster, boundsInRoot, showOnLeft2);
		}
	}

	private void RefreshMonsterBuffList(UICom_BuffInfo monsterInfoCom, List<Buff> buffs, List<PropertyData<int>> propertyBuffs)
	{
		int buffCount = buffs.Count + propertyBuffs.Count;
		monsterInfoCom.list_Buff.itemRenderer = delegate(int index, GObject item)
		{
			if (buffCount > index && item is UICom_BuffInfoItem uICom_BuffInfoItem)
			{
				BuffInfoConfigure buffInfoConfigure = ((propertyBuffs.Count > index) ? propertyBuffs[index].buffId.GetBuffConfigure() : buffs[index - propertyBuffs.Count].BuffId.GetBuffConfigure());
				if (buffInfoConfigure != null)
				{
					uICom_BuffInfoItem.loader_Buff.url = buffInfoConfigure.Icon;
					if (buffInfoConfigure.NameId != 0)
					{
						uICom_BuffInfoItem.txt_Title.text = buffInfoConfigure.NameId.GetLocal(UIStringType.Buff);
					}
					else
					{
						Debug.LogError($"展示怪物Buff:{buffInfoConfigure.Id}, NameId = 0");
					}
					if (buffInfoConfigure.DescId != 0)
					{
						string local = buffInfoConfigure.DescId.GetLocal(UIStringType.Buff);
						RefreshHyperlinkDesc(local, uICom_BuffInfoItem.txt_Desc);
					}
					else
					{
						Debug.LogError($"展示怪物Buff:{buffInfoConfigure.Id}, DescId = 0");
					}
				}
			}
		};
		monsterInfoCom.list_Buff.numItems = buffCount;
		monsterInfoCom.list_Buff.ResizeToFit(Mathf.Clamp(buffCount, 0, 4));
	}

	private static Rect GetBoundsInRoot(GObject anchorTarget)
	{
		Vector2 vector = (anchorTarget.pivotAsAnchor ? (-Vector2.Scale(anchorTarget.pivot, anchorTarget.size)) : Vector2.zero);
		GRoot inst = GRoot.inst;
		Vector2 vector2 = anchorTarget.LocalToRoot(vector, inst);
		Vector2 vector3 = anchorTarget.LocalToRoot(vector + anchorTarget.size, inst);
		return Rect.MinMaxRect(Mathf.Min(vector2.x, vector3.x), Mathf.Min(vector2.y, vector3.y), Mathf.Max(vector2.x, vector3.x), Mathf.Max(vector2.y, vector3.y));
	}

	private static void SetMonsterInfoPosition(GObject monsterInfoCom, Rect anchorBounds, bool showOnLeft)
	{
		GRoot inst = GRoot.inst;
		float value = (showOnLeft ? (anchorBounds.xMin - monsterInfoCom.width) : anchorBounds.xMax);
		float max = Mathf.Max(0f, inst.width - monsterInfoCom.width);
		float max2 = Mathf.Max(0f, inst.height - monsterInfoCom.height - 10f);
		monsterInfoCom.SetXY(Mathf.Clamp(value, 0f, max), Mathf.Clamp(anchorBounds.yMin, 0f, max2));
	}

	public (Vector2, float, float) ShowOpenSkillMask()
	{
		if (!(base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow))
		{
			return (Vector2.zero, 0f, 0f);
		}
		base.sortingOrder = SimpleSingletonProvider<UIManager>.inst.tutorial.sortingOrder - 1;
		Vector2 pt = uIBattlePlayerInfoWindow.com_Hero.btn_Skill.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, uIBattlePlayerInfoWindow.com_Hero.btn_Skill.width, uIBattlePlayerInfoWindow.com_Hero.btn_Skill.height, isRect: true);
		return (vector, uIBattlePlayerInfoWindow.com_Hero.btn_Skill.width, uIBattlePlayerInfoWindow.com_Hero.btn_Skill.height);
	}

	public async UniTask ShowBattleInfoByTutorial()
	{
		Show();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		await TryShowHeroInfo(selfPlayerData);
		GComponent gComponent = base.contentPane;
		UIBattlePlayerInfoWindow win = gComponent as UIBattlePlayerInfoWindow;
		if (win != null)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.Cut_in.playing, SimpleSingletonProvider<UIManager>.inst.tutorial.Hide);
			win.com_Hero.tab.selectedIndex = 0;
		}
	}

	public async UniTask ShowSkillInfoByTutorial()
	{
		ShowPopup();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		await TryShowHeroInfo(selfPlayerData);
		if (base.contentPane is UIBattlePlayerInfoWindow uIBattlePlayerInfoWindow)
		{
			uIBattlePlayerInfoWindow.com_Hero.tab.selectedIndex = 1;
		}
	}
}
