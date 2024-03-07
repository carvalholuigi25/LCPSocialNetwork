import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';
import { AlertsService, AuthService, PostsService } from '@app/services';

@Component({
  selector: 'app-readposts',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './read.component.html',
  styleUrl: './read.component.scss'
})
export class ReadPostsComponent implements OnInit {
  dataAll?: any;
  avatarId?: number;
  avatarName?: string;
  avatarUrl?: string;
  ctLikes: number = 0;
  ctComments: number = 0;
  ctShares: number = 0;

  constructor(private postsService: PostsService, private alertsService: AlertsService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getPosts();
  }

  getPosts() {
    const uid = this.authService.userValue ? this.authService.userValue["usersInfo"]["userId"] : 1;
    this.avatarId = this.authService.userValue ? this.authService.userValue["usersInfo"]["userId"] : 1;
    this.avatarName = this.authService.userValue ? this.authService.userValue["usersInfo"]["username"] : "guest";
    this.avatarUrl = this.authService.userValue ? this.authService.userValue["usersInfo"]["avatarUrl"] : "images/bkg.jpeg";
    this.postsService.getAllById(uid).subscribe({
      next: (r) => {
        this.dataAll = r;
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
