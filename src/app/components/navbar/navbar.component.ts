import { Component } from '@angular/core';
import { SharedModule } from '../../modules';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  user?: any;

  constructor(private authService: AuthService) {
      this.authService.user.subscribe(x => this.user = x);
  }

  logout() {
      this.authService.logout();
  }
}
