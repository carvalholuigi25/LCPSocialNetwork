import { Component } from '@angular/core';
import { FooterComponent } from '../../../components';
import { SharedModule } from '../../../modules';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

}
