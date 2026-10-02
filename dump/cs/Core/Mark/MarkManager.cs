using Core.Unit;

namespace Core.Mark;

public class MarkManager : Core.Unit.Unit
{
	private MarkModeController _markModeController;

	private MarkDetectionSystem _detectionSystem;

	public IMarkInput MarkInput { get; private set; }

	public bool IsActive => _markModeController.IsActive;

	protected override void Awake()
	{
		_markModeController = new MarkModeController();
		MarkInput = new MobileMarkInput(_markModeController);
		_detectionSystem = new MarkDetectionSystem();
		base.Awake();
	}

	private void OnEnable()
	{
		_detectionSystem?.OnEnable();
	}

	private void OnDisable()
	{
		_detectionSystem?.OnDisable();
	}

	public void Update()
	{
		MarkInput?.Update();
		if (_markModeController.IsActive)
		{
			_detectionSystem.Update();
		}
	}
}
