import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePostsComponent } from './update.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('UpdatePostsComponent', () => {
  let component: UpdatePostsComponent;
  let fixture: ComponentFixture<UpdatePostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatePostsComponent, RouterTestingModule]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdatePostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
