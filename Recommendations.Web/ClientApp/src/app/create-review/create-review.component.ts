import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {ReviewFormModel} from "src/common/models/ReviewFormModel";
import {formToFormData} from "src/common/functions/formToFormData";

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['create-review.component.css']
})
export class CreateReviewComponent {

  constructor(private http: HttpClient, private router: Router) {

  }

  reviewForm : ReviewFormModel = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(100)
    ]),
    productName: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(100)
    ]),
    categoryName: new FormControl('', [
      Validators.required,
      Validators.minLength(2)
    ]),
    tags: new FormControl(new Array<string>(), [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(100),
      Validators.maxLength(20000)
    ]),
    authorRate: new FormControl(1),
    image: new FormControl(new File([], ''))
  })

  onSubmitForm() {
    this.http.post("api/reviews", formToFormData(this.reviewForm.value))
      .subscribe({
        next: _ => this.router.navigate(['/']),
        error: err => {
          console.error(err)
        }
      })
  }
}


