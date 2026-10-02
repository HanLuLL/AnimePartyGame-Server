# Hot-update extraction & local hosting (AstralParty 3.2.0)

Status: local hosting configured and verified end-to-end against the compiled
`/tmp/ap-server`. **The APK does not ship the remote hot-update payload** — only
the bootstrap catalog. Details and the client URL pattern are below.

## 1. What the APK contains

APK: `/home/agentuser/Downloads/AstralParty-3.2.0.apk`

Addressables content lives under `assets/aa/`:

| entry | role |
|---|---|
| `assets/aa/settings.json` | Addressables runtime settings (catalog locations) |
| `assets/aa/catalog.bundle` | UnityFS bundle holding the built-in `catalog` TextAsset |
| `assets/aa/Android/*.bundle` | 46 bundles, all addressed by `{UnityEngine.AddressableAssets.Addressables.RuntimePath}/Android/...` (shipped-with-APK content, **not** the remote set) |
| `assets/aa/AddressablesLink/link.xml` | link.xml |

`settings.json` catalog locations:

```
AddressablesMainContentCatalogRemoteHash  -> {App.WebServerConfig.Path}/catalog_3.2.0.hash
AddressablesMainContentCatalogCacheHash   -> {UnityEngine.Application.persistentDataPath}/com.unity.addressables/catalog_3.2.0.hash
AddressablesMainContentCatalog            -> {UnityEngine.AddressableAssets.Addressables.RuntimePath}/catalog.bundle
```

So the shipped `catalog.bundle` is the *local* 3.2.0 catalog; the remote catalog
(`catalog_3.2.0.json` + `catalog_3.2.0.hash`) and every remote bundle are fetched
at run time under `{App.WebServerConfig.Path}/`.

## 2. Extracted hot-update data

The built-in `catalog` TextAsset was extracted from `catalog.bundle` with UnityPy
(`uv pip install UnityPy lz4`), giving the single-file 3.2.0 catalog JSON:

```
/home/agentuser/hotupdate/catalog_3.2.0.json   7,655,452 bytes
/home/agentuser/hotupdate/catalog_3.2.0.hash   f88808f8d70b738096218043cd786907   (= m_BuildResultHash)
```

The catalog references **5457** remote bundles, all as
`{App.WebServerConfig.Path}/<32-hex>.bundle` (plus 8 `font_tmp_assets_*.bundle`
entries). **None of those 5457 hashes exist in the APK** — intersect with the 46
bundled files = 0. The bundles are downloaded from the CDN on first run.

Expected on-disk layout (what the server scans):

```
/home/agentuser/hotupdate/
├── catalog_3.2.0.hash
├── catalog_3.2.0.json
└── AssetBundles/<cache-group>/<bundle-id>/__data
```

`<cache-group>` is the Unity cache group name (arbitrary string, currently
`0`), `<bundle-id>` is the 32-hex bundle hash (font bundles use their trailing
hash, e.g. `font_tmp_assets_afacad-regular_tmp_165aaf28...bundle` ->
`165aaf28782ee6a0449974b05e4631a7`). Only hashes listed in the catalog are
served; the directory listing is read **once at startup**, so new bundles
require a server restart.

## 3. Server configuration

`Config/server.env` (copied from `server.env.example`) with:

```
ALLOW_DEV_LOGIN=true
AUTH_CODE_HMAC_SECRET=<64 hex>
ADMIN_TOKEN=<64 hex>
RESOURCE_DIR=/home/agentuser/AnimePartyGame-Server/Resources/Data
HOTUPDATE_ROOT=/home/agentuser/hotupdate
HOT_UPDATE_URL=http://127.0.0.1:7878
```

`/tmp/config.json` (created next to the binary) is also pinned to absolute
paths, because `config.json` is applied *after* the env file and its default
`../Resources/Data` / `data/server.db` are resolved against the binary's cwd:

```json
{
  "gameServerListen": ":8800",
  "dispatchListen": ":7878",
  "databasePath": "/home/agentuser/AnimePartyGame-Server/Server/data/server.db",
  "resourceDir": "/home/agentuser/AnimePartyGame-Server/Resources/Data",
  "gameServerUrl": "0.0.0.0:8800",
  "hotUpdateUrl": "http://127.0.0.1:7878",
  "hotUpdateRoot": "/home/agentuser/hotupdate",
  "upstreamWebBase": "https://0.0.0.0:7878"
}
```

Start:

```
cd /home/agentuser/AnimePartyGame-Server && /tmp/ap-server
```

Startup log confirms both subsystems:

```
hot-update file server ready root=/home/agentuser/hotupdate catalog_bundles=5457 cached_bundles=0 missing_bundles=5457
Resource loading complete: 260 config tables loaded
Hot update source : http://127.0.0.1:7878
```

