import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterComponent } from '@app/components';

@Component({
  selector: 'app-tos',
  standalone: true,
  imports: [MatButtonModule, FooterComponent],
  templateUrl: './tos.component.html',
  styleUrl: './tos.component.scss'
})
export class TosComponent {
  yearnow = new Date().getUTCFullYear();

}
