import { Component, Input, OnInit } from '@angular/core';
import { ISubject } from 'src/app/models/subject';
import { IUserModel } from 'src/app/models/user-model';
import { LecturerService } from '../lecturer.service';

@Component({
  selector: 'app-lecturer-subject-view',
  templateUrl: './lecturer-subject-view.component.html',
  styleUrls: ['./lecturer-subject-view.component.css']
})
export class LecturerSubjectViewComponent implements OnInit {
  @Input() subject: ISubject;

  students: IUserModel[];

  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents() {
    this.lecturerService.getStudentsForSubject(this.subject.id).subscribe(response => {
      this.students = response;
      console.log(response);
    });
  }
}
