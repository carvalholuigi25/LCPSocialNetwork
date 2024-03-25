import { Component } from '@angular/core';
import { FooterComponent } from '@app/components';
import { SharedModule } from '@app/modules';
import { CreatePostsComponent, ReadPostsComponent } from '@app/features';

@Component({
  selector: 'app-newsfeed',
  standalone: true,
  imports: [FooterComponent, CreatePostsComponent, ReadPostsComponent, SharedModule],
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.scss'
})
export class NewsfeedComponent {

}
