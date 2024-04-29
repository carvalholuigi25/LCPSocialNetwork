import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { ReactionsService } from '@app/services';
import { Observable, of } from 'rxjs';

export interface ReactionsDialogData {
  postId?: number;
}

@Component({
  selector: 'app-reactions-dialog',
  standalone: true,
  imports: [SharedModule, SafePipe],
  templateUrl: './reactionsdialog.component.html',
  styleUrl: './reactionsdialog.component.scss'
})
export class ReactionsDialogComponent implements OnInit {
  reactionsDataLocal$: Observable<any> = new Observable<any>();
  reactionsData$: Observable<any> = new Observable<any>();
  usersData$: Observable<any> = new Observable<any>();
  reactionTypeValue: string = "like";
  reactionTypeAry: string[] = [];
  reactionTypeAryLen: number = 0;
  myindex: number = 0;
  isReactionIconEnabled: boolean = true;

  constructor(private reactionsService: ReactionsService, @Inject(MAT_DIALOG_DATA) public data: ReactionsDialogData) { }

  ngOnInit(): void {
    this.getReactions();
  }

  getReactionsWithUsers() {
    this.reactionsService.getAllWithUsers().subscribe({
      next: (r) => {
        r[0] = r[0].filter(x => x.reactionType == this.reactionTypeValue && x.postId == this.data.postId);
        this.reactionTypeAryLen = r[0].length;
        this.reactionsData$ = of(r[0]);
        this.usersData$ = of(r[1]);
      },
      error: (err) => console.error(err)
    });
  }

  getReactions() {
    this.reactionsDataLocal$ = this.reactionsService.getDataLocal();
    this.reactionsDataLocal$.subscribe({
      next: (r) => {
        for(var i = 0; i < r.length; i++) {
          this.reactionTypeAry.push(r[i].name);
        }

        this.getReactionsWithUsers();
      },
      error: (err) => console.error(err)
    });
  }

  setReactionTypeValue(value: number) {
    this.myindex = value;
    this.reactionTypeValue = this.reactionTypeAry[value];
    this.getReactionsWithUsers();
  }
}
