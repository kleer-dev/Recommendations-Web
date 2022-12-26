import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {UserService} from "../services/user/user.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private router: Router,
              private http: HttpClient,
              private userService: UserService) {

  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.userService.getRole()
      .subscribe({
        next: value => {
          if (!value)
            this.router.navigate(['/']);
        }
      })
    return this.userService.getRole()
  }
}
