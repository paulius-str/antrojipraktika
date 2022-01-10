import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { IGroup } from '../models/group';
import { ISubject } from '../models/subject';
import { IUserModel } from '../models/user-model';
import { AdminService } from './admin.service';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})
export class AdminViewComponent implements OnInit {
  subjects: ISubject[];
  students: IUserModel[];
  lecturers: IUserModel[];
  groups: IGroup[];
  assignedLecturers: IUserModel[];
  newSubjectName: string;
  authorized: boolean = false;

  newUserUsername:string;
  newUserFirstName:string;
  newUserLastName:string;
  newUserPassword:string

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getSubjects();
    this.getStudents();
    this.getLecturers();
    this.getGroups();
  }

  getSubjects() {
    this.adminService.getSubjects().subscribe(response => {
      this.subjects = response['result'];
      console.log(this.subjects);
      this.authorized = true;
  });
  }

  getStudents() {
    this.adminService.getStudents().subscribe(response => {
      this.students = response;
      console.log(this.students);
  });
  }

  getLecturers() {
    this.adminService.getLecturers().subscribe(response => {
      this.lecturers = response;
      console.log(this.lecturers);
  });
  }

  getGroups() {
    this.adminService.getGroups().subscribe(response => {
      this.groups = response;
      console.log(this.groups);
  });
  }

  addSubject() {
    this.adminService.addSubject({name: this.newSubjectName}).subscribe(response => {
      console.log("post success");
      this.getSubjects();
    });
  }

  addStudent() {
    this.adminService.addStudent({
        username: this.newUserUsername,
        firstname: this.newUserFirstName,
        lastname: this.newUserLastName,
        password: this.newUserPassword
    }).subscribe(response => {
      console.log("post success");
      this.getStudents();
    });
  }

  addLecturer() {
    this.adminService.addLecturer({
        username: this.newUserUsername,
        firstname: this.newUserFirstName,
        lastname: this.newUserLastName,
        password: this.newUserPassword
    }).subscribe(response => {
      console.log("post success");
      this.getLecturers();
    });
  }

  deleteStudent(id: number) {
    this.adminService.deleteStudent(id).subscribe(response => {
      console.log("delete success");
      this.getStudents();
    })
  }

  deleteLecturer(id: number) {
    this.adminService.deleteLecturer(id).subscribe(response => {
      console.log("delete success");
      this.getLecturers();
    })
  }

  }


