import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterPagesComponent } from '@app/components';

@Component({
  selector: 'app-cookiepolicy',
  standalone: true,
  imports: [MatButtonModule, FooterPagesComponent],
  templateUrl: './cookiepolicy.component.html',
  styleUrl: './cookiepolicy.component.scss'
})
export class CookiePolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
