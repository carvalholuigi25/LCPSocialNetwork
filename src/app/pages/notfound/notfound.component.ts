import { Component } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-notfound',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './notfound.component.html',
  styleUrl: './notfound.component.scss'
})
export class NotfoundComponent {

}
