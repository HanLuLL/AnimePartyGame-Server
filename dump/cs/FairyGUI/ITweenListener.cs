namespace FairyGUI;

public interface ITweenListener
{
	void OnTweenStart(GTweener tweener);

	void OnTweenUpdate(GTweener tweener);

	void OnTweenComplete(GTweener tweener);
}
