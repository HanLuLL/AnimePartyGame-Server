# AstralParty 3.2.0 Official APK Reverse-Engineering Analysis

APK: `/home/agentuser/Downloads/AstralParty-3.2.0.apk` (357MB, official release build)
Analysis method: read directly with python zipfile (resources not extracted to disk); AndroidManifest is binary AXML, parsed with a hand-written parser to extract attributes. No server was contacted.

## 1. Basic Info and Package Structure

- Package name: `com.feimo.astralparty`
- versionCode: **3200**, versionName: **3.2.0**
- minSdk: 26 (Android 8.0), targetSdk: 35, compileSdk: 31 (Android 12)
- 482 total entries: res 362, assets 75, lib 15, dex×4 + resources.arsc
- arm64-v8a only (no armeabi-v7a / x86)

### .so files >1MB under lib/

| File | Size |
|---|---|
| **libil2cpp.so** | **72,019,896 (68.7 MB)** |
| libunity.so | 19,871,376 |
| libAkSoundEngine.so (Wwise) | 3,820,008 |
| libtapsdkcore.so (TapTap) | 4,217,120 |
| libcri_ware_unity.so (CRI Middleware) | 2,768,008 |

Unity 2021.3 IL2CPP project (HybridCLR hot-update runtime, URP, Cinemachine, DOTween, EasySave3, Google.Protobuf).

### AndroidManifest permissions (uses-permission, 22 items)

INTERNET, ACCESS_NETWORK_STATE, ACCESS_WIFI_STATE, CHANGE_NETWORK_STATE, CHANGE_WIFI_STATE,
READ_PHONE_STATE, READ_EXTERNAL_STORAGE, WRITE_EXTERNAL_STORAGE, MOUNT_UNMOUNT_FILESYSTEMS,
ACCESS_FINE_LOCATION, ACCESS_COARSE_LOCATION, RECORD_AUDIO, BLUETOOTH, VIBRATE, GET_TASKS,
READ_LOGS, SYSTEM_ALERT_WINDOW, WRITE_SETTINGS, REQUEST_INSTALL_PACKAGES,
com.asus.msa.SupplementaryDID.ACCESS, freemme.permission.msa, oplus.permission.settings.LAUNCH_FOR_EXPORT

Key components: `com.ibingniao.unity.BnUnityGameActivity` (BnSdk main entry),
`prj.iyinghun.platform.sdk.SplashScreenActivity` (Yinghun / iyinghun SDK splash screen),
TapTap login (com.taptap.sdk.login), WeChat wxapi (WXEntryActivity/WXPayEntryActivity/WXminiResultActivity),
Alipay (com.alipay.sdk.app.*), flash one-tap login / China Mobile one-tap login (com.chuanglan.shanyan_sdk, com.cmic.gen.sdk),
TapTapKitInitProvider, fileProvider, themisLite (anti-cheat).

## 2. assets Structure

- `assets/bin/Data/` — Unity data: `data.unity3d` (32.6MB), `unity default resources` (4.2MB),
  `Managed/Metadata/global-metadata.dat` (8,635,540 bytes), `RuntimeInitializeOnLoads.json`,
  `ScriptingAssemblies.json`, `boot.config`
- `assets/aa/` — **Addressables assets** (48 .bundle files under Android/, ~300MB total, largest single file 38.4MB) +
  `catalog.bundle` + `settings.json` + `AddressablesLink/link.xml`
  (in-game content is served via Addressables and the full set of bundles is already included locally; there is no separate hotupdate directory — hot-update data is handled by the HotUpdateConfig/HotUpdateConfigData logic in code)
- `assets/bnsdk/config/` — BnSdk (ibingniao "Bi'an" SDK) configuration:
  - `sdkVersion.txt` = 3577
  - `env/env_config.json` (UI config such as floating window position; no server addresses)
  - `plugin_info.json` (versionCode 4, wxloginForceUpdate [])
  - `ry_sdk_config.json`: AppKey `6cda0b22900b7265`
  - `bn_protocol.txt`/`bn_h5_protocol.txt`, `bn_service.txt`, `bn_child.txt`, `bn_hide.txt`: all privacy policy / terms of service / child protection text (no server configuration)
- `assets/supplierconfig.json` — channel appids: vivo `100215079` (xiaomi/huawei/oppo empty)
- `assets/cucc/host_cucc.properties` — China Mobile/Unicom pass-through number fetching: `PRODUCE_DZH=https://msv6.wosms.cn`
  (comments mention a test environment `m.zzx.cnklog.com` and Guangdong Unicom `ms.zzx9.cn`)
