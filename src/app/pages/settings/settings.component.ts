import { Component } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {

}
