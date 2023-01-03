import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  constructor(private http: HttpClient) {

  }

  setLike(reviewId: number, isLike: boolean): Observable<any> {
    return this.http.post('api/likes', {reviewId: reviewId, isLike: isLike})
  }
}
