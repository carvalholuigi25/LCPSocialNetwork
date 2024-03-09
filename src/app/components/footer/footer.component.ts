import { Component } from '@angular/core';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  yearnow = new Date().getUTCFullYear();
}
