using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
	private int _size;

	private int _capacity;

	public T[] _elements;

	private readonly IComparer<T> _comparator;

	public int Size
	{
		get
		{
			if (_size < 0)
			{
				_size = 0;
			}
			return _size;
		}
		set
		{
			_size = value;
		}
	}

	public int Capacity
	{
		get
		{
			if (_capacity < 0)
			{
				_capacity = 0;
			}
			return _capacity;
		}
		set
		{
			_capacity = value;
		}
	}

	public bool IsEmpty => _size == 0;

	private T Top => _elements[0];

	public PriorityQueue(IComparer<T> comparator, int capacity = 1)
	{
		Size = 0;
		Capacity = capacity;
		_elements = new T[capacity];
		_comparator = comparator;
	}

	public void Enqueue(T element)
	{
		if (Size == Capacity)
		{
			ExpandCapacity();
		}
		_elements[Size] = element;
		HeapInsert(_elements, Size);
		Size++;
	}

	public T Dequeue()
	{
		if (Size == 0)
		{
			return default(T);
		}
		T result = _elements[0];
		Swap(_elements, 0, Size - 1);
		Size--;
		Heapify(_elements, 0, Size);
		return result;
	}

	public T Peek()
	{
		return Top;
	}

	private void HeapInsert(T[] elements, int index)
	{
		while (index > 0 && _comparator.Compare(elements[index], elements[(index - 1) / 2]) > 0)
		{
			Swap(elements, index, (index - 1) / 2);
			index = (index - 1) / 2;
		}
	}

	public void Clear()
	{
		Size = 0;
	}

	private void Heapify(T[] elements, int index, int size)
	{
		int num = index * 2 + 1;
		while (num < size)
		{
			int num2 = ((num + 1 < size && _comparator.Compare(elements[num + 1], elements[num]) > 0) ? (num + 1) : num);
			num2 = ((_comparator.Compare(elements[num2], elements[index]) > 0) ? num2 : index);
			if (num2 != index)
			{
				Swap(elements, num2, index);
				index = num2;
				num = index * 2 + 1;
				continue;
			}
			break;
		}
	}

	private void ExpandCapacity()
	{
		Capacity = (int)Math.Ceiling((float)Capacity * 1.5f);
		T[] array = new T[Capacity];
		for (int i = 0; i < _elements.Length; i++)
		{
			array[i] = _elements[i];
		}
		_elements = array;
	}

	private void Swap(T[] elements, int i, int j)
	{
		T val = elements[j];
		T val2 = elements[i];
		elements[i] = val;
		elements[j] = val2;
	}
}
