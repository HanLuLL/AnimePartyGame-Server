package gateway

import (
	"encoding/json"
	"fmt"
	"io"
	"log/slog"
	"net/http"
	"os"
	"path"
	"path/filepath"
	"regexp"
	"sort"
	"strings"
	"time"
)

const (
	hotUpdateCatalogHash = "catalog_3.2.0.hash"
	hotUpdateCatalogJSON = "catalog_3.2.0.json"
	maxCatalogJSONBytes  = 256 << 20
)

var hotUpdateBundleName = regexp.MustCompile(`^([0-9a-f]{32}|font_tmp_assets_[a-z0-9-]+_tmp_[0-9a-f]{32})\.bundle$`)
var hotUpdateBundleHash = regexp.MustCompile(`^[0-9a-f]{32}$`)
var hotUpdateFontCacheKey = regexp.MustCompile(`^font_tmp_assets_[a-z0-9-]+_tmp_([0-9a-f]{32})\.bundle$`)

// HotUpdateFileServer exposes the supplied Addressables catalog and maps its
// remote bundle IDs to Unity's on-disk cache layout:
// AssetBundles/<cache-group>/<bundle-id>/__data.
// It serves only catalog-listed bundle hashes; it is not a general file server.
type HotUpdateFileServer struct {
	root         string
	bundlePaths  map[string]string
	catalogCount int
	cachedCount  int
	logger       *slog.Logger
}

func NewHotUpdateFileServer(root string, logger *slog.Logger) (*HotUpdateFileServer, error) {
	root = strings.TrimSpace(root)
	if root == "" {
		return nil, fmt.Errorf("hot-update root is empty")
	}
	absRoot, err := filepath.Abs(root)
	if err != nil {
		return nil, fmt.Errorf("resolve hot-update root: %w", err)
	}
	rootInfo, err := os.Lstat(absRoot)
	if err != nil {
		return nil, fmt.Errorf("open hot-update root: %w", err)
	}
	if !rootInfo.IsDir() || rootInfo.Mode()&os.ModeSymlink != 0 {
		return nil, fmt.Errorf("hot-update root must be a real directory")
	}
	if logger == nil {
		logger = slog.Default()
	}

	if err := requireHotUpdateFile(filepath.Join(absRoot, hotUpdateCatalogHash)); err != nil {
		return nil, fmt.Errorf("hot-update catalog hash: %w", err)
	}
	catalogPath := filepath.Join(absRoot, hotUpdateCatalogJSON)
	if err := requireHotUpdateFile(catalogPath); err != nil {
		return nil, fmt.Errorf("hot-update catalog: %w", err)
	}
	catalogInfo, err := os.Stat(catalogPath)
	if err != nil {
		return nil, fmt.Errorf("stat hot-update catalog: %w", err)
	}
	if catalogInfo.Size() > maxCatalogJSONBytes {
		return nil, fmt.Errorf("hot-update catalog exceeds %d bytes", maxCatalogJSONBytes)
	}
	catalogFile, err := os.Open(catalogPath)
	if err != nil {
		return nil, fmt.Errorf("read hot-update catalog: %w", err)
	}
	var catalog struct {
		InternalIDs []string `json:"m_InternalIds"`
	}
	decodeErr := json.NewDecoder(io.LimitReader(catalogFile, maxCatalogJSONBytes)).Decode(&catalog)
	closeErr := catalogFile.Close()
	if decodeErr != nil {
		return nil, fmt.Errorf("decode hot-update catalog: %w", decodeErr)
	}
	if closeErr != nil {
		return nil, fmt.Errorf("close hot-update catalog: %w", closeErr)
	}

	const internalIDPrefix = "{App.WebServerConfig.Path}/"
	catalogBundles := make(map[string]struct{})
	bundleNamesByCacheKey := make(map[string][]string)
	for _, internalID := range catalog.InternalIDs {
		if !strings.HasPrefix(internalID, internalIDPrefix) {
			continue
		}
		name := strings.TrimPrefix(internalID, internalIDPrefix)
		if !hotUpdateBundleName.MatchString(name) {
			continue
		}
		cacheKey, ok := hotUpdateBundleCacheKey(name)
		if !ok {
			continue
		}
		if _, exists := catalogBundles[name]; exists {
			continue
		}
		catalogBundles[name] = struct{}{}
		bundleNamesByCacheKey[cacheKey] = append(bundleNamesByCacheKey[cacheKey], name)
	}
	if len(catalogBundles) == 0 {
		return nil, fmt.Errorf("hot-update catalog contains no supported bundle URLs")
	}

	bundleRoot := filepath.Join(absRoot, "AssetBundles")
	bundleRootInfo, err := os.Lstat(bundleRoot)
	if err != nil {
		return nil, fmt.Errorf("hot-update AssetBundles directory: %w", err)
	}
	if !bundleRootInfo.IsDir() || bundleRootInfo.Mode()&os.ModeSymlink != 0 {
		return nil, fmt.Errorf("hot-update AssetBundles path must be a real directory")
	}
	bundleDirs, err := os.ReadDir(bundleRoot)
	if err != nil {
		return nil, fmt.Errorf("read hot-update AssetBundles directory: %w", err)
	}

	bundlePaths := make(map[string]string, len(catalogBundles))
	for _, bundleDir := range bundleDirs {
		if !bundleDir.IsDir() || bundleDir.Type()&os.ModeSymlink != 0 {
			continue
		}
		bundleDirPath := filepath.Join(bundleRoot, bundleDir.Name())
		entries, readErr := os.ReadDir(bundleDirPath)
		if readErr != nil {
			logger.Warn("cannot inspect hot-update bundle cache directory", "path", bundleDirPath, "err", readErr)
			continue
		}
		for _, entry := range entries {
			if !entry.IsDir() || entry.Type()&os.ModeSymlink != 0 || !hotUpdateBundleHash.MatchString(entry.Name()) {
				continue
			}
			bundleNames, needed := bundleNamesByCacheKey[entry.Name()]
			if !needed {
				continue
			}
			dataPath := filepath.Join(bundleDirPath, entry.Name(), "__data")
			if err := requireHotUpdateFile(dataPath); err != nil {
				continue
			}
			for _, bundleName := range bundleNames {
				if previous, exists := bundlePaths[bundleName]; exists && previous != dataPath {
					return nil, fmt.Errorf("duplicate cached hot-update bundle %s", bundleName)
				}
				bundlePaths[bundleName] = dataPath
			}
		}
	}
	missingBundles := make([]string, 0, len(catalogBundles)-len(bundlePaths))
	for bundleName := range catalogBundles {
		if _, cached := bundlePaths[bundleName]; !cached {
			missingBundles = append(missingBundles, bundleName)
		}
	}
	sort.Strings(missingBundles)

	server := &HotUpdateFileServer{
		root:         absRoot,
		bundlePaths:  bundlePaths,
		catalogCount: len(catalogBundles),
		cachedCount:  len(bundlePaths),
		logger:       logger,
	}
	if len(missingBundles) > 0 {
		shown := missingBundles
		if len(shown) > 10 {
			shown = shown[:10]
		}
		logger.Warn("hot-update catalog references files not present on disk",
			"missing", len(missingBundles), "examples", strings.Join(shown, ", "))
	}
	logger.Info("hot-update file server ready", "root", absRoot, "catalog_bundles", server.catalogCount,
		"cached_bundles", server.cachedCount, "missing_bundles", server.MissingBundleCount())
	return server, nil
}

