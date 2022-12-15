import {FormControl, FormGroup} from "@angular/forms";

export interface ReviewFormModel extends FormGroup {
  controls: {
    title: FormControl,
    productName: FormControl,
    categoryName: FormControl,
    tags: FormControl,
    description: FormControl,
    authorRate: FormControl,
    image: FormControl
  }
}
