package trace

import (
	"encoding/json"
	"time"
)

// Event is one short-lived RPC, HTTP, or network diagnostic. RPC and HTTP
// payloads are decoded JSON and follow the configured trace redaction mode.
type Event struct {
	Timestamp             time.Time           `json:"timestamp"`
	Transport             string              `json:"transport"`
	Direction             string              `json:"direction"`
	ConnectionID          string              `json:"connectionId,omitempty"`
	RemoteAddr            string              `json:"remoteAddr,omitempty"`
	Detail                string              `json:"detail,omitempty"`
	CommandID             uint16              `json:"cmdId"`
	Message               string              `json:"message"`
	GameSessionID         int64               `json:"gameSessionId,omitempty"`
	HTTPStatus            int                 `json:"httpStatus,omitempty"`
	HTTPMethod            string              `json:"httpMethod,omitempty"`
	RequestTarget         string              `json:"requestTarget,omitempty"`
	RequestHeaders        map[string][]string `json:"requestHeaders,omitempty"`
	ResponseHeaders       map[string][]string `json:"responseHeaders,omitempty"`
	UPSN                  int64               `json:"upsn"`
	DOWNSN                int64               `json:"downSn,omitempty"`
	PlayerID              int64               `json:"playerId,omitempty"`
	PayloadBytes          int                 `json:"payloadBytes"`
	ErrorCode             int16               `json:"errorCode,omitempty"`
	TargetCount           int                 `json:"targetCount,omitempty"`
	ExcludePlayerID       int64               `json:"excludePlayerId,omitempty"`
	RedactedFields        []string            `json:"redactedFields,omitempty"`
	Payload               json.RawMessage     `json:"payload,omitempty"`
	PayloadOmitted        string              `json:"payloadOmitted,omitempty"`
	RawFrameBase64        string              `json:"rawFrameBase64,omitempty"`
	RawFrameOmitted       string              `json:"rawFrameOmitted,omitempty"`
	RawFrameBytesObserved int                 `json:"rawFrameBytesObserved,omitempty"`
	RawBodyBase64         string              `json:"rawBodyBase64,omitempty"`
	RawBodyOmitted        string              `json:"rawBodyOmitted,omitempty"`
}
