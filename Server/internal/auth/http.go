package auth

import (
	"bytes"
	"crypto/hmac"
	"crypto/rand"
	"crypto/sha256"
	"embed"
	"encoding/base32"
	"encoding/hex"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"log/slog"
	"math/big"
	"mime"
	"net"
	"net/http"
	"net/mail"
	"net/url"
	"strings"
	"time"
	"unicode/utf8"

	serverconfig "astralparty-server/internal/config"
	"astralparty-server/internal/db"
	"astralparty-server/internal/gateway"
	"astralparty-server/internal/httpsecurity"
	traceevent "astralparty-server/internal/trace"
)

//go:embed login.html
var loginPage embed.FS

type Config struct {
	Enabled           bool
	SMTPHost          string
	SMTPPort          int
	SMTPUser          string
	SMTPPassword      string
	SMTPFrom          string
	SMTPMode          string
	SDKPaySign        string
	CodeSecret        string
	SessionTTL        time.Duration
	BehindTLSProxy    bool
	TrustedProxyCIDRs string
	AllowHTTP         bool
	ProtocolTrace     func(traceevent.Event)
	TraceEnabled      func() bool
	TraceFull         func() bool
	PlayerOnline      func(int64) bool
	PushInventory     func(int64, map[int32]int32) bool
}

type Mailer interface {
	Send(to, subject, body string) error
}

type HTTPHandler struct {
	store          *db.Store
	log            *slog.Logger
	cfg            Config
	mailer         Mailer
	trustedProxies []*net.IPNet
	trace          func(traceevent.Event)
	traceEnabled   func() bool
	traceFull      func() bool
}

type emailRequest struct {
	Email string `json:"email"`
}

type verifyRequest struct {
	Email    string `json:"email"`
	Code     string `json:"code"`
	DeviceID string `json:"deviceId"`
}

type authResponse struct {
	AccountNo string `json:"accountNo"`
	Phone     string `json:"phone"`
	Email     string `json:"email"`
	Nick      string `json:"nick"`
}

func NewHTTPHandler(store *db.Store, cfg Config, logger *slog.Logger) (*HTTPHandler, error) {
	if store == nil {
		return nil, errors.New("self-hosted authentication requires a database store")
	}
	if logger == nil {
		logger = slog.Default()
	}
	if cfg.SessionTTL <= 0 {
		cfg.SessionTTL = 30 * 24 * time.Hour
	}
	trustedProxies, err := httpsecurity.ParseTrustedProxyCIDRs(cfg.TrustedProxyCIDRs)
	if err != nil {
		return nil, fmt.Errorf("AUTH_TRUSTED_PROXY_CIDRS: %w", err)
	}
	if cfg.BehindTLSProxy && len(trustedProxies) == 0 {
		return nil, errors.New("AUTH_TRUSTED_PROXY_CIDRS must include the TLS proxy address when AUTH_BEHIND_TLS_PROXY is enabled")
	}
	if cfg.Enabled && serverconfig.IsExampleSecret(cfg.CodeSecret) {
		return nil, errors.New("AUTH_CODE_HMAC_SECRET still uses an example placeholder")
	}
	if len(cfg.CodeSecret) < 32 {
		if cfg.Enabled {
			return nil, errors.New("AUTH_CODE_HMAC_SECRET must contain at least 32 bytes")
		}
		// The game and bootstrap listeners can run while optional email login is
		// unconfigured. Use a process-only key until email auth is explicitly
		// enabled; enabling it still requires an operator-provided persistent key.
		secret, err := randomToken()
		if err != nil {
			return nil, fmt.Errorf("generate temporary auth key: %w", err)
		}
		cfg.CodeSecret = secret
	}
	if cfg.Enabled {
		if serverconfig.IsExampleEndpoint(cfg.SMTPHost) || serverconfig.IsExampleEndpoint(cfg.SMTPFrom) ||
			serverconfig.IsExampleSecret(cfg.SMTPUser) || serverconfig.IsExampleSecret(cfg.SMTPPassword) {
			return nil, errors.New("email authentication SMTP settings still contain example placeholders")
		}
		if strings.TrimSpace(cfg.SMTPHost) == "" || cfg.SMTPPort <= 0 || strings.TrimSpace(cfg.SMTPFrom) == "" {
			return nil, errors.New("email auth enabled but SMTP host, port or sender is missing")
		}
		if cfg.SMTPMode != "starttls" && cfg.SMTPMode != "implicit" {
			return nil, errors.New("SMTP_TLS_MODE must be starttls or implicit")
		}
	}
	h := &HTTPHandler{store: store, log: logger, cfg: cfg, trustedProxies: trustedProxies, trace: cfg.ProtocolTrace, traceEnabled: cfg.TraceEnabled, traceFull: cfg.TraceFull}
	if cfg.Enabled {
		mailer, err := NewSMTPMailer(cfg.SMTPHost, cfg.SMTPPort, cfg.SMTPUser, cfg.SMTPPassword, cfg.SMTPFrom, cfg.SMTPMode)
		if err != nil {
			return nil, err
		}
		h.mailer = mailer
	}
	return h, nil
}

