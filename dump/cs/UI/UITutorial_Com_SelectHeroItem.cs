using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UITutorial_Com_SelectHeroItem : GComponent
{
	public HeroCardData Info;

	public bool selected;

	private uint _skinVoicePlayId;

	public Controller p;

	public Controller stateChange;

	public Controller Sure;

	public GGraph zhezhao;

	public GLoader load_Character;

	public GLoader load_SelectedCharacter;

	public UITutorial_Com_CharacterName com_CharacterName;

	public UITutorial_CharacterDi com_selectedaMovie;

	public GTextField txt_Trial;

	public Transition xuanting;

	public Transition queding;

	public Transition xuanze;

	public Transition OverSfx;

	public Transition DownSfx;

	public const string URL = "ui://b96qpoz6ia9ga";

	public void InitData(HeroCardData info)
	{
		if (info != null)
		{
			Info = info;
			Sure.selectedIndex = 0;
			load_Character.url = info.standingPainting.GetCharacterThin();
			load_SelectedCharacter.url = info.standingPainting.GetCharacterThin();
			com_CharacterName.txt_Title.TryScrollTextField(CharacterHandle.GetCharacterNickName(Info.HeroId));
			txt_Trial.text = 1000008.GetLocal(UIStringType.GUI);
			txt_Trial.visible = info.heroStatus != HeroStatus.Activate;
			base.visible = true;
		}
		else
		{
			load_Character.url = "";
			load_SelectedCharacter.url = "";
			com_CharacterName.txt_Title.TryScrollTextField("");
			base.visible = false;
		}
		base.onRollOut.Set((EventCallback0)delegate
		{
			if (!selected)
			{
				stateChange.selectedIndex = 0;
				xuanting.Stop();
			}
		});
		base.onRollOver.Set((EventCallback0)delegate
		{
			stateChange.selectedIndex = 1;
			xuanting.Play();
			OverSfx.Play();
		});
	}

	public void PlaySkinVoice()
	{
		int voice = Info.standingPainting.Voice;
		if (voice != 0)
		{
			CharacterVoiceConfigure voiceConfigure = voice.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				_skinVoicePlayId = SimpleSingletonProvider<AudioManager>.inst.SendEvent(voiceConfigure.FanfareVoice, Stage.inst.gameObject);
			}
		}
	}

	public void StopSkinVoice()
	{
		if (_skinVoicePlayId != 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(_skinVoicePlayId);
		}
	}

	public static UITutorial_Com_SelectHeroItem CreateInstance()
	{
		return (UITutorial_Com_SelectHeroItem)UIPackage.CreateObject("Tutorial", "Tutorial_Com_SelectHeroItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		stateChange = GetControllerAt(1);
		Sure = GetControllerAt(2);
		zhezhao = (GGraph)GetChildAt(0);
		load_Character = (GLoader)GetChildAt(3);
		load_SelectedCharacter = (GLoader)GetChildAt(4);
		com_CharacterName = (UITutorial_Com_CharacterName)GetChildAt(7);
		com_selectedaMovie = (UITutorial_CharacterDi)GetChildAt(14);
		txt_Trial = (GTextField)GetChildAt(15);
		xuanting = GetTransitionAt(0);
		queding = GetTransitionAt(1);
		xuanze = GetTransitionAt(2);
		OverSfx = GetTransitionAt(3);
		DownSfx = GetTransitionAt(4);
	}
}
