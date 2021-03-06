MultiSPA
------
Boilerplate code for hosting one or more angular apps inside a sinlge ASP.Net core 3.0 MVC solution. Based on this great article: https://www.infoq.com/articles/Angular-Core-3/

Simple ASP.Net Core 3.0 / Angular 8 starter
------

The Visual Studio solution is meant to be a starter for new projects with the need for separate frontend and backend code. The frontend is done 
with ASP.Net MVC to deliver fast and search engine friendly code that is rendered on the server. The backend (or any other more sophosticated part)
is outlined to be done in Angular.

Key features
------
- Can include multiple Angular apps
- Angular apps are hosted within a MVC view
- Angular apps are structured within a base app to deliver reusable components
- The components are even exported as web components so they can be used inside the MVC part
- Includes ASP.Net Identity for authentication
- Angular publishing is part of the regular project publish task - but remember: [Friends don't let friends right-click publish  ](https://damianbrady.com.au/2018/02/01/friends-dont-let-friends-right-click-publish/)

Usage
------
The MVC part can be started as usual (F5). Each Angular app (currently the base app and the single demo app) needs to be started separately. Start two command line windows in the directory "Apps".

For the base app type:
```
ng serve --liveReload=false
```
For the demo app1 type:
```
ng serve App1 --port 4201 --servePath / --baseHref /apps/app1/ --publicHost http://localhost:4201
```

Live Reload
------
By default the MVC site is served via https. If you want to use https on the main site, the angular content needs to be served via https as well in order to make things like live reload working.
Follow the steps outlined in the LocalCertificate/ folder's readme on how to create a self-signed certificate that will be trusted by chrome (at least version 78)

Uee find & replace to change all links from http://localhost to https://localhost

After the certificate has been installed, you can serve both angular parts with:
```
ng serve --liveReload=false --publicHost https://localhost:4200 --ssl --ssl-key ..\..\LocalCertificate\localhost.key --ssl-cert ..\..\LocalCertificate\localhost.crt
```
and
```
ng serve App1 --port 4201 --servePath / --baseHref /apps/app1/ --publicHost https://localhost:4201 --ssl --ssl-key ..\..\LocalCertificate\localhost.key --ssl-cert ..\..\LocalCertificate\localhost.crt
```
