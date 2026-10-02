using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UITutorial_Com_SelectHero : GComponent
{
	public List<HeroCardData> HeroInfos;

	public int SelectHeroId;

	public int SelectSkinItemId;

	private UITutorial_Com_SelectHeroItem _selectHeroItem;

	public Controller ShowHero;

	public GTextField txt_Tip;

	public GLoader loader_Skin_Image;

	public GLoader loader_Born;

	public GGraph loader_Animation;

	public GTextField txt_Nick;

	public GTextField txt_Name;

	public UITutorial_Com_HeroStory com_Story;

	public GList list_RendererSelectRole;

	public UITutorial_Button_Sure btn_SureHero;

	public Transition Cut_In;

	public Transition SwitchHero;

	public const string URL = "ui://b96qpoz6ia9g9";

	public void ShowComponent()
	{
		txt_Tip.text = 4.GetLocal(UIStringType.Tutorial);
		ReadyCharacterInfo();
		list_RendererSelectRole.itemRenderer = RendererSelectHero;
		int num = (int)Mathf.Ceil((float)HeroInfos.Count / 12f);
		if (num % 2 != 0)
		{
			num++;
		}
		list_RendererSelectRole.numItems = num * 12;
		list_RendererSelectRole.scrollPane.touchEffect = false;
		list_RendererSelectRole.ResizeToFit(24);
		DoSpecialEffect();
		ShowHero.selectedIndex = 0;
		btn_SureHero.touchable = false;
		btn_SureHero.grayed = true;
		Cut_In.Play();
	}

	private void ReadyCharacterInfo()
	{
		int[] heroPool = SimpleSingletonProvider<GameLogicManager>.inst.tutorial.HeroPool;
		HeroInfos = new List<HeroCardData>(heroPool.Length);
		int[] array = heroPool;
		foreach (int heroID in array)
		{
			HeroCardData cardData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroID);
			if (cardData != null)
			{
				HeroInfos.Add(cardData);
			}
		}
		HeroInfos.Sort(CompareTo);
	}

	private int CompareTo(HeroCardData heroX, HeroCardData heroY)
	{
		bool isHas = heroX.IsHas;
		bool isHas2 = heroY.IsHas;
		if (isHas == isHas2)
		{
			return heroX.InfoConfig.OrderWeight.CompareTo(heroY.InfoConfig.OrderWeight);
		}
		if (!isHas)
		{
			return 1;
		}
		return -1;
	}

	private void RendererSelectHero(int index, GObject item)
	{
		UITutorial_Com_SelectHeroItem heroItem = item as UITutorial_Com_SelectHeroItem;
		if (heroItem == null)
		{
			return;
		}
		heroItem.InitData((HeroInfos.Count > index) ? HeroInfos[index] : null);
		if (heroItem.Info == null)
		{
			return;
		}
		heroItem.onClick.Set((EventCallback0)delegate
		{
			heroItem.onClick.Retain();
			_selectHeroItem?.StopSkinVoice();
			_selectHeroItem = heroItem;
			SelectHeroId = heroItem.Info.HeroId;
			SelectSkinItemId = heroItem.Info.standingPainting.ItemID;
			(string, bool) character = heroItem.Info.standingPainting.GetCharacter();
			RendererSkin(character);
			RefreshHeroInfo(heroItem.Info.InfoConfig);
			heroItem.PlaySkinVoice();
			SwitchHero.Play();
			loader_Born.url = heroItem.Info.standingPainting.LandIcon;
			SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(heroItem.Info.standingPainting, "Walk", loader_Animation, 10f).Forget();
			Stage.inst.PlayOneShotSound(16);
			list_RendererSelectRole._children.ForEach(delegate(GObject btn)
			{
				if (btn != heroItem && btn is UITutorial_Com_SelectHeroItem uITutorial_Com_SelectHeroItem)
				{
					if (uITutorial_Com_SelectHeroItem.selected)
					{
						uITutorial_Com_SelectHeroItem.xuanze.PlayReverse();
					}
					uITutorial_Com_SelectHeroItem.selected = false;
					uITutorial_Com_SelectHeroItem.stateChange.selectedIndex = 0;
				}
			});
			heroItem.selected = true;
			heroItem.xuanze.Play();
			btn_SureHero.grayed = false;
			btn_SureHero.touchable = true;
			ShowHero.selectedIndex = 1;
			heroItem.onClick.Release();
		});
	}

	private async UniTask DoSpecialEffect()
	{
		await UniTask.NextFrame();
		float posY = list_RendererSelectRole.scrollPane.posY;
		int num = list_RendererSelectRole.numChildren;
		for (int i = 0; i < num; i++)
		{
			GObject childAt = list_RendererSelectRole.GetChildAt(i);
			float num2 = childAt.height + (float)list_RendererSelectRole.lineGap;
			float num3 = childAt.width + (float)list_RendererSelectRole.columnGap;
			float num4 = (1f - (childAt.y - posY) / num2) * 30f;
			childAt.x = num4 + (float)(i % 12) * num3;
		}
	}

	private void RendererSkin((string, bool) skinInfo)
	{
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(skinInfo.Item1, out var value))
		{
			customOffset = value.skinOffset;
		}
		if (!(loader_Skin_Image.url == skinInfo.Item1))
		{
			customOffset.y += 60f;
			loader_Skin_Image.customOffset = customOffset;
			loader_Skin_Image.customScale = Vector2.one * 1.1f;
			loader_Skin_Image.url = skinInfo.Item1;
		}
	}

	private void RefreshHeroInfo(CharacterInfoConfigure info)
	{
		txt_Nick.text = CharacterHandle.GetCharacterNickName(SelectHeroId);
		txt_Name.text = CharacterHandle.GetCharacterName(SelectHeroId);
		com_Story.txt_Story.text = info.BiographyID.GetLocal(UIStringType.Character);
	}

	public void OnClose()
	{
		base.visible = false;
		_selectHeroItem?.StopSkinVoice();
	}

	public static UITutorial_Com_SelectHero CreateInstance()
	{
		return (UITutorial_Com_SelectHero)UIPackage.CreateObject("Tutorial", "Tutorial_Com_SelectHero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ShowHero = GetControllerAt(0);
		txt_Tip = (GTextField)GetChildAt(5);
		loader_Skin_Image = (GLoader)GetChildAt(6);
		loader_Born = (GLoader)GetChildAt(7);
		loader_Animation = (GGraph)GetChildAt(8);
		txt_Nick = (GTextField)GetChildAt(10);
		txt_Name = (GTextField)GetChildAt(11);
		com_Story = (UITutorial_Com_HeroStory)GetChildAt(14);
		list_RendererSelectRole = (GList)GetChildAt(16);
		btn_SureHero = (UITutorial_Button_Sure)GetChildAt(17);
		Cut_In = GetTransitionAt(0);
		SwitchHero = GetTransitionAt(1);
	}
}
