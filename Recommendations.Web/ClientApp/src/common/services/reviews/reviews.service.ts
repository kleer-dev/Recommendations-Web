import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {ReviewPreviewModel} from "../../models/ReviewPreviewModel";
import {FilteringParameters} from "../../consts/FilteringParameters";
import {ReviewModel} from "../../models/ReviewModel";

@Injectable({
  providedIn: 'root'
})
export class ReviewsService {
  filtrate: string | null = FilteringParameters.recent;
  count: number | null = 10;
  public reviews: ReviewPreviewModel[] = [];

  waiter!: Promise<boolean>

  constructor(private http: HttpClient, private activateRoute: ActivatedRoute,
              private router: Router) {

  }

  async setParams(filtrate: string | null, count: number | null) {
    this.waiter = Promise.resolve(false)
    this.filtrate = filtrate;
    this.count = count;
    await this.changeRoute()
  }

  getParams() {
    this.activateRoute.queryParams.subscribe(params => {
      this.filtrate = params['filtrate'];
      this.count = params['count'];
    });

    if (this.filtrate == null || this.count == null) {
      this.filtrate = FilteringParameters.recent;
      this.count = 10
    }
  }

  async changeRoute() {
    await this.router.navigate(['/'], {
      queryParams: {
        'filtrate': this.filtrate,
        'count': this.count
      }
    })
  }

  getAllReviews() {
    this.getParams()

    this.http.get<any>(`api/reviews/get-all?filtrate=${this.filtrate}&count=${this.count}`)
      .subscribe({
        next: data => {
          this.reviews = data
          this.waiter = Promise.resolve(true)
        }
      });
  }
}
