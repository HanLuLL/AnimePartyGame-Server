package gateway

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"log/slog"
	"net"
	"net/http"
	"net/url"
	"os"
	"path"
	"regexp"
	"strings"
	"sync"
	"time"

	"astralparty-server/internal/config"
)

// WebConfigProxy exposes client bootstrap metadata and optionally delegates
// catalog-listed hot-update files. It is not a general-purpose/open proxy.
type WebConfigProxy struct {
	mu             sync.RWMutex
	upstream       *url.URL
	route          string
	gameServerURL  string
	hotUpdateURL   string
	hotUpdateFiles http.Handler
	localConfigs   map[string][]byte
	client         *http.Client
	log            *slog.Logger
}

var validRoute = regexp.MustCompile(`^[A-Za-z0-9_.-]{1,128}$`)

func (p *WebConfigProxy) Route() string {
	p.mu.RLock()
	defer p.mu.RUnlock()
	return p.route
}

func (p *WebConfigProxy) SetRoute(route string) error {
	route = strings.TrimSpace(route)
	if route != "" && !validRoute.MatchString(route) {
		return errors.New("invalid upstream route")
	}
	p.mu.Lock()
	p.route = route
	p.mu.Unlock()
	return nil
}

func (p *WebConfigProxy) GameServerURL() string {
	p.mu.RLock()
	defer p.mu.RUnlock()
	return p.gameServerURL
}

func (p *WebConfigProxy) SetGameServerURL(value string) error {
	value = strings.TrimSpace(value)
	if len(value) > 1024 || strings.ContainsAny(value, "\r\n\x00") {
		return errors.New("invalid game server URL")
	}
	if value != "" {
		if _, err := config.ParseSelfHostedGameEndpoint(value); err != nil {
			return err
		}
	}
	p.mu.Lock()
	p.gameServerURL = value
	p.mu.Unlock()
	return nil
}

func (p *WebConfigProxy) HotUpdateURL() string {
	p.mu.RLock()
	defer p.mu.RUnlock()
	return p.hotUpdateURL
}

func (p *WebConfigProxy) SetHotUpdateURL(value string) error {
	value = strings.TrimSpace(value)
	if len(value) > 2048 || strings.ContainsAny(value, "\r\n\x00") {
		return errors.New("invalid hot update URL")
	}
	if value != "" {
		parsed, err := url.Parse(value)
		if err != nil || parsed.Host == "" || parsed.User != nil || parsed.RawQuery != "" || parsed.Fragment != "" || (parsed.Scheme != "https" && parsed.Scheme != "http") {
			return errors.New("hot update URL must be an HTTP or HTTPS base URL without credentials, query, or fragment")
		}
	}
	p.mu.Lock()
	p.hotUpdateURL = value
	p.mu.Unlock()
	return nil
}

func (p *WebConfigProxy) SetHotUpdateFiles(handler http.Handler) {
	p.mu.Lock()
	p.hotUpdateFiles = handler
	p.mu.Unlock()
}

func NewWebConfigProxy(upstream, route, gameServerURL string, logger *slog.Logger) (*WebConfigProxy, error) {
	var u *url.URL
	if strings.TrimSpace(upstream) != "" {
		parsed, err := url.Parse(upstream)
		if err != nil || parsed.Host == "" || (parsed.Scheme != "https" && parsed.Scheme != "http") {
			return nil, fmt.Errorf("invalid upstream WebServer base URL")
		}
		u = parsed
	}
	if logger == nil {
		logger = slog.Default()
	}
	gameServerURL = strings.TrimSpace(gameServerURL)
	if gameServerURL != "" {
		if _, err := config.ParseSelfHostedGameEndpoint(gameServerURL); err != nil {
			return nil, fmt.Errorf("invalid GAME_SERVER_URL: %w", err)
		}
	}
	return &WebConfigProxy{upstream: u, route: route, gameServerURL: gameServerURL, client: &http.Client{Timeout: 8 * time.Second}, log: logger}, nil
}

