import { i18nMetaToJSDoc } from '@angular/compiler/src/render3/view/i18n/meta';
import { Component, Input, OnInit } from '@angular/core';
import { ISubject } from 'src/app/models/subject';
import { IUserModel } from 'src/app/models/user-model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-subject-view',
  templateUrl: './subject-view.component.html',
  styleUrls: ['./subject-view.component.css']
})
export class SubjectViewComponent implements OnInit {
  @Input() subject: ISubject;
  newAssignationLecturerId: number;

  assignedLecturers: IUserModel[];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getAssignedLecturers();
  }

  deleteAssignation(userId: any)
  {
    let values = {"lecturerId": userId, "subjectId": this.subject.id};

    this.adminService.deleteAssignation(values).subscribe(response => {
      this.getAssignedLecturers();
    }, error => {
      console.log(error);
    });
  }

  deleteSubject(id: number) {
    this.adminService.deleteSubject(id).subscribe(response => {
      console.log("delete success");
      this.subject = null;
    });
  }

  getAssignedLecturers() {
    this.adminService.getAssignedLecturers(this.subject.id).subscribe(response => {
      this.assignedLecturers = response;
  });
  }

  addAssignation() {
    this.adminService.addAssignation({"lecturerId": this.newAssignationLecturerId, "subjectId": this.subject.id}).subscribe(response => {
      this.getAssignedLecturers();
    });
  }
}
