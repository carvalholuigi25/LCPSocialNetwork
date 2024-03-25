import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-cookiepolicy',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './cookiepolicy.component.html',
  styleUrl: './cookiepolicy.component.scss'
})
export class CookiePolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
