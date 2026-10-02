using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

[Serializable]
public class GachaDiceManager : MonoBehaviour
{
	[SerializeField]
	public CustomRange rangeCom;

	[SerializeField]
	public GameObject dicePrefab;

	private GachaDicePool _pool;

	private List<GachaDice> _diceDataList = new List<GachaDice>();

	public List<RecordingData> RecordingDataList = new List<RecordingData>();

	[SerializeField]
	public int recordingFrameLength = 150;

	public float Force = 5f;

	public Vector2 RangeTorque = new Vector2(0f, 100f);

	[SerializeField]
	public float gravityScale = 50f;

	private List<DiceResult> diceResult;

	private CancellationTokenSource _cancelToken;

	private readonly List<Vector3> _DiceTargetPos = new List<Vector3>(10);

	public int _DiceShowTime = 1000;

	public float _DiceRotateTime = 0.2f;

	public float _DiceMoveTime = 0.2f;

	public float _DiceDistance = 25f;

	public float _DicePadding = 5f;

	public float _DiceScale = 5f;

	public float _OffsetRowHeight = 3f;

	private void Awake()
	{
		_pool = new GachaDicePool(dicePrefab);
	}

	private void GenerateDice(int count)
	{
		ReleaseDice();
		for (int i = 0; i < count; i++)
		{
			GachaDice gachaDice = _pool.Get();
			(Vector3, Quaternion, Vector3, Vector3) tuple = SetInitialState();
			gachaDice.transform.position = tuple.Item1;
			gachaDice.transform.rotation = tuple.Item2;
			gachaDice.EnablePhysics();
			gachaDice.rb.velocity = tuple.Item3;
			gachaDice.rb.AddTorque(tuple.Item4, (ForceMode)2);
			gachaDice.SetDiceMesh(status: false);
			_diceDataList.Add(gachaDice);
		}
	}

	private (Vector3, Quaternion, Vector3, Vector3) SetInitialState()
	{
		(Vector3, Vector3) random = rangeCom.GetRandom();
		Vector3 item = random.Item1;
		Vector3 item2 = random.Item2 * Force;
		Quaternion item3 = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
		Vector3 item4 = new Vector3(UnityEngine.Random.Range(RangeTorque.x, RangeTorque.y), UnityEngine.Random.Range(RangeTorque.x, RangeTorque.y), UnityEngine.Random.Range(RangeTorque.x, RangeTorque.y));
		return (item, item3, item2, item4);
	}

	public void ReleaseDice()
	{
		if (_pool != null && _diceDataList.Count != 0)
		{
			for (int i = 0; i < _diceDataList.Count; i++)
			{
				_pool.Release(_diceDataList[i]);
			}
			_diceDataList.Clear();
		}
	}

	public void DisposeDice()
	{
		if (_pool != null)
		{
			_pool.OnDestroy();
			_diceDataList.Clear();
		}
	}

	private void GetInitialState()
	{
		RecordingDataList.Clear();
		foreach (GachaDice diceData in _diceDataList)
		{
			RecordingData item = new RecordingData(diceData.rb, diceData.transform.position, diceData.transform.rotation);
			RecordingDataList.Add(item);
		}
	}

	private void StartRecording()
	{
		Physics.autoSimulation = false;
		for (int i = 0; i < recordingFrameLength; i++)
		{
			for (int j = 0; j < _diceDataList.Count; j++)
			{
				_diceDataList[j].rb.AddForce(Physics.gravity * gravityScale * Time.fixedDeltaTime, (ForceMode)5);
				Vector3 position = _diceDataList[j].transform.position;
				Quaternion rotation = _diceDataList[j].transform.rotation;
				bool isContactWithFloor = _diceDataList[j].isContactWithFloor;
				bool isContactWithDice = _diceDataList[j].isContactWithDice;
				bool isNotMoving = _diceDataList[j].CheckObjectHasStopped() || recordingFrameLength == i + 1;
				RecordedFrame item = new RecordedFrame(position, rotation, isContactWithFloor, isContactWithDice, isNotMoving);
				RecordingDataList[j].recordedFrames.Add(item);
			}
			Physics.Simulate(Time.fixedDeltaTime);
		}
		Physics.autoSimulation = true;
	}

	private void ResetToInitialState()
	{
		for (int i = 0; i < _diceDataList.Count; i++)
		{
			_diceDataList[i].SetDiceResult(RecordingDataList[i].initialPosition, RecordingDataList[i].initialRotation, (int)diceResult[i]);
		}
	}

