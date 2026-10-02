package gateway

import (
	"context"
	"encoding/base64"
	"encoding/binary"
	"errors"
	"fmt"
	"io"
	"log/slog"
	"net"
	"os"
	"path/filepath"
	"strconv"
	"strings"
	"sync"
	"sync/atomic"
	"time"

	store "astralparty-server/internal/db"
	"astralparty-server/internal/game"
	cfgstore "astralparty-server/internal/gdconf"
	"astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	traceevent "astralparty-server/internal/trace"
	"google.golang.org/protobuf/proto"
)

type Config struct {
	ListenAddr    string
	DBPath        string
	ResourceDir   string
	AllowDevLogin bool
	AllowTestRPC  bool
	BotAutoFill   bool
	ProtocolTrace bool
	TraceFull     bool
	LogPackets    bool
	MaxPayload    int
	Captures      *traceevent.CaptureStore
}

func ConfigFromEnv() Config {
	return Config{ListenAddr: env("LISTEN_ADDR", ":8800"), DBPath: env("DB_PATH", "data/server.db"),
		ResourceDir: env("RESOURCE_DIR", "../Resources/Data"), AllowDevLogin: envBool("ALLOW_DEV_LOGIN", false),
		AllowTestRPC: envBool("ALLOW_TEST_RPC", false), BotAutoFill: envBool("BOT_AUTO_FILL", true),
		ProtocolTrace: envBool("PROTOCOL_TRACE", true), MaxPayload: envInt("MAX_PAYLOAD", protocol.MaxPayload)}
}
func env(k, d string) string {
	if v := strings.TrimSpace(os.Getenv(k)); v != "" {
		return v
	}
	return d
}
func envBool(k string, d bool) bool {
	v := strings.TrimSpace(strings.ToLower(os.Getenv(k)))
	if v == "" {
		return d
	}
	return v == "1" || v == "true" || v == "yes" || v == "on"
}
func envInt(k string, d int) int {
	v := os.Getenv(k)
	if v == "" {
		return d
	}
	n, e := strconv.Atoi(v)
	if e != nil || n <= 0 {
		return d
	}
	return n
}

type Server struct {
	cfg            Config
	log            *slog.Logger
	registry       *protocol.Registry
	store          *store.Store
	resources      *cfgstore.Store
	domain         *game.Engine
	hub            *sessionHub
	battleMu       sync.Mutex
	gachaMu        sync.Mutex
	connMu         sync.Mutex
	conns          map[net.Conn]struct{}
	connWG         sync.WaitGroup
	connectionSN   atomic.Int64
	captureWork    chan struct{}
	chatMu         sync.Mutex
	chatLastAt     map[int64]time.Time
	actionSN       atomic.Int64
	botAutoFill    atomic.Bool
	packetLog      atomic.Bool
	protocolTrace  atomic.Bool
	traceFull      atomic.Bool
	protocolEvents packetTraceBuffer
	captures       *traceevent.CaptureStore
	gameplay       *cfgstore.GameplayStore
	startedAt      time.Time
	stopCh         chan struct{}
	stopOnce       sync.Once
}

func (s *Server) nextActionSN() int64 {
	return time.Now().UnixNano() + s.actionSN.Add(1)
}

func New(cfg Config, logger *slog.Logger) (*Server, error) {
	if logger == nil {
		logger = slog.New(slog.NewTextHandler(os.Stdout, &slog.HandlerOptions{Level: slog.LevelInfo}))
	}
	reg, err := protocol.NewRegistry()
	if err != nil {
		return nil, err
	}
	db, err := store.Open(cfg.DBPath)
	if err != nil {
		return nil, err
	}
	res, err := cfgstore.Load(cfg.ResourceDir)
	if err != nil {
		db.Close()
		return nil, err
	}
	gameplayPath := filepath.Join(filepath.Dir(cfg.DBPath), "gameplay.json")
	gameplay := cfgstore.NewGameplayStore(gameplayPath, logger)
	srv := &Server{cfg: cfg, log: logger, registry: reg, store: db, resources: res, domain: game.New(db, res), hub: newSessionHub(), conns: make(map[net.Conn]struct{}), chatLastAt: make(map[int64]time.Time), captures: cfg.Captures, captureWork: make(chan struct{}, 2), gameplay: gameplay, startedAt: time.Now(), stopCh: make(chan struct{})}
	srv.botAutoFill.Store(cfg.BotAutoFill)
	srv.packetLog.Store(cfg.LogPackets)
	if value, ok, e := db.GetSetting(context.Background(), "bot_auto_fill"); e != nil {
		db.Close()
		return nil, e
	} else if ok {
		srv.botAutoFill.Store(value == "true")
	}
	srv.protocolTrace.Store(cfg.ProtocolTrace)
	if value, ok, e := db.GetSetting(context.Background(), "protocol_trace"); e != nil {
		db.Close()
		return nil, e
	} else if ok {
		srv.protocolTrace.Store(value == "true")
	}
	srv.traceFull.Store(cfg.TraceFull)
	if value, ok, e := db.GetSetting(context.Background(), "protocol_trace_full"); e != nil {
		db.Close()
		return nil, e
	} else if ok {
		srv.traceFull.Store(value == "true")
	}
	return srv, nil
}
func (s *Server) Close() error {
	s.connMu.Lock()
	for c := range s.conns {
		_ = c.Close()
	}
	s.connMu.Unlock()
	if s.captures != nil {
		_ = s.captures.Close()
	}
	return s.store.Close()
}

