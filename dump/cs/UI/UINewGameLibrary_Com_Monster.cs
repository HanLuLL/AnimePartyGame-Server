using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UINewGameLibrary_Com_Monster : GComponent
{
	private readonly List<int> _monsterConfigIds = new List<int>();

	private int _CurrentMonsterIndex;

	private int _currentDifficulty;

	private int _curType;

	public GComponent loader_Skin;

	public GComponent Com_LibraryFile;

	public UINewGameLibrary_Com_Difficulty com_Difficulty;

	public GList list_Items;

	public const string URL = "ui://mc0y3plupj0z6";

	public void RefreshMonster(List<int> monsterConfigIds, int curType)
	{
		_curType = curType;
		_monsterConfigIds.Clear();
		_monsterConfigIds.AddRange(monsterConfigIds);
		_CurrentMonsterIndex = 0;
		list_Items.SetVirtual();
		list_Items.itemRenderer = RenderMonsterItems;
		list_Items.numItems = _monsterConfigIds.Count;
		RenderMonster();
	}

	private void RenderMonsterItems(int index, GObject item)
	{
		UINewGameLibrary_Btn_MonsterItem monster_Item = item as UINewGameLibrary_Btn_MonsterItem;
		if (monster_Item != null)
		{
			int num = _monsterConfigIds[index];
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(num, 0, 0);
			monster_Item.loader_Profile.url = configStandingPainting.ProfilePhoto;
			monster_Item.txt_Name.text = CharacterHandle.GetCharacterName(num, CharacterType.Monster);
			monster_Item.selected = index == _CurrentMonsterIndex;
			monster_Item.onClick.Set((EventCallback0)delegate
			{
				monster_Item.onClick.Retain();
				list_Items.touchable = false;
				_CurrentMonsterIndex = index;
				RenderMonster();
				list_Items.RefreshVirtualList();
				monster_Item.onClick.Release();
			});
		}
	}

	private void RenderMonster()
	{
		list_Items.touchable = true;
		if (_CurrentMonsterIndex < 0 || _CurrentMonsterIndex > _monsterConfigIds.Count - 1)
		{
			Com_LibraryFile.visible = false;
			loader_Skin.visible = false;
			com_Difficulty.visible = false;
			return;
		}
		Com_LibraryFile.visible = true;
		loader_Skin.visible = true;
		int num = _monsterConfigIds[_CurrentMonsterIndex];
		list_Items.ScrollToView(_CurrentMonsterIndex);
		(string, bool) character = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(num, 0, 0).GetCharacter();
		Vector2 offset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(character.Item1, out var value))
		{
			offset = value.skinOffset;
		}
		CommonUIManager.RendererSkin(UIType.Window, 348, (UICom_HeroSkin)loader_Skin, character.Item1, character.Item2, offset, Vector2.one);
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		com_Difficulty.visible = false;
		int difficulty = (roomController.IsBattle() ? roomController.localRoom.Difficulty : RefreshDifficultySelect(num));
		((UICom_LibraryCharacterFile)Com_LibraryFile).RenderMonster(num, difficulty);
	}

	private int RefreshDifficultySelect(int monsterId)
	{
		MonsterAttributeConfigure monsterAttributeConfigure = CharacterHandle.GetMonsterCharacterConfigure(monsterId)?.MonsterAttributeConfigure;
		if (monsterAttributeConfigure == null || _curType == 2)
		{
			com_Difficulty.visible = false;
			return 3;
		}
		RepeatedField<MonsterAttributeConfigureItem> attrItems = monsterAttributeConfigure.MonsterAttributeConfigureItems;
		com_Difficulty.visible = true;
		com_Difficulty.showSelect.selectedIndex = 0;
		com_Difficulty.list_Difficulty.itemRenderer = delegate(int index, GObject item)
		{
			if (item is GButton gButton)
			{
				ChoosingTimeLimitdifficultyConfigure difficultyInfo = attrItems[index].Index.GetChoosingTimeLimitDifficultyConfigure();
				gButton.title = difficultyInfo.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
				gButton.onClick.Set((EventCallback0)delegate
				{
					_currentDifficulty = (int)difficultyInfo.GameDifficultyType;
					com_Difficulty.showSelect.selectedIndex = 0;
					RefreshCurrentDifficulty();
					((UICom_LibraryCharacterFile)Com_LibraryFile).RenderMonster(monsterId, _currentDifficulty);
				});
			}
		};
		RepeatedField<MonsterAttributeConfigureItem> repeatedField = attrItems;
		if (repeatedField[repeatedField.Count - 1].Index < _currentDifficulty)
		{
			_currentDifficulty = 0;
		}
		RefreshCurrentDifficulty();
		com_Difficulty.btn_Select.onClick.Set((EventCallback0)delegate
		{
			com_Difficulty.btn_Select.onClick.Retain();
			com_Difficulty.list_Difficulty.numItems = attrItems.Count;
			com_Difficulty.list_Difficulty.ResizeToFit();
			com_Difficulty.showSelect.selectedIndex = 1;
			com_Difficulty.btn_Select.onClick.Release();
		});
		return _currentDifficulty;
	}

	private void RefreshCurrentDifficulty()
	{
		ChoosingTimeLimitdifficultyConfigure choosingTimeLimitDifficultyConfigure = _currentDifficulty.GetChoosingTimeLimitDifficultyConfigure();
		if (choosingTimeLimitDifficultyConfigure != null)
		{
			com_Difficulty.btn_Select.title = choosingTimeLimitDifficultyConfigure.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
		}
	}

	public static UINewGameLibrary_Com_Monster CreateInstance()
	{
		return (UINewGameLibrary_Com_Monster)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_Monster");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Skin = (GComponent)GetChildAt(0);
		Com_LibraryFile = (GComponent)GetChildAt(1);
		com_Difficulty = (UINewGameLibrary_Com_Difficulty)GetChildAt(2);
		list_Items = (GList)GetChildAt(4);
	}
}
