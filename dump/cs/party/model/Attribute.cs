using Google.Protobuf.Reflection;

namespace party.model;

public enum Attribute
{
	[OriginalName("Gold")]
	Gold = 0,
	[OriginalName("HP")]
	Hp = 1,
	[OriginalName("ATK")]
	Atk = 2,
	[OriginalName("DEF")]
	Def = 3,
	[OriginalName("Buffs")]
	Buffs = 4,
	[OriginalName("Place")]
	Place = 5,
	[OriginalName("Lottery")]
	Lottery = 6,
	[OriginalName("Cards")]
	Cards = 7,
	[OriginalName("Bombs")]
	Bombs = 8,
	[OriginalName("Lv")]
	Lv = 9,
	[OriginalName("Count")]
	Count = 10,
	[OriginalName("HpAdd")]
	HpAdd = 19,
	[OriginalName("CombatCardNum")]
	CombatCardNum = 20,
	[OriginalName("EffectCardNum")]
	EffectCardNum = 21,
	[OriginalName("Killer")]
	Killer = 23,
	[OriginalName("LotteryNum")]
	LotteryNum = 24,
	[OriginalName("NotBuyLottery")]
	NotBuyLottery = 25,
	[OriginalName("MovePoint")]
	MovePoint = 26,
	[OriginalName("BuffId")]
	BuffId = 27,
	[OriginalName("LandType")]
	LandType = 28,
	[OriginalName("ExecLimit")]
	ExecLimit = 29,
	[OriginalName("Distance")]
	Distance = 30,
	[OriginalName("Owner")]
	Owner = 31,
	[OriginalName("MustDie")]
	MustDie = 32,
	[OriginalName("Damage")]
	Damage = 33,
	[OriginalName("SummonId")]
	SummonId = 34,
	[OriginalName("NodeId")]
	NodeId = 35,
	[OriginalName("Trigger")]
	Trigger = 36,
	[OriginalName("CardNum")]
	CardNum = 37,
	[OriginalName("GoldRate")]
	GoldRate = 38,
	[OriginalName("Harm")]
	Harm = 39,
	[OriginalName("MoveEnd")]
	MoveEnd = 40,
	[OriginalName("UseCardId")]
	UseCardId = 41,
	[OriginalName("ImmuneBattle")]
	ImmuneBattle = 42,
	[OriginalName("ImmuneTrap")]
	ImmuneTrap = 43,
	[OriginalName("Defender")]
	Defender = 44,
	[OriginalName("AtkDropGold")]
	AtkDropGold = 45,
	[OriginalName("DefDropGold")]
	DefDropGold = 46,
	[OriginalName("DieDropGold")]
	DieDropGold = 47,
	[OriginalName("CostDamage")]
	CostDamage = 48,
	[OriginalName("AbandonCard")]
	AbandonCard = 49,
	[OriginalName("AbandonFight")]
	AbandonFight = 50,
	[OriginalName("UseCardNum")]
	UseCardNum = 51,
	[OriginalName("ChangeHp")]
	ChangeHp = 52,
	[OriginalName("Point")]
	Point = 53,
	[OriginalName("Interrupt")]
	Interrupt = 54,
	[OriginalName("AddCardNum")]
	AddCardNum = 55,
	[OriginalName("CanNotBattle")]
	CanNotBattle = 56,
	[OriginalName("CanNotFightBack")]
	CanNotFightBack = 57,
	[OriginalName("Attacker")]
	Attacker = 58,
	[OriginalName("CardSubNum")]
	CardSubNum = 59,
	[OriginalName("DicePoint")]
	DicePoint = 60,
	[OriginalName("TargetId")]
	TargetId = 61,
	[OriginalName("OldHp")]
	OldHp = 62,
	[OriginalName("BaseHarmRate")]
	BaseHarmRate = 63,
	[OriginalName("GiveCardNum")]
	GiveCardNum = 64,
	[OriginalName("IsNoActive")]
	IsNoActive = 65,
	[OriginalName("BattlePoint")]
	BattlePoint = 66,
	[OriginalName("BattleDodge")]
	BattleDodge = 67,
	[OriginalName("IsRemoveBuff")]
	IsRemoveBuff = 68,
	[OriginalName("IsNotUseSkill")]
	IsNotUseSkill = 69,
	[OriginalName("MonsterNoMove")]
	MonsterNoMove = 70,
	[OriginalName("HasWrapAction")]
	HasWrapAction = 71,
	[OriginalName("AddBattleCost")]
	AddBattleCost = 72,
	[OriginalName("Invincible")]
	Invincible = 73,
	[OriginalName("IgnoreDefense")]
	IgnoreDefense = 74,
	[OriginalName("MoveEffect")]
	MoveEffect = 75,
	[OriginalName("CandyVIPDiscount")]
	CandyVipdiscount = 76,
	[OriginalName("BattleFightBack")]
	BattleFightBack = 77,
	[OriginalName("NotAddProgress")]
	NotAddProgress = 78,
	[OriginalName("KillDropGold")]
	KillDropGold = 79,
	[OriginalName("KillerHeroId")]
	KillerHeroId = 80,
	[OriginalName("AddModifyNum")]
	AddModifyNum = 81,
	[OriginalName("AssistPlayerHeroId")]
	AssistPlayerHeroId = 82,
	[OriginalName("BlueSpirit")]
	BlueSpirit = 83,
	[OriginalName("RedSpirit")]
	RedSpirit = 84,
	[OriginalName("BreakThroughCardHarm")]
	BreakThroughCardHarm = 85,
	[OriginalName("UseBreakThroughCard")]
	UseBreakThroughCard = 86,
	[OriginalName("TriggerEventId")]
	TriggerEventId = 87,
	[OriginalName("PKParryDamage")]
	PkparryDamage = 88,
	[OriginalName("LaughMonsterImmuneBattle")]
	LaughMonsterImmuneBattle = 89,
	[OriginalName("TriggerEventPct")]
	TriggerEventPct = 90,
	[OriginalName("TriggerEventCount")]
	TriggerEventCount = 91,
	[OriginalName("RestRangeDamage")]
	RestRangeDamage = 92,
	[OriginalName("MoveDicePoint")]
	MoveDicePoint = 93,
	[OriginalName("LaunchBattle")]
	LaunchBattle = 94,
	[OriginalName("PveLockHp")]
	PveLockHp = 95,
	[OriginalName("PveLocDecHp")]
	PveLocDecHp = 96,
	[OriginalName("TermFightBackImme")]
	TermFightBackImme = 97,
	[OriginalName("IsFightBackBattle")]
	IsFightBackBattle = 98,
	[OriginalName("SuicideId")]
	SuicideId = 99,
	[OriginalName("AtkFromDefender")]
	AtkFromDefender = 100,
	[OriginalName("MosesSkill")]
	MosesSkill = 101,
	[OriginalName("GetCardFromDup")]
	GetCardFromDup = 102,
	[OriginalName("DamageType")]
	DamageType = 103,
	[OriginalName("IsRevive")]
	IsRevive = 104,
	[OriginalName("DamageModifier")]
	DamageModifier = 105,
	[OriginalName("ShopCardDiscount")]
	ShopCardDiscount = 106,
	[OriginalName("LevelRollGold")]
	LevelRollGold = 10001,
	[OriginalName("LevelBotRelic")]
	LevelBotRelic = 10002,
	[OriginalName("LevelBotAskBattle")]
	LevelBotAskBattle = 10003,
	[OriginalName("LevelNotUseBattleCard")]
	LevelNotUseBattleCard = 10004,
	[OriginalName("LevelBattleCardAtk")]
	LevelBattleCardAtk = 10005,
	[OriginalName("LevelBotFront")]
	LevelBotFront = 10006
}
