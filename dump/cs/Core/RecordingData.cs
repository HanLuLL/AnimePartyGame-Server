using System.Collections.Generic;
using UnityEngine;

namespace Core;

public struct RecordingData
{
	public Rigidbody rb;

	public Vector3 initialPosition;

	public Quaternion initialRotation;

	public List<RecordedFrame> recordedFrames;

	public RecordingData(Rigidbody rb, Vector3 initialPosition, Quaternion initialRotation)
	{
		this.rb = rb;
		this.initialPosition = initialPosition;
		this.initialRotation = initialRotation;
		recordedFrames = new List<RecordedFrame>();
	}
}
