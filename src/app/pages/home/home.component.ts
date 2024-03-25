import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FooterComponent, SharedModule],
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
