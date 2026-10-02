using System;
using Tools;

namespace Core.Net;

public class ByteArrayPool : SimpleSingletonProvider<ByteArrayPool>
{
	private readonly object PoolLock = new object();

	private ConcurrentQueue<byte[]> _bytPoolMore;

	private ConcurrentQueue<byte[]> _bytPoolLess;

	private byte[] _buf;

	public int TopByts = 10000;

	protected override void InstanceInit()
	{
		base.InstanceInit();
		_bytPoolMore = new ConcurrentQueue<byte[]>();
		_bytPoolLess = new ConcurrentQueue<byte[]>();
		initQueue(66, 6);
	}

	public void initQueue(int _less, int _more)
	{
		AddQueue(ref _bytPoolLess, _less, _is_less: true);
		AddQueue(ref _bytPoolMore, _more, _is_less: false);
	}

	protected void AddQueue(ref ConcurrentQueue<byte[]> _queue, int _len, bool _is_less)
	{
		for (int i = 0; i < _len; i++)
		{
			_queue.Enqueue(_is_less ? new byte[100] : new byte[20480]);
		}
	}

	public byte[] GetItem(int _len)
	{
		if (_len <= 1000)
		{
			return GetBufArray(ref _bytPoolLess, _len);
		}
		return GetBufArray(ref _bytPoolMore, _len);
	}

	protected byte[] GetBufArray(ref ConcurrentQueue<byte[]> _bytPool, int _len)
	{
		lock (PoolLock)
		{
			if (_bytPool.Count > 0)
			{
				while (_bytPool.Count > 0)
				{
					_buf = _bytPool.Dequeue();
					if (_buf != null || _bytPool.Count <= 0)
					{
						break;
					}
				}
				if (_buf != null)
				{
					if (_buf.Length < _len)
					{
						Array.Resize(ref _buf, _len);
					}
					return _buf;
				}
			}
			return new byte[_len];
		}
	}

	public byte[] StoreItem(byte[] _item)
	{
		lock (PoolLock)
		{
			if (_item.Length < TopByts)
			{
				if (_item.Length >= 1000)
				{
					_bytPoolMore.Enqueue(_item);
				}
				else
				{
					_bytPoolLess.Enqueue(_item);
				}
				return _item;
			}
			return null;
		}
	}

	protected override void OnDestroyInstance()
	{
		base.OnDestroyInstance();
		Destroy();
	}

	protected void Destroy()
	{
		_bytPoolMore.Clear();
		_bytPoolLess.Clear();
		_bytPoolMore = null;
		_bytPoolLess = null;
	}
}
