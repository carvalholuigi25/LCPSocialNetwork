import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  yearnow = new Date().getUTCFullYear();

}
