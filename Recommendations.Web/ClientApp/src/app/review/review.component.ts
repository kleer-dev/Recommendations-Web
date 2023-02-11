import {Component, ElementRef, OnDestroy, OnInit, ViewChild} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ReviewModel} from "../../common/models/ReviewModel";
import {CommentsSignalrService} from "../../common/services/signalr/comments-signalr.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {RatingService} from "../../common/services/rating/rating.service";
import {LikeService} from "../../common/services/like/like.service";
import {CommentService} from "../../common/services/comment/comment.service";
import {UserService} from "../../common/services/user/user.service";
import {firstValueFrom} from "rxjs";
import {LinkedReviewModel} from "../../common/models/LinkedReviewModel";
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import {ImageService} from "../../common/services/images/image-service";

const htmlToPdfmake = require("html-to-pdfmake");
(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;


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
  linkedReviews: LinkedReviewModel[] = []
  @ViewChild('htmlToPdf') private elementRef!: ElementRef;

  constructor(private activateRoute: ActivatedRoute,
              private http: HttpClient,
              public signalrService: CommentsSignalrService,
              private reviewsService: ReviewsService,
              private ratingService: RatingService,
              private likeService: LikeService,
              private commentService: CommentService,
              private router: Router,
              private userService: UserService,
              private imageService: ImageService) {
    this.getReview()
    this.getAllComments()
  }

  async ngOnInit(): Promise<void> {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    await this.signalrService.connect()
    await this.signalrService.connectToGroup(this.reviewId.toString())
    await this.getLinkedReviews()
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
    this.commentForm.patchValue({commentText: ''});
    let commentId = await firstValueFrom(this.commentService.sendComment(this.reviewId, commentText!))
    await this.signalrService.NotifyAboutComment(this.reviewId.toString(), commentId)
    await this.getAllComments()
  }

  async ngOnDestroy(): Promise<void> {
    await this.signalrService.closeConnection()
  }

  async getLinkedReviews() {
    this.linkedReviews = await firstValueFrom(this.reviewsService.getLinkedReviews(this.reviewId))
  }

  async downloadPdf() {
    const element = this.elementRef.nativeElement;
    let copyPdfSection = element.cloneNode(true);
    let elementsToRemove: NodeListOf<HTMLElement> = copyPdfSection.querySelectorAll('.no-print');
    let images: HTMLImageElement[] = Array.from(copyPdfSection.getElementsByTagName('img'));
    let svg: HTMLOrSVGImageElement[] = Array.from(copyPdfSection.getElementsByTagName('svg'));

    elementsToRemove.forEach(element => element.remove());
    svg.forEach(svg => svg.remove());
    for (const image of images) {
      image.src = await this.imageService.convertToBase64(image);
      image.width = 700
      image.style.margin = '0 0 0 20';
    }

    const html = htmlToPdfmake(copyPdfSection.innerHTML);
    const documentDefinition = {
      content: html
    };
    pdfMake.createPdf(documentDefinition).download();
  }
}

