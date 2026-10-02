using System.Collections.Generic;

namespace FairyGUI.Utils;

public class XMLList
{
	public struct Enumerator
	{
		private List<XML> _source;

		private string _selector;

		private int _index;

		private int _total;

		private XML _current;

		public XML Current => _current;

		public Enumerator(List<XML> source, string selector)
		{
			_source = source;
			_selector = selector;
			_index = -1;
			if (_source != null)
			{
				_total = _source.Count;
			}
			else
			{
				_total = 0;
			}
			_current = null;
		}

		public bool MoveNext()
		{
			while (++_index < _total)
			{
				_current = _source[_index];
				if (_selector == null || _current.name == _selector)
				{
					return true;
				}
			}
			return false;
		}

		public void Erase()
		{
			_source.RemoveAt(_index);
			_total--;
		}

		public void Reset()
		{
			_index = -1;
		}
	}

	public List<XML> rawList;

	private static List<XML> _tmpList = new List<XML>();

	public int Count => rawList.Count;

	public XML this[int index] => rawList[index];

	public XMLList()
	{
		rawList = new List<XML>();
	}

	public XMLList(List<XML> list)
	{
		rawList = list;
	}

	public void Add(XML xml)
	{
		rawList.Add(xml);
	}

	public void Clear()
	{
		rawList.Clear();
	}

	public Enumerator GetEnumerator()
	{
		return new Enumerator(rawList, null);
	}

	public Enumerator GetEnumerator(string selector)
	{
		return new Enumerator(rawList, selector);
	}

	public XMLList Filter(string selector)
	{
		bool flag = true;
		_tmpList.Clear();
		int count = rawList.Count;
		for (int i = 0; i < count; i++)
		{
			XML xML = rawList[i];
			if (xML.name == selector)
			{
				_tmpList.Add(xML);
			}
			else
			{
				flag = false;
			}
		}
		if (flag)
		{
			return this;
		}
		XMLList result = new XMLList(_tmpList);
		_tmpList = new List<XML>();
		return result;
	}

	public XML Find(string selector)
	{
		int count = rawList.Count;
		for (int i = 0; i < count; i++)
		{
			XML xML = rawList[i];
			if (xML.name == selector)
			{
				return xML;
			}
		}
		return null;
	}

	public void RemoveAll(string selector)
	{
		rawList.RemoveAll((XML xml) => xml.name == selector);
	}
}
