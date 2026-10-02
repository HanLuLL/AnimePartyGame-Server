using UnityEngine;

namespace FairyGUI;

internal class TimersEngine : MonoBehaviour
{
	private void Update()
	{
		Timers.inst.Update();
	}
}
