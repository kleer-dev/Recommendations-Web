import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {UserModel} from "../../common/models/UserModel";

@Component({
  selector: 'app-admin-page',
  templateUrl: 'admin-page.component.html',
  styleUrls: ['admin-page.component.css']
})
export class AdminPageComponent implements OnInit{

  ColumnMode = ColumnMode;
  users!: UserModel[]
  waiter!: Promise<boolean>

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {
  }

  ngOnInit(): void {
    this.getAllUsers()
  }

  getAllUsers(){
    this.userService.getAllUsers()
      .subscribe({
        next: users => {
          this.users = users
          this.waiter = Promise.resolve(true)
        }
      })
  }
}
