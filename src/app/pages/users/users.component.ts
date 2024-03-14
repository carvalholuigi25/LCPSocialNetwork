import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../components';
import { SharedModule } from '../../modules';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '@app/services/users.service';
import { ReadPostsComponent } from '@app/features';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [FooterComponent, ReadPostsComponent, SharedModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  id: number = -1;
  usersData!: any;

  constructor(private route: ActivatedRoute, private usersSrv: UsersService) {
    this.route.params.subscribe(params => {
      this.id = params["id"];
    });
  }

  ngOnInit() {
    if(this.id != -1 && this.id != undefined) {
      this.usersSrv.getAllById(this.id).subscribe((r: any) => {
        this.usersData = r;
      });
    } else {
      this.usersSrv.getAll().subscribe((r: any) => {
        this.usersData = r;
      });
    }
  }
}
