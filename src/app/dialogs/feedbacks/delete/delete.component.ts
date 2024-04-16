import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '@app/modules';
import { AlertsService, FeedbackService } from '@app/services';

export interface DeleteDialogData {
  id: number;
  mode: 'all' | 'id';
}

@Component({
  selector: 'app-deletefeedbacks',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.scss'
})
export class DeleteFeedbacksDialog implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DeleteDialogData, private feedbackService: FeedbackService, private alertsService: AlertsService) { }

  ngOnInit(): void {
    
  }

  sendDelete() {
    if(this.data.mode === 'id') {
      this.feedbackService.deleteFeedbacks(this.data.id).subscribe({
        next: (v) => {
          this.alertsService.openAlert(`Deleted feedback with id: ${this.data.id}!`, 1, "success");
          location.reload();
        },
        error: (err) => {
          console.error(err);
          this.alertsService.openAlert(`Error: ${err}`, 1, "error");
        }
      });
    } else {
      this.feedbackService.deleteAllFeedbacks().subscribe({
        next: (v) => {
          this.alertsService.openAlert(`Deleted all feedbacks!`, 1, "success");
          location.reload();
        },
        error: (err) => {
          console.error(err);
          this.alertsService.openAlert(`Error: ${err}`, 1, "error");
        }
      }); 
    }
  }
}