// Storage exposes the server-owned repository for startup wiring of HTTP
// services such as self-hosted account authentication.
func (s *Server) Storage() *store.Store { return s.store }

// ConfigTableCount reports how many config tables were loaded from resources.
func (s *Server) ConfigTableCount() int { return s.resources.TableCount() }

// Stopped exposes the console-triggered shutdown signal channel.
func (s *Server) Stopped() <-chan struct{} { return s.stopCh }

func (s *Server) BotAutoFill() bool { return s.botAutoFill.Load() }

func (s *Server) SetBotAutoFill(enabled bool) { s.botAutoFill.Store(enabled) }

func (s *Server) PacketLog() bool { return s.packetLog.Load() }

func (s *Server) SetPacketLog(enabled bool) { s.packetLog.Store(enabled) }

func (s *Server) ProtocolTrace() bool { return s.protocolTrace.Load() }

func (s *Server) SetProtocolTrace(enabled bool) { s.protocolTrace.Store(enabled) }

func (s *Server) ProtocolTraceFull() bool { return s.traceFull.Load() }

func (s *Server) SetProtocolTraceFull(enabled bool) { s.traceFull.Store(enabled) }

func (s *Server) CaptureStore() *traceevent.CaptureStore { return s.captures }

func (s *Server) activeCaptureID() (string, bool) {
	if s.captures == nil {
		return "", false
	}
	metadata, ok := s.captures.ActiveSession()
	return metadata.ID, ok
}

func (s *Server) tryCaptureWork() bool {
	select {
	case s.captureWork <- struct{}{}:
		return true
	default:
		return false
	}
}

func (s *Server) releaseCaptureWork() {
	<-s.captureWork
}

func (s *Server) recordRawFrame(sess *Session, producer *traceevent.CaptureProducer, direction string, raw []byte, observedBytes int, detail string) {
	if len(raw) == 0 || producer == nil {
		return
	}
	event := traceevent.Event{
		Timestamp: time.Now().UTC(), Transport: "rpc", Direction: direction,
		ConnectionID: sess.connectionID, RemoteAddr: sess.remoteAddr,
		Message: "raw RPC frame", RawFrameBase64: base64.StdEncoding.EncodeToString(raw),
		PayloadBytes: len(raw), RawFrameBytesObserved: observedBytes, Detail: detail,
	}
	expectedLength := -1
	if len(raw) < protocol.HeaderSize {
		event.RawFrameOmitted = "partial_header"
		event.PayloadBytes = 0
	}
	var route protocol.Command
	var routeFound bool
	if len(raw) >= protocol.HeaderSize {
		event.GameSessionID = int64(binary.BigEndian.Uint64(raw[4:12]))
		event.CommandID = binary.BigEndian.Uint16(raw[12:14])
		event.UPSN = int64(binary.BigEndian.Uint64(raw[17:25]))
		event.DOWNSN = int64(binary.BigEndian.Uint64(raw[25:33]))
		declared := int64(int32(binary.BigEndian.Uint32(raw[:4])))
		if declared < 0 || declared > int64(protocol.MaxPayload) {
			event.RawFrameOmitted = "invalid_declared_payload_length"
			event.PayloadBytes = len(raw) - protocol.HeaderSize
		} else {
			event.PayloadBytes = int(declared)
			expectedLength = protocol.HeaderSize + int(declared)
		}
		if route, routeFound = s.registry.Lookup(event.CommandID); routeFound {
			event.Message = route.MessageName
		} else {
			event.Message = "unknown CMDID"
		}
		if direction == "response" && event.UPSN == 0 {
			event.Direction = "push"
		}
	}
	if expectedLength >= 0 && len(raw) < expectedLength && event.RawFrameOmitted == "" {
		event.RawFrameOmitted = "partial_payload"
	}
	if detail != "" && strings.HasPrefix(detail, "socket_write_failed") {
		event.RawFrameOmitted = "partial_socket_write"
	}
	if expectedLength >= 0 && len(raw) > expectedLength && event.RawFrameOmitted == "" {
		event.RawFrameOmitted = fmt.Sprintf("unexpected_trailing_bytes_%d", len(raw)-expectedLength)
	}
	if routeFound && expectedLength == len(raw) {
		message, err := s.registry.NewMessage(route.Message)
		if err != nil {
			event.PayloadOmitted = "protobuf_message_unavailable"
		} else if err = proto.Unmarshal(raw[protocol.HeaderSize:], message); err != nil {
			event.PayloadOmitted = "protobuf_decode_failed"
		} else {
			event.Payload, event.RedactedFields, event.PayloadOmitted = tracePayloadSnapshot(message, true)
		}
	} else if event.RawFrameOmitted != "" && event.PayloadOmitted == "" {
		event.PayloadOmitted = "raw_frame_incomplete"
	}
	producer.Append(event)
}

