import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  readonly baseUrl: string = "api/ratings"

  constructor(private http: HttpClient) {

  }

  changeRating(reviewId: number, value: number): Observable<any> {
    return this.http.post(this.baseUrl, {reviewId: reviewId, value: value})
  }
}
