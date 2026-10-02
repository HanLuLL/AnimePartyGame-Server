#!/usr/bin/env python3
import csv,json,os,sys
from pathlib import Path
if os.environ.get('GITHUB_ACTIONS') != 'true':
    sys.exit('Protocol code generation is restricted to GitHub Actions; use `make codegen` from Server.')
root=Path(__file__).resolve().parents[1]
rows=json.loads((root/'internal/config/command-map.json').read_text(encoding='utf8'))
custom={
    'ConnectC2S','HeartbeatC2S','CreateRoomC2S','ChangeRoomC2S','JoinRoomC2S','QueryRoomC2S','SyncRoomC2S',
    'QuickJoinRoomC2S','RoomReadyC2S','StartGameC2S','ExitRoomC2S','RefreshRoomStateC2S',
    'SearchRoomC2S','RoomKickPlayerC2S','RoomAbdicationC2S','ChoiceHeroC2S2','AffirmHeroC2S','ChooseSkinC2S',
    'ThrowDiceC2S','ThrowDiceResultC2S','BombThrowDiceC2S','UseEffectCardC2S','AbandonCardC2S','MoveAgainC2S','RollGoldC2S','LotteryChoiceC2S','MoveC2S','PursuitC2S','LandChoiceTargetC2S','StopOrContinueC2S','TriggerEventC2S','TriggerDestinyC2S','TriggerHospitalC2S','TriggerDivinationC2S','StartGambleC2S','GambleThrowDicC2S','GetPlayerSimpleC2S','SearchPlayerC2S','FriendListC2S','FriendOpC2S','SendChatC2S',
    'RoomShortChatC2S','CreateMatchTeamC2S','ChangeMatchTeamC2S','JoinMatchTeamC2S','ExitMatchTeamC2S',
    'RefreshMatchTeamInfoC2S','MatchTeamReadyC2S','MatchTeamChatC2S','ChatMapMarkersC2S','PlayerChatC2S','StartMatchC2S','CancelMatchC2S','FriendApplyC2S',
    'FriendApplyListC2S','FriendApplyOpC2S','FriendBlacksListC2S','GetSignInRewardC2S','SetFriendNoteC2S','SetOnlineStatusC2S','FriendSendMsgC2S',
    'GetChatMsgC2S','ReadChatMsgC2S','PlayerUseItemC2S','PlayerShopBuyC2S','ShopBuyC2S','PVEShopBuyC2S','GachaC2S','GachaRecordC2S','GachaCountRewardC2S','RookieGachaRewardC2S','SelectEventC2S','PveHeroUpLvC2S','PveHeroTalentUpC2S','TaskRewardC2S','TeachingC2S','MailReadC2S',
    'MailStarC2S','MailGetRewardC2S','MailDelReadC2S','ChangeNameC2S','GetShowPlayerC2S','SetShowPlayerC2S','GetHeroInfoC2S','SetFashionC2S','SelectFashionPlanC2S',
    'AskBattleC2S','BattleUseCardC2S','BattleThrowDiceC2S','BattleChoiceC2S'
}
custom.update({'FriendInviteC2S','FriendInviteListC2S','FriendInviteCleanC2S','NearFightPlayerC2S','DelChatMsgInfoC2S'})
custom.add('ClientHarmonyC2S')
prefixes=('Get','Query','Search','Refresh','FriendList','FriendApplyList','FriendInviteList','FriendBlacksList','NearFightPlayer','GachaRecord','GetChatMsg','GetPlayerFightRecord','GetShowPlayer','WatchRefreshRoomState')
requests=[r for r in rows if r['direction']=='c2s' and r['scope'] in ('client','test')]
custom_notes={
    'DelChatMsgInfoC2S':'persists a per-player high-water cursor that hides existing messages from only the caller and leaves the other participant history intact; server-side visibility semantics are inferred from the client-local delete flow',
    'FriendInviteC2S':'persists room invites for client-selected friends/recent co-players, checks room/password/recipient state, and pushes FriendInviteNotifyS2C; relationship constraints and invitation lifetime are inferred',
    'FriendInviteListC2S':'returns active waiting-room invites with inviter, room, password, map and member count; JoinRoomC2S remains the accept path and joining clears pending invites; paging/expiry semantics are inferred',
    'FriendInviteCleanC2S':'clears the caller’s pending room invites; the client clears the invite UI after the empty success response',
    'NearFightPlayerC2S':'returns up to 50 recent human co-players with online/busy state and honors onlyOnline; recency is recorded when all match assets finish loading, while official retention/window and pagination are unverified',
    'LandChoiceTargetC2S':'Battery land(11) target selection: derives damage from negative Land_infos.params[0], accepts one eligible living opposing player or explicit exit, revalidates targets from authoritative room/team state, records UPSN and applies HP/turn changes transactionally, then pushes the result and attributes; phase is restored on reconnect and bots choose a vulnerable legal target; mode-specific team assignment outside confirmed modes remains inferred',
    'ThrowDiceResultC2S':'5067 is the controlled-movement point selection sent after supported cards 20005/20032; validates the outstanding action SN and selected point 1..server-provided MaxPoint, persists the choice and resumes movement; other control cards/skills and controlled-point passive triggers remain incomplete',
    'SetFashionC2S':'persists one of up to ten six-slot fashion plans; validates configured Item_infos slot types when that table is loaded and requires non-default items to be owned; zero remains allowed for the optional sixth slot',
    'SelectFashionPlanC2S':'persists the selected existing fashion plan (plan 1 resolves to the resource default when no plans are stored); the request and success callback are recovered, but no UI call site was found in the decompiled source',
    'FriendOpC2S':'implements client opType 1 remove-friend, 2 block and 3 unblock; removal deletes both friendship directions and notifies the other player, block state is per owner and clears pending requests while retaining the friendship, and unblock removes only the block; blocked-player checks are used by friend requests/private chat',
    'ChangeRoomC2S':'room master updates password and resource-validated map/time/upgrade/speed/difficulty/story/label options atomically; ChangeRoomS2C response is pushed to other room members',
    'SyncRoomC2S':'restores room and battle snapshots and reissues the active human phase prompt',
    'MoveAgainC2S':'re-rolls after MoveAgain land; phase-gated and persisted with the move points',
    'UseEffectCardC2S':'per-turn optional card phase; supports self-heal cards 20002/20014, direct-damage cards 20001/20012/20013 with one hostile target and graph-range validation, skip, bomb-transfer card 20021 with a persisted 5059 throw phase, and controlled-movement cards 20005/20032; card/HP/buff/action/use-count state is transactionally persisted and resumes on reconnect; bots share the rules; CardAddAttack/CardAddDistance/NotSelect runtime properties and other effect cards/skills remain incomplete',
    'AbandonCardC2S':'5075 discard phase is entered when a DrawCard land grant exceeds the resource hand limit; requires exactly the excess distinct owned card GUIDs, transactionally persists the remaining full hand and advances the turn, syncs 5076 plus the full hand snapshot, restores the prompt after reconnect, and bots use the timeout-compatible first-card choice; other over-limit reward paths remain to be wired',
    'BombThrowDiceC2S':'only accepted during the persisted bomb_throw phase; ignores client DevPoint and rolls server-side, then transactionally passes bomb 20021 on rolls 2-6 or removes it and applies up to 99 HP damage on 1 after one-shot Destiny damage modifiers; synchronizes the actual HP delta and consumed Buffs with 5060/1040; bots and reconnects use the same phase state; seat-order handoff and FIFO for multiple bombs are inferred',
    'SelectEventC2S':'PVE replacement skill 10202 offer; persists two resource-backed event IDs and action SN, validates the exact offer/index, applies the selected event transactionally, and resumes the same choice on reconnect; profile talent progression still gates availability',
    'PveHeroUpLvC2S':'validates resource-backed PVE EXP items and level thresholds, atomically consumes inventory and persists per-hero level/EXP, then pushes BagItemChangeS2C; gameplay reward sources for EXP items remain partial',
    'PveHeroTalentUpC2S':'validates the next Character_infos.pveBreak talent, configured level gate and material costs, atomically consumes inventory and persists the ordered talent list; gameplay reward sources for materials remain partial',
    'RollGoldC2S':'uses the configured RollGold land choices, persists the gold award once, and advances the turn',
    'LotteryChoiceC2S':'persists configured unique player picks and advances the turn; scheduled draw and payout remain unimplemented',
    'PursuitC2S':'phase-gated Pursuit land choice; validates living opposing target outside Hospital, persists teleport position/fronts, supports exit and reconnect; team assignment outside documented mode constants is inferred',
    'ShopBuyC2S':'PVP Shop(7) stops movement; persists a map-pool offer, sold flags, purchases, gold, cards, UPSN and reconnect phase; land price/count parameter mapping and uniform offer draw are inferred',
    'PVEShopBuyC2S':'PVE Shop(22) persists the entry card, three-card map-pool offer, sale flags, purchases, gold, cards, UPSN and reconnect phase; entry reward/pool outside tutorial and Land_infos[22] price mapping are inferred; assistant funding and talent discounts are not open',
    'StopOrContinueC2S':'5077 choice is phase-gated; stopping resolves the Born/FillingStation heal and configured upgrade, continuing preserves remaining movement',
    'TriggerEventC2S':'resource-backed draw and transactional effects for 30001/30002/30003/30005/30006/30008/30009/30010/30013/30014/30015/30018/30019/30020/30021/30202/30206; 30001 redistribution remainder, 30005 position shuffle, 30010 discard, 30013 connected-tile sampling/slot assignment, 30018 movement-step count, and pool weighting are inferred; 30006 poison ticks at turn start once per player/round; 30020 damage reduction and 30202 attack bonus resolve in base PvP; Destiny next-damage Buffs are consumed by supported damage paths; event pool remains filtered',
    'TriggerDestinyC2S':'resource-backed random selection and transactional HP, gold, hand, buff, and land-summon state for 40001-40012; 40007/40008 grant the full configured draw and enter persisted 5075 discard when the active hand exceeds its mode limit; 40003/40004 adjust and clear the next supported incoming damage from their resource parameters; 40012 persists Gold summon 2200 at the current node and pays one oldest drop per committed movement that reaches that node; bots and reconnects use the same state; candidate weighting, pickup timing, and multiple-drop behavior still need live trace confirmation',
    'TriggerHospitalC2S':'phase-gated Hospital(13) check; localized client text confirms +2 HP and one skipped round when sick; params[0] is treated as an HP threshold by inference; persists result, recovery, reconnect prompt, bot resolution and skipped turn',
    'TriggerDivinationC2S':'phase-gated Divination(6) choice; persists the two offered resource IDs with the move, validates the selected ID, resolves configured HP/gold/card effects for configured max/min targets, and restores the offer on reconnect; uniform card and target sampling plus tie handling are inferred',
    'StartGambleC2S':'Gamble(10) guess phase; builds room roles from authoritative HP/gold, persists each odd/even wager, broadcasts the hall, and resumes prompts after reconnect; Land_infos params are interpreted as base pot/stake',
    'GambleThrowDicC2S':'Gamble throw/result phases; ignores client DevPoint, rolls dice server-side, persists points and payouts transactionally, advances the landing player after the final throw, and auto-resolves bots; winner pot split and remainder order are inferred',
    'AskBattleC2S':'starts or declines a persisted battle after a move onto an opposing human player; the collision trigger is inferred and bot combat is not open',
    'BattleUseCardC2S':'validates battle role, hand GUID, resource-backed Attack/Defense type and cost; rolls configured bonus server-side, removes the card transactionally, and resumes the card phase; special battle-card effects and card-phase buff interactions remain incomplete',
    'BattleThrowDiceC2S':'attacker die is rolled server-side and ignores client DevPoint; applies hero passive attack modifiers 11611/10611 and consumes configured event buff 3020201 once; persists the battle state and prompts the defender',
    'BattleChoiceC2S':'defender die is rolled server-side; applies the tutorial dodge/damage formula, one-shot Destiny damage modifiers, configured HP-change passives and one-shot event buff 3002001, updates Hero 115 hit/dodge stacks, and persists HP, buffs, and combat stats before pushing Battle and the next turn; counterattacks, other skill effects, and death rewards remain incomplete',
    'RoomShortChatC2S':'validates configured room-chat indices and recipient membership, then persists and broadcasts only to room members with a shared 600 ms per-player cooldown',
    'ChatMapMarkersC2S':'validates resource-backed battle chat marks and structured battle-message types, then broadcasts to other active room members with a 600 ms per-player cooldown; optional admin packet trace retains this battle-message payload, while stdout logs do not',
    'PlayerChatC2S':'validates structured battle-message types and room-member recipients, limits payloads to 200 single-line Unicode characters, and applies a 600 ms per-player cooldown; optional admin packet trace retains the battle-message payload, while stdout logs do not',
    'MatchTeamChatC2S':'team quick-chat validates configured room-chat indices and reaches only current members of the specified match team; it shares the 600 ms per-player cooldown',
    'RefreshMatchTeamInfoC2S':'requires an authenticated current team member before returning the full persisted team snapshot; nonexistent teams and nonmembers receive InvalidParam without team data',
    'GetShowPlayerC2S':'loads the requested player profile, maps playerId=0 to the caller, and returns persisted display settings plus typed empty fight statistics/history; fight record aggregation remains unimplemented',
    'SetShowPlayerC2S':'persists the caller’s standing painting, up to six displayed achievement IDs, and public data/fight visibility flags; ignores client-supplied player IDs, statistics, and match records',
    'GetHeroInfoC2S':'requires both players to be in the same active room; returns match-scoped damage, injuries, deaths, kills, healing score, and the target hero’s resource-resolved active-skill cooldown; counters are updated by resolved combat and HP changes',
    'GetSignInRewardC2S':'selects the requested SignIn_infos/FixSignIn_infos activity, validates all seven SignIn_datas reward days, enforces the next daily claim under the client’s 04:00 Asia/Shanghai reset, then transactionally grants configured inventory and advances the count; updated signInReward is returned and BagItemChangeS2C is pushed',
    'FriendBlacksListC2S':'returns the authenticated player’s persisted blocked-player profiles, online/busy state, and block time with isEnd=true; the client requests page 0 and replaces its local list, so the handler returns the full list',
    'TaskRewardC2S':'validates resource-backed weekly tasks, achievements, weekly liveness, and seven-day tasks; claims and configured inventory rewards are atomic; Task_beginners is absent and gameplay condition coverage is partial',
    'TeachingC2S':'persists tutorial completion and returns the client condition push; task counter conditions 2-5 update atomically from resolved battle statistics',
    'ClientHarmonyC2S':'echoes the client anti-harmony (angel mode) switch with ERR=0 and persists players.is_harmony/harmony_type (schema v5); the decompiled callback only treats errId==0 as success and ignores the response body',
}
partial={'TriggerEventC2S','TriggerDestinyC2S','TriggerHospitalC2S','TriggerDivinationC2S','StartGambleC2S','GambleThrowDicC2S','LotteryChoiceC2S','UseEffectCardC2S','AbandonCardC2S','SelectEventC2S','PveHeroUpLvC2S','PveHeroTalentUpC2S','TaskRewardC2S','ShopBuyC2S','PVEShopBuyC2S','AskBattleC2S','BattleUseCardC2S','BattleThrowDiceC2S','BattleChoiceC2S','GetShowPlayerC2S','GetHeroInfoC2S','LandChoiceTargetC2S','ThrowDiceResultC2S'}
partial.add('BombThrowDiceC2S')
partial.add('GachaC2S')
custom_notes['GachaC2S']='uses resource-backed weighted rows, configured 10-draw purple+ / 40-draw hero guarantees, per-pull extra rewards and Item_infos duplicate transforms; atomically commits charges, inventory, draw history and progress, and deduplicates action sequence numbers; featured-UP/RoleUp remains incomplete'
custom_notes['GachaRecordC2S']='returns the authenticated player’s latest 40 persisted results for the requested banner in client display order'
partial.update({'GachaCountRewardC2S','RookieGachaRewardC2S'})
partial.update({'FriendInviteC2S','FriendInviteListC2S','NearFightPlayerC2S','DelChatMsgInfoC2S'})
custom_notes['GachaCountRewardC2S']='uses the configured pool milestones to grant all eligible unclaimed rewards atomically and persist the highest claimed draw threshold; the C2S contains no milestone ID, so claiming all eligible tiers is inferred'
custom_notes['RookieGachaRewardC2S']='validates the rookie banner, draw threshold, and selected configured item; grants it once and persists the claim threshold; the selected-item handling follows the decompiled client flow'
with (root/'internal/config/handler-status.csv').open('w',encoding='utf8',newline='') as f:
 w=csv.writer(f);w.writerow(['cmd_id','request_type','response_id','response_type','status','notes'])
 for r in requests:
  if r['messageName'] in custom:status,note=('partial' if r['messageName'] in partial else 'basic'),custom_notes.get(r['messageName'],'custom handler in internal/gateway/handlers.go')
  elif r['messageName'].startswith(prefixes):status,note='empty-success-stub','typed empty S2C; add state/resource data'
  else:status,note='not-open','returns code.NotOpen; mutation is not fabricated'
  w.writerow([r['cmdId'],r['message'],r['responseCmdId'],r['responseMessage'] or '',status,note])
print(f'requests={len(requests)} custom={sum(r["messageName"] in custom for r in requests)} empty_read={sum(r["messageName"].startswith(prefixes) and r["messageName"] not in custom for r in requests)} not_open={sum(r["messageName"] not in custom and not r["messageName"].startswith(prefixes) for r in requests)}')