func (h *HTTPHandler) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	if h.trace == nil || !traceableHTTPPath(r.URL.Path) || h.traceEnabled != nil && !h.traceEnabled() {
		h.serveHTTP(w, r)
		return
	}
	h.serveTracedHTTP(w, r)
}

func (h *HTTPHandler) serveHTTP(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("X-Content-Type-Options", "nosniff")
	w.Header().Set("Referrer-Policy", "no-referrer")
	w.Header().Set("Cache-Control", "no-store")
	w.Header().Set("Content-Security-Policy", "default-src 'none'; connect-src 'self'; style-src 'unsafe-inline'; script-src 'unsafe-inline'; form-action 'self'; base-uri 'none'; frame-ancestors 'none'")
	if r.URL.Path == "/auth" || r.URL.Path == "/auth/" {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		b, err := loginPage.ReadFile("login.html")
		if err != nil {
			http.Error(w, "auth page unavailable", http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "text/html; charset=utf-8")
		_, _ = w.Write(b)
		return
	}
	if r.URL.Path == "/api/client/readiness" {
		h.clientReadiness(w, r)
		return
	}
	if r.URL.Path == "/api/init" || r.URL.Path == "/api/data/get" || strings.HasPrefix(r.URL.Path, "/account/") || isBnSDKMaintenancePath(r.URL.Path) {
		h.serveBnSDK(w, r)
		return
	}
	if !strings.HasPrefix(r.URL.Path, "/api/auth/") {
		http.NotFound(w, r)
		return
	}
	if r.URL.Path == "/api/auth/player/self" {
		if r.Method != http.MethodGet && r.Method != http.MethodPatch {
			w.Header().Set("Allow", "GET, PATCH")
			writeJSON(w, http.StatusMethodNotAllowed, map[string]any{"error": "method not allowed"})
			return
		}
	} else if r.Method != http.MethodPost {
		w.Header().Set("Allow", http.MethodPost)
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}
	if !h.secureRequest(r) {
		writeJSON(w, http.StatusUpgradeRequired, map[string]any{"error": "HTTPS is required"})
		return
	}
	switch r.URL.Path {
	case "/api/auth/email/start":
		if !h.cfg.Enabled {
			h.log.Warn("email registration unavailable", "path", r.URL.Path, "reason", "email_auth_disabled")
			writeJSON(w, http.StatusServiceUnavailable, map[string]any{"error": "email authentication is disabled"})
			return
		}
		h.startEmail(w, r)
	case "/api/auth/email/verify":
		if !h.cfg.Enabled {
			h.log.Warn("email verification unavailable", "path", r.URL.Path, "reason", "email_auth_disabled")
			writeJSON(w, http.StatusServiceUnavailable, map[string]any{"error": "email authentication is disabled"})
			return
		}
		h.verifyEmail(w, r)
	case "/api/auth/logout":
		h.logout(w, r)
	case "/api/auth/player/login":
		h.playerWebLogin(w, r)
	case "/api/auth/player/password":
		if r.Method != http.MethodPost {
			w.Header().Set("Allow", http.MethodPost)
			writeJSON(w, http.StatusMethodNotAllowed, map[string]any{"error": "method not allowed"})
			return
		}
		h.playerWebPassword(w, r)
	case "/api/auth/player/self":
		if r.Method != http.MethodGet {
			w.Header().Set("Allow", http.MethodGet)
			writeJSON(w, http.StatusMethodNotAllowed, map[string]any{"error": "method not allowed"})
			return
		}
		h.playerWebSelf(w, r)
	case "/api/auth/player/logout":
		h.playerWebLogout(w, r)
	default:
		http.NotFound(w, r)
	}
}

func traceableHTTPPath(path string) bool {
	return path == "/api/init" || path == "/api/client/readiness" || strings.HasPrefix(path, "/account/") || strings.HasPrefix(path, "/api/auth/")
}

// isBnSDKMaintenancePath reports whether path is one of the BnSdk housekeeping
// endpoints the game client calls during startup. They are answered with empty
// success envelopes so the SDK does not stall before login.
func isBnSDKMaintenancePath(path string) bool {
	switch path {
	case "/api/backState", "/api/ysdksw", "/api/request", "/api/preRequest":
		return true
	default:
		return false
	}
}

// clientReadiness reports only whether client registration and login are
// configured. It does not reveal SMTP settings or credentials, and a positive
// result does not prove delivery to a particular recipient.
func (h *HTTPHandler) clientReadiness(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet {
		w.Header().Set("Allow", http.MethodGet)
		writeJSON(w, http.StatusMethodNotAllowed, map[string]any{"error": "method not allowed"})
		return
	}

	encrypted := httpsecurity.IsHTTPSRequest(r, h.cfg.BehindTLSProxy, h.trustedProxies)
	allowed := h.cfg.AllowHTTP || encrypted
	emailReady := h.cfg.Enabled && h.mailer != nil && allowed
	// BnSdk phone-code login rides /account/* OTP and needs only an allowed
	// transport; it does not depend on the email pipeline.
	phoneReady := allowed
	ready := emailReady || phoneReady
	reason := ""
	switch {
	case ready:
		reason = ""
	case !h.cfg.Enabled && !allowed:
		reason = "https_required"
	case !allowed:
		reason = "https_required"
	case h.cfg.Enabled && h.mailer == nil:
		reason = "email_delivery_unconfigured"
	default:
		reason = "email_auth_disabled"
	}
	h.log.Info("client login readiness probe", "ready", ready, "reason", reason, "email_ready", emailReady, "phone_ready", phoneReady, "request_allowed", allowed, "transport_encrypted", encrypted)
	writeJSON(w, http.StatusOK, map[string]any{
		"loginReady": ready, "emailAuthEnabled": h.cfg.Enabled,
		"phoneLoginEnabled":       phoneReady,
		"emailDeliveryConfigured": h.mailer != nil, "requestSecure": encrypted,
		"requestAllowed": allowed, "transportEncrypted": encrypted, "reason": reason,
	})
}

func (h *HTTPHandler) serveTracedHTTP(w http.ResponseWriter, r *http.Request) {
	originalBody := r.Body
	if originalBody == nil {
		originalBody = http.NoBody
	}
	body, readErr := io.ReadAll(io.LimitReader(originalBody, maxTraceHTTPBody+1))
	r.Body = &replayReadCloser{
		Reader: io.MultiReader(bytes.NewReader(body), originalBody),
		Closer: originalBody,
	}
	requestBytes := len(body)
	if r.ContentLength > int64(requestBytes) {
		requestBytes = int(r.ContentLength)
	}
	includeSensitive := h.traceFull != nil && h.traceFull()
	requestPayload, requestRedacted, requestOmitted := traceHTTPPayload(body, r.Header.Get("Content-Type"), r.URL.Query(), true, includeSensitive)
	if readErr != nil {
		requestOmitted = "request_body_read_failed"
		requestPayload = nil
		requestRedacted = nil
	}
	response := &traceResponseWriter{ResponseWriter: w}
	h.serveHTTP(response, r)
	status := response.status
	if status == 0 {
		status = http.StatusOK
	}
	requestEvent := traceevent.Event{
		Timestamp: time.Now().UTC(), Transport: "http", Direction: "request",
		Message: r.Method + " " + r.URL.Path, PayloadBytes: requestBytes,
		RedactedFields: requestRedacted, Payload: requestPayload, PayloadOmitted: requestOmitted,
	}
	responsePayload, responseRedacted, responseOmitted := traceHTTPPayload(response.body.Bytes(), "application/json", nil, false, includeSensitive)
	if response.bodyTooLarge {
		responsePayload, responseRedacted, responseOmitted = nil, nil, "over_64KiB"
	}
	responseEvent := traceevent.Event{
		Timestamp: time.Now().UTC(), Transport: "http", Direction: "response",
		Message: r.Method + " " + r.URL.Path, HTTPStatus: status,
		PayloadBytes: response.bytesWritten, RedactedFields: responseRedacted,
		Payload: responsePayload, PayloadOmitted: responseOmitted,
	}
	h.trace(requestEvent)
	h.trace(responseEvent)
}

const maxTraceHTTPBody = 64 * 1024

type replayReadCloser struct {
	io.Reader
	io.Closer
}

type traceResponseWriter struct {
	http.ResponseWriter
	status       int
	bytesWritten int
	body         bytes.Buffer
	bodyTooLarge bool
}

func (w *traceResponseWriter) WriteHeader(status int) {
	if w.status != 0 {
		return
	}
	w.status = status
	w.ResponseWriter.WriteHeader(status)
}

func (w *traceResponseWriter) Write(body []byte) (int, error) {
	if w.status == 0 {
		w.WriteHeader(http.StatusOK)
	}
	w.bytesWritten += len(body)
	if !w.bodyTooLarge {
		remaining := maxTraceHTTPBody + 1 - w.body.Len()
		if len(body) > remaining {
			w.body.Write(body[:remaining])
			w.bodyTooLarge = true
		} else {
			_, _ = w.body.Write(body)
		}
	}
	return w.ResponseWriter.Write(body)
}

func traceHTTPPayload(body []byte, contentType string, query url.Values, request bool, includeSensitive bool) (json.RawMessage, []string, string) {
	if len(body) > maxTraceHTTPBody {
		return nil, nil, "over_64KiB"
	}
	contentType, _, _ = mime.ParseMediaType(contentType)
	var payload []byte
	if len(body) == 0 {
		if len(query) == 0 {
			return nil, nil, "empty_body"
		}
		values := make(map[string][]string, len(query))
		for key, entries := range query {
			values[key] = entries
		}
		payload, _ = json.Marshal(values)
	} else if strings.Contains(strings.ToLower(contentType), "json") || bytes.HasPrefix(bytes.TrimSpace(body), []byte("{")) || bytes.HasPrefix(bytes.TrimSpace(body), []byte("[")) {
		payload = body
	} else if strings.Contains(strings.ToLower(contentType), "x-www-form-urlencoded") {
		values, err := url.ParseQuery(string(body))
		if err != nil {
			return nil, nil, "invalid_form"
		}
		payload, _ = json.Marshal(values)
	} else {
		return nil, nil, "non_json_body"
	}
	if includeSensitive {
		return traceevent.CaptureJSON(payload)
	}
	return traceevent.RedactJSON(payload, request)
}

func (h *HTTPHandler) startEmail(w http.ResponseWriter, r *http.Request) {
	var q emailRequest
	if err := decodeJSON(w, r, &q); err != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid request"})
		return
	}
	email, ok := normalizeEmail(q.Email)
	if !ok {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid email address"})
		return
	}
	if err := h.issueEmailCode(r, email, "email_code_start", ""); err != nil {
		if errors.Is(err, db.ErrEmailRateLimited) {
			writeJSON(w, http.StatusTooManyRequests, map[string]any{"error": "please wait before requesting another code"})
			return
		}
		writeJSON(w, http.StatusServiceUnavailable, map[string]any{"error": "verification email could not be delivered"})
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"ok": true, "expiresIn": 600, "resendAfter": 60})
}

