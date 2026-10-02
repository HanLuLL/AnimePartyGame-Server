using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using party.protocol;

namespace UI;

public class CreditWarningWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private int[] singlePlayerMsgIndices = new int[4] { 100050, 100060, 100070, 100080 };

	private int[] multiPlayerMsgIndices = new int[4] { 100010, 100020, 100030, 100040 };

	private Vector2Int[] warningScoreRanges = new Vector2Int[4]
	{
		new Vector2Int(80, 89),
		new Vector2Int(70, 79),
		new Vector2Int(60, 69),
		new Vector2Int(0, 59)
	};

	private long matchPunishmentTime;

	private bool isInvokeDelayClose;

	private bool isExemptedTip;

	private MatchData matchData => SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;

	private int maxScore => 100;

	private int minScore => 0;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public CreditWarningWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UICreditWarningWindow.CreateInstance();
		base.OnInit();
	}

	private int GetWariningScoreIndex(int score)
	{
		int result = -1;
		int num = warningScoreRanges.Length;
		for (int i = 0; i < num; i++)
		{
			Vector2Int vector2Int = warningScoreRanges[i];
			if (score >= vector2Int.x && score <= vector2Int.y)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public async UniTask ShowExemptedTip()
	{
		if (!GameSettings.IsShowExemptedTip)
		{
			return;
		}
		GameSettings.UpdateExemptedTipTime();
		isExemptedTip = true;
		await TryShowAsync();
		if (base.contentPane is UICreditWarningWindow uICreditWarningWindow)
		{
			uICreditWarningWindow.state.selectedIndex = 1;
			uICreditWarningWindow.txt_title.text = 2001.GetLocal(UIStringType.Match);
			string format = 2002.GetLocal(UIStringType.Match);
			int num = -1;
			int exemptedActionType = GameSettings.ExemptedActionType;
			if (StaticConfigure.Match.CreditActionDict.TryGetValue(exemptedActionType, out var value))
			{
				num = value.ActionName;
			}
			if (num >= 0)
			{
				format = string.Format(format, num.GetLocal(UIStringType.Match));
			}
			uICreditWarningWindow.txt_desc_2.text = format;
		}
	}

	public async UniTask ShowCreditWarning(MatchPunishmentS2C model)
	{
		isInvokeDelayClose = false;
		matchPunishmentTime = model.MatchPunishmentTime;
		isExemptedTip = false;
		await TryShowAsync();
		if (!(base.contentPane is UICreditWarningWindow uICreditWarningWindow))
		{
			return;
		}
		uICreditWarningWindow.state.selectedIndex = 0;
		List<RoomPlayer> teamPlayers = matchData.TeamPlayers;
		bool flag = teamPlayers.Count == 1;
		long playerId = model.PlayerId;
		int num = Mathf.Clamp(maxScore - model.DeductScore, minScore, maxScore);
		int wariningScoreIndex = GetWariningScoreIndex(num);
		bool flag2 = false;
		if (wariningScoreIndex >= 0)
		{
			if (flag)
			{
				int num2 = singlePlayerMsgIndices[wariningScoreIndex];
				uICreditWarningWindow.txt_desc.text = num2.GetLocal(UIStringType.Match);
			}
			else
			{
				RoomPlayer roomPlayer = teamPlayers.Single((RoomPlayer x) => x.Id == playerId);
				if (roomPlayer == null)
				{
					flag2 = true;
					Debug.LogError($"服务器下发的 playerID:{playerId} 不在房间中");
				}
				else
				{
					int num3 = multiPlayerMsgIndices[wariningScoreIndex];
					string text = string.Format(arg0: roomPlayer.serverPlayer.Nick, format: num3.GetLocal(UIStringType.Match));
					uICreditWarningWindow.txt_desc.text = text;
				}
			}
		}
		else
		{
			flag2 = true;
			Debug.LogError($"服务器下发的违规分数信息有误 playerID:{playerId} score:{num}");
		}
		if (flag2)
		{
			HideImmediately();
		}
	}

	private async UniTask DelayHide(int frame)
	{
		if (!isInvokeDelayClose)
		{
			isInvokeDelayClose = true;
			await UniTask.Delay(frame);
			HideImmediately();
		}
	}

	protected override void OnUpdate()
	{
		if (base.contentPane is UICreditWarningWindow uICreditWarningWindow && !isExemptedTip)
		{
			base.OnUpdate();
			int num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
			long num2 = matchPunishmentTime - num;
			if (num2 <= 0)
			{
				DelayHide(1).Forget();
				return;
			}
			long num3 = num2 / 3600;
			long num4 = num2 % 3600 / 60;
			long num5 = num2 % 60;
			string text = $"{num3:D2}:{num4:D2}:{num5:D2}";
			uICreditWarningWindow.txt_time.text = text;
		}
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UICreditWarningWindow uICreditWarningWindow)
		{
			blurBgCtrl.OnShown(base.BgLoader);
			base.BgLoader.color = new Color(0.6f, 0.6f, 0.6f);
			uICreditWarningWindow.btn_ok.onClick.Add(OnButtonOKClick);
			if (!isExemptedTip)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.AddListener(OnStartMatching);
			}
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UICreditWarningWindow uICreditWarningWindow)
		{
			uICreditWarningWindow.btn_ok.onClick.Remove(OnButtonOKClick);
			blurBgCtrl.OnHide();
			if (!isExemptedTip)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.RemoveListener(OnStartMatching);
			}
		}
	}

	private void OnStartMatching()
	{
		HideImmediately();
	}

	private void OnButtonOKClick()
	{
		Hide();
	}
}
