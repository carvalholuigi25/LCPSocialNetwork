import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterComponent } from '@app/components';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [MatButtonModule, FooterComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  yearnow = new Date().getUTCFullYear();

}
