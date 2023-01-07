import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";
import {UserModel} from "../../common/models/UserModel";
import {firstValueFrom, lastValueFrom} from "rxjs";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {Roles} from "../../common/consts/Roles";

@Component({
  selector: 'app-admin-page',
  templateUrl: 'admin-page.component.html',
  styleUrls: ['admin-page.component.css']
})
export class AdminPageComponent implements OnInit {

  adminRole: string = Roles.admin
  userRole: string = Roles.user

  currentUserInfo!: UserModel

  ColumnMode = ColumnMode
  users!: UserModel[]
  waiter: boolean = false

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {
  }

  async ngOnInit() {
    await this.getUserInfo()
    await this.getAllUsers()
  }

  async getAllUsers() {
    let users = await firstValueFrom(this.userService.getAllUsers())
    if (users) {
      this.users = users.filter(user => user.id !== this.currentUserInfo.id)
    }
    this.waiter = true
  }

  deleteUser(userId: number) {
    this.users = this.users.filter(user => user.id !== userId)
    this.userService.deleteUser(userId)
      .subscribe({})
  }

  async getUserInfo() {
    this.currentUserInfo = await firstValueFrom(this.userService.getUserInfo())
  }

  async blockUser(userId: number) {
    let changeUser = this.users.find(user => user.id === userId)
    if (changeUser) changeUser.accessStatus = 'Blocked'
    await firstValueFrom(this.userService.blockUser(userId))
  }

  async unblockUser(userId: number) {
    let changeUser = this.users.find(user => user.id === userId)
    if (changeUser) changeUser.accessStatus = 'Unblocked'
    await firstValueFrom(this.userService.unblockUser(userId))
  }

  async setRole(userId: number, role: string) {
    let changeUser = this.users.find(user => user.id === userId)
    if (changeUser) changeUser.role = role
    await firstValueFrom(this.userService.setRole(userId, role))
  }
}
