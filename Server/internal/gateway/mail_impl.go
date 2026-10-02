package gateway

import (
	"context"
	"sort"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// mailImpl*: connect-snapshot mail backfill and MailAddS2C push helpers.

// mailSnapshotForPlayer loads every stored mail and converts it into the
// ConnectS2C mails map. The client renders its mail list exclusively from this
// snapshot (plus MailAddS2C pushes for new mail), so an empty map hides any
// stored mail from the player.
func (s *Server) mailImplSnapshot(ctx context.Context, playerID int64) (map[int32]*modelpb.MailData, int32, error) {
	mails := map[int32]*modelpb.MailData{}
	if playerID <= 0 {
		return mails, 0, nil
	}
	records, err := s.store.ListMails(ctx, playerID)
	if err != nil {
		return nil, 0, err
	}
	now := time.Now().Unix()
	for _, rec := range records {
		if rec.ExpireAt > 0 && rec.ExpireAt < now {
			continue
		}
		if rec.StartTime > 0 && rec.StartTime > now {
			continue
		}
		mails[rec.ID] = mailImplData(rec)
	}
	return mails, 0, nil
}

func mailImplData(rec store.MailRecord) *modelpb.MailData {
	rewards := make(map[int32]int32, len(rec.Rewards))
	for itemID, count := range rec.Rewards {
		rewards[itemID] = count
	}
	return &modelpb.MailData{
		Id: rec.ID, Title: rec.Title, Context: rec.Context, SendName: rec.SendName,
		IsRead: rec.IsRead, IsGetReward: rec.IsRewarded, CreateTime: rec.CreatedAt,
		Rewards: rewards, ExpireTime: rec.ExpireAt, StartTime: rec.StartTime, IsStarMail: rec.IsStar,
	}
}

// mailImplBackfillPlayer fills the mails map on an already built Player
// snapshot. Called from messages.go right before the ConnectS2C response is
// assembled; failures only degrade to the (old) empty list, never to an error.
func (s *Server) mailImplBackfillPlayer(player *modelpb.Player, playerID int64) {
	if player == nil || playerID <= 0 {
		return
	}
	mails, _, err := s.mailImplSnapshot(context.Background(), playerID)
	if err != nil {
		s.log.Error("mail snapshot load failed", "player_id", playerID, "err", err)
		return
	}
	if player.Mails == nil {
		player.Mails = mails
		return
	}
	for id, mail := range mails {
		player.Mails[id] = mail
	}
}

// MailImplSend delivers one mail to a player. It always persists first, so the
// mail survives reconnects; if the player is online a MailAddS2C push is sent
// so the client appends it without a relogin.
func (s *Server) MailImplSend(ctx context.Context, playerID int64, title, body, sendName string, rewards map[int32]int32) (int32, error) {
	if playerID <= 0 {
		return 0, store.ErrMailNotFound
	}
	id, err := s.store.InsertMail(ctx, playerID, store.MailRecord{
		Title: title, Context: body, SendName: sendName, Rewards: rewards, CreatedAt: time.Now().Unix(),
	})
	if err != nil {
		return 0, err
	}
	if len(s.hub.sessions(playerID)) > 0 {
		rec, listErr := s.store.ListMails(ctx, playerID)
		if listErr == nil {
			for _, entry := range rec {
				if entry.ID == id {
					push := s.pushFor("MailAddS2C", &protocolpb.MailAddS2C{Mail: mailImplData(entry)}, []int64{playerID}, 0)
					s.broadcastPush(push, [3]byte{1, 0, 0})
					break
				}
			}
		}
	}
	return id, nil
}

// MailImplSendAll persists one mail for every registered player. Online players
// additionally receive a MailAddS2C push.
func (s *Server) MailImplSendAll(ctx context.Context, title, body, sendName string, rewards map[int32]int32) (int, error) {
	ids, err := s.store.PlayerIDs(ctx)
	if err != nil {
		return 0, err
	}
	sent := 0
	for _, playerID := range ids {
		if _, err = s.MailImplSend(ctx, playerID, title, body, sendName, rewards); err != nil {
			s.log.Error("broadcast mail insert failed", "player_id", playerID, "err", err)
			continue
		}
		sent++
	}
	return sent, nil
}

// MailImplClaim rewards one mail and returns the granted item counts so the
// handler can push a BagItemChangeS2C afterwards.
func (s *Server) MailImplClaim(ctx context.Context, playerID int64, id int32) (map[int32]int32, error) {
	return s.store.ClaimMailRewards(ctx, playerID, id)
}

// MailImplInventoryPush notifies a client that its bag changed (reward claim).
func (s *Server) MailImplInventoryPush(playerID int64, counts map[int32]int32) {
	if playerID <= 0 || len(counts) == 0 {
		return
	}
	ids := make([]int, 0, len(counts))
	for id := range counts {
		ids = append(ids, int(id))
	}
	sort.Ints(ids)
	items := make([]*modelpb.ItemEtc, 0, len(ids))
	for _, rawID := range ids {
		items = append(items, &modelpb.ItemEtc{ItemId: int32(rawID), Count: counts[int32(rawID)]})
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{Item: items, IsNotShow: true}, []int64{playerID}, 0)
	s.broadcastPush(push, [3]byte{1, 0, 0})
}

// MailImplDelete removes a read, already rewarded mail from storage so it no
// longer reappears in the next ConnectS2C snapshot.
func (s *Server) MailImplDelete(ctx context.Context, playerID int64, id int32) error {
	return s.store.DeleteReadMail(ctx, playerID, id)
}
