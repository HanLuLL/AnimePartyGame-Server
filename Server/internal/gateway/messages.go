package gateway

import (
	"context"
	"sort"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
	"google.golang.org/protobuf/reflect/protoreflect"
)

func roomTeamID(mode, slot int32) int32 {
	switch mode {
	case 4, 6, 9, 10, 12: // cooperative PVE modes
		return 10
	case 7: // AsymmetricalBattle: slot zero is the defender
		if slot == 0 {
			return 20
		}
		return 10
	case 11: // LuckyStarBattle alternates the two camps by room slot
		if slot%2 == 0 {
			return 100
		}
		return 110
	default: // public PVP and solo challenge modes use one camp per player
		if slot < 0 {
			slot = 0
		}
		return 10 + slot
	}
}

func (s *Server) roundStartMessage(ctx context.Context, roomID int64, round int32, playerID int64) *protocolpb.RoundStartS2C {
	_, maxUse, err := s.store.CardUseLimit(ctx, roomID, playerID, round)
	if err != nil || maxUse < 1 {
		if err != nil {
			s.log.Error("round card-use limit could not be loaded", "room_id", roomID, "player_id", playerID, "round", round, "err", err)
		}
		maxUse = 1
	}
	return &protocolpb.RoundStartS2C{Round: round, PlayerId: playerID, SkillCds: map[int32]int32{}, UseCardMaxNum: maxUse}
}

