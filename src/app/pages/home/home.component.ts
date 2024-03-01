import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  yearnow = new Date().getUTCFullYear();
  user?: any;

  constructor(private authService: AuthService) {
      this.authService.user.subscribe(x => this.user = x);
  }
}
