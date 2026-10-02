using System;
using Core.Scene;
using GameLogic;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class BattleShowControlBehaviour : PlayableBehaviour
{
	[SerializeField]
	public bool isFollow;

	[SerializeField]
	public bool NoPeriodic;

	[SerializeField]
	public Vector3 offset = Vector3.zero;

	[SerializeField]
	public GameObject prefabGameObject;

	private PlayableDirector _director;

	private PlayableDirector[] _subDirectors;

	private GameObject m_Instance;

	public override void OnPlayableCreate(Playable playable)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		if (prefabGameObject != null && m_Instance == null)
		{
			m_Instance = UnityEngine.Object.Instantiate(prefabGameObject, ((Component)(object)_director).transform.position + offset, Quaternion.identity);
			_subDirectors = m_Instance.transform.GetComponentsInChildren<PlayableDirector>();
			if (isFollow)
			{
				for (int i = 0; i < _subDirectors.Length; i++)
				{
					PlayableDirector val = _subDirectors[i];
					if (!((UnityEngine.Object)(object)val == null))
					{
						val.playOnAwake = false;
						val.timeUpdateMode = DirectorUpdateMode.Manual;
					}
				}
			}
			m_Instance.SetActive(value: false);
			if (NoPeriodic)
			{
				BattleSceneController.inst.directorManager.BattleEffect.TryAddBattleElement(m_Instance);
			}
		}
		base.OnPlayableCreate(playable);
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (!(m_Instance != null))
		{
			return;
		}
		m_Instance.SetActive(value: true);
		if (isFollow)
		{
			if (m_Instance.transform.parent == null)
			{
				m_Instance.transform.parent = ((Component)(object)_director).transform;
			}
		}
		else if (_subDirectors != null)
		{
			for (int i = 0; i < _subDirectors.Length; i++)
			{
				PlayableDirector val = _subDirectors[i];
				if ((UnityEngine.Object)(object)val == null)
				{
					continue;
				}
				val.Play();
				if (val.playableGraph.IsValid())
				{
					Playable rootPlayable = val.playableGraph.GetRootPlayable(0);
					if (rootPlayable.IsValid())
					{
						rootPlayable.SetSpeed(BattleConfig.RoleAnimatorSpeed);
					}
				}
			}
		}
		Animator[] componentsInChildren = m_Instance.transform.GetComponentsInChildren<Animator>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].cullingMode = (AnimatorCullingMode)0;
			componentsInChildren[j].speed = BattleConfig.RoleAnimatorSpeed;
		}
		m_Instance.transform.GetComponent<GameEffect>()?.Play(null, null);
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if (m_Instance != null && !NoPeriodic && _director.extrapolationMode != DirectorWrapMode.Loop)
		{
			m_Instance.SetActive(value: false);
		}
		base.OnBehaviourPause(playable, info);
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
		if (m_Instance != null && !m_Instance.activeInHierarchy)
		{
			m_Instance.SetActive(value: true);
		}
		base.PrepareFrame(playable, info);
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		base.ProcessFrame(playable, info, playerData);
		if (!isFollow || !(m_Instance != null) || !m_Instance.activeInHierarchy || _subDirectors == null)
		{
			return;
		}
		double time = playable.GetTime();
		for (int i = 0; i < _subDirectors.Length; i++)
		{
			PlayableDirector val = _subDirectors[i];
			if (!((UnityEngine.Object)(object)val == null))
			{
				val.time = time;
				val.Evaluate();
			}
		}
	}

	public override void OnPlayableDestroy(Playable playable)
	{
		if (m_Instance != null && !NoPeriodic)
		{
			UnityEngine.Object.Destroy(m_Instance);
		}
		base.OnPlayableDestroy(playable);
	}
}
