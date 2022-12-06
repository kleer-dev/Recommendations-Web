import { Component } from '@angular/core';
import {Subscription} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "../services/account/account.service";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";

@Component({
  selector: 'app-external-login',
  templateUrl: './external-login.component.html',
})
export class ExternalLoginComponent {

  constructor(private accountService: AccountService, private http: HttpClient) {

  }

  externalAuth(provider: string){
    this.accountService.login(provider);
  }
}
