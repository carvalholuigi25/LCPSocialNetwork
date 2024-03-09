import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterComponent } from '@app/components';

@Component({
  selector: 'app-codeconduct',
  standalone: true,
  imports: [MatButtonModule, FooterComponent],
  templateUrl: './codeconduct.component.html',
  styleUrl: './codeconduct.component.scss'
})
export class CodeConductComponent {
  yearnow = new Date().getUTCFullYear();

}
