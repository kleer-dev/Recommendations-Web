import {Injectable} from "@angular/core";
import {filterBy} from '@progress/kendo-data-query';
import {ReviewUserPageModel} from "../../models/ReviewUserPageModel";
import {NgbDate} from "@ng-bootstrap/ng-bootstrap";
import {Log} from "oidc-client";

@Injectable({
  providedIn: 'root'
})
export class FiltrationReviewService {

  filtrateData(fieldName: string, filterText: string,
               data: ReviewUserPageModel[]): ReviewUserPageModel[] {
    return filterBy(data, {
      logic: 'and',
      filters: [
        {field: fieldName, value: filterText, operator: 'contains', ignoreCase: true}
      ]
    })
  }
}

