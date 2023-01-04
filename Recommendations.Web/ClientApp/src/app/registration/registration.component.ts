import {Component, Inject} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  error?: string

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {

  }

  checkPasswordConfirmation: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const passwordConfirm = group.get('passwordConfirmation')?.value;
    return password === passwordConfirm ? null : {notSame: true}
  };

  registrationForm = new FormGroup({
    login: new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    passwordConfirmation: new FormControl('', [
      Validators.required
    ]),
    remember: new FormControl(false)
  }, {validators: this.checkPasswordConfirmation})

  onSubmit() {
    this.userService.registration(this.registrationForm)
      .subscribe({
        next: () => {
          this.userService.isAuthenticated = true
          this.router.navigate(['/'])
        },
        error: err => {
          if (err.status === 409)
            this.error = 'A user with the same email or login already exists'
        }
      })
  }
}
