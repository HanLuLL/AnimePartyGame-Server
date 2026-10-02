using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class RoomListLogic : IRPCSync
{
	public readonly RoomListSignal signal = new RoomListSignal();

	private int _mapModeType;

	private readonly Dictionary<int, RoomListData> roomListDict = new Dictionary<int, RoomListData>();

	private readonly List<GameModeInfoConfigure> _gameModeInfos = new List<GameModeInfoConfigure>();

	public int mapModeType
	{
		get
		{
			GameModeInfoConfigure value;
			if (_mapModeType == 0)
			{
				_mapModeType = (int)gameModeInfos[0].MapModeType;
			}
			else if (StaticConfigure.GameMode.InfoDict.TryGetValue(_mapModeType, out value) && !TimeHelper.ValidityTime(value.BeginTime, value.EndTime))
			{
				_mapModeType = (int)gameModeInfos[0].MapModeType;
			}
			return _mapModeType;
		}
		set
		{
			_mapModeType = 0;
			if (StaticConfigure.GameMode.InfoDict.TryGetValue(value, out var value2) && TimeHelper.ValidityTime(value2.BeginTime, value2.EndTime))
			{
				_mapModeType = value;
			}
		}
	}

	public List<GameModeInfoConfigure> gameModeInfos
	{
		get
		{
			if (_gameModeInfos.Count == 0)
			{
				RepeatedField<GameModeInfoConfigure> infos = StaticConfigure.GameMode.Infos;
				for (int i = 0; i < infos.Count; i++)
				{
					if (infos[i].IsShow && TimeHelper.ValidityTime(infos[i].BeginTime, infos[i].EndTime))
					{
						_gameModeInfos.Add(infos[i]);
					}
				}
				_gameModeInfos.Sort((GameModeInfoConfigure x, GameModeInfoConfigure y) => x.OrderWeight - y.OrderWeight);
			}
			return _gameModeInfos;
		}
	}

	public RoomListLogic()
	{
		_gameModeInfos.Clear();
	}

	public RoomShortInfo GetRoomLabelById(long roomId)
	{
		if (!roomListDict.TryGetValue(mapModeType, out var value))
		{
			return null;
		}
		return value.GetRoomLabelById(roomId);
	}

	public List<RoomShortInfo> GetRoomLabelInfos()
	{
		if (!roomListDict.TryGetValue(mapModeType, out var value))
		{
			return null;
		}
		return value.roomInfos;
	}

	public float GetCurrentRefreshTime()
	{
		if (!roomListDict.TryGetValue(mapModeType, out var value))
		{
			return 0f;
		}
		return value._currentRefreshTime;
	}

	public void UpdateCurrentRefreshTime(float time)
	{
		if (roomListDict.TryGetValue(mapModeType, out var value))
		{
			value._currentRefreshTime = time;
		}
	}

	public List<int> GetValidDifficultyConfig()
	{
		RepeatedField<MapMapLevelConfigure> mapLevels = StaticConfigure.Map.MapLevels;
		List<int> list = new List<int>();
		foreach (ChoosingTimeLimitdifficultyConfigure difficulty in StaticConfigure.ChoosingTimeLimit.Difficultys)
		{
			list.Add((int)difficulty.GameDifficultyType);
		}
		foreach (MapMapLevelConfigure item in mapLevels)
		{
			foreach (MapMapLevelConfigureItem mapMapLevelConfigureItem in item.MapMapLevelConfigureItems)
			{
				if (!TimeHelper.ValidityTime(mapMapLevelConfigureItem.BeginTime, mapMapLevelConfigureItem.EndTime) && list.Contains(mapMapLevelConfigureItem.Index))
				{
					list.Remove(mapMapLevelConfigureItem.Index);
				}
			}
		}
		return list;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.QueryRoomS2C.OnQueryRoomS2CServerCallBackAsync = OnQueryRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.QuickJoinRoomS2C.OnQuickJoinRoomS2CServerCallBackAsync = OnQuickJoinRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SearchRoomS2C.OnSearchRoomS2CServerCallBackAsync = OnSearchRoomS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.QueryRoomS2C.OnQueryRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.QuickJoinRoomS2C.OnQuickJoinRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SearchRoomS2C.OnSearchRoomS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestQueryRoomC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.QueryRoomC2S.QueryRoomC2SCall(new QueryRoomC2S
		{
			MapMod = mapModeType
		});
	}

	private async UniTask OnQueryRoomS2CServerCallBack(QueryRoomS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (!roomListDict.TryGetValue(mapModeType, out var value))
			{
				value = new RoomListData();
				roomListDict.TryAdd(mapModeType, value);
			}
			value.UpdateRoomList(model);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestQuickJoin()
	{
		List<int> selectedPVEDifficulty = LocalCache.GetSelectedPVEDifficulty();
		return MonoSingletonProvider<NetManager>.inst.RPC.QuickJoinRoomC2S.QuickJoinRoomC2SCall(new QuickJoinRoomC2S
		{
			MapMod = mapModeType,
			Difficulty = { (IEnumerable<int>)selectedPVEDifficulty }
		});
	}

	private async UniTask OnQuickJoinRoomS2CServerCallBack(QuickJoinRoomS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.DealJoinRoom(model.Room).Forget();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestJoinTargetRoom(long roomId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SearchRoomC2S.SearchRoomC2SCall(new SearchRoomC2S
		{
			RoomId = roomId
		});
	}

	private async UniTask OnSearchRoomS2CServerCallBack(SearchRoomS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomListPanel roomListPanel)
		{
			if (model.IsPwd)
			{
				roomListPanel.ShowInputPswTip(model.RoomId, model.RoomServerId);
				return;
			}
			roomListPanel.JoinPanel(new RoomShortInfo
			{
				IsPwd = model.IsPwd,
				Id = model.RoomId,
				PlayerCount = 1,
				RoomServerId = model.RoomServerId
			});
		}
		await UniTask.CompletedTask;
	}
}
