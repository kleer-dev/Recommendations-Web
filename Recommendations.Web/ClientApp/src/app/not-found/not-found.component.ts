import {Component, OnInit} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {ColumnMode} from '@swimlane/ngx-datatable';
import {ReviewUserPageModel} from "../../common/models/ReviewUserPageModel";
import {FormControl, FormGroup} from "@angular/forms";
import {FiltrationService} from "../../common/services/filtration/filtration.service";
import {ActivatedRoute} from "@angular/router";
import {UserModel} from "../../common/models/UserModel";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-not-found',
  templateUrl: 'not-found.component.html',
  styleUrls: ['not-found.component.css']
})
export class NotFoundComponent {

}
