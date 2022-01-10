import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LecturerStudentViewComponent } from './lecturer-student-view.component';

describe('LecturerStudentViewComponent', () => {
  let component: LecturerStudentViewComponent;
  let fixture: ComponentFixture<LecturerStudentViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LecturerStudentViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LecturerStudentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
