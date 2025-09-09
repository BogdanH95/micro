Create certificate to be used by identity server,
and .crt file to be added to any clients in order to 
establish trust in development

Identity Certificates 

identityserver.cnf : 
--------------------------------------------
[req]
default_bits       = 2048
prompt             = no
default_md         = sha256
req_extensions     = req_ext
distinguished_name = dn

[dn]
CN = micro.identityserver

[req_ext]
subjectAltName = @alt_names

[alt_names]
DNS.1 = micro.identityserver
DNS.2 = localhost
--------------------------------------------

openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout identityserver.key \
    -out identityserver.crt \
    -config identityserver.cnf


openssl pkcs12 -export -out identityserver.pfx \
  -inkey identityserver.key \
  -in identityserver.crt \
  -password pass:YourPassword