package gateway

import (
	"encoding/json"
	"strings"
	"sync"
	"time"

	traceevent "astralparty-server/internal/trace"
	"google.golang.org/protobuf/encoding/protojson"
	"google.golang.org/protobuf/proto"
	"google.golang.org/protobuf/reflect/protoreflect"
)

const maxTracePayloadJSON = 64 * 1024
const maxProtocolTraceEvents = 500

type packetTraceBuffer struct {
	mu     sync.RWMutex
	events [maxProtocolTraceEvents]traceevent.Event
	next   int
	count  int
}

func (b *packetTraceBuffer) add(event traceevent.Event) {
	b.mu.Lock()
	b.events[b.next] = event
	b.next = (b.next + 1) % len(b.events)
	if b.count < len(b.events) {
		b.count++
	}
	b.mu.Unlock()
}

func (b *packetTraceBuffer) snapshot(limit int) []traceevent.Event {
	b.mu.RLock()
	defer b.mu.RUnlock()
	if limit < 1 || limit > len(b.events) {
		limit = len(b.events)
	}
	if limit > b.count {
		limit = b.count
	}
	start := (b.next - limit + len(b.events)) % len(b.events)
	items := make([]traceevent.Event, 0, limit)
	for i := 0; i < limit; i++ {
		items = append(items, b.events[(start+i)%len(b.events)])
	}
	return items
}

// ProtocolEvents returns the most recent RPC, HTTP, and TCP lifecycle events.
func (s *Server) ProtocolEvents(limit int) []traceevent.Event {
	return s.protocolEvents.snapshot(limit)
}

// RecordHTTPTrace adds an SDK HTTP request/response to the same short-lived
// admin trace used for decoded game RPCs.
func (s *Server) RecordHTTPTrace(event traceevent.Event) {
	if !s.ProtocolTrace() {
		return
	}
	event.Transport = "http"
	if event.Timestamp.IsZero() {
		event.Timestamp = time.Now().UTC()
	}
	fields := []any{"transport", "http", "message", event.Message, "http_status", event.HTTPStatus,
		"payload_bytes", event.PayloadBytes, "direction", event.Direction}
	if len(event.RedactedFields) > 0 {
		fields = append(fields, "redacted_fields", event.RedactedFields)
	}
	if event.PayloadOmitted != "" {
		fields = append(fields, "payload_json_omitted", event.PayloadOmitted)
	} else if len(event.Payload) > 0 {
		fields = append(fields, "payload_json", event.Payload)
	}
	s.log.Info("protocol HTTP "+event.Direction, fields...)
	s.protocolEvents.add(event)
}

// recordNetworkTrace keeps TCP lifecycle events visible in the admin console
// regardless of the optional decoded-payload trace setting.
func (s *Server) recordNetworkTrace(direction, message, remote, detail string) {
	s.protocolEvents.add(traceevent.Event{
		Timestamp: time.Now().UTC(), Transport: "tcp", Direction: direction,
		Message: message, RemoteAddr: remote, Detail: detail,
	})
}

func (s *Server) recordProtocolTrace(direction string, commandID uint16, message string,
	upsn int64, downSN int64, playerID int64, payloadBytes int, errorCode int16, targetCount int,
	excludePlayerID int64, payload proto.Message) {
	var body json.RawMessage
	var redacted []string
	var omitted string
	if s.ProtocolTrace() {
		body, redacted, omitted = tracePayloadSnapshot(payload, s.ProtocolTraceFull())
	} else {
		omitted = "payload_trace_disabled"
	}
	if payload == nil && payloadBytes > 0 {
		omitted = "no_decoded_payload"
	}
	s.protocolEvents.add(traceevent.Event{
		Timestamp: time.Now().UTC(), Transport: "rpc", Direction: direction, CommandID: commandID, Message: message,
		UPSN: upsn, DOWNSN: downSN, PlayerID: playerID, PayloadBytes: payloadBytes, ErrorCode: errorCode,
		TargetCount: targetCount, ExcludePlayerID: excludePlayerID,
		RedactedFields: redacted, Payload: body, PayloadOmitted: omitted,
	})
}

