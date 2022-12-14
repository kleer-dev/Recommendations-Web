import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ReviewModel} from "../../common/models/ReviewModel";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['home.component.css']
})
export class HomeComponent {

  reviews: ReviewModel[] = [];

  constructor(private http: HttpClient) {

  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  onSubmit(){

  }

  ngOnInit() {
    this.http.get('api/reviews/get-all')
      .subscribe({
        next: (data: any) => this.reviews = data,
        complete: () => console.log(this.reviews)
      });
  }

}
