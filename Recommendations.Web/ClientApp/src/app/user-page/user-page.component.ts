import {Component, OnInit} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {ReviewUserPageModel} from "../../common/models/ReviewUserPageModel";
import {FormControl, FormGroup} from "@angular/forms";
import {FiltrationReviewService} from "../../common/services/filtration/filtration-review.service";
import {ActivatedRoute} from "@angular/router";
import {UserModel} from "../../common/models/UserModel";
import {UserService} from "../../common/services/user/user.service";
import {firstValueFrom} from "rxjs";

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

  async ngOnInit() {
    this.getUserIdFromQueryParams()
    await this.getReviews()
    await this.getUserInfo(this.userId!)
  }

  async getUserInfo(userId?: number) {
    if (userId === undefined) {
      this.userData = await firstValueFrom(this.userService.getUserInfo())
      this.waiter = true
    } else {
      this.userData = await firstValueFrom(this.userService.getUserInfoById(userId))
      this.waiter = true
    }
  }

  async getReviews() {
    this.reviews = await firstValueFrom(this.reviewsService.getReviewsByUserId(this.userId))
    this.rows = this.reviews
  }

  async deleteReview(reviewId: number) {
    this.rows = this.reviews.filter(review => review.reviewId !== reviewId.toString())
    await firstValueFrom(this.reviewsService.deleteReview(reviewId))
  }

  getUserIdFromQueryParams() {
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
    console.log(this.rows)
  }

  resetFiltration() {
    this.rows = this.reviews
  }
}