// SetLocalConfigFiles configures self-hosted bootstrap responses. Files are
// loaded once at startup.
func (p *WebConfigProxy) SetLocalConfigFiles(serverConfigPath, hotUpdateConfigPath string) error {
	serverConfigPath = strings.TrimSpace(serverConfigPath)
	hotUpdateConfigPath = strings.TrimSpace(hotUpdateConfigPath)
	if serverConfigPath == "" && hotUpdateConfigPath == "" {
		p.mu.Lock()
		p.localConfigs = nil
		p.mu.Unlock()
		return nil
	}
	if serverConfigPath == "" || hotUpdateConfigPath == "" {
		return errors.New("both local bootstrap JSON files are required")
	}
	serverConfig, err := readBootstrapJSON(serverConfigPath, true)
	if err != nil {
		return fmt.Errorf("load hotaddressServer config: %w", err)
	}
	hotUpdateConfig, err := readBootstrapJSON(hotUpdateConfigPath, false)
	if err != nil {
		return fmt.Errorf("load hotaddressExtend config: %w", err)
	}
	p.mu.Lock()
	p.localConfigs = map[string][]byte{
		"/api/hotaddressServer/get": serverConfig,
		"/api/hotaddressExtend/get": hotUpdateConfig,
	}
	p.mu.Unlock()
	return nil
}

func readBootstrapJSON(file string, requireServerURL bool) ([]byte, error) {
	body, err := os.ReadFile(file)
	if err != nil {
		return nil, err
	}
	var object map[string]json.RawMessage
	if err := json.Unmarshal(body, &object); err != nil || object == nil {
		return nil, errors.New("expected a top-level JSON object")
	}
	if requireServerURL {
		var value string
		raw, ok := object["serverUrl"]
		if !ok || json.Unmarshal(raw, &value) != nil || strings.TrimSpace(value) == "" {
			return nil, errors.New("server config requires a string serverUrl field")
		}
		// A copied example file can still contain its documented placeholder.
		// The effective endpoint is supplied by GAME_SERVER_URL or the admin
		// setting; validate it after applying that override in ServeHTTP. If no
		// valid override exists, ServeHTTP fails closed with HTTP 503.
	} else if err := validateBootstrapHTTPURL(object, "sourceUrl"); err != nil {
		return nil, err
	}
	return body, nil
}

func validateBootstrapHTTPURL(object map[string]json.RawMessage, field string) error {
	var value string
	raw, ok := object[field]
	if !ok || json.Unmarshal(raw, &value) != nil || strings.TrimSpace(value) == "" {
		return fmt.Errorf("bootstrap config requires a non-empty string %s field", field)
	}
	parsed, err := url.Parse(strings.TrimSpace(value))
	if err != nil || parsed.Host == "" || parsed.User != nil ||
		(parsed.Scheme != "https" && parsed.Scheme != "http") {
		return fmt.Errorf("bootstrap %s must be an HTTP or HTTPS URL without credentials", field)
	}
	return nil
}

func (p *WebConfigProxy) localConfig(endpoint string) ([]byte, bool) {
	p.mu.RLock()
	defer p.mu.RUnlock()
	body, ok := p.localConfigs[endpoint]
	return append([]byte(nil), body...), ok
}

