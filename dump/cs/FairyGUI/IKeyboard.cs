namespace FairyGUI;

public interface IKeyboard
{
	bool done { get; }

	bool supportsCaret { get; }

	string GetInput();

	void Open(string text, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int keyboardType, bool hideInput);

	void Close();
}
