import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SharedModule } from '@app/modules';
import { AlertsService, NotificationsService, PostsService } from '@app/services';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from '@app/models';
import { first } from 'rxjs';
import { FooterComponent } from '@app/components';

@Component({
  selector: 'app-deleteposts',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.scss'
})
export class DeletePostsComponent implements OnInit {
  postId: number = -1;
  dataPosts?: Post | any;
  postsDeleteFrm!: FormGroup;
  submitted = false;

  constructor(private alertsService: AlertsService, private postsService: PostsService, private notificationService: NotificationsService, private router: Router, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.postId = params["postId"];
    });
  }

  get f() { return this.postsDeleteFrm!.controls; }

  ngOnInit(): void {
     this.getPosts(); 
  }

  getPosts() {
    if(this.postId != -1) {
      this.postsService.getAllById(this.postId).pipe(first()).subscribe({
        next: (dataP) => {
          this.dataPosts = dataP;
        },
        error: error => {
          console.log(error);
        }
      });
    }
  }

  OnNotDelete() {
    this.alertsService.openAlert(`Cancelled the deletion of post (Id: ${this.postId})!`, 1, "success");
    this.router.navigate(['/newsfeed']);
  }

  OnDelete() {
    if(this.postId != -1) {
      this.submitted = true;

      this.postsService.deletePosts(this.postId).subscribe({
        next: () => {
          this.alertsService.openAlert(`Deleted post (Id: ${this.postId}) sucessfully!`, 1, "success");
          this.notificationService.deleteNotification(this.postId).subscribe({
            next: () => {
              console.log(`deleted the notification (id: ${this.postId})!`)
            }, 
            error: (emr) => {
              console.log(emr.Message);
            }
          });
          this.router.navigate(['/newsfeed']);
        },
        error: (em) => {
          this.alertsService.openAlert(`Error: ${em.message}`, 1, "error");
          console.log(em);
        }
      });
    }
  }
}
