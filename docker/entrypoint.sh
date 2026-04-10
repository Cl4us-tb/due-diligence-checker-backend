#!/usr/bin/env bash
set -euo pipefail

WAIT_FOR_HOST="${WAIT_FOR_HOST:-}"
WAIT_FOR_PORT="${WAIT_FOR_PORT:-}"

if [[ -n "${WAIT_FOR_HOST}" && -n "${WAIT_FOR_PORT}" ]]; then
  echo "Waiting for ${WAIT_FOR_HOST}:${WAIT_FOR_PORT}..."
  for i in {1..60}; do
    if (echo >"/dev/tcp/${WAIT_FOR_HOST}/${WAIT_FOR_PORT}") >/dev/null 2>&1; then
      echo "Host is reachable."
      break
    fi
    sleep 2
  done
fi

DISPLAY_VALUE="${DISPLAY:-:99}"
export DISPLAY="${DISPLAY_VALUE}"

echo "Starting Xvfb on ${DISPLAY}..."
Xvfb "${DISPLAY}" -screen 0 1920x1080x24 -nolisten tcp -ac &

sleep 1

echo "Starting API..."
exec dotnet DueDiligenceChecker.dll

