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

    data.forEach(value => {
      let date = new Date(value.creationDate)
      value.creationDate = date.toLocaleTimeString('en-US', {
        day: '2-digit',
        month: '2-digit',
        year: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        timeZone: 'Etc/GMT-3'
      }).toString();
    });

    return filterBy(data, {
      logic: 'and',
      filters: [
        {field: fieldName, value: filterText, operator: 'contains', ignoreCase: true}
      ]
    })
  }

}