func (s *Server) captureRawFrame(sess *Session, producer *traceevent.CaptureProducer, direction string, raw []byte, observedBytes int, detail string) {
	if producer == nil {
		return
	}
	defer producer.Close()
	if len(raw) == 0 {
		return
	}
	if !s.tryCaptureWork() {
		producer.MarkIncomplete("producer_limit_reached")
		return
	}
	defer s.releaseCaptureWork()
	s.recordRawFrame(sess, producer, direction, raw, observedBytes, detail)
}

func (s *Server) ListenAndServe(ctx context.Context) error {
	ln, err := net.Listen("tcp", s.cfg.ListenAddr)
	if err != nil {
		return err
	}
	s.gameplay.StartPolling(ctx, time.Second)
	handbookPath := HandbookPath(s.cfg.DBPath)
	if err := s.GenerateHandbook(handbookPath); err != nil {
		s.log.Warn("handbook generation failed", "path", handbookPath, "err", err)
	}
	go func() {
		<-ctx.Done()
		_ = ln.Close()
		s.connMu.Lock()
		for c := range s.conns {
			_ = c.Close()
		}
		s.connMu.Unlock()
	}()
	for {
		c, e := ln.Accept()
		if e != nil {
			if ctx.Err() != nil || errors.Is(e, net.ErrClosed) {
				s.connWG.Wait()
				return nil
			}
			s.log.Error("accept failed", "err", e)
			continue
		}
		s.connMu.Lock()
		s.conns[c] = struct{}{}
		s.connMu.Unlock()
		s.connWG.Add(1)
		go func() {
			defer s.connWG.Done()
			defer func() { s.connMu.Lock(); delete(s.conns, c); s.connMu.Unlock() }()
			s.serveConn(ctx, c)
		}()
	}
}

type Session struct {
	conn                               net.Conn
	server                             *Server
	connectionID                       string
	remoteAddr                         string
	writeMu                            sync.Mutex
	outMu                              sync.Mutex
	downSN                             int64
	mu                                 sync.RWMutex
	out                                chan []byte
	closeOnce                          sync.Once
	closed                             chan struct{}
	sessionID                          int64
	playerID                           int64
	accountID                          int64
	nick                               string
	loggedIn                           bool
	initialOnlineSyncAttemptsRemaining uint8
	initialOnlineSyncAcknowledged      bool
	initialOnlineSyncWindowEndLogged   bool
	initialRoomID                      int64
	responses                          map[int64][]byte
	responseOrder                      []int64
}

// A newly authenticated client can send its first heartbeat in the same Unity
// update that handles ConnectS2C. The client ignores OnlineSyncRoomIdS2C while
// it is still in Login, so retry startup room state until the client requests
// the corresponding SyncRoom snapshot or the bounded retry window expires.
const initialOnlineSyncAttempts = 5

func newSession(c net.Conn) *Session {
	return &Session{conn: c, responses: make(map[int64][]byte), out: make(chan []byte, 512), closed: make(chan struct{})}
}

