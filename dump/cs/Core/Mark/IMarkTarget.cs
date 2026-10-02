using UnityEngine;

namespace Core.Mark;

public interface IMarkTarget
{
	bool HoverWait { get; }

	void OnMarkHoverEnter();

	void OnMarkHoverExit();

	void OnMarkSelected();

	void TriggerHoverConfirmed();

	Vector2 GetPosition();
}
