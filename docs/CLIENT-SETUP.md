# Connecting AstralParty 3.2.0 to the private server

This covers Android and PC. Pick whichever route matches your device situation; you only need one.

## 1. What the client actually talks to

Three things, verified by packet inspection:

| Endpoint | What it does | Protocol / port |
|---|---|---|
| `se-web-cn.feimogames.com` | Bootstrap. `/api/hotaddressExtend/get` and `/api/hotaddressServer/get` hand back the game TCP server address (`serverUrl`) and the hot-update CDN address (`sourceUrl`) | Plain HTTP, port 7878 |
| Game TCP server | The gameplay connection | Whatever host:port the bootstrap returned |
| `serescn.feimogames.com` | Hot-update CDN, serves catalog/version files like `/CN_ANDROID/3.2.0/001` | HTTPS, port 443 |

A few things worth knowing before you start:

- Bootstrap is plain HTTP. No TLS, no cert pinning. That means a hosts-file entry or DNS override is enough to redirect it; no CA installation anywhere.
- The CDN is HTTPS on the official servers. We get around this by pointing it at a plain HTTP file server (details below).
- There's also a log-reporting host, `se-client-log-cn.feimogames.com:8810`. The private server can ignore it or answer with anything. It doesn't affect gameplay.

The bootstrap response is the linchpin of the whole setup. A trimmed-down example of what the client expects:

```json
{
  "serverUrl": "YOUR_IP:9000",
  "sourceUrl": "http://prv-dl.astralparty.lan/CN_ANDROID/3.2.0/001"
}
```

Whatever host and port you put in `serverUrl` is where the client opens its TCP game connection, so you have full control over that leg without touching the client. Same for `sourceUrl`: the client downloads `catalog.json` and the version files from whatever URL you hand it. As long as your bootstrap answers both endpoints with sensible JSON, the client is happy.

If you're implementing the bootstrap yourself, the minimum viable handler looks like this (the actual server code in this repo already does this, this is just to show the shape):

```go
func bootstrapHandler(w http.ResponseWriter, r *http.Request) {
    json.NewEncoder(w).Encode(map[string]string{
        "serverUrl": cfg.GameTCPAddr,
        "sourceUrl": cfg.HotUpdateBaseURL,
    })
}
```

Both endpoints (`/api/hotaddressExtend/get` and `/api/hotaddressServer/get`) return the same shape, and the client accepts either one.

### The two private hostnames

The patched APK carries two rewritten domains inside `global-metadata.dat` (same-length replacements, so the file layout is untouched):

| Original | Rewritten to | Points at |
|---|---|---|
| `se-web-cn.feimogames.com` | `prv-web.astralparty.lan` | Bootstrap HTTP server, port 7878 |
| `serescn.feimogames.com` | `prv-dl.astralparty.lan` | Hot-update file server |

Whatever route you pick, these two names have to resolve to the server. Section 4 has the DNS setup.

## 2. Android

### Option A: the patched APK (recommended)

Install `AstralParty-3.2.0-private.patched.apk` (357 MB, signed and zipaligned).

1. Uninstall the official app first. The patched build is signed with a different key, so installing over the official one fails with a signature mismatch.
2. Install the patched APK (`adb install AstralParty-3.2.0-private.patched.apk`, or just sideload the file).
3. Make DNS resolve the two `.lan` names to the server:
   - `prv-web.astralparty.lan` (the server must answer plain HTTP on 7878)
   - `prv-dl.astralparty.lan` (hot-update files)

No root, no permissions beyond the game's own, works on any device, survives reboots.

If you'd rather use different hostnames, you can rebuild the APK yourself. The replacements must be the same length or shorter (the string literals are length-delimited and NUL-padded), so a 24-char and a 22-char name are the natural fits. `foo.example.com.` with a trailing dot is a valid FQDN if you need padding. The procedure:

