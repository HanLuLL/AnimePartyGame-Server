using FairyGUI.Utils;

namespace FairyGUI;

public class PlayTransitionAction : ControllerAction
{
	public string transitionName;

	public int playTimes;

	public float delay;

	public bool stopOnExit;

	private Transition _currentTransition;

	public PlayTransitionAction()
	{
		playTimes = 1;
		delay = 0f;
	}

	protected override void Enter(Controller controller)
	{
		Transition transition = controller.parent.GetTransition(transitionName);
		if (transition != null)
		{
			if (_currentTransition != null && _currentTransition.playing)
			{
				transition.ChangePlayTimes(playTimes);
			}
			else
			{
				transition.Play(playTimes, delay, null);
			}
			_currentTransition = transition;
		}
	}

	protected override void Leave(Controller controller)
	{
		if (stopOnExit && _currentTransition != null)
		{
			_currentTransition.Stop();
			_currentTransition = null;
		}
	}

	public override void Setup(ByteBuffer buffer)
	{
		base.Setup(buffer);
		transitionName = buffer.ReadS();
		playTimes = buffer.ReadInt();
		delay = buffer.ReadFloat();
		stopOnExit = buffer.ReadBool();
	}
}
