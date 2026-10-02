using System.Collections.Generic;
using FairyGUI;
using SinglePlayer.Tools;
using Tools;

namespace UI;

public class Model_SinglePlayer_Book : BaseModel<UISinglePlayer_Com_Book>
{
	private int itemType;

	private int tagId;

	private int gradeId;

	private Dictionary<int, List<SinglePlayerCardConfigure>> buildingCardConfigsDic = new Dictionary<int, List<SinglePlayerCardConfigure>>();

	private Dictionary<int, List<SinglePlayerRelicConfigure>> relicConfigsDic = new Dictionary<int, List<SinglePlayerRelicConfigure>>();

	private List<SinglePlayerCardPackConfigure> cardPackConfigs = new List<SinglePlayerCardPackConfigure>();

	private List<EventCallback0> tagBtnEvents = new List<EventCallback0>();

	private List<UISinglePlayer_Button_BookTag> tagBtnList = new List<UISinglePlayer_Button_BookTag>();

	private List<EventCallback0> gradeBtnEvents = new List<EventCallback0>();

	private List<UISinglePlayer_Button_ItemGrade> gradeBtnList = new List<UISinglePlayer_Button_ItemGrade>();

	private const int ItemType_Building = 0;

	private const int ItemType_Relic = 1;

	private int filter => itemType | (tagId << 4);

	public Model_SinglePlayer_Book(UISinglePlayer_Com_Book _com)
		: base(_com)
	{
	}

	protected override void OnShow()
	{
		base.OnShow();
		com.Cut_in.Play();
	}

	public override void InitDataOrView()
	{
		base.InitDataOrView();
		cardPackConfigs.Clear();
		foreach (SinglePlayerCardPackConfigure value in StaticConfigure.SinglePlayer.CardPackDict.Values)
		{
			cardPackConfigs.Add(value);
		}
		tagBtnList.Clear();
		com.list_tag.numItems = cardPackConfigs.Count;
		for (int i = 0; i < cardPackConfigs.Count; i++)
		{
			if (com.list_tag.GetChildAt(i) is UISinglePlayer_Button_BookTag uISinglePlayer_Button_BookTag)
			{
				tagBtnList.Add(uISinglePlayer_Button_BookTag);
				SinglePlayerCardPackConfigure singlePlayerCardPackConfigure = cardPackConfigs[i];
				uISinglePlayer_Button_BookTag.loader_icon.url = singlePlayerCardPackConfigure.Icon;
				uISinglePlayer_Button_BookTag.title = singlePlayerCardPackConfigure.CardPackNameID.GetLocal(UIStringType.SinglePlayer);
			}
		}
		gradeBtnList.Clear();
		gradeBtnList.Add(com.btn_grade.btn_1);
		gradeBtnList.Add(com.btn_grade.btn_2);
		gradeBtnList.Add(com.btn_grade.btn_3);
		gradeBtnList.Add(com.btn_grade.btn_4);
		com.btn_grade.state.selectedIndex = 0;
	}

	public override void AddEvent()
	{
		base.AddEvent();
		com.btn_back.onClick.Add(OnBackBtnClick);
		com.btn_build.onClick.Add(OnBuildingBtnClick);
		com.btn_relic.onClick.Add(OnRelicBtnClick);
		com.btn_grade.onClick.Add(OnGradeBtnClick);
		tagBtnEvents.Clear();
		for (int i = 0; i < tagBtnList.Count; i++)
		{
			UISinglePlayer_Button_BookTag uISinglePlayer_Button_BookTag = tagBtnList[i];
			int id = i;
			EventCallback0 eventCallback = delegate
			{
				OnTagBtnClick(id);
			};
			uISinglePlayer_Button_BookTag.onClick.Add(eventCallback);
			tagBtnEvents.Add(eventCallback);
		}
		gradeBtnEvents.Clear();
		for (int num = 0; num < gradeBtnList.Count; num++)
		{
			UISinglePlayer_Button_ItemGrade uISinglePlayer_Button_ItemGrade = gradeBtnList[num];
			int id2 = num;
			EventCallback0 eventCallback2 = delegate
			{
				OnSubGradeBtnClick(id2);
			};
			uISinglePlayer_Button_ItemGrade.onClick.Add(eventCallback2);
			gradeBtnEvents.Add(eventCallback2);
		}
	}

