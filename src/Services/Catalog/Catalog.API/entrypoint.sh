#!/bin/sh
set -e

# Copy the IdentityServer public cert (mounted as Docker secret) into the trusted store
if [ -f "/run/secrets/identityserver_cert_pub" ]; then
    cp /run/secrets/identityserver_cert_pub /usr/local/share/ca-certificates/identityserver.crt
    update-ca-certificates
    echo "IdentityServer certificate trusted"
else
    echo "No identityserver_cert_pub secret found, continuing without adding cert"
fi

# Run the API
exec dotnet Catalog.API.dll
