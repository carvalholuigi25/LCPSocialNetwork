import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '@app/services/users.service';
import { ReadPostsComponent } from '@app/features';
import { AlertsService, FriendsRequestsService } from '@app/services';
import { FriendRequest, FriendRequestTypeEnum, User } from '@app/models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [FooterComponent, ReadPostsComponent, SharedModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  userId: number = -1;
  usersData!: any;
  friendRequestsData$: Observable<FriendRequest[]> = new Observable<FriendRequest[]>();

  constructor(
    private route: ActivatedRoute, 
    private usersSrv: UsersService, 
    private friendsRequestsService: FriendsRequestsService, 
    private alertsService: AlertsService
  ) {
    this.route.params.subscribe(params => {
      this.userId = params["userId"];
    });
  }

  ngOnInit() {
    this.GetUsersData();
    this.GetFriendRequestsData();
  }

  GetUsersData() {
    if(this.userId != -1 && this.userId != undefined) {
      this.usersSrv.getAllById(this.userId).subscribe({
        next: (r: any) => {
          this.usersData = r;
        },
        error: (err) => console.log(err.Message)
      });
    } else {
      this.usersSrv.getAll().subscribe({
        next: (r: any) => {
          this.usersData = r;
        },
        error: (err) => console.log(err.Message)
      });
    }
  }

  GetFriendRequestsData() {
    this.friendRequestsData$ = this.friendsRequestsService.getAll();
  }

  OnRejectFriend(user: User | any) {
    if(this.userId !== user.userId) {
      this.friendsRequestsService.deleteFriendsRequests(user.userId > 1 ? user.userId - 1 : user.userId).subscribe({
        next: (v) => {
          this.alertsService.openAlert(`Friend request rejected!`, 1, "success");
          this.GetFriendRequestsData();
          location.reload();
        },
        error: (err) => {
          console.error(err);
          this.alertsService.openAlert(`Error: ${err}`, 1, "error");
        }
      });
    } else {
      this.alertsService.openAlert(`You can't reject yourself as a friend!`, 1, "error");
    }
  }

  OnAddFriend(user: User | any) {
    if(this.userId !== user.userId) {
      const friendRequestData: FriendRequest | any = {
        description: `${user.firstName + " " + user.lastName} has sent you a friend request`,
        status: "private",
        friendRequestType: FriendRequestTypeEnum.pending,
        dateFriendRequestCreated: new Date().toISOString(),
        dateFriendRequestAccepted: null,
        dateFriendRequestDeleted: null,
        isAccepted: false,
        userId: user.userId
      };
  
      this.friendsRequestsService.createFriendsRequests(friendRequestData).subscribe({
        next: (data) => {
          this.alertsService.openAlert(`Friend request sent to ${user.username}!`, 1, "success");
          location.reload();
        },
        error: (err) => console.log(err.Message)
      });
    } else {
      this.alertsService.openAlert(`You can't add yourself as a friend!`, 1, "error");
    }
  }
}
