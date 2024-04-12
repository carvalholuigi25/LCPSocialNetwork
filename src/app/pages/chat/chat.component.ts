import * as signalR from '@microsoft/signalr';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChatMessage, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, AuthService, ChatMessagesService, UsersService } from '@app/services';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

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
  chatMessageForm!: FormGroup;
  isUserSelected: boolean = false;
  isChatEnabled: boolean = true;
  enableForceReload: boolean = true;
  hubConnection!: signalR.HubConnection;
  usersList$: Observable<User[] | any> = new Observable<User[] | any>();
  chatMessagesData$: Observable<ChatMessage[]> = new Observable<ChatMessage[]>();
  typeMsg: string = "sent";

  constructor(private authService: AuthService, private chatMessagesService: ChatMessagesService, private usersService: UsersService, private alertsService: AlertsService, public translateService: TranslateService) {
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
    this.usersList$ = this.usersService.getAll();
  }

  isOdd(x: number = 1) {
    return x % 2;
  }

  isEven(x: number = 1) {
    return !(x % 2);
  }

  setModeChatMessage(mode: number, element: ChatMessage) {
    // 1 = id (order by odd, default), 2 = id (order by even), 3 = time (by seconds)
    return mode == 1 ? this.isOdd(element.chatMessageId!) : 
           mode == 2 ? this.isEven(element.chatMessageId!) : 
           mode == 3 ? new Date(element.dateChatMessageCreated!.toString()).getUTCSeconds() !== new Date().getUTCSeconds() : 
           this.isOdd(element.chatMessageId!);
  }

  getChatMessages() {
    this.chatMessagesData$ = this.chatMessagesService.getAll();

    this.chatMessagesData$.subscribe({
      next: (msg: ChatMessage[]) => {
        msg.forEach(element => {
          const modeval = this.setModeChatMessage(1, element);
          this.typeMsg = modeval == true ? "received" : "sent";
        });
      },
      error: (err) => console.log(err)
    });
  }

  talkToThisUser(event: Event, uid: number = 1, username: string) {
    event.preventDefault();
    this.targetUserId = uid;
    this.chatMessageForm.patchValue({
      description: `@${username} `
    });
  }

  deleteAll() {
    this.chatMessagesService.deleteAllChatMessages().subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Deleted all messages!`, 1, "success");
        this.chatMessageForm.reset();
        this.getChatMessages();
        this.reload();
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

    const chatmsgval: ChatMessage = {
      description: this.f["description"].value!.toString(),
      status: 'public',
      isRead: false,
      typeMsg: this.typeMsg,
      dateChatMessageCreated: new Date().toISOString(),
      dateChatMessageUpdated: new Date().toISOString(),
      dateChatMessageDeleted: new Date().toISOString(),
      dateChatMessageReaded: new Date().toISOString(),
      connectionId: this.hubConnection.connectionId ?? "",
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
        this.reload();
      },
      error: (err) => { 
        console.log(err); 
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  loadSignalRStuff() {
    this.hubConnection = new signalR.HubConnectionBuilder()
        .configureLogging(environment.production ? signalR.LogLevel.Information : signalR.LogLevel.Debug)
        .withUrl('http://localhost:5001/chathub')
        .withAutomaticReconnect()
        .build();
    
    this.hubConnection.start().then(() => { 
      console.log('ChatHub Connected!');
      console.log('Connection ID: ' + this.hubConnection.connectionId);
      console.log('Connection State: ' + this.hubConnection.state);

      this.hubConnection.on("ReceiveMessage", () => {
        this.getChatMessages();
      });
  
      this.hubConnection.onreconnected(() => {
        console.log("Reconnected!");
        this.getChatMessages();
      });

      this.hubConnection.onclose(() => {
        console.log("Disconnected!");
      });
    }).catch((err) => {  
      return console.error(err.toString());  
    });
  }

  reload() {
    if(this.enableForceReload == true) {
      location.reload();
    }
  }
}
