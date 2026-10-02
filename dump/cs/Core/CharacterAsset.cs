using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace Core;

public class CharacterAsset : ScriptableObject
{
	[SerializeField]
	public List<AnimationClip> animationClips;

	[SerializeField]
	public List<TimelineAsset> timelineAssets;

	[SerializeField]
	public RuntimeAnimatorController animationController;

	public AsyncOperationHandle<object> loadHandle;
}
