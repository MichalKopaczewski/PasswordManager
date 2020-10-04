import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
export function tokenGetter() {
  return localStorage.getItem("token");
}
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    NgxSpinnerModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ["localhost:44374", 'localhost'],
        blacklistedRoutes: ["example.com/examplebadroute/"]
      }
    }),
    NgxPermissionsModule.forRoot(),
    FormsModule,
    NgxWebstorageModule.forRoot(),
    ToastrModule.forRoot({
      closeButton: true,
      autoDismiss: false,
      tapToDismiss: true,
      timeOut: 60000,
      countDuplicates: true,
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'decreasing'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
