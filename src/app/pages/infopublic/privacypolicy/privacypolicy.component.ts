import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterComponent } from '@app/components';

@Component({
  selector: 'app-privacypolicy',
  standalone: true,
  imports: [MatButtonModule, FooterComponent],
  templateUrl: './privacypolicy.component.html',
  styleUrl: './privacypolicy.component.scss'
})
export class PrivacyPolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
