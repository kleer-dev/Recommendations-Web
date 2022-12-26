import {BrowserModule} from '@angular/platform-browser';
import {NgModule, SecurityContext} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {RegistrationComponent} from "./registration/registration.component";
import {LoginComponent} from "./login/login.component";
import {ThemeToggleComponent} from "./theme-toggle/theme-toggle.component";

import {NgbRatingModule} from '@ng-bootstrap/ng-bootstrap';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ExternalLoginComponent} from "./external-login/external-login.component";
import {LoginCallbackComponent} from "./auth-callback/login-callback.component";
import {CreateReviewComponent} from "./create-review/create-review.component";
import {NgxDropzoneModule} from "ngx-dropzone";
import {TagInputModule} from 'ngx-chips';
import {NgxTagsInputBoxModule} from "ngx-tags-input-box";

import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MarkdownEditorModule} from "./markdown-editor/markdown-editor.module";
import {MarkdownModule, MarkedOptions} from 'ngx-markdown';
import {ReviewFormModule} from "./review-form/review-form.module";
import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {HttpLoaderFactory} from "../common/functions/httpLoaderFactory";
import {LanguageDropdownComponent} from "./language-dropdown/language-dropdown.component";
import {SearchComponent} from "./search/search.component";
import {ReviewComponent} from "./review/review.component";
import {FullscreenLoaderComponent} from "./loaders/fullscreen-loader/fullscreen-loader.component";
import {DataLoaderComponent} from "./loaders/data-loader/data-loader.component";
import {UserPageComponent} from "./user-page/user-page.component";
import {UpdateReviewComponent} from "./update-review/update-review.component";
import {NgxDatatableModule} from '@swimlane/ngx-datatable';
import {AuthInterceptor} from "../common/interceptors/auth.interceptor";
import {AuthGuard} from "../common/guards/auth.guard";
import {LogoutComponent} from "./logout/logout.component";
import {AdminPageComponent} from "./admin-page/admin-page.component";
import {RoleGuard} from "../common/guards/admin-role.guard";
import {SearchPageComponent} from "./search-page/search-page.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    ThemeToggleComponent,
    LoginCallbackComponent,
    ExternalLoginComponent,
    CreateReviewComponent,
    LanguageDropdownComponent,
    SearchComponent,
    ReviewComponent,
    FullscreenLoaderComponent,
    DataLoaderComponent,
    UpdateReviewComponent,
    UserPageComponent,
    LogoutComponent,
    AdminPageComponent,
    SearchPageComponent
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
      {path: 'create-review', component: CreateReviewComponent, canActivate: [AuthGuard]},
      {path: 'create-review/:userid', component: CreateReviewComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'update-review/:id', component: UpdateReviewComponent, canActivate: [AuthGuard]},
      {path: 'review/:id', component: ReviewComponent},
      {path: 'admin-profile', component: AdminPageComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'logout', component: LogoutComponent},
      {path: 'profile', component: UserPageComponent, canActivate: [AuthGuard]},
      {path: 'profile/:userid', component: UserPageComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'search/:search-query', component: SearchPageComponent}
    ]),
    NgbModule,
    ReviewFormModule,
    NgbRatingModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    TagInputModule,
    NgxTagsInputBoxModule,
    BrowserAnimationsModule,
    MarkdownEditorModule,
    NgxDatatableModule,
    MarkdownModule.forRoot(({
      markedOptions: {
        provide: MarkedOptions,
        useValue: {
          gfm: true,
          breaks: true,
          pedantic: false,
          smartLists: true,
          smartypants: true
        },
      },
      sanitize: SecurityContext.NONE
    })),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
      useDefaultLang: false,
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}


