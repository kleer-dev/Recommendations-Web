import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  isAuthenticated: boolean = false

  constructor(private http: HttpClient, private router: Router) {
    this.checkAuthentication()
      .subscribe({
        next: value => this.isAuthenticated = value
      })
  }

  checkAuthentication(): Observable<boolean> {
    return this.http.get<boolean>('api/user/check-auth')
      .pipe(map((isAuthenticated) => {
        if (!isAuthenticated){
          this.isAuthenticated = false
          this.router.navigate(['/login'])
          return false;
        }
        this.isAuthenticated = true
        return true
      }));
  }

  logout(){
    this.http.post('api/user/logout', {})
      .subscribe({
        next: () => this.router.navigate(['/login'])
      })
  }
}
