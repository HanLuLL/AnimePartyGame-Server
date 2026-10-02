using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIBattleInfo_Com_LuckyStar : GComponent
{
	private LuckyStarBattleParamConfigure _luckyStarBattleParam;

	private int _targetStarCount;

	public GTextField txt_GreenScore;

	public GTextField txt_RedScore;

	public GButton btn_OpenTask;

	public Transition Cut_in;

	public Transition GreenLevelUp;

	public Transition RedLevelUp;

	public const string URL = "ui://fxejlqlfu5uzbs";

	public void InitComponents()
	{
		_luckyStarBattleParam = StaticConfigure.LuckyStarBattle.Params.GetSafeByIndex(0);
		if (_luckyStarBattleParam != null)
		{
			_targetStarCount = _luckyStarBattleParam.NeedLuckyStar;
		}
	}

	public void Refresh()
	{
		RefreshLuckyStar(0, change: false);
	}

	public void AddEvent()
	{
		btn_OpenTask.onClick.Add(OpenLuckyStarTask);
	}

	public void RemoveEvent()
	{
		btn_OpenTask.onClick.Remove(OpenLuckyStarTask);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.luckyStar.AddListener(RefreshLuckyStar);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.luckyStar.RemoveListener(RefreshLuckyStar);
	}

	private void RefreshLuckyStar(int teamId, bool change)
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return;
		}
		if (curRoomInfo.GameLuckyStarDict.TryGetValue(BattleConfig.LuckyStarRedTeamId, out var value))
		{
			txt_RedScore.text = $"[size=110]{value}[/size]/{_targetStarCount}";
			if (teamId == BattleConfig.LuckyStarRedTeamId)
			{
				RedLevelUp.Play();
			}
		}
		else
		{
			txt_RedScore.text = $"[size=110]0[/size]/{_targetStarCount}";
		}
		if (curRoomInfo.GameLuckyStarDict.TryGetValue(BattleConfig.LuckyStarGreenTeamId, out var value2))
		{
			txt_GreenScore.text = $"[size=110]{value2}[/size]/{_targetStarCount}";
			if (teamId == BattleConfig.LuckyStarGreenTeamId)
			{
				GreenLevelUp.Play();
			}
		}
		else
		{
			txt_GreenScore.text = $"[size=110]0[/size]/{_targetStarCount}";
		}
	}

	private async void OpenLuckyStarTask(EventContext context)
	{
		btn_OpenTask.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.LuckyStarMission.TryOpenRoomMission();
		btn_OpenTask.onClick.Release();
	}

	public void Close()
	{
	}

	public static UIBattleInfo_Com_LuckyStar CreateInstance()
	{
		return (UIBattleInfo_Com_LuckyStar)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_LuckyStar");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_GreenScore = (GTextField)GetChildAt(4);
		txt_RedScore = (GTextField)GetChildAt(5);
		btn_OpenTask = (GButton)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
		GreenLevelUp = GetTransitionAt(1);
		RedLevelUp = GetTransitionAt(2);
	}
}
