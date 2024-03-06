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
  authorName?: string;
  ctLikes: number = 0;
  ctComments: number = 0;
  ctShares: number = 0;

  constructor(private postsService: PostsService, private alertsService: AlertsService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getPosts();
  }

  getPosts() {
    const uid = this.authService.userValue ? this.authService.userValue["usersInfo"]["userId"] : 1;
    this.postsService.getAllById(uid).subscribe({
      next: (r) => {
        this.dataAll = r;
      },
      error: error => {
        this.alertsService.openAlert(`Error: ${error}`, 1, "error");
        console.log(error);
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
    this.ctLikes!++;
  }
}
