import { Component, OnInit } from '@angular/core';
import { IGradeWithSubject } from '../models/gradewithsubject';
import { StudentService } from './student.service';

@Component({
  selector: 'app-student-view',
  templateUrl: './student-view.component.html',
  styleUrls: ['./student-view.component.css']
})
export class StudentViewComponent implements OnInit {
  gradesWithSubjects : IGradeWithSubject[];

  constructor(private studentService: StudentService) { }

  ngOnInit(): void {
    this.getGrades();
  }

  getGrades() {
    this.studentService.getGrades().subscribe(response => {
      this.gradesWithSubjects = response;
      console.log(this.gradesWithSubjects);
    }, error => {
      console.log(error);
    });
  }
}
