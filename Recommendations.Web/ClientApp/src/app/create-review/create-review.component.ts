import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {ReviewFormModel} from "src/common/models/ReviewFormModel";
import {formToFormData} from "src/common/functions/formToFormData";
import {ReviewsService} from "../../common/services/reviews/reviews.service";

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['create-review.component.css']
})
export class CreateReviewComponent implements OnInit {

  waiter: boolean = true
  userId?: number;

  constructor(private activatedRoute: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private reviewService: ReviewsService) {

  }

  ngOnInit(): void {
    this.getUserIdFromQueryParams()
  }

  getUserIdFromQueryParams(){
    this.activatedRoute.params.subscribe({
      next: value => this.userId = value['userid']
    })
  }

  reviewForm: ReviewFormModel = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(100)
    ]),
    productName: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(100)
    ]),
    categoryName: new FormControl('', [
      Validators.required
    ]),
    tags: new FormControl(new Array<string>(), [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(100),
      Validators.maxLength(20000)
    ]),
    authorRate: new FormControl(1),
    images: new FormControl([new File([], '')])
  })

  onSubmitForm() {
    this.waiter = false
    this.reviewService.createReview(this.reviewForm, this.userId)
      .subscribe({
        next: _ => window.history.back(),
        error: () => this.waiter = true
      })
  }
}


