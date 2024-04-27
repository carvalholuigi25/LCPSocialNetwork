import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReactionsDialogComponent, SharesDialogComponent } from '@app/dialogs';
import { Post, User, Comment } from '@app/models';
import { SharedModule } from '@app/modules';
import { Observable, of } from 'rxjs';
import { AlertsService, AuthService, CommentService, PostsService, ReactionsService, SharesService } from '@app/services';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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
  commentsData$: Observable<any> = new Observable<any>();
  commentsUsersData$: Observable<any> = new Observable<any>();
  isCommentsShown: boolean = false;
  isCommentsSubmitted: boolean = false;
  commentsFrm!: FormGroup;
  avatarUrl: string = "";
  
  @Input("AvatarId") avatarId: number = 1;
  @Input("PostId") postId: number = -1;
  @Input("UserId") userId: number = -1;
  @Input("DataPosts") mydataPosts: Post[] | any = [];

  constructor(
    private commentsService: CommentService, private reactionsService: ReactionsService, private sharesService: SharesService, 
    private alertsService: AlertsService, private postsService: PostsService, private authService: AuthService, 
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.avatarUrl = this.authService.getCurUserInfoAuth().avatarUrl ?? "";
    this.getCounters();
    this.getReactions();
    this.getComments();
  }

  getReactions() {
    this.reactionsData$ = this.reactionsService.getDataLocal();
  }

  getComments() {
    this.commentsService.getAllWithUsers(this.userId).subscribe({
      next: (data) => {
        this.commentsUsersData$ = of(data[0]);
        this.commentsData$ = of(data[1]);
      },
      error: (em) => console.log(em)
    });
  }

  getCounters() {
    this.numReactions$ = this.postId ? this.reactionsService.getCountByPostId(this.postId) : this.reactionsService.getCount();
    this.numComments$ = this.postId ? this.commentsService.getCountByPostId(this.postId) : this.commentsService.getCount();
    this.numShares$ = this.postId ? this.sharesService.getCountByPostId(this.postId) : this.sharesService.getCount();
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
        this.getReactions();
        this.getCounters();
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

  toggleComments() {
    this.isCommentsShown = !this.isCommentsShown;

    if(this.isCommentsShown) {
      this.commentsFrm = new FormGroup({
        comment: new FormControl("", [Validators.required])
      });
    }
  }

  SendComments() {
    this.isCommentsSubmitted = true;

    if(this.isCommentsSubmitted && this.commentsFrm.invalid) {
      return;
    }

    const commentsdata: any = {
      title: `The user (id: ${this.avatarId}) commented on the post (id: ${this.postId})`,
      description: this.commentsFrm.value.comment,
      imgUrl: "images/bkg.jpeg",
      status: "public",
      dateCommentCreated: new Date().toISOString(),
      dateCommentUpdated: new Date().toISOString(),
      dateCommentDeleted: new Date().toISOString(),
      isFeatured: false,
      userId: this.avatarId,
      postId: this.postId,
      replyId: 0,
      shareId: 0,
      reactionId: 0,
      attachmentId: 0
    };

    this.commentsService.createComments(commentsdata).subscribe({
      next: (r) => {
        this.alertsService.openAlert(`Created comment for post ${this.postId}!`, 1, "success");
        this.commentsFrm.reset();
        this.getComments();
        this.getCounters();
        location.reload();
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  openShareDialog(postId: number) {
    this.dialog.open(SharesDialogComponent, {data: { postId: postId }});
  }
}
