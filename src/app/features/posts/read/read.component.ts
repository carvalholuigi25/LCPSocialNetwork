import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { SocialcountersComponent } from '@app/features';
import { Post, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { AlertsService, AuthService, PostsService } from '@app/services';

@Component({
  selector: 'app-readposts',
  standalone: true,
  imports: [SharedModule, SocialcountersComponent, SafePipe],
  templateUrl: './read.component.html',
  styleUrl: './read.component.scss'
})
export class ReadPostsComponent implements OnInit {
  dataPosts?: Post[] | any;
  dataUsers?: User[] | any;
  avatarRole: string = "User";
  avatarId: number = 1;
  postId: number = -1;
  userId: number = -1;

  constructor(
    private postsService: PostsService,
    private alertsService: AlertsService, 
    private route: ActivatedRoute, 
    private authService: AuthService, 
    public dialog: MatDialog
  ) { 
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
}
