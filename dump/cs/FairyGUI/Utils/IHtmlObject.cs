namespace FairyGUI.Utils;

public interface IHtmlObject
{
	float width { get; }

	float height { get; }

	DisplayObject displayObject { get; }

	HtmlElement element { get; }

	void Create(RichTextField owner, HtmlElement element);

	void SetPosition(float x, float y);

	void Add();

	void Remove();

	void Release();

	void Dispose();
}
