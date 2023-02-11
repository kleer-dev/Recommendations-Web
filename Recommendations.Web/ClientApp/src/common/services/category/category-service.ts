import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  readonly baseUrl: string = "api/categories"

  constructor(private http: HttpClient) {

  }

  getAllCategories(){
    return this.http.get<any>(this.baseUrl)
  }
}
