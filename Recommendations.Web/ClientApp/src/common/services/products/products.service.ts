import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import { ProductModel } from 'src/common/models/ProductModel';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  readonly baseUrl: string = "api/products"

  constructor(private http: HttpClient) {

  }

  getAll() : Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(`${this.baseUrl}/get-all`)
  }

}
