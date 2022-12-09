import {Component} from '@angular/core';

@Component({
  selector: 'app-external-login',
  templateUrl: './external-login.component.html',
})
export class ExternalLoginComponent {

  constructor() {

  }

  externalAuth(provider: string) {
    window.location.href = `api/user/external-login?provider=${provider}`;
  }
}
