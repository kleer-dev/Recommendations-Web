import {Component, OnDestroy, OnInit} from "@angular/core";
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
import {UserService} from "../../common/services/user/user.service";
import {async, firstValueFrom} from "rxjs";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})
export class ReviewComponent implements OnInit, OnDestroy {

  waiter: boolean = false;
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
              private router: Router,
              private userService: UserService) {
    this.getReview()
    this.getAllComments()
    if (this.userService.isAuthenticated) {

    }
  }

  async ngOnInit(): Promise<void> {
    await this.signalrService.connect()
    await this.signalrService.connectToGroup(this.reviewId.toString())
  }

  async getReview() {
    this.reviewId = this.activateRoute.snapshot.params['id']

    this.review = await firstValueFrom(this.reviewsService.getReviewById(this.reviewId))
    this.rate = this.review.userRating
    this.waiter = true
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

  async getAllComments() {
    this.signalrService.comments = await firstValueFrom(this.commentService.getAllComments(this.reviewId))
  }

  commentForm = new FormGroup({
    commentText: new FormControl('', [
      Validators.required,
      Validators.maxLength(400)])
  })

  async sendComment() {
    let commentText = this.commentForm.get('commentText')!.value

    let commentId = await firstValueFrom(this.commentService.sendComment(this.reviewId, commentText!))
    await this.signalrService.NotifyAboutComment(this.reviewId.toString(), commentId)
    await this.getAllComments()
    this.commentForm.patchValue({commentText: ''});
  }

  async ngOnDestroy(): Promise<void> {
    await this.signalrService.closeConnection()
  }
}

