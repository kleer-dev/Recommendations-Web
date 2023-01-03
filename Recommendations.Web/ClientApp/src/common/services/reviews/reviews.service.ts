import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {ReviewPreviewModel} from "../../models/ReviewPreviewModel";
import {FilteringParameters} from "../../consts/FilteringParameters";
import {ReviewUserPageModel} from "../../models/ReviewUserPageModel";
import {Observable} from "rxjs";
import {FormControl, FormGroup} from "@angular/forms";
import {formToFormData} from "../../functions/formToFormData";
import {ReviewModel} from "../../models/ReviewModel";
import {UpdateReviewModel} from "../../models/UpdateReviewModel";

@Injectable({
  providedIn: 'root'
})
export class ReviewsService {
  filtrate?: string | null = FilteringParameters.recent;
  count?: number | undefined;
  tag: string | undefined
  public reviews: any;

  waiter!: Promise<boolean>

  constructor(private http: HttpClient, private activateRoute: ActivatedRoute,
              private router: Router) {

  }

  async setParams(filtrate?: string | null, count?: number | undefined, tag?: string | undefined) {
    this.waiter = Promise.resolve(false)
    this.filtrate = filtrate;
    this.count = count;
    this.tag = tag;
    await this.changeRoute()
  }

  getParams() {
    this.activateRoute.queryParams.subscribe(params => {
      this.filtrate = params['filtrate'];
      this.count = params['count'];
      this.tag = params['tag']
    });

    if (this.filtrate === undefined || this.count === undefined) {
      this.filtrate = FilteringParameters.recent;
      this.count = 10
      this.tag = undefined
    }
  }

  async changeRoute() {
    this.router.navigate(['/'], {
      queryParams: {
        'filtrate': this.filtrate,
        'count': this.count,
        'tag': this.tag
      }
    })
  }

  getAllReviews() {
    this.getParams()

    let getUrl = this.tag === undefined
      ? `api/reviews/get-all?filtrate=${this.filtrate}&count=${this.count}`
      : `api/reviews/get-all?filtrate=${this.filtrate}&count=${this.count}&tag=${this.tag}`;

    this.http.get<ReviewPreviewModel>(getUrl)
      .subscribe({
        next: data => {
          this.reviews = data
          this.waiter = Promise.resolve(true)
        }
      });
  }

  getReviewById(reviewId: number): Observable<ReviewModel> {
    return this.http.get<ReviewModel>(`api/reviews/${reviewId}`)
  }

  getReviewsByUserId(userId?: number | null): Observable<ReviewUserPageModel[]> {
    let url = `api/reviews/get-by-user`
    if (userId !== undefined)
      url = `${url}/${userId}`
    return this.http.get<ReviewUserPageModel[]>(url)
  }

  deleteReview(reviewId: number) {
    return this.http.delete(`api/reviews/${reviewId}`)
  }

  createReview(form: FormGroup, userId?: number): Observable<any> {
    let url = "api/reviews"
    if (userId)
      url = `${url}/${userId}`
    return this.http.post(url, formToFormData(form))
  }

  updateReview(form: FormGroup): Observable<any> {
    return this.http.put("api/reviews", formToFormData(form))
  }

  getReviewForUpdate(reviewId: number) : Observable<UpdateReviewModel> {
    return this.http.get<UpdateReviewModel>(`api/reviews/get-update-review/${reviewId}`)
  }
}
