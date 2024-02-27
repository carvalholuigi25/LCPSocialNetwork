import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-tos',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './tos.component.html',
  styleUrl: './tos.component.scss'
})
export class TosComponent {
  yearnow = new Date().getUTCFullYear();

}
