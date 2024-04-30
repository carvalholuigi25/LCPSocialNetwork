import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { ReactionsService } from '@app/services';
import { Observable, Subscription, of } from 'rxjs';

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
export class ReactionsDialogComponent implements OnInit, OnDestroy {
  isReactionIconEnabled: boolean = true;
  reactionsDataLocal$: Observable<any> = new Observable<any>();
  reactionsData$: Observable<any> = new Observable<any>();
  reactionTypeValue: string = "like";
  reactionTypeAry: string[] = [];
  reactionTypeAryLen: number = 0;
  myindex: number = 0;
  private mysub: Subscription = new Subscription();
  private mysub2: Subscription = new Subscription();

  constructor(private reactionsService: ReactionsService, @Inject(MAT_DIALOG_DATA) public data: ReactionsDialogData) { }

  ngOnInit(): void {
    this.getReactions();
  }

  getReactionsWithUsers() {
    this.mysub = this.reactionsService.getAllWithUsers().subscribe({
      next: (r) => {
        let mydata = this.reactionTypeValue == "all" ? r[0].filter(x => x.postId == this.data.postId) : r[0].filter(x => x.reactionType == this.reactionTypeValue && x.postId == this.data.postId);
        this.reactionTypeAryLen = mydata.length;
        this.reactionsData$ = of(mydata);
      },
      error: (err) => console.error(err)
    });
  }

  getReactions() {
    this.reactionsDataLocal$ = this.reactionsService.getDataLocal();
    this.mysub2 = this.reactionsDataLocal$.subscribe({
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

  ngOnDestroy() {
    if(this.mysub) {
      this.mysub.unsubscribe();
    }

    if(this.mysub2) {
      this.mysub2.unsubscribe();
    }
  }
}
