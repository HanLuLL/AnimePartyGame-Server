#!/usr/bin/env bash
set -euo pipefail

gh workflow run codegen.yml --repo HanLuLL/AstralParty --ref main