func (h *HTTPHandler) issueEmailCode(r *http.Request, email, operation, sdkTokenHash string) error {
	now := time.Now()
	code, err := randomCode()
	if err != nil {
		h.log.Error("email challenge generation failed", "err", err)
		return err
	}
	codeHash := h.codeHash(email, code)
	if err = h.store.IssueEmailCode(r.Context(), email, codeHash, sdkTokenHash, h.ipHash(r.RemoteAddr), now.Unix(), now.Add(10*time.Minute).Unix()); err != nil {
		if !errors.Is(err, db.ErrEmailRateLimited) {
			h.log.Error("email challenge persistence failed", "err", err)
		}
		return err
	}
	body := fmt.Sprintf("Your Astral Party sign-in code is %s. It expires in 10 minutes. If you did not request it, ignore this email.\r\n", code)
	if err = h.mailer.Send(email, "Astral Party sign-in code", body); err != nil {
		_ = h.store.InvalidateEmailCode(r.Context(), email, codeHash, time.Now().Unix())
		h.log.Error("email challenge delivery failed", "err", err)
		return err
	}
	h.log.Info("email challenge sent", "operation", operation)
	return nil
}

// initContent builds the BnSdk /api/init response body.
//
// The bundled BnSdk parses this into InitEntity and then runs
// BnDefaultDataUtils.judgeInitEntity. InitEntity's constructor initializes
// area_code and replace_lp but leaves float_setting null, and judgeInitEntity
// takes the null branch and calls float_setting.add() without assigning a list
// first (BnDefaultDataUtils.java:86), which throws a NullPointerException on
// the main thread and crashes the game. Sending every list field as an array
// keeps them non-null so those adds succeed.
func initContent() map[string]any {
	return map[string]any{
		"auto_reg": "0", "only_phone_login": "1", "close_reg_view": "0",
		// Select the SDK's built-in country-code fallback explicitly. The
		// InitEntity constructor also defaults this field to an empty list.
		"area_code":     []any{},
		"float_setting": []any{},
		"replace_lp":    []any{},
	}
}

