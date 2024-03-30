import { Subscription } from 'rxjs';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgcCookieConsentService, NgcInitializingEvent, NgcInitializationErrorEvent } from 'ngx-cookieconsent';
import { LanguagesService } from '@app/services';

@Component({
  selector: 'app-cookieconsent',
  standalone: true,
  imports: [],
  template: ''
})
export class CookieConsentComponent {
  initializingSubscription!: Subscription;
  initializedSubscription!: Subscription;
  initializationErrorSubscription!: Subscription;

  langAry!: any;
  langCodeIsoAry: any[] = [];

  constructor(
    private ccService: NgcCookieConsentService, 
    private translateService: TranslateService,
    private langService: LanguagesService
  ) {}

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
    this.langService.getListLanguages().subscribe({
      next: (rl: any) => {
        this.langAry = rl;
        
        if(this.langAry) {
          this.langCodeIsoAry = [];

          for(var i = 0; i < this.langAry.length; i++) {
            this.langCodeIsoAry.push(this.langAry[i].countryCodeIso);
          }

          this.translateService.addLangs(this.langCodeIsoAry);
          this.translateService.setDefaultLang(this.langCodeIsoAry[0]);
      
          this.translateService.get(['cookie.header', 'cookie.message', 'cookie.dismiss', 'cookie.allow', 'cookie.deny', 'cookie.link', 'cookie.policy']).subscribe(data => {
            if(this.ccService.getConfig() != null && this.ccService.getConfig().content != null) {
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
      },
      error: (er: any) => {
        console.log(er.message);
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