- `assets/yhsdk/config/` (BN_SDK_VERSION 3), `assets/zlsioh.dat` (56KB, encrypted data blob)

## 3. BnSdk / Third-Party Server Address List (from dex + resources.arsc strings)

**Feimoo Games' own SDK domains (feimogames.com, the game's act/m-sdk layer):**
- `https://act-sdk.feimogames.com` — events/operations SDK
- `https://m-sdk.feimogames.com` — main SDK interface
- `https://heartbeat-sdk.feimogames.com` — heartbeat
- `https://tj-sdk.feimogames.com` — analytics reporting

**TapTap SDK (xdrnd.com is a TapTap backup domain):**
- accounts.taptap.cn / accounts-io.xdrnd.com / accounts.xdrnd.cn / www.taptapauth.com (OAuth authorize)
- tapsdk.tapapis.cn / tapsdk.tapapis.com / tapsdk.api.xdrnd.cn / tapsdk.api.xdrnd.com
- gid.tapapis.cn(.com) / gid.api.xdrnd.cn(.com)
- e.tapdb.net / e.tapdb.ap-sg.tapapis.com (TapDB data)

**Other third parties (all bundled with SDKs, not game servers):**
Payments: alipay.com family (mobilegw.alipay.com etc.); WeChat: open.weixin.qq.com / long.open.weixin.qq.com;
one-tap login / number fetching: sy.cl2009.com, sysdk.cl2009.com (Shanyan/Chuanglan), msv6.wosms.cn (China Unicom), cmpassport.com (China Mobile),
opencloud.wostore.cn (China Telecom), api-e189.21cn.com / e.189.cn;
Analytics: Bugly (android.bugly.qq.com, astat.bugly.qcloud.com), Baidu ocpc, Kuaishou api.e.kuaishou.com,
tj.xiaotengyouxi.com (Xiaoteng analytics); real-name/token: token.aiyinghun.com, oauth.aiyinghun.com (Yinghun platform).

No plaintext URLs inside libil2cpp.so (strings are stored via IL2CPP metadata, see below).

## 4. global-metadata.dat Comparison (in-APK vs repo dump/dll)

In-APK metadata MD5 `9daad001328204832c62bfdeb6235623`,
repo dump/dll/global-metadata.dat MD5 `f5d7f6a6c6efc6cd8200564ea0b24aba` — **contents differ** (same 8,635,540 bytes).

Key finding: **the same set of server strings in the two files is a mirror swap of "production domain vs internal/official-server domain":**

| Purpose | In APK (feimogames.com family) | Repo dump (lxii.cc family) |
|---|---|---|
| Main server | `https://serescn.feimogames.com` | `https://serescn-server.lxii.cc` |
| Web service | `se-web-cn.feimogames.com` | `se-web-cn-server.lxii.cc` |
| Client logs (port :8810) | `se-client-log-cn.feimogames.com:8810` | `se-client-log-cn-server.lxii.cc:8810` |

The context also contains `192.168.101.193` (development-environment internal IP) and `https://www.baidu.com` (connectivity-test fallback).

Conclusions:
- The known lxii.cc family (serescn-server.lxii.cc / se-web-cn-server.lxii.cc / se-client-log-cn-server.lxii.cc:8810) are indeed the official server addresses, matching the repo dump; recorded only, not connected.
- feimogames.com is the mirror domain of the same string table in the release-channel build; the two domain sets correspond exactly (serescn ↔ serescn-server) and can serve as a naming reference for private-server deployment.
- Client protocol layer: the full Google.Protobuf + proto/AstralParty.proto protocol is already in the repo; hot updates go through HybridCLR + Addressables (local bundles under assets/aa + in-code HotUpdateConfig).

## 5. Private-Server Setup Notes

- Player version 3.2.0 (versionCode 3200) has metadata whose version strings differ from the repo dump, but the domain mapping is one-to-one, indicating the dump's decompiled artifacts belong to the same generation as this release build.
- The main server address `serescn.feimogames.com` is embedded in the il2cpp metadata (global-metadata.dat); a private server can redirect it by rewriting metadata strings or via hosts hijacking.
- The login chain involves TapTap/BnSdk/Yinghun tokens (token.aiyinghun.com/user/token); a private server needs to bypass or emulate the BnSdk login response on the client side.
