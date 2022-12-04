import {Component} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

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
    console.log(this.registrationForm?.controls.remember)
  }

}