// runWriter is the only goroutine that writes to the socket. Handlers enqueue
// instead of writing directly, so a stalled peer cannot block frame processing
// or another player's session.
func (s *Session) runWriter() {
	for {
		select {
		case <-s.closed:
			return
		case b := <-s.out:
			var producer *traceevent.CaptureProducer
			if s.server != nil {
				producer, _ = s.server.captures.BeginActiveProducer()
			}
			written, err := s.writeRaw(b)
			if producer != nil {
				detail := ""
				if err != nil {
					detail = fmt.Sprintf("socket_write_failed_after_%d_bytes: %v", written, err)
				}
				observed := b
				if written < len(b) {
					observed = b[:written]
				}
				s.server.captureRawFrame(s, producer, "response", observed, written, detail)
			}
			if err != nil {
				s.shutdown()
				return
			}
		}
	}
}
func (s *Session) shutdown() {
	s.closeOnce.Do(func() {
		close(s.closed)
		_ = s.conn.Close()
	})
}

// send blocks until the frame is handed to the writer goroutine or the peer
// stops draining long enough to hit the queue limit. It preserves ordering with
// push frames, unlike a direct socket write.
func (s *Session) send(b []byte) error {
	_, err := s.sendSequenced(b)
	return err
}

// sendSequenced assigns DOWNSN while holding outMu so responses and pushes
// share one per-connection counter in the exact order they enter the writer queue.
func (s *Session) sendSequenced(b []byte) (int64, error) {
	if len(b) == 0 {
		return 0, nil
	}
	s.outMu.Lock()
	defer s.outMu.Unlock()
	framed, downSN, err := s.withDownSN(b)
	if err != nil {
		return 0, err
	}
	select {
	case <-s.closed:
		return 0, net.ErrClosed
	case s.out <- framed:
		s.downSN = downSN
		return downSN, nil
	case <-time.After(10 * time.Second):
		s.shutdown()
		return 0, errors.New("outbound queue saturated")
	}
}

func (s *Session) enqueue(b []byte) {
	_, _ = s.enqueueSequenced(b)
}

func (s *Session) enqueueSequenced(b []byte) (int64, bool) {
	if len(b) == 0 {
		return 0, false
	}
	s.outMu.Lock()
	defer s.outMu.Unlock()
	framed, downSN, err := s.withDownSN(b)
	if err != nil {
		return 0, false
	}
	select {
	case <-s.closed:
		return 0, false
	case s.out <- framed:
		s.downSN = downSN
		return downSN, true
	default:
		// Peer socket is not draining. Drop this push rather than blocking the
		// game loop; the client re-syncs room state on the next request.
		return downSN, false
	}
}

func (s *Session) withDownSN(frame []byte) ([]byte, int64, error) {
	if len(frame) < protocol.HeaderSize {
		return nil, 0, errors.New("outbound frame shorter than protocol header")
	}
	downSN := s.downSN + 1
	framed := append([]byte(nil), frame...)
	binary.BigEndian.PutUint64(framed[25:33], uint64(downSN))
	return framed, downSN, nil
}
func (s *Session) setIdentity(sid, accountID, playerID int64, nick string, roomID int64) {
	s.mu.Lock()
	s.sessionID = sid
	s.accountID = accountID
	s.playerID = playerID
	s.nick = nick
	s.loggedIn = true
	s.initialOnlineSyncAttemptsRemaining = 0
	s.initialOnlineSyncAcknowledged = false
	s.initialOnlineSyncWindowEndLogged = false
	if sid > 0 {
		s.initialOnlineSyncAttemptsRemaining = initialOnlineSyncAttempts
	}
	s.initialRoomID = roomID
	s.mu.Unlock()
}
func (s *Session) identity() (sid, accountID, playerID int64, nick string, logged bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	return s.sessionID, s.accountID, s.playerID, s.nick, s.loggedIn
}

func (s *Session) nextInitialOnlineSync() (playerID int64, ok bool) {
	s.mu.Lock()
	defer s.mu.Unlock()
	if !s.loggedIn || s.initialOnlineSyncAcknowledged || s.initialOnlineSyncAttemptsRemaining == 0 {
		return 0, false
	}
	s.initialOnlineSyncAttemptsRemaining--
	return s.playerID, true
}

func (s *Session) takeInitialOnlineSyncWindowEnd() (playerID, roomID int64, ok bool) {
	s.mu.Lock()
	defer s.mu.Unlock()
	if !s.loggedIn || s.sessionID == 0 || s.initialOnlineSyncAcknowledged ||
		s.initialOnlineSyncAttemptsRemaining > 0 || s.initialOnlineSyncWindowEndLogged {
		return 0, 0, false
	}
	s.initialOnlineSyncWindowEndLogged = true
	return s.playerID, s.initialRoomID, true
}

