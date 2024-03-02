import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FooterPagesComponent } from '@app/components';

@Component({
  selector: 'app-codeconduct',
  standalone: true,
  imports: [MatButtonModule, FooterPagesComponent],
  templateUrl: './codeconduct.component.html',
  styleUrl: './codeconduct.component.scss'
})
export class CodeConductComponent {
  yearnow = new Date().getUTCFullYear();

}
