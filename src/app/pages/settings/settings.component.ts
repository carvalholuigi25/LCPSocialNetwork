import { Component } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';
import { ThemeswitchComponent } from '@app/features';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [FooterComponent, ThemeswitchComponent, SharedModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {

}
