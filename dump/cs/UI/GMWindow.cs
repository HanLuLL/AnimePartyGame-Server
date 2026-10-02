using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core;
using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Tutorial;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.GamePlay.Relic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using party.protocol;

namespace UI;

public class GMWindow : BaseWindow
{
	private UIGM_Com_SetAttr btn_GameSpeed;

	private UIGM_Com_SetAttr btn_GameRound;

	private readonly List<TextMeshPro> LandIdTexts = new List<TextMeshPro>();

	private GMFreeCamera gmFreeCamera;

	public GMWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIGMWindow.CreateInstance();
		base.OnInit();
	}

	public override async void Load(UILoadCallback callback)
	{
		try
		{
			await UIPackage.AddPackageFromFileAsync(base.fileName, string.Empty);
			base.loaded = true;
			callback?.Invoke();
		}
		catch (Exception arg)
		{
			Debug.LogError($"GM 包加载失败: {base.fileName}, {arg}");
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		RefreshServerSelect();
		RefreshServerTime();
		win.com_Replay.Refresh();
		win.btn_Close.onClick.Set(base.Hide);
		win.com_Move.txt_Set.text = GMConfig.dev_MovePoint.ToString();
		win.com_Move.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_Move.txt_Set.text, out var result))
			{
				GMConfig.dev_MovePoint = result;
				if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002)
				{
					long playerId = TutorialGame.GetSystem<TutorialPlayerActionFSM>().PlayerId;
					if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("当前行动不是玩家，请等待玩家行动开始再设置");
					}
					else if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round <= 2)
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("新手引导前两个回合是请引导，不能设置移动步数");
					}
					else
					{
						TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SpecifyDicePoint = result;
					}
				}
			}
		});
		win.com_MoveAgain.txt_Set.text = GMConfig.dev_MoveAgainPoint.ToString();
		win.com_MoveAgain.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.dev_MoveAgainPoint = int.Parse(win.com_MoveAgain.txt_Set.text);
		});
		win.com_BombPoint.txt_Set.text = GMConfig.dev_BombPoint.ToString();
		win.com_BombPoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.dev_BombPoint = int.Parse(win.com_BombPoint.txt_Set.text);
		});
		win.com_AtkPoint.txt_Set.text = GMConfig.dev_AttackerPoint.ToString();
		win.com_AtkPoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.dev_AttackerPoint = int.Parse(win.com_AtkPoint.txt_Set.text);
		});
		win.com_DefPoint.txt_Set.text = GMConfig.dev_DefenderPoint.ToString();
		win.com_DefPoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.dev_DefenderPoint = int.Parse(win.com_DefPoint.txt_Set.text);
		});
		win.btn_ClinetAI.selected = GMConfig.ClientCountDown;
		win.btn_ClinetAI.onClick.Set((EventCallback0)delegate
		{
			GMConfig.ClientCountDown = !GMConfig.ClientCountDown;
		});
		win.btn_ClinetAFK.selected = GMConfig.AFKCheck;
		win.btn_ClinetAFK.onClick.Set((EventCallback0)delegate
		{
			GMConfig.AFKCheck = !GMConfig.AFKCheck;
		});
		win.com_HP.txt_Set.text = "0";
		win.com_HP.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.RequestChangeHP(int.Parse(win.com_HP.txt_Set.text));
		});
		win.com_Gold.txt_Set.text = "0";
		win.com_Gold.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.RequestChangeGold(int.Parse(win.com_Gold.txt_Set.text));
		});
		win.com_GameProgress.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_GameProgress.txt_Set.text, out var result))
			{
				GMConfig.RequestChangeGameProgress(result);
				if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002)
				{
					long playerId = TutorialGame.GetSystem<TutorialPlayerActionFSM>().PlayerId;
					if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("当前行动不是玩家，请等待玩家行动开始再设置");
					}
					else
					{
						TutorialGame.GetSystem<TutorialBoardManager>().gameManager.GMSetRound(result);
					}
				}
			}
		});
		win.com_Credit.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_Credit.txt_Set.text, out var result))
			{
				result = Mathf.Clamp(result, 0, 100);
				int num = SimpleSingletonProvider<GameLogicManager>.inst?.account?.CreditScore ?? 100;
				int playerCreditAction = 8;
				if (num != result)
				{
					MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
					{
						IsAll = false,
						PlayerCreditScore = result,
						PlayerCreditAction = playerCreditAction
					});
				}
			}
		});
		win.com_CreditAction.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_CreditAction.txt_Set.text, out var result) && result >= 1 && result <= 7)
			{
				int num = SimpleSingletonProvider<GameLogicManager>.inst?.account?.CreditScore ?? 100;
				if (StaticConfigure.Match.CreditActionDict.TryGetValue(result, out var value))
				{
					int playerCreditScore = Mathf.Clamp(num + value.ScoreChange, 0, 100);
					MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
					{
						IsAll = false,
						PlayerCreditAction = result,
						PlayerCreditScore = playerCreditScore
					});
				}
			}
		});
		win.btn_ResetSkill.onClick.Set((EventCallback0)delegate
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				GMConfig.RequestResetSkill(selfPlayerData.CharacterInst.skill.skillId, selfPlayerData.player.Id);
			}
		});
		win.com_RelicCount.txt_Set.text = "0";
		win.com_RelicCount.btn_Set.onClick.Set((EventCallback0)delegate
		{
			GMConfig.RequestResetRelicCount(int.Parse(win.com_RelicCount.txt_Set.text));
		});
		SingPlay(win);
		RefreshCards();
		RefreshEvent();
		RefreshDestiny();
		RefreshDivination();
		RefreshRelic();
		SetUnlockSportInfo();
		RefreshMapDifficulty();
		win.btn_ReSet.onClick.Set((EventCallback0)delegate
		{
			GMConfig.ResetAllGMParams();
			win.com_Move.txt_Set.text = "0";
			win.com_MoveAgain.txt_Set.text = "0";
			win.com_AtkPoint.txt_Set.text = "0";
			win.com_DefPoint.txt_Set.text = "0";
			win.com_BombPoint.txt_Set.text = "0";
			win.com_HP.txt_Set.text = "0";
			win.com_Gold.txt_Set.text = "0";
		});
		win.btn_Perform.onClick.Set(CallPerform);
		win.btn_SetTerms.onClick.Set(CallSetTrems);
		win.btn_Item.onClick.Set(CallItemChange);
		win.btn_AllItem.onClick.Set(CallAllItemChange);
		win.btn_Quick.onClick.Set((EventCallback0)delegate
		{
			win.txtField_Gacha.text = "1 10000";
		});
		win.btn_Gacha.onClick.Set(CallGacha);
		win.btn_Exp.onClick.Set(CallExp);
		win.btn_AltArtCard.onClick.Set(CallAltArtCard);
		win.btn_GameOver.onClick.Set(CallGameOver);
		win.com_GameAFK.btn_Set.onClick.Set((EventCallback0)delegate
		{
			OperationTimer.SetAtkCount(int.Parse(win.com_GameAFK.txt_Set.text));
		});
		if (btn_GameSpeed == null)
		{
			btn_GameSpeed = UIGM_Com_SetAttr.CreateInstance();
			win.com_GameAFK.parent.AddChild(btn_GameSpeed);
			Vector3 vector = win.com_GameAFK.position;
			vector.y += 142f;
			btn_GameSpeed.position = vector;
			btn_GameSpeed.alpha = 1f;
			btn_GameSpeed.group = win.com_GameAFK.group;
			btn_GameSpeed.text = "游戏速度";
		}
		btn_GameSpeed.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (float.TryParse(btn_GameSpeed.txt_Set.text, out var result) && result >= 0f)
			{
				Time.timeScale = result;
			}
		});
		if (btn_GameRound == null)
		{
			btn_GameRound = UIGM_Com_SetAttr.CreateInstance();
			win.com_GameAFK.parent.AddChild(btn_GameRound);
			Vector3 vector2 = btn_GameSpeed.position;
			vector2.y += 142f;
			btn_GameRound.position = vector2;
			btn_GameRound.alpha = 1f;
			btn_GameRound.group = win.com_GameAFK.group;
			btn_GameRound.text = "轮次进度";
		}
		btn_GameRound.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(btn_GameRound.txt_Set.text, out var result))
			{
				GMConfig.RequestChangeGameRound(result);
			}
		});
		TestFunc();
		RefreshServerInfo();
		win.btn_Together.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.txtField_nodeId.text, out var result))
			{
				if (!SimpleSingletonProvider<LandManager>.inst.NodeDict.ContainsKey(result))
				{
					Debug.LogError($"当前无法找到节点id:{result}");
				}
				else
				{
					GMConfig.GetTogether(result);
				}
			}
			else
			{
				Debug.LogError("需要一个节点ID");
			}
		});
		RefreshNewGM();
		RefreshNovice();
		SetBattleHeroSkin();
		SetLandIdGM();
		SetFreeCameraGM();
	}

	private void RefreshServerTime()
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			uIGMWindow.txt_ServerTimer.text = $"GameTime: {serverTime.Year:D}-{serverTime.Month:D2}-{serverTime.Day:D2} {serverTime.Hour:D2}:{serverTime.Minute:D2}:{serverTime.Second:D2}";
		}
	}

	private void CallExp()
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			uIGMWindow.btn_Exp.onClick.Retain();
			string[] array = uIGMWindow.txtField_Exp.text.Trim().Split(" ");
			if (array.Length == 1 && int.TryParse(array[0], out var result))
			{
				MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
				{
					IsAll = false,
					Exp = result
				});
			}
			uIGMWindow.btn_Exp.onClick.Release();
		}
	}

	private void CallAltArtCard()
	{
		if (!(base.contentPane is UIGMWindow uIGMWindow))
		{
			return;
		}
		uIGMWindow.btn_AltArtCard.onClick.Retain();
		string[] array = uIGMWindow.txtField_AltArtCard.text.Trim().Split(" ");
		if (array.Length == 1 && int.TryParse(array[0], out var result))
		{
			int num = 0;
			foreach (CardAltArtConfigure altArt in StaticConfigure.Card.AltArts)
			{
				foreach (CardAltArtConfigureItem cardAltArtConfigureItem in altArt.CardAltArtConfigureItems)
				{
					if (cardAltArtConfigureItem.AltArtId == result)
					{
						num = altArt.CardId;
						break;
					}
				}
				if (num != 0)
				{
					break;
				}
			}
			if (num != 0)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(result) == 0)
				{
					MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
					{
						IsAll = false,
						ItemId = result,
						ItemCount = 1
					});
				}
				SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.SetCardAltArtC2S(num, result);
			}
		}
		uIGMWindow.btn_AltArtCard.onClick.Release();
	}

	private void RefreshServerInfo()
	{
		if (!(base.contentPane is UIGMWindow uIGMWindow))
		{
			return;
		}
		uIGMWindow.txt_IP.text = $"IP:{GameSettings.IP}:{GameSettings.Port}";
		List<BattlePlayerData> list = SimpleSingletonProvider<GameLogicManager>.inst.battle?.PlayerDatas;
		if (list == null)
		{
			return;
		}
		string text = "";
		foreach (BattlePlayerData item in list)
		{
			text = text + " playerId: " + item.player.Id + " heroID: " + item.player.Hero.HeroId + "\n";
		}
		uIGMWindow.txt_Player.text = string.Format(text);
	}

	private void RefreshCards()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Card != null)
		{
			string[] array = new string[StaticConfigure.Card.Infos.Count];
			for (int i = 0; i < StaticConfigure.Card.Infos.Count; i++)
			{
				CardInfoConfigure cardInfoConfigure = StaticConfigure.Card.Infos[i];
				array[i] = cardInfoConfigure.Id + cardInfoConfigure.NameID.GetLocal(UIStringType.Card);
			}
			win.comboBox_Card.items = array;
			win.btn_AddCard.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestAddCard(new List<int> { StaticConfigure.Card.Infos[win.comboBox_Card.selectedIndex].Id });
			});
			win.btn_RemoveCard.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestRemoveCard(new List<int> { StaticConfigure.Card.Infos[win.comboBox_Card.selectedIndex].Id });
			});
		}
	}

	private void RefreshEvent()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Event != null)
		{
			string[] array = new string[StaticConfigure.Event.Infos.Count];
			for (int i = 0; i < StaticConfigure.Event.Infos.Count; i++)
			{
				EventInfoConfigure eventInfoConfigure = StaticConfigure.Event.Infos[i];
				array[i] = eventInfoConfigure.Id + eventInfoConfigure.NameID.GetLocal(UIStringType.Event);
			}
			win.comboBox_Event.items = array;
			win.btn_Event.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestChangeEvent(StaticConfigure.Event.Infos[win.comboBox_Event.selectedIndex].Id);
			});
		}
	}

	private void RefreshDestiny()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Destiny != null)
		{
			string[] array = new string[StaticConfigure.Destiny.Infos.Count];
			for (int i = 0; i < StaticConfigure.Destiny.Infos.Count; i++)
			{
				DestinyInfoConfigure destinyInfoConfigure = StaticConfigure.Destiny.Infos[i];
				array[i] = destinyInfoConfigure.Id + destinyInfoConfigure.DescId.GetLocal(UIStringType.Destiny);
			}
			win.comboBox_Destiny.items = array;
			win.btn_Destiny.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestChangeDestiny(StaticConfigure.Destiny.Infos[win.comboBox_Destiny.selectedIndex].Id);
			});
		}
	}

	private void RefreshRelic()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Relic != null)
		{
			string[] array = new string[StaticConfigure.Relic.Infos.Count];
			for (int i = 0; i < StaticConfigure.Relic.Infos.Count; i++)
			{
				RelicInfoConfigure relicInfoConfigure = StaticConfigure.Relic.Infos[i];
				array[i] = relicInfoConfigure.Id + relicInfoConfigure.NameID.GetLocal(UIStringType.Relic);
			}
			win.comboBox_Relic.items = array;
			win.btn_Relic.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestAddRelic(StaticConfigure.Relic.Infos[win.comboBox_Relic.selectedIndex].Id);
			});
		}
	}

	private void RefreshDivination()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Divination != null)
		{
			string[] array = new string[StaticConfigure.Divination.Infos.Count];
			for (int i = 0; i < StaticConfigure.Divination.Infos.Count; i++)
			{
				DivinationInfoConfigure divinationInfoConfigure = StaticConfigure.Divination.Infos[i];
				array[i] = divinationInfoConfigure.Id + divinationInfoConfigure.NameID.GetLocal(UIStringType.Divination);
			}
			win.comboBox_Divination.items = array;
			win.btn_Divination.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestChangeDivination(StaticConfigure.Divination.Infos[win.comboBox_Divination.selectedIndex].Id);
			});
		}
	}

	private void CallSetTrems()
	{
		if (!(base.contentPane is UIGMWindow uIGMWindow) || string.IsNullOrEmpty(uIGMWindow.txtField_terms.text))
		{
			return;
		}
		string[] array = uIGMWindow.txtField_terms.text.Split(',', '|', '，', '&', ' ');
		int[] array2 = new int[array.Length];
		if (array != null && array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				int num = int.Parse(array[i]);
				if (num <= 0)
				{
					Debug.LogError("词条覆盖失败，部分数据解析错误，请检查");
					return;
				}
				array2[i] = num;
			}
			GMConfig.RequestGMTermIds(array2);
		}
		else
		{
			Debug.LogError("词条覆盖失败，数据解析错误，请检查");
		}
	}

	private void RefreshMapDifficulty()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null && StaticConfigure.Map != null && SimpleSingletonProvider<GameLogicManager>.inst != null && SimpleSingletonProvider<GameLogicManager>.inst.room != null && SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapId > 0)
		{
			MapInfoConfigure mapConfigure = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapId.GetMapDataConfigure();
			string[] array = new string[mapConfigure.DifficultyIds.Count];
			for (int i = 0; i < mapConfigure.DifficultyIds.Count; i++)
			{
				array[i] = mapConfigure.DifficultyIds[i].ToString();
			}
			win.comboBox_MapDifficulty.items = array;
			win.btn_SetMapDifficulty.onClick.Set((EventCallback0)delegate
			{
				GMConfig.RequestGMDifficultyId(mapConfigure.DifficultyIds[win.comboBox_MapDifficulty.selectedIndex]);
			});
		}
	}

	private async void CallPerform()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIGMWindow win)
		{
			string[] array = win.txtField_perform.text.Split(",");
			if (array.Length == 2)
			{
				win.visible = false;
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(int.Parse(array[0]), int.Parse(array[1]), "测试");
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
				win.visible = true;
			}
		}
	}

	private void CallItemChange(EventContext context)
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			string[] array = uIGMWindow.txtField_item.text.Trim().Split(" ");
			if (array.Length == 2)
			{
				MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
				{
					IsAll = false,
					ItemId = Convert.ToInt32(array[0]),
					ItemCount = Convert.ToInt32(array[1])
				});
			}
		}
	}

	private void CallAllItemChange(EventContext context)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
		{
			IsAll = true
		});
	}

	private void CallGacha(EventContext context)
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			string[] array = uIGMWindow.txtField_Gacha.text.Trim().Split(" ");
			if (array.Length == 2)
			{
				MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
				{
					IsGacha = true,
					ItemId = Convert.ToInt32(array[0]),
					ItemCount = Convert.ToInt32(array[1])
				});
			}
		}
	}

	private void CallGameOver(EventContext context)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			long playerId = TutorialGame.GetSystem<TutorialPlayerActionFSM>().PlayerId;
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
			{
				return;
			}
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
			{
				TutorialGame.GetSystem<TutorialBoardManager>().gameManager.SetTutorialStatus(TutorialStatus.Success);
				return;
			}
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial)
			{
				TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
				BattlePlayerData bossData = mapGimmickManager_Tutorial.BossData;
				long playerId2 = bossData.player.Id;
				int changeHp = -bossData.Property.maxHP;
				HeroAttrEffect hpUpdate = characterManager.GetHpUpdate(playerId2, changeHp);
				characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
				{
					Cause = new CauseOrigin(),
					PlayerId = playerId2,
					EffectDatas = { hpUpdate }
				}).Forget();
				return;
			}
		}
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			IsOver = true
		});
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIGMWindow uIGMWindow && base.isShowing && context.inputEvent.ctrl && context.inputEvent.keyCode == KeyCode.PageDown)
		{
			uIGMWindow.btn_GameOver.onClick.Call();
		}
	}

	private void TestFunc()
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			uIGMWindow.btn_Test1.title = "断网";
			uIGMWindow.btn_Test1.onClick.Set((EventCallback0)delegate
			{
				MonoSingletonProvider<NetManager>.inst.Close();
			});
			uIGMWindow.btn_Test2.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.BattleVideo.PlayCombineVideo(null).Forget();
			});
		}
	}

	private void RefreshNewGM()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle != null)
		{
			List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			List<BattlePlayerData> targetPlayers = (from player in playerDatas
				where player.CharacterInst != null
				select (player)).ToList();
			win.com_Target.txt_TargetPlayer.text = "";
			win.com_Target.list_Players.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIGM_Button_Operate uIGM_Button_Operate)
				{
					uIGM_Button_Operate.title = targetPlayers[index].player.Id.ToString();
					uIGM_Button_Operate.onClick.Set((EventCallback0)delegate
					{
						win.com_Target.txt_TargetPlayer.text = targetPlayers[index].player.Id.ToString();
						targetPlayers[index].CharacterInst.SwitchCamera().Forget();
					});
				}
			};
			win.com_Target.list_Players.numItems = targetPlayers.Count;
		}
		win.com_Target.com_Move.txt_Set.text = "";
		win.com_Target.com_Move.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_Move.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, result);
			}
		});
		win.com_Target.com_FightAtkPoint.txt_Set.text = "";
		win.com_Target.com_FightAtkPoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_FightAtkPoint.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, 0, 0, result);
			}
		});
		win.com_Target.com_FightDefPoint.txt_Set.text = "";
		win.com_Target.com_FightDefPoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_FightDefPoint.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, 0, 0, 0, result);
			}
		});
		win.com_Target.com_Portal.txt_Set.text = "";
		win.com_Target.com_Portal.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_Portal.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), result);
			}
		});
		win.com_Target.com_HP.txt_Set.text = "";
		win.com_Target.com_HP.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_HP.txt_Set.text, out result))
			{
				if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002)
				{
					TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
					BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(GetTargetId());
					HeroAttrEffect item = characterManager.GetHpUpdate(changeHp: result - playerDataById.Property.HP.Value, playerId: GetTargetId());
					characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
					{
						Cause = new CauseOrigin(),
						PlayerId = GetTargetId(),
						EffectDatas = { item }
					}).Forget();
				}
				else
				{
					GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, result);
				}
			}
		});
		win.com_Target.com_ATK.txt_Set.text = "";
		win.com_Target.com_ATK.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_ATK.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, result);
			}
		});
		win.com_Target.com_DEF.txt_Set.text = "";
		win.com_Target.com_DEF.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_DEF.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, result);
			}
		});
		win.com_Target.com_Gold.txt_Set.text = "";
		win.com_Target.com_Gold.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_Gold.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, 0, result);
			}
		});
		win.com_Target.com_LV.txt_Set.text = "";
		win.com_Target.com_LV.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_LV.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, result);
			}
		});
		win.com_Target.com_CardCount.txt_Set.text = "";
		win.com_Target.com_CardCount.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int result;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_CardCount.txt_Set.text, out result))
			{
				GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, result);
			}
		});
		RefreshGM_Buff();
		win.com_Target.com_CreateMonster.txt_Set_1.text = "";
		win.com_Target.com_CreateMonster.txt_Set_2.text = "";
		win.com_Target.com_CreateMonster.btn_Set.onClick.Set((EventCallback0)delegate
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo != null)
			{
				List<int> currentConfigMonsterIds = curRoomInfo.GetCurrentConfigMonsterIds();
				if (int.TryParse(win.com_Target.com_CreateMonster.txt_Set_1.text, out var result) && int.TryParse(win.com_Target.com_CreateMonster.txt_Set_2.text, out var result2))
				{
					if (currentConfigMonsterIds != null && currentConfigMonsterIds.Count > 0 && !currentConfigMonsterIds.Contains(result))
					{
						SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("当前地图不存在该角色资源");
					}
					else
					{
						GMConfig.RequestGMForTarget(GetTargetId(), 0, 0, 0, 0, 0, 0, 0, 0, 0, result, result2);
					}
				}
			}
		});
		win.com_Target.btn_ResetSkill.onClick.Set((EventCallback0)delegate
		{
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else
			{
				long targetId = GetTargetId();
				if (targetId != 0L)
				{
					BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetId);
					if (playerDataById != null)
					{
						GMConfig.RequestResetSkill(playerDataById.CharacterInst.skill.skillId, targetId);
					}
				}
			}
		});
	}

	private long GetTargetId()
	{
		if (!(base.contentPane is UIGMWindow uIGMWindow))
		{
			return 0L;
		}
		if (int.TryParse(uIGMWindow.com_Target.txt_TargetPlayer.text, out var result))
		{
			return result;
		}
		return 0L;
	}

	private void RefreshGM_Buff()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		win.com_Target.com_Buff.txt_Set.text = "0";
		win.com_Target.com_Buff.txt_SetSkillSource.text = "0";
		win.com_Target.com_Buff.txt_SetCardSource.text = "0";
		win.com_Target.com_Buff.btn_SetBuff.onClick.Set((EventCallback0)delegate
		{
			int result;
			int result2;
			int result3;
			if (string.IsNullOrEmpty(win.com_Target.txt_TargetPlayer.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips("请选择目标");
			}
			else if (int.TryParse(win.com_Target.com_Buff.txt_Set.text, out result) && int.TryParse(win.com_Target.com_Buff.txt_SetCardSource.text, out result2) && int.TryParse(win.com_Target.com_Buff.txt_SetSkillSource.text, out result3))
			{
				GMConfig.RequestGMBuffForTarget(GetTargetId(), result, result3, result2);
			}
		});
	}

	private void RefreshServerSelect()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		win.ShowServer.selectedIndex = ((SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login) ? 1 : 0);
		win.btn_ChangeIP.visible = false;
		win.Input_Server.text = $"{GameSettings.IP}:{GameSettings.Port}";
		win.Input_Server.onChanged.Set((EventCallback0)delegate
		{
			win.btn_ChangeIP.visible = !win.Input_Server.text.Equals($"{GameSettings.IP}:{GameSettings.Port}");
		});
		win.btn_ChangeIP.onClick.Set((EventCallback0)delegate
		{
			string[] array = win.Input_Server.text.Split(':');
			UpdateServerIP(array[0], int.Parse(array[1]));
		});
		win.list_Server.onClickItem.Set(delegate(EventContext context)
		{
			if (context.data is UIGM_Button_Server uIGM_Button_Server)
			{
				string[] array = uIGM_Button_Server.title.Split('=')[1].Split(':');
				UpdateServerIP(array[0], int.Parse(array[1]));
				win.Input_Server.text = $"{GameSettings.IP}:{GameSettings.Port}";
			}
		});
	}

	private void UpdateServerIP(string ip, int port)
	{
		GameSettings.IP = ip;
		GameSettings.Port = port;
	}

	private void RefreshNovice()
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			uIGMWindow.btn_Tutorial.selected = GMConfig.Tutorial;
			uIGMWindow.btn_Tutorial.onClick.Set((EventCallback0)delegate
			{
				GMConfig.Tutorial = !GMConfig.Tutorial;
			});
			uIGMWindow.btn_Tutorial1001.selected = GMConfig.Tutorial1001;
			uIGMWindow.btn_Tutorial1001.onClick.Set((EventCallback0)delegate
			{
				GMConfig.Tutorial1001 = !GMConfig.Tutorial1001;
			});
			uIGMWindow.btn_Tutorial1002.selected = GMConfig.Tutorial1002;
			uIGMWindow.btn_Tutorial1002.onClick.Set((EventCallback0)delegate
			{
				GMConfig.Tutorial1002 = !GMConfig.Tutorial1002;
			});
			uIGMWindow.btn_TutorialSingle.selected = GMConfig.TutorialSingle;
			uIGMWindow.btn_TutorialSingle.onClick.Set((EventCallback0)delegate
			{
				GMConfig.TutorialSingle = !GMConfig.TutorialSingle;
			});
		}
	}

	private static void SingPlay(UIGMWindow win)
	{
		win.com_SinglePlayer.com_DicePoint.btn_Set.onClick.Set((EventCallback0)delegate
		{
			Game.GetModel<GMData>().SetDicePoint(int.Parse(win.com_SinglePlayer.com_DicePoint.txt_Set.text));
		});
		win.com_SinglePlayer.com_AddCard.btn_Set.onClick.Set((EventCallback0)delegate
		{
			int num = int.Parse(win.com_SinglePlayer.com_AddCard.txt_Set.text);
			if (!StaticConfigure.SinglePlayer.CardDict.ContainsKey(num))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips($"不存在卡片id:{num}");
			}
			else
			{
				Game.GetSystem<BoardManager>().cardManager.AddCardToBag(num);
			}
		});
		win.com_SinglePlayer.com_AddGold.btn_Set.onClick.Set((EventCallback0)delegate
		{
			Hero hero = Game.GetSystem<BoardManager>().characterManager.Hero;
			hero.Property.ChangeGold(int.Parse(win.com_SinglePlayer.com_AddGold.txt_Set.text));
			hero.UpdateViewProperty();
		});
		win.com_SinglePlayer.com_AddProgress.btn_Set.onClick.Set((EventCallback0)delegate
		{
			ReactiveEncryptorProperty<int, Int32Encryptor> gameProgress = Game.GetModel<GameData>().GameProgress;
			if (int.TryParse(win.com_SinglePlayer.com_AddProgress.txt_Set.text, out var result) && result > 0)
			{
				gameProgress.Value = result;
				foreach (MapMission mapMissionDatum in Game.GetModel<GameData>().MapData.MapMissionData)
				{
					mapMissionDatum.GMSkipProgress(result);
				}
			}
		});
		win.com_SinglePlayer.com_AddRelic.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_SinglePlayer.com_AddRelic.txt_Set.text, out var result) && result > 0)
			{
				Game.GetSystem<BoardManager>().relicManager.AddRelic(new RelicInfo(result));
			}
		});
		win.com_SinglePlayer.com_GameOver.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(win.com_SinglePlayer.com_GameOver.txt_Set.text, out var result) && result > 0)
			{
				Game.GetModel<GMData>().SetScore(result);
			}
			GameOverpanel();
		});
		win.com_SinglePlayer.btn_CardProbability.onClick.Set((EventCallback0)delegate
		{
			Game.GetSystem<BoardManager>().cardManager.TestCardProbability();
		});
		win.com_SinglePlayer.btn_LockProgress.onClick.Set((EventCallback0)delegate
		{
			Game.GetModel<GMData>().SetLockGameProgress(win.com_SinglePlayer.btn_LockProgress.selected);
		});
	}

	private static async void GameOverpanel()
	{
		Game.GetModel<GameData>().UploadDataToServer(GameStatus.Victory);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.HandleSinglePlayerDataAfterBattle();
		if (await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SinglePlayerSettlement) is SinglePlayerSettlementPanel singlePlayerSettlementPanel)
		{
			singlePlayerSettlementPanel.ShowResult(GameStatus.Victory);
		}
	}

	private void SetBattleHeroSkin()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win != null)
		{
			win.com_Random.btn_Random.onClick.Set((EventCallback0)delegate
			{
				SetSlotValue(win.com_Random.com_Slot1.txt_Set, 0, 100101004);
				SetSlotValue(win.com_Random.com_Slot2.txt_Set, 1, 100102005);
				SetSlotValue(win.com_Random.com_Slot3.txt_Set, 2, 100115004);
				SetSlotValue(win.com_Random.com_Slot4.txt_Set, 3, 100120003);
			});
			win.com_Random.com_Slot1.txt_Set.text = GMConfig.HeroStandingPainting[0].ToString();
			win.com_Random.com_Slot2.txt_Set.text = GMConfig.HeroStandingPainting[1].ToString();
			win.com_Random.com_Slot3.txt_Set.text = GMConfig.HeroStandingPainting[2].ToString();
			win.com_Random.com_Slot4.txt_Set.text = GMConfig.HeroStandingPainting[3].ToString();
			SetSlotEvent(win.com_Random.com_Slot1, 0);
			SetSlotEvent(win.com_Random.com_Slot2, 1);
			SetSlotEvent(win.com_Random.com_Slot3, 2);
			SetSlotEvent(win.com_Random.com_Slot4, 3);
		}
	}

	private void SetSlotValue(GTextInput txtSet, int index, int skinItemId)
	{
		txtSet.text = skinItemId.ToString();
		GMConfig.HeroStandingPainting[index] = skinItemId;
	}

	private void SetSlotEvent(UIGM_Com_SetAttr comSlot, int index)
	{
		comSlot.btn_Set.onClick.Set((EventCallback0)delegate
		{
			if (int.TryParse(comSlot.txt_Set.text, out var result))
			{
				GMConfig.HeroStandingPainting[index] = result;
			}
		});
	}

	private void SetLandIdGM()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		win.btn_OpenLandId.onClick.Set((EventCallback0)delegate
		{
			Dictionary<int, UnitLand>.ValueCollection valueCollection = SimpleSingletonProvider<LandManager>.inst?.NodeDict?.Values;
			if (valueCollection != null)
			{
				win.btn_OpenLandId.onClick.Retain();
				DestroyLandIdTexts();
				foreach (UnitLand item in valueCollection)
				{
					TextMeshPro val = new GameObject("LandId").AddComponent<TextMeshPro>();
					((TMP_Text)val).alignment = (TextAlignmentOptions)514;
					((Graphic)val).color = new Color(0.06f, 1f, 0f, 1f);
					((TMP_Text)val).SetText(item.Id.ToString(), true);
					((Component)(object)val).gameObject.layer = item.gameObject.layer;
					((TMP_Text)val).rectTransform.SetParent(item.transform);
					((TMP_Text)val).rectTransform.localEulerAngles = new Vector3(45f, -45f, 0f);
					((TMP_Text)val).rectTransform.localPosition = Vector3.up * 10f;
					LandIdTexts.Add(val);
				}
				win.btn_OpenLandId.onClick.Release();
			}
		});
		win.btn_CloseLandId.onClick.Set((EventCallback0)delegate
		{
			win.btn_CloseLandId.onClick.Retain();
			DestroyLandIdTexts();
			win.btn_CloseLandId.onClick.Release();
		});
		void DestroyLandIdTexts()
		{
			foreach (TextMeshPro landIdText in LandIdTexts)
			{
				if ((UnityEngine.Object)(object)landIdText != null)
				{
					UnityEngine.Object.Destroy(((Component)(object)landIdText).gameObject);
				}
			}
			LandIdTexts.Clear();
		}
	}

	private void SetFreeCameraGM()
	{
		GComponent gComponent = base.contentPane;
		UIGMWindow win = gComponent as UIGMWindow;
		if (win == null)
		{
			return;
		}
		win.btn_OpenFreeCamera.onClick.Set((EventCallback0)delegate
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
			{
				CinemachineVirtualCamera cinemachineVirtualCamera = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData()?.CharacterInst?.vCamera?.vCamera;
				if ((object)cinemachineVirtualCamera != null)
				{
					win.btn_OpenFreeCamera.onClick.Retain();
					DestroyFreeCamera();
					GameObject gameObject = UnityEngine.Object.Instantiate(cinemachineVirtualCamera.gameObject);
					gameObject.name = "GMFreeCamera";
					gmFreeCamera = gameObject.AddComponent<GMFreeCamera>();
					gmFreeCamera.Initialize(cinemachineVirtualCamera);
					win.btn_OpenFreeCamera.onClick.Release();
				}
			}
		});
		win.btn_CloseFreeCamera.onClick.Set((EventCallback0)delegate
		{
			win.btn_CloseFreeCamera.onClick.Retain();
			DestroyFreeCamera();
			win.btn_CloseFreeCamera.onClick.Release();
		});
		void DestroyFreeCamera()
		{
			if (gmFreeCamera != null)
			{
				UnityEngine.Object.Destroy(gmFreeCamera.gameObject);
				gmFreeCamera = null;
			}
		}
	}

	private void SetUnlockSportInfo()
	{
		if (base.contentPane is UIGMWindow uIGMWindow)
		{
			uIGMWindow.btn_UnlockSportInfo.onClick.Add((EventCallback0)delegate
			{
				MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
				{
					IsUnlockSportInfo = true
				});
			});
			uIGMWindow.btn_UnlockRoleInfo.onClick.Add((EventCallback0)delegate
			{
				MonoSingletonProvider<NetManager>.inst.RPC.CheatItemC2S.CheatItemC2SCall(new CheatItemC2S
				{
					IsUnlockRoleInfo = true
				});
			});
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (base.contentPane is UIGMWindow uIGMWindow && Input.GetKey(KeyCode.Return) && uIGMWindow.visible)
		{
			CallPerform();
		}
	}
}
