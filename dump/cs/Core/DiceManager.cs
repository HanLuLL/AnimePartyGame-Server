using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class DiceManager : SimpleSingletonProvider<DiceManager>
{
	private DiceControllerPool _diceControllerPool;

	private readonly Dictionary<string, BattleDice> _BattleDiceDict = new Dictionary<string, BattleDice>();

	private readonly Dictionary<string, AsyncOperationHandle<IList<GameObject>>> _DiceHandleDict = new Dictionary<string, AsyncOperationHandle<IList<GameObject>>>();

	private async UniTask<DiceController> GetDiceController()
	{
		DiceController result;
		if (_diceControllerPool != null)
		{
			result = _diceControllerPool.Get();
		}
		else
		{
			AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>("DiceController");
			if (_diceControllerPool == null)
			{
				_diceControllerPool = new DiceControllerPool(asyncOperationHandle);
			}
			else
			{
				Addressables.Release(asyncOperationHandle);
			}
			result = _diceControllerPool.Get();
		}
		return result;
	}

	public void DisposeController()
	{
		if (_diceControllerPool != null)
		{
			_diceControllerPool?.OnDestroy();
			_diceControllerPool = null;
		}
	}

	public void StopDiceController(DiceController diceController)
	{
		_diceControllerPool?.Release(diceController);
	}

	public void LoadDiceAsset(GameObject diceObject, string DiceModel)
	{
		if (diceObject == null)
		{
			Debug.LogError("加载" + DiceModel + "骰子资源结果位null");
		}
		else if (!_BattleDiceDict.ContainsKey(diceObject.name))
		{
			_BattleDiceDict.Add(diceObject.name, new BattleDice(diceObject, DiceModel));
		}
	}

	public async UniTask LoadDice(string label)
	{
		if (_DiceHandleDict.ContainsKey(label))
		{
			return;
		}
		AsyncOperationHandle<IList<GameObject>> value = await AddressableHelper.LoadAssetsAsync<GameObject>(new List<string> { label });
		foreach (GameObject item in value.Result)
		{
			if (!_BattleDiceDict.ContainsKey(item.name))
			{
				_BattleDiceDict.Add(item.name, new BattleDice(item, item.name));
			}
			else
			{
				Debug.LogError("重复加载" + item.name + "骰子资源");
			}
		}
		_DiceHandleDict.Add(label, value);
	}

	public GameObject GetDiceObject(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		if (!_BattleDiceDict.TryGetValue(key, out var value))
		{
			Debug.LogError("无法通过" + key + "找到骰子资源");
			return null;
		}
		return value.GetDice();
	}

	public void StopDice(string diceKey, GameObject diceGameObject)
	{
		if (_BattleDiceDict.TryGetValue(diceKey, out var value))
		{
			value.StopDice(diceGameObject);
		}
	}

	public void DestroyPool()
	{
		foreach (KeyValuePair<string, BattleDice> item in _BattleDiceDict)
		{
			item.Value.DestroyPool();
		}
		DisposeController();
	}

	public void Dispose()
	{
		foreach (KeyValuePair<string, BattleDice> item in _BattleDiceDict)
		{
			item.Value.DestroyPool();
		}
		_BattleDiceDict.Clear();
		foreach (var (_, handle) in _DiceHandleDict)
		{
			if (handle.IsValid())
			{
				Addressables.Release(handle);
			}
		}
		_DiceHandleDict.Clear();
		DisposeController();
		OnDestroyInstance();
	}

	public async UniTask<bool> ThrowDice(long _playerId, RepeatedField<int> Points, int movePoint, bool controlled)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (playerDataById == null || playerDataById.CharacterInst == null)
		{
			return true;
		}
		Vector3 playerPos = playerDataById.CharacterInst.transform.position;
		if (Points.Count == 1)
		{
			DiceController obj = await GetDiceController();
			obj.ReadyDiceData(_playerId, Points[0], new Vector3(playerPos.x, playerPos.y + 48f, playerPos.z));
			if (await obj.ShowDice())
			{
				return false;
			}
		}
		else
		{
			DiceController diceController_1 = await GetDiceController();
			DiceController diceController = await GetDiceController();
			diceController_1.ReadyDiceData(_playerId, Points[0], new Vector3(playerPos.x - 10f, playerPos.y + 48f, playerPos.z - 10f));
			diceController.ReadyDiceData(_playerId, Points[1], new Vector3(playerPos.x + 10f, playerPos.y + 48f, playerPos.z + 10f));
			UniTask[] tasks = new UniTask[2]
			{
				diceController_1.ShowDice(),
				diceController.ShowDice()
			};
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(tasks))
			{
				return false;
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.ThrowDice.Dispatch(_playerId, Points.Sum(), movePoint, controlled);
		return true;
	}
}
