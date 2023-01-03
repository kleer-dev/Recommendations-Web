import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http: HttpClient) {

  }

  changeRating(reviewId: number, value: number): Observable<any> {
    return this.http.post('api/ratings', {reviewId: reviewId, value: value})
  }
}