func (s *Session) updateInitialOnlineRoomID(roomID int64) {
	s.mu.Lock()
	defer s.mu.Unlock()
	if s.loggedIn && !s.initialOnlineSyncAcknowledged {
		s.initialRoomID = roomID
	}
}

func (s *Session) acknowledgeInitialOnlineSync(roomID int64) bool {
	s.mu.Lock()
	defer s.mu.Unlock()
	if !s.loggedIn || s.initialOnlineSyncAcknowledged || roomID != s.initialRoomID {
		return false
	}
	s.initialOnlineSyncAcknowledged = true
	s.initialOnlineSyncAttemptsRemaining = 0
	return true
}

func (s *Session) cacheGet(up int64) ([]byte, bool) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	b, ok := s.responses[up]
	return b, ok
}
func (s *Session) cachePut(up int64, b []byte) {
	if up <= 0 {
		return
	}
	s.mu.Lock()
	defer s.mu.Unlock()
	if _, ok := s.responses[up]; !ok {
		s.responseOrder = append(s.responseOrder, up)
	}
	s.responses[up] = append([]byte(nil), b...)
	if len(s.responseOrder) > 512 {
		old := s.responseOrder[0]
		s.responseOrder = s.responseOrder[1:]
		delete(s.responses, old)
	}
}
func (s *Session) writeRaw(b []byte) (int, error) {
	s.writeMu.Lock()
	defer s.writeMu.Unlock()
	written := 0
	for len(b) > 0 {
		n, e := s.conn.Write(b)
		if n > 0 {
			written += n
			b = b[n:]
		}
		if e != nil {
			return written, e
		}
		if n == 0 {
			return written, io.ErrShortWrite
		}
	}
	return written, nil
}
func (s *Session) writeFrame(f protocol.Frame) error {
	b, e := f.Bytes()
	if e != nil {
		return e
	}
	return s.send(b)
}

func (s *Server) serveConn(ctx context.Context, c net.Conn) {
	defer c.Close()
	sess := newSession(c)
	sess.server = s
	sess.remoteAddr = c.RemoteAddr().String()
	sess.connectionID = fmt.Sprintf("c-%x-%x", time.Now().UnixNano(), s.connectionSN.Add(1))
	go sess.runWriter()
	defer sess.shutdown()
	remote := sess.remoteAddr
	s.log.Info("client connected", "remote", remote)
	s.recordNetworkTrace("connect", "TCP connection accepted", remote, "")
	for {
		_ = c.SetReadDeadline(time.Now().Add(90 * time.Second))
		var producer *traceevent.CaptureProducer
		f, err := protocol.ReadFrameWithRawOnStart(c, func() bool {
			if s.captures == nil {
				return false
			}
			var ok bool
			producer, ok = s.captures.BeginActiveProducer()
			return ok
		})
		readDetail := ""
		if err != nil {
			readDetail = "frame_read_error: " + err.Error()
		}
		if len(f.Raw) > 0 {
			s.captureRawFrame(sess, producer, "request", f.Raw, len(f.Raw), readDetail)
		} else if producer != nil {
			producer.Close()
		}
		if err != nil {
			if errors.Is(err, io.EOF) || errors.Is(err, net.ErrClosed) {
				s.log.Info("client closed TCP connection", "remote", remote)
				s.recordNetworkTrace("disconnect", "client closed TCP connection", remote, "")
			} else {
				s.log.Warn("frame read failed", "remote", remote, "err", err)
				s.recordNetworkTrace("read_error", "TCP frame read failed", remote, err.Error())
			}
			break
		}
		if len(f.Payload) > s.cfg.MaxPayload {
			s.log.Warn("payload exceeds configured limit", "cmd", f.CmdID, "len", len(f.Payload))
			break
		}
		if err = s.processFrame(ctx, sess, f); err != nil {
			s.log.Warn("request processing failed", "remote", remote, "cmd", f.CmdID, "err", err)
		}
	}
	_, _, pid, nick, logged := sess.identity()
	if logged {
		s.hub.unregister(pid, sess)
		s.log.Info("client disconnected", "player_id", pid, "nick", nick)
	}
}

type Push struct {
	CmdID       uint16
	Message     proto.Message
	Targets     []int64
	Exclude     int64
	OnlySession *Session
}
type Result struct {
	Message proto.Message
	Err     int16
	Pushes  []Push
}

