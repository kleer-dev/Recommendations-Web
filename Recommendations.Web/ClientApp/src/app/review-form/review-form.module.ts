import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {ReviewFormComponent} from "./review-form.component";
import {MarkdownModule} from "ngx-markdown";
import {MarkdownEditorModule} from "../markdown-editor/markdown-editor.module";
import {TagInputModule} from "ngx-chips";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import {NgxDropzoneModule} from "ngx-dropzone";
import {NgForOf, NgIf} from "@angular/common";
import {TranslateModule} from "@ngx-translate/core";

@NgModule({
    imports: [
        MarkdownModule,
        MarkdownEditorModule,
        TagInputModule,
        FormsModule,
        NgbModule,
        NgxDropzoneModule,
        ReactiveFormsModule,
        NgIf,
        NgForOf,
        TranslateModule
    ],
  exports: [ReviewFormComponent],
  declarations: [
    ReviewFormComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]})

export class ReviewFormModule {

}
