import {Component, HostBinding, Input, OnInit} from "@angular/core";
import '@github/markdown-toolbar-element'
import {FormControl} from "@angular/forms";

@Component({
  selector: 'markdown-editor',
  templateUrl: './markdown-editor.component.html',
  styleUrls: ['markdown-editor.component.scss']
})
export class MarkdownEditorComponent implements OnInit {
  controlId?: string
  @Input() control: FormControl = new FormControl();
  @HostBinding('class.focus') isFocus?: boolean;

  constructor() { }

  ngOnInit(): void {
    this.controlId = `MarkdownEditor-${Math.floor(100000 * Math.random())}`;
    this.control = this.control ?? new FormControl();
  }

  focus() {
    this.isFocus = true;
  }

  blur() {
    this.isFocus = false;
  }

  onChange(){

  }
}