func (s *Server) playerMessage(p store.Player, roomID int64, mode int32) *modelpb.Player {
	// Keep commonly-read Player data present: the client initializes shop/task
	// state and reads the active fashion plan before showing its first Home scene.
	cards := make([]*modelpb.CardInfo, 0, len(p.Cards))
	for _, card := range p.Cards {
		cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
	}
	buffs := make(map[int64]*modelpb.Buff, len(p.Buffs))
	for _, buff := range p.Buffs {
		buffs[buff.UniqueID] = buffMessage(buff)
	}
	lotterys := p.Lotterys
	if lotterys == nil {
		lotterys = map[int32]bool{}
	}
	teamID := int32(0)
	if roomID > 0 {
		teamID = roomTeamID(mode, p.Slot)
	}
	heroID := p.HeroID
	if heroID <= 0 {
		// A brand new account has no selected hero: hero_id only exists once a
		// hero is picked inside a room. Report a real roster hero so the client
		// never sees HeroId 0 in its first Home snapshot; the client resolves
		// the hero's standing painting, level and combat attributes from this
		// ID while it builds the Home scene after InitAccount.
		if starters := s.resources.DefaultHeroIDs(); len(starters) > 0 {
			heroID = starters[0]
		}
	}
	heroLevel := p.HeroLevel
	if heroLevel < 1 {
		heroLevel = 1
	}
	attack, defense := s.resources.HeroCombatAttributes(heroID)
	profile, hasProfile := p.PveHeroes[heroID]
	if !hasProfile {
		profile = store.PveHeroProfile{HeroID: heroID, Level: 1}
		if p.PveLevel > 0 {
			profile.Level = p.PveLevel
		}
		if p.PveTalentID > 0 {
			profile.Talents = []int32{p.PveTalentID}
		}
	}
	pveStrengthen := pveStrengthenMessage(profile)
	maxCardNum := p.UseCardMaxNum
	if maxCardNum < 1 {
		maxCardNum = 1
	}
	hero := &modelpb.Hero{PlayerId: p.ID, HeroId: heroID, Lv: heroLevel, TeamId: teamID, NodeId: p.NodeID, BackNodeId: p.BackNodeID, FrontNodeIds: append([]int32(nil), p.FrontNodeIDs...), Gold: p.Gold, Hp: p.HP, SpecialScore: p.SpecialScore, MaxHp: s.resources.HeroMaxHP(heroID), Attack: attack, Defense: defense, MovePoint: 0, Cards: cards, Buffs: buffs, Bombs: bombModels(p.Bombs), Lotterys: lotterys, SkillCds: p.SkillCooldowns, PveHeroStrengthen: pveStrengthen, UserCardNum: p.UseCardNum, UseCardMaxNum: maxCardNum}
	if hero.SkillCds == nil {
		hero.SkillCds = map[int32]int32{}
	}
	roleCards := make(map[int32]*modelpb.RoleCard)
	heroIDs := s.resources.DefaultHeroIDs()
	for _, id := range heroIDs {
		heroProfile, exists := p.PveHeroes[id]
		if !exists {
			heroProfile = store.PveHeroProfile{HeroID: id, Level: 1}
		}
		roleCards[id] = &modelpb.RoleCard{DefId: id, Lv: 1, PveStrengthen: pveStrengthenMessage(heroProfile)}
	}
	if roleCard, exists := roleCards[heroID]; exists {
		roleCard.PveStrengthen = pveStrengthen
	}
	bagItems := make([]*modelpb.ItemEtc, 0, len(p.Inventory))
	for _, item := range p.Inventory {
		if item.Count > 0 {
			bagItems = append(bagItems, &modelpb.ItemEtc{ItemId: item.ItemID, Count: item.Count})
		}
	}
	gachaRecords := make([]*modelpb.GachaRecordList, 0, len(p.GachaProgress))
	for _, progress := range p.GachaProgress {
		gachaRecords = append(gachaRecords, &modelpb.GachaRecordList{
			GachaId: progress.GachaID, PoolId: progress.PoolID,
			Count: progress.Count, RewardCount: progress.RewardCount,
			Records: []*modelpb.GachaRecord{},
		})
	}
	fashionPlans := p.FashionPlans
	if len(fashionPlans) == 0 {
		fashionPlans = map[int32]map[int32]int32{1: s.resources.DefaultFashionPlan()}
	} else if _, exists := fashionPlans[1]; !exists {
		fashionPlans = make(map[int32]map[int32]int32, len(p.FashionPlans)+1)
		for plan, items := range p.FashionPlans {
			fashionPlans[plan] = items
		}
		fashionPlans[1] = s.resources.DefaultFashionPlan()
	}
	planIDs := make([]int32, 0, len(fashionPlans))
	for plan := range fashionPlans {
		planIDs = append(planIDs, plan)
	}
	sort.Slice(planIDs, func(i, j int) bool { return planIDs[i] < planIDs[j] })
	playerFashionPlans := make([]*modelpb.FashionPlan, 0, len(planIDs))
	for _, plan := range planIDs {
		items := fashionPlans[plan]
		if items == nil {
			items = s.resources.DefaultFashionPlan()
		}
		playerFashionPlans = append(playerFashionPlans, &modelpb.FashionPlan{Plan: plan, Fashion: items})
	}
	usePlan := p.UsePlan
	if _, exists := fashionPlans[usePlan]; !exists {
		usePlan = 1
	}
	signInRewards := make(map[int32]*modelpb.SignInReward, len(p.SignInRewards))
	for activityID, reward := range p.SignInRewards {
		signInRewards[activityID] = &modelpb.SignInReward{
			ActivityId: reward.ActivityID, SignInCount: reward.SignInCount, UpdateTime: reward.UpdateTime,
		}
	}
	message := &modelpb.Player{Id: p.ID, Nick: p.Nick, RoomId: roomID, Slot: p.Slot, RoomReady: p.Ready, Level: p.Level, Exp: p.Exp, OffLine: false, IsBot: p.IsBot,
		Hero: hero, BagItems: bagItems, ShopInfo: &modelpb.PlayerShopInfo{}, FashionPlan: playerFashionPlans, UsePlan: usePlan, GachaCount: gachaCountSnapshot(p.GachaProgress), GachaRecords: gachaRecords, RoleCard: roleCards,
		Day7: map[int32]*modelpb.Day7Reward{}, WeeklyLimits: map[int32]int32{}, ActivityPass: map[int32]*modelpb.ActivityPass{}, AltArtCards: map[int32]*modelpb.AltArtCardInfo{},
		ScratchCard: map[int32]*modelpb.ScratchCardRecord{}, LightGift: map[int32]*modelpb.LightGift{}, FlipCard: map[int32]*modelpb.FlipCardActivity{},
		ShowPlayer: &modelpb.ShowPlayerInfo{}, ClientData: &modelpb.ClientData{},
		InviteInfo: &modelpb.InviteInfo{}, SingleInfo: &modelpb.SingleInfo{}, SportsMeetInfo: &modelpb.SportsMeetInfo{},
		CreditInfo: &modelpb.CreditInfo{}, ReturnInfo: &modelpb.ReturnInfo{}, PayAmountInfo: &modelpb.PayAmountInfo{},
		SignInReward: signInRewards,
		Task:         &modelpb.TaskInfo{ProgressReward: 0, WeekCondition: map[int32]int32{}, Condition: map[int32]int32{}, TaskRewardIs: []int32{}, AchieveRewardIs: []int32{}, WeekTaskRewardIs: []int32{}},
		Mails:        map[int32]*modelpb.MailData{}, ActivityTask: []*modelpb.ActivityInfo{}, Friends: &modelpb.FriendList{FriendIds: []int64{}, Blacks: []int64{}, Apply: []*modelpb.FriendApply{}, SelfApply: []*modelpb.FriendSelfApply{}, FriendNotes: map[int64]string{}},
		BattlePass: &modelpb.BattlePass{DefId: 0, Lv: 1, Exp: 0, Gear: 0, RewardIds: map[int32]int32{}, Task: map[int32]int32{}, TaskRewardIs: []int32{}},
		GuildInfo:  &modelpb.PlayerGuildInfo{}, QuestionInfo: map[int32]*modelpb.QuestionModel{}, MissionMod: &modelpb.MissionMod{ActivityTasks: []*modelpb.TaskDSO{}}}
	// Bootstrap snapshot backfill: fill the hardcoded-empty sub-messages
	// (shop, gacha counts, day-7, activities, profile blocks) and the mail
	// list from storage/config before the ConnectS2C frame ships. Failures
	// are logged and degrade to the previous empty state.
	if single, err := s.store.LoadSingleProgress(context.Background(), p.ID); err == nil {
		s.snapImplBackfill(message, p.ID, single.LevelPass, single.MaxScore)
	} else {
		s.snapImplBackfill(message, p.ID, nil, 0)
	}
	return message
}

