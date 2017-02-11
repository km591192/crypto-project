
..\openssl\openssl x509 -req -days 365 -in reqIvan.pem -CA myCA\cacert.pem -CAkey myCA\cakey.pem -CAcreateserial -out Ivan.cert.pem
pause
..\openssl\openssl pkcs12 -export -in Ivan.cert.pem -inkey Ivan.pr.pem -out Ivan.cert.p12
pause