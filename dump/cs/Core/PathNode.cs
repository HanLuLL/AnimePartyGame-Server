namespace Core;

public class PathNode
{
	public int preNodeId;

	public int curNodeId;

	public PathNode(int curNodeId, int preNodeId)
	{
		this.preNodeId = preNodeId;
		this.curNodeId = curNodeId;
	}
}