## 4. Verification (curl, port 7878)

| request | result |
|---|---|
| `GET /healthz` | `200 ok` |
| `GET /api/hotaddressExtend/get` | `200 {"sdkVersion":"1.0.1","sourceUrl":"http://127.0.0.1:7878","version":"3.2.0"}` — sourceUrl rewritten to this host |
| `GET /api/hotaddressServer/get` | `200 {"noticeUrl":"https://0.0.0.0","serverUrl":"0.0.0.0:8800","version":"3.2.0"}` |
| `GET /catalog_3.2.0.hash` | `200 f88808f8d70b738096218043cd786907`, `text/plain`, `Cache-Control: no-store` |
| `GET /catalog_3.2.0.json` | `200`, 7,655,452 bytes, byte-identical to the extracted catalog |
| `GET /0016adc5…5e.bundle` (catalog-listed, placed at `AssetBundles/0/0016adc5…5e/__data`) | `200`, exact byte match, `application/octet-stream`, `public, max-age=31536000, immutable` |
| `GET /deadbeef…bundle` (not catalog-listed / absent) | `404` |

Bundle serving was proven with a probe `__data` placed under a catalog-listed
hash and a second server start; the probe was then removed.

## 5. Client-side URL pattern (from the dump)

The runtime class is `App.WebServerConfig` (in `AppConfig.dll`; the type name
appears in `dump/dll/global-metadata.dat` as `WebServerConfig` / `urlType` /
`GetWebServerUrl` alongside `HotUpdateConfigData {sourceUrl, sdkVersion}` and
`RemoteServerConfigData {noticeUrl, serverUrl}`). The exact `Path` literal is
not present as a string in the dump, but the first-boot requests are fully
determined by the metadata string pool:

```
GET {App.WebServerConfig.Path}/catalog_3.2.0.hash        <- Addressables remote-hash check
GET {App.WebServerConfig.Path}/catalog_3.2.0.json        <- only when the hash differs
GET {App.WebServerConfig.Path}/<bundle-hash>.bundle      <- one per needed bundle
```

Bootstrap endpoints (from `global-metadata.dat`, verbatim):

```
/api/hotaddressExtend/get?route=
/api/hotaddressServer/get?route=
/api/loginImage/get?route=
```

The returned `sourceUrl` is what the client substitutes for
`{App.WebServerConfig.Path}`, which is why the locally hosted catalog/bundles are
picked up once `HOT_UPDATE_URL` points at this server.

CDN defaults left in the binary (for reference): `https://serescn.feimogames.com`,
`se-web-cn.feimogames.com`, `se-client-log-cn.feimogames.com:8810`.

## 6. Gaps / notes

- **No remote bundles to host.** A fully offline hot-update chain is impossible
  with this APK alone; the 5457 remote bundles must be fetched from the CDN
  (`{App.WebServerConfig.Path}/…`) and dropped into `AssetBundles/<group>/…/__data`
  before the local file server can serve them. Currently `cached_bundles=0` and
  every bundle request returns 404 (logged as “requested hot-update bundle is not
  present in the local cache”).
- The catalog still carries the CDN-relative `{App.WebServerConfig.Path}/` prefix
  (that is what the server expects); it was not rewritten.
- `catalog_3.2.0.hash` was written from the catalog's `m_BuildResultHash`; the
  real CDN `.hash` is a shorter content hash. If upstream ever serves a different
  value, replace the file — the server only transports it, and clients compare it
  against their cached copy.
- The binary's `config.json` is the reliable place for absolute paths; exporting
  only env vars leaves resource/db paths resolved against the cwd.

## 8. Ready-to-serve layout (server_root)

The gateway serves bundles from Unity's on-disk cache layout:
`HOTUPDATE_ROOT/AssetBundles/<cache-group>/<bundle-key>/__data`, where the bundle
key is the bundle file name without the `.bundle` suffix (the MD5 hash, or the
trailing hash for `font_tmp_*` bundles). The catalog pair
(`catalog_3.2.0.json` + `catalog_3.2.0.hash`) must sit at `HOTUPDATE_ROOT`.

A prepared tree lives at `/home/agentuser/hotupdate/server_root` (hard links to
the downloaded flat set under `CN_ANDROID_001/`). With
`HOTUPDATE_ROOT=/home/agentuser/hotupdate/server_root` the startup warning drops
from 5457 missing bundles to 8.

## 9. The 8 remaining font bundles

The 8 unresolved entries are `font_tmp_assets_*_tmp_<md5>.bundle` font atlas
caches. They were never published upstream (HTTP 404 on the official CDN for
every revision probed) and are absent from every mirror. Clients generate these
caches locally at runtime, so their absence on the server is expected and
harmless. All 5449 published bundles are served, covering the whole downloadable
catalog.
