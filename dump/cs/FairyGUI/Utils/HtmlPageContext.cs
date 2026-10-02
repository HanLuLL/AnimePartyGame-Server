using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlPageContext : IHtmlPageContext
{
	private Stack<IHtmlObject> _imagePool;

	private Stack<IHtmlObject> _inputPool;

	private Stack<IHtmlObject> _buttonPool;

	private Stack<IHtmlObject> _selectPool;

	private Stack<IHtmlObject> _linkPool;

	public static HtmlPageContext inst = new HtmlPageContext();

	private static Transform _poolManager;

	public HtmlPageContext()
	{
		_imagePool = new Stack<IHtmlObject>();
		_inputPool = new Stack<IHtmlObject>();
		_buttonPool = new Stack<IHtmlObject>();
		_selectPool = new Stack<IHtmlObject>();
		_linkPool = new Stack<IHtmlObject>();
		if (Application.isPlaying && _poolManager == null)
		{
			_poolManager = Stage.inst.CreatePoolManager("HtmlObjectPool");
		}
	}

	public virtual IHtmlObject CreateObject(RichTextField owner, HtmlElement element)
	{
		IHtmlObject htmlObject = null;
		bool flag = false;
		if (element.type == HtmlElementType.Image)
		{
			if (_imagePool.Count > 0 && _poolManager != null)
			{
				htmlObject = _imagePool.Pop();
				flag = true;
			}
			else
			{
				htmlObject = new HtmlImage();
			}
		}
		else if (element.type == HtmlElementType.Link)
		{
			if (_linkPool.Count > 0 && _poolManager != null)
			{
				htmlObject = _linkPool.Pop();
				flag = true;
			}
			else
			{
				htmlObject = new HtmlLink();
			}
		}
		else if (element.type == HtmlElementType.Input)
		{
			string text = element.GetString("type");
			if (text != null)
			{
				text = text.ToLower();
			}
			if (text == "button" || text == "submit")
			{
				if (_buttonPool.Count > 0 && _poolManager != null)
				{
					htmlObject = _buttonPool.Pop();
					flag = true;
				}
				else
				{
					htmlObject = new HtmlButton();
				}
			}
			else if (_inputPool.Count > 0 && _poolManager != null)
			{
				htmlObject = _inputPool.Pop();
				flag = true;
			}
			else
			{
				htmlObject = new HtmlInput();
			}
		}
		else if (element.type == HtmlElementType.Select)
		{
			if (_selectPool.Count > 0 && _poolManager != null)
			{
				htmlObject = _selectPool.Pop();
				flag = true;
			}
			else
			{
				htmlObject = new HtmlSelect();
			}
		}
		if (htmlObject != null)
		{
			if (flag && htmlObject.displayObject != null && htmlObject.displayObject.isDisposed)
			{
				htmlObject.Dispose();
				return CreateObject(owner, element);
			}
			htmlObject.Create(owner, element);
			if (htmlObject.displayObject != null)
			{
				htmlObject.displayObject.home = owner.cachedTransform;
			}
		}
		return htmlObject;
	}

	public virtual void FreeObject(IHtmlObject obj)
	{
		if (_poolManager == null)
		{
			obj.Dispose();
			return;
		}
		if (obj.displayObject != null && obj.displayObject.isDisposed)
		{
			obj.Dispose();
			return;
		}
		obj.Release();
		if (obj is HtmlImage)
		{
			_imagePool.Push(obj);
		}
		else if (obj is HtmlInput)
		{
			_inputPool.Push(obj);
		}
		else if (obj is HtmlButton)
		{
			_buttonPool.Push(obj);
		}
		else if (obj is HtmlLink)
		{
			_linkPool.Push(obj);
		}
		if (obj.displayObject != null)
		{
			obj.displayObject.cachedTransform.SetParent(_poolManager, worldPositionStays: false);
		}
	}

	public virtual NTexture GetImageTexture(HtmlImage image)
	{
		return null;
	}

	public virtual void FreeImageTexture(HtmlImage image, NTexture texture)
	{
	}
}
