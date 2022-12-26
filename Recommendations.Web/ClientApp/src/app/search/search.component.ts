import {ChangeDetectorRef, Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: ['search.component.css']
})
export class SearchComponent {

  constructor(private http: HttpClient, private router: Router, private cdr: ChangeDetectorRef) {

  }

  searchForm = new FormGroup({
    search: new FormControl('', [Validators.required])
  })

  search() {
    let searchQuery = this.searchForm.get('search')?.value
    this.router.navigate(['/search', searchQuery]);
  }

}
