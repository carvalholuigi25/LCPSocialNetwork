import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterPagesComponent } from '@app/components';

@Component({
  selector: 'app-tos',
  standalone: true,
  imports: [MatButtonModule, FooterPagesComponent],
  templateUrl: './tos.component.html',
  styleUrl: './tos.component.scss'
})
export class TosComponent {
  yearnow = new Date().getUTCFullYear();

}
