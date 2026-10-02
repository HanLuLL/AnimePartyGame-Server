using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core;

[CreateAssetMenu(fileName = "GachaDice Data", menuName = "Scriptable Object/GachaDice Data")]
public class GachaDiceData : ScriptableObject
{
	[Serializable]
	public struct FaceRelativeRotation
	{
		public DiceResult element;

		public List<Vector3> rotation;
	}

	public List<FaceRelativeRotation> faceRelativeRotation;
}
