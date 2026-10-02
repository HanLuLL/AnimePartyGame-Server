# AstralParty Hot-update Audio Extraction Report

Source: `http://serescn.feimogames.com/CN_ANDROID/3.2.0/001` (route=CN_ANDROID rev001, catalog MD5 matches APK catalog `9c6d144652612aac566a0fbfebd9f3bf`).

## Output
- `ogg/` — 2338 files, `{Bank}_{WwiseID}.ogg` (converted from Wwise wem; Vorbis via ww2ogg with aoTuV 6.03 codebooks, Opus via vgmstream). 258 MB.
- `bnk/` — 51 Wwise banks (BKHD/DIDX/HIRC/DATA), extracted from bundle MonoBehaviour raw data.
- `wem/` — 2338 intermediate wem files.

## Verification
- 40/40 random ogg decode OK (ffmpeg).
- Waveform correlation vs vgmstream native decode: 9/9 files corr=1.0000 (includes Vorbis + Opus).

## Bank list
See `_banks.json` (51 banks: Hero_101..Hero_130, BATTLE_*, UISFX, ROLE_COMMON, NPC, HOME, etc.)

## Tools
- ww2ogg 0.24 + `packed_codebooks_aoTuV_603.bin` (Wwise Vorbis uses aoTuV 6.03 variant codebooks; default codebooks produce noise!)
- vgmstream-cli (built with `make VGM_VORBIS=1`)
