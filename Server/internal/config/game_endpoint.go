package config

import (
	"errors"
	"net"
	"net/url"
	"strconv"
	"strings"
)

// GameEndpoint is the host and explicit TCP port advertised in bootstrap
// serverUrl. The client ignores the URI scheme and opens a TCP connection.
type GameEndpoint struct {
	Host string
	Port int
}

// ParseGameEndpoint accepts host:port, tcp://host:port, or an HTTP(S) URL
// containing only a host and port. It deliberately does not resolve DNS; the
// Android preflight performs the reachability check from the player's device.
func ParseGameEndpoint(value string) (GameEndpoint, error) {
	value = strings.TrimSpace(value)
	if value == "" || len(value) > 1024 || strings.ContainsAny(value, "\r\n\x00") {
		return GameEndpoint{}, errors.New("game server address must contain a host and explicit TCP port")
	}
	candidate := value
	if !strings.Contains(candidate, "://") {
		candidate = "tcp://" + candidate
	}
	parsed, err := url.Parse(candidate)
	scheme := ""
	if parsed != nil {
		scheme = strings.ToLower(parsed.Scheme)
	}
	if err != nil || (scheme != "tcp" && scheme != "http" && scheme != "https") {
		return GameEndpoint{}, errors.New("game server address must be host:port or tcp/http(s)://host:port")
	}
	if parsed.User != nil || parsed.RawQuery != "" || parsed.ForceQuery || parsed.Fragment != "" ||
		strings.Contains(candidate, "#") ||
		(parsed.Path != "" && parsed.Path != "/") {
		return GameEndpoint{}, errors.New("game server address may contain only a host and TCP port")
	}
	host := parsed.Hostname()
	portText := parsed.Port()
	if host == "" || portText == "" {
		return GameEndpoint{}, errors.New("game server address must contain a host and explicit TCP port")
	}
	if net.ParseIP(host) == nil && !validEndpointHostname(host) {
		return GameEndpoint{}, errors.New("game server address contains an invalid host")
	}
	port, err := strconv.Atoi(portText)
	if err != nil || port < 1 || port > 65535 {
		return GameEndpoint{}, errors.New("game server TCP port must be between 1 and 65535")
	}
	return GameEndpoint{Host: host, Port: port}, nil
}

// ParseSelfHostedGameEndpoint additionally rejects the official game domain so
// neither runtime configuration nor bootstrap can silently send the client to
// the vendor's game servers.
func ParseSelfHostedGameEndpoint(value string) (GameEndpoint, error) {
	endpoint, err := ParseGameEndpoint(value)
	if err != nil {
		return GameEndpoint{}, err
	}
	host := strings.TrimSuffix(strings.ToLower(endpoint.Host), ".")
	if host == "feimogames.com" || strings.HasSuffix(host, ".feimogames.com") {
		return GameEndpoint{}, errors.New("official game server destinations are not allowed")
	}
	return endpoint, nil
}

func validEndpointHostname(host string) bool {
	host = strings.TrimSuffix(host, ".")
	if host == "" || len(host) > 253 {
		return false
	}
	for _, label := range strings.Split(host, ".") {
		if len(label) == 0 || len(label) > 63 || label[0] == '-' || label[len(label)-1] == '-' {
			return false
		}
		for _, r := range label {
			if !(r >= 'a' && r <= 'z') && !(r >= 'A' && r <= 'Z') &&
				!(r >= '0' && r <= '9') && r != '-' {
				return false
			}
		}
	}
	return true
}