```bash
# Tools: python3, Android build-tools (zipalign + apksigner), JDK keytool
python3 - <<'EOF'
import zipfile
SRC='AstralParty-3.2.0.apk'; OUT='patched-unsigned.apk'
META='assets/bin/Data/Managed/Metadata/global-metadata.dat'
OLD=[(b'se-web-cn.feimogames.com', b'prv-web.astralparty.lan'),
     (b'serescn.feimogames.com',   b'prv-dl.astralparty.lan')]
z=zipfile.ZipFile(SRC); meta=z.read(META)
for o,n in OLD:
    assert len(o)==len(n) and meta.count(o)>0
    meta=meta.replace(o,n)
with zipfile.ZipFile(OUT,'w',zipfile.ZIP_DEFLATED) as zo:
    for it in z.infolist():
        d = meta if it.filename==META else z.read(it.filename)
        zi=zipfile.ZipInfo(it.filename, it.date_time); zi.compress_type=it.compress_type
        zi.external_attr=it.external_attr
        zo.writestr(zi,d)
EOF
zipalign -f -p 4 patched-unsigned.apk patched-aligned.apk
keytool -genkeypair -keystore private.keystore -alias private -keyalg RSA \
  -keysize 2048 -validity 10000 -storepass astralparty -keypass astralparty \
  -dname "CN=AstralParty Private"
apksigner sign --ks private.keystore --ks-pass pass:astralparty \
  --key-pass pass:astralparty --ks-key-alias private \
  --out AstralParty-3.2.0-private.patched.apk patched-aligned.apk
apksigner verify --print-certs AstralParty-3.2.0-private.patched.apk
```

The whole patch lives in `global-metadata.dat`. The native library (`lib/arm64-v8a/libil2cpp.so`) doesn't contain any of these strings, so there's nothing else to touch, and `AndroidManifest.xml` stays as-is. After patching, verify with `grep -c` on the extracted metadata: you should see 3 occurrences of each new hostname and zero of the old ones.

### Option B: rooted device, keep the official APK

If you'd rather not repack anything, root lets you redirect the original domains instead:

| Original domain | Map to |
|---|---|
| `se-web-cn.feimogames.com` | Server IP |
| `serescn.feimogames.com` | Server IP |

Any of these works:

- Edit `/system/etc/hosts` directly (remount read-write first) and add one line: `YOUR_IP se-web-cn.feimogames.com serescn.feimogames.com`. Simplest option, no extra apps.
- Magisk users: the "Systemless Hosts" module does the same thing without touching `/system`.
- If you'd rather not override DNS, iptables DNAT also works, e.g.
  `iptables -t nat -A OUTPUT -p tcp -d <official-ip> --dport 7878 -j DNAT --to-destination <your-ip>:7878`, plus a second rule for port 443 for the CDN.

The catch with Option B is the CDN: the official client builds `https://serescn.feimogames.com/...` URLs, so your file server has to answer HTTPS on 443 with a certificate the device trusts. Android 7+ doesn't trust user-installed CAs by default, and this game doesn't opt in, so a self-signed cert will not fly on a stock device. If your file server can't do trusted HTTPS, use Option A (or the host-patching variant in Option B below: some rooted setups find it easier to just add `YOUR_IP prv-dl.astralparty.lan` to hosts and accept that the official client's HTTPS call fails while waiting for the patched one, but honestly that path is more trouble than it's worth — go with Option A).

### Option C: no root, no patched APK

A local-VPN hosts app (RethinkDNS, NetGuard, personalDNSfilter) can do the mapping:

1. Add A-records for `se-web-cn.feimogames.com` and `serescn.feimogames.com`, both pointing at the server IP.
2. Route the game through the app.

This keeps the official client untouched, but it has two costs: the app occupies the device's one VPN slot, and it only changes DNS, so your private bootstrap has to listen on 7878 and the file server on 443, same ports as the official setup. Same HTTPS caveat as Option B applies to the CDN leg.

