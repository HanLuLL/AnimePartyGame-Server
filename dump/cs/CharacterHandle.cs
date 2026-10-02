using System.Collections.Generic;
using Google.Protobuf.Collections;
using UI;
using UnityEngine;

public class CharacterHandle
{
	public readonly int Id;

	public readonly CharacterType CharacterType;

	public readonly int OrderWeight;

	public readonly int NameID;

	public readonly int NickID;

	public readonly int CommentId;

	public readonly int BiographyID;

	public readonly bool IsGalleryShow;

	public readonly int Mechanism;

	public readonly CharacterTagType TagType;

	public readonly int Blood;

	public readonly int Attack;

	public readonly int Defense;

	public readonly string CharacterMap;

	public readonly RepeatedField<int> OffsetInMap;

	public readonly int StandingPainting;

	public readonly bool IsLinkage;

	public readonly int linesID;

	public readonly string NameBGColor;

	public readonly bool IsDefault;

	public readonly RepeatedField<int> PveBreak;

	public readonly string LandTex;

	public readonly int ExpressionPackID;

	public readonly bool HasKizuna;

	public readonly int HeroFavorGift;

	public readonly int FavorLevelReward;

	public readonly int FavorBreakthroughReward;

	public readonly int IntenseFixSkill;

	public readonly MonsterType MonsterType;

	public readonly int Gold;

	public readonly bool CanCounter;

