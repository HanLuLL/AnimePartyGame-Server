using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public static class CommonUIManager
{
	public static string[] DifficultyIcons = new string[5] { "ui://m6sn3r22gyhsq3m", "ui://m6sn3r22gyhsq3h", "ui://m6sn3r22gyhsq3i", "ui://m6sn3r22gyhsq3j", "ui://m6sn3r22gyhsq3k" };

	public static string[] PVPModeIcons = new string[3] { "ui://m6sn3r22r26sq3t", "ui://m6sn3r22r26sq3u", "ui://m6sn3r22r26sq3v" };

	private static UICom_BuffInfo comBuffInfo;

	private static readonly Dictionary<int, Dictionary<int, List<GGraph>>> VideoGraphDict = new Dictionary<int, Dictionary<int, List<GGraph>>>();

	private static UIGlobalModalWaiting _modalWaitPane;

	public static async UniTask RegisterCommonPackage()
	{
		if (!UIPackage.ExistPackage("Common"))
		{
			await UIPackage.AddPackageAsync("Common", string.Empty);
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jz1wk0", typeof(UICom_PlayerLabel));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jaepoa6", typeof(UICom_PlayerLabel_Loader));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jheh5a5", typeof(UICom_Loader_PlayerLabel));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jz1wk2", typeof(UICom_PlayIcon));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jak421h", typeof(UICom_HeroSkin));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmouys8q", typeof(UICom_PlayerName));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmicc2g", typeof(UICom_Card));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jh334q3o", typeof(UICom_CardBack));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jgj393m", typeof(UICom_Card_Name));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jgj393l", typeof(UICom_Card_Icon));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8j9wtj8h", typeof(UICom_CostPoint));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0z2", typeof(UICom_LandCard));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw3z", typeof(UICom_Icon_Atk));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw40", typeof(UICom_Icon_Def));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw41", typeof(UICom_Icon_Card));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw3y", typeof(UICom_Icon_Gold));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw42", typeof(UICom_Icon_Hp));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jmmmw43", typeof(UICom_Icon_Mov));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8ju43ct", typeof(UICom_Expression));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jo812a", typeof(UICom_RelicKeyword));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78b3", typeof(UIButton_Next));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9l", typeof(UICom_SkillContent));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9m", typeof(UICom_CharacterFile));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpx78j9k", typeof(UICom_FileContent));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jfu2tq3e", typeof(UICom_RelicQuality_Large));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8j9wy8bw", typeof(UICom_Relic_Quality));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq42", typeof(UICom_LibraryCharacterFile));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq45", typeof(UICom_LibraryFileContent));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jpj0zq46", typeof(UICom_LibrarySkillContent));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbczmq3s", typeof(UICom_BuffInfo));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jbczmq3t", typeof(UICom_BuffInfoItem));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jm48iv", typeof(UICom_PopUpWindow_Bottom));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jm48iw", typeof(UICom_PopUpWindow_MohuBg));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jth3ls8e", typeof(UIGlobalModalWaiting));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jrct9q3d", typeof(UIRoomPlayer_Button_RewardUp));
			UIObjectFactory.SetPackageItemExtension("ui://xuaw6o8jimo37v", typeof(UICom_PlayerLevel));
		}
	}

	public static async UniTask RegisterCommonExternalPackage()
	{
		if (!UIPackage.ExistPackage("Common_External"))
		{
			await UIPackage.AddPackageAsync("Common_External", string.Empty);
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22jn4e9a", typeof(UICom_Item));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22hcgi26", typeof(UICom_ItemType));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22jz24ao", typeof(UICom_LitItem));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22gd4db8", typeof(UICom_QualityType));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ot0wbd", typeof(UIButton_Way));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22rct9q3e", typeof(UIRoomPlayer_Com_RewardUpDesc));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22zi0cbx", typeof(UICom_RoomPlayer));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22zi0cbp", typeof(UIRoomPlayer_Com_FriendStatus));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22zi0cbv", typeof(UIRoomPlayer_Com_MasterMenu));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22zi0cbg", typeof(UIRoomPlayer_Com_PlayerLabel));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22zi0cbs", typeof(UIRoomPlayer_Com_ShortChat));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22gib2q3p", typeof(UICom_InvitePlayer));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22o1g0c2", typeof(UIButton_SwitchTab));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22o1g0c3", typeof(UIButton_SwitchTabChild));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ot0w0", typeof(UIButton_GoodsItem));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22h0atqq41", typeof(UIButton_GoodsItem_Store));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22o1g0c4", typeof(UICom_GoodsItemName));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ot0w7", typeof(UICom_Label));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ot0w9", typeof(UICom_Price));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ot0wbc", typeof(UICom_GoodsQuality));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22h0atqq40", typeof(UICom_GoodsQuality_Store));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22px78j9h", typeof(UICom_CreateRoom));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22r2u9j8s", typeof(UIButton_RoomMapCard));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22esjw8q", typeof(UICom_RoomSetting));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ucnq9u", typeof(UIRoomSetting_Button_Info));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22ucnq9n", typeof(UIRoomSetting_ComboBox_popup));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22n2xaby", typeof(UIRoomSetting_Com_SetItem));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22n2xabz", typeof(UIRoomSetting_Com_SetItem_Upgrade));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22n2xac0", typeof(UIRoomSetting_Com_SetItem_ThinkTime));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22n2xac1", typeof(UIRoomSetting_Com_SetItem_GameSpeed));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r229536q3c", typeof(UIButton_MapEventSelect));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22uws2q3f", typeof(UIRoomSetting_Com_DifficultyExplain));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22l500qq49", typeof(UICom_MapTag));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22px78j9i", typeof(UIButton_MapInfoSelect));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22px78j9j", typeof(UICom_MapInfo));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22o812c5", typeof(UICom_SkinQuality));
			UIObjectFactory.SetPackageItemExtension("ui://m6sn3r22w8cnq37", typeof(UIButton_Collect));
		}
	}

	public static async UniTask RegisterCommonInternalPackage()
	{
		if (!UIPackage.ExistPackage("Common_Internal"))
		{
			await UIPackage.AddPackageAsync("Common_Internal", string.Empty);
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9imo37u", typeof(UICom_PlayerInfo));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9cz37bp", typeof(UICom_playerInfo_Head));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9kqgjbm", typeof(UIButton_MonsterItem));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9hqftbs", typeof(UIButton_Buff));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9rmeybs", typeof(UICom_AttrInfo));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9kv7e2d", typeof(UIButton_MoveArrow));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9qle6by", typeof(UICom_PlayerAttrInfo));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9qle6c0", typeof(UICom_NGOCounter));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9qle6c1", typeof(UICom_ModifyCounter));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9kxm5s8d", typeof(UICom_UniqueNum));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9l6yms8u", typeof(UICom_CrimeNum));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9drks1", typeof(UICom_AttrTip));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9bczmcc", typeof(UICom_Point));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9iu434p", typeof(UICom_BossTips));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9ul3cq4w", typeof(UICom_SelectTips));
			UIObjectFactory.SetPackageItemExtension("ui://1ov1i0v9iu434o", typeof(UICom_SuggestMove));
		}
	}

	public static void RemoveCommonPackage()
	{
		comBuffInfo?.Dispose();
		comBuffInfo = null;
		if (UIPackage.GetByName("Common") != null)
		{
			UIPackage.RemovePackage("Common");
		}
		RemoveCommonInternalPackage();
		RemoveCommonExternalPackage();
	}

	public static void RemoveCommonInternalPackage()
	{
		if (UIPackage.GetByName("Common_Internal") != null)
		{
			UIPackage.RemovePackage("Common_Internal");
		}
	}

	public static void RemoveCommonExternalPackage()
	{
		if (UIPackage.GetByName("Common_External") != null)
		{
			UIPackage.RemovePackage("Common_External");
		}
	}

	public static void PlayDice(int slot, int point, UICom_Point com_dice, Action Callback = null)
	{
		com_dice.player.selectedIndex = Mathf.Min(slot, 4);
		com_dice.data = true;
		com_dice.group_Point.visible = false;
		com_dice.aMovie_Dice.onPlayEnd.Set((EventCallback0)delegate
		{
			com_dice.txt_Point.text = point.ToString();
			com_dice.PointChange.Play();
			Callback?.Invoke();
			com_dice.data = false;
		});
		com_dice.aMovie_Dice.SetPlaySettings(0, -1, 3, point - 1);
		Stage.inst.PlayOneShotSound(8);
		com_dice.aMovie_Dice.playing = true;
	}

	public static void RendererCard(UICom_Card com_card, string url, string name, string content, string cardIndex, string cardTips, int cost, CardType cardType = CardType.None, CardTargetType targetType = CardTargetType.None, bool _showFront = true)
	{
		CardView cardView = new CardView
		{
			Key = url,
			IsVideo = false,
			IsAltArtCard = false
		};
		RendererCard(com_card, cardView, name, content, cardIndex, cardTips, cost, cardType, targetType, _showFront);
	}

	private static async UniTask RendererAltCardFront(UICom_Card com_card, CardView cardView)
	{
		if (!string.IsNullOrEmpty(cardView.CardFrontMatKey))
		{
			GLoader loader_CardFront = com_card.loader_CardFront;
			loader_CardFront.material = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial(cardView.CardFrontMatKey);
		}
		com_card.loader_CardFront.url = cardView.CardFrontTexKey;
		if (cardView.CardType == 2)
		{
			GImage bg_txt = com_card.bg_txt;
			bg_txt.material = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial("Image_GammaAlpha");
		}
	}

	private static void RendererCardContent(UICom_Card com_card, CardView cardView, string name, string content, string cardIndex, string cardTips, int cost, CardType cardType, CardTargetType targetType, bool _showFront)
	{
		if (cardView.IsVideo)
		{
			com_card.isVideo.selectedIndex = 1;
			if (cardView.CardType == 1)
			{
				SimpleSingletonProvider<CriMovieManager>.inst.PlaAutoReleaseVideo(cardView.Key, com_card.video_FrontCard, 0, PlayEndImmediatelyStop: false, uiRenderMode: true).Forget();
			}
			else if (cardView.CardType == 2)
			{
				SimpleSingletonProvider<CriMovieManager>.inst.PlaAutoReleaseVideo(cardView.Key, com_card.video_FullCard, 0, PlayEndImmediatelyStop: false, uiRenderMode: true).Forget();
			}
		}
		else
		{
			com_card.isVideo.selectedIndex = 0;
			com_card.loader_FrontCard.url = cardView.Key;
			com_card.loader_FullCard.url = cardView.Key;
		}
		com_card.frontState.selectedIndex = cardView.CardType;
		com_card.showFront.selectedIndex = ((!_showFront) ? 1 : 0);
		com_card.txt_Name.text = name;
		com_card.com_CardName.CardType.selectedIndex = (int)cardType;
		com_card.txt_Content.text = MatchSkillTextContent(content);
		com_card.txt_CardIndex.text = cardIndex;
		com_card.txt_CardIndex_2.text = cardIndex;
		com_card.txt_CardTips.text = cardTips;
		com_card.com_CardIcon.Cost.selectedIndex = cost;
		com_card.com_CardIcon.CardType.selectedIndex = (int)cardType;
		com_card.com_CardIcon.TargetType.selectedIndex = (int)targetType;
		if (cardView.IsAltArtCard)
		{
			RendererAltCardFront(com_card, cardView).Forget();
		}
	}

	public static void RendererCard(UICom_Card com_card, CardView cardView, string name, string content, string cardIndex, string cardTips, int cost, CardType cardType = CardType.None, CardTargetType targetType = CardTargetType.None, bool _showFront = true, bool showRedPoint = false)
	{
		RendererCardContent(com_card, cardView, name, content, cardIndex, cardTips, cost, cardType, targetType, _showFront);
		if (SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap.TryGetValue(3, out var value) && StaticConfigure.Fashion.CardBackDict.TryGetValue(value, out var value2))
		{
			com_card.loader_CardFrame.visible = true;
			com_card.loader_CardFrame.url = value2.CardFront;
			SetCardFrontColor(com_card.loader_CardFrame, value2.CardFrontColor);
		}
		else
		{
			com_card.loader_CardFrame.visible = false;
		}
		com_card.com_CardBack.visible = false;
		if (showRedPoint)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.IsReceiveNewAltArtCard(cardView.CardId);
			com_card.redPoint.selectedIndex = (flag ? 1 : 0);
		}
		else
		{
			com_card.redPoint.selectedIndex = 0;
		}
	}

	public static void RendererCardInRoom(long playerId, UICom_Card com_card, string url, string name, string content, string cardIndex, string cardTips, int cost, CardType cardType = CardType.None, CardTargetType targetType = CardTargetType.None, bool _showFront = true)
	{
		CardView cardView = new CardView
		{
			Key = url,
			IsVideo = false
		};
		RendererCardInRoom(playerId, com_card, cardView, name, content, cardIndex, cardTips, cost, cardType, targetType, _showFront);
	}

	public static async void RendererCardInRoom(long playerId, UICom_Card com_card, CardView cardView, string name, string content, string cardIndex, string cardTips, int cost, CardType cardType = CardType.None, CardTargetType targetType = CardTargetType.None, bool _showFront = true)
	{
		FashionCardBackConfigure cardFashion = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.CardBackConfig(playerId);
		if (!_showFront)
		{
			await RendererCardBack(com_card.com_CardBack, cardFashion);
		}
		com_card.loader_CardFrame.url = cardFashion.CardFront;
		SetCardFrontColor(com_card.loader_CardFrame, cardFashion.CardFrontColor);
		RendererCardContent(com_card, cardView, name, content, cardIndex, cardTips, cost, cardType, targetType, _showFront);
	}

	private static void SetCardFrontColor(GLoader loader_CardFrame, string colorHTML)
	{
		if (ColorUtility.TryParseHtmlString(colorHTML, out var color))
		{
			loader_CardFrame.color = color;
		}
		else
		{
			loader_CardFrame.color = Color.white;
		}
	}

	public static async UniTask RendererCardBack(UICom_CardBack comCardBack, FashionCardBackConfigure cardFashionConfig)
	{
		if (!string.IsNullOrEmpty(cardFashionConfig.CardBackMaterial))
		{
			Material material = await SimpleSingletonProvider<Core.MaterialManager>.inst.GetMaterial(cardFashionConfig.CardBackMaterial);
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(cardFashionConfig.CardPerview, delegate(NTexture texture)
			{
				comCardBack.loader_CardBack.material = material;
				comCardBack.loader_CardBack.material.SetTexture("_MainTex", texture.nativeTexture);
				comCardBack.loader_CardBack.url = cardFashionConfig.CardPerview;
			}, null);
		}
		else
		{
			comCardBack.loader_CardBack.material = null;
			comCardBack.loader_CardBack.url = cardFashionConfig.CardPerview;
		}
	}

	public static void RendererHeadShot(UICom_PlayerLabel com_Label, string _headShot, bool isVideo)
	{
		com_Label.com_PlayerPhoto.loader_PlayerPhoto.url = _headShot;
	}

	public static void RendererLabel(UIType uiType, int uiEnumValue, UICom_PlayerLabel com_Label, string _labelName, bool isVideo)
	{
		RendererLabel(uiType, uiEnumValue, com_Label.loader_Label, _labelName, isVideo);
	}

	public static void RendererLabel(UIType uiType, int uiEnumValue, UICom_PlayerLabel_Loader loader_Label, string _labelName, bool isVideo)
	{
		UICom_Loader_PlayerLabel com_Loader = loader_Label.com_Loader;
		if (isVideo)
		{
			com_Loader.type.selectedIndex = 1;
			TryAddVideoGraph(uiType, uiEnumValue, com_Loader.loader_Video);
			SimpleSingletonProvider<CriMovieManager>.inst.Play(_labelName, com_Loader.loader_Video).Forget();
		}
		else
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(com_Loader.loader_Video);
			com_Loader.type.selectedIndex = 0;
			com_Loader.loader_Image.url = _labelName;
		}
	}

	public static void RendererLabelInfo(UICom_PlayerLabel com_Label, string _playerName, long _playerLv)
	{
		com_Label.com_Name.txt_PlayerName.TryScrollTextField(_playerName, AlignType.Left);
		float width = com_Label.com_Name.txt_PlayerName.width;
		com_Label.image_Icon.width = Mathf.Clamp(60f + width, 60f, 530f);
		com_Label.txt_lv.text = _playerLv.ToString();
		com_Label.showLevel.selectedIndex = 0;
	}

	public static void RendererLabel(UICom_PlayerLabel com_Label, GameModeNPCPlayerConfigure npcPlayerConfigure)
	{
		if (npcPlayerConfigure != null && npcPlayerConfigure.MonsterId != 0)
		{
			MonsterInfoConfigure monsterInfoConfigure = npcPlayerConfigure.MonsterId.GetMonsterInfoConfigure();
			if (monsterInfoConfigure != null)
			{
				RendererLabelInfo(com_Label, monsterInfoConfigure.NameID.GetLocal(UIStringType.Monster), 0L);
				com_Label.showLevel.selectedIndex = 1;
				com_Label.com_PlayerPhoto.loader_PlayerPhoto.url = npcPlayerConfigure.PlayerPhoto;
				com_Label.loader_Label.com_Loader.type.selectedIndex = 0;
				SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(com_Label.loader_Label.com_Loader.loader_Video);
				com_Label.loader_Label.com_Loader.loader_Image.url = npcPlayerConfigure.AccountBackground;
			}
		}
	}

	public static void RendererSkin(UIType uiType, int uiEnumValue, UICom_HeroSkin _loader, string _URL, bool isVideo, Vector2 _offset, Vector2 _scale)
	{
		if (isVideo)
		{
			_loader.type.selectedIndex = 1;
			TryAddVideoGraph(uiType, uiEnumValue, _loader.loader_Skin_Video);
			SimpleSingletonProvider<CriMovieManager>.inst.Play(_URL, _loader.loader_Skin_Video).Forget();
			return;
		}
		_loader.type.selectedIndex = 0;
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(_loader.loader_Skin_Video);
		if (!(_loader.loader_Skin_Image.url == _URL))
		{
			_loader.loader_Skin_Image.customOffset = _offset;
			_loader.loader_Skin_Image.customScale = _scale;
			_loader.loader_Skin_Image.url = _URL;
		}
	}

	public static void StopAllVideo()
	{
		foreach (Dictionary<int, List<GGraph>> value in VideoGraphDict.Values)
		{
			if (value == null)
			{
				continue;
			}
			foreach (List<GGraph> value2 in value.Values)
			{
				foreach (GGraph item in value2)
				{
					SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(item);
				}
			}
		}
		VideoGraphDict.Clear();
	}

	public static void StopVideo(UIType uiType, int uiEnumValue)
	{
		StopVideoByUIType(uiType, uiEnumValue);
		if (uiType == UIType.Panel)
		{
			StopVideoByUIType(UIType.None, 0);
		}
	}

	private static void StopVideoByUIType(UIType uiType, int uiEnumValue)
	{
		if (!VideoGraphDict.TryGetValue((int)uiType, out var value) || !value.TryGetValue(uiEnumValue, out var value2))
		{
			return;
		}
		foreach (GGraph item in value2)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(item);
		}
		value2.Clear();
	}

	public static void TryAddVideoGraph(UIType uiType, int uiEnumValue, GGraph graph)
	{
		if (!VideoGraphDict.TryGetValue((int)uiType, out var value))
		{
			value = new Dictionary<int, List<GGraph>>();
			VideoGraphDict.Add((int)uiType, value);
		}
		if (!value.TryGetValue(uiEnumValue, out var value2))
		{
			value2 = new List<GGraph>();
			value.Add(uiEnumValue, value2);
		}
		if (!value2.Contains(graph))
		{
			value2.Add(graph);
		}
	}

	public static void RendererLitItem(UICom_LitItem item, int itemId, int count, bool showCount = true, bool usable = false)
	{
		ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
		item.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		item.loader_Icon.url = itemInfoConfigure.ShowIcon;
		item.txt_itemNum.text = count.ToString();
		item.isShowNum.selectedIndex = ((!showCount) ? 1 : 0);
		item.onClick.Set((EventCallback0)delegate
		{
			item.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, count, usable).Forget();
			item.onClick.Release();
		});
	}

	public static void RefreshCharacterSkillHyperlinkDesc(this int descID, GRichTextField textField)
	{
		string local = descID.GetLocal(UIStringType.Skill);
		textField.text = MatchSkillTextContent(local);
		textField.onClickLink.Set(TryClickLink);
	}

	public static void RefreshMonsterSkillHyperlinkDesc(this int descID, GRichTextField textField, int difficulty)
	{
		string local = descID.GetLocal(UIStringType.Skill);
		textField.text = MatchSkillTextContent(local, difficulty);
		textField.onClickLink.Set(TryClickLink);
	}

	public static void RefreshHyperlinkDesc(string desc, GRichTextField textField, int difficulty = 0)
	{
		textField.text = MatchSkillTextContent(desc, difficulty);
		textField.onClickLink.Set(TryClickLink);
	}

	public static UICom_RelicKeyword.RelicKeywordData TryShowDisplayCardRelicKeywordData(string linkData)
	{
		linkData = MatchSkillTextContent(linkData);
		string pattern = "\\[url=([A-Za-z]+):([^\\]]+)\\](.*?)\\[\\/url\\]";
		Match match = Regex.Match(linkData, pattern);
		string value = match.Groups[1].Value;
		string value2 = match.Groups[2].Value;
		if (value == "Buff" && int.TryParse(value2, out var result))
		{
			BuffInfoConfigure buffConfigure = result.GetBuffConfigure();
			if (buffConfigure != null)
			{
				return new UICom_RelicKeyword.RelicKeywordData
				{
					icon = buffConfigure.Icon,
					title = buffConfigure.NameId.GetLocal(UIStringType.Buff),
					desc = buffConfigure.DescId.GetLocal(UIStringType.Buff)
				};
			}
		}
		return default(UICom_RelicKeyword.RelicKeywordData);
	}

	private static string MatchSkillTextContent(string text, int difficulty = 0)
	{
		string pattern = "\\[Astral=([A-Za-z]+):([^\\]]+)\\](.*?)\\[\\/Astral\\]";
		return Regex.Replace(text, pattern, delegate(Match match)
		{
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string value3 = match.Groups[3].Value;
			return value switch
			{
				"Card" => MatchCard(value2), 
				"Blood" => MatchBlood(value2, difficulty), 
				"Buff" => MatchBuff(value2), 
				"EventCards" => MatchEventCard(value2, value3), 
				_ => match.Value, 
			};
		});
	}

	private static string MatchBuff(string param)
	{
		if (!int.TryParse(param, out var result))
		{
			return "";
		}
		BuffInfoConfigure buffConfigure = result.GetBuffConfigure();
		if (buffConfigure == null)
		{
			return "";
		}
		if (buffConfigure.NameId != 0)
		{
			string local = buffConfigure.NameId.GetLocal(UIStringType.Buff);
			return $"[url=Buff:{result}][color=#FFB425]{local}[/color][/url]";
		}
		Debug.LogError($"展示Buff:{buffConfigure.Id}, NameId = 0");
		return "";
	}

	private static string MatchCard(string param)
	{
		if (!int.TryParse(param, out var result))
		{
			return "";
		}
		CardInfoConfigure cardConfigure = result.GetCardConfigure();
		if (cardConfigure == null)
		{
			return "";
		}
		string local = cardConfigure.NameID.GetLocal(UIStringType.Card);
		return $"[url=Card:{result}][color=#FFB425]{local}[/color][/url]";
	}

	private static string MatchBlood(string param, int difficulty)
	{
		string[] array = param.Split('*');
		if (array.Length != 2)
		{
			return "0";
		}
		if (!int.TryParse(array[0], out var result))
		{
			return "0";
		}
		if (!float.TryParse(array[1], out var _))
		{
			return "0";
		}
		float f = CharacterHandle.GetCharacterBlood(result, CharacterType.None, difficulty);
		return $"({Mathf.Floor(f):f0})";
	}

	private static string MatchEventCard(string param, string content)
	{
		if (param == null)
		{
			return "";
		}
		if (content == null)
		{
			content = "";
		}
		return "[url=EventCard:" + param + "][color=#FFB425]" + content + "[/color][/url]";
	}

	private static void TryClickLink(EventContext context)
	{
		if (!(context.data is string text))
		{
			return;
		}
		string[] array = text.Split(':');
		if (array.Length != 2)
		{
			return;
		}
		switch (array[0])
		{
		case "Card":
		{
			if (int.TryParse(array[1], out var result3))
			{
				CardInfoConfigure cardConfigure = result3.GetCardConfigure();
				if (cardConfigure != null)
				{
					SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowHandCard(new List<CardInfoConfigure> { cardConfigure }, 0);
				}
			}
			break;
		}
		case "Buff":
		{
			if (int.TryParse(array[1], out var result2))
			{
				ShowBuffInfo(result2);
			}
			break;
		}
		case "EventCard":
		{
			string[] array2 = array[1].Split("|");
			if (array2 == null || array2.Length <= 0)
			{
				break;
			}
			List<EventInfoConfigure> list = new List<EventInfoConfigure>();
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				if (int.TryParse(array3[i], out var result))
				{
					list.Add(result.GetEventConfigure());
				}
			}
			SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowEventCard(list, 0);
			break;
		}
		}
	}

	public static void ShowBuffInfo(int buffId)
	{
		if (comBuffInfo == null)
		{
			comBuffInfo = UICom_BuffInfo.CreateInstance();
		}
		comBuffInfo.list_Buff.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICom_BuffInfoItem uICom_BuffInfoItem)
			{
				BuffInfoConfigure buffConfigure = buffId.GetBuffConfigure();
				if (buffConfigure != null)
				{
					uICom_BuffInfoItem.loader_Buff.url = buffConfigure.Icon;
					if (buffConfigure.NameId != 0)
					{
						uICom_BuffInfoItem.txt_Title.text = buffConfigure.NameId.GetLocal(UIStringType.Buff);
					}
					else
					{
						Debug.LogError($"展示Buff:{buffConfigure.Id}, NameId = 0");
					}
					if (buffConfigure.DescId != 0)
					{
						RefreshHyperlinkDesc(buffConfigure.DescId.GetLocal(UIStringType.Buff), uICom_BuffInfoItem.txt_Desc);
					}
					else
					{
						Debug.LogError($"展示Buff:{buffConfigure.Id}, DescId = 0");
					}
				}
			}
		};
		comBuffInfo.list_Buff.numItems = 1;
		comBuffInfo.list_Buff.ResizeToFit(1);
		GRoot.inst.ShowPopup(comBuffInfo, GRoot.inst.touchTarget);
		Vector2 vector = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
		float width = GRoot.inst.width;
		if (vector.x + comBuffInfo.width > width)
		{
			vector.x = width - comBuffInfo.width;
		}
		comBuffInfo.position = vector;
	}

	public static void ShowModalWait()
	{
		if (_modalWaitPane == null || _modalWaitPane.isDisposed)
		{
			_modalWaitPane = UIGlobalModalWaiting.CreateInstance();
			_modalWaitPane.SetHome(GRoot._inst);
		}
		_modalWaitPane.sortingOrder = 999999;
		_modalWaitPane.SetSize(GRoot._inst.width, GRoot._inst.height);
		_modalWaitPane.AddRelation(GRoot._inst, FairyGUI.RelationType.Size);
		GRoot._inst.AddChild(_modalWaitPane);
		_modalWaitPane.SetBlur().Forget();
	}

	public static void CloseModalWait()
	{
		if (_modalWaitPane != null && _modalWaitPane.parent != null)
		{
			GRoot._inst.RemoveChild(_modalWaitPane);
		}
	}
}
