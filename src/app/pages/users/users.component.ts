import { Component } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

}
