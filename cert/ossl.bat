
..\openssl\openssl genrsa -des3 -out myCA\cakey.pem 1024
pause
..\openssl\openssl req -new -x509 -key myCA\cakey.pem -out myCA\cacert.pem -days 365 -config ..\openssl\openssl.cnf
pause