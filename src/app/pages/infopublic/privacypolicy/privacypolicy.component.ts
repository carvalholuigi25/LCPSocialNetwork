import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-privacypolicy',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './privacypolicy.component.html',
  styleUrl: './privacypolicy.component.scss'
})
export class PrivacyPolicyComponent {
  yearnow = new Date().getUTCFullYear();

}
