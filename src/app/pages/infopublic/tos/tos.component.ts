import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-tos',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './tos.component.html',
  styleUrl: './tos.component.scss'
})
export class TosComponent implements OnInit {
  yearnow = new Date().getUTCFullYear();

  ngOnInit(): void {
      
  }
}
