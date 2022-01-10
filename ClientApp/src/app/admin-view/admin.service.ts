import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAssignedLecturer } from '../models/assignedLecturer';
import { IGroup } from '../models/group';
import { ISubject } from '../models/subject';
import { IUserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { 
  }

  getSubjects(){
    return this.http.get<ISubject[]>(environment.baseUrl + 'data/subjects');
  }

  getSubject(id: number){
    return this.http.get<ISubject>(environment.baseUrl + 'data/subject/' + id);
  }

  getStudents(){
    return this.http.get<IUserModel[]>(environment.baseUrl + 'data/students');
  }

  getLecturers(){
    return this.http.get<IUserModel[]>(environment.baseUrl + 'data/lecturers');
  }

  getAssignedLecturers(subjectId: number){
    return this.http.get<IUserModel[]>(environment.baseUrl + 'admin/assignedlecturers/' + subjectId);
  }

  getAssignedSubjects(groupId: number){
    return this.http.get<ISubject[]>(environment.baseUrl + 'admin/assignedsubjects/' + groupId);
  }

  getGroups(){
    return this.http.get<IGroup[]>(environment.baseUrl + 'admin/groups');
  }

  getAssignedToGroupStudents(groupId: number){
    return this.http.get<IUserModel[]>(environment.baseUrl + 'admin/groups/students/' + groupId);
  }

  addSubject(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/addsubject', values);
  }

  addStudent(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/addstudent', values);
  }

  addLecturer(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/addlecturer', values);
  }
  
  addAssignation(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/assignedlecturers/add', values);
  }

  deleteSubject(id: number)
  {
    return this.http.delete(environment.baseUrl + 'admin/deletesubject/' + id);
  }

  deleteStudent(id: number)
  {
    return this.http.delete(environment.baseUrl + 'admin/removestudent/' + id);
  }

  deleteLecturer(id: number)
  {
    return this.http.delete(environment.baseUrl + 'admin/removelecturer/' + id);
  }

  deleteAssignation(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/assignedlecturers/remove', values);
  }

  deleteStudentFromGroup(studentId: number)
  {
    return this.http.delete(environment.baseUrl + 'admin/groups/students/remove/' + studentId); 
  }

  deleteAssignedSubject(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/assignedsubjects/delete', values); 
  }

  assignStudentToGroup(values: any)
  {
    return this.http.put(environment.baseUrl + 'admin/groups/students/assigntogroup', values);
  }

  assignSubjectToGroup(values: any)
  {
    return this.http.post(environment.baseUrl + 'admin/assignsubjecttogroup', values);
  }
}
