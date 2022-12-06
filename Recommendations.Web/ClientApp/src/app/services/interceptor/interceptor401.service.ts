import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {AccountService} from "../account/account.service";

@Injectable({
  providedIn: 'root'
})
export class Interceptor401Service {

  constructor(private accountService: AccountService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(tap(null,
      (error: HttpErrorResponse) => {
        if (error.status === 401)
          this.accountService.setUserAsNotAuthenticated();
      }));
  }

}
