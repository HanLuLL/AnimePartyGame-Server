using System.Collections.Generic;
using FairyGUI;
using SinglePlayer;
using SinglePlayer.GamePlay;
using Tools;

namespace UI;

public class Model_SinglePlayer_SelectTag : BaseModel<UISinglePlayer_Com_SelectTag>
{
	private HashSet<int> selectedCardPacks = new HashSet<int>();

	private List<SinglePlayerCardPackConfigure> cardPackConfigs = new List<SinglePlayerCardPackConfigure>();

	private List<UISinglePlayer_Button_Tag> tagButtons = new List<UISinglePlayer_Button_Tag>();

	private List<EventCallback0> tagEvents = new List<EventCallback0>();

	private const int CardPack_General = 100;

	private const int MinCardPackCount = 4;

	public Model_SinglePlayer_SelectTag(UISinglePlayer_Com_SelectTag _com)
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
		GameData model = Game.GetModel<GameData>();
		selectedCardPacks.Clear();
		foreach (int selectedCardPack in model.SelectedCardPacks)
		{
			selectedCardPacks.Add(selectedCardPack);
		}
		cardPackConfigs.Clear();
		foreach (SinglePlayerCardPackConfigure value in StaticConfigure.SinglePlayer.CardPackDict.Values)
		{
			if (value.Id != 100)
			{
				cardPackConfigs.Add(value);
			}
		}
		tagButtons.Clear();
		int count = cardPackConfigs.Count;
		com.list_tag.numItems = count;
		for (int i = 0; i < count; i++)
		{
			if (com.list_tag.GetChildAt(i) is UISinglePlayer_Button_Tag item)
			{
				tagButtons.Add(item);
			}
		}
	}

	public override void AddEvent()
	{
		base.AddEvent();
		tagEvents.Clear();
		for (int i = 0; i < tagButtons.Count; i++)
		{
			UISinglePlayer_Button_Tag uISinglePlayer_Button_Tag = tagButtons[i];
			int id = i;
			EventCallback0 eventCallback = delegate
			{
				OnTagButtonClick(id);
			};
			uISinglePlayer_Button_Tag.onClick.Add(eventCallback);
			tagEvents.Add(eventCallback);
		}
		com.btn_canel.onClick.Add(OnCanelButtonDown);
		com.btn_ok.onClick.Add(OnOkButtonDown);
		com.btn_book.onClick.Add(OnBtnBookClick);
	}

	public override void RemoveEvent()
	{
		base.RemoveEvent();
		for (int i = 0; i < tagEvents.Count; i++)
		{
			UISinglePlayer_Button_Tag uISinglePlayer_Button_Tag = tagButtons[i];
			EventCallback0 callback = tagEvents[i];
			uISinglePlayer_Button_Tag.onClick.Remove(callback);
		}
		tagEvents.Clear();
		com.btn_canel.onClick.Remove(OnCanelButtonDown);
		com.btn_ok.onClick.Remove(OnOkButtonDown);
		com.btn_book.onClick.Remove(OnBtnBookClick);
	}

	private void OnTagButtonClick(int i)
	{
		SinglePlayerCardPackConfigure singlePlayerCardPackConfigure = cardPackConfigs[i];
		bool flag = selectedCardPacks.Contains(singlePlayerCardPackConfigure.Id);
		if (flag)
		{
			selectedCardPacks.Remove(singlePlayerCardPackConfigure.Id);
		}
		else
		{
			selectedCardPacks.Add(singlePlayerCardPackConfigure.Id);
		}
		flag = !flag;
		tagButtons[i].state.selectedIndex = (flag ? 1 : 0);
		RefreshPackNum();
	}

	private void OnCanelButtonDown()
	{
		Hide();
	}

	private void OnOkButtonDown()
	{
		if (selectedCardPacks.Count >= 4)
		{
			Game.GetModel<GameData>().SetSelectedCardPacks(selectedCardPacks);
			Hide();
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11029);
		}
	}

	private void OnBtnBookClick()
	{
	}

	private void RefreshPackNum()
	{
		int num = selectedCardPacks.Count - 1;
		int count = cardPackConfigs.Count;
		com.txt_num.text = $"{num} / {count}";
	}

	public override void Refresh()
	{
		base.Refresh();
		for (int i = 0; i < tagButtons.Count; i++)
		{
			SinglePlayerCardPackConfigure singlePlayerCardPackConfigure = cardPackConfigs[i];
			UISinglePlayer_Button_Tag uISinglePlayer_Button_Tag = tagButtons[i];
			uISinglePlayer_Button_Tag.title = singlePlayerCardPackConfigure.CardPackNameID.GetLocal(UIStringType.SinglePlayer);
			uISinglePlayer_Button_Tag.loader_tag.url = singlePlayerCardPackConfigure.Icon;
			bool flag = selectedCardPacks.Contains(singlePlayerCardPackConfigure.Id);
			uISinglePlayer_Button_Tag.state.selectedIndex = (flag ? 1 : 0);
		}
		RefreshPackNum();
	}
}
