using System.Collections.Generic;

namespace UI;

public class PVEProgressData
{
	public int progress;

	public List<int> mapEventIds;

	public PVEProgressData(int _progress)
	{
		progress = _progress;
	}
}