(Xposed/LSPosed interception and iptables redirects need root; don't try them on an unrooted device.)

## 3. PC

Same Unity IL2CPP stack as the phone client. Options, roughly in order of effort:

1. **Hosts file.** Add both original hostnames to `C:\Windows\System32\drivers\etc\hosts`:
   ```
   YOUR_IP se-web-cn.feimogames.com
   YOUR_IP serescn.feimogames.com
   ```
   That covers the bootstrap completely, since it's plain HTTP on 7878. The CDN call is the one catch: it's HTTPS. Either run your file server with a cert Windows trusts (much easier than on Android; any CA in the Windows store works, including your own if you add it), or hex-edit the PC's `global-metadata.dat` and change the `https://` prefix to `http://` (same length, so nothing shifts).
2. **DNS proxy.** If editing the hosts file is a problem (managed machines, group policy), a local DNS proxy with host mapping does the same job. Acrylic DNS Proxy and Simple DNS Plus both work.
3. **mitmproxy.** Rewrites the requests without touching the client at all:
   ```bash
   mitmproxy --mode transparent --listen-port 8080 \
     --map-remote "|~u ^http://se-web-cn.feimogames.com:7878/(.*)|http://YOUR_IP:7878/\1"
   ```
   Point the client at the proxy through Windows proxy settings or Proxifier. Since the bootstrap is plaintext HTTP you don't need to install mitmproxy's CA for it. You would need it for HTTPS CDN calls, so patch to HTTP or serve a trusted cert to avoid that.
4. **Patch the binary.** The `global-metadata.dat` procedure used for the Android build works on the PC copy of the file too. Same strings, same offsets-ish (the PC metadata is a different build, so re-run the search rather than reusing offsets; the string contents are identical). Find the file under the install directory, patch both hostnames same-length, and the client behaves exactly like the patched phone APK.

One quirk we noticed: the official bootstrap server accepts a TLS handshake on port 7878 as well, but the client always speaks plaintext to it. A plain HTTP implementation on your side is fine.

## 4. DNS on the server side

Run dnsmasq or any DNS server that answers these names with the server IP:

| Hostname | Needed by | Port |
|---|---|---|
| `prv-web.astralparty.lan` | Patched APK (Option A) | 7878, plain HTTP |
| `prv-dl.astralparty.lan` | Patched APK (Option A) | hot-update files (HTTP recommended) |
| `se-web-cn.feimogames.com` | Root/hosts routes (Option B, PC) | 7878 |
| `serescn.feimogames.com` | Root/hosts routes (Option B, PC) | 443 |

dnsmasq config:

```
address=/prv-web.astralparty.lan/<SERVER_IP>
address=/prv-dl.astralparty.lan/<SERVER_IP>
address=/se-web-cn.feimogames.com/<SERVER_IP>
address=/serescn.feimogames.com/<SERVER_IP>
```

Hand this DNS server out to client devices, either directly or through the router's DHCP. Devices using the patched APK need to resolve the `.lan` names, so they must actually use this DNS (or carry static entries).

## 5. Hot-update files

The hot-update tree lives under `HOTUPDATE_ROOT` on the server. `prv-dl.astralparty.lan` (and `serescn.feimogames.com` for hosts-based routes) both need to reach it.

The layout the client expects mirrors the official CDN:

```
HOTUPDATE_ROOT/
  CN_ANDROID/
    3.2.0/
      001/
        catalog.json
        <version files...>
```

The client fetches `catalog.json` first, compares checksums against what it has locally, then downloads whatever changed. The catalog's md5 in the shipped assets is `9c6d144652612aac566a0fbfebd9f3bf`, which matches the official file, so if you're serving the extracted hot-update assets as-is the client will find everything it expects. Any static file server works here (nginx, caddy, a plain Go `http.FileServer`); just make sure it sends `Content-Length` correctly, since the client uses it for progress display.

If you modify game assets and regenerate the catalog, update the md5 wherever it's checked; the client validates the catalog before trusting the rest of the tree.

## 6. Troubleshooting

A few things that go wrong most often, in the order we usually check them:

- **Client hangs at the loading screen right after launch.** The bootstrap call didn't get through. From the device's network, `curl -v http://SERVER_IP:7878/api/hotaddressExtend/get` should return JSON. If it doesn't, DNS or firewall is the problem, not the game.
- **Client errors out after bootstrap, before login.** Bootstrap worked, the TCP game server didn't answer. Double-check that the host:port you return in `serverUrl` is reachable from the device, and that the server is actually listening on it.
- **Hot-update fails with a network error partway.** Usually the CDN hostname isn't resolving (patched APK: `prv-dl.astralparty.lan`; hosts route: `serescn.feimogames.com`). Check both against dnsmasq.
- **Hot-update downloads but the game still fails to start.** The catalog or the files under it don't match what the client expects. Compare against the extracted assets.
- **"App not installed" when updating from the official build.** Signature mismatch; uninstall the official app first (Option A step 1). Save data will be lost, so back up anything you care about first.
- **Works on WiFi, fails on mobile data.** The device's DNS on mobile data isn't yours. Either use the patched APK with hosts entries on-device, or accept that this setup is LAN/WiFi-only.

## 7. Quick reference

| Thing | Value |
|---|---|
| Bootstrap endpoints | `/api/hotaddressExtend/get`, `/api/hotaddressServer/get` on port 7878, plain HTTP |
| Patched APK | `AstralParty-3.2.0-private.patched.apk`, 357 MB |
| Private bootstrap host | `prv-web.astralparty.lan` |
| Private hot-update host | `prv-dl.astralparty.lan` |
| Game TCP | Host/port from bootstrap `serverUrl` |
| Hot-update root | `HOTUPDATE_ROOT` on the server |
| Catalog md5 | `9c6d144652612aac566a0fbfebd9f3bf` |

Last detail: if you want the whole setup certificate-free, the file server can serve plain HTTP, but note the patched APK's metadata still carries the `https://` prefix literal in front of the rewritten CDN hostname. Either patch that literal to `http://` too (it's the same length, so the swap is trivial), or put a client-trusted cert on the file server. One or the other, not both.