	public override void RemoveEvent()
	{
		base.RemoveEvent();
		com.btn_back.onClick.Remove(OnBackBtnClick);
		com.btn_build.onClick.Remove(OnBuildingBtnClick);
		com.btn_relic.onClick.Remove(OnRelicBtnClick);
		com.btn_grade.onClick.Remove(OnGradeBtnClick);
		for (int i = 0; i < tagBtnEvents.Count; i++)
		{
			UISinglePlayer_Button_BookTag uISinglePlayer_Button_BookTag = tagBtnList[i];
			EventCallback0 callback = tagBtnEvents[i];
			uISinglePlayer_Button_BookTag.onClick.Remove(callback);
		}
		tagBtnEvents.Clear();
		for (int j = 0; j < gradeBtnEvents.Count; j++)
		{
			UISinglePlayer_Button_ItemGrade uISinglePlayer_Button_ItemGrade = gradeBtnList[j];
			EventCallback0 callback2 = gradeBtnEvents[j];
			uISinglePlayer_Button_ItemGrade.onClick.Remove(callback2);
		}
		gradeBtnEvents.Clear();
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshTypeBtnState();
		RefreshTagBtnState();
		RefreshItem();
		ReferehGradeBtn();
	}

	private List<SinglePlayerCardConfigure> GetBuildingCardConfigs()
	{
		if (!buildingCardConfigsDic.TryGetValue(filter, out var value))
		{
			value = new List<SinglePlayerCardConfigure>();
			int item = cardPackConfigs.GetSafeByIndex(tagId)?.Id ?? (-1);
			foreach (SinglePlayerCardConfigure value2 in StaticConfigure.SinglePlayer.CardDict.Values)
			{
				if (value2.CardPackID.Contains(item))
				{
					value.Add(value2);
				}
			}
			buildingCardConfigsDic.Add(filter, value);
		}
		return value;
	}

	private List<SinglePlayerRelicConfigure> GetRelicConfigs()
	{
		if (!relicConfigsDic.TryGetValue(filter, out var value))
		{
			value = new List<SinglePlayerRelicConfigure>();
			int item = cardPackConfigs.GetSafeByIndex(tagId)?.Id ?? (-1);
			foreach (SinglePlayerRelicConfigure value2 in StaticConfigure.SinglePlayer.RelicDict.Values)
			{
				if (value2.CardPackID.Contains(item))
				{
					value.Add(value2);
				}
			}
			relicConfigsDic.Add(filter, value);
		}
		return value;
	}

	private void ReferehGradeBtn(bool isUpdateSubBtn = true)
	{
		com.btn_grade.visible = itemType == 0;
		if (!isUpdateSubBtn)
		{
			return;
		}
		for (int i = 0; i < gradeBtnList.Count; i++)
		{
			UISinglePlayer_Button_ItemGrade uISinglePlayer_Button_ItemGrade = gradeBtnList[i];
			bool flag = gradeId == i;
			uISinglePlayer_Button_ItemGrade.state.selectedIndex = (flag ? 1 : 0);
			if (flag)
			{
				com.btn_grade.title = uISinglePlayer_Button_ItemGrade.title;
			}
		}
	}

	private void RefreshItem()
	{
		int count = GetBuildingCardConfigs().Count;
		int count2 = GetRelicConfigs().Count;
		com.btn_build.visible = count > 0;
		com.btn_relic.visible = count2 > 0;
		if (itemType == 0 && count == 0)
		{
			itemType = 1;
			RefreshTypeBtnState();
		}
		if (itemType == 1 && count2 == 0)
		{
			itemType = 0;
			RefreshTypeBtnState();
		}
		ReferehGradeBtn(isUpdateSubBtn: false);
		if (itemType == 0)
		{
			RefreshItem_Building();
		}
		else if (itemType == 1)
		{
			RefreshItem_Relic();
		}
	}

