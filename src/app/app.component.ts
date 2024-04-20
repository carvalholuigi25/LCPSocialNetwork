import { Component, OnInit, ViewChild } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { SharedModule } from './modules';
import { AuthService } from './services/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';
import { CookieConsentComponent, FriendsrequestsComponent, NotificationsComponent } from './features';
import { AlertsService, ChatMessagesService, FriendsRequestsService, NotificationsService, ThemesService } from './services';
import { Observable, filter } from 'rxjs';
import { LanguagesService } from '@app/services';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CookieConsentComponent, NotificationsComponent, FriendsrequestsComponent, SharedModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'LCPSocialNetwork';
  user?: any;
  isMobile = true;
  isCollapsed = true;
  isNavMenuHiddenForPages = true;
  rname?: string;
  friendRequestsCounter$: Observable<number> = new Observable<number>();
  notificationsCounter$: Observable<number> = new Observable<number>();
  chatMessagesCounter$: Observable<number> = new Observable<number>();

  @ViewChild(MatSidenav) sidenav!: MatSidenav;
  
  constructor(
    private authService: AuthService, 
    private observer: BreakpointObserver,
    private router: Router,
    private alertsService: AlertsService,
    private themesService: ThemesService,
    private languagesService: LanguagesService, 
    private friendRequestsService: FriendsRequestsService,
    private notificationsService: NotificationsService,
    private chatMessagesService: ChatMessagesService,
    public translate: TranslateService
  ) { 
    this.translate.use(this.languagesService.getLanguage()! ?? "en");
    this.DoRouterStuff();
  }

  ngOnInit() {
    this.themesService.setTheme(this.themesService.getTheme()!);
    this.LoadMediaObserver();
    this.LoadCounters();
  }

  logout() {
    this.alertsService.openAlert(`Logged out!`, 1, "success");
    this.authService.logout();
  }

  DoPreventMenuCloseAfterClicked($event:any){
    if($event) {
      $event.stopPropagation();
    }
  }

  toggleMenu() {
    if(this.isMobile){
      if(this.sidenav) {
        this.sidenav.toggle();
      }

      this.isCollapsed = false; // On mobile, the menu can never be collapsed
    } else {
      if(this.sidenav) {
        this.sidenav.open(); // On desktop/tablet, the menu can never be fully closed
      }
      
      this.isCollapsed = !this.isCollapsed;
    }
  }

  LoadCounters() {
    this.friendRequestsCounter$ = this.friendRequestsService.getCount();
    this.notificationsCounter$ = this.notificationsService.getCount();
    this.chatMessagesCounter$ = this.chatMessagesService.getCount();
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

  DoRouterStuff() {
    this.router.events.pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd)).forEach(rd => { 
      this.isNavMenuHiddenForPages = ["/auth", "/auth/login", "/auth/login?returnUrl=%2Fnewsfeed", "/auth/register", "/", "/home", "/tos", "/privacypolicy", "/codeconduct", "/cookiepolicy"].includes(rd.url) ? true : false;
    });
    this.authService.user.subscribe(x => {
      this.user = x;
    });
  }
}
