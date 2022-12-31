import {Component, Input} from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {FilteringParameters} from "../../common/consts/FilteringParameters";
import {ActivatedRoute, Router} from "@angular/router";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {TagService} from "../../common/services/tag/tag-service";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-reviews-grid',
  templateUrl: 'review-grid.component.html',
  styleUrls: ['review-grid.component.css']
})
export class ReviewGridComponent {
  @Input() public reviews: any;
  @Input() ReviewForm!: FormGroup

}
