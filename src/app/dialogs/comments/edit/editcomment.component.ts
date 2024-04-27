import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Comment } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, CommentService } from '@app/services';
import moment from 'moment';

export interface EditCommentDialogData {
  comment: Comment | any;
}

@Component({
  selector: 'app-editcommentdialog',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './editcomment.component.html',
  styleUrl: './editcomment.component.scss'
})
export class EditcommentDialogComponent implements OnInit {
  submitted: boolean = false;
  frmEdit!: FormGroup;

  constructor(private alertsService: AlertsService, private commentsService: CommentService, @Inject(MAT_DIALOG_DATA) public data: EditCommentDialogData) {}

  ngOnInit(): void {
    this.loadForm();
  }

  loadForm() {
    this.frmEdit = new FormGroup({
      description: new FormControl(this.data.comment.description, Validators.required)
    });
  }

  get f() { return this.frmEdit.controls; }

  sendEdit() {
    this.submitted = true;

    if (this.frmEdit.invalid) {
      return;
    }

    this.commentsService.updateComments(this.data.comment.commentId!, {
      CommentId: this.data.comment.commentId,
      Title: `Edited comment with id: ${this.data.comment.commentId}!`,
      Description: this.f['description'].value,
      ImgUrl: this.data.comment.imgUrl,
      Status: this.data.comment.status,
      DateCommentCreated: this.data.comment.dateCommentCreated,
      DateCommentUpdated: moment().format("YYYY-MM-DDTHH:mm:ss.SSS").toString(),
      DateCommentDeleted: this.data.comment.dateCommentDeleted,
      IsFeatured: this.data.comment.isFeatured,
      UserId: this.data.comment.userId,
      PostId: this.data.comment.postId,
      ReplyId: this.data.comment.replyId,
      ShareId: this.data.comment.shareId,
      ReactionId: this.data.comment.reactionId,
      AttachmentId: this.data.comment.attachmentId
    }).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Edited comment with id: ${this.data.comment.commentId}!`, 1, "success");
        location.reload();
      },
      error: (err) => {
        console.log(err);
        this.alertsService.openAlert(`Error editing comment with id: ${this.data.comment.commentId}!`, 2, "error");
      }
    });
  }
}
