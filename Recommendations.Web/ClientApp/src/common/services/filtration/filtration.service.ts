import {Injectable} from "@angular/core";
import { filterBy } from '@progress/kendo-data-query';
import {ReviewUserPageModel} from "../../models/ReviewUserPageModel";

@Injectable({
  providedIn: 'root'
})
export class FiltrationService {

  filtrateData(fieldName: string, filterText: string,
               data: ReviewUserPageModel[]): ReviewUserPageModel[]{

    return filterBy(data , {
      logic: 'and',
      filters: [
        {field: fieldName, value: filterText, operator: 'contains', ignoreCase: true}
      ]
    })
  }

}
