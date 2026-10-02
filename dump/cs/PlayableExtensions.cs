using UnityEngine;
using UnityEngine.Playables;

public static class PlayableExtensions
{
	public static PlayableDirector GetPlayableDirector(this Playable playable)
	{
		IExposedPropertyTable resolver = playable.GetGraph().GetResolver();
		return (PlayableDirector)((resolver is PlayableDirector) ? resolver : null);
	}

	public static T GetCustomComponent<T>(this Playable playable)
	{
		((Component)(object)playable.GetPlayableDirector()).TryGetComponent(out T component);
		return component;
	}
}
