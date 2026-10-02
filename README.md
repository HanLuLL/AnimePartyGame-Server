# AnimeParty Game Server

A Go re-implementation of the **AstralParty 3.2.0** game server, built for
private-server hosting with a fully interactive command-line console. The
project aims for feature parity with the official servers: accounts, lobby,
rooms, real-time battle, guilds, single-player campaign, inventory, shop,
gacha, mail, tasks, battle pass, friends, matchmaking and more.

The server ships with a colorful terminal log, a rich interactive console, and a clean, auditable codebase — no web back-office required.

## Features

- **Complete protocol** — generated from the game's own `AstralParty.proto`
  (260 config tables loaded from the original `Resources/Data` JSON dumps).
- **Login & accounts** — phone-number / BnSdk login flow, dev login for
  testing, self-hosted account authentication with optional e-mail delivery.
- **Rooms & real-time battle** — create/join/invite rooms, ready-up, hero
  selection, state-machine-driven battle progression, bot auto-fill, victory
  settlement and rewards.
- **Guilds** — create, search, apply/invite, roles (master/officer/member),
  announcements, kick/transfer/impeach, guild chat, guild missions.
- **Single-player campaign** — persistent per-player progress, level unlock
  flow driven by the original `SinglePlayer_*` config tables.
- **Economy** — inventory with bag pushes, shop, gacha/lottery, currency,
  mail (single + broadcast), tasks, achievements, battle pass.
- **Replays** — fight records and replay snapshots persisted per battle.
- **Hot-update CDN** — serves the original 5,457-bundle catalog so the
  patched client boots without any official infrastructure.
- **Interactive console** — slash commands with tab-friendly help (see below).
- **Colorful logging** — ANSI-highlighted `[HH:MM:SS] [LEVEL]` lines, with a
  file sink in `logs/latest.log`.

## Requirements

- Go 1.22+ with cgo (SQLite via mattn/go-sqlite3)
- The game's `Resources/` directory (config JSON dumps)

## Building

```bash
cp Config/server.env.example Config/server.env
cd Server
go build -o bin/astralparty-server .
```

## Configuration

Configuration is environment-driven; `Config/server.env.example` documents
every supported variable. The essentials:

| Variable           | Default    | Description                          |
|--------------------|------------|--------------------------------------|
| `LISTEN_ADDR`      | `:8800`    | Game (TCP) listen address            |
| `WEB_CONFIG_ADDR`  | `:7878`    | Dispatch / bootstrap HTTP address    |
| `DB_PATH`          | `server.db`| SQLite database file                 |
| `RESOURCE_DIR`     | `Resources`| Path to the config table dumps       |
| `ALLOW_DEV_LOGIN`  | `false`    | Enable the dev login flow            |
| `BOT_AUTO_FILL`    | `true`     | Fill incomplete rooms with bots      |

## Running

```bash
./bin/astralparty-server
```

Startup output looks like this:

```
[13:05:52] [INFO ] Loaded 260 config tables.
[13:05:52] [INFO ] Game server listening on :8800
[13:05:52] [INFO ] Dispatch server listening on :7878
[13:05:52] [INFO ] Type /help for server commands.
[13:05:52] [INFO ] Game Server started on [::]:8800
[13:05:52] [INFO ] Dispatch Server started on [::]:7878 (HTTP)
```

## Console commands

Type commands directly into the running server terminal (a leading `/` is
optional):

```
/help                          show the command list
/status                        server uptime, table count and gameplay flags
/give <playerId> <itemId> [count]   give an item to a player
/giveall <itemId> <count>      give an item to every player
/gold <playerId> <amount>      set a player's gold
/setlevel <playerId> <level>   set a player's level
/mail <playerId|all> <title> | <body>   send a system mail
/kick <playerId>               disconnect a player
/delplayer <playerId>          delete a player
/finishroom <roomId>           force-finish a running room
/reload                        reload the gameplay config
/stop                          shut the server down
```

## Client bootstrap

Before opening the game socket the client fetches two endpoints:

    /api/hotaddressExtend/get -> HotUpdateConfigData { version, sourceUrl, sdkVersion }
    /api/hotaddressServer/get -> RemoteServerConfigData { noticeUrl, serverUrl, version }

`serverUrl` is the game TCP endpoint; `sourceUrl` is the hot-update base URL.
With `GAME_SERVER_URL` set the server rewrites it to your address and rejects
any response that would point clients elsewhere.

## Hot update

`HOTUPDATE_ROOT` may point at an extracted 3.2.0 Addressables cache:

    HOTUPDATE_ROOT=/path/to/cache
    HOT_UPDATE_URL=https://your-host

The cache needs `catalog_3.2.0.hash`, `catalog_3.2.0.json` and
`AssetBundles/<cache-group>/<bundle-id>/__data`. Only bundle hashes listed in
the catalog are served; anything else returns 404. Leave both variables empty
and clients keep pulling hot-update files from the official CDN.

## Client connection

The companion patched APK replaces the official endpoints with private
hostnames (`prv-web.astralparty.lan`, `prv-dl.astralparty.lan`). Point your
private DNS for those names at this server: `:7878` serves the bootstrap /
dispatch HTTP API and `:8800` the game TCP socket. See `docs/CLIENT-SETUP.md` for
Android (patched APK, root hosts, VPN-based) and PC (hosts file, DNS proxy,
mitmproxy) connection guides.

## Wire protocol

`proto/AstralParty.proto` carries every message exchanged with the client.
Each frame starts with a 35-byte big-endian header:

    offset  size  field
    0       4     payload length
    4       8     session id
    12      2     command id
    14      3     client version
    17      8     up sequence number
    25      8     down sequence number
    33      2     error code

Command ids pair as Handle/S2C, for example 5001 ConnectC2S with 5002
ConnectS2C.

## Project layout

```
Server/
  main.go                 entry point: wiring, startup banner, console loop
  consolelog.go           colorful slog handler + logs/latest.log file sink
  internal/
    gateway/              TCP server, protocol dispatch, all game handlers
      handlers.go         C2S message switch
      console_exec.go     console/operator command implementations
      snapshot_fill.go    ConnectS2C bootstrap snapshot assembly
    db/                   SQLite persistence layer (versioned migrations)
    gdconf/               config table loading (Resources/Data JSON dumps)
    game/                 domain logic shared by handlers
    auth/                 account authentication (phone/BnSdk/dev)
    console/              interactive stdin command loop
    protocol/gen/         generated protobuf types from AstralParty.proto
assets/
  audio/                  extracted hot-update audio (ogg + banks)
  resources/              extracted APK resources (textures, meshes, fonts)
Resources/                original config table JSON (loaded at runtime)
proto/                    AstralParty.proto — merged wire schema
dump/                     reverse-engineering output (decompiled C#, DLLs)
```

## Testing

```bash
cd Server
go vet ./...
go test ./...
go run ./internal/smokeclient -addr 127.0.0.1:8800 -nick player-a
```

## License

For research and private-server education. AstralParty and all related
assets belong to their respective owners. See LICENSE.
