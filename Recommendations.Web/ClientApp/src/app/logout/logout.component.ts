import {Component, OnInit} from '@angular/core';
import {UserService} from "../../common/services/user/user.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-logout',
  templateUrl: 'logout.component.html',
  styleUrls: ['logout.component.css']
})
export class LogoutComponent implements OnInit{

  constructor(private userService: UserService) {

  }

  ngOnInit(): void {
    this.logout()
  }

  logout(){
    this.userService.isAuthenticated = false
    this.userService.logout()
  }
}
