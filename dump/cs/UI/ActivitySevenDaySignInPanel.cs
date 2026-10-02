using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivitySevenDaySignInPanel : BasePanel<UIActivitySevenDaySignInPanel>
{
	private enum Status
	{
		NotReach,
		Completed,
		Reach
	}

	private UIActivitySevenDaySignIn_Btn[] _dayButtons;

	private SignInDataConfigure _signInDataConfigure;

	private readonly float[] _progressBarXPositions = new float[7] { 67f, 281f, 496f, 712f, 935f, 1160f, 1375f };

	private const float BAR_LENGTH = 1500f;

	private int _activityId;

	private int SignInGroupId => SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInByActivityId(_activityId);

	public ActivitySevenDaySignInPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivitySevenDaySignInPanel.CreateInstance();
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
		int signInGroupId = SignInGroupId;
		if (!StaticConfigure.SignIn.InfoDict.TryGetValue(signInGroupId, out var value) || !StaticConfigure.SignIn.DataDict.TryGetValue(value.SigninRewardID, out _signInDataConfigure) || _signInDataConfigure.SignInDataConfigureItems.Count < 7)
		{
			return;
		}
		base.ui.Cut_in.Play();
		base.ui.txt_duration.text = TimeHelper.GetDurationText(value.BeginTime, value.EndTime, OnlyDuration: true);
		_dayButtons = new UIActivitySevenDaySignIn_Btn[7]
		{
			base.ui.day1,
			base.ui.day2,
			base.ui.day3,
			base.ui.day4,
			base.ui.day5,
			base.ui.day6,
			base.ui.day7
		};
		for (int i = 0; i < _dayButtons.Length; i++)
		{
			UIActivitySevenDaySignIn_Btn uIActivitySevenDaySignIn_Btn = _dayButtons[i];
			if (uIActivitySevenDaySignIn_Btn != null)
			{
				KeyValuePair<int, int> keyValuePair = _signInDataConfigure.SignInDataConfigureItems[i].Reward.First();
				ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
				uIActivitySevenDaySignIn_Btn.icon1.url = itemInfoConfigure.ShowIcon;
				uIActivitySevenDaySignIn_Btn.txt_number.text = "x" + keyValuePair.Value;
			}
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.Cut_in.Play();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.bg.MallScreen();
	}

	public override void Refresh()
	{
		base.Refresh();
		if (_dayButtons != null && _activityId > 0)
		{
			UpdateSignInData();
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		if (_dayButtons == null || _signInDataConfigure == null)
		{
			return;
		}
		for (int i = 0; i < _dayButtons.Length; i++)
		{
			if (_dayButtons[i] == null)
			{
				continue;
			}
			KeyValuePair<int, int> kvp = _signInDataConfigure.SignInDataConfigureItems[i].Reward.First();
			int index = i;
			int currentActivityId = _activityId;
			_dayButtons[i].onClick.Set((EventCallback0)delegate
			{
				if (currentActivityId == _activityId)
				{
					int signInGroupId = SignInGroupId;
					int? signInCount = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInCount(signInGroupId);
					if (signInCount.HasValue)
					{
						if (index < signInCount.Value)
						{
							SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
							return;
						}
						if (index > signInCount.Value)
						{
							SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
							return;
						}
					}
					else if (index != 0)
					{
						SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
						return;
					}
					if (SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(signInGroupId) && SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetCanSignInToday(signInGroupId))
					{
						SimpleSingletonProvider<GameLogicManager>.inst.signIn.SignIn(signInGroupId).OnFinishedOnly.AddOnce(delegate
						{
							if (currentActivityId == _activityId)
							{
								UpdateSignInData();
							}
						});
					}
					else
					{
						SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
					}
				}
			});
		}
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		if (_dayButtons != null)
		{
			for (int i = 0; i < _dayButtons.Length; i++)
			{
				_dayButtons[i].onClick.Clear();
			}
		}
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
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void UpdateSignInData()
	{
		if (_dayButtons == null || _activityId <= 0)
		{
			return;
		}
		int signInGroupId = SignInGroupId;
		if (!StaticConfigure.SignIn.InfoDict.ContainsKey(signInGroupId))
		{
			return;
		}
		int? signInCount = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInCount(signInGroupId);
		if (!signInCount.HasValue)
		{
			UpdateProgressBarValue(0);
			if (SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(signInGroupId) && SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetCanSignInToday(signInGroupId))
			{
				SetState(_dayButtons[0], Status.Reach);
			}
			else
			{
				SetState(_dayButtons[0], Status.NotReach);
			}
			for (int i = 1; i < _dayButtons.Length; i++)
			{
				SetState(_dayButtons[i], Status.NotReach);
			}
			return;
		}
		for (int j = 0; j < _dayButtons.Length; j++)
		{
			if (j < signInCount.Value)
			{
				SetState(_dayButtons[j], Status.Completed);
				UpdateProgressBarValue(j);
			}
			else if (j == signInCount.Value)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(signInGroupId) && SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetCanSignInToday(signInGroupId))
				{
					SetState(_dayButtons[j], Status.Reach);
					UpdateProgressBarValue(j);
				}
				else
				{
					SetState(_dayButtons[j], Status.NotReach);
					UpdateProgressBarValue(Mathf.Max(0, j - 1));
				}
			}
			else
			{
				SetState(_dayButtons[j], Status.NotReach);
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(_activityId);
	}

	private void SetState(UIActivitySevenDaySignIn_Btn button, Status status)
	{
		if (button != null && button.get != null)
		{
			switch (status)
			{
			case Status.NotReach:
				button.get.selectedIndex = 0;
				button.text_yes.visible = false;
				button.text_no.visible = false;
				break;
			case Status.Completed:
				button.get.selectedIndex = 1;
				button.text_yes.visible = true;
				button.text_no.visible = false;
				break;
			case Status.Reach:
				button.get.selectedIndex = 2;
				button.text_yes.visible = false;
				button.text_no.visible = true;
				break;
			}
		}
	}

	private void UpdateProgressBarValue(int dayIndex)
	{
		if (base.ui.ActivitySevenDaySignIn_Progress == null)
		{
			return;
		}
		GGroup arrow = base.ui.ActivitySevenDaySignIn_Progress.arrow;
		if (arrow != null && dayIndex >= 0 && dayIndex < _progressBarXPositions.Length)
		{
			arrow.x = _progressBarXPositions[dayIndex];
			if (dayIndex == _progressBarXPositions.Length - 1)
			{
				base.ui.ActivitySevenDaySignIn_Progress.value = 100.0;
			}
			else
			{
				base.ui.ActivitySevenDaySignIn_Progress.value = (_progressBarXPositions[dayIndex] + 20f) / 1500f * 100f;
			}
		}
	}
}
