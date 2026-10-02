using FairyGUI;
using Google.Protobuf.Collections;

namespace UI;

public class ActivityTab
{
	public int TitleId;

	public int SelectedIndex;

	public GButton Entity;

	public RepeatedField<int> VoteConfigs;
}
