import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Post, Share } from '@app/models';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { AlertsService, PostsService, SharesService } from '@app/services';
import { Observable } from 'rxjs';

export interface DialogData {
  postId: number;
}

@Component({
  selector: 'app-shares',
  standalone: true,
  imports: [SharedModule, SafePipe],
  templateUrl: './sharesdialog.component.html',
  styleUrl: './sharesdialog.component.scss'
})
export class SharesDialogComponent implements OnInit {
  dataPosts: Post[] | any = [];
  shareCounter: number = 0;
  numShares$: Observable<any> = new Observable<any>();

  constructor(private postsService: PostsService, private sharesService: SharesService, private alertsService: AlertsService, @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void { 
    this.getPosts();
    this.numShares$ = this.data.postId ? this.sharesService.getCountByPostId(this.data.postId) : this.sharesService.getCount();
  }

  getPosts() {
    if(this.data.postId) {
      this.postsService.getAllById(this.data.postId).subscribe({
        next: (data) => {
          this.dataPosts = data;
          this.shareCounter++;
        },
        error: (em) => console.log(em)
      });
    }
  }

  shareThisPost() {
    const datacrpost: Share = {
      shareCounter: this.shareCounter,
      postId: this.data.postId,
      dateShared: new Date().toISOString(),
      attachmentId: this.dataPosts.attachmentId,
      commentId: this.dataPosts.commentId,
      replyId: this.dataPosts.replyId,
      userId: this.dataPosts.userId
    };

    this.sharesService.createShares(datacrpost).subscribe({
      next: (data) => {
        this.alertsService.openAlert(`Shared post (id: ${datacrpost.postId})!`, 1, "success");
        location.reload();
      },
      error: (em) => console.log(em)
    });
  }

  removeThisShare() {
    this.sharesService.deleteAllShares().subscribe({
      next: (data) => {
        this.alertsService.openAlert(`Removed all shared posts`, 1, "success");
        location.reload();
      },
      error: (em) => console.log(em)
    });
  }
}
