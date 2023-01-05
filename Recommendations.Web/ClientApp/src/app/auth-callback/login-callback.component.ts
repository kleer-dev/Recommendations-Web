import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-login-callback',
  templateUrl: 'login-callback.component.html',
})

export class LoginCallbackComponent {
  error?: string;

  constructor(private http: HttpClient, private router: Router,
              private userService: UserService) {

    this.http.get<boolean>('api/user/external-login-callback')
      .subscribe({
        next: () => {
          this.userService.isAuthenticated = true
          this.userService.checkRole()
          this.router.navigate(['/'])
        }
    });
  }
}
