import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadPostsComponent } from './read.component';

describe('ReadPostsComponent', () => {
  let component: ReadPostsComponent;
  let fixture: ComponentFixture<ReadPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReadPostsComponent]
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
