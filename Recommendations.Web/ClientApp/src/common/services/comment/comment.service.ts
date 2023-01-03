import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  readonly baseUrl: string = "api/comments"

  constructor(private http: HttpClient) {

  }

  getAllComments(reviewId: number) : Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${reviewId}`)
  }

  sendComment(reviewId: number, text: string) : Observable<any> {
    return this.http.post<any>(this.baseUrl, {reviewId: reviewId, text: text})
  }
}
