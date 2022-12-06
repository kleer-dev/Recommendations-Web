import {Inject, Injectable} from '@angular/core';
import {BehaviorSubject, Observable, tap} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {DOCUMENT} from "@angular/common";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private _isUserAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isUserAuthenticated: Observable<boolean> = this._isUserAuthenticatedSubject.asObservable();

  constructor(@Inject(DOCUMENT) private document: Document, private httpClient: HttpClient) { }

  updateUserAuthenticationStatus(){
    return this.httpClient.get<boolean>(`api/home/check-auth`,
      {withCredentials: true}).pipe(tap(isAuthenticated => {
      this._isUserAuthenticatedSubject.next(isAuthenticated);
    }));
  }

  setUserAsNotAuthenticated(){
    this._isUserAuthenticatedSubject.next(false);
  }

  login(provider: string) {
    this.document.location.href = `api/user/external-login?provider=${provider}`;
  }

  logout() {
    this.document.location.href = "api/user/logout";
  }
}
