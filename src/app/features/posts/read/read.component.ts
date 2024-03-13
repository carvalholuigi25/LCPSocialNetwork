import { Component, OnInit } from '@angular/core';
import { Posts, Users } from '@app/models';
import { SharedModule } from '@app/modules';
import { SafePipe } from '@app/pipes';
import { AlertsService, AuthService, PostsService } from '@app/services';

@Component({
  selector: 'app-readposts',
  standalone: true,
  imports: [SharedModule, SafePipe],
  templateUrl: './read.component.html',
  styleUrl: './read.component.scss'
})
export class ReadPostsComponent implements OnInit {
  dataPosts?: Posts[] | any;
  dataUsers?: Users[] | any;
  avatarId: number = 1;
  avatarRole: string = "User";
  ctLikes: number = 0;
  ctComments: number = 0;
  ctShares: number = 0;

  constructor(private postsService: PostsService, private alertsService: AlertsService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getPosts();
  }

  getPosts() {
    this.avatarId = this.authService.userValue["usersInfo"]["userId"];
    this.avatarRole = this.authService.userValue["usersInfo"]["role"];
    this.postsService.getAllWithUsers(-1).subscribe({
      next: (r) => {
        this.dataPosts = r[0];
        this.dataUsers = r[1];
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  ClickLikes() {
    this.ctLikes!++;
  }

  ClickComments() {
    this.ctComments!++;
  }

  ClickShares() {
    this.ctShares!++;
  }
}
