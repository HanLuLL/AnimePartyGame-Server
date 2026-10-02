using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic.PlotTree;

[Serializable]
public class DialogNode : PlotActionNode
{
	[Serializable]
	public class DialogPerform
	{
		public string Guid;

		public int id;

		public string emoji;

		public Vector2 emojiOffset;

		public Vector2 emojiFlipOffset;

		public string UITexture;

		public string SfwUITexture;

		public int SoundId;

		public float Delay;

		public Vector2 StartPosition;

		public Vector2 EndPosition;

		public float StartAlpha = 1f;

		public float EndAlpha = 1f;

		public float StartScale = 1f;

		public float EndScale = 1f;

		public bool Flip;
	}

	public int SpeakerId;

	public int DialogId;

	public string DialogStyle;

	public string AudioIds;

	public string ImpulseSource;

	[SerializeReference]
	public List<DialogPerform> Performs = new List<DialogPerform>();

	public bool Auto = true;

	[SerializeReference]
	public List<DialogReadTime> readTimes = new List<DialogReadTime>();

	public VideoType VideoType;

	public int VideoId;

	public override PlotState Tick()
	{
		NodeState = PlotState.Success;
		SimpleSingletonProvider<UIManager>.inst.story.TryShowDialog(this).Forget();
		return NodeState;
	}
}
