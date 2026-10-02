using GameLogic.PlotTree;
using UnityEngine;

namespace GameLogic;

public class StoryData : ScriptableObject
{
	[SerializeReference]
	public PlotTreeBase Root;

	private void OnEnable()
	{
	}
}