func pveStrengthenMessage(profile store.PveHeroProfile) *modelpb.PveHeroStrengthen {
	level := profile.Level
	if level <= 0 {
		level = 1
	}
	talents := append([]int32(nil), profile.Talents...)
	return &modelpb.PveHeroStrengthen{Level: level, Exp: profile.Exp, Talent: talents}
}

func buffMessage(buff store.BuffState) *modelpb.Buff {
	message := &modelpb.Buff{
		UniqueId: buff.UniqueID, BuffId: buff.BuffID, Params: buff.Params, RestoreParams: buff.RestoreParams,
		KeepRound: buff.KeepRound, DelayRound: buff.DelayRound, UseTime: buff.UseTime, TargetIds: buff.TargetIDs,
		Priority: buff.Priority, NodeId: buff.NodeID, Progress: buff.Progress, RelationId: buff.RelationID,
		IsSakura: buff.IsSakura, BuffIndex: buff.BuffIndex, CanRangeDamage: buff.CanRangeDamage,
		RelativeBuffUids: buff.RelativeBuffUIDs, Key: buff.Key,
	}
	for _, source := range buff.Chain {
		message.Chain = append(message.Chain, &modelpb.BuffSource{S: modelpb.BuffSourceSource(source.S), Id: source.ID})
	}
	if buff.Source != nil {
		message.Source = &modelpb.BuffSource{S: modelpb.BuffSourceSource(buff.Source.S), Id: buff.Source.ID}
	}
	return message
}

func (s *Server) landBuffsPush(update store.LandBuffUpdate, recipients []int64) Push {
	buffs := make(map[int64]*modelpb.Buff, len(update.Buffs))
	for _, buff := range update.Buffs {
		buffs[buff.UniqueID] = buffMessage(buff)
	}
	message := &protocolpb.LandBuffsS2C{Buffs: []*protocolpb.LandBuffsS2C_LandBuffsWrap{{
		NodeId: update.NodeID, BuffArr: &modelpb.BuffArray{Buffs: buffs},
	}}}
	return s.pushFor("LandBuffsS2C", message, recipients, 0)
}

func landBuffPickupAttrUpdate(playerID int64, pickup store.LandBuffPickupResult, includeGold bool) *protocolpb.UpdateHeroAttrS2C {
	change := pickup.NewGold - pickup.OldGold
	currGold := pickup.NewGold
	if !includeGold {
		change = 0
		currGold = pickup.OldGold
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_landBuff, Id: pickup.LandBuff.Buff.UniqueID},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: change, OriGold: pickup.OldGold, CurrGold: currGold,
			}},
		}},
	}
}

