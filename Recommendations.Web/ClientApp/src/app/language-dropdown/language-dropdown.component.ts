import {Component, HostBinding, Input, OnInit} from "@angular/core";
import '@github/markdown-toolbar-element'
import {FormControl} from "@angular/forms";
import {environment} from "../../environments/environment";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'language-dropdown',
  templateUrl: 'language-dropdown.component.html',
  styleUrls: ['language-dropdown.component.css']
})
export class LanguageDropdownComponent implements OnInit{

  constructor(private translateService: TranslateService) { }

  ngOnInit(): void {
    let lang: string | null = this.getLanguageFromLocalStorage();
    if (lang === null)
      this.translateService.use(environment.defaultLocale);
    else
      this.changeLanguage(lang);
  }

  changeLanguage(language: string) {
    this.translateService.use(language);
    this.saveLanguageToLocalStorage(language);
  }

  saveLanguageToLocalStorage(language: string) {
    localStorage.setItem('language', language);
  }

  getLanguageFromLocalStorage(): string | null {
    return localStorage.getItem('language');
  }
}
