import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup} from "@angular/forms";
import {ReviewPreviewModel} from "src/common/models/ReviewPreviewModel";
import {FilteringParameters} from "../../common/consts/FilteringParameters";
import {ActivatedRoute, Route, Router} from "@angular/router";
import {ReviewsService} from "../../common/services/reviews/reviews.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['home.component.css']
})
export class HomeComponent {

  constructor(public reviewService: ReviewsService,
              private activateRoute: ActivatedRoute, private router: Router) {

  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  countInput = new FormGroup({
    'count': new FormControl(0)
  })

  ngOnInit() {
    this.reviewService.getAllReviews()
  }

  async getRecentReviews() {
    await this.reviewService.setParams(FilteringParameters.recent,
      this.reviewService.count)
    await this.checkCountInput()
  }

  async getMostRatedReviews() {
    await this.reviewService.setParams(FilteringParameters.mostRated,
      this.reviewService.count)
    await this.checkCountInput()
  }

  async checkCountInput() {
    let count = this.countInput.get('count')?.value
    if (count == 0 || count == null) {
        this.reviewService.getParams()
        this.reviewService.getAllReviews()
    } else {
      await this.reviewService.setParams(this.reviewService.filtrate, count)
      this.reviewService.getAllReviews()
    }
  }
}
