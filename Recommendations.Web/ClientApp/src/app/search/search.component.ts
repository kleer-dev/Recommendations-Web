import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: ['search.component.css']
})
export class SearchComponent {

  constructor(private http: HttpClient) {

  }

  searchForm = new FormGroup({
    search: new FormControl('')
  })

  search(){
    let searchQuery = this.searchForm.get('search')?.value
    this.http.get(`api/search/reviews?searchQuery=${searchQuery}`)
      .subscribe({
        next: value => console.log(value)
      })
  }

}