	private async UniTask PlayAnimation(CancellationToken cancellationToken)
	{
		foreach (GachaDice diceData in _diceDataList)
		{
			diceData.SetDiceMesh(status: true);
		}
		for (int i = 0; i < recordingFrameLength; i++)
		{
			for (int j = 0; j < RecordingDataList.Count; j++)
			{
				if (j >= RecordingDataList.Count || j >= _diceDataList.Count)
				{
					Debug.LogWarning($"当前索引地址：j={j}, RecordingDataList.Count:{RecordingDataList.Count}, _diceDataList.Count:{_diceDataList.Count}");
					break;
				}
				if (i >= RecordingDataList[j].recordedFrames.Count)
				{
					Debug.LogWarning($"当前索引地址：i={i}, RecordingDataList[{j}].recordedFrames.Count:{RecordingDataList[j].recordedFrames.Count}");
					break;
				}
				_diceDataList[j].transform.position = RecordingDataList[j].recordedFrames[i].position;
				_diceDataList[j].transform.rotation = RecordingDataList[j].recordedFrames[i].rotation;
				if (RecordingDataList[j].recordedFrames[i].isContactWithArena)
				{
					_diceDataList[j].PlaySoundTouchFloor();
				}
				if (RecordingDataList[j].recordedFrames[i].isContactWithDice)
				{
					_diceDataList[j].PlaySoundTouchDice();
				}
				if (RecordingDataList[j].recordedFrames[i].isNotMoving)
				{
					_diceDataList[j].ShowDiceResult();
				}
			}
			await UniTask.WaitForFixedUpdate(cancellationToken);
		}
	}

	private void ShowDiceResult()
	{
		for (int i = 0; i < RecordingDataList.Count; i++)
		{
			List<RecordedFrame> recordedFrames = RecordingDataList[i].recordedFrames;
			RecordedFrame recordedFrame = recordedFrames[recordedFrames.Count - 1];
			_diceDataList[i].transform.position = recordedFrame.position;
			_diceDataList[i].transform.rotation = recordedFrame.rotation;
			_diceDataList[i].ShowDiceResult();
		}
	}

	public void KillGachaShow()
	{
		for (int i = 0; i < _diceDataList.Count; i++)
		{
			_diceDataList[i].KillTweener();
		}
	}

	public async UniTask ShowGachaDiceResult(CancellationToken cancellationToken)
	{
		Transform transform = UnityEngine.Camera.main.transform;
		CreateGachaDicePos(_diceDataList.Count, transform);
		for (int i = 0; i < _diceDataList.Count; i++)
		{
			_diceDataList[i].ResetDoAnimation(_DiceTargetPos[i], _DiceMoveTime, _DiceRotateTime, transform);
		}
		await UniTask.Delay(_DiceShowTime, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
	}

	private void CreateGachaDicePos(int diceNum, Transform gachaCamera)
	{
		_DiceTargetPos.Clear();
		int num = 1;
		int num2 = 1;
		if (diceNum == 10)
		{
			num = 5;
			num2 = 2;
		}
		Vector3 vector = gachaCamera.forward * _DiceDistance - Vector3.right * _DiceScale * (num - 1) / 2f - Vector3.up * _DiceScale * (num2 - 1) / 2f;
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector2 = ((diceNum != 10) ? (Vector3.up * _OffsetRowHeight) : (Vector3.up * _OffsetRowHeight * (i - 1) / 2f));
			for (int j = 0; j < num; j++)
			{
				Vector3 item = gachaCamera.position + vector + vector2 + new Vector3((float)j * _DicePadding, 0f, (float)(-i) * _DicePadding);
				_DiceTargetPos.Add(item);
			}
		}
	}

	public void ReadyGachaDice(int count, List<DiceResult> results)
	{
		diceResult = results;
		GenerateDice(count);
		GetInitialState();
		StartRecording();
		ResetToInitialState();
	}

	public async void ThrowDice()
	{
		_cancelToken?.Dispose();
		_cancelToken = new CancellationTokenSource();
		bool flag = await PlayAnimation(_cancelToken.Token).SuppressCancellationThrow();
		if (!flag)
		{
			_cancelToken?.Dispose();
			_cancelToken = new CancellationTokenSource();
			flag = await ShowGachaDiceResult(_cancelToken.Token).SuppressCancellationThrow();
			if (flag)
			{
				for (int i = 0; i < _diceDataList.Count; i++)
				{
					_diceDataList[i].KillTweener();
				}
			}
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.gacha != null && SimpleSingletonProvider<UIManager>.inst.currentPanel is GachaPanel)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.gacha.signal.finishGacha.Dispatch(flag);
		}
	}

	public void CancelThrowDice()
	{
		_cancelToken?.Cancel();
		_cancelToken?.Dispose();
		_cancelToken = null;
	}
}
