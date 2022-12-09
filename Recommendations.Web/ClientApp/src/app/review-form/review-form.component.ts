import {Component, Input, Output} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {CategoryService} from "src/common/services/category/category-service";
import {TagService} from "src/common/services/tag/tag-service";
import {FormControl} from "@angular/forms";
import {map, Observable} from "rxjs";
import {TagModel} from "ngx-chips/core/tag-model";
import {ReviewFormModel} from "src/common/models/ReviewFormModel";

@Component({
  selector: 'app-review-form',
  templateUrl: 'review-form.component.html',
  styleUrls: ['review-form.component.css']
})
export class ReviewFormComponent {
  file?: File;
  rate = 1;
  tags: string[] = []
  categories!: string[]

  @Input() @Output() reviewForm!: ReviewFormModel;
  @Input() onSubmitForm!: Function;

  constructor(private http: HttpClient, private router: Router,
              private categoryService: CategoryService,
              private tagService: TagService) {
    this.getAllCategories()
  }

  getAllCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: value => this.categories = value.map(function (a: any) {
        return a.name;
      })
    });
  }

  value: string = ""
  onChangeMarkdownEditor() {
    this.value = (this.reviewForm.get('description')!.value!).replace(/(?:\r\n|\r|\n)/g, `  \n`)
  }

  get descriptionRawControl() {
    this.reviewForm.patchValue({
      description: this.reviewForm?.controls.description.value
    })
    return this.reviewForm?.controls.description as FormControl;
  }

  onSelectImage(event: any) {
    if (event.addedFiles[0] === undefined)
      return;

    this.file = <File>event.addedFiles[0]
    this.reviewForm.patchValue({
      image: <any>this.file
    })
  }

  onRemoveImage(event: any) {
    this.file = undefined;
  }

  requestAutocompleteTags = (text: any): Observable<any> => {
    return this.tagService.getAllTags().pipe(
      map((data: any) => {
        return data;
      }))
  }

  onTagAdd(tag: TagModel) {
    this.tags.push((<any>tag).value);
    this.reviewForm.patchValue({
      tags: this.tags
    })
  }

  onTagRemove(tag: any) {
    let index = this.tags.indexOf((<any>tag).value)
    if (index != -1) {
      this.tags.splice(index, 1)
    }

    this.reviewForm.patchValue({
      tags: this.tags
    })
  }

  onRateChange(e: number) {
    this.rate = e
    this.reviewForm.patchValue({
      rate: this.rate
    })
  }

  onSubmit(){
    this.onSubmitForm(this.reviewForm)
  }
}