func (s *Server) processFrame(ctx context.Context, sess *Session, in protocol.Frame) error {
	if in.UPSN > 0 {
		if cached, ok := sess.cacheGet(in.UPSN); ok {
			if s.ProtocolTrace() {
				_, _, playerID, _, _ := sess.identity()
				s.log.Info("protocol request replayed", "cmd_id", in.CmdID, "upsn", in.UPSN, "player_id", playerID)
			}
			return sess.send(cached)
		}
	}
	route, ok := s.registry.Lookup(in.CmdID)
	if !ok {
		s.recordProtocolTrace("request", in.CmdID, "unknown command", in.UPSN, in.DOWNSN, 0, len(in.Payload), 0, 0, 0, nil)
		s.log.Warn("unknown CMDID", "cmd", in.CmdID, "remote", sess.conn.RemoteAddr().String())
		return nil
	}
	if route.Direction != "c2s" {
		s.recordProtocolTrace("request", in.CmdID, route.MessageName, in.UPSN, in.DOWNSN, 0, len(in.Payload), 0, 0, 0, nil)
		s.log.Warn("client sent non-C2S route", "cmd", in.CmdID, "message", route.MessageName)
		return nil
	}
	if route.Scope == "internal" {
		s.recordProtocolTrace("request", in.CmdID, route.MessageName, in.UPSN, in.DOWNSN, 0, len(in.Payload), 0, 0, 0, nil)
		s.log.Warn("client sent internal CMDID", "cmd", in.CmdID, "message", route.MessageName)
		return nil
	}
	if route.Scope == "test" && !s.cfg.AllowTestRPC {
		s.recordProtocolTrace("request", in.CmdID, route.MessageName, in.UPSN, in.DOWNSN, 0, len(in.Payload), 0, 0, 0, nil)
		s.log.Warn("test rpc disabled", "cmd", in.CmdID)
		return nil
	}
	msg, err := s.registry.NewMessage(route.Message)
	if err != nil {
		return err
	}
	if err = proto.Unmarshal(in.Payload, msg); err != nil {
		s.recordProtocolTrace("request", in.CmdID, route.MessageName, in.UPSN, in.DOWNSN, 0, len(in.Payload), 0, 0, 0, nil)
		s.log.Warn("protobuf decode failed", "cmd", in.CmdID, "type", route.Message, "err", err)
		return nil
	}
	sid, _, pid, _, logged := sess.identity()
	s.recordProtocolTrace("request", in.CmdID, route.MessageName, in.UPSN, in.DOWNSN,
		pid, len(in.Payload), 0, 0, 0, msg)
	if s.packetLog.Load() && route.MessageName != "HeartbeatC2S" {
		s.log.Info(fmt.Sprintf("RECV: %s (%d)", route.MessageName, in.CmdID))
	}
	if s.ProtocolTrace() {
		fields := []any{"cmd_id", in.CmdID, "message", route.MessageName,
			"upsn", in.UPSN, "down_sn", in.DOWNSN, "payload_bytes", len(in.Payload), "player_id", pid}
		fields = append(fields, traceRequestFields(msg)...)
		fields = append(fields, tracePayloadFields(msg, s.ProtocolTraceFull())...)
		s.log.Info("protocol request", fields...)
	}
	if route.MessageName != "ConnectC2S" && route.MessageName != "ConnectC2S2" {
		if !logged {
			return s.sendErrorResponse(sess, in, route, ErrAuth)
		}
		if in.SessionID != sid {
			return s.sendErrorResponse(sess, in, route, ErrAuth)
		}
	}
	res, err := s.dispatchGuarded(ctx, sess, in, route, msg)
	if err != nil {
		s.log.Warn("handler failed", "cmd", in.CmdID, "type", route.MessageName, "player", pid, "err", err)
		if res.Message == nil {
			res.Err = ErrNotOpen
		}
	}
	if err == nil && res.Err == 0 && requestMayAdvanceBattle(msg) {
		if _, playerID, ok := sessionPlayer(sess); ok {
			if roomID, roomErr := s.store.RoomForPlayer(ctx, playerID); roomErr == nil {
				res.Pushes = append(res.Pushes, s.resolveBotBattleActions(ctx, roomID)...)
			}
		}
	}
	responseRoute, has := s.registry.ResponseFor(in.CmdID)
	if !has {
		return nil
	}
	if res.Message == nil {
		res.Message, err = s.registry.NewMessage(responseRoute.Message)
		if err != nil {
			return err
		}
	}
	setCodeField(res.Message, int32(res.Err))
	payload, err := proto.Marshal(res.Message)
	if err != nil {
		return err
	}
	newSID, _, _, _, _ := sess.identity()
	if newSID == 0 {
		if connected, ok := res.Message.(*protocolpb.ConnectS2C); ok && res.Err == ErrSucc && connected.GetSessionId() > 0 {
			newSID = connected.GetSessionId()
		} else {
			newSID = in.SessionID
		}
	}
	out := protocol.Frame{SessionID: newSID, CmdID: uint16(responseRoute.CmdID), Version: in.Version, UPSN: in.UPSN, DOWNSN: 0, Err: res.Err, Payload: payload}
	encoded, err := out.Bytes()
	if err != nil {
		return err
	}
	downSN, err := sess.sendSequenced(encoded)
	if err != nil {
		return err
	}
	binary.BigEndian.PutUint64(encoded[25:33], uint64(downSN))
	if connected, ok := res.Message.(*protocolpb.ConnectS2C); ok && res.Err == ErrSucc {
		account := connected.GetAccount()
		player := connected.GetPlayer()
		if account == nil || player == nil {
			return errors.New("successful ConnectS2C missing account or player after validation")
		}
		sess.setIdentity(connected.GetSessionId(), int64(account.GetAccountId()), player.GetId(), player.GetNick(), player.GetRoomId())
		s.hub.register(player.GetId(), sess)
		s.log.Info("login accepted", "auth_method", account.GetPlat(), "account_id", account.GetAccountId(), "player_id", player.GetId(), "nick", player.GetNick(), "session_id", connected.GetSessionId())
	}
	if in.UPSN > 0 {
		sess.cachePut(in.UPSN, encoded)
	}
	_, _, responsePlayerID, _, _ := sess.identity()
	if responsePlayerID == 0 {
		responsePlayerID = pid
	}
	s.recordProtocolTrace("response", uint16(responseRoute.CmdID), responseRoute.MessageName,
		in.UPSN, downSN, responsePlayerID, len(payload), res.Err, 0, 0, res.Message)
	if s.packetLog.Load() && responseRoute.MessageName != "HeartbeatS2C" {
		if res.Err != 0 {
			s.log.Info(fmt.Sprintf("SEND: %s (%d) err=%d", responseRoute.MessageName, responseRoute.CmdID, res.Err))
		} else {
			s.log.Info(fmt.Sprintf("SEND: %s (%d)", responseRoute.MessageName, responseRoute.CmdID))
		}
	}
	if s.ProtocolTrace() {
		fields := []any{"cmd_id", responseRoute.CmdID, "message", responseRoute.MessageName,
			"upsn", in.UPSN, "down_sn", downSN, "error_code", res.Err, "payload_bytes", len(payload), "player_id", responsePlayerID}
		fields = append(fields, tracePayloadFields(res.Message, s.ProtocolTraceFull())...)
		s.log.Info("protocol response", fields...)
	}
	for _, p := range res.Pushes {
		s.broadcastPush(p, in.Version)
	}
	return nil
}