	private void RefreshItem_Building()
	{
		List<SinglePlayerCardConfigure> buildingCardConfigs = GetBuildingCardConfigs();
		int count = buildingCardConfigs.Count;
		com.list_item.numItems = count;
		for (int i = 0; i < count; i++)
		{
			UISinglePlayer_Button_Item uISinglePlayer_Button_Item = com.list_item.GetChildAt(i) as UISinglePlayer_Button_Item;
			SinglePlayerCardConfigure singlePlayerCardConfigure = buildingCardConfigs[i];
			if (uISinglePlayer_Button_Item != null)
			{
				uISinglePlayer_Button_Item.title = singlePlayerCardConfigure.NameID.GetLocal(UIStringType.SinglePlayer);
				uISinglePlayer_Button_Item.loader_icon.url = singlePlayerCardConfigure.Icon;
				uISinglePlayer_Button_Item.txt_desc.text = ParseSinglePlayerText.ParseAstralCardDesc(singlePlayerCardConfigure.Id, gradeId + 1);
				uISinglePlayer_Button_Item.rare.selectedIndex = singlePlayerCardConfigure.Rarity;
			}
		}
	}

	private void RefreshItem_Relic()
	{
		List<SinglePlayerRelicConfigure> relicConfigs = GetRelicConfigs();
		int count = relicConfigs.Count;
		com.list_item.numItems = count;
		for (int i = 0; i < count; i++)
		{
			UISinglePlayer_Button_Item uISinglePlayer_Button_Item = com.list_item.GetChildAt(i) as UISinglePlayer_Button_Item;
			SinglePlayerRelicConfigure singlePlayerRelicConfigure = relicConfigs[i];
			if (uISinglePlayer_Button_Item != null)
			{
				uISinglePlayer_Button_Item.title = singlePlayerRelicConfigure.NameID.GetLocal(UIStringType.SinglePlayer);
				uISinglePlayer_Button_Item.loader_icon.url = singlePlayerRelicConfigure.Icon;
				uISinglePlayer_Button_Item.txt_desc.text = singlePlayerRelicConfigure.DesiID.GetLocal(UIStringType.SinglePlayer);
				uISinglePlayer_Button_Item.rare.selectedIndex = singlePlayerRelicConfigure.Rarity;
			}
		}
	}

	private void OnBackBtnClick()
	{
		Hide();
	}

	private void RefreshTypeBtnState()
	{
		com.btn_build.state.selectedIndex = ((itemType == 0) ? 1 : 0);
		com.btn_relic.state.selectedIndex = ((itemType == 1) ? 1 : 0);
	}

	private void RefreshTagBtnState()
	{
		for (int i = 0; i < tagBtnList.Count; i++)
		{
			tagBtnList[i].state.selectedIndex = ((tagId == i) ? 1 : 0);
		}
	}

	private void OnBuildingBtnClick()
	{
		if (itemType != 0)
		{
			itemType = 0;
			RefreshTypeBtnState();
			RefreshItem();
			ReferehGradeBtn(isUpdateSubBtn: false);
		}
	}

	private void OnRelicBtnClick()
	{
		if (itemType != 1)
		{
			itemType = 1;
			RefreshTypeBtnState();
			RefreshItem();
			ReferehGradeBtn(isUpdateSubBtn: false);
		}
	}

	private void OnTagBtnClick(int id)
	{
		if (tagId != id)
		{
			tagId = id;
			RefreshTagBtnState();
			RefreshItem();
		}
	}

	private void OnGradeBtnClick()
	{
		com.btn_grade.state.selectedIndex = 1 - com.btn_grade.state.selectedIndex;
	}

	private void OnSubGradeBtnClick(int id)
	{
		if (gradeId != id)
		{
			gradeId = id;
			ReferehGradeBtn();
			RefreshItem();
		}
	}
}