func hotUpdateBundleCacheKey(name string) (string, bool) {
	if strings.HasSuffix(name, ".bundle") {
		key := strings.TrimSuffix(name, ".bundle")
		if hotUpdateBundleHash.MatchString(key) {
			return key, true
		}
	}
	if match := hotUpdateFontCacheKey.FindStringSubmatch(name); len(match) == 2 {
		return match[1], true
	}
	return "", false
}

func requireHotUpdateFile(file string) error {
	info, err := os.Lstat(file)
	if err != nil {
		return err
	}
	if !info.Mode().IsRegular() || info.Mode()&os.ModeSymlink != 0 {
		return fmt.Errorf("%q is not a regular file", file)
	}
	return nil
}

func (s *HotUpdateFileServer) BundleCounts() (catalog, cached int) {
	return s.catalogCount, s.cachedCount
}

func (s *HotUpdateFileServer) MissingBundleCount() int {
	return s.catalogCount - s.cachedCount
}

func (s *HotUpdateFileServer) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet && r.Method != http.MethodHead {
		w.Header().Set("Allow", "GET, HEAD")
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}
	if strings.HasSuffix(r.URL.Path, "/") {
		http.NotFound(w, r)
		return
	}
	name := path.Base(r.URL.Path)
	var file string
	var contentType string
	fileKind := "bundle"
	immutable := false
	mutableCatalog := false
	switch name {
	case hotUpdateCatalogHash:
		file = filepath.Join(s.root, hotUpdateCatalogHash)
		contentType = "text/plain; charset=utf-8"
		fileKind = "catalog_hash"
		mutableCatalog = true
	case hotUpdateCatalogJSON:
		file = filepath.Join(s.root, hotUpdateCatalogJSON)
		contentType = "application/json"
		fileKind = "catalog"
		mutableCatalog = true
	default:
		if !hotUpdateBundleName.MatchString(name) {
			http.NotFound(w, r)
			return
		}
		var ok bool
		file, ok = s.bundlePaths[name]
		if !ok {
			s.logger.Warn("requested hot-update bundle is not present in the local cache", "path", r.URL.Path, "bundle", name)
			http.NotFound(w, r)
			return
		}
		contentType = "application/octet-stream"
		immutable = true
	}
	opened, err := os.Open(file)
	if err != nil {
		s.logger.Error("cannot open hot-update file", "path", r.URL.Path, "file", file, "err", err)
		http.Error(w, "hot-update file unavailable", http.StatusInternalServerError)
		return
	}
	defer opened.Close()
	info, err := opened.Stat()
	if err != nil || !info.Mode().IsRegular() {
		s.logger.Error("hot-update path is not a regular file", "path", r.URL.Path, "file", file, "err", err)
		http.Error(w, "hot-update file unavailable", http.StatusInternalServerError)
		return
	}
	w.Header().Set("Content-Type", contentType)
	w.Header().Set("X-Content-Type-Options", "nosniff")
	if mutableCatalog {
		// Addressables compares the remote .hash with its cached value, then
		// downloads the catalog JSON only when that value changes. Intermediary
		// caches must not keep either mutable file stale across a content update.
		w.Header().Set("Cache-Control", "no-store")
	} else if immutable {
		w.Header().Set("Cache-Control", "public, max-age=31536000, immutable")
	} else {
		w.Header().Set("Cache-Control", "public, max-age=300")
	}
	s.logger.Info("hot-update file response", "method", r.Method, "path", r.URL.Path,
		"kind", fileKind, "file_bytes", info.Size(), "range", r.Header.Get("Range"))
	http.ServeContent(w, r, name, info.ModTime().UTC().Round(time.Second), opened)
}
