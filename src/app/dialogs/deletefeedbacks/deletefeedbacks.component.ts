import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';
import { AlertsService, FeedbackService } from '@app/services';

@Component({
  selector: 'app-deletefeedbacks',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './deletefeedbacks.component.html',
  styleUrl: './deletefeedbacks.component.scss'
})
export class DeleteFeedbacksDialog implements OnInit {
  constructor(private feedbackService: FeedbackService, private alertsService: AlertsService) { }

  ngOnInit(): void {
    
  }

  deleteAllFeedbacks() {
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