func (s *Server) roomMessageBase(r store.Room) *modelpb.Room {
	players := make([]*modelpb.Player, 0, len(r.Players))
	box := &modelpb.HeroBarBox{Box: make(map[int64]*modelpb.HeroBar, len(r.Players))}
	landBuffs := make(map[int32]*modelpb.BuffArray)
	for _, p := range r.Players {
		players = append(players, s.playerMessage(p, r.ID, r.Mode))
		box.Box[p.ID] = &modelpb.HeroBar{PlayerId: p.ID, HeroId: p.HeroID, Affirm: p.HeroConfirmed,
			UseAdorn: p.UseAdorn, PveLevel: p.PveLevel, SkinPendant: p.SkinPendant,
			AffirmedSkin: p.SkinConfirmed, PveTalentId: p.PveTalentID, GameData: p.GameData}
	}
	for _, landBuff := range r.LandBuffs {
		buffs := landBuffs[landBuff.NodeID]
		if buffs == nil {
			buffs = &modelpb.BuffArray{Buffs: make(map[int64]*modelpb.Buff)}
			landBuffs[landBuff.NodeID] = buffs
		}
		buffs.Buffs[landBuff.Buff.UniqueID] = buffMessage(landBuff.Buff)
	}
	message := &modelpb.Room{Id: r.ID, Name: r.Name, Pwd: "", MaxTime: r.MaxTime, MapId: r.MapID, MapIndex: r.MapIndex,
		MasterId: r.MasterID, CreateTime: r.CreatedAt, UpdateTime: r.UpdatedAt, Players: players,
		State: modelpb.Room_State(r.State), Box: box, UpgradePlan: r.UpgradePlan, TimePlan: r.TimePlan, MapType: r.Mode,
		SteamLobbyId: r.LobbyID, SpeedType: r.SpeedType, Difficulty: r.Difficulty, SkipStory: r.SkipStory,
		RoomLabel: r.RoomLabel, GameProgress: r.GameProgress, GameMaxProgress: r.GameMaxProgress, SpecialScore: r.SpecialScore,
		LandBuffs: landBuffs}
	if r.Battle != nil {
		message.Battle = battleMessage(r.Battle)
	}
	return message
}

func battleMessage(state *store.BattleState) *modelpb.Battle {
	if state == nil {
		return nil
	}
	return &modelpb.Battle{BattleId: state.BattleID, Attacker: battleRoleMessage(state.Attacker), Defender: battleRoleMessage(state.Defender),
		CardUseState: state.CardUseState, IsEnd: state.IsEnd, FightBack: state.FightBack, IsPursuit: state.IsPursuit}
}

func battleRoleMessage(role store.BattleRoleState) *modelpb.BattleRole {
	message := &modelpb.BattleRole{PlayerId: role.PlayerID, HeroId: role.HeroID, Atk: role.Atk, Def: role.Def,
		Cost: role.Cost, MaxCost: role.MaxCost, UseCards: role.UseCards, Point: role.Point, Dodge: role.Dodge,
		IncHp: role.IncHP, DropGold: role.DropGold, CanNotFightBack: role.CanNotFightBack, AttackBonus: role.AttackBonus,
		ChainAttacker: role.ChainAttacker, ChainAttackDamage: role.ChainAttackDamage, MaxAtk: role.MaxAtk, MaxDef: role.MaxDef,
		MinAtk: role.MinAtk, MinDef: role.MinDef, InitAtk: role.InitAtk, InitDef: role.InitDef}
	for _, bonus := range role.CardCombatBonus {
		message.CardCombatBonus = append(message.CardCombatBonus, &modelpb.CardCombat{CardId: bonus.CardID, CardBonus: bonus.Bonus})
	}
	return message
}
func shortRoomMessage(r store.Room) *protocolpb.RoomShortInfo {
	return &protocolpb.RoomShortInfo{Id: r.ID, Name: r.Name, MapId: r.MapID, MasterId: r.MasterID,
		CreateTime: r.CreatedAt, IsPwd: r.Password != "", PlayerCount: int32(len(r.Players)),
		UpgradePlan: r.UpgradePlan, TimePlan: r.TimePlan, SpeedType: r.SpeedType,
		Difficulty: r.Difficulty, RoomLabel: r.RoomLabel, MapType: r.Mode}
}
func setCodeField(m proto.Message, code int32) {
	if m == nil {
		return
	}
	rm := m.ProtoReflect()
	fd := rm.Descriptor().Fields().ByName("code")
	if fd == nil || fd.Kind() != protoreflect.EnumKind {
		return
	}
	rm.Set(fd, protoreflect.ValueOfEnum(protoreflect.EnumNumber(code)))
}

// roomMessage attaches only static IDs from the loaded Resource table; it never synthesizes config rows.
func (s *Server) roomMessage(r store.Room) *modelpb.Room {
	m := s.roomMessageBase(r)
	m.MapEventIds = s.resources.MapEventIDs(int64(r.MapID))
	return m
}
