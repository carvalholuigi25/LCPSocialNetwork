import { Component } from '@angular/core';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-footerpages',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './footerpages.component.html',
  styleUrl: './footerpages.component.scss'
})
export class FooterPagesComponent {
  yearnow = new Date().getUTCFullYear();
}
