using Core;
using Core.Audio;
using FairyGUI;
using Tools;

namespace GameLogic;

public class RoomBattleBGM
{
	private long _BGMOwner;

	private int _BGMId;

	private bool _LockStatus;

	private int _map_BGMId;

	private uint Fight_BGMId;

	public int Map_BGMId
	{
		get
		{
			if (_map_BGMId == 0)
			{
				RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
				if (curRoomInfo == null || curRoomInfo.SceneConfig == null)
				{
					return 0;
				}
				_map_BGMId = curRoomInfo.SceneConfig.Bgm;
			}
			return _map_BGMId;
		}
		set
		{
			_map_BGMId = value;
		}
	}

	public void ResetBGM()
	{
		_BGMOwner = 0L;
		_map_BGMId = 0;
	}

	public void PlayBattleBGM(long playerId, bool reset = false)
	{
		if (reset)
		{
			ResetBGM();
		}
		if (!_LockStatus && (_BGMOwner == 0L || playerId == _BGMOwner))
		{
			_BGMId = Map_BGMId;
			_BGMOwner = 0L;
		}
		BGMHelper.TryPlayBGM(_BGMId);
	}

	public void TryPlayCharacterBGM(long playerId, int bgmId, bool lockStatus = false)
	{
		_BGMOwner = playerId;
		_BGMId = bgmId;
		_LockStatus = lockStatus;
		BGMHelper.TryPlayBGM(_BGMId);
	}

	public void PlayFightBGM(int fightConfigId)
	{
		if (Fight_BGMId == 0)
		{
			Fight_BGMId = SimpleSingletonProvider<AudioManager>.inst.SendEvent(fightConfigId, Stage.inst.gameObject);
		}
	}

	public void ContinueBGMAfterFight()
	{
		if (Fight_BGMId != 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(Fight_BGMId);
			Fight_BGMId = 0u;
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(109, Stage.inst.gameObject);
		}
	}
}
