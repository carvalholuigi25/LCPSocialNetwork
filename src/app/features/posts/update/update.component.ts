import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';
import { PostsService } from '@app/services';

@Component({
  selector: 'app-updateposts',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss'
})
export class UpdatePostsComponent implements OnInit {
  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
      
  }
}
