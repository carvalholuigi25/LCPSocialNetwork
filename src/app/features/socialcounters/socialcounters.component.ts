import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReactionsDialogComponent } from '@app/dialogs';
import { Post, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { Observable } from 'rxjs';
import { AlertsService, CommentService, PostsService, ReactionsService, SharesService } from '@app/services';

@Component({
  selector: 'app-socialcounters',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './socialcounters.component.html',
  styleUrl: './socialcounters.component.scss'
})
export class SocialcountersComponent implements OnInit {
  dataPosts?: Post[] | any;
  dataUsers?: User[] | any;
  reactionId: number = 0;
  ctReactions: number = 0;
  reactionTypeName: string = "like";
  reactionTypeValue: string = "thumb_up";
  numReactions$: Observable<number> = new Observable<number>();
  numComments$: Observable<number> = new Observable<number>();
  numShares$: Observable<number> = new Observable<number>();
  reactionsData$: Observable<any> = new Observable<any>();
  
  @Input("AvatarId") avatarId: number = 1;
  @Input("PostId") postId: number = -1;
  @Input("UserId") userId: number = -1;
  @Input("DataPosts") mydataPosts: Post[] | any = [];

  constructor(
    private commentsService: CommentService, private reactionsService: ReactionsService, private sharesService: SharesService, 
    private alertsService: AlertsService, private postsService: PostsService, public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.reactionsData$ = this.reactionsService.getDataLocal();
    this.getCounters();
  }

  getCounters() {
    this.numReactions$ = this.postId ? this.reactionsService.getCountByPostId(this.postId) : this.reactionsService.getCount();
    this.numComments$ = this.commentsService.getCount();
    this.numShares$ = this.sharesService.getCount();
  }

  setReactionType(reactionData: any, ind: number) {
    this.reactionId = ind;
    this.reactionTypeName = reactionData.name;
    this.reactionsData$.subscribe(xrd => {
      this.reactionTypeValue = xrd.filter((y: any) => y.name == reactionData.name)[0].value.toString();
    });

    if(this.reactionTypeValue !== "") {
      this.createReactions(this.reactionTypeName, ind);
      this.updatePosts(ind, this.mydataPosts[ind-1]);
    }
  }

  createReactions(reactionTypeName: string, pId: number) {
    this.ctReactions++;

    this.reactionsService.createReactions({
      reactionType: reactionTypeName,
      reactionCounter: this.ctReactions,
      dateReacted: new Date().toISOString(),
      userId: this.avatarId,
      postId: pId
    }).subscribe({
      next: (r) => {
        this.alertsService.openAlert(`Created reaction ${this.reactionTypeName} for post ${pId}!`, 1, "success");
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  updatePosts(id: number, dataPosts: any) {
    const postsdata = {
      PostId: id,
      Title: dataPosts.title,
      Description: dataPosts.description,
      ImgUrl: dataPosts.imgUrl,
      Status: dataPosts.status,
      DatePostCreated: dataPosts.datePostCreated,
      DatePostUpdated: dataPosts.datePostUpdated,
      DatePostDeleted: dataPosts.datePostDeleted,
      TypeTxtPost: dataPosts.typeTxtPost,
      IsFeatured: dataPosts.isFeatured,
      UserId: dataPosts.userId,
      CommentId: dataPosts.commentId,
      ReplyId: dataPosts.replyId,
      ShareId: dataPosts.shareId,
      ReactionId: dataPosts.reactionId,
      AttachmentId: dataPosts.attachmentId
    };

    this.postsService.updatePosts(id, postsdata).subscribe({
      next: (r) => {
        this.alertsService.openAlert(`Updated post with id: ${id}!`, 1, "success");
        location.reload();
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  openReactionMenu() {
    this.dialog.open(ReactionsDialogComponent);
  }
}
