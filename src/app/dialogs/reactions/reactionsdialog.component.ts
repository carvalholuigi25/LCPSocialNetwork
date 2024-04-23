import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';

@Component({
  selector: 'app-reactions-dialog',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './reactionsdialog.component.html',
  styleUrl: './reactionsdialog.component.scss'
})
export class ReactionsDialogComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    
  }
}
