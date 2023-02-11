import {Component, OnInit} from '@angular/core';
import {UserService} from "../../common/services/user/user.service";
import {FormControl, FormGroup} from "@angular/forms";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  constructor(public userService: UserService) {

  }

  async ngOnInit() {
    await firstValueFrom(this.userService.checkAuthentication())
    this.userService.checkRole()
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    window.location.href = "api/user/logout";
  }
}
