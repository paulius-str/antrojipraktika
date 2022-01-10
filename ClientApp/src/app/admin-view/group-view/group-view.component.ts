import { Component, Input, OnInit } from '@angular/core';
import { IGroup } from 'src/app/models/group';
import { ISubject } from 'src/app/models/subject';
import { IUserModel } from 'src/app/models/user-model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-group-view',
  templateUrl: './group-view.component.html',
  styleUrls: ['./group-view.component.css']
})
export class GroupViewComponent implements OnInit {
  @Input() group: IGroup;
  @Input() allStudents: IUserModel[];

  assignedStudents: IUserModel[];
  assignedSubjects: ISubject[];
  newStudentId: number;
  newSubjectId: number;

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getAssignedStudents(this.group.id);
    this.getAssignedSubjects(this.group.id);
  }

  getAssignedStudents(groupId: number) {
    this.adminService.getAssignedToGroupStudents(groupId).subscribe(response => {
      this.assignedStudents = response;
    });
  }

  getAssignedSubjects(groupId: number) {
    this.adminService.getAssignedSubjects(groupId).subscribe(response => {
      this.assignedSubjects = response;
    })
  }

  removeStudentFromGroup(studentId: number) {
    this.adminService.deleteStudentFromGroup(studentId).subscribe(response => {
      this.getAssignedStudents(this.group.id);
    });
  }

  addStudent(studentId: number) {
    this.adminService.assignStudentToGroup({studentId: studentId, groupId: this.group.id}).subscribe(response => {
      this.getAssignedStudents(this.group.id);
    });
  }

  addSubject(subjectId: number) {
    this.adminService.assignSubjectToGroup({subjectId: subjectId, groupId: this.group.id}).subscribe(response => {
      this.getAssignedSubjects(this.group.id);
    })
  }

  removeAssignedSubject(subjectId: number) {
    this.adminService.deleteAssignedSubject({subjectId: subjectId, groupId: this.group.id}).subscribe(response => {
      this.getAssignedSubjects(this.group.id);
    });
  }
}
