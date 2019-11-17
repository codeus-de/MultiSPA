1. install openssl for windows: https://slproweb.com/products/Win32OpenSSL.html (manual here: https://tecadmin.net/install-openssl-on-windows/)
1.1 create new system variable "OPENSSL_CONF" that points to the openssl.cfg file in the openssl bin directory
1.2 add openssl bin directory to the PATH system variable
2. configure the makeCERT.bat file to match requirements - it should be properly configured for localhost and works under chrome v78
3. run the makeCERT.bat. It will create localhost.cnf, localhost.crt and localhost.key
4. open mmc.exe, add snap in "certificates" (for local user) and import the .crt file into "Trusted Root Certification Authorities/Certificates" (context menu)
4.1 alternatively add the certificate to the trusted roots via chrome settings
5. to make angular (app1 on port 4201) use SSL run: ng serve App1 --port 4201 --servePath / --baseHref /apps/app1/ --publicHost https://localhost:4201 --ssl --ssl-key ..\..\LocalCertificate\localhost.key --ssl-cert ..\..\LocalCertificate\localhost.crt