import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { NgcCookieConsentService, NgcInitializationErrorEvent, NgcInitializingEvent, NgcNoCookieLawEvent, NgcStatusChangeEvent } from 'ngx-cookieconsent';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'LCPSocialNetwork';

  //keep refs to subscriptions to be able to unsubscribe later
  private initializingSubscription!: Subscription;
  private initializedSubscription!: Subscription;
  private initializationErrorSubscription!: Subscription;

  constructor(private ccService: NgcCookieConsentService, private translateService: TranslateService) { }

  ngOnInit() {
    this.LoadCookieConsent();
    this.LoadTranslateService();
  }

  ngOnDestroy() {
    this.UnloadCookieConsent();
  }

  LoadCookieConsent() {
    this.initializingSubscription = this.ccService.initializing$.subscribe((event: NgcInitializingEvent) => {
      // the cookieconsent is initilializing... Not yet safe to call methods like `NgcCookieConsentService.hasAnswered()`
      console.log(`initializing: ${JSON.stringify(event)}`);
    });

    this.initializedSubscription = this.ccService.initialized$.subscribe(() => {
      // the cookieconsent has been successfully initialized.
      // It's now safe to use methods on NgcCookieConsentService that require it, like `hasAnswered()` for eg...
      console.log(`initialized: ${JSON.stringify(event)}`);
    });

    this.initializationErrorSubscription = this.ccService.initializationError$.subscribe((event: NgcInitializationErrorEvent) => {
      // the cookieconsent has failed to initialize... 
      console.log(`initializationError: ${JSON.stringify(event.error?.message)}`);
    });
  }

  LoadTranslateService() {
    this.translateService.addLangs(['de', 'en', 'es', 'fr', 'it', 'pt']);
    this.translateService.setDefaultLang('en');

    const browserLang = this.translateService.getBrowserLang();
    this.translateService.use(browserLang?.match(/de|en|es|fr|it|pt/) ? browserLang : 'en');

    this.translateService.get(['cookie.header', 'cookie.message', 'cookie.dismiss', 'cookie.allow', 'cookie.deny', 'cookie.link', 'cookie.policy']).subscribe(data => {
      if(this.ccService.getConfig() != null) {
        this.ccService.getConfig().content!.header = data['cookie.header'];
        this.ccService.getConfig().content!.message = data['cookie.message'];
        this.ccService.getConfig().content!.dismiss = data['cookie.dismiss'];
        this.ccService.getConfig().content!.allow = data['cookie.allow'];
        this.ccService.getConfig().content!.deny = data['cookie.deny'];
        this.ccService.getConfig().content!.link = data['cookie.link'];
        this.ccService.getConfig().content!.policy = data['cookie.policy'];
          
        this.ccService.destroy(); // remove previous cookie bar (with default messages)
        this.ccService.init(this.ccService.getConfig()); // update config with translated messages
      }
    });
  }

  UnloadCookieConsent() {
     // unsubscribe to cookieconsent observables to prevent memory leaks
     this.initializingSubscription.unsubscribe();
     this.initializedSubscription.unsubscribe();
     this.initializationErrorSubscription.unsubscribe();
  }
}
