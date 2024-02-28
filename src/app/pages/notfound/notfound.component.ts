import { Component } from '@angular/core';
import { FooterComponent, NavbarComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-notfound',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, SharedModule],
  templateUrl: './notfound.component.html',
  styleUrl: './notfound.component.scss'
})
export class NotfoundComponent {

}
