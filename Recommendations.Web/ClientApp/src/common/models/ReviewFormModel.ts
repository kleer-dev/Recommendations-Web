import {FormControl, FormGroup} from "@angular/forms";

export interface ReviewFormModel extends FormGroup {
  controls: {
    title: FormControl,
    productName: FormControl,
    category: FormControl,
    tags: FormControl,
    description: FormControl,
    rate: FormControl,
    image: FormControl
  }
}
