/* c8 ignore start */

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FooterComponent } from '@app/components';
import { Post } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, AuthService, NotificationsService, PostsService } from '@app/services';

@Component({
  selector: 'app-updateposts',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss'
})
export class UpdatePostsComponent implements OnInit {
  postId: number = -1;
  isAnyPostsData: boolean = false;
  postsUpdateFrm!: FormGroup;
  submitted = false;

  constructor(private alertsService: AlertsService, private postsService: PostsService, private notificationService: NotificationsService, private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.postId = params["postId"];
    });
  }

  get f() { return this.postsUpdateFrm!.controls; }

  ngOnInit(): void {
     this.getPosts(); 
  }

  getPosts() {
    this.postsUpdateFrm = new FormGroup({
      Title: new FormControl('', Validators.required),
      Description: new FormControl('', Validators.required),
      ImgUrl: new FormControl('assets/images/bkg.jpeg'),
      Status: new FormControl('public'),
      IsFeatured: new FormControl(false),
      TypeTxtPost: new FormControl('html')
    });

    if(this.postId != -1) {
      this.postsService.getAllById(this.postId!).subscribe({
        next: (dataP: any) => {
          this.isAnyPostsData = dataP.length > 0 ? true : false;
          this.postsUpdateFrm.patchValue({
            Title: dataP[0].title,
            Description: dataP[0].description,
            ImgUrl: dataP[0].imgUrl,
            Status: dataP[0].status,
            IsFeatured: dataP[0].isFeatured,
            TypeTxtPost: dataP[0].typeTxtPost
          });
        },
        error: error => {
          this.isAnyPostsData = false;
          console.log(error);
        }
      });
    } else {
      this.isAnyPostsData = false;
    }
  }

  OnClear() {
    this.postsUpdateFrm.reset();
    this.postsUpdateFrm.clearValidators();
    this.postsUpdateFrm.updateValueAndValidity();
  }

  OnResetToDefValues() {
    this.getPosts();
  }

  OnNotUpdate() {
    this.alertsService.openAlert(`Cancelled the update of post (Id: ${this.postId})!`, 1, "success");
    this.router.navigate(['/newsfeed']);
  }

  OnUpdate() {
    if(this.postId != -1) {
      this.submitted = true;

      const postsObj: Post = {
        PostId: this.postId ?? 1,
        Title: this.f["Title"].value.toString(),
        Description: this.f["Description"].value.toString(),
        ImgUrl: this.f["ImgUrl"].value.toString(),
        Status: this.f["Status"].value.toString(),
        IsFeatured: this.f["IsFeatured"].value.toString() == "true" ? true : false,
        TypeTxtPost: this.f["TypeTxtPost"].value.toString(),
        UserId: this.authService.userValue != null ? (this.authService.userValue["usersInfo"]["userId"] ?? 1) : 1
      };

      this.postsService.updatePosts(this.postId, postsObj).subscribe({
        next: () => {
          this.alertsService.openAlert(`Updated post (Id: ${this.postId}) sucessfully!`, 1, "success");
          this.notificationService.updateNotification(this.postId, {
            description: postsObj.Description,
            status: postsObj.Status,
            dateUserNotificationUpdated: postsObj.DatePostUpdated,
            userId: postsObj.UserId,
            postId: postsObj.PostId
          }).subscribe({
            next: () => {
              console.log(`updated the current notification (id: ${this.postId}!`);
            }, 
            error: (emr) => {
              console.log(emr.Message);
            }
          });
          this.router.navigate(['/newsfeed']);
        },
        error: (em: any) => {
          this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
          console.log(em);
        }
      });
    }
  }
}

/* c8 ignore end */