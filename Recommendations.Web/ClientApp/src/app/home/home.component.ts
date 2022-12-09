import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private http: HttpClient, private router: Router) {
  }

  email?: any;

  ngOnInit() {
    this.http.get<any>('api/home/name')
      .subscribe({
        next: (data: any) => this.email = data
      });
  }
}
