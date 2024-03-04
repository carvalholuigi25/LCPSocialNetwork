import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { SharedModule } from './modules';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HttpClient, provideHttpClient, withFetch } from '@angular/common/http';
import {NgcCookieConsentConfig, provideNgcCookieConsent} from 'ngx-cookieconsent';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

const cookieConfig: NgcCookieConsentConfig = {
  cookie: {
    domain: "localhost",
  },
  position: "bottom",
  palette: {
    popup: {
      background: "#000000",
      text: "#ffffff",
      link: "#ffffff"
    },
    button: {
      background: "#f1d600",
      text: "#000000",
      border: "transparent"
    }
  },
  theme: 'edgeless',
  type: "opt-out",
  layout: 'my-custom-layout',
  layouts: {
    "my-custom-layout": '{{messagelink}}{{compliance}}'
  },
  elements: {
    messagelink: `
    <span id="cookieconsent:desc" class="cc-message">{{message}} 
      <a aria-label="learn more about cookies" tabindex="0" class="cc-link" href="{{cookiePolicyHref}}" target="_blank" rel="noopener">{{cookiePolicyLink}}</a>, 
      <a aria-label="learn more about our privacy policy" tabindex="1" class="cc-link" href="{{privacyPolicyHref}}" target="_blank" rel="noopener">{{privacyPolicyLink}}</a> and our 
      <a aria-label="learn more about our terms of service" tabindex="2" class="cc-link" href="{{tosHref}}" target="_blank" rel="noopener">{{tosLink}}</a>
    </span>
    `,
    compliance: `<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Incidunt maxime voluptas neque optio quidem voluptate nam perspiciatis, vero, aliquam facilis eius. Dolores consectetur aliquam veniam harum inventore. Modi, natus ipsum?</p>`
  },
  content: {
    message: 'By using our site, you acknowledge that you have read and understand our ',
    cookiePolicyLink: 'Cookie Policy',
    cookiePolicyHref: 'https://localhost:4200/cookiepolicy',
    privacyPolicyLink: 'Privacy Policy',
    privacyPolicyHref: 'https://localhost:4200/privacypolicy',
    tosLink: 'Terms of Service',
    tosHref: 'https://localhost:4200/tos',
    header: "Cookies used on the website!",
    dismiss: "Got it!",
    allow: "Allow cookies",
    deny: "Decline",
    link: "Learn more",
    policy: "Cookie Policy"
  }
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideClientHydration(), 
    provideHttpClient(withFetch()),
    SharedModule, 
    provideNgcCookieConsent(cookieConfig), 
    importProvidersFrom(TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: createTranslateLoader,
          deps: [HttpClient]
    }
    })), 
    provideAnimationsAsync()
  ]
};
