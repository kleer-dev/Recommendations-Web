import {ChangeDetectorRef, Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-access-denied',
  templateUrl: 'access-denied.component.html',
  styleUrls: ['access-denied.component.css']
})
export class AccessDeniedComponent {

  constructor(public userService: UserService) {

  }

}
