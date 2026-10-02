package config

import "strings"

// IsExampleSecret rejects secrets that are visibly copied from the checked-in
// configuration template. It deliberately does not inspect domain suffixes,
// since secrets and SMTP credentials may legitimately contain those strings.
func IsExampleSecret(value string) bool {
	value = strings.ToLower(strings.TrimSpace(value))
	if value == "" {
		return false
	}
	switch value {
	case "replace-me", "changeme", "change-me", "your-secret", "your-secret-here":
		return true
	}
	return strings.HasPrefix(value, "replace-with-")
}

// IsExampleEndpoint rejects example-only hosts and sender addresses from the
// checked-in configuration template.
func IsExampleEndpoint(value string) bool {
	value = strings.ToLower(strings.TrimSpace(value))
	if value == "" {
		return false
	}
	return value == "smtp.example.com" || value == "game@example.com" ||
		strings.HasSuffix(value, "@example.com") || strings.HasSuffix(value, ".example.com")
}
