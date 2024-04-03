/* c8 ignore start */

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Post } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, AuthService, NotificationsService, PostsService } from '@app/services';
import { first } from 'rxjs';

@Component({
  selector: 'app-createposts',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreatePostsComponent implements OnInit {
  postsFrm!: FormGroup;
  isPostFrmVisible = false;
  submitted = false;
  pidCtx: number = 0;

  constructor(private alertsService: AlertsService, private postsService: PostsService, private notificationService: NotificationsService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
      this.postsFrm = new FormGroup({
        Title: new FormControl('', Validators.required),
        Description: new FormControl('', Validators.required),
        ImgUrl: new FormControl('assets/images/bkg.jpeg'),
        Status: new FormControl('public'),
        IsFeatured: new FormControl(false),
        TypeTxtPost: new FormControl('html')
      });

      this.notificationService.getCount().subscribe({
        next: (r: any) => {
          this.pidCtx = r;
        },
        error: (err: any) => {
          this.pidCtx = 0;
          console.error(err.message);
        } 
      });
  }

  get f() { return this.postsFrm!.controls; }

  OnSubmit() {
    this.submitted = true;

    if (this.postsFrm!.invalid) {
      return;
    }

    const postsObj: Post = {
      Title: this.f["Title"].value!.toString(),
      Description: this.f["Description"].value!.toString(),
      ImgUrl: this.f["ImgUrl"].value!.toString(),
      Status: this.f["Status"].value!.toString(),
      IsFeatured: this.f["IsFeatured"].value!.toString() == "true" ? true : false,
      DatePostCreated: new Date().toISOString(),
      UserId: this.authService.userValue != null ? (this.authService.userValue["usersInfo"]["userId"] ?? 1) : 1
    };

    this.postsService.createPosts(postsObj).subscribe({
      next: () => {
        this.pidCtx++;
        this.alertsService.openAlert(`Created new post!`, 1, "success");
        this.notificationService.createNotification({
          description: postsObj.Description,
          status: postsObj.Status,
          dateUserNotificationCreated: postsObj.DatePostCreated,
          userId: postsObj.UserId,
          postId: this.pidCtx
        }).subscribe({
          next: () => {
            console.log("created new notification!")
          }, 
          error: (emr) => {
            console.log(emr.Message);
          }
        });
        window.location.reload();
      },
      error: (em) => {
        this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
        console.log(em);
      }
    });
  }

  toggleCreate() {
    this.isPostFrmVisible = !this.isPostFrmVisible;
  }
}

/* c8 ignore end */