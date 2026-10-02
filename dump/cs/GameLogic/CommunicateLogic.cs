using System;
using System.Collections.Generic;
using Core;
using Core.Mark;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class CommunicateLogic : IRPCSync
{
	private readonly Dictionary<int, Func<BattlePlayerData, string[], BattleMessage>> _messageFactory = new Dictionary<int, Func<BattlePlayerData, string[], BattleMessage>>();

	public readonly MarkSignal MarkSignal = new MarkSignal();

	private readonly Dictionary<long, List<Effect>> _playerMarkEffects = new Dictionary<long, List<Effect>>();

	private const int MaxMarkEffectCount = 4;

	private readonly List<ExpressionData> expressionData = new List<ExpressionData>();

	private CharacterExpression _expressionContainer;

	private float _lastSendTime;

	public readonly int COLLECTED_PACK_ID;

	private HashSet<int> _redPointPackIds = new HashSet<int>();

	private readonly List<int> _starExpressions = new List<int>();

	private readonly List<int> _topExpressionPacks = new List<int>();

	private readonly Dictionary<int, CommunicateExpressionPack> _packDic = new Dictionary<int, CommunicateExpressionPack>();

	private readonly List<CommunicateExpressionPack> _cacheSortedPackList = new List<CommunicateExpressionPack>();

	private bool isDirtyPackDic;

	public readonly CommunicateSignal Signal = new CommunicateSignal();

	private readonly Dictionary<long, SessionData> _sessionDataDict = new Dictionary<long, SessionData>();

	private readonly List<SessionData> _sessions = new List<SessionData>();

	private void InitMsgFactory()
	{
		_messageFactory[3] = (BattlePlayerData player, string[] msg) => new BattleLandMessage(player, msg);
		_messageFactory[6] = (BattlePlayerData player, string[] msg) => new BattlePveEventMessage(player, msg);
		_messageFactory[7] = (BattlePlayerData player, string[] msg) => new BattlePveTaskMessage(player, msg);
		_messageFactory[8] = (BattlePlayerData player, string[] msg) => new BattleCardMessage(player, msg);
		_messageFactory[9] = (BattlePlayerData player, string[] msg) => new BattleSkillMessage(player, msg);
		_messageFactory[4] = (BattlePlayerData player, string[] msg) => new BattleRelicMessage(player, msg);
		_messageFactory[5] = (BattlePlayerData player, string[] msg) => new BattlePraiseMessage(player, msg);
		_messageFactory[10] = (BattlePlayerData player, string[] msg) => new BattlePlayerMarkMessage(player, msg);
	}

	public void ManagerMarkEffect(long playerId, Effect effect)
	{
		if (!_playerMarkEffects.TryGetValue(playerId, out var value))
		{
			value = new List<Effect>();
			_playerMarkEffects.Add(playerId, value);
		}
		Effect effect2 = value.Find((Effect x) => x == effect);
		if (effect2 != null)
		{
			value.Remove(effect2);
		}
		value.Add(effect);
		if (value.Count > 4)
		{
			Effect effect3 = value[0];
			value.RemoveAt(0);
			if (effect3 != null && effect3.IsActive)
			{
				effect3.ReleaseEffect();
			}
		}
	}

	public void Connect_Battle()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SendChatS2C.OnSendChatS2CServerCallBackAsync = OnSendChatS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChatMapMarkersS2C.OnChatMapMarkersS2CServerCallBackAsync = OnChatMapMarkersS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SayPhraseNotifyS2C.OnSayPhraseNotifyS2CServerCallBackAsync = OnSayPhraseNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerChatS2C.OnPlayerChatS2CServerCallBackAsync = OnPlayerChatS2CServerCallBackAsync;
	}

	public void Disconnect_Battle()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SendChatS2C.OnSendChatS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChatMapMarkersS2C.OnChatMapMarkersS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SayPhraseNotifyS2C.OnSayPhraseNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PlayerChatS2C.OnPlayerChatS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestSendChatC2S(int expressionId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			return null;
		}
		SendChatC2S sendChatC2S = new SendChatC2S
		{
			ExpressionId = expressionId
		};
		foreach (RoomPlayer player in SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Players)
		{
			sendChatC2S.PlayerIds.Add(player.Id);
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.SendChatC2S.SendChatC2SCall(sendChatC2S);
	}

	private async UniTask OnSendChatS2CServerCallBack(SendChatS2C model, int errId, bool isdispatch)
	{
		if (errId == 0 && model != null && model.PlayerId != 0L)
		{
			await SendExpressionChat(model.PlayerId, model.ExpressionId);
		}
	}

	private async UniTask SendExpressionChat(long playerId, int expressionId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Contains(playerId) || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || !SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			return;
		}
		RoomStateType roomStateType = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType;
		if (roomStateType == RoomStateType.RUNNING || roomStateType == RoomStateType.SETTLEMENT)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
			{
				return;
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById == null)
			{
				return;
			}
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.EXPRESSION, playerDataById, expressionId));
		}
		await UniTask.CompletedTask;
	}

	public void RequestSendMessageC2S(BattleMessage msgData, int markId, bool CD_CHECK = true)
	{
		if (!CD_CHECK || SimpleSingletonProvider<GameLogicManager>.inst.communicate.SendChatLicense())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(msgData);
			MonoSingletonProvider<NetManager>.inst.RPC.ChatMapMarkersC2S.ChatMapMarkersC2SCall(new ChatMapMarkersC2S
			{
				Msg = (msgData.SendMsg ?? ""),
				MarkId = markId
			});
		}
	}

	private async UniTask OnChatMapMarkersS2CServerCallBack(ChatMapMarkersS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && model != null)
		{
			DealChatMsg(model.PlayerId, model.Msg);
			await UniTask.CompletedTask;
		}
	}

	private void DealChatMsg(long playerId, string msg)
	{
		if (playerId == 0L || string.IsNullOrEmpty(msg) || SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Contains(playerId) || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || !SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null)
		{
			string[] array = msg.Split(',');
			BattleMessage battleMessage = null;
			if (int.TryParse(array[0], out var result) && _messageFactory.TryGetValue(result, out var value))
			{
				battleMessage = value(playerDataById, array);
			}
			if (battleMessage != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(battleMessage);
			}
		}
	}

	private async UniTask OnSayPhraseNotifyS2CServerCallBack(SayPhraseNotifyS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null && SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady && GameSettings.AutoShortChat && !SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Contains(model.Phrase.ActivePlayerId) && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.Phrase.PassivePlayerId))
		{
			int chatId = model.Phrase.TriggerType switch
			{
				TriggerPhrase.Types.Type.TransferGold => 50000, 
				TriggerPhrase.Types.Type.CureFriend => 50001, 
				TriggerPhrase.Types.Type.GiveCard => 50002, 
				TriggerPhrase.Types.Type.KillBoss => 50003, 
				_ => 0, 
			};
			if (model.Phrase.TriggerType != TriggerPhrase.Types.Type.KillBoss)
			{
				SimpleSingletonProvider<UIManager>.inst.expression.TryShowQuickReply(chatId).Forget();
			}
			await UniTask.CompletedTask;
		}
	}

	public void RequestPlayerChatC2S(BattleMessage msgData, List<long> receivers, bool CD_CHECK = true)
	{
		if (!CD_CHECK || SimpleSingletonProvider<GameLogicManager>.inst.communicate.SendChatLicense())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(msgData);
			MonoSingletonProvider<NetManager>.inst.RPC.PlayerChatC2S.PlayerChatC2SCall(new PlayerChatC2S
			{
				PlayerIds = { (IEnumerable<long>)receivers },
				Msg = (msgData.SendMsg ?? "")
			});
		}
	}

	private async UniTask OnPlayerChatS2CServerCallBackAsync(PlayerChatS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null)
		{
			DealChatMsg(model.PlayerId, model.Msg);
			await UniTask.CompletedTask;
		}
	}

	public void InitFromServer(Player player)
	{
		InitCommunicateExpressionFromServer(player);
		InitSessionDataFromServer(player.MsgInfos);
		InitMsgFactory();
	}

	public async UniTask<List<ExpressionData>> LoadExpression(CharacterInfoConfigure heroInfo)
	{
		CharacterExpressionPackConfigure characterExpressionPackConfigure = heroInfo.ExpressionPackID.GetCharacterExpressionPackConfigure();
		if (_expressionContainer != null && _expressionContainer._ExpressionPackConfig.Id == characterExpressionPackConfigure.Id)
		{
			return expressionData;
		}
		UnLoadExpression();
		_expressionContainer = new CharacterExpression(heroInfo.Id);
		await _expressionContainer.ReadyExpression(checkStatus: true);
		return GetExpressionDataList(_expressionContainer);
	}

	private List<ExpressionData> GetExpressionDataList(CharacterExpression _container)
	{
		expressionData.Clear();
		foreach (KeyValuePair<int, ExpressionData> item in _container.expressionDict)
		{
			expressionData.Add(item.Value);
		}
		expressionData.Sort(delegate(ExpressionData x, ExpressionData y)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(x.expressionItemID);
			bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(y.expressionItemID);
			return (flag != flag2) ? ((!flag) ? 1 : (-1)) : (x.expressionItemID - y.expressionItemID);
		});
		return expressionData;
	}

	public void UnLoadExpression()
	{
		_expressionContainer?.Dispose();
		_expressionContainer = null;
	}

	public bool SendChatLicense()
	{
		float time = Time.time;
		if ((time - _lastSendTime) * 1000f > (float)StaticGlobalData.GAME_CHARACTER_EXPRESSION_CD)
		{
			_lastSendTime = time;
			return true;
		}
		SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10017);
		return false;
	}

	public void Connect()
	{
		Connect_Battle();
		Connect_Chat();
		Connect_Expression();
	}

	public void Disconnect()
	{
		Disconnect_Battle();
		Disconnect_Chat();
		Disconnect_Expression();
	}

	private void InitCommunicateExpressionFromServer(Player player)
	{
		HandleStarExpressions(player);
		HandleTopExpressions(player);
		LocalLoadRedPointPackIds();
		UpdateCollectedPack();
		foreach (int item in SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.HeroExpression))
		{
			TryAddExpressionToPack(item, out var _);
		}
	}

	private void HandleTopExpressions(Player player)
	{
		RepeatedField<int> repeatedField = player?.ClientData?.TopExpression;
		if (repeatedField != null && repeatedField.Count > 0)
		{
			_topExpressionPacks.Clear();
			_topExpressionPacks.AddRange(repeatedField);
		}
	}

	private void HandleStarExpressions(Player player)
	{
		RepeatedField<int> repeatedField = player?.ClientData?.StarExpression;
		if (repeatedField != null && repeatedField.Count > 0)
		{
			_starExpressions.Clear();
			_starExpressions.AddRange(repeatedField);
		}
	}

	public bool TryAddExpressionToPack(int itemId, out int packId)
	{
		if (!StaticConfigure.Chat.ExpressionPackIdDic.TryGetValue(itemId, out packId))
		{
			return false;
		}
		if (!_packDic.TryGetValue(packId, out var value))
		{
			value = new CommunicateExpressionPack(packId);
			_packDic[packId] = value;
		}
		value.AddExpression(itemId);
		isDirtyPackDic = true;
		return true;
	}

	public List<CommunicateExpressionPack> GetSortedPackList()
	{
		if (!isDirtyPackDic)
		{
			return _cacheSortedPackList;
		}
		isDirtyPackDic = false;
		foreach (KeyValuePair<int, CommunicateExpressionPack> item in _packDic)
		{
			item.Value.StarId = ((item.Key == COLLECTED_PACK_ID) ? _packDic.Count : 0);
		}
		for (int i = 0; i < _topExpressionPacks.Count; i++)
		{
			int starId = _topExpressionPacks.Count - i;
			int key = _topExpressionPacks[i];
			if (_packDic.TryGetValue(key, out var value))
			{
				value.StarId = starId;
			}
		}
		_cacheSortedPackList.Clear();
		_cacheSortedPackList.AddRange(_packDic.Values);
		_cacheSortedPackList.Sort(delegate(CommunicateExpressionPack x, CommunicateExpressionPack y)
		{
			int num = y.StarId.CompareTo(x.StarId);
			if (num == 0)
			{
				int packId = x.PackId;
				return packId.CompareTo(y.PackId);
			}
			return num;
		});
		return _cacheSortedPackList;
	}

	private void UpdateCollectedPack()
	{
		if (!_packDic.TryGetValue(COLLECTED_PACK_ID, out var value))
		{
			value = new CommunicateExpressionPack(COLLECTED_PACK_ID);
			_packDic[COLLECTED_PACK_ID] = value;
		}
		value.UpdateExpressionNoop(_starExpressions);
	}

	public List<int> GetStarExpressionSnapshot()
	{
		return new List<int>(_starExpressions);
	}

	public List<int> GetTopExpressionSnapshot()
	{
		return new List<int>(_topExpressionPacks);
	}

	public bool IsTop(int packId)
	{
		return _topExpressionPacks.Contains(packId);
	}

	public bool IsCollected(int expressionId)
	{
		return _starExpressions.Contains(expressionId);
	}

	public int GetCollectedCount()
	{
		return _starExpressions.Count;
	}

	public void ChangeExpressionStarState(int expressionId, out bool collectStatus)
	{
		if (_starExpressions.Contains(expressionId))
		{
			_starExpressions.Remove(expressionId);
			collectStatus = false;
		}
		else
		{
			_starExpressions.Add(expressionId);
			collectStatus = true;
		}
	}

	public void SyncStarExpression(bool sync)
	{
		if (sync)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.StarExpression).OnFinished.AddOnce(delegate(RPCAsyncResult _)
			{
				if (_.errId == 0)
				{
					ClientData clientData = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo()?.ClientData;
					if (clientData != null)
					{
						clientData.StarExpression.Clear();
						clientData.StarExpression.AddRange(_starExpressions);
					}
				}
			});
		}
		else
		{
			Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
			HandleStarExpressions(playerInfo);
		}
		UpdateCollectedPack();
	}

	public void ChangeExpressionPackPinState(int packId)
	{
		if (_topExpressionPacks.Contains(packId))
		{
			_topExpressionPacks.Remove(packId);
		}
		else
		{
			_topExpressionPacks.Insert(0, packId);
		}
		isDirtyPackDic = true;
		SimpleSingletonProvider<GameLogicManager>.inst.account.RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.TopExpression);
	}

	private void OnNewItemAdded(int itemId)
	{
		if (TryAddExpressionToPack(itemId, out var packId))
		{
			_redPointPackIds.Add(packId);
			LocalSaveRedPointPackIds();
		}
	}

	public bool IsNewExpression()
	{
		return _redPointPackIds.Count > 0;
	}

	public bool IsPackShowRedPoint(int packId)
	{
		return _redPointPackIds.Contains(packId);
	}

	public void RemovePackRedPoint(int packId)
	{
		_redPointPackIds.Remove(packId);
		LocalSaveRedPointPackIds();
		Signal.newExpression.Dispatch();
	}

	private void LocalSaveRedPointPackIds()
	{
		ES3.Save(LocalStore.NewExpressionReceivePackCache, _redPointPackIds);
	}

	private void LocalLoadRedPointPackIds()
	{
		if (ES3.KeyExists(LocalStore.NewExpressionReceivePackCache))
		{
			_redPointPackIds = ES3.Load<HashSet<int>>(LocalStore.NewExpressionReceivePackCache);
		}
	}

	private void Connect_Expression()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemAdded.AddListener(OnNewItemAdded);
	}

	private void Disconnect_Expression()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemAdded.RemoveListener(OnNewItemAdded);
	}

	private void InitSessionDataFromServer(MapField<long, FriendChatMsgInfo> msgInfos)
	{
		foreach (KeyValuePair<long, FriendChatMsgInfo> msgInfo in msgInfos)
		{
			TryGetSessionByPlayerId(msgInfo.Key).UpdateSessionTime(msgInfo.Value.LastTime, msgInfo.Value.ReadTime);
		}
	}

	public SessionData TryGetSessionByPlayerId(long playerId)
	{
		if (!_sessionDataDict.TryGetValue(playerId, out var value))
		{
			value = new SessionData(playerId);
			_sessionDataDict.TryAdd(playerId, value);
		}
		return value;
	}

	public List<SessionData> GetSessionList()
	{
		_sessions.Clear();
		foreach (KeyValuePair<long, SessionData> item in _sessionDataDict)
		{
			_sessions.Add(item.Value);
		}
		_sessions.Sort(CompareByTime);
		return _sessions;
	}

	private int CompareByTime(SessionData x, SessionData y)
	{
		if (x.isHaveUnReadMsg && y.isHaveUnReadMsg)
		{
			if (x.lastTime <= y.lastTime)
			{
				return -1;
			}
			return 1;
		}
		if (x.isHaveUnReadMsg && !y.isHaveUnReadMsg)
		{
			return -1;
		}
		if (!x.isHaveUnReadMsg && y.isHaveUnReadMsg)
		{
			return 1;
		}
		if (x.targetPlayerId <= y.targetPlayerId)
		{
			return -1;
		}
		return 1;
	}

	private void InsertSession(SessionData newData)
	{
		int num = _sessions.FindLastIndex((SessionData x) => !x.isHaveUnReadMsg);
		if (num != -1)
		{
			_sessions.Insert(num + 1, newData);
		}
		else
		{
			_sessions.Add(newData);
		}
	}

	public SessionData IsHaveUnReadMsg()
	{
		foreach (KeyValuePair<long, SessionData> item in _sessionDataDict)
		{
			if (item.Value.isHaveUnReadMsg)
			{
				return item.Value;
			}
		}
		return null;
	}

	public void TryRemoveChatData(long targetPlayerId)
	{
		if (_sessionDataDict.TryGetValue(targetPlayerId, out var value))
		{
			_sessions.Remove(value);
			_sessionDataDict.Remove(targetPlayerId);
		}
	}

	public void Connect_Chat()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoomShortChatS2C.OnRoomShortChatS2CServerCallBackAsync = OnRoomShortChatS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetChatMsgS2C.OnGetChatMsgS2CServerCallBackAsync = OnGetChatMsgS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendSendMsgS2C.OnFriendSendMsgS2CServerCallBackAsync = OnFriendSendMsgS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ReadChatMsgS2C.OnReadChatMsgS2CServerCallBackAsync = OnReadChatMsgS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendsChatMsgS2C.OnFriendsChatMsgS2CServerCallBackAsync = OnFriendsChatMsgS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.DelChatMsgInfoS2C.OnDelChatMsgInfoS2CServerCallBackAsync = OnDelChatMsgInfoS2CServerCallBack;
	}

	public void Disconnect_Chat()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoomShortChatS2C.OnRoomShortChatS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetChatMsgS2C.OnGetChatMsgS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendSendMsgS2C.OnFriendSendMsgS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ReadChatMsgS2C.OnReadChatMsgS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendsChatMsgS2C.OnFriendsChatMsgS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.DelChatMsgInfoS2C.OnDelChatMsgInfoS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestRoomShortChatC2S(int MessageId)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			return null;
		}
		RoomShortChatC2S roomShortChatC2S = new RoomShortChatC2S
		{
			Index = MessageId
		};
		foreach (RoomPlayer player in SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Players)
		{
			roomShortChatC2S.PlayerIds.Add(player.Id);
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.RoomShortChatC2S.RoomShortChatC2SCall(roomShortChatC2S);
	}

	private async UniTask OnRoomShortChatS2CServerCallBack(RoomShortChatS2C model, int errid, bool isdispatch)
	{
		if (errid != 0 || model == null || model.PlayerId == 0L)
		{
			return;
		}
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				Signal.shortChat.Dispatch(model.PlayerId, model.Index);
			}
		}
		else if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady || SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Contains(model.PlayerId) || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				return;
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
			if (playerDataById == null)
			{
				return;
			}
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.SHORTINFO, playerDataById, model.Index));
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestGetChatMsgC2S(long _targetPlayerId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetChatMsgC2S.GetChatMsgC2SCall(new GetChatMsgC2S
		{
			TargetId = _targetPlayerId
		});
	}

	private async UniTask OnGetChatMsgS2CServerCallBack(GetChatMsgS2C model, int errid, bool isdispatch)
	{
		switch (errid)
		{
		case 12009:
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestDeleteChatC2S(model.TargetId);
			break;
		case 0:
		{
			SessionData sessionData = TryGetSessionByPlayerId(model.TargetId);
			sessionData.UpdateChatMsg(model.Message);
			sessionData.FinishRead();
			await UniTask.CompletedTask;
			break;
		}
		}
	}

	public void RequestFriendSendMsgC2S(long targetId, string msg)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.FriendSendMsgC2S.FriendSendMsgC2SCall(new FriendSendMsgC2S
		{
			PlayerId = targetId,
			Msg = msg
		});
	}

	private async UniTask OnFriendSendMsgS2CServerCallBack(FriendSendMsgS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (model.Time <= 0 || !StaticConfigure.Server.ErrorDict.TryGetValue(10022, out var value))
			{
				await UniTask.CompletedTask;
				return;
			}
			long num = model.Time / 60;
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(string.Format(value.DescId.GetLocal(UIStringType.Server), num));
		}
	}

	public void RequestReadChatMsgC2S(long targetId)
	{
		TryGetSessionByPlayerId(targetId).FinishRead();
		MonoSingletonProvider<NetManager>.inst.RPC.ReadChatMsgC2S.ReadChatMsgC2SCall(new ReadChatMsgC2S
		{
			TargetId = targetId
		});
	}

	private async UniTask OnReadChatMsgS2CServerCallBack(ReadChatMsgS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnFriendsChatMsgS2CServerCallBack(FriendsChatMsgS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			bool flag = !_sessionDataDict.ContainsKey(model.PlayerId);
			SessionData sessionData = TryGetSessionByPlayerId(model.PlayerId);
			sessionData.UpdateSessionTime(model.LastTime, sessionData.readTime);
			sessionData.UpdateChatMsg(model.PlayerId, SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID(), model.Message, model.LastTime);
			if (flag)
			{
				InsertSession(sessionData);
			}
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowChatSignal(sessionData.targetPlayerId);
			Signal.newMessage.Dispatch(sessionData.targetPlayerId, flag);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestDeleteChatC2S(long targetPlayerId)
	{
		TryRemoveChatData(targetPlayerId);
		return MonoSingletonProvider<NetManager>.inst.RPC.DelChatMsgInfoC2S.DelChatMsgInfoC2SCall(new DelChatMsgInfoC2S
		{
			TargetId = targetPlayerId
		});
	}

	private async UniTask OnDelChatMsgInfoS2CServerCallBack(DelChatMsgInfoS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}
}
