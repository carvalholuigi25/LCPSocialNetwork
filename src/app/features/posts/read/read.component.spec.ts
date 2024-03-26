import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadPostsComponent } from './read.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('ReadPostsComponent', () => {
  let component: ReadPostsComponent;
  let fixture: ComponentFixture<ReadPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ReadPostsComponent, 
        RouterTestingModule
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReadPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
