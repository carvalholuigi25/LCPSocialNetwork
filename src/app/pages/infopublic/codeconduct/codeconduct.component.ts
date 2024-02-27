import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-codeconduct',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './codeconduct.component.html',
  styleUrl: './codeconduct.component.scss'
})
export class CodeConductComponent {
  yearnow = new Date().getUTCFullYear();

}
