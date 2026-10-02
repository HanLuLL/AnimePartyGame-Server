using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using Google.Protobuf.Collections;

namespace UI;

public class UIActivityMapPass_MapInfo : GComponent
{
	public int MapId;

	public MapInfoConfigure mapInfo;

	private HashSet<string> mapMonsterHeadImages;

	public Controller showBossCount;

	public GTextField txt_name;

	public GLoader loader_bossImg2;

	public GLoader loader_bossImg1;

	public GGroup two;

	public GLoader loader_bossImg;

	public GGroup one;

	public GGroup mapButton;

	public const string URL = "ui://vvv9zaj1qa2x2";

	public void RefreshUI(int mapId)
	{
		if (MapId == mapId)
		{
			return;
		}
		MapId = mapId;
		if (!StaticConfigure.Map.InfoDict.TryGetValue(MapId, out mapInfo))
		{
			return;
		}
		if (mapMonsterHeadImages == null)
		{
			mapMonsterHeadImages = new HashSet<string>();
		}
		mapMonsterHeadImages.Clear();
		GetMonsterHeadImages(mapMonsterHeadImages);
		txt_name.text = mapInfo.MapName.GetLocal(UIStringType.Map);
		showBossCount.selectedIndex = ((mapMonsterHeadImages.Count > 1) ? 1 : 0);
		if (mapMonsterHeadImages.Count == 1)
		{
			foreach (string mapMonsterHeadImage in mapMonsterHeadImages)
			{
				loader_bossImg.url = mapMonsterHeadImage;
			}
			return;
		}
		if (mapMonsterHeadImages.Count >= 2)
		{
			HashSet<string>.Enumerator enumerator2 = mapMonsterHeadImages.GetEnumerator();
			enumerator2.MoveNext();
			loader_bossImg1.url = enumerator2.Current;
			enumerator2.MoveNext();
			loader_bossImg2.url = enumerator2.Current;
			enumerator2.Dispose();
		}
	}

	public override void Dispose()
	{
		MapId = 0;
		mapInfo = null;
		if (mapMonsterHeadImages != null)
		{
			mapMonsterHeadImages.Clear();
			mapMonsterHeadImages = null;
		}
		base.Dispose();
	}

	private void GetMonsterHeadImages(HashSet<string> monsterHeadImages)
	{
		if (monsterHeadImages == null || mapInfo == null)
		{
			return;
		}
		foreach (int preloadCharacterId in mapInfo.PreloadCharacterIds)
		{
			MonsterInfoConfigure monsterCharacterConfigure = CharacterHandle.GetMonsterCharacterConfigure(preloadCharacterId);
			if (monsterCharacterConfigure != null && monsterCharacterConfigure.MonsterType == MonsterType.Boss)
			{
				monsterHeadImages.Add(monsterCharacterConfigure.CharacterMap);
			}
		}
		RepeatedField<int> difficultyIds = mapInfo.DifficultyIds;
		if (difficultyIds == null || difficultyIds.Count <= 0)
		{
			return;
		}
		foreach (int item in difficultyIds)
		{
			RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = item.GetMapGameDifficultyItems();
			for (int i = 0; i < mapGameDifficultyItems.Count; i++)
			{
				foreach (int preloadCharacterId2 in mapGameDifficultyItems[i].PreloadCharacterIds)
				{
					if (CharacterHandle.GetCharacterMonsterType(preloadCharacterId2) == MonsterType.Boss)
					{
						monsterHeadImages.Add(CharacterHandle.GetCharacterMap(preloadCharacterId2));
					}
				}
			}
		}
	}

	public static UIActivityMapPass_MapInfo CreateInstance()
	{
		return (UIActivityMapPass_MapInfo)UIPackage.CreateObject("ActivityMapPass", "ActivityMapPass_MapInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showBossCount = GetControllerAt(0);
		txt_name = (GTextField)GetChildAt(0);
		loader_bossImg2 = (GLoader)GetChildAt(1);
		loader_bossImg1 = (GLoader)GetChildAt(2);
		two = (GGroup)GetChildAt(3);
		loader_bossImg = (GLoader)GetChildAt(4);
		one = (GGroup)GetChildAt(5);
		mapButton = (GGroup)GetChildAt(6);
	}
}
