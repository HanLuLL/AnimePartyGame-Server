namespace UI;

public interface ILoginComponent
{
	void OnLogin();

	void Show(LoginPanel panel);

	void Refresh();

	void AddEvent();

	void RemoveEvent();

	void DisposeUI();
}
