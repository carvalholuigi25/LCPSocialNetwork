import { Component } from '@angular/core';
import { FooterComponent, NavbarComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, SharedModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {

}
