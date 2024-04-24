import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ReactionsDialogComponent } from '@app/dialogs';
import { Post, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { Observable } from 'rxjs';
import { AlertsService, AuthService, CommentService, PostsService, ReactionsService, SharesService } from '@app/services';

@Component({
  selector: 'app-readposts',
  standalone: true,
  imports: [SharedModule, SafePipe],
  templateUrl: './read.component.html',
  styleUrl: './read.component.scss'
})
export class ReadPostsComponent implements OnInit {
  dataPosts?: Post[] | any;
  dataUsers?: User[] | any;
  avatarId: number = 1;
  avatarRole: string = "User";
  reactionTypeName: string = "like";
  reactionTypeValue: string = "thumb_up";
  postId: number = -1;
  userId: number = -1;
  reactionId: number = 0;
  ctReactions: number = 0;
  numReactions$: Observable<number> = new Observable<number>();
  numComments$: Observable<number> = new Observable<number>();
  numShares$: Observable<number> = new Observable<number>();
  reactionsData$: Observable<any> = new Observable<any>();

  constructor(
    private postsService: PostsService, private reactionsService: ReactionsService, private commentsService: CommentService, 
    private sharesService: SharesService, private alertsService: AlertsService, private route: ActivatedRoute, 
    private authService: AuthService, public dialog: MatDialog
  ) { 
    this.route.params.subscribe(params => {
      this.postId = params["postId"];
      this.userId = params["userId"];
    });
  }

  ngOnInit(): void {
    this.getPosts();
  }

  getCounters() {
    this.numReactions$ = this.postId ? this.reactionsService.getCountByPostId(this.postId) : this.reactionsService.getCount();
    this.numComments$ = this.commentsService.getCount();
    this.numShares$ = this.sharesService.getCount();
  }

  getPosts() {
    this.avatarId = this.authService.userValue != null ? this.authService.userValue["usersInfo"]["userId"] : 1;
    this.avatarRole = this.authService.userValue != null ? this.authService.userValue["usersInfo"]["role"] : "user";
    this.reactionsData$ = this.reactionsService.getDataLocal();
    this.getCounters();
    
    this.postsService.getAllWithUsers(-1, -1).subscribe({
      next: (r) => {
        this.dataUsers = r[0];
        this.dataPosts = r[1];

        if(this.postId >= 0) {
          this.dataPosts = this.dataPosts.filter((x: any) => x.postId == this.postId);
        }

        if(this.userId >= 0) {
          this.dataPosts = this.dataPosts.filter((x: any) => x.userId == this.userId);
        }
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  setReactionType(reactionData: any, ind: number) {
    this.reactionId = ind;
    this.reactionTypeName = reactionData.name;
    this.reactionsData$.subscribe(xrd => {
      this.reactionTypeValue = xrd.filter((y: any) => y.name == reactionData.name)[0].value.toString();
    });

    if(this.reactionTypeValue !== "") {
      this.createReactions(this.reactionTypeName, ind);
      this.updatePosts(ind);
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
        console.log(r);
        this.alertsService.openAlert(`Created reaction ${this.reactionTypeName} for post ${pId}!`, 1, "success");
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  updatePosts(id: number) {
    const postsdata = {
      PostId: id,
      Title: this.dataPosts[id-1].title,
      Description: this.dataPosts[id-1].description,
      ImgUrl: this.dataPosts[id-1].imgUrl,
      Status: this.dataPosts[id-1].status,
      DatePostCreated: this.dataPosts[id-1].datePostCreated,
      DatePostUpdated: this.dataPosts[id-1].datePostUpdated,
      DatePostDeleted: this.dataPosts[id-1].datePostDeleted,
      TypeTxtPost: this.dataPosts[id-1].typeTxtPost,
      IsFeatured: this.dataPosts[id-1].isFeatured,
      UserId: this.dataPosts[id-1].userId,
      CommentId: this.dataPosts[id-1].commentId,
      ReplyId: this.dataPosts[id-1].replyId,
      ShareId: this.dataPosts[id-1].shareId,
      ReactionId: this.dataPosts[id-1].reactionId,
      AttachmentId: this.dataPosts[id-1].attachmentId
    };

    console.log(postsdata);
    this.postsService.updatePosts(id, postsdata).subscribe({
      next: (r) => {
        console.log(r);
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
