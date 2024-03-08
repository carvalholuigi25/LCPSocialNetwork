import { Component } from '@angular/core';
import { ThemeswitchComponent } from '@app/features';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-footerpages',
  standalone: true,
  imports: [ThemeswitchComponent, SharedModule],
  templateUrl: './footerpages.component.html',
  styleUrl: './footerpages.component.scss'
})
export class FooterPagesComponent {
  yearnow = new Date().getUTCFullYear();
}
