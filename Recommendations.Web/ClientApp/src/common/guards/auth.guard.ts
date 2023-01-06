import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {UserService} from "../services/user/user.service";
import {first, Observable, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
              private http: HttpClient,
              private userService: UserService) {

  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean> | Promise<boolean> | boolean {
    return this.userService.checkAuthentication().pipe(tap(isAuth => {
      if (!isAuth){
        this.router.navigate(['/login']);
      }
      this.userService.isAuthenticated = true
    }), first())
  }
}
