import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

}