// tracePayloadFields records decoded Proto bodies for opt-in packet analysis.
// Its field redaction follows the runtime trace setting; large messages keep
// their byte count without dumping an unbounded log record.
func tracePayloadFields(message proto.Message, includeSensitive bool) []any {
	if message == nil {
		return nil
	}
	payload, redacted, omitted := tracePayloadSnapshot(message, includeSensitive)
	fields := []any{"payload_redacted_fields", redacted}
	if omitted != "" {
		return append(fields, "payload_json_omitted", omitted)
	}
	return append(fields, "payload_json", payload)
}

func tracePayloadSnapshot(message proto.Message, includeSensitive bool) (json.RawMessage, []string, string) {
	if message == nil {
		return nil, nil, ""
	}
	copy := proto.Clone(message)
	redacted := make([]string, 0, 4)
	if !includeSensitive {
		redactTraceMessage(copy.ProtoReflect(), "", &redacted)
	}

	payload, err := (protojson.MarshalOptions{UseProtoNames: true}).Marshal(copy)
	if err != nil {
		return nil, redacted, "marshal_failed"
	}
	if len(payload) > maxTracePayloadJSON {
		return nil, redacted, "over_64KiB"
	}
	return json.RawMessage(payload), redacted, ""
}

func redactTraceMessage(message protoreflect.Message, prefix string, redacted *[]string) {
	fields := message.Descriptor().Fields()
	for i := 0; i < fields.Len(); i++ {
		field := fields.Get(i)
		name := string(field.Name())
		path := name
		if prefix != "" {
			path = prefix + "." + name
		}
		if traceevent.IsSensitiveField(name) && !keepStructuredBattleChat(message, field) {
			if message.Has(field) {
				message.Clear(field)
				*redacted = append(*redacted, path)
			}
			continue
		}
		if !message.Has(field) || field.Kind() != protoreflect.MessageKind && field.Kind() != protoreflect.GroupKind {
			continue
		}
		switch {
		case field.IsList():
			list := message.Mutable(field).List()
			for index := 0; index < list.Len(); index++ {
				redactTraceMessage(list.Get(index).Message(), path, redacted)
			}
		case field.IsMap():
			if field.MapValue().Kind() != protoreflect.MessageKind && field.MapValue().Kind() != protoreflect.GroupKind {
				continue
			}
			entries := message.Mutable(field).Map()
			entries.Range(func(key protoreflect.MapKey, value protoreflect.Value) bool {
				redactTraceMessage(value.Message(), path, redacted)
				return true
			})
		default:
			redactTraceMessage(message.Mutable(field).Message(), path, redacted)
		}
	}
}

// keepStructuredBattleChat retains only numeric battle-expression messages so
// administrators can compare client packets with decompiled message factories.
// Private, arbitrary, and malformed text-chat payloads stay redacted.
func keepStructuredBattleChat(message protoreflect.Message, field protoreflect.FieldDescriptor) bool {
	if field.Name() != "msg" || field.Kind() != protoreflect.StringKind {
		return false
	}
	switch message.Descriptor().FullName() {
	case "protocol.ChatMapMarkersC2S", "protocol.ChatMapMarkersS2C", "protocol.PlayerChatC2S", "protocol.PlayerChatS2C":
		return structuredBattleChatText(message.Get(field).String())
	default:
		return false
	}
}

func structuredBattleChatText(value string) bool {
	parts := strings.Split(value, ",")
	if len(parts) < 2 {
		return false
	}
	typeID, ok := parseChatInt(parts[0])
	if !ok || typeID < 3 || typeID > 10 {
		return false
	}
	wantParts := 2
	switch typeID {
	case 3, 10, 6:
		wantParts = 3
		if typeID == 6 {
			wantParts = 4
		}
	}
	if len(parts) != wantParts {
		return false
	}
	for i, part := range parts[1:] {
		if typeID == 6 && i == 2 {
			if part == "" {
				continue
			}
			for _, cardID := range strings.Split(part, "|") {
				if _, ok := parseChatInt(cardID); !ok {
					return false
				}
			}
			continue
		}
		if _, ok := parseChatInt(part); !ok {
			return false
		}
	}
	return true
}
