namespace FairyGUI;

internal class RelationDef
{
	public bool percent;

	public RelationType type;

	public int axis;

	public void copyFrom(RelationDef source)
	{
		percent = source.percent;
		type = source.type;
		axis = source.axis;
	}
}
