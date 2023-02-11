import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UpdateReviewModel} from "../../common/models/UpdateReviewModel";
import {ReviewFormModel} from "../../common/models/ReviewFormModel";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {firstValueFrom} from "rxjs";
import {ProductsService} from "../../common/services/products/products.service";
import {ImageService} from "../../common/services/images/image-service";

@Component({
  selector: 'app-update-review',
  templateUrl: 'update-review.component.html'
})
export class UpdateReviewComponent implements OnInit {

  waiter: boolean = false
  reviewId: number = 0
  review!: UpdateReviewModel
  tags: string[] = []
  userId: number | null = null
  files: File[] = []
  reviewForm!: ReviewFormModel
  products: string[] = []

  constructor(private http: HttpClient, private router: Router,
              private activatedRoute: ActivatedRoute,
              private reviewService: ReviewsService,
              private productsService: ProductsService,
              private imageService: ImageService) {

  }

  async ngOnInit() {
    await this.getAllProducts()
    this.getUserIdFromQueryParams()
    this.reviewId = this.activatedRoute.snapshot.params['id']
    this.reviewService.getReviewForUpdate(this.reviewId)
      .subscribe({
        next: async data => {
          this.review = data
          await this.getImages(data.imagesUrls)
          this.tags = data.tags
          this.reviewForm = new FormGroup({
            reviewId: new FormControl(this.reviewId),
            title: new FormControl(data.title, [
              Validators.required,
              Validators.minLength(5),
              Validators.maxLength(100)
            ]),
            productName: new FormControl(data.productName, [
              Validators.required,
              Validators.minLength(5),
              Validators.maxLength(100)
            ]),
            categoryName: new FormControl(data.categoryName, [
              Validators.required,
              Validators.minLength(2)
            ]),
            tags: new FormControl(data.tags, [
              Validators.required
            ]),
            description: new FormControl(data.description, [
              Validators.required,
              Validators.minLength(100),
              Validators.maxLength(5000)
            ]),
            authorRate: new FormControl(data.authorRate),
            images: new FormControl(this.files)
          })
          this.waiter = true
        }
      })
  }

  getUserIdFromQueryParams() {
    this.activatedRoute.params.subscribe({
      next: value => this.userId = value['userid']
    })
  }

  onSubmitForm() {
    this.waiter = false;
    console.log(this.reviewForm)
    this.reviewService.updateReview(this.reviewForm)
      .subscribe({
        next: _ => window.history.back(),
        error: err => {
          this.waiter = true;
        }
      })
  }

  async getImages(urls: string[]) {
    this.files = await this.imageService.getImages(this.files, urls)
  }

  async getAllProducts() {
    this.products = (await firstValueFrom(this.productsService.getAll()))
      .map(product => product.name)
  }
}


