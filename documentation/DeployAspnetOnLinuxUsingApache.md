:computer: *Utilisation d'Apache 2 pour déployer des applications ASP.NET avec .NET 6*

# Configure system services :

## ServiceConsumer.service :

```
[Unit]
Description=ASP.NET Consumer Application
[Service]
WorkingDirectory=/home/debian/release/var/netcore/consumer
ExecStart=/usr/bin/dotnet /home/debian/release/var/netcore/consumer/CashManager.Consumer.Api.dll
Restart=always
RestartSec=10
SyslogIdentifier=consumer
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
[Install]
WantedBy=multi-user.target
```

## ServiceBanking.service :

```
[Unit]
Description=ASP.NET Banking Application
[Service]
WorkingDirectory=/home/debian/release/var/netcore/banking
ExecStart=/usr/bin/dotnet /home/debian/release/var/netcore/banking/CashManager.Banking.Api.dll
Restart=always
RestartSec=10
SyslogIdentifier=netcore-demo
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://127.0.0.1:5001
[Install]
WantedBy=multi-user.target
```

# Configure Apache conf-enabled (using https) :

Utilisation du packet letsencrypt afin de générer un certificat SSL diférrent pour chaque VirutalHost.

```
<VirtualHost *:444>
   ServerName g24.epihub.eu

   SSLEngine on
   SSLCertificateFile /etc/letsencrypt/live/g24.epihub.eu/fullchain.pem
   SSLCertificateKeyFile /etc/letsencrypt/live/g24.epihub.eu/privkey.pem
   SSLCertificateChainFile /etc/letsencrypt/live/g24.epihub.eu/chain.pem

   ProxyPreserveHost On
   ProxyPass / http://127.0.0.1:5000/
   ProxyPassReverse / http://127.0.0.1:5000/

   RewriteEngine on
   RewriteCond %{HTTP:UPGRADE} ^WebSocket$ [NC]
   RewriteCond %{HTTP:CONNECTION} Upgrade$ [NC]
   RewriteRule /(.*) ws://127.0.0.1:5000/$1 [P,L]

   ErrorLog /var/log/apache2/netcore-error.log
   CustomLog /var/log/apache2/netcore-access.log common
</VirtualHost>

<VirtualHost *:445>
   ServerName g24.epihub.eu

   SSLEngine on
   SSLCertificateFile /etc/letsencrypt/live/g24.epihub2.eu/fullchain.pem
   SSLCertificateKeyFile /etc/letsencrypt/live/g24.epihub2.eu/privkey.pem
   SSLCertificateChainFile /etc/letsencrypt/live/g24.epihub2.eu/chain.pem

   ProxyPreserveHost On
   ProxyPass / http://127.0.0.1:5001/
   ProxyPassReverse / http://127.0.0.1:5001/

   RewriteEngine on
   RewriteCond %{HTTP:UPGRADE} ^WebSocket$ [NC]
   RewriteCond %{HTTP:CONNECTION} Upgrade$ [NC]
   RewriteRule /(.*) ws://127.0.0.1:5001/$1 [P,L]

   ErrorLog /var/log/apache2/netcore-error.log
   CustomLog /var/log/apache2/netcore-access.log common
</VirtualHost>
```
