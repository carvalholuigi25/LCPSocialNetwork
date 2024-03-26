import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent implements OnInit {
  yearnow = new Date().getUTCFullYear();

  ngOnInit(): void {
      
  }
}
