import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatMessage, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, AuthService, ChatMessagesService, UsersService } from '@app/services';
import { environment } from '@environments/environment';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit {
  userId?: number = 1;
  targetUserId?: number = 1;
  submitted!: boolean;
  chatMessage: ChatMessage | any;
  chatMessagesData$: Observable<ChatMessage[]> = new Observable<ChatMessage[]>();
  usersList: User[] | any = [];
  chatMessageForm!: FormGroup;
  isUserSelected: boolean = false;
  isChatEnabled: boolean = true;

  constructor(private router: Router, private authService: AuthService, private chatMessagesService: ChatMessagesService, private usersService: UsersService, private alertsService: AlertsService) {
    this.authService.user.subscribe((x: any) => {
      this.userId = x.usersInfo.userId;
    });
  }

  ngOnInit(): void {
    this.loadChatMessagesForm();
    this.getUsersList();
    this.getChatMessages();
    this.loadSignalRStuff();
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
    this.chatMessagesData$ = this.chatMessagesService.getAll();
  }

  talkToThisUser(event: Event, uid: number = 1) {
    event.preventDefault();
    this.targetUserId = uid;
    this.chatMessageForm.patchValue({
      description: "@" + this.usersList[this.targetUserId-1].username + " "
    });
  }

  deleteAll() {
    this.chatMessagesService.deleteAllChatMessages().subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Deleted all messages!`, 1, "success");
        this.chatMessageForm.reset();
        this.getChatMessages();
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
      userId: this.userId ?? 1,
      targetUserId: this.targetUserId ?? 2
    };

    this.chatMessagesService.createChatMessages(chatmsgval).subscribe({
      next: (v) => { 
        console.log("Created new message!"); 
        this.alertsService.openAlert(`Created new message!`, 1, "success");
        this.chatMessageForm.reset();
        this.getChatMessages();
      },
      error: (err) => { 
        console.log(err); 
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  loadSignalRStuff() {
    const hubConnection = new signalR.HubConnectionBuilder()
        .configureLogging(environment.production ? signalR.LogLevel.Information : signalR.LogLevel.Debug)
        .withUrl('http://localhost:5001/chathub')
        .withAutomaticReconnect()
        .build();

    hubConnection.start().then(function () {  
        console.log('ChatHub Connected!');
    }).catch(function (err) {  
        return console.error(err.toString());  
    });

    hubConnection.on("ReceiveChanges", () => {
      this.getChatMessages();
    });

    hubConnection.onreconnected(() => {
      this.getChatMessages();
    });
  }

  reload() {
    location.reload();
  }
}
