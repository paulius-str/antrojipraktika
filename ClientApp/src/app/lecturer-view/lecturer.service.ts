import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IGrade } from '../models/grade';
import { ISubject } from '../models/subject';
import { IUserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class LecturerService {

  constructor(private http: HttpClient) { }

  getSubjects() 
  {
    return this.http.get<ISubject[]>(environment.baseUrl + 'lecturer/getsubjects');
  }

  getStudentsForSubject(subjectId: number)
  {
    return this.http.get<IUserModel[]>(environment.baseUrl + 'lecturer/getstudents/' + subjectId);
  }

  getGrade(values: any)
  {
    return this.http.post<IGrade>(environment.baseUrl + 'lecturer/getgrade', values);
  }

  setGrade(values: any)
  {
    return this.http.put<IGrade>(environment.baseUrl + 'lecturer/setgrade', values);
  }
}
