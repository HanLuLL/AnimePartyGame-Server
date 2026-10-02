using System;
using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class RichTextField : Container
{
	public IHtmlPageContext htmlPageContext { get; set; }

	public HtmlParseOptions htmlParseOptions { get; private set; }

	public Dictionary<uint, Emoji> emojies { get; set; }

	public TextField textField { get; private set; }

	public virtual string text
	{
		get
		{
			return textField.text;
		}
		set
		{
			textField.text = value;
		}
	}

	public virtual string htmlText
	{
		get
		{
			return textField.htmlText;
		}
		set
		{
			textField.htmlText = value;
		}
	}

	public virtual TextFormat textFormat
	{
		get
		{
			return textField.textFormat;
		}
		set
		{
			textField.textFormat = value;
		}
	}

	public int htmlElementCount => textField.htmlElements.Count;

	public RichTextField()
	{
		base.gameObject.name = "RichTextField";
		opaque = true;
		htmlPageContext = HtmlPageContext.inst;
		htmlParseOptions = new HtmlParseOptions();
		textField = new TextField();
		textField.EnableRichSupport(this);
		AddChild(textField);
	}

	public HtmlElement GetHtmlElement(string name)
	{
		List<HtmlElement> htmlElements = textField.htmlElements;
		int count = htmlElements.Count;
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = htmlElements[i];
			if (name.Equals(htmlElement.name, StringComparison.OrdinalIgnoreCase))
			{
				return htmlElement;
			}
		}
		return null;
	}

	public HtmlElement GetHtmlElementAt(int index)
	{
		return textField.htmlElements[index];
	}

	public void ShowHtmlObject(int index, bool show)
	{
		HtmlElement htmlElement = textField.htmlElements[index];
		if (htmlElement.htmlObject == null || htmlElement.type == HtmlElementType.Link)
		{
			return;
		}
		if (show)
		{
			htmlElement.status &= 253;
		}
		else
		{
			htmlElement.status |= 2;
		}
		if ((htmlElement.status & 3) == 0)
		{
			if ((htmlElement.status & 4) == 0)
			{
				htmlElement.status |= 4;
				htmlElement.htmlObject.Add();
			}
		}
		else if ((htmlElement.status & 4) != 0)
		{
			htmlElement.status &= 251;
			htmlElement.htmlObject.Remove();
		}
	}

	public override void EnsureSizeCorrect()
	{
		textField.EnsureSizeCorrect();
	}

	protected override void OnSizeChanged()
	{
		textField.size = _contentRect.size;
		base.OnSizeChanged();
	}

	public override void Update(UpdateContext context)
	{
		textField.Redraw();
		base.Update(context);
	}

	public override void Dispose()
	{
		if ((_flags & Flags.Disposed) == 0)
		{
			CleanupObjects();
			base.Dispose();
		}
	}

	internal void CleanupObjects()
	{
		List<HtmlElement> htmlElements = textField.htmlElements;
		int count = htmlElements.Count;
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = htmlElements[i];
			if (htmlElement.htmlObject != null)
			{
				htmlElement.htmlObject.Remove();
				htmlPageContext.FreeObject(htmlElement.htmlObject);
			}
		}
	}

	internal virtual void RefreshObjects()
	{
		List<HtmlElement> htmlElements = textField.htmlElements;
		int count = htmlElements.Count;
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = htmlElements[i];
			if (htmlElement.htmlObject == null)
			{
				continue;
			}
			if ((htmlElement.status & 3) == 0)
			{
				if ((htmlElement.status & 4) == 0)
				{
					htmlElement.status |= 4;
					htmlElement.htmlObject.Add();
				}
			}
			else if ((htmlElement.status & 4) != 0)
			{
				htmlElement.status &= 251;
				htmlElement.htmlObject.Remove();
			}
		}
	}
}
