// Package config owns process-level settings only; gameplay configuration is gdconf.
package config

import (
	"os"
	"strconv"
)

type App struct {
	ListenAddr            string
	DBPath                string
	ResourcePath          string
	AllowDevLogin         bool
	AllowTestRPC          bool
	BotAutoFill           bool
	ProtocolTrace         bool
	ProtocolTraceFull     bool
	LogPackets            bool
	MaxPayload            int
	CaptureDir            string
	CaptureMaxBytes       int64
	CaptureTotalMaxBytes  int64
	WebListenAddr         string
	WebUpstreamBase       string
	WebRoute              string
	GameServerURL         string
	HotUpdateURL          string
	HotUpdateRoot         string
	HotAddressJSON        string
	ServerConfigJSON      string
	WebCertFile           string
	WebKeyFile            string
	AuthEmailEnabled      bool
	AuthCodeSecret        string
	AuthSessionHours      int
	BnSDKPaySign          string
	AuthBehindTLSProxy    bool
	AuthTrustedProxyCIDRs string
	AuthAllowHTTP         bool
	SMTPHost              string
	SMTPPort              int
	SMTPUser              string
	SMTPPassword          string
	SMTPFrom              string
	SMTPMode              string
}

func Default() App {
	return App{ListenAddr: ":8800", DBPath: "data/server.db", ResourcePath: "../Resources/Data", AllowDevLogin: false, BotAutoFill: true, ProtocolTrace: true, GameServerURL: "0.0.0.0:8800", HotUpdateURL: "https://0.0.0.0",
		MaxPayload: 16 << 20, CaptureDir: "data/captures", CaptureMaxBytes: 256 << 20, CaptureTotalMaxBytes: 1 << 30, WebListenAddr: ":7878", WebUpstreamBase: "https://0.0.0.0:7878",
		SMTPPort: 587, SMTPMode: "starttls", AuthSessionHours: 24 * 30, AuthTrustedProxyCIDRs: "127.0.0.1/32,::1/128"}
}
func LoadEnv() App {
	c := Default()
	str := func(key string, dst *string) {
		if v := os.Getenv(key); v != "" {
			*dst = v
		}
	}
	str("LISTEN_ADDR", &c.ListenAddr)
	str("DB_PATH", &c.DBPath)
	str("CAPTURE_DIR", &c.CaptureDir)
	str("RESOURCE_DIR", &c.ResourcePath)
	str("WEB_CONFIG_ADDR", &c.WebListenAddr)
	str("UPSTREAM_WEB_BASE", &c.WebUpstreamBase)
	str("UPSTREAM_ROUTE", &c.WebRoute)
	str("GAME_SERVER_URL", &c.GameServerURL)
	str("HOT_UPDATE_URL", &c.HotUpdateURL)
	str("HOTUPDATE_ROOT", &c.HotUpdateRoot)
	str("HOTADDRESS_SERVER_JSON", &c.HotAddressJSON)
	str("HOTADDRESS_EXTEND_JSON", &c.ServerConfigJSON)
	str("WEB_TLS_CERT", &c.WebCertFile)
	str("WEB_TLS_KEY", &c.WebKeyFile)
	str("AUTH_CODE_HMAC_SECRET", &c.AuthCodeSecret)
	str("BNSDK_PAY_SIGN", &c.BnSDKPaySign)
	str("AUTH_TRUSTED_PROXY_CIDRS", &c.AuthTrustedProxyCIDRs)
	str("SMTP_HOST", &c.SMTPHost)
	str("SMTP_USER", &c.SMTPUser)
	str("SMTP_PASSWORD", &c.SMTPPassword)
	str("SMTP_FROM", &c.SMTPFrom)
	str("SMTP_TLS_MODE", &c.SMTPMode)
	if v := os.Getenv("SMTP_PORT"); v != "" {
		if n, e := strconv.Atoi(v); e == nil && n > 0 && n <= 65535 {
			c.SMTPPort = n
		}
	}
	if v := os.Getenv("AUTH_SESSION_HOURS"); v != "" {
		if n, e := strconv.Atoi(v); e == nil && n > 0 {
			c.AuthSessionHours = n
		}
	}
	if v := os.Getenv("AUTH_EMAIL_ENABLED"); v != "" {
		c.AuthEmailEnabled = truthy(v)
	}
	if v := os.Getenv("AUTH_BEHIND_TLS_PROXY"); v != "" {
		c.AuthBehindTLSProxy = truthy(v)
	}
	if v := os.Getenv("AUTH_ALLOW_HTTP"); v != "" {
		c.AuthAllowHTTP = truthy(v)
	}
	if v := os.Getenv("ALLOW_DEV_LOGIN"); v != "" {
		c.AllowDevLogin = truthy(v)
	}
	if v := os.Getenv("ALLOW_TEST_RPC"); v != "" {
		c.AllowTestRPC = truthy(v)
	}
	if v := os.Getenv("BOT_AUTO_FILL"); v != "" {
		c.BotAutoFill = truthy(v)
	}
	if v := os.Getenv("PROTOCOL_TRACE"); v != "" {
		c.ProtocolTrace = truthy(v)
	}
	if v := os.Getenv("PROTOCOL_TRACE_FULL"); v != "" {
		c.ProtocolTraceFull = truthy(v)
	}
	if v := os.Getenv("LOG_PACKETS"); v != "" {
		c.LogPackets = truthy(v)
	}
	if v := os.Getenv("MAX_PAYLOAD"); v != "" {
		if n, e := strconv.Atoi(v); e == nil && n > 0 {
			c.MaxPayload = n
		}
	}
	if v := os.Getenv("CAPTURE_MAX_BYTES"); v != "" {
		if n, e := strconv.ParseInt(v, 10, 64); e == nil && n > 0 {
			c.CaptureMaxBytes = n
		}
	}
	if v := os.Getenv("CAPTURE_TOTAL_MAX_BYTES"); v != "" {
		if n, e := strconv.ParseInt(v, 10, 64); e == nil && n > 0 {
			c.CaptureTotalMaxBytes = n
		}
	}
	return c
}
func truthy(v string) bool {
	switch v {
	case "1", "true", "yes", "on", "TRUE", "YES", "ON":
		return true
	}
	return false
}
