#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "$0")"

sass "wwwroot/assets/scss/main.scss:wwwroot/assets/css/main.css" --source-map