	public CharacterHandle(int characterId)
	{
		Id = characterId;
		CharacterType = GetCharacterType(characterId);
		OrderWeight = GetCharacterOrderWeight(characterId, CharacterType);
		NameID = GetCharacterNameId(characterId, CharacterType);
		NickID = GetCharacterNickId(characterId, CharacterType);
		CommentId = GetCharacterCommentId(characterId, CharacterType);
		BiographyID = GetCharacterBiographyId(characterId, CharacterType);
		IsGalleryShow = GetCharacterIsGalleryShow(characterId, CharacterType);
		Mechanism = GetCharacterMechanism(characterId, CharacterType);
		TagType = GetCharacterTagType(characterId, CharacterType);
		Blood = GetCharacterBlood(characterId, CharacterType);
		Attack = GetCharacterAttack(characterId, CharacterType);
		Defense = GetCharacterDefense(characterId, CharacterType);
		CharacterMap = GetCharacterMap(characterId, CharacterType);
		OffsetInMap = GetCharacterOffsetInMap(characterId, CharacterType);
		StandingPainting = GetCharacterStandingPainting(characterId, CharacterType);
		switch (CharacterType)
		{
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			IsLinkage = heroCharacterConfigure?.IsLinkage ?? false;
			linesID = heroCharacterConfigure?.LinesID ?? 0;
			NameBGColor = heroCharacterConfigure?.NameBGColor ?? "";
			IsDefault = heroCharacterConfigure?.IsDefault ?? false;
			PveBreak = heroCharacterConfigure?.PveBreak ?? new RepeatedField<int>();
			LandTex = heroCharacterConfigure?.LandTex ?? "";
			ExpressionPackID = heroCharacterConfigure?.ExpressionPackID ?? 0;
			HasKizuna = heroCharacterConfigure?.HasKizuna ?? false;
			HeroFavorGift = heroCharacterConfigure?.HeroFavorGift ?? 0;
			FavorLevelReward = heroCharacterConfigure?.FavorLevelReward ?? 0;
			FavorBreakthroughReward = heroCharacterConfigure?.FavorBreakthroughReward ?? 0;
			IntenseFixSkill = heroCharacterConfigure?.IntenseFixSkill ?? 0;
			break;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			MonsterType = monsterCharacterConfigure?.MonsterType ?? MonsterType.None;
			Gold = monsterCharacterConfigure?.Gold ?? 0;
			CanCounter = monsterCharacterConfigure?.CanCounter ?? false;
			break;
		}
		case CharacterType.None:
			break;
		}
	}

	public static int GetBattleActiveSkillId(int characterId, CharacterType characterType = CharacterType.None, int talentId = 0)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.GetBattleActiveSkillId(talentId);
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.GetBattleActiveSkillId();
		}
		default:
			return 0;
		}
	}

	public static RepeatedField<int> GetBattlePassiveSkills(int characterId, CharacterType characterType = CharacterType.None, int talentId = 0)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return null;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return null;
			}
			return heroCharacterConfigure.GetBattlePassiveSkills(talentId);
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return null;
			}
			return monsterCharacterConfigure.GetBattlePassiveSkills();
		}
		default:
			return null;
		}
	}

	public static int GetCharacterOrderWeight(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.OrderWeight;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.OrderWeight;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterNameId(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.NameID;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.NameID;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterNickId(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.NickID;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.NickID;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterCommentId(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.CommentId;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.CommentId;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterBiographyId(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.BiographyID;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.BiographyID;
		}
		default:
			return 0;
		}
	}

	public static bool GetCharacterIsGalleryShow(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return false;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return false;
			}
			return heroCharacterConfigure.IsGalleryShow;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return false;
			}
			return monsterCharacterConfigure.IsGalleryShow;
		}
		default:
			return false;
		}
	}

	public static int GetCharacterMechanism(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.MechanismID;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.MechanismID;
		}
		default:
			return 0;
		}
	}

	public static CharacterTagType GetCharacterTagType(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return CharacterTagType.None;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return CharacterTagType.None;
			}
			return heroCharacterConfigure.TagType;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return CharacterTagType.None;
			}
			return monsterCharacterConfigure.TagType;
		}
		default:
			return CharacterTagType.None;
		}
	}

	public static string GetCharacterMap(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return "";
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return "";
			}
			return heroCharacterConfigure.CharacterMap;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return "";
			}
			return monsterCharacterConfigure.CharacterMap;
		}
		default:
			return "";
		}
	}

	public static RepeatedField<int> GetCharacterOffsetInMap(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return null;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return null;
			}
			return heroCharacterConfigure.OffsetInMap;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return null;
			}
			return monsterCharacterConfigure.OffsetInMap;
		}
		default:
			return null;
		}
	}

	public static int GetCharacterStandingPainting(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.StandingPainting;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			return monsterCharacterConfigure.StandingPainting;
		}
		default:
			return 0;
		}
	}

	public static string GetCharacterName(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return "";
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return "";
			}
			return heroCharacterConfigure.NameID.GetLocal(UIStringType.Character);
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return "";
			}
			return monsterCharacterConfigure.NameID.GetLocal(UIStringType.Monster);
		}
		default:
			return "";
		}
	}

	public static string GetCharacterNickName(int characterId, CharacterType characterType = CharacterType.None)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return "";
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return "";
			}
			return heroCharacterConfigure.NickID.GetLocal(UIStringType.Character);
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return "";
			}
			return monsterCharacterConfigure.NickID.GetLocal(UIStringType.Monster);
		}
		default:
			return "";
		}
	}

	public static int GetCharacterBlood(int characterId, CharacterType characterType = CharacterType.None, int difficulty = 0)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.Blood;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			if (difficulty <= 0)
			{
				return monsterCharacterConfigure.Blood;
			}
			MonsterAttributeConfigureItem monsterAttributeConfigureByDifficulty = monsterCharacterConfigure.GetMonsterAttributeConfigureByDifficulty(difficulty);
			if (monsterAttributeConfigureByDifficulty == null)
			{
				Debug.LogError($"数据异常，角色{characterId}difficulty为{difficulty}的属性配置为null");
				return 0;
			}
			return monsterAttributeConfigureByDifficulty.Blood;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterAttack(int characterId, CharacterType characterType = CharacterType.None, int difficulty = 0)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.Attack;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			if (difficulty <= 0)
			{
				return monsterCharacterConfigure.Attack;
			}
			MonsterAttributeConfigureItem monsterAttributeConfigureByDifficulty = monsterCharacterConfigure.GetMonsterAttributeConfigureByDifficulty(difficulty);
			if (monsterAttributeConfigureByDifficulty == null)
			{
				Debug.LogError($"数据异常，角色{characterId}difficulty为{difficulty}的属性配置为null");
				return 0;
			}
			return monsterAttributeConfigureByDifficulty.Attack;
		}
		default:
			return 0;
		}
	}

	public static int GetCharacterDefense(int characterId, CharacterType characterType = CharacterType.None, int difficulty = 0)
	{
		if (characterType == CharacterType.None)
		{
			characterType = GetCharacterType(characterId);
		}
		switch (characterType)
		{
		case CharacterType.None:
			Debug.LogError($"数据异常，角色{characterId}类型为{characterType}");
			return 0;
		case CharacterType.Hero:
		{
			CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
			if (heroCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}characterConfigure为null");
				return 0;
			}
			return heroCharacterConfigure.Defense;
		}
		case CharacterType.Monster:
		{
			MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
			if (monsterCharacterConfigure == null)
			{
				Debug.LogError($"数据异常，角色{characterId}monsterInfoConfigure为null");
				return 0;
			}
			if (difficulty <= 0)
			{
				return monsterCharacterConfigure.Defense;
			}
			MonsterAttributeConfigureItem monsterAttributeConfigureByDifficulty = monsterCharacterConfigure.GetMonsterAttributeConfigureByDifficulty(difficulty);
			if (monsterAttributeConfigureByDifficulty == null)
			{
				Debug.LogError($"数据异常，角色{characterId}difficulty为{difficulty}的属性配置为null");
				return 0;
			}
			return monsterAttributeConfigureByDifficulty.Defense;
		}
		default:
			return 0;
		}
	}

	public static CharacterType GetCharacterType(int characterId)
	{
		CharacterInfoConfigure heroCharacterConfigure = GetHeroCharacterConfigure(characterId);
		if (heroCharacterConfigure != null && heroCharacterConfigure.HeroType == CharacterType.Hero)
		{
			return CharacterType.Hero;
		}
		MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(characterId);
		if (monsterCharacterConfigure != null && monsterCharacterConfigure.HeroType == CharacterType.Monster)
		{
			return CharacterType.Monster;
		}
		return CharacterType.None;
	}

	public static CharacterHeroFavorGiftConfigure GetCharacterHeroFavorGiftConfigure(int characterId)
	{
		return characterId.GetheroFavorGift();
	}

	public static int GetCharacterGold(int heroId)
	{
		MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(heroId);
		if (monsterCharacterConfigure == null)
		{
			Debug.LogError($"数据异常，角色{heroId}monsterInfoConfigure为null");
			return 0;
		}
		return monsterCharacterConfigure.Gold;
	}

	public static bool GetCharacterCanCounter(int heroId)
	{
		MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(heroId);
		if (monsterCharacterConfigure == null)
		{
			Debug.LogError($"数据异常，角色{heroId}monsterInfoConfigure为null");
			return false;
		}
		return monsterCharacterConfigure.CanCounter;
	}

	public static MonsterType GetCharacterMonsterType(int heroId)
	{
		MonsterInfoConfigure monsterCharacterConfigure = GetMonsterCharacterConfigure(heroId);
		if (monsterCharacterConfigure == null)
		{
			Debug.LogError($"数据异常，角色{heroId}monsterInfoConfigure为null");
			return MonsterType.None;
		}
		return monsterCharacterConfigure.MonsterType;
	}

	public static CharacterInfoConfigure GetHeroCharacterConfigure(int characterId)
	{
		return StaticConfigure.Character.InfoDict.GetValueOrDefault(characterId, null);
	}

	public static MonsterInfoConfigure GetMonsterCharacterConfigure(int characterId)
	{
		return StaticConfigure.Monster.InfoDict.GetValueOrDefault(characterId);
	}

	public static int TryGetCharacterGold(int heroId)
	{
		return GetMonsterCharacterConfigure(heroId)?.Gold ?? 0;
	}

	public static bool TryGetCharacterCanCounter(int heroId)
	{
		return GetMonsterCharacterConfigure(heroId)?.CanCounter ?? false;
	}
}
