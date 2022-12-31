import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ReviewModel} from "../../common/models/ReviewModel";
import {CommentsSignalrService} from "../../common/services/signalr/comments-signalr.service";
import {CommentModel} from "../../common/models/CommentModel";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})
export class ReviewComponent implements OnInit {

  waiter!: Promise<boolean>;
  review!: ReviewModel;
  reviewId: number = 0;
  rate: number = 0;

  style: string = 'btn-dark'

  constructor(private activateRoute: ActivatedRoute,
              private http: HttpClient,
              public signalrService: CommentsSignalrService) {
    this.getReview()
    this.getAllComments()
  }

  async ngOnInit(): Promise<void> {
    await this.signalrService.connect().then(async () => {
        await this.signalrService.connectToGroup(this.reviewId.toString())
    })
  }

  getReview() {
    this.reviewId = this.activateRoute.snapshot.params['id']
    this.http.get<ReviewModel>(`api/reviews?reviewId=${this.reviewId}`)
      .subscribe({
        next: data => {
          this.review = data
          this.waiter = Promise.resolve(true)
        },
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
      this.review.likesCount += 1;
    else
      this.review.likesCount -= 1;

    this.http.post('api/likes', {reviewId: this.reviewId, isLike: this.review.isLike})
      .subscribe()
  }

  getAllComments() {
    this.http.get(`api/comments/${this.reviewId}`)
      .subscribe({
        next: (comments: any) => this.signalrService.comments = comments
      });
  }

  async sendComment() {
    let commentText = (<HTMLTextAreaElement>document.getElementById('comment'))
    this.http.post<string>('api/comments', {reviewId: this.reviewId, text: commentText.value})
      .subscribe({
        next: async (commentId) => {
          await this.signalrService.NotifyAboutComment(this.reviewId.toString(), commentId)
          this.getAllComments()
        }
      })
    commentText.value = ''
  }
}

