using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameLogic;

public class SubMapSwitchController : MonoBehaviour
{
	[SerializeField]
	protected List<SubMap> subMapList = new List<SubMap>();

	[SerializeField]
	private List<AudioInfo> audioInfos = new List<AudioInfo>();

	[SerializeField]
	private float tranEffectDruation = 10f;

	[SerializeField]
	protected Transform vfxRoot;

	protected int currentFromId = -1;

	protected int currentToId = -1;

	protected float timer;

	public const int SubMapStencilRef = 8;

	[HideInInspector]
	public bool IsPlaying;

	private void OnSwitchFinish()
	{
		if (currentFromId >= 0 && currentToId >= 0)
		{
			SubMap subMap = subMapList[currentFromId];
			subMapList[currentToId].SetStencil(0, UnityEngine.Rendering.CompareFunction.Disabled);
			subMap.SetStencil(0, UnityEngine.Rendering.CompareFunction.Disabled);
			subMap.MapRoot.gameObject.SetActive(value: false);
			vfxRoot.gameObject.SetActive(value: false);
			currentFromId = -1;
			currentToId = -1;
		}
	}

	private void Update()
	{
		TryFinishSwitch();
	}

	protected virtual void TryFinishSwitch()
	{
		if (currentFromId >= 0 && currentToId >= 0)
		{
			float lastFrameTime = timer;
			timer += Time.deltaTime;
			IsPlaying = timer < tranEffectDruation;
			TriggerEvent(lastFrameTime, timer, currentFromId, currentToId);
			if (!IsPlaying)
			{
				OnSwitchFinish();
			}
		}
	}

	public virtual void TriggerEvent(float lastFrameTime, float currentFrameTime, int fromId, int toId)
	{
	}

	private void OnEnable()
	{
		vfxRoot.gameObject.SetActive(value: false);
	}

	private void OnDisable()
	{
		OnSwitchFinish();
	}

	protected async UniTask PlayAudio(int toId)
	{
		if (toId >= 0 && toId < audioInfos.Count)
		{
			AudioInfo audioInfo = audioInfos[toId];
			int audioEventID = audioInfo.Id;
			bool flag = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(audioInfo.DelayTime));
			if (audioEventID != 0 && !flag)
			{
				Stage.inst.PlayOneShotSound(audioEventID);
			}
		}
	}

	public virtual void StartSubMapSwitch(int fromId, int toId)
	{
		OnSwitchFinish();
		vfxRoot.gameObject.SetActive(value: true);
		IsPlaying = true;
		currentFromId = fromId;
		currentToId = toId;
		timer = 0f;
		SubMap subMap = subMapList[fromId];
		SubMap subMap2 = subMapList[toId];
		subMap.SetStencil(8, UnityEngine.Rendering.CompareFunction.NotEqual);
		subMap2.SetStencil(8, UnityEngine.Rendering.CompareFunction.Equal);
		subMap2.MapRoot.gameObject.SetActive(value: true);
		PlayAudio(toId).Forget();
	}

	protected virtual void OnDestroy()
	{
		subMapList.ForEach(delegate(SubMap map)
		{
			map.OnDestory();
		});
	}
}
