import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http: HttpClient) {

  }

  getAllComments(reviewId: number) : Observable<any> {
    return this.http.get<any>(`api/comments/${reviewId}`)
  }

  sendComment(reviewId: number, text: string) : Observable<any> {
    return this.http.post<any>('api/comments', {reviewId: reviewId, text: text})
  }
}
