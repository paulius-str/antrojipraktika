import { Component, Input, OnInit } from '@angular/core';
import { map, mapTo } from 'rxjs/operators';
import { IGrade } from 'src/app/models/grade';
import { ISubject } from 'src/app/models/subject';
import { IUserModel } from 'src/app/models/user-model';
import { LecturerService } from '../../lecturer.service';

@Component({
  selector: 'app-lecturer-student-view',
  templateUrl: './lecturer-student-view.component.html',
  styleUrls: ['./lecturer-student-view.component.css']
})
export class LecturerStudentViewComponent implements OnInit {
  @Input() student: IUserModel;
  @Input() subject: ISubject;

  grade: IGrade;

  newGrade: number;
  
  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
    this.getGrade();
  }

  getGrade() {
    this.lecturerService.getGrade({subjectId: this.subject.id, studentId: this.student.id}).subscribe(response => {
      this.grade = response;
    })
  }

  setGrade() {
    this.lecturerService.setGrade({subjectId: this.subject.id, studentId: this.student.id, value: this.newGrade}).subscribe(response => {
       this.getGrade(); 
    });
  }
}
