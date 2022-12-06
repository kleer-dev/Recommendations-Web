import {BrowserModule} from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {RegistrationComponent} from "./registration/registration.component";
import {LoginComponent} from "./login/login.component";
import {ThemeToggleComponent} from "./theme-toggle/theme-toggle.component";

import {SocialLoginModule, SocialAuthServiceConfig} from '@abacritt/angularx-social-login';
import {AccountService} from "./services/account/account.service";
import {Interceptor401Service} from "./services/interceptor/interceptor401.service";
import {checkIfUserIsAuthenticated} from "./services/check-login-intializer";
import {ExternalLoginComponent} from "./external-login/external-login.component";
import {LoginCallbackComponent} from "./auth-callback/login-callback.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    ThemeToggleComponent,
    LoginCallbackComponent,
    ExternalLoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'registration', component: RegistrationComponent},
      {path: 'login', component: LoginComponent},
      {path: 'login-callback', component: LoginCallbackComponent},
    ]),
    ReactiveFormsModule,
    SocialLoginModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: Interceptor401Service, multi: true },
    { provide: APP_INITIALIZER, useFactory: checkIfUserIsAuthenticated, multi: true, deps: [AccountService]}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
