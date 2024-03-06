import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@app/modules';
import { PostsService } from '@app/services';

@Component({
  selector: 'app-deleteposts',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.scss'
})
export class DeletePostsComponent implements OnInit {
  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
      
  }
}
