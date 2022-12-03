import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface ThemeObject {
  oldValue: string;
  newValue: string;
}

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  initialSetting: ThemeObject = {
    oldValue: "",
    newValue: 'bootstrap'
  };

  themeSelection: BehaviorSubject<ThemeObject> =  new BehaviorSubject<ThemeObject>(this.initialSetting);

  constructor() { }

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
