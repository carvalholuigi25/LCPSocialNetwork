import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ChatMessage, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, ChatMessagesService, UsersService } from '@app/services';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit {
  userId?: number = 1;
  submitted!: boolean;
  chatMessage: ChatMessage | any;
  chatMessagesData: ChatMessage[] = [];
  usersList: User[] | any = [];
  chatMessageForm!: FormGroup;
  isUserSelected: boolean = false;
  isChatEnabled: boolean = true;

  constructor(private route: ActivatedRoute, private chatMessagesService: ChatMessagesService, private usersService: UsersService, private alertsService: AlertsService) { 
    this.route.params.subscribe(params => {
      this.userId = params["userId"];
    });
  }

  ngOnInit(): void {
    this.loadChatMessagesForm();
    this.getUsersList();
    this.getChatMessages();
  }

  get f() { return this.chatMessageForm.controls; }

  loadChatMessagesForm() {
    this.chatMessageForm = new FormGroup({
      description: new FormControl('', [Validators.required])
    });
  }

  getUsersList() {
    this.usersService.getAll().subscribe({
      next: (r) => {
        this.usersList = r;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getChatMessages() {
    this.chatMessagesService.getAll().subscribe({
      next: (r) => {
        this.chatMessagesData = r;
      },
      error: (err) => {
        console.error(err.Message);
      }
    });
  }

  talkToThisUser(uid: number = 1) {
    this.userId = uid;
    this.isUserSelected = this.userId === uid ? true : false;
  }

  deleteAll() {
    this.chatMessagesService.deleteAllChatMessages().subscribe({
      next: (v) => { 
        this.alertsService.openAlert(`Deleted all messages!`, 1, "success");
        window.location.reload();
      },
      error: (err) => { 
        console.log(err); 
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.chatMessageForm.invalid) {
      return;
    }

    const chatmsgval = {
      description: this.f["description"].value!.toString(),
      status: 'public',
      isRead: false,
      dateChatMessageCreated: new Date().toISOString(),
      dateChatMessageUpdated: new Date().toISOString(),
      dateChatMessageDeleted: new Date().toISOString(),
      dateChatMessageReaded: new Date().toISOString(),
      commentId: 0,
      replyId: 0,
      reactionId: 0,
      shareId: 0,
      attachmentId: 0,
      userId: parseInt(this.userId!.toString(), 0) ?? 1
    };

    this.chatMessagesService.createChatMessages(chatmsgval).subscribe({
      next: (v) => { 
        this.alertsService.openAlert(`Created new message!`, 1, "success");
        console.log("Created new message!"); 
        window.location.reload();
      },
      error: (err) => { 
        console.log(err); 
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    })
  }
}
