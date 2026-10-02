package httpsecurity

import (
	"fmt"
	"net"
	"net/http"
	"strings"
)

// ParseTrustedProxyCIDRs accepts a comma-separated list of proxy IP addresses
// or CIDRs. Empty entries are ignored so an unset optional value is valid.
func ParseTrustedProxyCIDRs(raw string) ([]*net.IPNet, error) {
	networks := make([]*net.IPNet, 0)
	for _, entry := range strings.Split(raw, ",") {
		entry = strings.TrimSpace(entry)
		if entry == "" {
			continue
		}
		if ip := net.ParseIP(entry); ip != nil {
			if v4 := ip.To4(); v4 != nil {
				mask := net.CIDRMask(32, 32)
				networks = append(networks, &net.IPNet{IP: v4.Mask(mask), Mask: mask})
			} else {
				mask := net.CIDRMask(128, 128)
				networks = append(networks, &net.IPNet{IP: ip.Mask(mask), Mask: mask})
			}
			continue
		}
		_, network, err := net.ParseCIDR(entry)
		if err != nil {
			return nil, fmt.Errorf("invalid address or CIDR %q", entry)
		}
		networks = append(networks, network)
	}
	return networks, nil
}

// IsHTTPSRequest trusts X-Forwarded-Proto only when the direct peer belongs to
// the operator-configured proxy list. A client-supplied header is never proof
// of TLS by itself.
func IsHTTPSRequest(r *http.Request, behindTLSProxy bool, trustedProxies []*net.IPNet) bool {
	if r == nil {
		return false
	}
	if r.TLS != nil {
		return true
	}
	if !behindTLSProxy || !isTrustedProxy(r.RemoteAddr, trustedProxies) {
		return false
	}
	values := r.Header.Values("X-Forwarded-Proto")
	return len(values) == 1 && strings.EqualFold(strings.TrimSpace(values[0]), "https")
}

// AllowsSensitiveRequest keeps direct HTTP as an explicit operator choice;
// callers can separately report IsHTTPSRequest so plaintext is not described
// as encrypted.
func AllowsSensitiveRequest(r *http.Request, allowHTTP, behindTLSProxy bool, trustedProxies []*net.IPNet) bool {
	return allowHTTP || IsHTTPSRequest(r, behindTLSProxy, trustedProxies)
}

func isTrustedProxy(remoteAddr string, trustedProxies []*net.IPNet) bool {
	host, _, err := net.SplitHostPort(remoteAddr)
	if err != nil {
		host = remoteAddr
	}
	if zone := strings.LastIndexByte(host, '%'); zone >= 0 {
		host = host[:zone]
	}
	ip := net.ParseIP(strings.Trim(host, "[]"))
	if ip == nil {
		return false
	}
	for _, network := range trustedProxies {
		if network != nil && network.Contains(ip) {
			return true
		}
	}
	return false
}
