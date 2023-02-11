import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  readonly baseUrl: string = "api/likes"

  constructor(private http: HttpClient) {

  }

  setLike(reviewId: number, isLike: boolean): Observable<any> {
    return this.http.post(this.baseUrl, {reviewId: reviewId, isLike: isLike})
  }
}
