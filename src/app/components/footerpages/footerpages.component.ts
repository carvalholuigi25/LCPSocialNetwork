import { Component } from '@angular/core';
import { ThemeswitchComponent } from '@app/features';

@Component({
  selector: 'app-footerpages',
  standalone: true,
  imports: [ThemeswitchComponent],
  templateUrl: './footerpages.component.html',
  styleUrl: './footerpages.component.scss'
})
export class FooterPagesComponent {
  yearnow = new Date().getUTCFullYear();
}
