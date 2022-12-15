import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ReviewModel} from "../../common/models/ReviewModel";
import {Log} from "oidc-client";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})
export class ReviewComponent implements OnInit {

  review!: ReviewModel;
  reviewId: number = 0;

  rate: number = 0;
  style: string = 'btn-dark'

  constructor(private activateRoute: ActivatedRoute,
              private http: HttpClient) {

  }

  ngOnInit(): void {
    this.getReview()
  }

  getReview() {
    this.reviewId = this.activateRoute.snapshot.params['id']
    this.http.get(`api/reviews/get?reviewId=${this.reviewId}`)
      .subscribe({
        next: (data: any) => this.review = data,
        complete: () => {
          this.rate = this.review.userRating
        }
      })
  }

  onRateChange(rating: number) {
    this.rate = rating;
    this.http.post('api/ratings', {reviewId: this.reviewId, value: this.rate})
      .subscribe({
        error: err => {
          if (err.status === 404)
            console.error('The review not found')
        }
      })
  }

  onLike() {
    this.review.isLike = !this.review.isLike;

    if (this.review.isLike)
      this.review.likeCount += 1;
    else
      this.review.likeCount -= 1;

    this.http.post('api/likes', {reviewId: this.reviewId, isLike: this.review.isLike})
      .subscribe({
        error: err => {
          if (err.status === 404)
            console.error('The review not found')
        }
      })
  }
}

