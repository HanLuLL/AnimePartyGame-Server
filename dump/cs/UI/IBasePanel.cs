using Tools;

namespace UI;

public interface IBasePanel
{
	UIPanelConfigure config { get; }

	Signal onShown { get; }

	Signal onClose { get; }

	void Show(params object[] objs);

	void Refresh();

	void Close();

	void Dispose();

	bool IsOpen();

	void SetTouchable(bool touchable);

	void LoseFocus();

	void ResumeFocus();

	void PlayBGM();

	void SetUIVisible(bool status);

	void CoverMode(bool inCoverMode);

	void AdultMode(bool inAdultMode);
}
