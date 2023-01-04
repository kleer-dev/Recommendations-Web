import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ReviewModel} from "../../common/models/ReviewModel";
import {CommentsSignalrService} from "../../common/services/signalr/comments-signalr.service";
import {CommentModel} from "../../common/models/CommentModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {RatingService} from "../../common/services/rating/rating.service";
import {LikeService} from "../../common/services/like/like.service";
import {CommentService} from "../../common/services/comment/comment.service";

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

  constructor(private activateRoute: ActivatedRoute,
              private http: HttpClient,
              public signalrService: CommentsSignalrService,
              private reviewsService: ReviewsService,
              private ratingService: RatingService,
              private likeService: LikeService,
              private commentService: CommentService,
              private router: Router) {
    this.getReview()
    this.getAllComments()
  }

  async ngOnInit(): Promise<void> {
    await this.signalrService.connect()
    await this.signalrService.connectToGroup(this.reviewId.toString())
  }

  getReview() {
    this.reviewId = this.activateRoute.snapshot.params['id']
    this.reviewsService.getReviewById(this.reviewId)
      .subscribe({
        next: data => {
          this.review = data
          this.waiter = Promise.resolve(true)
          this.rate = this.review.userRating
        }
      })
  }

  onRateChange(rating: number) {
    this.rate = rating;
    this.ratingService.changeRating(this.reviewId, this.rate)
      .subscribe({})
  }

  onLike() {
    this.review.isLike = !this.review.isLike;
    if (this.review.isLike)
      this.review.likesCount += 1;
    else
      this.review.likesCount -= 1;

    this.likeService.setLike(this.reviewId, this.review.isLike)
      .subscribe()
  }

  getAllComments() {
    this.commentService.getAllComments(this.reviewId)
      .subscribe({
        next: (comments) => this.signalrService.comments = comments
      });
  }

  commentForm = new FormGroup({
    commentText: new FormControl('', [
      Validators.required,
      Validators.maxLength(400)])
  })

  async sendComment() {
    let commentText = this.commentForm.get('commentText')!.value
    this.commentService.sendComment(this.reviewId, commentText!)
      .subscribe({
        next: async (commentId) => {
          await this.signalrService.NotifyAboutComment(this.reviewId.toString(), commentId)
          this.getAllComments()
          this.commentForm.patchValue({commentText: ''});
        }
      })
  }
}

