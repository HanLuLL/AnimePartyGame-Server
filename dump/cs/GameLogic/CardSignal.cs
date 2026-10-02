using Google.Protobuf.Collections;
using Tools;

namespace GameLogic;

public class CardSignal
{
	public readonly Signal resetCardList = new Signal();

	public readonly Signal<long> resetSelfCardList = new Signal<long>();

	public readonly Signal recycleCard = new Signal();

	public readonly Signal cancelUse = new Signal();

	public readonly Signal<bool> SetCancelBtn = new Signal<bool>();

	public readonly Signal<int, RepeatedField<long>, int> selectPlayer = new Signal<int, RepeatedField<long>, int>();

	public readonly Signal<RepeatedField<long>, bool> showSelectPlayer = new Signal<RepeatedField<long>, bool>();

	public readonly Signal closeSelectPlayer = new Signal();

	public readonly Signal<bool> LookCard = new Signal<bool>();

	public readonly Signal ZoomOutCard = new Signal();

	public readonly Signal<HandCardData> CardConvertChanged = new Signal<HandCardData>();
}
