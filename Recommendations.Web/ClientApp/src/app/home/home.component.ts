import {Component} from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {FilteringParameters} from "../../common/consts/FilteringParameters";
import {ActivatedRoute, Router} from "@angular/router";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {TagService} from "../../common/services/tag/tag-service";
import {UserService} from "../../common/services/user/user.service";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['home.component.css']
})
export class HomeComponent {

  waiter: boolean = false;
  tags: any

  constructor(public reviewService: ReviewsService,
              private activateRoute: ActivatedRoute,
              private router: Router,
              private tagService: TagService,
              private userService: UserService) {
  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  countInput = new FormGroup({
    'count': new FormControl()
  })

  async ngOnInit() {
    await this.reviewService.getAllReviews()
    await this.getTags()
  }

  async getTags(){
    this.tags = await firstValueFrom(this.tagService.getAllTags())
    this.waiter = true
  }

  async getRecentReviews() {
    await this.reviewService.setParams(FilteringParameters.recent,
      this.reviewService.count, this.reviewService.tag)
    await this.checkCountInput()
  }

  async getMostRatedReviews() {
    await this.reviewService.setParams(FilteringParameters.mostRated,
      this.reviewService.count, this.reviewService.tag)
    await this.checkCountInput()
  }

  async resetTag(){
    this.reviewService.tag = undefined
    await this.reviewService.setParams(this.reviewService.filtrate,
      this.reviewService.count, undefined)
    this.reviewService.reviews = []
    this.reviewService.getAllReviews()
  }

  async checkCountInput() {
    let count = this.countInput.get('count')?.value
    if (count == 0 || count == null) {
      this.reviewService.reviews = []
      await this.reviewService.getAllReviews()
    } else {
      await this.reviewService.setParams(this.reviewService.filtrate, count, this.reviewService.tag)
      this.reviewService.reviews = []
      await this.reviewService.getAllReviews()
    }
  }

  async onTagSelect(event: any) {
    let tagName = event.target.value
    await this.reviewService.setParams(this.reviewService.filtrate,
      this.reviewService.count, tagName)
    this.reviewService.reviews = []
    await this.reviewService.getAllReviews()
  }
}
