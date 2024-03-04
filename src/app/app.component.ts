import { Component, ViewChild } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgcCookieConsentService, NgcInitializationErrorEvent, NgcInitializingEvent } from 'ngx-cookieconsent';
import { Subscription } from 'rxjs';
import { SharedModule } from './modules';
import { AuthService } from './services/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SharedModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'LCPSocialNetwork';
  user?: any;
  isMobile = true;
  isCollapsed = true;
  isNavMenuHiddenForPages = true;
  rname?: string;
  @ViewChild(MatSidenav) sidenav!: MatSidenav;
  
  //keep refs to subscriptions to be able to unsubscribe later
  private initializingSubscription!: Subscription;
  private initializedSubscription!: Subscription;
  private initializationErrorSubscription!: Subscription;

  constructor(
    private ccService: NgcCookieConsentService, 
    private translateService: TranslateService, 
    private authService: AuthService, 
    private observer: BreakpointObserver,
    private router: Router
  ) { 
    this.router.events.subscribe((rd:any) => { 
      this.isNavMenuHiddenForPages = ["/auth", "/auth/login", "/auth/register", "/", "/home", "/tos", "/privacypolicy", "/codeconduct", "/cookiepolicy"].includes(rd.url) ? true : false;
    });
    this.authService.user.subscribe(x => this.user = x);
  }

  ngOnInit() {
    this.LoadCookieConsent();
    this.LoadTranslateService();
    this.LoadMediaObserver();
  }

  ngOnDestroy() {
    this.UnloadCookieConsent();
  }

  logout() {
    this.authService.logout();
  }

  DoPreventMenuCloseAfterClicked($event:any){
    $event.stopPropagation();
    //Another instructions
  }

  toggleMenu() {
    if(this.isMobile){
      this.sidenav.toggle();
      this.isCollapsed = false; // On mobile, the menu can never be collapsed
    } else {
      this.sidenav.open(); // On desktop/tablet, the menu can never be fully closed
      this.isCollapsed = !this.isCollapsed;
    }
  }

  LoadMediaObserver() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if(screenSize.matches){
        this.isMobile = true;
      } else {
        this.isMobile = false;
      }
    });
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
