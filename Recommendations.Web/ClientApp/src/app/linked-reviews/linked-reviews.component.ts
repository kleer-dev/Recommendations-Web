import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Route, Router} from "@angular/router";
import {LinkedReviewModel} from "../../common/models/LinkedReviewModel";

@Component({
  selector: 'app-linked-reviews',
  templateUrl: 'linked-reviews.component.html',
  styleUrls: ['linked-reviews.component.css']
})
export class LinkedReviewsComponent implements OnInit {

  @Input() linkedReviews!: LinkedReviewModel[]

  constructor() {

  }

  ngOnInit(): void {

  }

}


