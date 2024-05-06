import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '@app/components';
import { GraphComponent } from '@app/features';
import { SharedModule } from '@app/modules';
import { AuthService, PostsService, UsersService } from '@app/services';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FooterComponent, GraphComponent, SharedModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  curLogRole$: Observable<string> = new Observable<string>();
  users$: Observable<any> = new Observable<any>(); 
  posts$: Observable<any> = new Observable<any>(); 
  latestUser: any;

  constructor(private authService: AuthService, private usersService: UsersService, private postsService: PostsService) { }

  ngOnInit(): void {
    this.getCurLogRole();
    this.getStats();
  }

  getCurLogRole() {
    this.curLogRole$ = of(this.authService.getCurUserInfoAuth().role);
  }

  getStats() {
    this.users$ = this.usersService.getAll();
    this.posts$ = this.postsService.getAll();

    this.users$.subscribe(users => {
      this.latestUser = users[users.length - 1];
    });
  }
}
