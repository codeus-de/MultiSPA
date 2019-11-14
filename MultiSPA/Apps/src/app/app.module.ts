import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import { createCustomElement } from '@angular/elements';

import { AppComponent } from './app.component';
import { CurrentTimeComponent } from './current-time/current-time.component';

@NgModule({
  declarations: [
    AppComponent,
    CurrentTimeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [    
  ],
  entryComponents: [
    CurrentTimeComponent
  ],
  exports: [
    CurrentTimeComponent
  ]
})
export class AppModule {

  constructor(private injector: Injector) {
  }
  
  ngDoBootstrap(){
    customElements.define('current-time', createCustomElement(CurrentTimeComponent, {injector: this.injector}));
  }

 }