// serveBnSDK accepts the original account and initialization paths sent by the
// game client. The LSP only changes the HTTP origin; it does not rename these
// routes or alter request payloads. Arbitrary SDK and payment URLs are not proxied.
func (h *HTTPHandler) serveBnSDK(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("X-Content-Type-Options", "nosniff")
	w.Header().Set("Cache-Control", "no-store")
	if r.URL.Path == "/api/data/get" {
		// The in-game announcement feed is fetched from the bootstrap noticeUrl
		// as plain HTTP on the CDN port, so it has to be served ahead of the TLS
		// gate. Only public announcement data is exposed through this branch.
		content, err := gateway.NoticesContent(r.Context(), h.store)
		if err != nil {
			h.log.Warn("BnSdk data endpoint notice feed unavailable", "path", r.URL.Path, "err", err)
			writeBnSDKError(w, r.URL.Path, 10015, "notice feed unavailable")
			return
		}
		h.log.Info("notice feed served from data endpoint", "path", r.URL.Path, "notices", len(content.Notices))
		writeNoticeFeed(w, content)
		return
	}
	if !h.secureRequest(r) {
		h.log.Warn("BnSdk request rejected", "path", r.URL.Path, "reason", "https_required")
		writeBnSDKError(w, r.URL.Path, 10000, "HTTPS is required")
		return
	}
	if r.URL.Path == "/api/init" {
		if r.Method != http.MethodPost {
			writeBnSDKResponse(w, 10013, "method not allowed", map[string]any{})
			return
		}
		if _, err := readBnSDKParams(r); err != nil {
			writeBnSDKResponse(w, 10013, "invalid request", map[string]any{})
			return
		}
		content := initContent()
		writeBnSDKResponse(w, 0, "success", content)
		return
	}
	if r.Method != http.MethodPost {
		writeBnSDKError(w, r.URL.Path, 10013, "method not allowed")
		return
	}
	params, err := readBnSDKParams(r)
	if err != nil {
		writeBnSDKError(w, r.URL.Path, 10013, "invalid request")
		return
	}
	switch r.URL.Path {
	case "/account/sendCode":
		h.sendBnSDKCode(w, r, params)
	case "/account/authorize":
		h.authorizeBnSDKPhone(w, r, params)
	case "/account/userReg":
		h.registerBnSDKPhone(w, r, params)
	case "/account/quickReg":
		h.quickRegisterBnSDKPhone(w, r, params)
	case "/account/info":
		h.bnSDKUserInfo(w, r, params)
	case "/account/bindReal", "/account/opBind", "/account/telChgPwd":
		// Real-name and binding flows are not part of a self-hosted server;
		// acknowledge so the SDK does not stall on a missing endpoint.
		h.log.Info("BnSdk endpoint acknowledged without state", "path", r.URL.Path)
		writeBnSDKResponse(w, 0, "success", map[string]any{})
	case "/api/backState", "/api/ysdksw", "/api/request", "/api/preRequest":
		// SDK housekeeping calls: return an empty success envelope.
		h.log.Info("BnSdk maintenance endpoint acknowledged", "path", r.URL.Path)
		writeBnSDKResponse(w, 0, "success", map[string]any{})
	default:
		writeBnSDKResponse(w, 10015, "unsupported SDK endpoint", map[string]any{})
	}
}

