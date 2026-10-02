package gateway

import (
	"context"
	"encoding/json"
	"net/http"
	"strconv"

	"astralparty-server/internal/db"
)

// NoticeStore is the subset of the persistent store the notice feed needs.
type NoticeStore interface {
	EnabledNotices(ctx context.Context) ([]db.Notice, error)
}

type NoticeTextPayload struct {
	Cn  string `json:"cn"`
	En  string `json:"en"`
	Jp  string `json:"jp"`
	Cht string `json:"cht"`
	Ko  string `json:"ko"`
}

type NoticeItemPayload struct {
	Stencil string            `json:"stencil"`
	Banner  string            `json:"banner"`
	Content NoticeTextPayload `json:"content"`
}

type NoticePayload struct {
	ID    int64               `json:"id"`
	Sort  int                 `json:"sort"`
	Title NoticeTextPayload   `json:"title"`
	Items []NoticeItemPayload `json:"items"`
}

type NoticeServerPayload struct {
	State int               `json:"state"`
	Hint  NoticeTextPayload `json:"hint"`
}

type NoticesPayload struct {
	Server  NoticeServerPayload `json:"server"`
	Notices []NoticePayload     `json:"notices"`
}

func localizedPayload(text db.LocalizedText) NoticeTextPayload {
	normalized := text.Normalize()
	return NoticeTextPayload{Cn: normalized.Cn, En: normalized.En, Jp: normalized.Jp, Cht: normalized.Cht, Ko: normalized.Ko}
}

// NoticesContent builds the announcement payload the client's
// NoticesServerManager parses out of the {ret, msg, content} envelope. It is
// shared by the /Temp/Notice feed and the BnSdk /api/data/get endpoint so both
// entry points can never drift apart.
func NoticesContent(ctx context.Context, store NoticeStore) (NoticesPayload, error) {
	payload := NoticesPayload{
		Server:  NoticeServerPayload{State: 1},
		Notices: make([]NoticePayload, 0, 4),
	}
	if store != nil {
		notices, err := store.EnabledNotices(ctx)
		if err != nil {
			return payload, err
		}
		for _, notice := range notices {
			payload.Notices = append(payload.Notices, NoticePayload{
				ID:    notice.ID,
				Sort:  notice.SortOrder,
				Title: localizedPayload(notice.Title),
				Items: []NoticeItemPayload{{Content: localizedPayload(notice.Content)}},
			})
		}
	}
	return payload, nil
}

// NoticeHandler serves the in-game announcement feed. The client requests it
// from the bootstrap noticeUrl as {noticeUrl}/Temp/Notice and parses it with
// JsonUtility.FromJson<NoticesServerData>. server.state must never be 0:
// LoginServiceHelper.IsInvalidForServer blocks the login button with the hint
// text when the notice request succeeds and state == 0, so a healthy feed
// always reports state 1.
func NoticeHandler(store NoticeStore) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet && r.Method != http.MethodHead {
			w.Header().Set("Allow", "GET, HEAD")
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		payload, err := NoticesContent(r.Context(), store)
		if err != nil {
			http.Error(w, "notice feed unavailable", http.StatusInternalServerError)
			return
		}
		body, err := json.Marshal(payload)
		if err != nil {
			http.Error(w, "notice feed unavailable", http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "application/json; charset=utf-8")
		w.Header().Set("Cache-Control", "no-store")
		if r.Method == http.MethodHead {
			w.Header().Set("Content-Length", strconv.Itoa(len(body)))
			return
		}
		_, _ = w.Write(body)
	})
}
