import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '@app/modules';
import { Observable } from 'rxjs';
import { AlertsService, CommentService } from '@app/services';

export interface DelCommentDialogData {
  commentId: number;
}

@Component({
  selector: 'app-deletecommentdialog',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './deletecomment.component.html',
  styleUrl: './deletecomment.component.scss'
})
export class DeletecommentDialogComponent implements OnInit {
  dataComments$: Observable<any> = new Observable<any>();

  constructor(private alertsService: AlertsService, private commentsService: CommentService, @Inject(MAT_DIALOG_DATA) public data: DelCommentDialogData) {}

  ngOnInit(): void {
    this.loadComments();
  }

  loadComments() {
    this.dataComments$ = this.commentsService.getAllById(this.data.commentId);
  }

  sendDelete() {
    this.commentsService.deleteComments(this.data.commentId).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Deleted comment with id: ${this.data.commentId}!`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }
}