func (h *HTTPHandler) sendBnSDKCode(w http.ResponseWriter, r *http.Request, params map[string]string) {
	phone, ok := normalizeVirtualPhone(sdkParam(params, "phone", "tel_num", "mobile"))
	if !ok {
		writeBnSDKError(w, r.URL.Path, 10013, "invalid phone number")
		return
	}
	// Phone-first mode: the number itself is the account. There is no SMS or
	// email delivery; the client only needs to advance to its code screen, and
	// any code the player enters is accepted at authorize time.
	h.log.Info("BnSdk code requested", "operation", "sdk_send_code", "phone_suffix", phone[len(phone)-4:], "delivery", "none_phone_first")
	// This APK's sendCode callback declares content as a Java List and does not
	// read its elements. Returning an object makes Gson fail the callback cast.
	writeBnSDKResponse(w, 0, "success", []any{})
}

func (h *HTTPHandler) authorizeBnSDKPhone(w http.ResponseWriter, r *http.Request, params map[string]string) {
	h.completePhoneLogin(w, r, params, "sdk_phone_authorize")
}

func (h *HTTPHandler) registerBnSDKPhone(w http.ResponseWriter, r *http.Request, params map[string]string) {
	h.completePhoneLogin(w, r, params, "sdk_phone_register")
}