func (s *Server) sendErrorResponse(sess *Session, in protocol.Frame, route protocol.Command, code int16) error {
	r, ok := s.registry.ResponseFor(in.CmdID)
	if !ok {
		return nil
	}
	m, err := s.registry.NewMessage(r.Message)
	if err != nil {
		return err
	}
	setCodeField(m, int32(code))
	b, err := proto.Marshal(m)
	if err != nil {
		return err
	}
	sid, _, _, _, _ := sess.identity()
	if sid == 0 {
		sid = in.SessionID
	}
	out := protocol.Frame{SessionID: sid, CmdID: uint16(r.CmdID), Version: in.Version, UPSN: in.UPSN, Err: code, Payload: b}
	raw, err := out.Bytes()
	if err != nil {
		return err
	}
	downSN, err := sess.sendSequenced(raw)
	if err != nil {
		return err
	}
	binary.BigEndian.PutUint64(raw[25:33], uint64(downSN))
	if in.UPSN > 0 {
		sess.cachePut(in.UPSN, raw)
	}
	_, _, playerID, _, _ := sess.identity()
	s.recordProtocolTrace("response", uint16(r.CmdID), r.MessageName, in.UPSN, downSN,
		playerID, len(b), code, 0, 0, m)
	return nil
}

func (s *Server) broadcastPush(p Push, version [3]byte) {
	if p.OnlySession != nil {
		_, _, playerID, _, ok := p.OnlySession.identity()
		if !ok || playerID == p.Exclude {
			return
		}
		for _, target := range p.Targets {
			if target == playerID {
				s.sendPush(p.OnlySession, p.CmdID, p.Message, version)
				break
			}
		}
		return
	}
	for _, pid := range p.Targets {
		for _, sess := range s.hub.sessions(pid) {
			_, _, spid, _, ok := sess.identity()
			if !ok || spid == p.Exclude {
				continue
			}
			s.sendPush(sess, p.CmdID, p.Message, version)
		}
	}
}
func (s *Server) PushConsoleInventory(playerID int64, counts map[int32]int32) bool {
	if playerID <= 0 || len(counts) == 0 {
		return false
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: pveInventoryMessages(counts), IsNotShow: true,
	}, []int64{playerID}, 0)
	s.broadcastPush(push, [3]byte{1, 0, 0})
	return len(s.hub.sessions(playerID)) > 0
}

