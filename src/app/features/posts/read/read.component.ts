import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { AlertsService, AuthService, CommentService, PostsService } from '@app/services';
import { ReactionsService } from '@app/services/reactions.service';
import { SharesService } from '@app/services/shares.service';
import { Observable } from 'rxjs';

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
  ctReactions$: Observable<number> = new Observable<number>();
  ctComments$: Observable<number> = new Observable<number>();
  ctShares$: Observable<number> = new Observable<number>();
  reactionsData$: Observable<any> = new Observable<any>();

  constructor(private postsService: PostsService, private reactionsService: ReactionsService, private commentsService: CommentService, private sharesService: SharesService, private alertsService: AlertsService, private route: ActivatedRoute, private authService: AuthService) { 
    this.route.params.subscribe(params => {
      this.postId = params["postId"];
      this.userId = params["userId"];
    });
  }

  ngOnInit(): void {
    this.getPosts();
  }

  getPosts() {
    this.avatarId = this.authService.userValue != null ? this.authService.userValue["usersInfo"]["userId"] : 1;
    this.avatarRole = this.authService.userValue != null ? this.authService.userValue["usersInfo"]["role"] : "user";
    this.reactionsData$ = this.reactionsService.getDataLocal();
    this.ctReactions$ = this.reactionsService.getCount();
    this.ctComments$ = this.commentsService.getCount();
    this.ctShares$ = this.sharesService.getCount();

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

  setReactionType(reactionType: string) {
    this.reactionTypeName = reactionType;
    this.reactionTypeValue = this.reactionTypeName == 'like' ? "thumb_up" : 
    reactionType == 'dislike' ? "thumb_down" : 
    reactionType == 'love' ? "favorite" : 
    reactionType == 'courage' ? "cheer" : 
    reactionType == 'laugh' ? "mood" : 
    reactionType == 'surprised' ? "sentiment_excited" : 
    reactionType == 'sad' ? "sentiment_sad" : 
    reactionType == 'angry' ? "mood_bad" : 
    "thumb_up";
  }
}
