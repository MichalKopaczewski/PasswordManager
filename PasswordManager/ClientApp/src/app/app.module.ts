import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NgxSpinnerModule, NgxSpinnerService } from "ngx-spinner";
import { JwtModule } from '@auth0/angular-jwt';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ToastrModule } from 'ngx-toastr';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthService } from './services/auth.service';
import { LocalStorageService } from './services/local-storage-service';
import { LoginComponent } from './login/login.component';
import { appRoutes } from './routes';
import { CounterComponent } from './counter/counter.component';
import { StorageServiceModule } from 'angular-webstorage-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserService } from './services/users.service';
import { ConfirmationDialogService } from './confirmation-dialog/confirmation-dialog.service';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { UsersDetailsComponent } from './users/users-details/users-details.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserNewComponent } from './users/user-new/user-new.component';
import { UserRolesComponent } from './users/user-roles/user-roles.component';
import { UserUpdateComponent } from './users/user-update/user-update.component';
import { UserDetailResolver } from './resolvers/user-detail.resolver';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
// export function tokenGetter() {
//   return localStorage.getItem("token");
// }
export function tokenGetter(): string {
  //return localStorageService.getFromLocalStorage<string>(LocalStorageService.TOKEN,'');
  const token = localStorage.getItem("token");
  if (token === null) return "";
  return token.substring(1, token.length - 1);
}
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    UserNewComponent,
    UserRolesComponent,
    UserListComponent,
    UsersDetailsComponent,
    UserUpdateComponent,
    CounterComponent,
    ConfirmationDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    StorageServiceModule,
    NgbModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    NgxSpinnerModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ["localhost:44376", 'localhost'],
        disallowedRoutes: ["example.com/examplebadroute/"]
      }
    }),
    NgxPermissionsModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
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
  providers: [
    AuthService,
    NgxSpinnerService,
    LocalStorageService,
    UserService,
    UserDetailResolver,
    ConfirmationDialogService
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