func (p *WebConfigProxy) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet && r.Method != http.MethodHead {
		w.Header().Set("Allow", "GET, HEAD")
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}
	if r.URL.Path == "/healthz" {
		w.Header().Set("Content-Type", "text/plain; charset=utf-8")
		if r.Method == http.MethodGet {
			_, _ = io.WriteString(w, "ok")
		}
		return
	}
	endpoint := ""
	switch path.Clean(r.URL.Path) {
	case "/api/hotaddressServer/get":
		endpoint = "/api/hotaddressServer/get"
	case "/api/hotaddressExtend/get":
		endpoint = "/api/hotaddressExtend/get"
	default:
		p.mu.RLock()
		files := p.hotUpdateFiles
		p.mu.RUnlock()
		if files == nil {
			http.NotFound(w, r)
			return
		}
		files.ServeHTTP(w, r)
		return
	}
	if r.Method != http.MethodGet {
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}
	route := r.URL.Query().Get("route")
	// The Android settings screen uses route=local for its preflight. Treat
	// that sentinel like an omitted route so live-upstream mode uses the
	// operator-configured route instead of forwarding the sentinel upstream.
	if route == "" || route == "local" {
		route = p.Route()
	}
	body, local := p.localConfig(endpoint)
	requestSource := "local"
	if !local {
		requestSource = "upstream"
	}
	p.log.Info("bootstrap request", "method", r.Method, "path", endpoint,
		"query", r.URL.RawQuery, "route", route, "source", requestSource)
	if !local && !validRoute.MatchString(route) {
		http.Error(w, "missing/invalid route", http.StatusBadRequest)
		return
	}
	if !local {
		if p.upstream == nil {
			http.Error(w, "bootstrap config is not configured", http.StatusServiceUnavailable)
			return
		}
		target := *p.upstream
		target.Path = path.Join(strings.TrimSuffix(target.Path, "/"), endpoint)
		q := url.Values{}
		q.Set("route", route)
		target.RawQuery = q.Encode()
		ctx, cancel := context.WithTimeout(r.Context(), 8*time.Second)
		defer cancel()
		req, err := http.NewRequestWithContext(ctx, http.MethodGet, target.String(), nil)
		if err != nil {
			http.Error(w, "bad upstream request", http.StatusInternalServerError)
			return
		}
		req.Header.Set("User-Agent", "UnityPlayer/2021.3.45f2 (UnityWebRequest/1.0)")
		resp, err := p.client.Do(req)
		if err != nil {
			p.log.Warn("official bootstrap metadata request failed", "path", endpoint, "err", err)
			http.Error(w, "upstream metadata unavailable", http.StatusBadGateway)
			return
		}
		defer resp.Body.Close()
		body, err = io.ReadAll(io.LimitReader(resp.Body, 1<<20))
		if err != nil {
			http.Error(w, "upstream read failed", http.StatusBadGateway)
			return
		}
		if resp.StatusCode < 200 || resp.StatusCode >= 300 {
			http.Error(w, "upstream metadata returned error", http.StatusBadGateway)
			return
		}
	}
	gameServerURL := p.GameServerURL()
	hotUpdateURL := p.HotUpdateURL()
	if endpoint == "/api/hotaddressExtend/get" && local && hotUpdateURL == "" {
		p.mu.RLock()
		hasLocalHotUpdateFiles := p.hotUpdateFiles != nil
		p.mu.RUnlock()
		if hasLocalHotUpdateFiles {
			p.log.Error("local hot-update files are enabled but HOT_UPDATE_URL is empty; refusing the sample sourceUrl", "path", endpoint)
			http.Error(w, "local hot update requires a public HOT_UPDATE_URL", http.StatusServiceUnavailable)
			return
		}
	}
	if endpoint == "/api/hotaddressServer/get" {
		if gameServerURL == "" && !local {
			p.log.Error("GAME_SERVER_URL is empty; refusing to return upstream game server address", "path", endpoint)
			http.Error(w, "self-hosted game server address is not configured", http.StatusServiceUnavailable)
			return
		}
		if gameServerURL != "" {
			updated, err := p.overrideServerURL(body, gameServerURL)
			if err != nil {
				p.log.Error("cannot override bootstrap serverUrl; refusing upstream response", "err", err)
				http.Error(w, "bootstrap game server address could not be replaced", http.StatusServiceUnavailable)
				return
			}
			body = updated
		}
		if err := validateSelfHostedServerURL(body); err != nil {
			p.log.Error("refusing non-self-hosted bootstrap serverUrl", "err", err)
			http.Error(w, "bootstrap must contain a self-hosted game server address", http.StatusServiceUnavailable)
			return
		}
	}
	if endpoint == "/api/hotaddressExtend/get" && hotUpdateURL != "" {
		updated, err := p.overrideJSONURL(body, "sourceUrl", hotUpdateURL)
		if err != nil {
			if local {
				http.Error(w, "local hot update config is invalid", http.StatusServiceUnavailable)
				return
			}
			p.log.Warn("cannot override sourceUrl; forwarding upstream response", "err", err)
		} else {
			body = updated
		}
	}
	if endpoint == "/api/hotaddressExtend/get" {
		var object map[string]json.RawMessage
		validationErr := json.Unmarshal(body, &object)
		if validationErr == nil {
			validationErr = validateBootstrapHTTPURL(object, "sourceUrl")
		}
		if validationErr != nil {
			p.log.Error("refusing bootstrap response with invalid hot update sourceUrl", "err", validationErr)
			status := http.StatusBadGateway
			if local {
				status = http.StatusServiceUnavailable
			}
			http.Error(w, "hot update source is not configured", status)
			return
		}
	}
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.Header().Set("Cache-Control", "no-store")
	p.log.Info("bootstrap response", "path", endpoint, "status", http.StatusOK,
		"source", requestSource, "payload_bytes", len(body), "body", string(body))
	w.WriteHeader(http.StatusOK)
	_, _ = w.Write(body)
}
func (p *WebConfigProxy) overrideServerURL(body []byte, gameURL string) ([]byte, error) {
	return p.overrideJSONURL(body, "serverUrl", gameURL)
}

