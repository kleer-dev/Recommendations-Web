import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class TagService {

  readonly baseUrl: string = "api/tags"

  constructor(private http: HttpClient) {

  }

  getAllTags(){
    return this.http.get<any>(this.baseUrl)
  }
}
