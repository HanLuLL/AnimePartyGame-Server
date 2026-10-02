package trace

import (
	"bytes"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"strings"
)

const maxJSONPayload = 64 * 1024

// CaptureJSON validates and preserves the JSON value bytes for an explicitly
// enabled full packet trace. Callers must keep that mode access-controlled.
func CaptureJSON(body []byte) (json.RawMessage, []string, string) {
	trimmed := bytes.TrimSpace(body)
	if len(trimmed) > maxJSONPayload {
		return nil, nil, "over_64KiB"
	}
	if !json.Valid(trimmed) {
		return nil, nil, "invalid_json"
	}
	return append(json.RawMessage(nil), trimmed...), nil, ""
}

// RedactJSON keeps the shape of a JSON value while replacing credential and
// contact values. Request bodies also redact a top-level field named "code";
// in responses, "code" is retained because it is the SDK result code.
func RedactJSON(body []byte, request bool) (json.RawMessage, []string, string) {
	if len(body) > maxJSONPayload {
		return nil, nil, "over_64KiB"
	}
	decoder := json.NewDecoder(bytes.NewReader(body))
	decoder.UseNumber()
	var value any
	if err := decoder.Decode(&value); err != nil {
		return nil, nil, "invalid_json"
	}
	var trailing any
	if err := decoder.Decode(&trailing); err == nil {
		return nil, nil, "multiple_json_values"
	} else if !errors.Is(err, io.EOF) {
		return nil, nil, "invalid_json"
	}
	redacted := make([]string, 0, 4)
	value = redactJSONValue(value, "", request, &redacted)
	payload, err := json.Marshal(value)
	if err != nil {
		return nil, redacted, "marshal_failed"
	}
	return json.RawMessage(payload), redacted, ""
}

func redactJSONValue(value any, path string, request bool, redacted *[]string) any {
	switch item := value.(type) {
	case map[string]any:
		for key, child := range item {
			childPath := key
			if path != "" {
				childPath = path + "." + key
			}
			if sensitiveJSONField(key, request) {
				item[key] = "[REDACTED]"
				*redacted = append(*redacted, childPath)
				continue
			}
			item[key] = redactJSONValue(child, childPath, request, redacted)
		}
		return item
	case []any:
		for index, child := range item {
			item[index] = redactJSONValue(child, fmt.Sprintf("%s[%d]", path, index), request, redacted)
		}
		return item
	case string:
		trimmed := strings.TrimSpace(item)
		if strings.HasPrefix(trimmed, "{") || strings.HasPrefix(trimmed, "[") {
			var nested any
			if json.Unmarshal([]byte(trimmed), &nested) == nil {
				return redactJSONValue(nested, path, request, redacted)
			}
		}
	}
	return value
}

func sensitiveJSONField(name string, request bool) bool {
	var normalized strings.Builder
	for _, r := range name {
		if r == '_' || r == '-' || r == '.' {
			continue
		}
		if r >= 'A' && r <= 'Z' {
			r += 'a' - 'A'
		}
		normalized.WriteRune(r)
	}
	field := normalized.String()
	if request && field == "code" {
		return true
	}
	for _, marker := range []string{
		"sid", "token", "password", "passwd", "pwd", "email", "mail", "phone", "mobile",
		"sms", "captcha", "verification", "verifycode", "otp", "secret", "signature", "authorize",
		"device", "cookie", "ticket", "credential", "authorization", "session", "authcode",
	} {
		if strings.Contains(field, marker) {
			return true
		}
	}
	return false
}

// IsSensitiveField is the protobuf field-name policy shared by the RPC and
// HTTP trace redactors.
func IsSensitiveField(name string) bool {
	var normalized strings.Builder
	for _, r := range name {
		if r == '_' || r == '-' {
			continue
		}
		if r >= 'A' && r <= 'Z' {
			r += 'a' - 'A'
		}
		normalized.WriteRune(r)
	}
	switch normalized.String() {
	case "sid", "extra", "pwd", "token", "accesstoken", "refreshtoken", "authorization",
		"email", "emailaddress", "useremail", "mail", "phone", "phonenumber", "mobile",
		"mobilephone", "tel", "password", "passwd", "verificationcode", "verifycode",
		"smscode", "captcha", "otp", "deviceid", "devicefingerprint", "publickey",
		"cipherkey", "accesskey", "secretkey", "signature", "sign", "cookie", "ticket", "secret",
		"msg", "message", "content", "text":
		return true
	default:
		return false
	}
}
