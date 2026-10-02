using UnityEngine;

namespace FairyGUI;

public class TransitionAsyncResult : CustomYieldInstruction
{
	public bool isCompleted { get; private set; }

	public override bool keepWaiting => !isCompleted;

	public TransitionAsyncResult()
	{
		isCompleted = false;
	}

	public void Complete()
	{
		isCompleted = true;
	}
}
