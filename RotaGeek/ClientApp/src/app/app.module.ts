import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { ContactUsMessagesComponent } from './contact-us-messages/contact-us-messages.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ContactUsComponent,
    ContactUsMessagesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ContactUsComponent, pathMatch: 'full' },
      { path: 'contact-us-messages', component: ContactUsMessagesComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
