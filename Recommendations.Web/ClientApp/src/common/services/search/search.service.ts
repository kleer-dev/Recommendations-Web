import {HttpClient} from "@angular/common/http";
import {ReviewPreviewModel} from "../../models/ReviewPreviewModel";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  readonly baseUrl: string = "api/search"

  constructor(private http: HttpClient) {

  }

  findReview(searchQuery: string | null) : Observable<ReviewPreviewModel[]> {
    return this.http.get<ReviewPreviewModel[]>(`${this.baseUrl}/reviews?searchQuery=${searchQuery}`)
  }
}
