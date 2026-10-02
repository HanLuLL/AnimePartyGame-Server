package main

import (
	"context"
	"fmt"
	"log/slog"
	"net/http"
	"os"
	"os/signal"
	"path/filepath"
	"strings"
	"syscall"
	"time"

	"astralparty-server/internal/auth"
	"astralparty-server/internal/config"
	"astralparty-server/internal/console"
	"astralparty-server/internal/gateway"
	traceevent "astralparty-server/internal/trace"
)

func main() {
	logger := slog.New(newConsoleHandler())
	configPath, err := config.LoadEnvFile()
	if err != nil {
		logger.Error("configuration load failed", "err", err)
		os.Exit(1)
	}
	if configPath != "" {
		logger.Info("configuration loaded", "path", configPath)
	}
	app := config.LoadEnv()
	if configPath, err := config.LoadJSONFile(&app, ""); err != nil {
		logger.Warn("config.json ignored", "err", err)
	} else {
		logger.Info("json config loaded", "path", configPath)
	}
	captures, err := traceevent.NewCaptureStore(app.CaptureDir, app.CaptureMaxBytes, app.CaptureTotalMaxBytes)
	if err != nil {
		logger.Error("packet capture storage unavailable; game listeners will continue without persistent capture", "err", err)
		captures = nil
	} else {
		defer captures.Close()
	}
	if app.DBPath != ":memory:" {
		if err := os.MkdirAll(filepath.Dir(app.DBPath), 0o750); err != nil {
			panic(err)
		}
	}
	srv, err := gateway.New(gateway.Config{ListenAddr: app.ListenAddr, DBPath: app.DBPath, ResourceDir: app.ResourcePath, AllowDevLogin: app.AllowDevLogin, AllowTestRPC: app.AllowTestRPC, BotAutoFill: app.BotAutoFill, ProtocolTrace: app.ProtocolTrace, TraceFull: app.ProtocolTraceFull, LogPackets: app.LogPackets, MaxPayload: app.MaxPayload, Captures: captures}, logger)
	if err != nil {
		panic(err)
	}
	defer srv.Close()
	webProxy, err := gateway.NewWebConfigProxy(app.WebUpstreamBase, app.WebRoute, app.GameServerURL, logger)
	if err != nil {
		panic(err)
	}
	if err := webProxy.SetHotUpdateURL(app.HotUpdateURL); err != nil {
		panic(err)
	}
	var hotUpdateCatalogBundles, hotUpdateCachedBundles int
	if strings.TrimSpace(app.HotUpdateRoot) != "" {
		hotUpdateFiles, err := gateway.NewHotUpdateFileServer(app.HotUpdateRoot, logger)
		if err != nil {
			logger.Error("cannot start configured hot-update file server", "err", err)
			return
		}
		webProxy.SetHotUpdateFiles(hotUpdateFiles)
		hotUpdateCatalogBundles, hotUpdateCachedBundles = hotUpdateFiles.BundleCounts()
	}
	if app.HotAddressJSON != "" || app.ServerConfigJSON != "" {
		if err := webProxy.SetLocalConfigFiles(app.HotAddressJSON, app.ServerConfigJSON); err != nil {
			panic(err)
		}
	}
	if value, ok, e := srv.Storage().GetSetting(context.Background(), "bootstrap_route"); e != nil {
		panic(e)
	} else if ok {
		if e = webProxy.SetRoute(value); e != nil {
			panic(e)
		}
	}
	if value, ok, e := srv.Storage().GetSetting(context.Background(), "game_server_url"); e != nil {
		panic(e)
	} else if ok {
		if e = webProxy.SetGameServerURL(value); e != nil {
			logger.Warn("stored game server address is invalid and was ignored", "reason", e.Error())
		}
	}
	if value, ok, e := srv.Storage().GetSetting(context.Background(), "hot_update_url"); e != nil {
		panic(e)
	} else if ok {
		if e = webProxy.SetHotUpdateURL(value); e != nil {
			panic(e)
		}
	}
	smtpConfigured := strings.TrimSpace(app.SMTPHost) != "" && app.SMTPPort > 0 && strings.TrimSpace(app.SMTPFrom) != "" &&
		(app.SMTPMode == "starttls" || app.SMTPMode == "implicit") && !config.IsExampleEndpoint(app.SMTPHost) && !config.IsExampleEndpoint(app.SMTPFrom) &&
		!config.IsExampleSecret(app.SMTPUser) && !config.IsExampleSecret(app.SMTPPassword)
	authSecretConfigured := len(app.AuthCodeSecret) >= 32 && !config.IsExampleSecret(app.AuthCodeSecret)
	httpsTerminationConfigured := app.WebCertFile != "" && app.WebKeyFile != "" || app.AuthBehindTLSProxy
	authTransportConfigured := httpsTerminationConfigured || app.AuthAllowHTTP
	localBootstrapConfigured := strings.TrimSpace(app.HotAddressJSON) != "" && strings.TrimSpace(app.ServerConfigJSON) != ""
	emailLoginConfigured := app.AuthEmailEnabled && smtpConfigured && authSecretConfigured && authTransportConfigured
	logger.Info("client login readiness",
		"email_auth_enabled", app.AuthEmailEnabled,
		"smtp_delivery_configured", smtpConfigured,
		"https_termination_configured", httpsTerminationConfigured,
		"auth_behind_tls_proxy", app.AuthBehindTLSProxy,
		"auth_trusted_proxy_allowlist_configured", strings.TrimSpace(app.AuthTrustedProxyCIDRs) != "",
		"http_auth_allowed", app.AuthAllowHTTP,
		"auth_transport_configured", authTransportConfigured,
		"auth_hmac_secret_configured", authSecretConfigured,
		"email_login_static_prerequisites_present", emailLoginConfigured,
		"local_bootstrap_files_configured", localBootstrapConfigured,
		"hot_update_root_configured", strings.TrimSpace(app.HotUpdateRoot) != "",
		"hot_update_catalog_bundles", hotUpdateCatalogBundles,
		"hot_update_cached_bundles", hotUpdateCachedBundles,
		"hot_update_missing_bundles", hotUpdateCatalogBundles-hotUpdateCachedBundles,
		"hot_update_source_url_override_configured", strings.TrimSpace(webProxy.HotUpdateURL()) != "",
		"game_server_url_override_configured", strings.TrimSpace(webProxy.GameServerURL()) != "")
	authHandler, err := auth.NewHTTPHandler(srv.Storage(), auth.Config{
		Enabled: app.AuthEmailEnabled, SMTPHost: app.SMTPHost, SMTPPort: app.SMTPPort,
		SMTPUser: app.SMTPUser, SMTPPassword: app.SMTPPassword, SMTPFrom: app.SMTPFrom,
		SMTPMode: app.SMTPMode, SDKPaySign: app.BnSDKPaySign, CodeSecret: app.AuthCodeSecret,
		SessionTTL:     time.Duration(app.AuthSessionHours) * time.Hour,
		BehindTLSProxy: app.AuthBehindTLSProxy, TrustedProxyCIDRs: app.AuthTrustedProxyCIDRs, AllowHTTP: app.AuthAllowHTTP,
		ProtocolTrace: srv.RecordHTTPTrace,
		TraceEnabled:  srv.ProtocolTrace,
		TraceFull:     srv.ProtocolTraceFull,
		PlayerOnline:  srv.PlayerOnline,
		PushInventory: srv.PushConsoleInventory,
	}, logger)
	if err != nil {
		panic(err)
	}
	noticeHandler := gateway.NoticeHandler(srv.Storage())
	clientWebHandler := http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		// The notice feed is requested from the bootstrap noticeUrl. Match any
		// path ending in /Temp/Notice so channel/version prefixes such as
		// /CN_ANDROID/3.2.0/Temp/Notice are served the same way RemoteConfig is.
		if strings.HasSuffix(r.URL.Path, "/Temp/Notice") {
			noticeHandler.ServeHTTP(w, r)
			return
		}
		webProxy.ServeHTTP(w, r)
	})
	webHandler := srv.CaptureHTTP(gateway.NewWebMux(clientWebHandler, authHandler, logger, srv.ReplayHTTPHandler()))
	logger.Info(fmt.Sprintf("Loaded %d config tables.", srv.ConfigTableCount()))
	logger.Warn("AstralParty is free and open-source. If you paid for it, you were scammed.")
	logger.Info(fmt.Sprintf("Game server listening on %s", app.ListenAddr))
	logger.Info(fmt.Sprintf("Dispatch server listening on %s", app.WebListenAddr))
	logger.Info(fmt.Sprintf("Game endpoint: %s", webProxy.GameServerURL()))
	logger.Info(fmt.Sprintf("Hot update source: %s", webProxy.HotUpdateURL()))
	logger.Info("Database: " + app.DBPath)
	logger.Info("Resources: " + app.ResourcePath)
	logger.Info("Type /help for server commands.")
	ctx, stop := signal.NotifyContext(context.Background(), os.Interrupt, syscall.SIGTERM)
	defer stop()
	errCh := make(chan error, 2)
	go func() { errCh <- srv.ListenAndServe(ctx) }()
	go func() {
		errCh <- gateway.ListenWebConfig(ctx, app.WebListenAddr, app.WebCertFile, app.WebKeyFile, webHandler, logger)
	}()
	go console.Run(ctx, srv, func(format string, args ...any) { logger.Info(fmt.Sprintf(format, args...)) })
	select {
	case <-srv.Stopped():
		logger.Info("shutting down")
		stop()
	case <-ctx.Done():
	case err := <-errCh:
		if err != nil {
			logger.Error("listener stopped", "err", err)
		}
		stop()
		<-ctx.Done()
	}
}
