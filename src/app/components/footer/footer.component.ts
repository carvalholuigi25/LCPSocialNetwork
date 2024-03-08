import { Component } from '@angular/core';
import { ThemeswitchComponent } from '@app/features';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [ThemeswitchComponent],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  yearnow = new Date().getUTCFullYear();
}
