import { Component, OnInit } from '@angular/core';
import { FriendRequest, FriendRequestTypeEnum, User } from '@app/models';
import { SharedModule } from '@app/modules';
import { AlertsService, FriendsRequestsService } from '@app/services';

@Component({
  selector: 'app-friendsrequests',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './friendsrequests.component.html',
  styleUrl: './friendsrequests.component.scss'
})
export class FriendsrequestsComponent implements OnInit {
  friendsRequests: FriendRequest[] = [];
  users: User[] | any;

  constructor(private friendsRequestsService: FriendsRequestsService, private alertsService: AlertsService) {}

  ngOnInit(): void {
    this.getFriendsRequests();
  }

  getFriendsRequests() {
    this.friendsRequestsService.getAllWithUsers().subscribe({
      next: (r) => {
        this.friendsRequests = r[0];   
        this.users = r[1]; 
      },
      error: (err) => console.log(err.Message)
    });
  }

  OnAccept(friendRequestId: number) {
    const friendRequestData: FriendRequest = {
      friendRequestId: friendRequestId,
      description: this.friendsRequests[0].description,
      status: this.friendsRequests[0].status,
      friendRequestType: FriendRequestTypeEnum.accepted, 
      isAccepted: true,
      dateFriendRequestCreated: this.friendsRequests[0].dateFriendRequestCreated,
      dateFriendRequestAccepted: this.friendsRequests[0].dateFriendRequestAccepted,
      dateFriendRequestDeleted: this.friendsRequests[0].dateFriendRequestDeleted,
      userId: this.friendsRequests[0].userId, 
    };

    this.friendsRequestsService.updateFriendsRequests(friendRequestId, friendRequestData).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Accepted friend request!`, 1, "success");
        this.getFriendsRequests();
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }

  OnReject(friendRequestId: number) {
    this.friendsRequestsService.deleteFriendsRequests(friendRequestId).subscribe({
      next: (v) => {
        this.alertsService.openAlert(`Rejected friend request!`, 1, "success");
        this.getFriendsRequests();
        location.reload();
      },
      error: (err) => {
        console.error(err);
        this.alertsService.openAlert(`Error: ${err}`, 1, "error");
      }
    });
  }
}
