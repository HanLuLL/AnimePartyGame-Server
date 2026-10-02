using System.Threading;

namespace Core;

public class CtsInfo
{
	public int id;

	public CancellationTokenSource cts;

	public CancellationTokenRegistration reg;

	private bool _dispose;

	public CancellationToken Token => cts.Token;

	public bool IsCancellationRequested
	{
		get
		{
			if (cts != null)
			{
				_ = cts.Token;
				return cts.Token.IsCancellationRequested;
			}
			return false;
		}
	}

	public void Cancel()
	{
		if (!_dispose && !IsCancellationRequested)
		{
			cts.Cancel();
		}
	}

	public void DisposeAfterFinish()
	{
		_dispose = true;
		reg.Dispose();
		cts.Dispose();
	}

	public void Dispose()
	{
		_dispose = true;
		if (cts.Token.CanBeCanceled)
		{
			cts.Cancel();
		}
		reg.Dispose();
		cts.Dispose();
	}
}
