import { Component } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {

}
