import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-admin-page',
  templateUrl: 'admin-page.component.html',
  styleUrls: ['admin-page.component.css']
})
export class AdminPageComponent implements OnInit{

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {
    this.userService.checkRole();
  }

  ngOnInit(): void {
    this.userService.checkRole()
  }

}
