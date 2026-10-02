#!/bin/sh
set -eu

# Certbot exports RENEWED_LINEAGE when this deploy hook runs.
: "${RENEWED_LINEAGE:?Certbot must provide RENEWED_LINEAGE}"

# Only the dedicated IP certificate belongs to this Caddy site.
if [ "$RENEWED_LINEAGE" != "/etc/letsencrypt/live/astralparty-ip" ]; then
	exit 0
fi

CADDY_GROUP="${CADDY_GROUP:-caddy}"

# Copy out of Certbot's root-only directories with read access for Caddy only.
install -d -o root -g "$CADDY_GROUP" -m 0750 /etc/caddy/certs
install -o root -g "$CADDY_GROUP" -m 0640 "$RENEWED_LINEAGE/fullchain.pem" /etc/caddy/certs/fullchain.pem
install -o root -g "$CADDY_GROUP" -m 0640 "$RENEWED_LINEAGE/privkey.pem" /etc/caddy/certs/privkey.pem

# Install this script under Certbot's deploy-hooks directory.
systemctl reload caddy
