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
import {LinkedReviewModel} from "../../models/LinkedReviewModel";

@Injectable({
  providedIn: 'root'
})
export class ReviewsService {

  readonly baseUrl: string = "api/reviews"

  filtrate?: string | null = FilteringParameters.recent;
  count?: number | undefined;
  tag: string | undefined
  public reviews: any;

  waiter: boolean = false

  constructor(private http: HttpClient, private activateRoute: ActivatedRoute,
              private router: Router) {

  }

  async setParams(filtrate?: string | null, count?: number | undefined, tag?: string | undefined) {
    this.waiter = false
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

    let getUrl = this.tag === undefined || this.tag === null
      ? `${this.baseUrl}/get-all?filtrate=${this.filtrate}&count=${this.count}`
      : `${this.baseUrl}/get-all?filtrate=${this.filtrate}&count=${this.count}&tag=${this.tag}`;

    this.http.get<ReviewPreviewModel>(getUrl)
      .subscribe({
        next: data => {
          this.reviews = data
          this.waiter = true
        }
      });
  }

  getReviewById(reviewId: number): Observable<ReviewModel> {
    return this.http.get<ReviewModel>(`${this.baseUrl}/${reviewId}`)
  }

  getReviewsByUserId(userId?: number | null): Observable<ReviewUserPageModel[]> {
    let url = `${this.baseUrl}/get-by-user`
    if (userId !== undefined)
      url = `${url}/${userId}`
    return this.http.get<ReviewUserPageModel[]>(url)
  }

  deleteReview(reviewId: number) {
    return this.http.delete(`${this.baseUrl}/${reviewId}`)
  }

  createReview(form: FormGroup, userId?: number): Observable<any> {
    let url = this.baseUrl
    if (userId)
      url = `${url}/${userId}`
    return this.http.post(url, formToFormData(form))
  }

  updateReview(form: FormGroup): Observable<any> {
    return this.http.put(this.baseUrl, formToFormData(form))
  }

  getReviewForUpdate(reviewId: number) : Observable<UpdateReviewModel> {
    return this.http.get<UpdateReviewModel>(`${this.baseUrl}/get-update-review/${reviewId}`)
  }

  getLinkedReviews(reviewId: number) : Observable<LinkedReviewModel[]> {
    return this.http.get<LinkedReviewModel[]>(`${this.baseUrl}/get-linked-reviews/${reviewId}`)
  }
}
