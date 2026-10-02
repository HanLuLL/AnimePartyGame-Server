namespace Tools;

public class HashUtils
{
	private static uint _genID;

	public static uint GUID => _genID++;
}