// completePhoneLogin signs in a phone-first account: the phone number is the
// username. userReg creates the account on first use, authorize reuses it, and
// the verification code is not validated because there is no delivery channel.
func (h *HTTPHandler) completePhoneLogin(w http.ResponseWriter, r *http.Request, params map[string]string, operation string) {
	phone, ok := normalizeVirtualPhone(sdkParam(params, "phone", "tel_num", "mobile"))
	deviceID := sdkParam(params, "device_id", "deviceid", "oaid")
	if !ok || len(deviceID) > 128 || !utf8.ValidString(deviceID) {
		writeBnSDKResponse(w, 10013, "invalid login data", map[string]any{})
		return
	}
	now := time.Now()
	account, err := h.store.EnsurePhoneAccount(r.Context(), phone, deviceID, now.Unix())
	if errors.Is(err, db.ErrAccountDisabled) {
		h.log.Warn("BnSdk phone login rejected", "operation", operation, "reason", "account_disabled")
		writeBnSDKResponse(w, 10000, "account is disabled", map[string]any{})
		return
	}
	if err != nil {
		h.log.Error("BnSdk phone account resolve failed", "operation", operation, "err", err)
		writeBnSDKResponse(w, 10000, "login service unavailable", map[string]any{})
		return
	}
	token, err := randomToken()
	if err != nil {
		h.log.Error("BnSdk session generation failed", "err", err)
		writeBnSDKResponse(w, 10000, "login service unavailable", map[string]any{})
		return
	}
	expiresAt := now.Add(h.cfg.SessionTTL).Unix()
	if err = h.store.IssueSelfSession(r.Context(), account.ID, db.SessionTokenHash(token), now.Unix(), expiresAt); err != nil {
		h.log.Error("BnSdk session persistence failed", "err", err)
		writeBnSDKResponse(w, 10000, "login service unavailable", map[string]any{})
		return
	}
	encodedUserID, err := encodeBNSDKUserID(account.Number, h.cfg.SDKPaySign)
	if err != nil {
		h.log.Error("BnSdk user ID encoding failed", "err", err)
		writeBnSDKResponse(w, 10000, "login service unavailable", map[string]any{})
		return
	}
	gameID := sdkParam(params, "game_id", "gameid")
	// This APK sends the channel under `channel`; accept the common ID aliases too.
	channelID := sdkParam(params, "channel", "channel_id", "channelid")
	appID := sdkParam(params, "app_id", "appid")
	if gameID == "" {
		gameID = "120000182"
	}
	if appID == "" {
		appID = "110001949"
	}
	data := map[string]any{
		"user_id": encodedUserID, "user_name": account.Nick, "password": "",
		"phone": account.Phone, "authorize_code": token,
		// The bundled BnSDK requires cData != null for its bingniao channel
		// callback. Its internal keys are opaque and are not used by game Connect.
		"cData": map[string]any{},
		"data": map[string]any{
			"gameId": gameID, "channelId": channelID, "appId": appID,
			"userId": account.Number, "accessToken": token,
		},
	}
	h.log.Info("BnSdk phone login succeeded", "operation", operation, "account_id", account.ID, "player_id", account.PlayerID)
	writeBnSDKResponse(w, 0, "success", data)
}

func (h *HTTPHandler) quickRegisterBnSDKPhone(w http.ResponseWriter, r *http.Request, _ map[string]string) {
	// The APK uses quickReg only for auto_reg=1 with a local account and expects a
	// UsernameEntity. This server returns auto_reg=0 and has no username/password
	// credential contract, so reject instead of consuming an OTP or creating a
	// game player through a response path the client does not expect.
	h.log.Warn("BnSdk quick registration rejected", "reason", "auto_reg_disabled")
	writeBnSDKError(w, r.URL.Path, 10015, "quick registration is disabled")
}

func (h *HTTPHandler) bnSDKUserInfo(w http.ResponseWriter, r *http.Request, params map[string]string) {
	token := sdkParam(params, "authorize_code", "sid", "access_token", "accesstoken", "token")
	if token == "" {
		token = bearerToken(r.Header.Get("Authorization"))
	}
	if token == "" {
		writeBnSDKResponse(w, 10000, "login required", map[string]any{})
		return
	}
	account, err := h.store.ResolveSelfSession(r.Context(), db.SessionTokenHash(token), time.Now().Unix())
	if err != nil || account.Disabled {
		writeBnSDKResponse(w, 10000, "login required", map[string]any{})
		return
	}
	writeBnSDKResponse(w, 0, "success", map[string]any{
		"user_id": account.Number, "user_name": account.Nick, "phone": account.Phone, "tel_num": account.Phone,
		"authorize_code": token, "sid": token, "is_visitor": false,
	})
}

// writeBnSDKResponse matches the Android SDK BaseResponse contract: ret="1"
// denotes success, content carries the typed response model, and msg carries
// the user-facing result. Internal error codes are deliberately not exposed.
func writeBnSDKResponse(w http.ResponseWriter, code int, message string, content any) {
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.Header().Set("Cache-Control", "no-store")
	w.WriteHeader(http.StatusOK)
	ret := "0"
	if code == 0 {
		ret = "1"
	}
	_ = json.NewEncoder(w).Encode(map[string]any{"ret": ret, "msg": message, "content": content})
}

// noticeMaintenanceHint is reported when the feed says the server is under
// maintenance. A non-zero state keeps it from ever being shown.
var noticeMaintenanceHint = map[string]any{
	"cn":  "Welcome to the AstralParty private server. The server is under maintenance, please try again later.",
	"en":  "Welcome to the AstralParty private server. The server is under maintenance, please try again later.",
	"jp":  "Welcome to the AstralParty private server. The server is under maintenance, please try again later.",
	"cht": "Welcome to the AstralParty private server. The server is under maintenance, please try again later.",
	"ko":  "AstralParty 프라이빗 서버에 오신 것을 환영합니다. 서버 점검 중입니다. 나중에 다시 시도해 주세요.",
}

