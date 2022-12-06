import { Component } from '@angular/core';
import {Subscription} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "../services/account/account.service";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  isUserAuthenticated = false;
  subscription?: Subscription;
  userName?: string;

  constructor(private httpClient: HttpClient, private accountService: AccountService, private router: Router) {
  }

  ngOnInit() {
    this.subscription = this.accountService.isUserAuthenticated.subscribe(isAuthenticated => {
      this.isUserAuthenticated = isAuthenticated;
      if (this.isUserAuthenticated) {
        this.httpClient.get(`api/home/name`, { responseType: 'text', withCredentials: true }).subscribe(theName => {
          this.userName = theName;
        });
      }
    });
  }
}
