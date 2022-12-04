import {Component, OnInit} from '@angular/core';
import {ThemeService} from 'src/app/services/theme.service';

@Component({
  selector: 'app-theme-toggle',
  templateUrl: 'theme-toggle.component.html',
  styleUrls: ['theme-toggle.component.css']
})
export class ThemeToggleComponent implements OnInit {
  savedTheme = ''
  theme: string = 'bootstrap';

  constructor(private themeService: ThemeService) {
    this.getSavedTheme()
  }

  ngOnInit(): void {
  }

  toggleTheme() {
    if (this.theme === 'bootstrap') {
      this.theme = 'bootstrap-dark';
      localStorage.setItem('theme', 'dark')
    } else {
      this.theme = 'bootstrap';
      localStorage.setItem('theme', 'light')
    }

    this.themeService.setTheme(this.theme)
  }

  getSavedTheme() {
    this.savedTheme = localStorage.getItem('theme')!
    if (this.savedTheme !== undefined) {
      switch (this.savedTheme) {
        case 'dark':
          this.theme = 'bootstrap-dark'
          break;

        case 'light':
          this.theme = 'bootstrap'
          break;
      }
    }
  }

}
