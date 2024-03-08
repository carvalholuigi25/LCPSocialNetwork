import { Component } from '@angular/core';
import { ThemeswitchComponent } from '@app/features';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [ThemeswitchComponent, SharedModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  yearnow = new Date().getUTCFullYear();
}
