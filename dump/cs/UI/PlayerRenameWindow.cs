using System.Text.RegularExpressions;
using Core;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace UI;

public class PlayerRenameWindow : BaseWindow
{
	private readonly Regex _regex = new Regex("[^a-zA-Z0-9\\u4e00-\\u9fa5\\u3040-\\u309F\\u30A0-\\u30FF\\u31F0-\\u31FF-_]");

	private UIPlayerRenameWindow _win;

	private UniTaskCompletionSource _unitask;

	private bool _intention;

	private bool _UpdFriendRmk;

	private long _FriendId;

	private bool _FocusIn;

	public PlayerRenameWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		_win = UIPlayerRenameWindow.CreateInstance();
		base.contentPane = _win;
		base.OnInit();
		_win.txt_input.singleLine = true;
	}

	protected override void OnShown()
	{
		base.OnShown();
		_win.Cut_in.Play();
		SetTitleText();
		_win.btn_confirm.onClick.Add(OnConfirm);
		_win.btn_close.onClick.Add(OnClose);
		_win.txt_input.onFocusIn.Add(onFocusInTxtInput);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.AddListener(OnChangeNameSuccess);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.AddListener(OnChangeFriendNoteSuccess);
		_win.btn_close.visible = _intention || _UpdFriendRmk;
	}

	private void SetTitleText()
	{
		if (_intention)
		{
			_win.txt_title.text = 1004.GetLocal(UIStringType.GUI) + "\n" + 1005.GetLocal(UIStringType.GUI);
			_win.txt_input.promptText = 26.GetLocal(UIStringType.Message);
		}
		else
		{
			_win.txt_title.text = 1004.GetLocal(UIStringType.GUI);
			_win.txt_input.promptText = 26.GetLocal(UIStringType.Message);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		_unitask = null;
		_UpdFriendRmk = false;
		_intention = false;
		_win.btn_close.visible = false;
		_win.txt_input.text = "";
		_win.txt_tips.text = "";
		_FocusIn = false;
		_FriendId = 0L;
		_win.btn_confirm.onClick.Remove(OnConfirm);
		_win.txt_input.onFocusIn.Remove(onFocusInTxtInput);
		_win.btn_close.onClick.Remove(OnClose);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.RemoveListener(OnChangeNameSuccess);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.RemoveListener(OnChangeFriendNoteSuccess);
	}

	private void OnInputChanged()
	{
		string input = _win.txt_input.text;
		Verify(input).Forget();
	}

	private void onFocusInTxtInput()
	{
		_FocusIn = true;
	}

	private async void OnConfirm()
	{
		string inputText = _win.txt_input.text;
		if (!(await Verify(inputText)))
		{
			return;
		}
		if (_UpdFriendRmk && _FriendId != 0L)
		{
			if (!_FocusIn)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.Dispatch(t: true);
				return;
			}
			FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(_FriendId);
			if (friendData == null)
			{
				return;
			}
			if (!(await SensitiveWords.Valid(inputText + friendData.Nick)))
			{
				_win.txt_tips.text = 35.GetLocal(UIStringType.Message);
				return;
			}
			_win.btn_confirm.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.friend.SetFriendNoteC2S(_FriendId, inputText).OnFinishedOnly.AddOnce(delegate
			{
				_win.btn_confirm.onClick.Release();
			});
		}
		else if (_intention)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(33.GetLocal(UIStringType.Message) + "\r\n " + inputText, delegate
			{
				_win.btn_confirm.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.account.ChangeName(inputText).OnFinishedOnly.AddOnce(delegate
				{
					_win.btn_confirm.onClick.Release();
				});
			}).Forget();
		}
		else
		{
			_win.btn_confirm.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.ChangeName(inputText).OnFinishedOnly.AddOnce(delegate
			{
				_win.btn_confirm.onClick.Release();
			});
		}
	}

	private void OnChangeNameSuccess(bool result)
	{
		if (result && !_intention)
		{
			RoLeInfoBase.uploadCreateRoleToSdk();
		}
		_unitask?.TrySetResult();
	}

	private void OnChangeFriendNoteSuccess(bool result)
	{
		_unitask?.TrySetResult();
	}

	public void OnClose()
	{
		_win.btn_close.onClick.Retain();
		StaticConfigure.UnloadSensitiveWords();
		Hide();
		_win.btn_close.onClick.Release();
	}

	public async UniTask Display(bool intention = false)
	{
		_intention = intention;
		await TryShowAsync();
		_unitask = new UniTaskCompletionSource();
		await _unitask.Task;
		Hide();
	}

	public async UniTask ChangeFriendRemark(bool UpdFriendRmk = false, long playerId = 0L)
	{
		_UpdFriendRmk = UpdFriendRmk;
		_FriendId = playerId;
		if (SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(playerId) != null)
		{
			await TryShowAsync();
			if (_UpdFriendRmk)
			{
				_win.txt_title.text = 1006.GetLocal(UIStringType.GUI);
				string friendNote = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendNote(playerId);
				_win.txt_input.promptText = ((!string.IsNullOrEmpty(friendNote)) ? friendNote : ("[color=#999999]" + 1123.GetLocal(UIStringType.Message) + "[/color]"));
			}
			_unitask = new UniTaskCompletionSource();
			await _unitask.Task;
			Hide();
		}
	}

	private async UniTask TryShowAsync()
	{
		await StaticConfigure.LoadSensitiveWords();
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private async UniTask<bool> Verify(string input)
	{
		if (_UpdFriendRmk)
		{
			if (input.Length > 6)
			{
				_win.txt_tips.text = 1123.GetLocal(UIStringType.Message);
				return false;
			}
		}
		else
		{
			if (input.Length > 18)
			{
				_win.txt_tips.text = 17.GetLocal(UIStringType.Message);
				return false;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Nick == input)
			{
				_win.txt_tips.text = 29.GetLocal(UIStringType.Message);
				return false;
			}
			if (string.IsNullOrEmpty(input))
			{
				_win.txt_tips.text = 16.GetLocal(UIStringType.Message);
				return false;
			}
		}
		if (_regex.IsMatch(input))
		{
			_win.txt_tips.text = 18.GetLocal(UIStringType.Message);
			return false;
		}
		if (!(await SensitiveWords.Valid(input)))
		{
			_win.txt_tips.text = 19.GetLocal(UIStringType.Message);
			return false;
		}
		_win.txt_tips.text = "";
		return true;
	}
}