func validateSelfHostedServerURL(body []byte) error {
	var object map[string]json.RawMessage
	if err := json.Unmarshal(body, &object); err != nil {
		return fmt.Errorf("invalid bootstrap JSON: %w", err)
	}
	var serverURL string
	raw, ok := object["serverUrl"]
	if !ok || json.Unmarshal(raw, &serverURL) != nil || strings.TrimSpace(serverURL) == "" {
		return errors.New("bootstrap requires a non-empty string serverUrl")
	}
	_, err := config.ParseSelfHostedGameEndpoint(serverURL)
	if err != nil {
		return fmt.Errorf("bootstrap serverUrl is invalid: %w", err)
	}
	return nil
}

func (p *WebConfigProxy) overrideJSONURL(body []byte, field, value string) ([]byte, error) {
	var obj map[string]json.RawMessage
	if err := json.Unmarshal(body, &obj); err != nil {
		return body, err
	}
	if _, ok := obj[field]; !ok {
		return body, fmt.Errorf("bootstrap response has no %s field", field)
	}
	b, err := json.Marshal(value)
	if err != nil {
		return body, err
	}
	obj[field] = b
	return json.Marshal(obj)
}

// telemetryStub answers the client's post-session telemetry and startup image
// endpoints so a dropped game TCP socket does not surface repeated 404 noise.
// These endpoints carry no state the server consumes; an empty JSON body is a
// valid idempotent response.
//
// The body must still be a BnSdk BaseResponse envelope. HttpCallBack.onResponse
// reads the `ret` field and calls ret.equals(getSuccessCode()) with no null
// check (HttpCallBack.java:47), so a bare "{}" leaves ret null after Gson
// parsing and throws a NullPointerException on the main thread, which crashes
// the game. Returning ret="1" routes those callbacks into their success path.
func telemetryStub(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		path := r.URL.Path
		// RemoteConfig drives the client's runtime switches. EnableHttps must
		// stay false: RunTimeRemoteConfigHandler only assigns JsonData when the
		// request succeeds, so a 200 with EnableHttps=true would push the
		// bootstrap onto https://host:7878 where no TLS terminator exists and
		// AppLauncher shows its "failed to fetch server data" dialog. IsAuditMode=false
		// together with EnableLogUpload=true lets RunTimeAstralErrorHandler
		// upload Unity error logs to the self-hosted log endpoint, which is how
		// client-side Addressables failures are diagnosed.
		if strings.HasSuffix(path, "/RemoteConfig.json") {
			w.Header().Set("Content-Type", "application/json; charset=utf-8")
			w.Header().Set("Cache-Control", "no-store")
			w.WriteHeader(http.StatusOK)
			_, _ = w.Write([]byte(`{"EnableHttps":false,"IsAuditMode":false,"EnableLogUpload":true,"IsAngelMode":true,"EnableContact":""}`))
			return
		}
		if strings.HasPrefix(path, "/bna1/") || strings.HasPrefix(path, "/real/") ||
			path == "/api/loginImage/get" || path == "/kick" ||
			path == "/index/agreements" {
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusOK)
			_, _ = w.Write([]byte(`{"ret":"1","msg":"success","content":{}}`))
			return
		}
		next.ServeHTTP(w, r)
	})
}

