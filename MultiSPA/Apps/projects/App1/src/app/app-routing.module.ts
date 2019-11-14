import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router, NavigationEnd } from '@angular/router';
import { Page1Component } from './pages/page1/page1.component';
import { Page2Component } from './pages/page2/page2.component';

const routes: Routes = [
  {path: '', redirectTo: 'page1', pathMatch: 'full'},
  {path: 'page1', component: Page1Component},
  {path: 'page2', component: Page2Component}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

    constructor(private route: Router) {

        /*This code determines if the app is running in the iframe by comparing the href of the top window and of the current window.
         * If the app is running in the iframe, the code saves the HREF of the top window in a local variable, but trims the part of
         * the HREF to the right of the /app1 segment. Next, the code taps into the NavigationEnd event and appends the routed URL to
         * the top window's HREF.*/

        var topHref = window.top.location.href != window.location.href ?
            window.top.location.href.substring(0,
                window.top.location.href.indexOf('/app1') + 5) :
            null;
        this.route.events.subscribe(e => {
            if (e instanceof NavigationEnd) {
                if (topHref) {
                    window.top.history.replaceState(window.top.history.state,
                        window.top.document.title, topHref + e.url);
                }
            }
        });
    }

}
