import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-cookiepolicy',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './cookiepolicy.component.html',
  styleUrl: './cookiepolicy.component.scss'
})
export class CookiePolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
