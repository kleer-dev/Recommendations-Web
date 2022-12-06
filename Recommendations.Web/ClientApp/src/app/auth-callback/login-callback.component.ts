import {Component, Inject} from '@angular/core';
import {Subscription} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "../services/account/account.service";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";
import {DOCUMENT} from "@angular/common";
import {LoginComponent} from "../login/login.component";

@Component({
  selector: 'app-login-callback',
  templateUrl: 'login-callback.component.html',
})

export class LoginCallbackComponent {
  isUserAuthenticated = false;
  subscription?: Subscription;
  message?: string
  error?: string;

  constructor(private http: HttpClient, private accountService: AccountService,
              @Inject(DOCUMENT) private document: Document, private router: Router) {

    this.http.get('api/user/external-login-callback').subscribe({
      next: _ => this.router.navigate(['/']),
      error: error => this.error = error
    });
    this.checkStatus()
  }

  checkStatus() {
    this.subscription = this.accountService.isUserAuthenticated.subscribe(isAuthenticated => {
      this.isUserAuthenticated = isAuthenticated;
      if (this.isUserAuthenticated) {
        this.message = "Successfully authenticated"
        this.document.location.href = "/";
      } else {
        this.message = "Failed authenticated"
      }
    });
  }
}
