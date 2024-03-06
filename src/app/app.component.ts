import { Component, ViewChild } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SharedModule } from './modules';
import { AuthService } from './services/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';
import { CookieConsentComponent } from './features';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CookieConsentComponent, SharedModule],
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
  
  constructor(
    private authService: AuthService, 
    private observer: BreakpointObserver,
    private router: Router
  ) { 
    this.DoRouterStuff();
  }

  ngOnInit() {
    this.LoadMediaObserver();
  }

  logout() {
    this.authService.logout();
  }

  DoPreventMenuCloseAfterClicked($event:any){
    $event.stopPropagation();
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

  DoRouterStuff() {
    this.router.events.subscribe((rd:any) => { 
      this.isNavMenuHiddenForPages = ["/auth", "/auth/login", "/auth/register", "/", "/home", "/tos", "/privacypolicy", "/codeconduct", "/cookiepolicy"].includes(rd.url) ? true : false;
    });
    this.authService.user.subscribe(x => this.user = x);
  }
}
