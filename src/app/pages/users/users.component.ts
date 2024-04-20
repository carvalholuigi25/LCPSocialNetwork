import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '@app/services/users.service';
import { ReadPostsComponent } from '@app/features';
import { AlertsService, AuthService, FriendsRequestsService } from '@app/services';
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
  authUserInfo: any;
  userId: number = -1;
  usersData!: any;
  friendRequestsData$: Observable<any> = new Observable<any>();

  constructor(
    private route: ActivatedRoute, 
    private usersSrv: UsersService, 
    private friendsRequestsService: FriendsRequestsService, 
    private alertsService: AlertsService,
    private authService: AuthService
  ) {
    this.authUserInfo = this.authService.getCurUserInfoAuth();

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

  OnRejectFriend() {
    if(this.userId !== this.authUserInfo.userId) {
      const uidreq = this.authUserInfo.userId > 1 ? this.authUserInfo.userId-1 : this.authUserInfo.userId;
      this.friendsRequestsService.deleteFriendsRequests(uidreq).subscribe({
        next: (v) => {
          const fulluname = this.authUserInfo.firstName + ' ' + this.authUserInfo.lastName;
          this.alertsService.openAlert(`Friend request rejected by ${fulluname}!`, 1, "success");
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

  OnAddFriend(user: any) {
    if(this.userId !== this.authUserInfo.userId) {
      const fulluname = user.firstName + " " + user.lastName;
      const fulluaname = this.authUserInfo.firstName + " " + this.authUserInfo.lastName;
      const friendRequestData: FriendRequest | any = {
        description: `${fulluaname} has sent a friend request to ${fulluname}!`,
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
          this.alertsService.openAlert(friendRequestData.description, 1, "success");
          location.reload();
        },
        error: (err) => console.log(err.Message)
      });
    } else {
      this.alertsService.openAlert(`You can't add yourself as a friend!`, 1, "error");
    }
  }
}
