import { Component } from '@angular/core';
import { SharedModule } from '../../modules';
import { FooterComponent } from '../../components';
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
