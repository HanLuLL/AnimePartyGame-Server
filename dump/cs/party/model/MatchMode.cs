using Google.Protobuf.Reflection;

namespace party.model;

public enum MatchMode
{
	[OriginalName("match_mode_none")]
	None = 0,
	[OriginalName("standard")]
	Standard = 1,
	[OriginalName("ultra")]
	Ultra = 3,
	[OriginalName("pve")]
	Pve = 4,
	[OriginalName("asymmetrical_battle")]
	AsymmetricalBattle = 7,
	[OriginalName("lucky_star_battle")]
	LuckyStarBattle = 11,
	[OriginalName("mutator_pve")]
	MutatorPve = 12
}
