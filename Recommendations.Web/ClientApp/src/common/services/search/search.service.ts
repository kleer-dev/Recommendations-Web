import {HttpClient} from "@angular/common/http";
import {ReviewPreviewModel} from "../../models/ReviewPreviewModel";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) {

  }

  findReview(searchQuery: string | null) : Observable<ReviewPreviewModel[]> {
    return this.http.get<ReviewPreviewModel[]>(`api/search/reviews?searchQuery=${searchQuery}`)
  }
}