// canonicalBootstrapPaths rewrites a request path onto the two bootstrap routes
// when it merely carries extra prefix segments. WebServerConfig builds its URL as
// {protocol}{IP_WEB_SERVER}{Path}{/api/hotaddress...}, so a non-empty Path field
// yields a prefixed path that an exact-match route would miss. Normalising here
// keeps the bootstrap answerable regardless of that prefix.
func canonicalBootstrapPaths(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		path := r.URL.Path
		if path != "/api/hotaddressServer/get" && path != "/api/hotaddressExtend/get" {
			for _, suffix := range []string{"/api/hotaddressServer/get", "/api/hotaddressExtend/get"} {
				if idx := strings.Index(path, suffix); idx >= 0 {
					r.URL.Path = suffix
					break
				}
			}
		}
		next.ServeHTTP(w, r)
	})
}

// NewWebMux combines bootstrap metadata, catalog-bounded hot-update files, and
// self-hosted BnSdk routes. The LSP changes only the
// request origin, so handlers receive the game's original path and query.
// This is not a general-purpose/open HTTP proxy.
func NewWebMux(proxy http.Handler, auth http.Handler, logger *slog.Logger, replay http.Handler) http.Handler {
	if logger == nil {
		logger = slog.Default()
	}
	mux := http.NewServeMux()
	if proxy != nil {
		mux.Handle("/healthz", proxy)
		mux.Handle("/api/hotaddressServer/get", proxy)
		mux.Handle("/api/hotaddressExtend/get", proxy)
		mux.Handle("/", telemetryStub(proxy))
	}
	if auth != nil {
		mux.Handle("/auth", auth)
		mux.Handle("/auth/", auth)
		mux.Handle("/api/auth/", auth)
		mux.Handle("/api/client/readiness", auth)
		mux.Handle("/api/init", auth)
		mux.Handle("/account/", auth)
		// SDK housekeeping endpoints the client calls during startup; routed to
		// the auth handler so they answer with a BnSDK success envelope.
		for _, path := range []string{"/api/backState", "/api/ysdksw", "/api/request", "/api/preRequest", "/api/data/get"} {
			mux.Handle(path, auth)
		}
	}
	if replay != nil {
		mux.Handle("/replays/", replay)
	}
	return logHTTPRequests(logger, canonicalBootstrapPaths(mux))
}

type statusResponseWriter struct {
	http.ResponseWriter
	status       int
	bytesWritten int64
}

func (w *statusResponseWriter) WriteHeader(status int) {
	if w.status != 0 {
		return
	}
	w.status = status
	w.ResponseWriter.WriteHeader(status)
}

func (w *statusResponseWriter) Write(body []byte) (int, error) {
	if w.status == 0 {
		w.WriteHeader(http.StatusOK)
	}
	n, err := w.ResponseWriter.Write(body)
	w.bytesWritten += int64(n)
	return n, err
}

func (w *statusResponseWriter) Unwrap() http.ResponseWriter { return w.ResponseWriter }

func logHTTPRequests(logger *slog.Logger, next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		started := time.Now()
		recorder := &statusResponseWriter{ResponseWriter: w}
		next.ServeHTTP(recorder, r)
		status := recorder.status
		if status == 0 {
			status = http.StatusOK
		}
		logger.Info("http request", "method", r.Method, "path", r.URL.Path, "status", status,
			"response_bytes", recorder.bytesWritten, "duration_ms", time.Since(started).Milliseconds())
	})
}

func ListenWebConfig(ctx context.Context, addr, certFile, keyFile string, handler http.Handler, logger *slog.Logger) error {
	if addr == "" {
		return nil
	}
	ln, err := net.Listen("tcp", addr)
	if err != nil {
		return err
	}
	srv := &http.Server{Handler: handler, ReadHeaderTimeout: 5 * time.Second, ReadTimeout: 60 * time.Second, IdleTimeout: 30 * time.Second}
	go func() {
		<-ctx.Done()
		shutdownCtx, cancel := context.WithTimeout(context.Background(), 3*time.Second)
		defer cancel()
		_ = srv.Shutdown(shutdownCtx)
	}()
	logger.Info("bootstrap metadata listener started", "addr", ln.Addr().String())
	if certFile != "" && keyFile != "" {
		err = srv.ServeTLS(ln, certFile, keyFile)
	} else {
		err = srv.Serve(ln)
	}
	if errors.Is(err, http.ErrServerClosed) || errors.Is(err, net.ErrClosed) {
		return nil
	}
	return err
}
