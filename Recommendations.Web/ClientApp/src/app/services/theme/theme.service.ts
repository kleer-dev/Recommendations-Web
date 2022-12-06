import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface ThemeObject {
  oldValue?: string;
  newValue?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  savedTheme = ""

  initialSetting: ThemeObject = {
    oldValue: "",
    newValue: 'bootstrap'
  };

  themeSelection: BehaviorSubject<ThemeObject> =  new BehaviorSubject<ThemeObject>(this.initialSetting);

  constructor() {
    this.savedTheme = localStorage.getItem('theme')!

    if (this.savedTheme == 'dark') {
      this.initialSetting.newValue = 'bootstrap-dark'
    }
    else {
      this.initialSetting.newValue = 'bootstrap'
    }
  }

  setTheme(theme: string) {

    this.themeSelection.next(
      {
        oldValue: this.themeSelection.value.newValue,
        newValue: theme
      });
  }

  themeChanges(): Observable<ThemeObject> {
    return this.themeSelection.asObservable();
  }
}
