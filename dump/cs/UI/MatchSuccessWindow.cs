using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class MatchSuccessWindow : BaseWindow
{
	public MatchSuccessWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIMatchSuccessWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async UniTask ShowMatchResult()
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIMatchSuccessWindow win = gComponent as UIMatchSuccessWindow;
		if (win == null)
		{
			return;
		}
		SimpleSingletonProvider<UIManager>.inst.messageBox.Hide();
		win.txt_Theme.text = 1020014.GetLocal(UIStringType.GUI);
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			RefreshPlayerLabel(win.com_Player_1, (curRoomInfo.Players.Count > 0) ? curRoomInfo.Players.GetSafeByIndex(0) : null);
			RefreshPlayerLabel(win.com_Player_2, (curRoomInfo.Players.Count > 1) ? curRoomInfo.Players.GetSafeByIndex(1) : null);
			RefreshPlayerLabel(win.com_Player_3, (curRoomInfo.Players.Count > 2) ? curRoomInfo.Players.GetSafeByIndex(2) : null);
			RefreshPlayerLabel(win.com_Player_4, (curRoomInfo.Players.Count > 3) ? curRoomInfo.Players.GetSafeByIndex(3) : null);
			if (StaticConfigure.GameMode.InfoDict.TryGetValue(curRoomInfo.MapType, out var value))
			{
				win.txt_ModeName.text = value.NameID.GetLocal(UIStringType.GameMode);
			}
			string text = "";
			if (curRoomInfo.IsPVE() && StaticConfigure.ChoosingTimeLimit.DifficultyDict.TryGetValue(curRoomInfo.Difficulty, out var value2))
			{
				text = value2.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit);
			}
			if (StaticConfigure.Map.InfoDict.TryGetValue(curRoomInfo.MapId, out var value3))
			{
				win.com_Map.loader_Map.url = value3.MapSceneImage;
				win.txt_MapName.text = value3.MapName.GetLocal(UIStringType.Map) + "·" + text;
			}
			win.visible = true;
			win.btn_StartGame.onClick.Set(SimpleSingletonProvider<GameLogicManager>.inst.match.StopTimer);
			string tipTxt = 1020004.GetLocal(UIStringType.GUI);
			await SimpleSingletonProvider<GameLogicManager>.inst.match.StartTimer(5f, SimpleSingletonProvider<GameLogicManager>.inst.match.StopTimer, delegate(float t)
			{
				string arg = (5f - t).ToString("f0");
				win.txt_Time.text = string.Format(tipTxt, arg);
			});
			Hide();
		}
	}

	private void RefreshPlayerLabel(GComponent com_PlayerLabel, RoomPlayer playerData)
	{
		if (playerData == null)
		{
			com_PlayerLabel.visible = false;
			return;
		}
		com_PlayerLabel.visible = true;
		UICom_PlayerLabel com_Label = (UICom_PlayerLabel)com_PlayerLabel;
		CommonUIManager.RendererLabelInfo(com_Label, playerData.GetNick(showRemark: true), playerData.Level);
		(string, bool) tuple = playerData.AccountBackgroundURL();
		CommonUIManager.RendererLabel(UIType.Panel, 28, com_Label, tuple.Item1, tuple.Item2);
		string headShot = playerData.HeadURL();
		CommonUIManager.RendererHeadShot(com_Label, headShot, isVideo: false);
	}
}