// writeNoticeFeed answers the announcement request that NoticesServerManager
// issues against the bootstrap noticeUrl. The SDK envelope carries the feed in
// "content", but LoginServiceHelper.IsInvalidForServer dereferences
// noticesServerData.server.hint without a null check, so a partially populated
// server object crashes the login button. The payload is therefore mirrored at
// both levels with state 1 and a populated hint, so whichever position the
// client reads, it sees a healthy, non-maintenance feed.
func writeNoticeFeed(w http.ResponseWriter, content gateway.NoticesPayload) {
	server := map[string]any{
		"state": 1,
		"hint":  noticeMaintenanceHint,
	}
	serverAsString := map[string]any{
		"state": "1",
		"hint":  noticeMaintenanceHint,
	}
	body := map[string]any{
		"ret":     "1",
		"msg":     "success",
		"content": content,
		"server":  server,
		"notices": content.Notices,
		// Some client builds read the maintenance state as a string field.
		"serverState":   "1",
		"serverStateAs": serverAsString,
	}
	raw, err := json.Marshal(body)
	if err != nil {
		http.Error(w, "notice feed unavailable", http.StatusInternalServerError)
		return
	}
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.Header().Set("Cache-Control", "no-store")
	w.Header().Set("X-Content-Type-Options", "nosniff")
	w.WriteHeader(http.StatusOK)
	_, _ = w.Write(raw)
}

func writeBnSDKError(w http.ResponseWriter, path string, code int, message string) {
	var content any = map[string]any{}
	if path == "/account/sendCode" {
		// The APK's sendCode success and error callbacks both cast content to List.
		content = []any{}
	}
	writeBnSDKResponse(w, code, message, content)
}

func readBnSDKParams(r *http.Request) (map[string]string, error) {
	if r.ContentLength > 65536 {
		return nil, errors.New("SDK request too large")
	}
	body, err := io.ReadAll(io.LimitReader(r.Body, 65537))
	if err != nil || len(body) > 65536 {
		return nil, errors.New("SDK request too large")
	}
	params := make(map[string]string)
	contentType, _, _ := mime.ParseMediaType(r.Header.Get("Content-Type"))
	if len(body) > 0 && (strings.Contains(contentType, "json") || strings.HasPrefix(strings.TrimSpace(string(body)), "{")) {
		var value any
		decoder := json.NewDecoder(strings.NewReader(string(body)))
		decoder.UseNumber()
		if err := decoder.Decode(&value); err != nil {
			return nil, err
		}
		flattenBnSDKParams(params, "", value)
	} else if len(body) > 0 {
		form, err := url.ParseQuery(string(body))
		if err != nil {
			return nil, err
		}
		for key, values := range form {
			if len(values) > 0 {
				params[strings.ToLower(key)] = values[len(values)-1]
			}
		}
	}
	for key, values := range r.URL.Query() {
		if len(values) > 0 {
			params[strings.ToLower(key)] = values[len(values)-1]
		}
	}
	return params, nil
}

func flattenBnSDKParams(dst map[string]string, parent string, value any) {
	switch item := value.(type) {
	case map[string]any:
		for key, child := range item {
			flattenBnSDKParams(dst, key, child)
		}
	case json.Number:
		if parent != "" {
			dst[strings.ToLower(parent)] = item.String()
		}
	case string:
		if parent != "" {
			trimmed := strings.TrimSpace(item)
			if strings.HasPrefix(trimmed, "{") {
				var nested any
				if json.Unmarshal([]byte(trimmed), &nested) == nil {
					flattenBnSDKParams(dst, parent, nested)
					return
				}
			}
			dst[strings.ToLower(parent)] = trimmed
		}
	case bool:
		if parent != "" {
			dst[strings.ToLower(parent)] = fmt.Sprint(item)
		}
	}
}

func sdkParam(params map[string]string, names ...string) string {
	for _, name := range names {
		if value := strings.TrimSpace(params[strings.ToLower(name)]); value != "" {
			return value
		}
	}
	return ""
}

func normalizeVirtualPhone(value string) (string, bool) {
	value = strings.NewReplacer("+", "", "-", "", " ", "", "(", "", ")", "").Replace(strings.TrimSpace(value))
	if strings.HasPrefix(value, "86") && len(value) == 13 {
		value = value[2:]
	}
	if len(value) != 11 || !asciiDigits(value) {
		return "", false
	}
	return value, true
}

