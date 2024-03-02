import { Component } from '@angular/core';
import { SharedModule } from '../../modules';
import { FooterComponent } from '../../components';

@Component({
  selector: 'app-newsfeed',
  standalone: true,
  imports: [FooterComponent, SharedModule],
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.scss'
})
export class NewsfeedComponent {
  ctLikes?: number = 0;
  ctComments?: number = 0;
  ctShares?: number = 0;

  onClickLikes() {
    this.ctLikes!++;
  }

  onClickComments() {
    this.ctComments!++;
  }

  onClickShares() {
    this.ctShares!++;
  }
}
