import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePostsComponent } from './create.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('CreatePostsComponent', () => {
  let component: CreatePostsComponent;
  let fixture: ComponentFixture<CreatePostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatePostsComponent, RouterTestingModule]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreatePostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
