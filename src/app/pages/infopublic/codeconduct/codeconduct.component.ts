import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-codeconduct',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './codeconduct.component.html',
  styleUrl: './codeconduct.component.scss'
})
export class CodeConductComponent {
  yearnow = new Date().getUTCFullYear();

}
