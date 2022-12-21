import {Component, OnInit, ViewChild} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {ColumnMode, DatatableComponent} from '@swimlane/ngx-datatable';
import {ReviewUserPageModel} from "../../common/models/ReviewUserPageModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {FiltrationService} from "../../common/services/filtration/filtration.service";

@Component({
  selector: 'app-user-page',
  templateUrl: 'user-page.component.html',
  styleUrls: ['user-page.component.css']
})
export class UserPageComponent implements OnInit {

  ColumnMode = ColumnMode;
  reviews!: ReviewUserPageModel[]
  rows!: ReviewUserPageModel[]

  constructor(public reviewsService: ReviewsService,
              private filtrationService: FiltrationService) {

  }

  filterForm = new FormGroup({
    filterType: new FormControl(),
    filterValue: new FormControl()
  })

  ngOnInit() {
    this.getReviews()
  }

  getReviews(){
    this.reviewsService.getReviewsByUserId()
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

  updateFilter() {
    let filterType: string = this.filterForm.get('filterType')?.value!
    let filterValue: string = this.filterForm.get('filterValue')?.value!

    this.rows = this.filtrationService.filtrateData(filterType, filterValue, this.reviews)
  }

  resetFiltration(){
    this.rows = this.reviews
  }

  logout() {
    window.location.href = "api/user/logout";
  }
}
