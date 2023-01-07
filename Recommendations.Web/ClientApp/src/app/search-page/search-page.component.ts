import {ChangeDetectorRef, Component, OnInit} from "@angular/core";
import {ReviewPreviewModel} from "../../common/models/ReviewPreviewModel";
import {FormControl, FormGroup} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {SearchService} from "../../common/services/search/search.service";

@Component({
  selector: 'app-search-page',
  templateUrl: 'search-page.component.ts.html'
})
export class SearchPageComponent implements OnInit {

  waiter: boolean = false
  reviews!: ReviewPreviewModel[]
  previousQuery!: string | null;
  currentQuery!: string | null;

  constructor(private activatedRoute: ActivatedRoute,
              private searchService: SearchService,) {
    this.activatedRoute.params.subscribe(value => this.ngOnInit())
  }

  ngOnInit() {
    this.currentQuery = this.activatedRoute.snapshot.paramMap.get('search-query')
    this.checkQueryParams()
  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  checkQueryParams(){
    if (this.currentQuery !== this.previousQuery) {
      this.previousQuery = this.currentQuery;
      this.getReviews()
    }
  }

  getReviews() {
    this.searchService.findReview(this.currentQuery)
      .subscribe({
        next: value => {
          this.reviews = value
          this.waiter = true
        },
        error: err => {
          this.waiter = true
        }
      })
  }
}
