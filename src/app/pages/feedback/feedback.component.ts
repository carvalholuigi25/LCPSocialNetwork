import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { SharedModule } from '@app/modules';
import { Feedback, FeedbackStatusEnum, FeedbackTypeEnum } from '@app/models';
import { AlertsService, AuthService, FeedbackService } from '@app/services';

@Component({
  selector: 'app-feedback',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.scss'
})
export class FeedbackComponent implements OnInit {
  counter: number = 0;
  userId: number = 1;
  feedbackId: number = 1;
  submitted: boolean = false;
  feedbackData!: Feedback;
  frmFeedback!: FormGroup;
  feedbacksList$: Observable<Feedback[] | any> = new Observable<Feedback[] | any>();

  constructor(private feedbackService: FeedbackService, private alertsService: AlertsService, private authService: AuthService) {
    this.authService.user.subscribe((x: any) => {
      this.userId = x.usersInfo.userId;
    });
  }

  ngOnInit() {
    this.loadFormFeedback();
    this.getFeedbacksList();
  }

  get f() { return this.frmFeedback.controls; }

  loadFormFeedback() {
    this.frmFeedback = new FormGroup({
      Title: new FormControl('', Validators.required),
      Description: new FormControl('', Validators.required)
    });
  }

  getFeedbacksList() {
    this.feedbacksList$ = this.feedbackService.getAll();
    this.feedbacksList$.subscribe({
      next: (r: any) => {
        if(r) {
          this.counter = r[0].counter! ?? 0;
          this.feedbackData = {
            FeedbackId: r[0].feedbackId,
            Title: r[0].title,
            Description: r[0].description,
            IsFeatured: r[0].isFeatured,
            IsLocked: r[0].isLocked,
            TypeFeedback: r[0].typeFeedback,
            StatusFeedback: r[0].statusFeedback,
            DateFeedbackCreated: r[0].dateFeedbackCreated,
            DateFeedbackUpdated: r[0].dateFeedbackUpdated,
            DateFeedbackDeleted: r[0].dateFeedbackDeleted,
            Counter: this.counter,
            UserId: r[0].userId
          };
        }
      },
      error: (err) => {
        this.counter = 0;
        console.error(err);
      }
    });
  }

  countUp(fbid: number) {
    this.counter++;
    this.updateFeedback(fbid, this.counter);
  }

  countDown(fbid: number) {
    this.counter--;
    this.updateFeedback(fbid, this.counter);
  }

  updateFeedback(fbid: number, counter: number = 0) {
    this.feedbackData.Counter = counter;
    this.feedbackService.updateFeedbacks(fbid, this.feedbackData).subscribe({
      next: (v) => {
        console.log(v);
        this.alertsService.openAlert(`Updated feedback! (id: ${fbid})`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  onReset() {
    this.frmFeedback.reset();
    this.frmFeedback.clearValidators();
    this.frmFeedback.updateValueAndValidity();
  }

  onSubmit() {
    this.submitted = true;

    if (this.frmFeedback.invalid) {
      return;
    }

    const frmFeedbackData: Feedback = {
      Title: this.f["Title"].value!.toString(),
      Description: this.f["Description"].value!.toString(),
      IsLocked: false,
      IsFeatured: false,
      TypeFeedback: FeedbackTypeEnum.pending.toString(),
      StatusFeedback: FeedbackStatusEnum.public.toString(),
      DateFeedbackCreated: new Date().toISOString(),
      DateFeedbackUpdated: new Date().toISOString(),
      DateFeedbackDeleted: new Date().toISOString(),
      Counter: this.counter,
      UserId: this.userId ?? 1
    };

    this.feedbackService.createFeedbacks(frmFeedbackData).subscribe({
      next: (v) => {
        console.log(v);
        this.alertsService.openAlert(`Created feedback! ${v.FeedbackId}`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }
}
