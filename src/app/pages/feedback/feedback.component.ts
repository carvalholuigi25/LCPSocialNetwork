import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { SharedModule } from '@app/modules';
import { Feedback, FeedbackStatusEnum, FeedbackTypeEnum } from '@app/models';
import { AlertsService, AuthService, FeedbackService } from '@app/services';
import { MatDialog } from '@angular/material/dialog';
import { DeleteFeedbacksDialog } from '@app/dialogs';

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
  submitted: boolean = false;
  frmFeedback!: FormGroup;
  feedbacksList$: Observable<Feedback[] | any> = new Observable<Feedback[] | any>();

  constructor(private feedbackService: FeedbackService, private alertsService: AlertsService, private authService: AuthService, public dialog: MatDialog) {
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
  }

  countUp(fbid: number, feedbackdata: any) {
    this.counter = feedbackdata.counter;
    this.counter++;
    this.updateFeedback(fbid, this.counter, feedbackdata);
  }

  countDown(fbid: number, feedbackdata: any) {
    this.counter = feedbackdata.counter;
    this.counter--;
    this.updateFeedback(fbid, this.counter, feedbackdata);
  }

  updateFeedback(fbid: number, counter: number = 0, feedbackdata: any) {
    feedbackdata.counter = counter;
    this.feedbackService.updateFeedbacks(fbid, feedbackdata).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Updated feedback! (id: ${fbid})`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  onDelete() {
    this.dialog.open(DeleteFeedbacksDialog);
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
