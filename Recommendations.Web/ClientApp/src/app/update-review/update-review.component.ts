import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {formToFormData} from "src/common/functions/formToFormData";
import {UpdateReviewModel} from "../../common/models/UpdateReviewModel";
import {ReviewFormModel} from "../../common/models/ReviewFormModel";
import {Log} from "oidc-client";

@Component({
  selector: 'app-update-review',
  templateUrl: 'update-review.component.html'
})
export class UpdateReviewComponent implements OnInit{

  waiter!: Promise<boolean>
  reviewId: number = 0
  review!: UpdateReviewModel
  files: File[] = []
  tags: string[] = []

  userId: number | null = null

  reviewForm!: ReviewFormModel

  constructor(private http: HttpClient, private router: Router,
              private activatedRoute: ActivatedRoute) {

  }

  async ngOnInit() {
    this.getUserIdFromQueryParams()
    this.reviewId = this.activatedRoute.snapshot.params['id']
    this.http.get<UpdateReviewModel>(`api/reviews/get-update-review/${this.reviewId}`)
      .subscribe({
        next: data => {
          this.review = data
          this.getImages(data.imagesUrls)
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
              Validators.maxLength(20000)
            ]),
            authorRate: new FormControl(data.authorRate),
            images: new FormControl(this.files)
          })
          this.waiter = Promise.resolve(true)
        }
      })
  }

  getUserIdFromQueryParams(){
    this.activatedRoute.params.subscribe({
      next: value => this.userId = value['userid']
    })
  }

  onSubmitForm() {
    this.http.put("api/reviews", formToFormData(this.reviewForm))
      .subscribe({
        next: _ => window.history.back(),
        error: err => {
          console.error(err)
        }
      })
  }

  getImages(urls: Array<string>) {
    urls.forEach(url => {
      this.http.get(url, {responseType: 'blob'}).subscribe(blob => {
        this.files.push(new File([blob], 'filename.ext'));
      });
    })
    console.log(this.files)
  }
}


