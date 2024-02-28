import { Component } from '@angular/core';
import { NavbarComponent, FooterComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, SharedModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {

}
