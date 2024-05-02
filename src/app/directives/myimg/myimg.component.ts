import { CommonModule, IMAGE_CONFIG, NgOptimizedImage } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MyImage } from '@app/models';

@Component({
  selector: 'myimg',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  providers: [
    {
      provide: IMAGE_CONFIG,
      useValue: {
        breakpoints: [16, 32, 48, 64, 96, 128, 256, 384, 640, 750, 828, 1080, 1200, 1920, 2048, 3840]
      }
    }
  ],
  template: `
    <img *ngIf="!isOptimizerNgEnabled" src="{{myimgurl}}" width="{{imgObj.width}}" height="{{imgObj.height}}" class="{{imgObj.class}}" title="{{imgObj.title}}" alt="{{imgObj.alt}}" />
    <img *ngIf="!!isOptimizerNgEnabled" ngSrc="{{myimgurl}}" width="{{imgObj.width}}" height="{{imgObj.height}}" class="{{imgObj.class}}" title="{{imgObj.title}}" alt="{{imgObj.alt}}" priority="{{imgObj.isPriority}}" />
  `,
  styleUrl: './myimg.component.scss'
})
export class MyImageComponent implements OnInit {
  @Input() imgObj!: MyImage;
  defImg: string = 'bkp.jpeg';
  myimgurl: string = "";
  /* read more in: https://angular.io/guide/image-directive */
  isOptimizerNgEnabled: boolean = true;

  constructor() { }

  ngOnInit() { 
    this.loadImgUrl();
  }

  loadImgUrl() {
    this.imgObj.url = this.getMyBaseUrl();
    this.myimgurl = "assets/images/" + (this.imgObj!.url ? this.imgObj.url : this.defImg);
  }

  getMyBaseUrl() {
    return this.imgObj.url.replace("images/", "").replace("assets/", "");
  }
}
