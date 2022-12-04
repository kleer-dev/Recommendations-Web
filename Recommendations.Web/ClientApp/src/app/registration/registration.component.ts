import {Component} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  error?: string

  constructor(private http: HttpClient, private router: Router) {

  }

  checkPasswordConfirmation: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const passwordConfirm = group.get('passwordConfirmation')?.value;

    return password === passwordConfirm ? null : {notSame: true}
  };

  registrationForm = new FormGroup({
    'login': new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    'email': new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    'password': new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    'passwordConfirmation': new FormControl('', [
      Validators.required
    ]),
    'remember': new FormControl(false)
  }, {validators: this.checkPasswordConfirmation})

  onSubmit() {
    this.http.post('api/user/registration', this.registrationForm.value)
      .subscribe({
        next: _ => this.router.navigate(['/']),
        error: err => {
          if (err.status === 409)
            this.error = 'A user with the same email or login already exists'
        }
      })
  }
}
