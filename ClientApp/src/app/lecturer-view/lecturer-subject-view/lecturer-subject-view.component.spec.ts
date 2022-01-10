import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LecturerSubjectViewComponent } from './lecturer-subject-view.component';

describe('LecturerSubjectViewComponent', () => {
  let component: LecturerSubjectViewComponent;
  let fixture: ComponentFixture<LecturerSubjectViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LecturerSubjectViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LecturerSubjectViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
