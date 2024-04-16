import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Feedback } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, FeedbackService } from '@app/services';

export interface EditDialogData {
  id: number;
  dataOldFeedback: Feedback | any;
}

@Component({
  selector: 'app-editfeedbacks',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.scss'
})
export class EditFeedbacksDialog implements OnInit {
  feedbackEditFrm!: FormGroup;
  dataFeedback!: Feedback;
  submitted: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: EditDialogData, private feedbackService: FeedbackService, private alertsService: AlertsService) { }

  ngOnInit(): void {
    this.feedbackEditFrm = new FormGroup({
      Title: new FormControl(this.data.dataOldFeedback.title, Validators.required),
      Description: new FormControl(this.data.dataOldFeedback.description, Validators.required)
    });
  }

  get f() { return this.feedbackEditFrm.controls; }

  onReset() {
    this.feedbackEditFrm.reset();
    this.feedbackEditFrm.clearValidators();
    this.feedbackEditFrm.updateValueAndValidity();
  }

  onResetToDefValues() {
    this.onReset();
    this.feedbackEditFrm.patchValue({
      Title: this.data.dataOldFeedback.title,
      Description: this.data.dataOldFeedback.description
    });
  }

  sendEdit() {
    this.submitted = true;

    if (this.feedbackEditFrm.invalid) {
      return;
    }

    this.dataFeedback = {
      FeedbackId: this.data.id,
      Title: this.f["Title"].value,
      Description: this.f["Description"].value,
      IsFeatured: this.data.dataOldFeedback.isFeatured,
      IsLocked: this.data.dataOldFeedback.isLocked,
      StatusFeedback: this.data.dataOldFeedback.statusFeedback,
      TypeFeedback: this.data.dataOldFeedback.typeFeedback,
      DateFeedbackCreated: this.data.dataOldFeedback.dateFeedbackCreated,
      DateFeedbackUpdated: this.data.dataOldFeedback.dateFeedbackDeleted,
      DateFeedbackDeleted: this.data.dataOldFeedback.dateFeedbackDeleted,
      Counter: this.data.dataOldFeedback.counter,
      UserId: this.data.dataOldFeedback.userId
    };

    this.feedbackService.updateFeedbacks(this.data.id, this.dataFeedback).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Edited feedback with id: ${this.data.id}!`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }
}
