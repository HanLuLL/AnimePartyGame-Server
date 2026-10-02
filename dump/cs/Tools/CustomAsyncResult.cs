using UnityEngine;

namespace Tools;

public class CustomAsyncResult : CustomYieldInstruction
{
	public bool isCompleted { get; private set; }

	public override bool keepWaiting => !isCompleted;

	public CustomAsyncResult()
	{
		isCompleted = false;
	}

	public void Complete()
	{
		isCompleted = true;
	}
}
