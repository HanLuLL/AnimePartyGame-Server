using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class SignInWindow : BaseWindow
{
	private enum Status
	{
		NotReach,
		Completed,
		Reach
	}

	private const int MAX = 7;

	private int _id;

	private SignInDataConfigure _signInDataConfigure;

	private readonly List<GButton> _btnList = new List<GButton>(7);

	private UISignInWindow window;

	public SignInWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		isAdapter = true;
		base.contentPane = UISignInWindow.CreateInstance();
		window = (UISignInWindow)base.contentPane;
		base.OnInit();
		_btnList.Add(window.btn_day1);
		_btnList.Add(window.btn_day2);
		_btnList.Add(window.btn_day3);
		_btnList.Add(window.btn_day4);
		_btnList.Add(window.btn_day5);
		_btnList.Add(window.btn_day6);
		_btnList.Add(window.btn_day7);
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISignInWindow uISignInWindow)
		{
			uISignInWindow.mohu.SetSize(GRoot.inst.width, GRoot.inst.height);
		}
		AddEvent();
	}

	protected override void OnHide()
	{
		base.OnHide();
		RemoveEvent();
		_id = 0;
		_signInDataConfigure = null;
		SimpleSingletonProvider<GameLogicManager>.inst.home.TriggerMessage();
	}

	private void AddEvent()
	{
		window.btn_back.onClick.Add(OnBack);
	}

	private void RemoveEvent()
	{
		window.btn_back.onClick.Remove(OnBack);
	}

	public async UniTask Open(int signInId)
	{
		_id = signInId;
		if (!StaticConfigure.SignIn.InfoDict.TryGetValue(_id, out var infoConfigure))
		{
			Debug.LogError($"未在Sign-Info表中找到ID={_id}的配置项！");
		}
		else if (!StaticConfigure.SignIn.DataDict.TryGetValue(infoConfigure.SigninRewardID, out _signInDataConfigure))
		{
			Debug.LogError($"未在Sign-Data表中找到ID={infoConfigure.SigninRewardID}的配置项！");
		}
		else
		{
			if (_signInDataConfigure.SignInDataConfigureItems.Count < 7)
			{
				return;
			}
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(infoConfigure.Bg, null, null);
			Show();
			if (!base.initialized)
			{
				await UniTask.WaitUntil(() => base.initialized);
			}
			window.Cut_in.Play();
			window.txt_headline.text = infoConfigure.Headline.GetLocal(UIStringType.SignIn);
			window.txt_desc.text = infoConfigure.Description.GetLocal(UIStringType.SignIn);
			window.txt_duration.text = TimeHelper.GetDurationText(infoConfigure.BeginTime, infoConfigure.EndTime, OnlyDuration: true);
			window.loader_bg.url = infoConfigure.Bg;
			for (int num = 0; num < _btnList.Count; num++)
			{
				if (!(_btnList[num] is UISignIn_Button_SignIn uISignIn_Button_SignIn))
				{
					continue;
				}
				KeyValuePair<int, int> kvp = _signInDataConfigure.SignInDataConfigureItems[num].Reward.First();
				ItemInfoConfigure itemInfoConfigure = kvp.Key.GetItemInfoConfigure();
				uISignIn_Button_SignIn.loader_icon.url = itemInfoConfigure.ShowIcon;
				uISignIn_Button_SignIn.txt_number.text = "x" + kvp.Value;
				int index = num;
				_btnList[num].onClick.Set((EventCallback0)delegate
				{
					int? signInCount = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInCount(_id);
					if (signInCount.HasValue)
					{
						if (index > signInCount)
						{
							SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
						}
						else if (!SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(_id))
						{
							SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(kvp.Key, kvp.Value, _Usable: false).Forget();
						}
						else
						{
							SimpleSingletonProvider<GameLogicManager>.inst.signIn.SignIn(_id).OnFinishedOnly.AddOnce(RefreshSignInState);
						}
					}
				});
			}
			RefreshSignInState();
		}
	}

	private void OnBack()
	{
		SimpleSingletonProvider<UIManager>.inst.signInWindow.Hide();
	}

	private void SetState(GButton gButton, Status status)
	{
		if (gButton is UISignIn_Button_SignIn uISignIn_Button_SignIn)
		{
			switch (status)
			{
			case Status.NotReach:
				uISignIn_Button_SignIn.grayed = true;
				uISignIn_Button_SignIn.get.selectedIndex = 0;
				break;
			case Status.Completed:
				uISignIn_Button_SignIn.grayed = false;
				uISignIn_Button_SignIn.get.selectedIndex = 1;
				break;
			case Status.Reach:
				uISignIn_Button_SignIn.grayed = false;
				uISignIn_Button_SignIn.get.selectedIndex = 2;
				break;
			}
		}
	}

	private void RefreshSignInState()
	{
		int? signInCount = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInCount(_id);
		if (!signInCount.HasValue)
		{
			return;
		}
		for (int i = 0; i < _btnList.Count; i++)
		{
			if (i < signInCount.Value)
			{
				SetState(_btnList[i], Status.Completed);
				window.progress_reward.value = i;
			}
			else if (i == signInCount.Value)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(_id) && SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetCanSignInToday(_id))
				{
					SetState(_btnList[i], Status.Reach);
					window.progress_reward.value = i;
				}
				else
				{
					SetState(_btnList[i], Status.NotReach);
					window.progress_reward.value = i - 1;
				}
			}
			else
			{
				SetState(_btnList[i], Status.NotReach);
			}
		}
	}
}
