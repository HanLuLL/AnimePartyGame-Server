using System;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class UIMGWT_Com_Advent : GComponent
{
	private CollaborationInfoConfigure InfoConfig;

	private Action BuyAction;

	public Controller language;

	public GLoader loader_sherry;

	public GLoader loader_hanna;

	public UIMGWT_Preview_Button preview_sherry;

	public UIMGWT_Preview_Button preview_hanna;

	public GButton btn_buy;

	public Transition Cut_in;

	public const string URL = "ui://2p754tqkilj58";

	public void Init(CollaborationInfoConfigure info, Action buy)
	{
		InfoConfig = info;
		BuyAction = buy;
		RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[0], preview_hanna);
		RefreshHeroAdvertInfo(InfoConfig.PreviewIndex[1], preview_sherry);
		int dataForLanguage = GameSettings.GetDataForLanguage(1, 2, 0, 3);
		language.selectedIndex = dataForLanguage;
		preview_sherry.language.selectedIndex = dataForLanguage;
		preview_hanna.language.selectedIndex = dataForLanguage;
		btn_buy.title = 1007.GetLocal(UIStringType.GUI);
	}

	public void OnShow()
	{
	}

	private void RefreshHeroAdvertInfo(int index, GButton previewButton)
	{
		(int heroId, RepeatedField<int> photoId, RepeatedField<int> labelId) tuple = RefreshAdvertInfo(index);
		int item = tuple.heroId;
		RepeatedField<int> item2 = tuple.photoId;
		RepeatedField<int> item3 = tuple.labelId;
		ItemInfoConfigure itemInfoConfigure = GetHeroConfig(item).ItemID.GetItemInfoConfigure();
		BindPreviewButton(previewButton, itemInfoConfigure.Id, item2[0], item3[0]);
	}

	private (int heroId, RepeatedField<int> photoId, RepeatedField<int> labelId) RefreshAdvertInfo(int index)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = index.GetCollaborationGoodsConfigure();
		if (collaborationGoodsConfigure == null)
		{
			return (heroId: 0, photoId: null, labelId: null);
		}
		int item = collaborationGoodsConfigure.HeroID[0];
		RepeatedField<int> playerPhotoID = collaborationGoodsConfigure.PlayerPhotoID;
		RepeatedField<int> accountBackgroundID = collaborationGoodsConfigure.AccountBackgroundID;
		return (heroId: item, photoId: playerPhotoID, labelId: accountBackgroundID);
	}

	private SkinStandingPaintingConfigureItem GetHeroConfig(int heroId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroId, 0, 0);
	}

	public void AddEvent()
	{
		btn_buy.onClick.Add(OnBtnBuyClicked);
	}

	public void RemoveEvent()
	{
		btn_buy.onClick.Remove(OnBtnBuyClicked);
	}

	private void OnBtnBuyClicked()
	{
		BuyAction?.Invoke();
	}

	private void BindPreviewButton(GButton previewButton, int skinId, int photoId, int labelId)
	{
		previewButton.onClick.Set((EventCallback0)delegate
		{
			PreviewSkin(previewButton, skinId, photoId, labelId);
		});
	}

	private async void PreviewSkin(GButton btnPreview, int SkinId, int photoId, int labelId)
	{
		btnPreview.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(SkinId, labelId, photoId);
		btnPreview.onClick.Release();
	}

	public static UIMGWT_Com_Advent CreateInstance()
	{
		return (UIMGWT_Com_Advent)UIPackage.CreateObject("MGWTStore", "MGWT_Com_Advent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_sherry = (GLoader)GetChildAt(0);
		loader_hanna = (GLoader)GetChildAt(1);
		preview_sherry = (UIMGWT_Preview_Button)GetChildAt(3);
		preview_hanna = (UIMGWT_Preview_Button)GetChildAt(6);
		btn_buy = (GButton)GetChildAt(14);
		Cut_in = GetTransitionAt(0);
	}
}