func (h *HTTPHandler) verifyEmail(w http.ResponseWriter, r *http.Request) {
	var q verifyRequest
	if err := decodeJSON(w, r, &q); err != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid request"})
		return
	}
	email, ok := normalizeEmail(q.Email)
	if !ok || len(q.Code) != 6 || !asciiDigits(q.Code) || len(q.DeviceID) > 128 || !utf8.ValidString(q.DeviceID) {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid verification data"})
		return
	}
	now := time.Now()
	account, err := h.store.ConsumeEmailCode(r.Context(), email, h.codeHash(email, q.Code), "", "", "", now.Unix(), 0)
	if err != nil {
		if errors.Is(err, db.ErrEmailCodeInvalid) || errors.Is(err, db.ErrAccountDisabled) {
			h.log.Warn("email verification rejected", "operation", "email_code_verify", "reason", "invalid_or_expired_or_disabled")
			writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "verification code is invalid or expired"})
			return
		}
		h.log.Error("email verification persistence failed", "err", err)
		writeJSON(w, http.StatusServiceUnavailable, map[string]any{"error": "verification service unavailable"})
		return
	}
	h.log.Info("email login verified", "account_id", account.ID, "account_no", account.Number)
	writeJSON(w, http.StatusOK, authResponse{AccountNo: account.Number, Phone: account.Phone, Email: account.Email, Nick: account.Nick})
}

func (h *HTTPHandler) logout(w http.ResponseWriter, r *http.Request) {
	token := bearerToken(r.Header.Get("Authorization"))
	if token == "" {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "missing bearer token"})
		return
	}
	if err := h.store.RevokeEmailSession(r.Context(), db.SessionTokenHash(token), time.Now().Unix()); err != nil {
		h.log.Error("email session revoke failed", "err", err)
		writeJSON(w, http.StatusServiceUnavailable, map[string]any{"error": "logout failed"})
		return
	}
	h.log.Info("email session revoked", "operation", "logout")
	writeJSON(w, http.StatusOK, map[string]any{"ok": true})
}

func (h *HTTPHandler) secureRequest(r *http.Request) bool {
	return httpsecurity.AllowsSensitiveRequest(r, h.cfg.AllowHTTP, h.cfg.BehindTLSProxy, h.trustedProxies)
}

func (h *HTTPHandler) codeHash(email, code string) string {
	m := hmac.New(sha256.New, []byte(h.cfg.CodeSecret))
	_, _ = io.WriteString(m, email+"\x00"+code)
	return hex.EncodeToString(m.Sum(nil))
}

func (h *HTTPHandler) ipHash(remote string) string {
	host, _, err := net.SplitHostPort(remote)
	if err != nil {
		host = remote
	}
	m := hmac.New(sha256.New, []byte(h.cfg.CodeSecret))
	_, _ = io.WriteString(m, "ip\x00"+host)
	return hex.EncodeToString(m.Sum(nil))
}

func decodeJSON(w http.ResponseWriter, r *http.Request, dst any) error {
	if r.ContentLength > 4096 {
		return errors.New("request body too large")
	}
	dec := json.NewDecoder(http.MaxBytesReader(w, r.Body, 4096))
	dec.DisallowUnknownFields()
	if err := dec.Decode(dst); err != nil {
		return err
	}
	var trailing any
	if err := dec.Decode(&trailing); !errors.Is(err, io.EOF) {
		return errors.New("trailing JSON data")
	}
	return nil
}

func normalizeEmail(s string) (string, bool) {
	s = strings.TrimSpace(s)
	if len(s) < 3 || len(s) > 254 {
		return "", false
	}
	a, err := mail.ParseAddress(s)
	if err != nil || !strings.EqualFold(a.Address, s) || strings.ContainsAny(s, "\r\n\x00") {
		return "", false
	}
	return strings.ToLower(a.Address), true
}

func randomCode() (string, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(1_000_000))
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("%06d", n.Int64()), nil
}

func randomToken() (string, error) {
	b := make([]byte, 32)
	if _, err := rand.Read(b); err != nil {
		return "", err
	}
	return hex.EncodeToString(b), nil
}

func randomHandoffCode() (string, error) {
	b := make([]byte, 10)
	if _, err := rand.Read(b); err != nil {
		return "", err
	}
	return base32.StdEncoding.WithPadding(base32.NoPadding).EncodeToString(b), nil
}

func asciiDigits(s string) bool {
	for _, r := range s {
		if r < '0' || r > '9' {
			return false
		}
	}
	return true
}

func bearerToken(header string) string {
	parts := strings.Fields(header)
	if len(parts) != 2 || !strings.EqualFold(parts[0], "Bearer") {
		return ""
	}
	return parts[1]
}

func writeJSON(w http.ResponseWriter, status int, value any) {
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.WriteHeader(status)
	_ = json.NewEncoder(w).Encode(value)
}
