import { Component } from '@angular/core';
import { FooterComponent, NavbarComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, SharedModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

}
