// JSON process configuration, complementing the .env file: the file is
// created with defaults on first run and rewritten with the full field set so
// operators can discover every option without reading source. Environment
// variables keep precedence for container deployments; absent JSON fields are
// ignored rather than treated as zero values.
package config

import (
	"encoding/json"
	"fmt"
	"os"
	"path/filepath"
)

type FileConfig struct {
	ListenAddr        string `json:"gameServerListen"`
	WebListenAddr     string `json:"dispatchListen"`
	DBPath            string `json:"databasePath"`
	ResourcePath      string `json:"resourceDir"`
	GameServerURL     string `json:"gameServerUrl"`
	HotUpdateURL      string `json:"hotUpdateUrl"`
	HotUpdateRoot     string `json:"hotUpdateRoot"`
	UpstreamWebBase   string `json:"upstreamWebBase"`
	UpstreamRoute     string `json:"upstreamRoute"`
	WebCertFile       string `json:"webTlsCert"`
	WebKeyFile        string `json:"webTlsKey"`
	AllowDevLogin     *bool  `json:"allowDevLogin"`
	BotAutoFill       *bool  `json:"botAutoFill"`
	ProtocolTrace     *bool  `json:"protocolTrace"`
	ProtocolTraceFull *bool  `json:"protocolTraceFull"`
	LogPackets        *bool  `json:"logPackets"`
	AuthEmailEnabled  *bool  `json:"emailAuthEnabled"`
	AuthCodeSecret    string `json:"authCodeSecret"`
}

func defaultFileConfig() FileConfig {
	d := Default()
	yes := true
	return FileConfig{
		ListenAddr: d.ListenAddr, WebListenAddr: d.WebListenAddr,
		DBPath: d.DBPath, ResourcePath: d.ResourcePath,
		UpstreamWebBase: d.WebUpstreamBase,
		BotAutoFill:     &yes, ProtocolTrace: &yes,
	}
}

// LoadJSONFile reads configPath (default ./config.json next to the executable)
// and applies values onto the env-loaded App for fields the env left empty.
// Missing files are created with defaults; unknown fields are ignored so older
// binaries keep booting with newer configs.
func LoadJSONFile(app *App, explicitPath string) (string, error) {
	path := explicitPath
	if path == "" {
		exe, err := os.Executable()
		if err == nil {
			path = filepath.Join(filepath.Dir(exe), "config.json")
		} else {
			path = "config.json"
		}
	}
	data, err := os.ReadFile(path)
	if err != nil {
		if !os.IsNotExist(err) {
			return path, err
		}
		if writeErr := writeJSONFile(path, defaultFileConfig()); writeErr != nil {
			return path, writeErr
		}
		return path, nil
	}
	var parsed FileConfig
	if err := json.Unmarshal(data, &parsed); err != nil {
		return path, fmt.Errorf("parse %s: %w", path, err)
	}
	applyFileConfig(app, &parsed)
	_ = writeJSONFile(path, parsed)
	return path, nil
}

func applyFileConfig(app *App, c *FileConfig) {
	setStr(&app.ListenAddr, c.ListenAddr)
	setStr(&app.WebListenAddr, c.WebListenAddr)
	setStr(&app.DBPath, c.DBPath)
	setStr(&app.ResourcePath, c.ResourcePath)
	setStr(&app.GameServerURL, c.GameServerURL)
	setStr(&app.HotUpdateURL, c.HotUpdateURL)
	setStr(&app.HotUpdateRoot, c.HotUpdateRoot)
	setStr(&app.WebUpstreamBase, c.UpstreamWebBase)
	setStr(&app.WebRoute, c.UpstreamRoute)
	setStr(&app.WebCertFile, c.WebCertFile)
	setStr(&app.WebKeyFile, c.WebKeyFile)
	setStr(&app.AuthCodeSecret, c.AuthCodeSecret)
	if c.AllowDevLogin != nil {
		app.AllowDevLogin = *c.AllowDevLogin
	}
	if c.BotAutoFill != nil {
		app.BotAutoFill = *c.BotAutoFill
	}
	if c.ProtocolTrace != nil {
		app.ProtocolTrace = *c.ProtocolTrace
	}
	if c.ProtocolTraceFull != nil {
		app.ProtocolTraceFull = *c.ProtocolTraceFull
	}
	if c.LogPackets != nil {
		app.LogPackets = *c.LogPackets
	}
	if c.AuthEmailEnabled != nil {
		app.AuthEmailEnabled = *c.AuthEmailEnabled
	}
}

func setStr(dst *string, value string) {
	if value != "" {
		*dst = value
	}
}

func writeJSONFile(path string, c FileConfig) error {
	out, err := json.MarshalIndent(c, "", "  ")
	if err != nil {
		return err
	}
	if err := os.MkdirAll(filepath.Dir(path), 0o750); err != nil {
		return err
	}
	return os.WriteFile(path, out, 0o640)
}
