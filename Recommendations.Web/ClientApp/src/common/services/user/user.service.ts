import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {Router} from "@angular/router";
import {Roles} from "../../consts/Roles";
import {UserModel} from "../../models/UserModel";
import {RoleModel} from "../../models/RoleModel";
import {FormGroup} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly baseUrl: string = "api/user"

  isAuthenticated: boolean = false
  isAdmin: boolean = false

  constructor(private http: HttpClient, private router: Router) {

  }

  checkAuthentication(): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/check-auth`)
      .pipe(map((isAuthenticated) => {
        if (!isAuthenticated) {
          this.isAuthenticated = false
          return false;
        }
        this.isAuthenticated = true
        return true
      }));
  }

  getRole(): Observable<boolean> {
    return this.http.get<UserModel>(`${this.baseUrl}/get-info`)
      .pipe(map((user) => {
        console.log(user)
        if (user.role !== Roles.admin) {
          this.isAdmin = false
          return false;
        }
        this.isAdmin = true
        return true;
      }))
  }

  checkRole() {
    this.getRole()
      .subscribe({
        next: value => {
          console.log(value)
          this.isAdmin = value
        }
      })
  }

  getAllUsers(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.baseUrl}/get-all-users`)
  }

  getUserInfo(): Observable<UserModel> {
    return this.http.get<UserModel>(`${this.baseUrl}/get-info`)
  }

  getUserInfoById(userId: number): Observable<UserModel> {
    return this.http.get<UserModel>(`${this.baseUrl}/get-info/${userId}`)
  }

  login(form: FormGroup): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, form.value,
      {
        headers: new HttpHeaders({
          'X-Skip-Interceptor': 'true'
        })
      });
  }

  registration(form: FormGroup): Observable<any> {
    return this.http.post(`${this.baseUrl}/registration`, form.value,
      {
        headers: new HttpHeaders({
          'X-Skip-Interceptor': 'true'
        })
      })
  }

  logout() {
    this.http.post(`${this.baseUrl}/logout`, {})
      .subscribe({
        next: () => this.router.navigate(['/login'])
      })
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${userId}`)
  }

  blockUser(userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/block/${userId}`, {})
  }

  unblockUser(userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/unblock/${userId}`, {})
  }

  setRole(userId: number, role: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/set-role`, {userId, role})
  }
}
