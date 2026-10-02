using SinglePlayer.GamePlay;
using Tools;
using UnityEngine;

namespace SinglePlayer.Tools;

public class MonoBehaviourManager : MonoBehaviour, ISystem
{
	public readonly Signal Update0 = new Signal(48);

	public readonly Signal<bool, bool, bool> Update1 = new Signal<bool, bool, bool>(48);

	public readonly Signal fixedUpdate = new Signal();

	public bool IsPressedDown;

	public bool IsReleased;

	public bool IsHolding;

	public Vector3 MouseTouchPosition { get; private set; }

	private void Update()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Invalid comparison between Unknown and I4
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Invalid comparison between Unknown and I4
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Invalid comparison between Unknown and I4
		Touch touch;
		int isPressedDown;
		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
			isPressedDown = (((int)((Touch)(ref touch)).phase == 0) ? 1 : 0);
		}
		else
		{
			isPressedDown = 0;
		}
		IsPressedDown = (byte)isPressedDown != 0;
		int isReleased;
		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
			if ((int)((Touch)(ref touch)).phase != 3)
			{
				touch = Input.GetTouch(0);
				isReleased = (((int)((Touch)(ref touch)).phase == 4) ? 1 : 0);
			}
			else
			{
				isReleased = 1;
			}
		}
		else
		{
			isReleased = 0;
		}
		IsReleased = (byte)isReleased != 0;
		IsHolding = Input.touchCount > 0;
		if (Input.touchCount > 0)
		{
			Touch touch2 = Input.GetTouch(0);
			if ((int)((Touch)(ref touch2)).phase == 0)
			{
				Game.GetModel<GlobalSignal>()?.Down.Dispatch();
			}
			if ((int)((Touch)(ref touch2)).phase == 3)
			{
				Game.GetModel<GlobalSignal>()?.Up.Dispatch();
				MouseTouchPosition = Vector2.zero;
			}
			TouchPhase phase = ((Touch)(ref touch2)).phase;
			if ((int)phase == 0 || (int)phase == 1 || (int)phase == 2)
			{
				MouseTouchPosition = ((Touch)(ref touch2)).position;
			}
		}
		Update0.Dispatch();
		Update1.Dispatch(IsPressedDown, IsReleased, IsHolding);
	}

	private void FixedUpdate()
	{
		fixedUpdate.Dispatch();
	}
}