func (s *Server) PlayerOnline(playerID int64) bool {
	return len(s.hub.sessions(playerID)) > 0
}

// SendAdminMail delivers one admin-authored mail to a player.
func (s *Server) SendAdminMail(ctx context.Context, playerID int64, title, body, sendName string, rewards map[int32]int32) (int32, error) {
	return s.MailImplSend(ctx, playerID, title, body, sendName, rewards)
}

// SendAdminMailAll delivers one admin-authored mail to every player.
func (s *Server) SendAdminMailAll(ctx context.Context, title, body, sendName string, rewards map[int32]int32) (int, error) {
	return s.MailImplSendAll(ctx, title, body, sendName, rewards)
}

func (s *Server) sendPush(sess *Session, cmd uint16, msg proto.Message, version [3]byte) {
	b, err := proto.Marshal(msg)
	if err != nil {
		s.log.Warn("push marshal failed", "cmd", cmd, "err", err)
		return
	}
	sid, _, _, _, _ := sess.identity()
	f, err := (protocol.Frame{SessionID: sid, CmdID: cmd, Version: version, UPSN: 0, DOWNSN: 0, Err: 0, Payload: b}).Bytes()
	if err != nil {
		s.log.Warn("push encode failed", "cmd", cmd, "err", err)
		return
	}
	downSN, queued := sess.enqueueSequenced(f)
	if queued {
		_, _, playerID, _, _ := sess.identity()
		s.recordProtocolTrace("push", cmd, string(proto.MessageName(msg)), 0, downSN, playerID,
			len(b), 0, 1, 0, msg)
		if onlineSync, ok := msg.(*protocolpb.OnlineSyncRoomIdS2C); ok {
			s.log.Info("online room state push queued", "player_id", playerID, "room_id", onlineSync.GetRoomId())
		}
		if s.ProtocolTrace() {
			fields := []any{"cmd_id", cmd, "message", string(proto.MessageName(msg)), "upsn", 0,
				"down_sn", downSN, "error_code", 0, "payload_bytes", len(b), "player_id", playerID}
			fields = append(fields, tracePayloadFields(msg, s.ProtocolTraceFull())...)
			if actions, ok := msg.(*protocolpb.PredictActionS2C); ok {
				actionIDs := make([]int32, 0, len(actions.GetActions()))
				actionPlayers := make([]int64, 0, len(actions.GetActions()))
				for _, action := range actions.GetActions() {
					if action == nil {
						continue
					}
					actionIDs = append(actionIDs, action.GetId())
					actionPlayers = append(actionPlayers, action.GetPlayerId())
				}
				fields = append(fields, "room_round", actions.GetRoomRound(), "action_ids", actionIDs, "action_player_ids", actionPlayers)
			}
			s.log.Info("protocol push", fields...)
		}
	} else if onlineSync, ok := msg.(*protocolpb.OnlineSyncRoomIdS2C); ok {
		_, _, playerID, _, _ := sess.identity()
		reason := "outbound_queue_full"
		select {
		case <-sess.closed:
			reason = "session_closed"
		default:
		}
		s.log.Warn("online room state push dropped", "player_id", playerID,
			"room_id", onlineSync.GetRoomId(), "reason", reason)
	}
}

// traceRequestFields adds only selected, non-sensitive gameplay fields to the
// opt-in protocol trace. Authentication tokens and arbitrary protobuf payloads
// are never serialized into the log.
func traceRequestFields(msg proto.Message) []any {
	switch q := msg.(type) {
	case *protocolpb.MoveC2S:
		return []any{"direction", q.GetDirection(), "force_dir", q.GetForceDir()}
	case *protocolpb.ThrowDiceResultC2S:
		var actionSN int64
		if q.GetInfo() != nil {
			actionSN = q.GetInfo().GetSn()
		}
		return []any{"action_sn", actionSN, "point", q.GetPoint()}
	case *protocolpb.StopOrContinueC2S:
		return []any{"stop", q.GetStop()}
	default:
		return nil
	}
}
