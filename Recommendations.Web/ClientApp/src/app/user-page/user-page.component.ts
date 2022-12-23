import {Component, OnInit} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {ReviewUserPageModel} from "../../common/models/ReviewUserPageModel";
import {FormControl, FormGroup} from "@angular/forms";
import {FiltrationService} from "../../common/services/filtration/filtration.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user-page',
  templateUrl: 'user-page.component.html',
  styleUrls: ['user-page.component.css']
})
export class UserPageComponent implements OnInit {

  ColumnMode = ColumnMode;
  reviews!: ReviewUserPageModel[]
  rows!: ReviewUserPageModel[]

  userId?: number | null = null

  constructor(public reviewsService: ReviewsService,
              private filtrationService: FiltrationService,
              private activatedRoute: ActivatedRoute) {

  }

  filterForm = new FormGroup({
    filterType: new FormControl(),
    filterValue: new FormControl()
  })

  ngOnInit() {
    this.getUserIdFromQueryParams()
    this.getReviews()
  }

  getReviews(){
    this.reviewsService.getReviewsByUserId(this.userId)
      .subscribe({
        next: data => {
          this.reviews = data
          this.rows = this.reviews
        }
      });
  }

  deleteReview(reviewId: number) {
    this.reviewsService.deleteReview(reviewId)
      .subscribe({
        complete: () => this.getReviews()
      })
  }

  getUserIdFromQueryParams(){
     this.activatedRoute.params.subscribe({
      next: value => this.userId = value['userid']
    })
  }

  updateFilter() {
    let filterType: string = this.filterForm.get('filterType')?.value!
    let filterValue: string = this.filterForm.get('filterValue')?.value!

    this.rows = this.filtrationService.filtrateData(filterType, filterValue, this.reviews)
  }

  resetFiltration(){
    this.rows = this.reviews
  }
}
