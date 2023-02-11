import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  error?: string

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {

  }

  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    remember: new FormControl(false)
  })

  onSubmit() {
    this.userService.login(this.loginForm)
      .subscribe({
         next: async () => {
           this.userService.isAuthenticated = true
           await this.userService.checkRole()
           this.router.navigate(['/'])
        },
        error: err => {
          if (err.status === 404)
            this.error = 'notExist'
          if (err.status === 401)
            this.error = 'invalidPassword'
          if (err.status === 403)
            this.error = 'accountBlocked'
        }
      })
  }
}
