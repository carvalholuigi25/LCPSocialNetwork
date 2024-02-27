import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-privacypolicy',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './privacypolicy.component.html',
  styleUrl: './privacypolicy.component.scss'
})
export class PrivacyPolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
