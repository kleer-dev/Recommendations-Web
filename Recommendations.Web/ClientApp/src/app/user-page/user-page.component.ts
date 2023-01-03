import {Component, OnInit} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {ReviewUserPageModel} from "../../common/models/ReviewUserPageModel";
import {FormControl, FormGroup} from "@angular/forms";
import {FiltrationReviewService} from "../../common/services/filtration/filtration-review.service";
import {ActivatedRoute} from "@angular/router";
import {UserModel} from "../../common/models/UserModel";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-user-page',
  templateUrl: 'user-page.component.html',
  styleUrls: ['user-page.component.css']
})
export class UserPageComponent implements OnInit {
  waiter: boolean = false
  ColumnMode = ColumnMode;
  reviews!: ReviewUserPageModel[]
  rows!: ReviewUserPageModel[]
  userData!: UserModel

  userId?: number | null = null

  constructor(public reviewsService: ReviewsService,
              private filtrationService: FiltrationReviewService,
              private activatedRoute: ActivatedRoute,
              private userService: UserService) {

  }

  filterForm = new FormGroup({
    filterType: new FormControl(),
    filterValue: new FormControl()
  })

  ngOnInit() {
    this.getUserIdFromQueryParams()
    this.getReviews()
    this.getUserInfo(this.userId!)
  }

  getUserInfo(userId?: number){
    if (userId === undefined){
      this.userService.getUserInfo()
        .subscribe({
          next: data => {
            this.userData = data
            this.waiter = true
          }
        })
    }
    else {
      this.userService.getUserInfoById(userId)
        .subscribe({
          next: data => {
            this.userData = data
            this.waiter = true
          }
        })
    }
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
        next: () => this.getReviews()
      })
  }

  getUserIdFromQueryParams(){
     this.activatedRoute.params.subscribe({
      next: value => {
        this.userId = value['userid']
      }
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
