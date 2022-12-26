import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {Router} from "@angular/router";
import {Roles} from "../../consts/Roles";
import {UserModel} from "../../models/UserModel";
import {RoleModel} from "../../models/RoleModel";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  isAuthenticated: boolean = false
  isAdmin: boolean = false

  constructor(private http: HttpClient, private router: Router) {

  }

  checkAuthentication(): Observable<boolean> {
    return this.http.get<boolean>('api/user/check-auth')
      .pipe(map((isAuthenticated) => {
        if (!isAuthenticated) {
          this.isAuthenticated = false
          // this.router.navigate(['/login'])
          return false;
        }
        this.isAuthenticated = true
        return true
      }));
  }

  getRole(): Observable<boolean> {
    return this.http.get<RoleModel>('api/user/get-role')
      .pipe(map((role) => {
        if (role.roleName !== Roles.admin) {
          this.isAdmin = false
          return false;
        }
        this.isAdmin = true
        return true;
      }))
  }

  checkRole(){
    this.getRole()
      .subscribe({
        next: value => {
          this.isAdmin = value
        }
      })
  }

  getAllUsers(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>('api/user/get-all-users')
  }

  logout() {
    this.http.post('api/user/logout', {})
      .subscribe({
        next: () => this.router.navigate(['/login'])
      })
  }
}
