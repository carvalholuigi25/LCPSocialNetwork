import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';
import { Notification, User } from '@app/models';
import { NotificationsService } from '@app/services';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent implements OnInit {
  notificationsItems: Notification[] = [];
  users: User[] = [];

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
      this.getNotifications();
  }

  getNotifications() {
    this.notificationsService.getAllWithUsers().subscribe({
      next: (r) => {
          this.notificationsItems = r[0];
          this.users = r[1];
      },
      error: (err) => {
        console.log(err.Message);
      }
    });
  }

  DeleteAllNotifications() {
    this.notificationsService.deleteAllNotifications().subscribe({
      next: (r) => {
        console.log('deleted all notifications!');
        window.location.reload();
      },
      error: (err) => {
        console.log(err.Message);
      }
    });
  }
}
