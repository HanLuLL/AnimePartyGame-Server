package config

import (
	"bufio"
	"fmt"
	"os"
	"path/filepath"
	"strconv"
	"strings"
)

// LoadEnvFile loads Config/server.env without overriding values already set by
// the process environment. Relative file paths in the config are project-root
// relative, so the server can be started from any working directory.
func LoadEnvFile() (string, error) {
	root := findProjectRoot()
	configPath := strings.TrimSpace(os.Getenv("ASTRAL_CONFIG"))
	if configPath != "" {
		if !filepath.IsAbs(configPath) {
			cwd, err := os.Getwd()
			if err != nil {
				return "", err
			}
			configPath = filepath.Join(cwd, configPath)
		}
	} else if root != "" {
		configPath = filepath.Join(root, "Config", "server.env")
	}

	if configPath != "" {
		if _, err := os.Stat(configPath); err == nil {
			if err := loadEnvFile(configPath, root); err != nil {
				return "", err
			}
		} else if !os.IsNotExist(err) {
			return "", fmt.Errorf("inspect config file: %w", err)
		} else if strings.TrimSpace(os.Getenv("ASTRAL_CONFIG")) != "" {
			return "", fmt.Errorf("config file does not exist: %s", configPath)
		} else {
			configPath = ""
		}
	}

	if root != "" {
		setDefaultPath("RESOURCE_DIR", filepath.Join(root, "Resources", "Data"))
		setDefaultPath("DB_PATH", filepath.Join(root, "Server", "data", "server.db"))
	}
	return configPath, nil
}

func loadEnvFile(path, root string) error {
	file, err := os.Open(path)
	if err != nil {
		return fmt.Errorf("open config file: %w", err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	lineNumber := 0
	for scanner.Scan() {
		lineNumber++
		line := strings.TrimSpace(scanner.Text())
		if lineNumber == 1 {
			line = strings.TrimPrefix(line, "\uFEFF")
		}
		if line == "" || strings.HasPrefix(line, "#") {
			continue
		}
		key, value, ok := strings.Cut(line, "=")
		key = strings.TrimSpace(key)
		if !ok || !validEnvName(key) {
			return fmt.Errorf("invalid setting at %s:%d", path, lineNumber)
		}
		if _, exists := os.LookupEnv(key); exists {
			continue
		}
		value = strings.TrimSpace(value)
		if len(value) >= 2 && ((value[0] == '"' && value[len(value)-1] == '"') || (value[0] == '\'' && value[len(value)-1] == '\'')) {
			if value[0] == '"' {
				value, err = strconv.Unquote(value)
				if err != nil {
					return fmt.Errorf("invalid quoted value at %s:%d", path, lineNumber)
				}
			} else {
				value = value[1 : len(value)-1]
			}
		}
		if root != "" && isPathSetting(key) && value != "" && value != ":memory:" && !filepath.IsAbs(value) {
			value = filepath.Join(root, value)
		}
		if err := os.Setenv(key, value); err != nil {
			return fmt.Errorf("set config value %s: %w", key, err)
		}
	}
	if err := scanner.Err(); err != nil {
		return fmt.Errorf("read config file: %w", err)
	}
	return nil
}

func findProjectRoot() string {
	starts := make([]string, 0, 2)
	if cwd, err := os.Getwd(); err == nil {
		starts = append(starts, cwd)
	}
	if executable, err := os.Executable(); err == nil {
		starts = append(starts, filepath.Dir(executable))
	}
	seen := make(map[string]struct{})
	for _, start := range starts {
		current := filepath.Clean(start)
		for range 12 {
			if _, ok := seen[current]; ok {
				break
			}
			seen[current] = struct{}{}
			if isDir(filepath.Join(current, "Resources", "Data")) && isDir(filepath.Join(current, "Config")) {
				return current
			}
			parent := filepath.Dir(current)
			if parent == current {
				break
			}
			current = parent
		}
	}
	return ""
}

func isDir(path string) bool {
	info, err := os.Stat(path)
	return err == nil && info.IsDir()
}

func setDefaultPath(key, value string) {
	if _, exists := os.LookupEnv(key); !exists {
		_ = os.Setenv(key, value)
	}
}

func isPathSetting(key string) bool {
	switch key {
	case "DB_PATH", "RESOURCE_DIR", "HOTUPDATE_ROOT", "HOTADDRESS_SERVER_JSON", "HOTADDRESS_EXTEND_JSON", "WEB_TLS_CERT", "WEB_TLS_KEY":
		return true
	default:
		return false
	}
}

func validEnvName(key string) bool {
	if key == "" {
		return false
	}
	for i, char := range key {
		if (char < 'A' || char > 'Z') && (char < 'a' || char > 'z') && char != '_' && (i == 0 || char < '0' || char > '9') {
			return false
		}
	}
	return true
}
