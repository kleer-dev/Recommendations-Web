import {BrowserModule} from '@angular/platform-browser';
import {NgModule, SecurityContext} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {RegistrationComponent} from "./registration/registration.component";
import {LoginComponent} from "./login/login.component";
import {ThemeToggleComponent} from "./theme-toggle/theme-toggle.component";

import { NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ExternalLoginComponent} from "./external-login/external-login.component";
import {LoginCallbackComponent} from "./auth-callback/login-callback.component";
import {CreateReviewComponent} from "./create-review/create-review.component";
import {NgxDropzoneModule} from "ngx-dropzone";
import { TagInputModule } from 'ngx-chips';
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
    ReviewComponent
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
      {path: 'create-review', component: CreateReviewComponent},
      {path: 'review', component: ReviewComponent}
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

  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}


