import { Component } from '@angular/core';
import { SharedModule } from '../../modules';
import { NavbarComponent, FooterComponent } from '../../components';

@Component({
  selector: 'app-newsfeed',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, SharedModule],
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.scss'
})
export class NewsfeedComponent {

}
