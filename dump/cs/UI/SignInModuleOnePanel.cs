using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class SignInModuleOnePanel : BasePanel<UISignInModuleOnePanel>
{
	private const int CharacterId = 101;

	private const int SkinId = 100101004;

	private UISignInModuleOne_Button_Reward[] _dayButtons;

	private int _activityId;

	private SignInInfoConfigure signInConfigure;

	private SignInDataConfigure _signInDataConfigure;

	private const int ProgressMax = 140;

	private readonly int[] ProgressValues = new int[7] { 8, 28, 49, 70, 91, 112, 132 };

	public SignInModuleOnePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UISignInModuleOnePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs == null || objs.Length == 0)
		{
			if (_activityId <= 0)
			{
				return;
			}
		}
		else if (objs[0] is RepeatedField<int> { Count: >0 } repeatedField)
		{
			_activityId = repeatedField[0];
		}
		_dayButtons = new UISignInModuleOne_Button_Reward[7]
		{
			base.ui.day1,
			base.ui.day2,
			base.ui.day3,
			base.ui.day4,
			base.ui.day5,
			base.ui.day6,
			base.ui.day7
		};
		int signInByActivityId = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInByActivityId(_activityId);
		if (!StaticConfigure.SignIn.InfoDict.TryGetValue(signInByActivityId, out signInConfigure))
		{
			Debug.LogError($"无法通关跳转的ID:{signInByActivityId}, 找到对应的签到配置");
		}
		else if (!StaticConfigure.SignIn.DataDict.TryGetValue(signInConfigure.SigninRewardID, out _signInDataConfigure))
		{
			Debug.LogError($"无法通关奖励ID:{signInConfigure.SigninRewardID}, 找到对应的配置");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.loader_BG.MallScreen();
		if (signInConfigure != null)
		{
			RefreshCharacter();
			base.ui.txt_Duration.text = TimeHelper.GetDurationText(signInConfigure.BeginTime, signInConfigure.EndTime, OnlyDuration: true);
			base.ui.txt_Explain.text = 1060001.GetLocal(UIStringType.GUI);
			base.ui.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
			RefreshDayButtons();
		}
	}

	private async void RefreshCharacter()
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(101, 100101004, 0);
		if (configStandingPainting != null)
		{
			string item = configStandingPainting.GetCharacter().Item1;
			Color32 characterColor = new Color32(byte.MaxValue, 218, 156, byte.MaxValue);
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(item, delegate(NTexture texture)
			{
				base.ui.loader_Character.texture = texture;
			}, null);
			if (base.ui.loader_Character.texture != null)
			{
				GLoader loader_Character_Color = base.ui.loader_Character_Color;
				loader_Character_Color.texture = await ShadowTextureCreator.CreateShadowTexture(base.ui.loader_Character.texture, characterColor, 1f, 0f, 0);
			}
		}
	}

	private void RefreshDayButtons()
	{
		for (int i = 0; i < _dayButtons.Length; i++)
		{
			KeyValuePair<int, int> kvp = _signInDataConfigure.SignInDataConfigureItems[i].Reward.First();
			ItemInfoConfigure itemInfoConfigure = kvp.Key.GetItemInfoConfigure();
			UISignInModuleOne_Button_Reward btn = _dayButtons[i];
			btn.icon1.url = itemInfoConfigure.ShowIcon;
			btn.txt_number.text = $"x{kvp.Value}";
			int index = i;
			btn.onClick.Set((EventCallback0)delegate
			{
				SignInLogic signIn = SimpleSingletonProvider<GameLogicManager>.inst.signIn;
				int? num = signIn?.GetSignInCount(signInConfigure.Id);
				if (num.HasValue)
				{
					if (index > num || !signIn.CanSignIn(signInConfigure.Id))
					{
						SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
					}
					else
					{
						btn.onClick.Retain();
						signIn.SignIn(signInConfigure.Id).OnFinishedOnly.AddOnce(delegate
						{
							RefreshSignInState();
							SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(_activityId);
							btn.onClick.Release();
						});
					}
				}
			});
		}
		RefreshSignInState();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.ui.loader_Character_Color.texture?.Dispose();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void SetState(UISignInModuleOne_Button_Reward button, SignInStatus status)
	{
		if (button != null && button.get != null)
		{
			switch (status)
			{
			case SignInStatus.NotReach:
				button.get.selectedIndex = 0;
				button.text_yes.visible = false;
				button.text_no.visible = false;
				break;
			case SignInStatus.Completed:
				button.get.selectedIndex = 1;
				button.text_yes.visible = true;
				button.text_no.visible = false;
				break;
			case SignInStatus.Reach:
				button.get.selectedIndex = 2;
				button.text_yes.visible = false;
				button.text_no.visible = true;
				break;
			}
		}
	}

	private void RefreshSignInState()
	{
		SignInLogic signIn = SimpleSingletonProvider<GameLogicManager>.inst.signIn;
		int? num = signIn?.GetSignInCount(signInConfigure.Id);
		if (!num.HasValue)
		{
			return;
		}
		base.ui.Progress_SignIn.max = 140.0;
		for (int i = 0; i < _dayButtons.Length; i++)
		{
			if (i < num.Value)
			{
				SetState(_dayButtons[i], SignInStatus.Completed);
				base.ui.Progress_SignIn.value = ProgressValues[i];
			}
			else if (i == num.Value)
			{
				if (signIn.CanSignIn(signInConfigure.Id) && signIn.GetCanSignInToday(signInConfigure.Id))
				{
					SetState(_dayButtons[i], SignInStatus.Reach);
					base.ui.Progress_SignIn.value = ProgressValues[i];
				}
				else
				{
					SetState(_dayButtons[i], SignInStatus.NotReach);
					base.ui.Progress_SignIn.value = ProgressValues.GetSafeByIndex(i - 1);
				}
			}
			else
			{
				SetState(_dayButtons[i], SignInStatus.NotReach);
			}
		}
		double num2 = (double)(base.ui.Progress_SignIn.width - base.ui.Progress_SignIn.BarStartX * 2f) * base.ui.Progress_SignIn.value / 140.0;
		base.ui.Progress_SignIn.arrow.x = base.ui.Progress_SignIn.BarStartX - base.ui.Progress_SignIn.arrow.width * 0.5f + (float)num2;
		if (num.Value == _dayButtons.Length)
		{
			base.ui.Progress_SignIn.value = 140.0;
			base.ui.Progress_SignIn.arrow.visible = false;
		}
		else
		{
			base.ui.Progress_SignIn.arrow.visible = true;
		}
	}
}
