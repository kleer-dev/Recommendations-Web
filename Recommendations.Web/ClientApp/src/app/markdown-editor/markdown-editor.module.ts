import {CommonModule} from "@angular/common";
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {MarkdownEditorComponent} from "./markdown-editor.component";
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  imports: [CommonModule, ReactiveFormsModule],
  exports: [MarkdownEditorComponent],
  declarations: [MarkdownEditorComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MarkdownEditorModule {

}

